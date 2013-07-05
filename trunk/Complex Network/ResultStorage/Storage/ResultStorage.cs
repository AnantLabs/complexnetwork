using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RandomGraph.Common.Storage;
using CommonLibrary.Model.Result;
using Model.HierarchicModel;
using Model.ParisiHierarchicModel;
using Model.NonRegularHierarchicModel;
using Model.BAModel;
using Model.ERModel;
using Model.WSModel;

namespace ResultStorage.Storage
{
    // Базовый абстрактный класс для имплементации хранилища данных.
    public abstract class ResultStorage : IResultStorage
    {
        // Возвращает тип по имени модели графа.
        public static Type GetModelType(string modelName)
        {
            switch (modelName)
            {
                case "HierarchicModel":
                    return typeof(HierarchicModel);
                case "BAModel":
                    return typeof(Model.BAModel.BAModel);
                case "HierarchicModelParizi":
                    return typeof(ParisiHierarchicModel);
                case "WSModel":
                    return typeof(WSModel);
                case "ERModel":
                    return typeof(ERModel);
                case "NonRegularHierarchicModel":
                    return typeof(NonRegularHierarchicModel);
                default:
                    throw new SystemException("Model Type is not recognized.");
            }
        }

        // Возвращает идентификатор модели графа по типу.
        public static int GetModelID(Type ModelType)
        {
            string name = ModelType.Name;
            switch (name)
            {
                case "HierarchicModel":
                    return 1;
                case "BAModel":
                    return 2;
                case "HierarchicModelParizi":
                    return 3;
                case "WSModel":
                    return 4;
                case "ERModel":
                    return 5;
                case "NonRegularHierarchicModel":
                    return 6;
                default:
                    return 0;
            }

        }

        // Возвращает тип по идентификатору модели графа.
        public static Type GetModelType(int ModelTypeID)
        {
            switch (ModelTypeID)
            {
                case 1:
                    return typeof(HierarchicModel);
                case 2:
                    return typeof(Model.BAModel.BAModel);
                case 3:
                    return typeof(ParisiHierarchicModel);
                case 4:
                    return typeof(WSModel);
                case 5:
                    return typeof(ERModel);
                case 6:
                    return typeof(NonRegularHierarchicModel);
                default:
                    return null;
            }
        }

        // Абстрактный метод для сохранения результатов анализа (сборки) в хранилище данных.
        public abstract void Save(ResultAssembly assembly);

        // Абстрактный метод для удаления сборки из хранилища данных по данному идентификатору сборки.
        public abstract void Delete(Guid assemblyID);

        // Абстрактный метод для загрузки сборки из хранилища данных по данному идентификатору сборки.
        public abstract ResultAssembly Load(Guid assemblyID);

        // Абстрактный метод для загрузки всех сборок из хранилища данных.
        public abstract List<ResultAssembly> LoadAllAssemblies();

        // Абстрактный метод для загрузки всех сборок из хранилища данных по данному имени модели.
        public abstract List<ResultAssembly> LoadAssembliesByModel(string modelName);
    }
}
