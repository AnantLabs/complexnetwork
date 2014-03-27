﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Enumerations;
using Core.Result;

namespace Storage
{
    /// <summary>
    /// 
    /// </summary>
    class TXTResultStorage : AbstractResultStorage
    {
        public TXTResultStorage(string str) : base(str) { }

        public override void Save(ResearchResult result)
        {
            throw new NotImplementedException();
        }

        private void SaveResearchInfo(Guid researchID,
            string researchName,
            ResearchType rType,
            ModelType mType,
            int realizationCount)
        {
            throw new NotImplementedException();
        }

        private void SaveResearchParameters(Dictionary<ResearchParameter, object> p)
        {
            throw new NotImplementedException();
        }

        private void SaveGenerationParameters(Dictionary<GenerationParameter, object> p)
        {
            throw new NotImplementedException();
        }

        private void SaveResearchResult(ResearchResult r)
        {
            throw new NotImplementedException();
        }
    }
}
