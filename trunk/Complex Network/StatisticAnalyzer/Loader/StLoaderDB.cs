using System;
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
    // Класс для загрузки сборок и информации из БД.
    public class StLoaderDB : AbstractStLoader
    {
        SQLResultStorage storage;

        // Конструктор по-умолчанию.
        public StLoaderDB() : base() 
        {
            InitStorage();
        }

        public override string ModelName
        {
            set
            {
                modelName = AvailableModels.models[value].Name;
            }
        }

        // Возвращает имена job-oв сборок (выбранных по имени модели).
        public override object[] GetAvailableJobNames()
        {
            return this.storage.GetJobNamesByModelID(AvailableModels.models[this.modelName]);
        }

        // Удаляет сборку по имени job-а.
        public override void DeleteJob(string jobName)
        {
            this.storage.Delete(jobName);
        }

        // Возвращает значение данного параметра генерации для данного job-а.
        public override string GetParameterValue(string jobName, GenerationParam p)
        {
            return this.storage.GetParameterValueByID(jobName, (int)p);
        }

        // Возвращает все значения данного параметра генерации из всех сборок .
        // (выбранных по имени модели).
        public override List<string> GetParameterValues(GenerationParam p)
        {
            return this.storage.GetParameterValuesByID(AvailableModels.models[this.modelName], (int)p);
        }

        // Возвращает все значения параметра генерации p из тех сборок, 
        // для которых значения параметров генерации соответствуют данным значениям (values).
        // (из сборок выбранных по имени модели).
        public override List<string> GetParameterValues(Dictionary<GenerationParam, string> values, 
            GenerationParam p)
        {
            Dictionary<int, string> idValues = new Dictionary<int, string>();
            foreach(GenerationParam param in values.Keys)
            {
                idValues.Add((int)param, values[param]);
            }
            return this.storage.GetParameterValuesByID(AvailableModels.models[this.modelName],
                idValues, (int)p);
        }

        // Возвращает число реализаций для сборки по имени job-а.
        public override int GetRealizationCount(string jobName)
        {
            return this.storage.GetRealizationCount(jobName);
        }

        // Возвращает сборку с данным именем job-а.
        public override ResultAssembly SelectAssemblyByJob(string jobName)
        {
            return this.storage.Load(this.storage.GetAssemblyIDByJobName(jobName));
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

            List<Guid> resultAssembliesID = this.storage.GetAssembliesID();
            foreach (Guid id in resultAssembliesID)
            {
                result.Add(this.storage.Load(id));
            }

            return result;
        }

        // Инициализация обьекта для работы с хранилищем данных.
        protected override void InitStorage()
        {
            this.resultStorage = new SQLResultStorage(ConfigurationManager.
                ConnectionStrings[ConfigurationManager.AppSettings[ConfigurationManager.
                AppSettings["Storage"]]]);

            this.storage = (SQLResultStorage)this.resultStorage;
        }
    }
}
