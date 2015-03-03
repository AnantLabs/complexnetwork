﻿using System;
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
    public class XMLResultStorage : AbstractResultStorage
    {
        private XmlWriter writer;
        private XmlReader reader;

        /// <summary>
        /// 
        /// </summary>
        private SortedDictionary<Guid, string> existingFileNames; 

        public XMLResultStorage(string str) : base(str) 
        {
            // TODO maybe exception will be thrown
            if (!storageStr.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                storageStr += Path.DirectorySeparatorChar;
            }
        }

        public override StorageType GetStorageType()
        {
            return StorageType.XMLStorage;
        }

        public override void Save(ResearchResult result)
        {
            if (!Directory.Exists(storageStr))
            {
                Directory.CreateDirectory(storageStr);
            }

            string fileName = storageStr + result.ResearchName;
            if (File.Exists(fileName + ".xml"))
                fileName += result.ResearchID;

            using (writer = XmlWriter.Create(fileName + ".xml"))
            {
                writer.WriteStartDocument(true);
                writer.WriteStartElement("Research");

                SaveResearchInfo(result.ResearchID, result.ResearchName, 
                    result.ResearchType, result.ModelType, result.RealizationCount, 
                    result.Date, result.Size, result.Edges);
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

        public override void Delete(Guid researchID)
        {
            if (existingFileNames != null && existingFileNames.Keys.Contains(researchID))
            {
                File.Delete(existingFileNames[researchID]);
                existingFileNames.Remove(researchID);
            }
            else
            {
                string fileNameToDelete = FileNameByGuid(researchID);
                if(fileNameToDelete != null)
                    File.Delete(fileNameToDelete);
            }   
        }

        public override List<ResearchResult> LoadAllResearchInfo()
        {
            existingFileNames = new SortedDictionary<Guid, string>();
            List<ResearchResult> researchInfos = new List<ResearchResult>();

            ResearchResult singleResearchInfo = null;
            foreach (string fileName in Directory.GetFiles(storageStr, "*.xml",
                SearchOption.TopDirectoryOnly))
            {
                singleResearchInfo = new ResearchResult();
                using (reader = XmlReader.Create(fileName))
                {
                    try
                    {
                        while (reader.Read() && 
                            (reader.NodeType != XmlNodeType.Element || 
                            reader.Name == "Research")) { }

                        LoadResearchInfo(singleResearchInfo);
                        LoadResearchParameters(singleResearchInfo);
                        LoadGenerationParameters(singleResearchInfo);

                        researchInfos.Add(singleResearchInfo);
                        existingFileNames.Add(singleResearchInfo.ResearchID, fileName);
                    }
                    catch (SystemException)
                    {
                        continue;
                    }
                }
            }

            return researchInfos;
        }

        public override ResearchResult Load(Guid researchID)
        {
            ResearchResult r = null;

            string fileNameToLoad = null;
            if (existingFileNames != null && existingFileNames.Keys.Contains(researchID))
                fileNameToLoad = existingFileNames[researchID];
            else
                fileNameToLoad = FileNameByGuid(researchID);

            if (fileNameToLoad != null)
            {
                r = new ResearchResult();
                using (reader = XmlReader.Create(fileNameToLoad))
                {
                    try
                    {
                        while (reader.Read() &&
                            (reader.NodeType != XmlNodeType.Element ||
                            reader.Name == "Research")) { }

                        LoadResearchInfo(r);
                        LoadResearchParameters(r);
                        LoadGenerationParameters(r);
                        LoadEnsembleResults(r);
                    }
                    catch (SystemException ex)
                    {
                        Console.WriteLine(ex.Data);
                    }
                }
            }

            return r;
        }

        #region Utilities

        #region Save

        private void SaveResearchInfo(Guid researchID,
            string researchName,
            ResearchType rType,
            ModelType mType,
            int realizationCount,
            DateTime date,
            UInt32 size,
            Double edges)
        {
            writer.WriteElementString("ResearchID", researchID.ToString());
            writer.WriteElementString("ResearchName", researchName);
            writer.WriteElementString("ResearchType", rType.ToString());
            writer.WriteElementString("ModelType", mType.ToString());
            writer.WriteElementString("RealizationCount", realizationCount.ToString());
            writer.WriteElementString("Date", date.ToString());
            writer.WriteElementString("Size", size.ToString());
            writer.WriteElementString("Edges", edges.ToString());
        }

        private void SaveResearchParameters(Dictionary<ResearchParameter, object> p)
        {
            writer.WriteStartElement("ResearchParameterValues");
            foreach (ResearchParameter rp in p.Keys)
            {
                if (p[rp] != null)
                {
                    writer.WriteStartElement("ResearchParameter");
                    writer.WriteAttributeString("name", rp.ToString());
                    writer.WriteAttributeString("value", p[rp].ToString());
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }

        private void SaveGenerationParameters(Dictionary<GenerationParameter, object> p)
        {
            writer.WriteStartElement("GenerationParameterValues");
            foreach (GenerationParameter gp in p.Keys)
            {
                if (p[gp] != null)
                {
                    writer.WriteStartElement("GenerationParameter");
                    writer.WriteAttributeString("name", gp.ToString());
                    writer.WriteAttributeString("value", p[gp].ToString());
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }

        private void SaveEnsembleResult(EnsembleResult e, int id)
        {
            writer.WriteStartElement("Ensemble");
            writer.WriteAttributeString("id", id.ToString());

            foreach (AnalyzeOption opt in e.Result.Keys)
            {
                AnalyzeOptionInfo info = ((AnalyzeOptionInfo[])opt.GetType().GetField(opt.ToString()).GetCustomAttributes(typeof(AnalyzeOptionInfo), false))[0];
                OptionType optionType = info.OptionType;

                switch (optionType)
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
                    case OptionType.Trajectory:
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
            if (info.EnsembleResultType.Equals(typeof(List<Double>)))
            {
                List<Double> l = value as List<Double>;
                foreach (Double d in l)
                    writer.WriteElementString("Value", d.ToString());
            }
        }

        private void SaveDistribution(AnalyzeOptionInfo info, Object value)
        {
            if (info.EnsembleResultType.Equals(typeof(SortedDictionary<Double, Double>)))
            {
                SortedDictionary<Double, Double> l = value as SortedDictionary<Double, Double>;
                foreach (Double d in l.Keys)
                {
                    writer.WriteStartElement("pair");
                    writer.WriteAttributeString(info.XAxisName, d.ToString());
                    writer.WriteAttributeString(info.YAxixName, l[d].ToString());
                    writer.WriteEndElement();
                }
            }
            else if (info.EnsembleResultType.Equals(typeof(SortedDictionary<UInt32, Double>)))
            {
                SortedDictionary<UInt32, Double> l = value as SortedDictionary<UInt32, Double>;
                foreach (UInt32 d in l.Keys)
                {
                    writer.WriteStartElement("pair");
                    writer.WriteAttributeString(info.XAxisName, d.ToString());
                    writer.WriteAttributeString(info.YAxixName, l[d].ToString());
                    writer.WriteEndElement();
                }
            }
            else if (info.EnsembleResultType.Equals(typeof(SortedDictionary<UInt16, Double>)))
            {
                SortedDictionary<UInt16, Double> l = value as SortedDictionary<UInt16, Double>;
                foreach (UInt16 d in l.Keys)
                {
                    writer.WriteStartElement("pair");
                    writer.WriteAttributeString(info.XAxisName, d.ToString());
                    writer.WriteAttributeString(info.YAxixName, l[d].ToString());
                    writer.WriteEndElement();
                }
            }
        }

        #endregion

        #region Load

        private void LoadResearchInfo(ResearchResult r)
        {
            if (reader.Name == "ResearchID")
                r.ResearchID = new Guid(reader.ReadElementString());
            if (reader.Name == "ResearchName")
                r.ResearchName = reader.ReadElementString();
            if (reader.Name == "ResearchType")
                r.ResearchType = (ResearchType)Enum.Parse(typeof(ResearchType), reader.ReadElementString());
            if (reader.Name == "ModelType")
                r.ModelType = (ModelType)Enum.Parse(typeof(ModelType), reader.ReadElementString());
            if (reader.Name == "RealizationCount")
                r.RealizationCount = Int32.Parse(reader.ReadElementString());
            if (reader.Name == "Date")
                r.Date = DateTime.Parse(reader.ReadElementString());
            if (reader.Name == "Size")
                r.Size = UInt32.Parse(reader.ReadElementString());
        }

        private void LoadResearchParameters(ResearchResult r)
        {
            while (reader.Read())
            {
                if (reader.Name == "ResearchParameter")
                {
                    reader.MoveToAttribute("name");
                    ResearchParameter rp = (ResearchParameter)Enum.Parse(typeof(ResearchParameter), reader.ReadContentAsString());

                    reader.MoveToAttribute("value");
                    ResearchParameterInfo rpInfo = (ResearchParameterInfo)(rp.GetType().GetField(rp.ToString()).GetCustomAttributes(typeof(ResearchParameterInfo), false)[0]);
                    if (rpInfo.Type.Equals(typeof(UInt32)))
                        r.ResearchParameterValues.Add(rp, UInt32.Parse(reader.Value));
                    else if (rpInfo.Type.Equals(typeof(Single)))
                        r.ResearchParameterValues.Add(rp, Single.Parse(reader.Value));
                    else if (rpInfo.Type.Equals(typeof(Boolean)))
                        r.ResearchParameterValues.Add(rp, Boolean.Parse(reader.Value));
                }
                else if (reader.Name == "GenerationParameterValues")
                    break;
            }
        }

        private void LoadGenerationParameters(ResearchResult r)
        {
            while (reader.Read())
            {
                if (reader.Name == "GenerationParameter")
                {
                    reader.MoveToAttribute("name");
                    GenerationParameter gp = (GenerationParameter)Enum.Parse(typeof(GenerationParameter), reader.ReadContentAsString());

                    reader.MoveToAttribute("value");
                    GenerationParameterInfo gpInfo = (GenerationParameterInfo)(gp.GetType().GetField(gp.ToString()).GetCustomAttributes(typeof(GenerationParameterInfo), false)[0]);
                    if (gpInfo.Type.Equals(typeof(UInt16)))
                        r.GenerationParameterValues.Add(gp, UInt16.Parse(reader.Value));
                    else if (gpInfo.Type.Equals(typeof(Single)))
                        r.GenerationParameterValues.Add(gp, Single.Parse(reader.Value));
                    else if (gpInfo.Type.Equals(typeof(Boolean)))
                        r.GenerationParameterValues.Add(gp, Boolean.Parse(reader.Value));
                    else if (gpInfo.Type.Equals(typeof(UInt32)))
                        r.GenerationParameterValues.Add(gp, UInt32.Parse(reader.Value));
                }
                if (reader.Name == "Ensembles")
                    break;
            }
        }

        private void LoadEnsembleResults(ResearchResult r)
        {
            while (reader.Read())
            {
                if (reader.Name == "Ensemble" && !reader.IsEmptyElement)
                {
                    EnsembleResult e = new EnsembleResult();
                    e.NetworkSize = r.Size;
                    e.EdgesCount = r.Edges;
                    e.Result = new Dictionary<AnalyzeOption, Object>();

                    reader.Read();
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        AnalyzeOption opt = (AnalyzeOption)Enum.Parse(typeof(AnalyzeOption), reader.Name);
                        AnalyzeOptionInfo optInfo = (AnalyzeOptionInfo)(opt.GetType().GetField(opt.ToString()).GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);
                        switch (optInfo.OptionType)
                        {
                            case OptionType.Global:
                                if (optInfo.EnsembleResultType.Equals(typeof(Double)))
                                    e.Result.Add(opt, reader.ReadElementContentAsDouble());
                                break;
                            case OptionType.ValueList:
                                e.Result.Add(opt, LoadValueList(optInfo));
                                reader.Read();
                                break;
                            case OptionType.Distribution:
                            case OptionType.Trajectory:
                                e.Result.Add(opt, LoadDistribution(optInfo));
                                reader.Read();
                                break;
                            default:
                                break;
                        }
                    }

                    r.EnsembleResults.Add(e);
                }
            }
        }

        private Object LoadValueList(AnalyzeOptionInfo info)
        {
            if (info.EnsembleResultType.Equals(typeof(List<Double>)))
            {
                List<Double> valueList = new List<Double>();
                reader.Read();
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    valueList.Add(Double.Parse(reader.ReadElementString()));
                }
                return valueList;
            }

            return null;
        }

        private Object LoadDistribution(AnalyzeOptionInfo info)
        {
            if (info.EnsembleResultType.Equals(typeof(SortedDictionary<Double, Double>)))
            {
                SortedDictionary<Double, Double> d = new SortedDictionary<Double, Double>();
                double first, second;
                while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.MoveToFirstAttribute();
                    first = Double.Parse(reader.Value);
                    reader.MoveToNextAttribute();
                    second = Double.Parse(reader.Value);
                    d.Add(first, second);
                }
                return d;
            }
            else if (info.EnsembleResultType.Equals(typeof(SortedDictionary<UInt32, Double>)))
            {
                SortedDictionary<UInt32, Double> d = new SortedDictionary<UInt32, Double>();
                UInt32 first;
                double second;
                while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.MoveToFirstAttribute();
                    first = UInt32.Parse(reader.Value);
                    reader.MoveToNextAttribute();
                    second = Double.Parse(reader.Value);
                    d.Add(first, second);
                }
                return d;
            }
            else if (info.EnsembleResultType.Equals(typeof(SortedDictionary<UInt16, Double>)))
            {
                SortedDictionary<UInt16, Double> d = new SortedDictionary<UInt16, Double>();
                UInt16 first;
                double second;
                while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.MoveToFirstAttribute();
                    first = UInt16.Parse(reader.Value);
                    reader.MoveToNextAttribute();
                    second = Double.Parse(reader.Value);
                    d.Add(first, second);
                }
                return d;
            }

            return null;
        }

        #endregion

        private string FileNameByGuid(Guid id)
        {
            foreach (string fileName in Directory.GetFiles(storageStr, "*.xml",
                SearchOption.TopDirectoryOnly))
            {
                using (reader = XmlReader.Create(fileName))
                {
                    try
                    {
                        while (reader.Read() &&
                            (reader.NodeType != XmlNodeType.Element ||
                            reader.Name == "Research")) { }

                        if (reader.Name == "ResearchID")
                        {
                            if (id == Guid.Parse(reader.ReadElementString()))
                            {
                                return fileName;
                            }
                        }
                    }
                    catch (SystemException)
                    {
                        continue;
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
