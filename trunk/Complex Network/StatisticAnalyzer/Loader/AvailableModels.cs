using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnalyzerFramework.Manager.ModelRepo;

namespace StatisticAnalyzer.Loader
{
    public static class AvailableModels
    {
        // Dictionary имен доступных моделей и их описаний.
        public static Dictionary<string, Type> models;

        static AvailableModels()
        {
            models = new Dictionary<string, Type>();
            List<Type> availableModelFactoryTypes =
                ModelRepository.GetInstance().GetAvailableModelTypes();
            foreach (Type modelType in availableModelFactoryTypes)
            {
                models.Add(modelType.Name, modelType);
            }
        }

        // Возвращает массив имен доступных моделей.
        public static object[] GetAvailableModelNames()
        {
            return models.Keys.ToArray();
        }
    }
}
