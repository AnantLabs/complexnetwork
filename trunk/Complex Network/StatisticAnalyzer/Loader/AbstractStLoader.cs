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
    // Абстрактный класс для загрузки сборок и информации из хранилища данных.
    // Используется для последующего статистического анализа.
    public abstract class AbstractStLoader
    {
        // Оранизация работы с хранилищем данных.
        protected IResultStorage resultStorage;
        // Имя модели анализируемых сборок.
        protected string modelName;

        // Конструктор по-умолчанию.
        public AbstractStLoader() { }

        // Свойство для доступа к имени модели. Только передача значения. 
        public abstract string ModelName { set;}

        // Возвращает имена job-oв сборок (выбранных по имени модели).
        public abstract object[] GetAvailableJobNames();
        // Удаляет сборку по имени job-а.
        public abstract void DeleteJob(string jobName);
        // Возвращает значение данного параметра генерации для данного job-а.
        public abstract string GetParameterValue(string jobName, GenerationParam p);
        // Возвращает все значения данного параметра генерации из всех сборок .
        // (выбранных по имени модели).
        public abstract List<string> GetParameterValues(GenerationParam p);
        // Возвращает все значения параметра генерации p из тех сборок, 
        // для которых значения параметров генерации соответствуют данным значениям (values).
        // (из сборок выбранных по имени модели).
        public abstract List<string> GetParameterValues(Dictionary<GenerationParam, string> values,
            GenerationParam p);
        // Возвращает число реализаций для сборки по имени job-а.
        public abstract int GetRealizationCount(string jobName);
        // Возвращает сборку с данным именем job-а.
        public abstract ResultAssembly SelectAssemblyByJob(string jobName);
        // Возвращает список сборок с данными параметрами (генерации и анализа).
        // Если (allAssemblies == true), то возвращаются все сборки с данными параметрами.
        // Если (allAssemblies == false), то возвращается первая соответствующая сборка.
        public abstract List<ResultAssembly> SelectAssemblyByParameters(
            Dictionary<GenerationParam, string> gValues,
            Dictionary<AnalyzeOptionParam, string> aValues,
            bool allAssemblies);

        // Инициализация обьекта для работы с хранилищем данных.
        protected abstract void InitStorage();
    }
}
