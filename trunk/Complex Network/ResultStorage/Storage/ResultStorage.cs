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
using Model.StaticModel;

namespace ResultStorage.Storage
{
    public abstract class ResultStorage : IResultStorage
    {
        protected int GetModelID(Type ModelType)
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
                case "StaticcModel":
                    return 7;
                default:
                    return 0;
            }

        }

        protected Type GetModelType(int ModelTypeID)
        {
            switch (ModelTypeID)
            {
                case 1:
                    return typeof(Model.HierarchicModel.HierarchicModel);
                case 2:
                    return typeof(BAModel);
                case 3:
                    return typeof(ParisiHierarchicModel);
                case 4:
                    return typeof(ERModel);
                case 5:
                    return typeof(WSModel);
                case 6:
                    return typeof(Model.NonRegularHierarchicModel.NonRegularHierarchicModel);
                case 7:
                    return typeof(StaticModel);
                default:
                    return null;
            }
        }

        public abstract void Save(ResultAssembly assembly);

        public abstract void Delete(Guid assemblyID);

        public abstract ResultAssembly Load(Guid assemblyID);

        public abstract List<ResultAssembly> LoadAllAssemblies();

        public List<ResultAssembly> LoadAssembliesByModel(string modelName)
        {
            List<ResultAssembly> allResults = LoadAllAssemblies();
            List<ResultAssembly> results = new List<ResultAssembly>();
            foreach (ResultAssembly result in allResults)
            {
                if (result.ModelName == modelName)
                {
                    results.Add(result);
                }
            }
            return results;
        }

    }
}
