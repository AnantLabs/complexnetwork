using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Numerics;

using Core;
using Core.Enumerations;
using Core.Attributes;
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
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument(true);
                writer.WriteStartElement("Research");

                SaveResearchInfo(result.ResearchID, result.ResearchName, 
                    result.ResearchType, result.ModelType, result.RealizationCount);
                SaveResearchParameters(result.ResearchParameterValues);
                SaveGenerationParameters(result.GenerationParameterValues);

                writer.WriteStartElement("Ensembles");
                for (int i = 0; i < result.EnsembleResults.Count; ++i)
                {
                    SaveEnsembleResult(result.EnsembleResults[i], i);
                }
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        private void SaveResearchInfo(Guid researchID,
            string researchName,
            ResearchType rType,
            ModelType mType,
            int realizationCount)
        {
            writer.WriteElementString("ResearchID", researchID.ToString());
            writer.WriteElementString("ResearchName", researchName);
            writer.WriteElementString("ResearchType", rType.ToString());
            writer.WriteElementString("ModelType", mType.ToString());
            writer.WriteElementString("RealizationCount", realizationCount.ToString());
            writer.WriteElementString("Date", DateTime.Now.ToString());
        }

        private void SaveResearchParameters(Dictionary<ResearchParameter, object> p)
        {
            writer.WriteStartElement("ResearchParameterValues");
            foreach (ResearchParameter rp in p.Keys)
            {
                writer.WriteStartElement("ResearchParameter");
                writer.WriteAttributeString("name", rp.ToString());
                writer.WriteAttributeString("value", p[rp].ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void SaveGenerationParameters(Dictionary<GenerationParameter, object> p)
        {
            writer.WriteStartElement("GenerationParameterValues");
            foreach (GenerationParameter gp in p.Keys)
            {
                writer.WriteStartElement("GenerationParameter");
                writer.WriteAttributeString("name", gp.ToString());
                writer.WriteAttributeString("value", p[gp].ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void SaveEnsembleResult(EnsembleResult e, int id)
        {
            writer.WriteStartElement("Ensemble");
            writer.WriteAttributeString("id", id.ToString());

            foreach (AnalyzeOption opt in e.Result.Keys)
            {
                AnalyzeOptionInfo info = ((AnalyzeOptionInfo[])opt.GetType().GetCustomAttributes(typeof(AnalyzeOptionInfo), false))[0];
                OptionType optionType = info.OptionType;

                switch(optionType)
                {
                    case OptionType.Global:
                        writer.WriteElementString(opt.ToString(), e.Result[opt].ToString());
                        break;
                    case OptionType.ValueList:
                        writer.WriteStartElement(opt.ToString());
                        SaveValueList(info, e.Result[opt]);                            
                        writer.WriteEndElement();
                        break;
                    case OptionType.Distribution:
                        writer.WriteStartElement(opt.ToString());
                        SaveDistribution(info, e.Result[opt]);
                        writer.WriteEndElement();
                        break;
                    default:
                        break;
                }
            }

            writer.WriteEndElement();
        }

        private void SaveValueList(AnalyzeOptionInfo info, Object value)
        {
            if (info.ResultType.Equals(typeof(List<Double>)))
            {
                List<Double> l = value as List<Double>;
                foreach (Double d in l)
                    writer.WriteElementString("Value", d.ToString());
            }
        }

        private void SaveDistribution(AnalyzeOptionInfo info, Object value)
        {
            if (info.ResultType.Equals(typeof(SortedDictionary<Double, UInt32>)))
            {
                SortedDictionary<Double, UInt32> l = value as SortedDictionary<Double, UInt32>;
                foreach (Double d in l.Keys)
                {
                    writer.WriteStartElement("pair");
                    writer.WriteAttributeString(info.XAxisName, d.ToString());
                    writer.WriteAttributeString(info.YAxixName, l[d].ToString());
                    writer.WriteEndElement();
                }
            }
            else if (info.ResultType.Equals(typeof(SortedDictionary<UInt32, UInt32>)))
            {
                SortedDictionary<UInt32, UInt32> l = value as SortedDictionary<UInt32, UInt32>;
                foreach (UInt32 d in l.Keys)
                {
                    writer.WriteStartElement("pair");
                    writer.WriteAttributeString(info.XAxisName, d.ToString());
                    writer.WriteAttributeString(info.YAxixName, l[d].ToString());
                    writer.WriteEndElement();
                }
            }
            else if (info.ResultType.Equals(typeof(SortedDictionary<UInt16, BigInteger>)))
            {
                SortedDictionary<UInt16, BigInteger> l = value as SortedDictionary<UInt16, BigInteger>;
                foreach (UInt16 d in l.Keys)
                {
                    writer.WriteStartElement("pair");
                    writer.WriteAttributeString(info.XAxisName, d.ToString());
                    writer.WriteAttributeString(info.YAxixName, l[d].ToString());
                    writer.WriteEndElement();
                }
            }
        }
    }
}
