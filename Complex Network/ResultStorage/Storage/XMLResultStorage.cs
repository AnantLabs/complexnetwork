using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Storage;
using RandomGraph.Common.Model.Result;
using CommonLibrary.Model.Result;
using System.IO;
using System.Xml;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using CommonLibrary.Model.Attributes;
using System.Globalization;
using log4net;

namespace ResultStorage.Storage
{

    public class XMLResultStorage : ResultStorage
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(XMLResultStorage));
        private string directory;

        public XMLResultStorage(string dir)
        {
            if (dir.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                this.directory = dir;
            }
            else
            {
                this.directory = dir + Path.DirectorySeparatorChar;
            }
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        #region IResultStorage Members

        public override void Save(ResultAssembly assembly)
        {
            using (XmlTextWriter writer = new XmlTextWriter(directory + assembly.ID.ToString() + ".xml", Encoding.ASCII))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument(true);
                writer.WriteStartElement("assembly");

                writer.WriteElementString("id", assembly.ID.ToString());
                writer.WriteElementString("name", assembly.Name);
                writer.WriteElementString("date", DateTime.Now.ToString());

                writer.WriteStartElement("graphmodel");
                writer.WriteAttributeString("id", GetModelID(assembly.ModelType).ToString());
                writer.WriteAttributeString("modelname", assembly.ModelType.Name);
                writer.WriteEndElement();

                writer.WriteStartElement("generationparams");
                foreach (GenerationParam genParameter in assembly.GenerationParams.Keys)
                {
                    writer.WriteStartElement("generationparam");
                    writer.WriteAttributeString("id", Convert.ToInt32(genParameter).ToString());
                    writer.WriteAttributeString("parametername", Enum.GetName(typeof(GenerationParam), genParameter));
                    writer.WriteAttributeString("value", assembly.GenerationParams[genParameter].ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("analyseresults");

                foreach (AnalizeResult result in assembly.Results)
                {
                    writer.WriteStartElement("instance");

                    writer.WriteStartElement("Model");
                    writer.WriteAttributeString("grapSize", result.graphSize.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("result");
                    foreach (AnalyseOptions analyseOption in result.Result.Keys)
                    {
                        writer.WriteStartElement("item");
                        writer.WriteAttributeString("option", analyseOption.ToString());
                        writer.WriteString(result.Result[analyseOption].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("vertexdegree");
                    foreach (int degree in result.VertexDegree.Keys)
                    {
                        writer.WriteStartElement("vd");
                        writer.WriteAttributeString("degree", degree.ToString());
                        writer.WriteAttributeString("count", result.VertexDegree[degree].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("coefficients");
                    foreach (double coefficient in result.Coefficient.Keys)
                    {
                        writer.WriteStartElement("coeff");
                        writer.WriteAttributeString("coefficient", coefficient.ToString());
                        writer.WriteAttributeString("count", result.Coefficient[coefficient].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("Connsubgraphs");
                    foreach (int sub in result.Subgraphs.Keys)
                    {
                        writer.WriteStartElement("csub");
                        writer.WriteAttributeString("vx", sub.ToString());
                        writer.WriteAttributeString("count", result.Subgraphs[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("Fullsubgraphs");
                    foreach (int sub in result.FullSubgraphs.Keys)
                    {
                        writer.WriteStartElement("fsub");
                        writer.WriteAttributeString("vx", sub.ToString());
                        writer.WriteAttributeString("count", result.FullSubgraphs[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                     writer.WriteStartElement("vertexdistance");
                    foreach (int sub in result.DistanceBetweenVertices.Keys)
                    {
                        writer.WriteStartElement("vd");
                        writer.WriteAttributeString("distance", sub.ToString());
                        writer.WriteAttributeString("count", result.DistanceBetweenVertices[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("eigenvalues");
                    foreach (double sub in result.EigenVector)
                    {
                        writer.WriteStartElement("ev");
                        writer.WriteAttributeString("eigenValue", sub.ToString());
                        //writer.WriteAttributeString("count", result.Subgraphs[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("eigenvaluesdistance");
                    foreach (double sub in result.DistancesBetweenEigenValues.Keys)
                    {
                        writer.WriteStartElement("ev");
                        writer.WriteAttributeString("distance", sub.ToString());
                        writer.WriteAttributeString("count", result.DistancesBetweenEigenValues[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("cycles");
                    foreach (int sub in result.Cycles.Keys)
                    {
                        writer.WriteStartElement("cl");
                        writer.WriteAttributeString("degree", sub.ToString());
                        writer.WriteAttributeString("count", result.Cycles[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("motives");
                    foreach (int sub in result.MotivesCount.Keys)
                    {
                        writer.WriteStartElement("mt");
                        writer.WriteAttributeString("degree", sub.ToString());
                        writer.WriteAttributeString("count", result.MotivesCount[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("parisiarray");
                    if (result.TreeVector != null)
                    {
                        foreach (bool bit in result.TreeVector)
                        {
                            writer.WriteStartElement("par");
                            writer.WriteAttributeString("bit", bit.ToString());
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        public override void Delete(Guid assemblyID)
        {
            string fileName = directory + assemblyID.ToString() + ".xml";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public override ResultAssembly Load(Guid assemblyID)
        {
            ResultAssembly resultAssembly = new ResultAssembly();
            resultAssembly.ID = assemblyID;

            List<AnalizeResult> results = new List<AnalizeResult>();
            resultAssembly.Results = results;
            AnalizeResult result = null;
            XmlDocument xml = new XmlDocument();

            xml.Load(directory + assemblyID.ToString() + ".xml");
            resultAssembly.Name = xml.SelectSingleNode("/assembly/name").InnerText;
            resultAssembly.ModelType = GetModelType(int.Parse(xml.SelectSingleNode("/assembly/graphmodel").Attributes["id"].Value));

            foreach (XmlNode paramNode in xml.SelectNodes("/assembly/generationparams/generationparam"))
            {
                GenerationParam param = (GenerationParam)Enum.ToObject(typeof(GenerationParam), int.Parse(paramNode.Attributes["id"].Value));

                GenerationParamInfo paramInfo = (GenerationParamInfo)(param.GetType().GetField(param.ToString()).GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                if (paramInfo.Type.Equals(typeof(Double)))
                {
                    resultAssembly.GenerationParams.Add(param, Convert.ToDouble(paramNode.Attributes["value"].Value, CultureInfo.InvariantCulture));
                }
                else if (paramInfo.Type.Equals(typeof(Int16)))
                {
                    resultAssembly.GenerationParams.Add(param, Convert.ToInt16(paramNode.Attributes["value"].Value));
                }
                else if (paramInfo.Type.Equals(typeof(Int32)))
                {
                    resultAssembly.GenerationParams.Add(param, Convert.ToInt32(paramNode.Attributes["value"].Value));
                }
                else if (paramInfo.Type.Equals(typeof(bool)))
                {
                    resultAssembly.GenerationParams.Add(param, Convert.ToBoolean(paramNode.Attributes["value"].Value));
                }
            }
            int degree, count, sub, distance;
            double coefficient;
            foreach (XmlNode paramNode in xml.SelectNodes("/assembly/analyseresults/instance"))
            {
                result = new AnalizeResult();
                results.Add(result);

                string base64Motif = paramNode.SelectNodes("motives")[0].InnerText;
                if (!String.IsNullOrEmpty(base64Motif))
                {
                    //result.MotifCount = (SubgruphCount)BinaryDeserialization(Convert.FromBase64String(base64Motif));
                }
                foreach (XmlNode item in paramNode.SelectNodes("result/item"))
                {
                    AnalyseOptions option = (AnalyseOptions)Enum.Parse(typeof(AnalyseOptions), item.Attributes["option"].Value, true);
                    result.Result.Add(option, double.Parse(item.InnerText));
                }
                foreach (XmlNode item in paramNode.SelectNodes("vertexdegree/vd"))
                {
                    degree = int.Parse(item.Attributes["degree"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.VertexDegree.Add(degree, count);
                }
                foreach (XmlNode item in paramNode.SelectNodes("coefficients/coeff"))
                {
                    coefficient = double.Parse(item.Attributes["coefficient"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.Coefficient.Add(coefficient, count);
                }
                /////////////////////////////////////////////////////////////////////////////
                foreach (XmlNode item in paramNode.SelectNodes("eigenvalues/ev"))
                {
                    coefficient = double.Parse(item.Attributes["eigenValue"].Value);
                    result.EigenVector.Add(coefficient);
                }
                foreach (XmlNode item in paramNode.SelectNodes("eigenvaluesdistance/ev"))
                {
                    coefficient = double.Parse(item.Attributes["distance"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.DistancesBetweenEigenValues.Add(coefficient, count);
                }
                foreach (XmlNode item in paramNode.SelectNodes("vertexdistance/vd"))
                {
                    distance = int.Parse(item.Attributes["distance"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.DistanceBetweenVertices.Add(distance, count);
                }
                foreach (XmlNode item in paramNode.SelectNodes("cycles/cl"))
                {
                    distance = int.Parse(item.Attributes["degree"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.Cycles.Add(distance, count);
                }
                /////////////////////////////////////////////////////////////////////////////
                foreach (XmlNode item in paramNode.SelectNodes("subgraphs/sub"))
                {
                    sub = int.Parse(item.Attributes["vx"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.Subgraphs.Add(sub, count);
                }
                XmlNodeList XmlBits = paramNode.SelectNodes("parisiarray/par");
                BitArray bits = new BitArray(XmlBits.Count);
                for (int i = 0; i < XmlBits.Count; i++)
                {
                    bits[i] = bool.Parse(XmlBits[i].Attributes["bit"].Value);
                }
            }
            return resultAssembly;
        }

        public ResultAssembly LoadXML(String assemblyID)
        {
            ResultAssembly resultAssembly = new ResultAssembly();
            //resultAssembly.ID = assemblyID;

            List<AnalizeResult> results = new List<AnalizeResult>();
            resultAssembly.Results = results;
            AnalizeResult result = null;
            XmlDocument xml = new XmlDocument();

            xml.Load(assemblyID);
            resultAssembly.Name = xml.SelectSingleNode("/assembly/name").InnerText;
            resultAssembly.ModelType = GetModelType(int.Parse(xml.SelectSingleNode("/assembly/graphmodel").Attributes["id"].Value));

            foreach (XmlNode paramNode in xml.SelectNodes("/assembly/generationparams/generationparam"))
            {
                GenerationParam param = (GenerationParam)Enum.ToObject(typeof(GenerationParam), int.Parse(paramNode.Attributes["id"].Value));

                GenerationParamInfo paramInfo = (GenerationParamInfo)(param.GetType().GetField(param.ToString()).GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                if (paramInfo.Type.Equals(typeof(Double)))
                {
                    resultAssembly.GenerationParams.Add(param, Convert.ToDouble(paramNode.Attributes["value"].Value, CultureInfo.InvariantCulture));
                }
                else if (paramInfo.Type.Equals(typeof(Int16)))
                {
                    resultAssembly.GenerationParams.Add(param, Convert.ToInt16(paramNode.Attributes["value"].Value));
                }
                else if (paramInfo.Type.Equals(typeof(Int32)))
                {
                    resultAssembly.GenerationParams.Add(param, Convert.ToInt32(paramNode.Attributes["value"].Value));
                }
                else if (paramInfo.Type.Equals(typeof(bool)))
                {
                    resultAssembly.GenerationParams.Add(param, Convert.ToBoolean(paramNode.Attributes["value"].Value));
                }
            }
            int degree, count, sub, distance;
            double coefficient;
            foreach (XmlNode paramNode in xml.SelectNodes("/assembly/analyseresults/instance"))
            {
                result = new AnalizeResult();
                results.Add(result);

                string base64Motif = paramNode.SelectNodes("motives")[0].InnerText;
                if (!String.IsNullOrEmpty(base64Motif))
                {
                    //result.MotifCount = (SubgruphCount)BinaryDeserialization(Convert.FromBase64String(base64Motif));
                }
                foreach (XmlNode item in paramNode.SelectNodes("result/item"))
                {
                    AnalyseOptions option = (AnalyseOptions)Enum.Parse(typeof(AnalyseOptions), item.Attributes["option"].Value, true);
                    result.Result.Add(option, double.Parse(item.InnerText));
                }
                foreach (XmlNode item in paramNode.SelectNodes("vertexdegree/vd"))
                {
                    degree = int.Parse(item.Attributes["degree"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.VertexDegree.Add(degree, count);
                }
                foreach (XmlNode item in paramNode.SelectNodes("coefficients/coeff"))
                {
                    coefficient = double.Parse(item.Attributes["coefficient"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.Coefficient.Add(coefficient, count);
                }
                /////////////////////////////////////////////////////////////////////////////
                foreach (XmlNode item in paramNode.SelectNodes("eigenvalues/ev"))
                {
                    coefficient = double.Parse(item.Attributes["eigenValue"].Value);
                    result.EigenVector.Add(coefficient);
                }
                foreach (XmlNode item in paramNode.SelectNodes("eigenvaluesdistance/ev"))
                {
                    coefficient = double.Parse(item.Attributes["distance"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.DistancesBetweenEigenValues.Add(coefficient, count);
                }
                foreach (XmlNode item in paramNode.SelectNodes("cycles/cl"))
                {
                    distance = int.Parse(item.Attributes["degree"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.Cycles.Add(distance, count);
                }
                foreach (XmlNode item in paramNode.SelectNodes("vertexdistance/vd"))
                {
                    distance = int.Parse(item.Attributes["distance"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.DistanceBetweenVertices.Add(distance, count);
                }
                /////////////////////////////////////////////////////////////////////////////
                foreach (XmlNode item in paramNode.SelectNodes("subgraphs/sub"))
                {
                    sub = int.Parse(item.Attributes["vx"].Value);
                    count = int.Parse(item.Attributes["count"].Value);
                    result.Subgraphs.Add(sub, count);
                }
                XmlNodeList XmlBits = paramNode.SelectNodes("parisiarray/par");
                BitArray bits = new BitArray(XmlBits.Count);
                for (int i = 0; i < XmlBits.Count; i++)
                {
                    bits[i] = bool.Parse(XmlBits[i].Attributes["bit"].Value);
                }
            }
            return resultAssembly;
        }
        public override List<ResultAssembly> LoadAllAssemblies()
        {
            List<ResultAssembly> assemblies = new List<ResultAssembly>();
            ResultAssembly assembly = null;

            foreach (string file in Directory.GetFiles(directory, "*.xml", SearchOption.TopDirectoryOnly))
            {
                assembly = new ResultAssembly();
                assemblies.Add(assembly);
                using (XmlTextReader reader = new XmlTextReader(file))
                {

                    reader.WhitespaceHandling = WhitespaceHandling.None;
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "id")
                            {
                                assembly.ID = new Guid(reader.ReadElementString());
                            }
                            if (reader.Name == "name")
                            {
                                assembly.Name = reader.ReadElementString();
                            }
                            if (reader.Name == "graphmodel")
                            {
                                reader.MoveToAttribute("modelname");
                                assembly.ModelName = reader.ReadContentAsString();
                                break;
                            }
                        }
                    }
                }
            }
            return assemblies;
        }

        #endregion

        private byte[] BinarySerialization(Object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, obj);
            return stream.GetBuffer();
        }

        private Object BinaryDeserialization(byte[] buffer)
        {
            Stream stream = new MemoryStream(buffer);
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(stream);
        }

    }

}
