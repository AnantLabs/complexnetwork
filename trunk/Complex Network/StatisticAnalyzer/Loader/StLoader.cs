using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using AnalyzerFramework.Manager.ModelRepo;
using CommonLibrary.Model.Result;
using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Storage;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model;
using ResultStorage.Storage;

namespace StatisticAnalyzer.Loader
{
    // Класс для загрузки списка сборок (для последующего статистического анализа).
    public class StLoader
    {
        // Статическая часть
        // Dictionary имен доступных моделей и их описаний.
        public static Dictionary<string, Type> models;
        // Возвращает массив имен доступных моделей.
        public static object[] GetAvailableModelNames()
        {
            models = new Dictionary<string, Type>();
            List<Type> availableModelFactoryTypes = ModelRepository.GetInstance().GetAvailableModelTypes();
            foreach (Type modelType in availableModelFactoryTypes)
            {
                string modelName = modelType.Name;
                models.Add(modelName, modelType);
            }

            return models.Keys.ToArray();
        }
        
        // Уровень обьекта.
        // Оранизация работы с хранилищем данных.
        private IResultStorage resultStorage; 
        // Список сборок (выбранный по критериям).
        private List<ResultAssembly> assemblies;
        // Список строковых идентификаторов сборок (assemblies).
        private List<string> assembliesID;
 
        // Имя модели анализируемтых сборок.
        private string modelName = "";

        // Конструктор по-умолчания. 
        // Организует инициализацию обьекта для работы с хранилищем данных.
        // Организует загрузку всех сборок и их идентификаторов (выбранных по имени модели).
        public StLoader()
        {
            InitStorage();
            InitAssemblies();
        }

        // Свойство для доступа к списку сборок. Только чтение.
        public List<ResultAssembly> Assemblies
        {
            get { return assemblies; }
        }

        // Свойство для доступа к имени модели. Только передача значения. 
        public string ModelName
        {
            set
            {
                modelName = models[value].Name;
            }
        }

        // Инициализация обьекта для работы с хранилищем данных.
        public void InitStorage()
        {
            string provider = ConfigurationManager.AppSettings["Storage"];
            if (provider == "XmlProvider")
            {
                resultStorage = new XMLResultStorage(ConfigurationManager.AppSettings[provider]);
            }
            else
            {
                resultStorage = new SQLResultStorage(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[provider]]);
            }
        }

        // Организует загрузку всех сборок и их идентификаторов (выбранных по имени модели).
        public void InitAssemblies()
        {
            assemblies = resultStorage.LoadAssembliesByModel(modelName);
            InitAssembliesID();
        }

        // Возвращает имена job-oв сборок (выбранных по имени модели).
        public object[] GetAvailableJobs()
        {
            InitAssemblies();

            ArrayList result = new ArrayList();
            foreach (ResultAssembly r in assemblies)
                result.Add(r.Name);
            return result.ToArray();
        }

        // Удаляет сборку по имени job-а.
        public void DeleteJob(string jobName)
        {
            InitAssemblies();

            ResultAssembly result = assemblies.Find(i => i.Name == jobName);
            resultStorage.Delete(result.ID);
            InitAssemblies();
        }

        // Возвращает значение данного параметра генерации для данного job-а.
        public string GetParameterValue(string jobName, GenerationParam p)
        {
            try
            {
                ResultAssembly result = resultStorage.Load(assemblies.Find(i => i.Name == jobName).ID);
                return result.GenerationParams[p].ToString();
            }
            // Такая ситуация возникает при наличии xml-а с результатом статической генерации.
            catch(KeyNotFoundException)
            {
                return "Generation Parameter Error!";
            }
        }

