using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using Core;
using Core.Enumerations;
using Core.Result;

namespace Storage
{
    /// <summary>
    /// 
    /// </summary>
    class XMLResultStorage : AbstractResultStorage
    {
        XmlTextWriter writer;

        public XMLResultStorage(string str) : base(str) 
        {
            // TODO maybe exception will be thrown
            if (!storageStr.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                storageStr += Path.DirectorySeparatorChar;
            }
        }

        public override void Save(ResearchResult result)
        {
            if (!Directory.Exists(storageStr))
            {
                Directory.CreateDirectory(storageStr);
            }

            string fileName = storageStr + result.ResearchName;
            if (!File.Exists(fileName + ".xml"))
                fileName += result.ResearchID;

            using (writer = new XmlTextWriter(fileName + ".xml", Encoding.ASCII))
            {
                SaveResearchInfo(result.ResearchID, result.ResearchName, 
                    result.RType, result.MType, result.RealizationCount);
                SaveResearchParameters(result.ResearchParameterValues);
                SaveGenerationParameters(result.GenerationParameterValues);

                foreach (EnsembleResult e in result.EnsembleResults)
                    SaveEnsembleResult(e);
            }
        }

        public override ResearchResult Load()
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

        private void SaveEnsembleResult(EnsembleResult e)
        {
            throw new NotImplementedException();
        }        
    }
}
