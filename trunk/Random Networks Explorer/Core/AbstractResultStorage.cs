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

        public AbstractResultStorage(string str)
        {
            storageStr = str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public abstract void Save(ResearchResult result);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract ResearchResult Load();
    }
}