        // Возвращает все значения данного параметра генерации из всех сборок.
        public List<string> GetParameterValues(GenerationParam p)
        {
            List<string> result = new List<string>();
            foreach (string resultName in assembliesID)
            {
                ResultAssembly r = resultStorage.Load(assemblies.Find(i => i.Name == resultName).ID);
                if(r.GenerationParams.Count != 0)
                    result.Add(r.GenerationParams[p].ToString());
                else
                    continue;
            }
            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        // Возвращает все значения параметра генерации parameter из тех сборок, 
        // для которых значения параметров генерации соответствуют данным значениям (values).
        public List<string> GetParameterValues(Dictionary<GenerationParam, string> values,
            GenerationParam parameter)
        {
            List<string> result = new List<string>();
            foreach (string resultName in assembliesID)
            {
                ResultAssembly r = resultStorage.Load(assemblies.Find(i => i.Name == resultName).ID);
                Dictionary<GenerationParam, string>.KeyCollection keys = values.Keys;
                bool b = true;
                foreach (GenerationParam key in keys)
                {
                    if (r.GenerationParams.Count != 0)
                        b = b && (r.GenerationParams[key].ToString() == values[key]);
                    else
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    result.Add(r.GenerationParams[parameter].ToString());
                }
            }
            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        // ??
        public List<string> GetOptionParameterValues(Dictionary<GenerationParam, string> gValues,
            AnalyzeOptionParam parameter)
        {
            List<string> result = new List<string>();
            foreach (string resultName in assembliesID)
            {
                ResultAssembly r = resultStorage.Load(assemblies.Find(i => i.Name == resultName).ID);
                Dictionary<GenerationParam, string>.KeyCollection gKeys = gValues.Keys;
                bool b = true;
                foreach (GenerationParam key in gKeys)
                {
                    if (r.GenerationParams.Count != 0)
                        b = b && (r.GenerationParams[key].ToString() == gValues[key]);
                    else
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    result.Add(r.AnalyzeOptionParams[parameter].ToString());
                }
            }

            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        // Возвращает список всех сборок.
        public List<ResultAssembly> SelectAllAssemblies()
        {
            List<ResultAssembly> result = new List<ResultAssembly>();

            foreach (string resultName in assembliesID)
            {
                result.Add(resultStorage.Load(assemblies.Find(i => i.Name == resultName).ID));
            }

            return result;
        }

        // Возвращает сборку, выбранную по имени job-а.
        public ResultAssembly SelectAssemblyByJob(string jobName)
        {
            return resultStorage.Load(assemblies.Find(i => i.Name == jobName).ID);
        }

        // Возвращает список сборок, выбранных по параметрам генерации.
        // Если allAssemblies == true, то в список поподают все соответствыющие сборки.
        // В обратном случае, только первая соответствующая сборка.
        // ??
        public List<ResultAssembly> SelectAssemblyByParameters(Dictionary<GenerationParam, string> gValues,
            Dictionary<AnalyzeOptionParam, string> aValues,
            bool allAssemblies)
        {
            List<ResultAssembly> result = new List<ResultAssembly>();

            foreach (string resultName in assembliesID)
            {
                ResultAssembly r = resultStorage.Load(assemblies.Find(i => i.Name == resultName).ID);

                Dictionary<GenerationParam, string>.KeyCollection keys = gValues.Keys;
                bool b = true;
                foreach (GenerationParam key in keys)
                {
                    if (r.GenerationParams.Count != 0)
                        b = b && (r.GenerationParams[key].ToString() == gValues[key]);
                    else
                    {
                        b = false;
                        break;
                    }
                }

                Dictionary<AnalyzeOptionParam, string>.KeyCollection aKeys = aValues.Keys;
                foreach (AnalyzeOptionParam key in aKeys)
                {
                    b = b && (r.AnalyzeOptionParams[key].ToString() == aValues[key]);
                }

                if (b)
                    result.Add(r);
            }

            return result;
        }

        public int GetRealizationCount(string jobName)
        {
            return Convert.ToInt32(resultStorage.Load(assemblies.Find(i => i.Name == jobName).ID).Results.Count);
        }

        // Utilities //

        private void InitAssembliesID()
        {
            assembliesID = new List<string>();
            foreach (ResultAssembly result in assemblies)
                assembliesID.Add(result.Name);
        }
    }
}
