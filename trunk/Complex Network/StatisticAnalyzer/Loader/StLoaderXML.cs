using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using CommonLibrary.Model.Result;
using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Storage;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model;
using ResultStorage.Storage;

namespace StatisticAnalyzer.Loader
{
    // Класс для загрузки сборок и информации из XML файлов.
    public class StLoaderXML : AbstractStLoader
    {
        // Список сборок (выбранный по критериям).
        private List<ResultAssembly> assemblies;
        // Список строковых идентификаторов сборок (assemblies).
        private List<string> assembliesID;

        // Конструктор по-умолчанию.
        public StLoaderXML() : base() 
        {
            InitStorage();
        }

        public override string  ModelName
        {
	        set 
            { 
                modelName = AvailableModels.models[value].Name;
                InitAssemblies();
            }
        }

        // Возвращает имена job-oв сборок (выбранных по имени модели).
        public override object[] GetAvailableJobNames()
        {
            InitAssemblies();

            ArrayList result = new ArrayList();
            foreach (ResultAssembly r in this.assemblies)
                result.Add(r.Name);
            return result.ToArray();
        }

        // Удаляет сборку по имени job-а.
        public override void DeleteJob(string jobName)
        {
            InitAssemblies();

            resultStorage.Delete(this.assemblies.Find(i => i.Name == jobName).ID);
            InitAssemblies();
        }

        // Возвращает значение данного параметра генерации для данного job-а.
        public override string GetParameterValue(string jobName, GenerationParam p)
        {
            try
            {
                ResultAssembly result = this.resultStorage.Load(this.assemblies.
                    Find(i => i.Name == jobName).ID);
                return result.GenerationParams[p].ToString();
            }
            // Такая ситуация возникает при наличии XML-а с результатом статической генерации.
            catch (KeyNotFoundException)
            {
                return "Generation Parameter Error!";
            }
        }

        // Возвращает все значения данного параметра генерации из всех сборок .
        // (выбранных по имени модели).
        public override List<string> GetParameterValues(GenerationParam p)
        {
            List<string> result = new List<string>();
            foreach (string resultName in this.assembliesID)
            {
                ResultAssembly r = this.resultStorage.Load(this.assemblies.
                    Find(i => i.Name == resultName).ID);
                if (r.GenerationParams.Count != 0)
                    result.Add(r.GenerationParams[p].ToString());
                else
                    continue;
            }
            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        // Возвращает все значения параметра генерации p из тех сборок, 
        // для которых значения параметров генерации соответствуют данным значениям (values).
        // (из сборок выбранных по имени модели).
        public override List<string> GetParameterValues(Dictionary<GenerationParam, string> values,
            GenerationParam p)
        {
            List<string> result = new List<string>();
            foreach (string resultName in this.assembliesID)
            {
                ResultAssembly r = this.resultStorage.Load(this.assemblies.
                    Find(i => i.Name == resultName).ID);
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
                    result.Add(r.GenerationParams[p].ToString());
                }
            }
            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        // Возвращает число реализаций для сборки по имени job-а.
        public override int GetRealizationCount(string jobName)
        {
            return this.resultStorage.Load(this.assemblies.
                Find(i => i.Name == jobName).ID).Results.Count;
        }

        // Возвращает сборку с данным именем job-а.
        public override ResultAssembly SelectAssemblyByJob(string jobName)
        {
            return this.resultStorage.Load(this.assemblies.Find(i => i.Name == jobName).ID);
        }

        // Возвращает список сборок с данными параметрами (генерации и анализа).
        // Если (allAssemblies == true), то возвращаются все сборки с данными параметрами.
        // Если (allAssemblies == false), то возвращается первая соответствующая сборка.
        public override List<ResultAssembly> SelectAssemblyByParameters(
            Dictionary<GenerationParam, string> gValues,
            Dictionary<AnalyzeOptionParam, string> aValues,
            bool allAssemblies)
        {
            List<ResultAssembly> result = new List<ResultAssembly>();

            foreach (string resultName in this.assembliesID)
            {
                ResultAssembly r = this.resultStorage.Load(this.assemblies.
                    Find(i => i.Name == resultName).ID);

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

                if (!allAssemblies && result.Count == 1)
                    break;
            }

            return result;
        }

        // Инициализация обьекта для работы с хранилищем данных.
        protected override void InitStorage()
        {
            this.resultStorage = new XMLResultStorage(ConfigurationManager.
                AppSettings[ConfigurationManager.AppSettings["Storage"]]);
        }

        // Утилиты.

        // Организует загрузку всех сборок и их идентификаторов (выбранных по имени модели).
        private void InitAssemblies()
        {
            this.assemblies = resultStorage.LoadAssembliesByModel(modelName);

            this.assembliesID = new List<string>();
            foreach (ResultAssembly result in this.assemblies)
                this.assembliesID.Add(result.Name);
        }
    }
}
