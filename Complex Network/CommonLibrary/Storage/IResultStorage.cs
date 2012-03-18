namespace RandomGraph.Common.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RandomGraph.Common.Model.Result;
    using CommonLibrary.Model.Result;

    public interface IResultStorage
    {
        void Save(ResultAssembly assembly);
        void Delete(Guid assemblyID);
        ResultAssembly Load(Guid assemblyID);
        List<ResultAssembly> LoadAllAssemblies();
        List<ResultAssembly> LoadAssembliesByModel(string modelName);
    }
}

