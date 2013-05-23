using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonLibrary.Model.Result;
using RandomGraph.Common.Storage;
using ResultStorage.Storage;

namespace ResultStorage.StorageConverter
{
    public abstract class AbstractStorageConverter
    {
        protected string rootPath;
        protected List<ResultAssembly> assembliesToConvert;

        public AbstractStorageConverter(string path)
        {
            rootPath = path;
            assembliesToConvert = new List<ResultAssembly>();
        }

        public abstract void ReadRootDirectory();

        // ??
        public void Save(IResultStorage storage, bool avg)
        {
            foreach (ResultAssembly assembly in assembliesToConvert)
            {
                if (avg)
                {
                    SQLResultStorage st = (SQLResultStorage)storage;
                    st.SaveTT(assembly);
                }
                else
                {
                    storage.Save(assembly);
                }
            }
        }
    }
}
