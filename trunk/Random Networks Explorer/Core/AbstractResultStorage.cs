using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Result;

namespace Core
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractResultStorage
    {
        protected string storageStr;

        public string StorageString
        {
            get { return storageStr; }
        }

        public AbstractResultStorage(string str)
        {
            storageStr = str;
        }

        /// <summary>
        /// Returns storage type.
        /// </summary>
        /// <returns>Storage type.</returns>
        public abstract StorageType GetStorageType();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public abstract void Save(ResearchResult result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="researchID"></param>
        public abstract void Delete(Guid researchID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract List<ResearchResult> LoadAllResearchInfo();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="researchID"></param>
        /// <returns></returns>
        public abstract ResearchResult Load(Guid researchID);
    }
}
