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
                    SaveEnsembleResult(result.EnsembleResults[i], i, result.AnalyzeOption);
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

        private void SaveEnsembleResult(EnsembleResult e, int id, AnalyzeOption opts)
        {
            writer.WriteStartElement("Ensemble");
            writer.WriteAttributeString("id", id.ToString());

            /*if ((opts & AnalyzeOption.AvgPathLength) == AnalyzeOption.AvgPathLength)
            {
                writer.WriteElementString("AvgPathLength", e.AvgPathLength.ToString());
            }

            if ((opts & AnalyzeOption.Diameter) == AnalyzeOption.Diameter)
            {
                writer.WriteElementString("Diameter", e.Diameter.ToString());
            }

            if ((opts & AnalyzeOption.AvgDegree) == AnalyzeOption.AvgDegree)
            {
                writer.WriteElementString("AvgDegree", e.AvgDegree.ToString());
            }

            if ((opts & AnalyzeOption.AvgClusteringCoefficient) == AnalyzeOption.AvgClusteringCoefficient)
            {
                writer.WriteElementString("AvgClusteringCoefficient", e.AvgClusteringCoefficient.ToString());
            }

            if ((opts & AnalyzeOption.Cycles3) == AnalyzeOption.Cycles3)
            {
                writer.WriteElementString("Cycles3", e.Cycles3.ToString());
            }

            if ((opts & AnalyzeOption.Cycles4) == AnalyzeOption.Cycles4)
            {
                writer.WriteElementString("Cycles4", e.Cycles4.ToString());
            }

            if ((opts & AnalyzeOption.Cycles3Eigen) == AnalyzeOption.Cycles3Eigen)
            {
                writer.WriteElementString("Cycles3Eigen", e.Cycles3Eigen.ToString());
            }

            if ((opts & AnalyzeOption.Cycles3Eigen) == AnalyzeOption.Cycles3Eigen)
            {
                writer.WriteElementString("Cycles3Eigen", e.Cycles3Eigen.ToString());
            }

            if ((opts & AnalyzeOption.EigenValues) == AnalyzeOption.EigenValues)
            {
                writer.WriteStartElement("EigenValues");
                foreach(Double d in e.EigenValues)
                    writer.WriteElementString("EigenValue", d.ToString());
                writer.WriteEndElement();
            }

            if ((opts & AnalyzeOption.EigenDistanceDistribution) == AnalyzeOption.EigenDistanceDistribution)
            {
                writer.WriteStartElement("EigenDistanceDistribution");
                foreach (Double d in e.EigenDistanceDistribution.Keys)
                {
                    writer.WriteStartElement("pair");
                    writer.WriteAttributeString("distance", d.ToString());
                    writer.WriteAttributeString("count", e.EigenDistanceDistribution[d].ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            if ((opts & AnalyzeOption.DegreeDistribution) == AnalyzeOption.DegreeDistribution)
            {
                writer.WriteStartElement("DegreeDistribution");
                foreach (UInt32 d in e.DegreeDistribution.Keys)
                {
                    writer.WriteStartElement("pair");
                    writer.WriteAttributeString("degree", d.ToString());
                    writer.WriteAttributeString("count", e.DegreeDistribution[d].ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            if ((opts & AnalyzeOption.DegreeDistribution) == AnalyzeOption.DegreeDistribution)
            {
                writer.WriteStartElement("DegreeDistribution");
                foreach (UInt32 d in e.DegreeDistribution.Keys)
                {
                    writer.WriteStartElement("pair");
                    writer.WriteAttributeString("degree", d.ToString());
                    writer.WriteAttributeString("count", e.DegreeDistribution[d].ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            if ((opts & AnalyzeOption.Diameter) == AnalyzeOption.Diameter)
            {
                networkAnalyzer.CalculateDiameter();
            }

            if ((opts & AnalyzeOption.DistanceDistribution) == AnalyzeOption.DistanceDistribution)
            {
                networkAnalyzer.CalculateDistanceDistribution();
            }

            if ((opts & AnalyzeOption.EigenDistanceDistribution) == AnalyzeOption.EigenDistanceDistribution)
            {
                networkAnalyzer.CalculateEigenDistanceDistribution();
            }

            if ((opts & AnalyzeOption.EigenValues) == AnalyzeOption.EigenValues)
            {
                networkAnalyzer.CalculateEigenValues();
            }

            if ((opts & AnalyzeOption.TriangleByVertexDistribution) == AnalyzeOption.TriangleByVertexDistribution)
            {
                networkAnalyzer.CalculateTriangleByVertexDistribution();
            }*/

            writer.WriteEndElement();
        }
    }
}
