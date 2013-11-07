using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;
using System.Numerics;

using RandomGraph.Common.Storage;
using RandomGraph.Common.Model.Result;
using CommonLibrary.Model.Result;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model;
using CommonLibrary.Model.Attributes;
using log4net;

namespace ResultStorage.Storage
{
    // Реализация хранилища данных - xml файл.
    public class XMLResultStorage : ResultStorage
    {
        // Организация работы с лог файлом.
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(XMLResultStorage));

        // Путь к директории, где должны быть сохранены xml файлы.
        private string directory;

        // Конструктор, который получает путь к директории для сохранения xml файлов.
        public XMLResultStorage(string dir)
        {
            log.Info("Creating XMLResultStorage object with path of directory.");

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

        // Сохранение сборки в xml файле.
        public override void Save(ResultAssembly assembly)
        {
            using (XmlTextWriter writer = new XmlTextWriter(directory + assembly.ID.ToString() + ".xml", Encoding.ASCII))
            {
                // Сохранение общей информации для данной сборки.
                log.Info("Saving common info of assembly.");

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

                // Сохранение значений параметров генерации для данной сборки.
                log.Info("Saving generation parameters values of assembly.");

                writer.WriteStartElement("generationparams");
                if (assembly.GenerationParams != null)
                {
                    foreach (GenerationParam genParameter in assembly.GenerationParams.Keys)
                    {
                        writer.WriteStartElement("generationparam");
                        writer.WriteAttributeString("id", Convert.ToInt32(genParameter).ToString());
                        writer.WriteAttributeString("parametername", Enum.GetName(typeof(GenerationParam), genParameter));
                        writer.WriteAttributeString("value", assembly.GenerationParams[genParameter].ToString());
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();

                // Сохранение результатов анализа для данной сборки.
                log.Info("Saving analyze results.");

                writer.WriteStartElement("analyseresults");
                int instanceNumber = 1;
                foreach (AnalizeResult result in assembly.Results)
                {
                    log.Info("Saving analyze results for instance - " + instanceNumber.ToString() + ".");

                    writer.WriteStartElement("instance");
                    writer.WriteElementString("size", result.Size.ToString());

                    // Сохранение результатов анализа для глобальных свойств.
                    log.Info("Saving analyze results for global options.");

                    writer.WriteStartElement("result");
                    foreach (AnalyseOptions analyseOption in result.Result.Keys)
                    {
                        writer.WriteStartElement("item");
                        writer.WriteAttributeString("option", analyseOption.ToString());
                        writer.WriteString(result.Result[analyseOption].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving analyze results for local options.");

                    log.Info("Saving results for vertex degrees.");
                    writer.WriteStartElement("vertexdegree");
                    foreach (int degree in result.VertexDegree.Keys)
                    {
                        writer.WriteStartElement("vd");
                        writer.WriteAttributeString("degree", degree.ToString());
                        writer.WriteAttributeString("count", result.VertexDegree[degree].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving results for clustering coefficients.");
                    writer.WriteStartElement("coefficients");
                    foreach (double coefficient in result.Coefficient.Keys)
                    {
                        writer.WriteStartElement("coeff");
                        writer.WriteAttributeString("coefficient", coefficient.ToString());
                        writer.WriteAttributeString("count", result.Coefficient[coefficient].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving results for connected subgraphs.");
                    writer.WriteStartElement("connsubgraphs");
                    foreach (int sub in result.Subgraphs.Keys)
                    {
                        writer.WriteStartElement("csub");
                        writer.WriteAttributeString("vx", sub.ToString());
                        writer.WriteAttributeString("count", result.Subgraphs[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving results for full subgraphs.");
                    writer.WriteStartElement("fullsubgraphs");
                    foreach (int sub in result.FullSubgraphs.Keys)
                    {
                        writer.WriteStartElement("fsub");
                        writer.WriteAttributeString("vx", sub.ToString());
                        writer.WriteAttributeString("count", result.FullSubgraphs[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving results for distances between vertices.");
                    writer.WriteStartElement("vertexdistance");
                    foreach (int sub in result.DistanceBetweenVertices.Keys)
                    {
                        writer.WriteStartElement("vd");
                        writer.WriteAttributeString("distance", sub.ToString());
                        writer.WriteAttributeString("count", result.DistanceBetweenVertices[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving results for eigen values.");
                    writer.WriteStartElement("eigenvalues");
                    foreach (double sub in result.EigenVector)
                    {
                        writer.WriteStartElement("ev");
                        writer.WriteAttributeString("eigenValue", sub.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving results for distances between eigen values.");
                    writer.WriteStartElement("eigenvaluesdistance");
                    foreach (double sub in result.DistancesBetweenEigenValues.Keys)
                    {
                        writer.WriteStartElement("ev");
                        writer.WriteAttributeString("distance", sub.ToString());
                        writer.WriteAttributeString("count", result.DistancesBetweenEigenValues[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving results for cycles.");
                    writer.WriteStartElement("cycles");
                    writer.WriteAttributeString("cycleslow",
                        assembly.AnalyzeOptionParams[AnalyzeOptionParam.CyclesLow].ToString());
                    writer.WriteAttributeString("cycleshigh",
                            assembly.AnalyzeOptionParams[AnalyzeOptionParam.CyclesHigh].ToString());
                    foreach (int sub in result.Cycles.Keys)
                    {
                        writer.WriteStartElement("cs");
                        writer.WriteAttributeString("order", sub.ToString());
                        writer.WriteAttributeString("count", result.Cycles[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving results for triangles.");
                    writer.WriteStartElement("triangles");
                    foreach (int count in result.TriangleCount.Keys)
                    {
                        writer.WriteStartElement("tc");
                        writer.WriteAttributeString("trianglecount", count.ToString());
                        writer.WriteAttributeString("count", result.TriangleCount[count].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving results for triangle trajectory.");
                    writer.WriteStartElement("triangletrajectory");
                    writer.WriteAttributeString("mu", 
                        assembly.AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryMu].ToString());
                    writer.WriteAttributeString("stepcount", 
                            assembly.AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryStepCount].ToString());
                    foreach (int count in result.TriangleTrajectory.Keys)
                    {
                        writer.WriteStartElement("tt");
                        writer.WriteAttributeString("time", count.ToString());
                        writer.WriteAttributeString("trianglecount", result.TriangleTrajectory[count].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    log.Info("Saving results for motivs.");
                    writer.WriteStartElement("motives");
                    writer.WriteAttributeString("motivslow",
                        assembly.AnalyzeOptionParams[AnalyzeOptionParam.MotifsLow].ToString());
                    writer.WriteAttributeString("motivshigh",
                            assembly.AnalyzeOptionParams[AnalyzeOptionParam.MotifsHigh].ToString());
                    foreach (int sub in result.MotivesCount.Keys)
                    {
                        writer.WriteStartElement("mf");
                        writer.WriteAttributeString("id", sub.ToString());
                        writer.WriteAttributeString("count", result.MotivesCount[sub].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    ++instanceNumber;
                }
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        // Сохранение исследования в xml файле.
        public override void SaveResearch(ResultResearch research)
        {
            using (XmlTextWriter writer = new XmlTextWriter(
                this.directory + research.ResearchID.ToString() + ".xml", 
                Encoding.ASCII))
            {
                // Сохранение общей информации для данной сборки.
                log.Info("Saving common info of research.");

                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument(true);
                writer.WriteStartElement("research");

                writer.WriteElementString("id", research.ResearchID.ToString());
                writer.WriteElementString("name", research.Name);
                writer.WriteElementString("date", DateTime.Now.ToString());
                writer.WriteElementString("realizationcount", research.RealizationCount.ToString());
                writer.WriteElementString("delta", research.Delta.ToString());
                writer.WriteElementString("function", research.Function);
                writer.WriteElementString("size", research.Size.ToString());

                writer.WriteStartElement("graphmodel");
                writer.WriteAttributeString("id", GetModelID(research.ModelType).ToString());
                writer.WriteAttributeString("modelname", research.ModelType.Name);
                writer.WriteEndElement();

                // Сохранение значений параметров генерации для данной сборки.
                log.Info("Saving generation parameters values of research.");

                writer.WriteStartElement("generationparams");
                if (research.GenerationParams != null)
                {
                    foreach (GenerationParam genParameter in research.GenerationParams.Keys)
                    {
                        writer.WriteStartElement("generationparam");
                        writer.WriteAttributeString("id", Convert.ToInt32(genParameter).ToString());
                        writer.WriteAttributeString("parametername", Enum.GetName(typeof(GenerationParam), genParameter));
                        writer.WriteAttributeString("value", research.GenerationParams[genParameter].ToString());
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();   // generationparams

                // Сохранение результатов анализа для данной сборки.
                log.Info("Saving research results.");

                writer.WriteStartElement("results");
                foreach (double l in research.Result.Keys)
                {
                    log.Info("Saving analyze results for level - " + l.ToString() + ".");

                    writer.WriteStartElement("level" + l.ToString());
                    SortedDictionary<double, double> r = research.Result[l];
                    foreach (double degree in r.Keys)
                    {
                        writer.WriteStartElement("avgorder");
                        writer.WriteAttributeString("q", degree.ToString());
                        writer.WriteAttributeString("order", r[degree].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();   // level
                }

                writer.WriteEndElement();   // results
                writer.WriteEndElement();   // research
            }
        }

        // Удаление сборки по данному идентификатору сборки.
        public override void Delete(Guid assemblyID)
        {
            string fileName = directory + assemblyID.ToString() + ".xml";
            if (File.Exists(fileName))
            {
                log.Info("Deleting assembly with ID " + assemblyID.ToString() + ".");
                File.Delete(fileName);
            }
        }

        // Удаление исследования по данному идентификатору исследования.
        public override void DeleteResearch(ResultResearch research)
        {
            throw new NotImplementedException();
        }

        // Загрузка сборки по данному идентификатору сборки.
        public override ResultAssembly Load(Guid assemblyID)
        {
            log.Info("Loading assembly with ID " + assemblyID.ToString() + ".");

            log.Info("Loading common info of assembly.");

            ResultAssembly resultAssembly = new ResultAssembly();
            resultAssembly.ID = assemblyID;

            List<AnalizeResult> results = new List<AnalizeResult>();
            resultAssembly.Results = results;
            AnalizeResult result = null;
            XmlDocument xml = new XmlDocument();

            xml.Load(directory + assemblyID.ToString() + ".xml");
            resultAssembly.Name = xml.SelectSingleNode("/assembly/name").InnerText;
            resultAssembly.ModelType = GetModelType(int.Parse(xml.SelectSingleNode("/assembly/graphmodel").Attributes["id"].Value));

            log.Info("Loading generation parameters values of assembly.");
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
                else if (paramInfo.Type.Equals(typeof(String)))
                {
                    resultAssembly.GenerationParams.Add(param, Convert.ToString(paramNode.Attributes["value"].Value));
                }
            }

            log.Info("Loading analyze results.");

            int instanceNumber = 0, tempInt1, tempInt2;
            long tempLong;
            double tempDouble;
            foreach (XmlNode paramNode in xml.SelectNodes("/assembly/analyseresults/instance"))
            {
                log.Info("Loading analyze results for instance - " + instanceNumber.ToString() + ".");

                result = new AnalizeResult();
                results.Add(result);

                result.Size = int.Parse(paramNode.SelectSingleNode("size").InnerText);
                
                log.Info("Loading analyze results for global options.");
                foreach (XmlNode item in paramNode.SelectNodes("result/item"))
                {
                    AnalyseOptions option = (AnalyseOptions)Enum.Parse(typeof(AnalyseOptions), 
                        item.Attributes["option"].Value, true);
                    result.Result.Add(option, double.Parse(item.InnerText));
                }

                log.Info("Loading analyze results for local options.");

                log.Info("Loading vertex degrees.");
                foreach (XmlNode item in paramNode.SelectNodes("vertexdegree/vd"))
                {
                    tempInt1 = int.Parse(item.Attributes["degree"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.VertexDegree.Add(tempInt1, tempInt2);
                }

                log.Info("Loading clustering coefficients.");
                foreach (XmlNode item in paramNode.SelectNodes("coefficients/coeff"))
                {
                    tempDouble = double.Parse(item.Attributes["coefficient"].Value);
                    tempInt1 = int.Parse(item.Attributes["count"].Value);
                    result.Coefficient.Add(tempDouble, tempInt1);
                }

                log.Info("Loading connected subgraphs.");
                foreach (XmlNode item in paramNode.SelectNodes("connsubgraphs/csub"))
                {
                    tempInt1 = int.Parse(item.Attributes["vx"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.Subgraphs.Add(tempInt1, tempInt2);
                }

                log.Info("Loading full subgraphs.");
                foreach (XmlNode item in paramNode.SelectNodes("fullsubgraphs/fsub"))
                {
                    tempInt1 = int.Parse(item.Attributes["vx"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.FullSubgraphs.Add(tempInt1, tempInt2);
                }

                log.Info("Loading distances between vertices.");
                foreach (XmlNode item in paramNode.SelectNodes("vertexdistance/vd"))
                {
                    tempInt1 = int.Parse(item.Attributes["distance"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.DistanceBetweenVertices.Add(tempInt1, tempInt2);
                }

                log.Info("Loading eigen values.");
                foreach (XmlNode item in paramNode.SelectNodes("eigenvalues/ev"))
                {
                    tempDouble = double.Parse(item.Attributes["eigenValue"].Value);
                    result.EigenVector.Add(tempDouble);
                }

                log.Info("Loading distances between eigen values.");
                foreach (XmlNode item in paramNode.SelectNodes("eigenvaluesdistance/ev"))
                {
                    tempDouble = double.Parse(item.Attributes["distance"].Value);
                    tempInt1 = int.Parse(item.Attributes["count"].Value);
                    result.DistancesBetweenEigenValues.Add(tempDouble, tempInt1);
                }

                log.Info("Loading cycles.");
                XmlNode it = paramNode.SelectSingleNode("cycles");
                resultAssembly.AnalyzeOptionParams[AnalyzeOptionParam.CyclesLow] =
                    Int16.Parse(it.Attributes["cycleslow"].Value);
                resultAssembly.AnalyzeOptionParams[AnalyzeOptionParam.CyclesHigh] =
                    Int16.Parse(it.Attributes["cycleshigh"].Value);
                foreach (XmlNode item in paramNode.SelectNodes("cycles/cs"))
                {
                    tempInt1 = int.Parse(item.Attributes["order"].Value);
                    tempLong = long.Parse(item.Attributes["count"].Value);
                    result.Cycles.Add(tempInt1, tempLong);
                }

                log.Info("Loading triangles.");
                foreach (XmlNode item in paramNode.SelectNodes("triangles/tc"))
                {
                    tempInt1 = int.Parse(item.Attributes["trianglecount"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.TriangleCount.Add(tempInt1, tempInt2);
                }

                log.Info("Loading triangle trajectory.");
                it = paramNode.SelectSingleNode("triangletrajectory");
                resultAssembly.AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryMu] = 
                    Double.Parse(it.Attributes["mu"].Value);
                resultAssembly.AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryStepCount] =
                    BigInteger.Parse(it.Attributes["stepcount"].Value);
                foreach (XmlNode item in paramNode.SelectNodes("triangletrajectory/tt"))
                {
                    tempInt1 = int.Parse(item.Attributes["time"].Value);
                    tempDouble = double.Parse(item.Attributes["trianglecount"].Value);
                    result.TriangleTrajectory.Add(tempInt1, tempDouble);
                }

                log.Info("Loading motifs.");
                it = paramNode.SelectSingleNode("motives");
                resultAssembly.AnalyzeOptionParams[AnalyzeOptionParam.MotifsLow] =
                    Int16.Parse(it.Attributes["motivslow"].Value);
                resultAssembly.AnalyzeOptionParams[AnalyzeOptionParam.MotifsHigh] =
                    Int16.Parse(it.Attributes["motivshigh"].Value);
                foreach (XmlNode item in paramNode.SelectNodes("motives/mf"))
                {
                    tempInt1 = int.Parse(item.Attributes["id"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.MotivesCount.Add(tempInt1, tempInt2);
                }

                ++instanceNumber;
            }
            return resultAssembly;
        }

        // Загрузка исследования по данному идентификатору исследования.
        public override ResultResearch LoadResearch(Guid researchID)
        {
            log.Info("Loading research with ID " + researchID.ToString() + ".");

            log.Info("Loading common info of research.");

            ResultResearch resultResearch = new ResultResearch();
            resultResearch.ResearchID = researchID;

            XmlDocument xml = new XmlDocument();

            xml.Load(this.directory + researchID.ToString() + ".xml");
            resultResearch.Name = xml.SelectSingleNode("/research/name").InnerText;
            resultResearch.Delta = Double.Parse(xml.SelectSingleNode("/research/delta").InnerText);
            resultResearch.RealizationCount = Int32.Parse(xml.SelectSingleNode("/research/realizationcount").InnerText);
            resultResearch.Function = xml.SelectSingleNode("/research/function").InnerText;
            resultResearch.ModelType = GetModelType(int.Parse(xml.SelectSingleNode("/research/graphmodel").Attributes["id"].Value));
            resultResearch.Size = Int32.Parse(xml.SelectSingleNode("/research/size").InnerText);

            log.Info("Loading generation parameters values of research.");
            foreach (XmlNode paramNode in xml.SelectNodes("/research/generationparams/generationparam"))
            {
                GenerationParam param = (GenerationParam)Enum.ToObject(typeof(GenerationParam), int.Parse(paramNode.Attributes["id"].Value));

                GenerationParamInfo paramInfo = (GenerationParamInfo)(param.GetType().GetField(param.ToString()).GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                if (paramInfo.Type.Equals(typeof(Double)))
                {
                    resultResearch.GenerationParams.Add(param, Convert.ToDouble(paramNode.Attributes["value"].Value, CultureInfo.InvariantCulture));
                }
                else if (paramInfo.Type.Equals(typeof(Int16)))
                {
                    resultResearch.GenerationParams.Add(param, Convert.ToInt16(paramNode.Attributes["value"].Value));
                }
                else if (paramInfo.Type.Equals(typeof(Int32)))
                {
                    resultResearch.GenerationParams.Add(param, Convert.ToInt32(paramNode.Attributes["value"].Value));
                }
                else if (paramInfo.Type.Equals(typeof(bool)))
                {
                    resultResearch.GenerationParams.Add(param, Convert.ToBoolean(paramNode.Attributes["value"].Value));
                }
                else if (paramInfo.Type.Equals(typeof(String)))
                {
                    resultResearch.GenerationParams.Add(param, Convert.ToString(paramNode.Attributes["value"].Value));
                }
            }

            log.Info("Loading research results.");

            int count = xml.SelectSingleNode("/research/results").ChildNodes.Count;
            for (int i = 1; i <= count; ++i)
            {
                SortedDictionary<double, double> r = new SortedDictionary<double, double>();
                foreach (XmlNode paramNode in xml.SelectNodes("/research/results/level" + i.ToString()))
                {
                    foreach (XmlNode item in paramNode.SelectNodes("avgorder"))
                    {
                        r.Add(Double.Parse(item.Attributes["q"].Value),
                        Double.Parse(item.Attributes["order"].Value));
                    }
                }

                resultResearch.Result.Add(i, r);
            }

            return resultResearch;
        }

        // Загрузка сборки по строковому идентификатору сборки.
        public ResultAssembly LoadXML(String assemblyID)
        {
            log.Info("Loading assembly with ID " + assemblyID.ToString() + ".");

            log.Info("Loading common info of assembly.");

            ResultAssembly resultAssembly = new ResultAssembly();

            List<AnalizeResult> results = new List<AnalizeResult>();
            resultAssembly.Results = results;
            AnalizeResult result = null;
            XmlDocument xml = new XmlDocument();

            xml.Load(assemblyID);
            resultAssembly.Name = xml.SelectSingleNode("/assembly/name").InnerText;
            resultAssembly.ModelType = GetModelType(int.Parse(xml.SelectSingleNode("/assembly/graphmodel").Attributes["id"].Value));

            log.Info("Loading generation parameters values of assembly.");
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
                else if (paramInfo.Type.Equals(typeof(String)))
                {
                    resultAssembly.GenerationParams.Add(param, Convert.ToString(paramNode.Attributes["value"].Value));
                }
            }

            log.Info("Loading analyze results.");

            int instanceNumber = 0, tempInt1, tempInt2;
            long tempLong;
            double tempDouble;
            foreach (XmlNode paramNode in xml.SelectNodes("/assembly/analyseresults/instance"))
            {
                log.Info("Loading analyze results for instance - " + instanceNumber.ToString() + ".");

                result = new AnalizeResult();
                results.Add(result);

                result.Size = int.Parse(paramNode.SelectSingleNode("size").InnerText);

                log.Info("Loading analyze results for global options.");
                foreach (XmlNode item in paramNode.SelectNodes("result/item"))
                {
                    AnalyseOptions option = (AnalyseOptions)Enum.Parse(typeof(AnalyseOptions),
                        item.Attributes["option"].Value, true);
                    result.Result.Add(option, double.Parse(item.InnerText));
                }

                log.Info("Loading analyze results for local options.");

                log.Info("Loading vertex degrees.");
                foreach (XmlNode item in paramNode.SelectNodes("vertexdegree/vd"))
                {
                    tempInt1 = int.Parse(item.Attributes["degree"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.VertexDegree.Add(tempInt1, tempInt2);
                }

                log.Info("Loading clustering coefficients.");
                foreach (XmlNode item in paramNode.SelectNodes("coefficients/coeff"))
                {
                    tempDouble = double.Parse(item.Attributes["coefficient"].Value);
                    tempInt1 = int.Parse(item.Attributes["count"].Value);
                    result.Coefficient.Add(tempDouble, tempInt1);
                }

                log.Info("Loading connected subgraphs.");
                foreach (XmlNode item in paramNode.SelectNodes("connsubgraphs/csub"))
                {
                    tempInt1 = int.Parse(item.Attributes["vx"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.Subgraphs.Add(tempInt1, tempInt2);
                }

                log.Info("Loading full subgraphs.");
                foreach (XmlNode item in paramNode.SelectNodes("fullsubgraphs/fsub"))
                {
                    tempInt1 = int.Parse(item.Attributes["vx"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.FullSubgraphs.Add(tempInt1, tempInt2);
                }

                log.Info("Loading distances between vertices.");
                foreach (XmlNode item in paramNode.SelectNodes("vertexdistance/vd"))
                {
                    tempInt1 = int.Parse(item.Attributes["distance"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.DistanceBetweenVertices.Add(tempInt1, tempInt2);
                }

                log.Info("Loading eigen values.");
                foreach (XmlNode item in paramNode.SelectNodes("eigenvalues/ev"))
                {
                    tempDouble = double.Parse(item.Attributes["eigenValue"].Value);
                    result.EigenVector.Add(tempDouble);
                }

                log.Info("Loading distances between eigen values.");
                foreach (XmlNode item in paramNode.SelectNodes("eigenvaluesdistance/ev"))
                {
                    tempDouble = double.Parse(item.Attributes["distance"].Value);
                    tempInt1 = int.Parse(item.Attributes["count"].Value);
                    result.DistancesBetweenEigenValues.Add(tempDouble, tempInt1);
                }

                log.Info("Loading cycles.");
                XmlNode it = paramNode.SelectSingleNode("cycles");
                resultAssembly.AnalyzeOptionParams[AnalyzeOptionParam.CyclesLow] =
                    Int16.Parse(it.Attributes["cycleslow"].Value);
                resultAssembly.AnalyzeOptionParams[AnalyzeOptionParam.CyclesHigh] =
                    Int16.Parse(it.Attributes["cycleshigh"].Value);
                foreach (XmlNode item in paramNode.SelectNodes("cycles/cs"))
                {
                    tempInt1 = int.Parse(item.Attributes["order"].Value);
                    tempLong = long.Parse(item.Attributes["count"].Value);
                    result.Cycles.Add(tempInt1, tempLong);
                }

                log.Info("Loading triangles.");
                foreach (XmlNode item in paramNode.SelectNodes("triangles/tc"))
                {
                    tempInt1 = int.Parse(item.Attributes["trianglecount"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.TriangleCount.Add(tempInt1, tempInt2);
                }

                log.Info("Loading triangle trajectory.");
                it = paramNode.SelectSingleNode("triangletrajectory");
                resultAssembly.AnalyzeOptionParams.Add(AnalyzeOptionParam.TrajectoryMu, 
                    Double.Parse(it.Attributes["mu"].Value));
                resultAssembly.AnalyzeOptionParams.Add(AnalyzeOptionParam.TrajectoryStepCount,
                    BigInteger.Parse(it.Attributes["stepcount"].Value));
                foreach (XmlNode item in paramNode.SelectNodes("triangletrajectory/tt"))
                {
                    tempInt1 = int.Parse(item.Attributes["time"].Value);
                    tempDouble = double.Parse(item.Attributes["trianglecount"].Value);
                    result.TriangleTrajectory.Add(tempInt1, tempDouble);
                }

                log.Info("Loading motifs.");
                it = paramNode.SelectSingleNode("motives");
                resultAssembly.AnalyzeOptionParams[AnalyzeOptionParam.MotifsLow] =
                    Int16.Parse(it.Attributes["motivslow"].Value);
                resultAssembly.AnalyzeOptionParams[AnalyzeOptionParam.MotifsHigh] =
                    Int16.Parse(it.Attributes["motivshigh"].Value);
                foreach (XmlNode item in paramNode.SelectNodes("motives/mf"))
                {
                    tempInt1 = int.Parse(item.Attributes["id"].Value);
                    tempInt2 = int.Parse(item.Attributes["count"].Value);
                    result.MotivesCount.Add(tempInt1, tempInt2);
                }

                ++instanceNumber;
            }
            return resultAssembly;
        }

        // Загрузка всех сборок.
        public override List<ResultAssembly> LoadAllAssemblies()
        {
            List<ResultAssembly> assemblies = new List<ResultAssembly>();
            ResultAssembly assembly = null;

            foreach (string file in Directory.GetFiles(directory, "*.xml", 
                SearchOption.TopDirectoryOnly))
            {
                assembly = new ResultAssembly();
                assemblies.Add(assembly);
                using (XmlTextReader reader = new XmlTextReader(file))
                {
                    try
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
                                // !исправить!
                            }
                        }
                    }
                    catch (SystemException)
                    {
                        continue;
                    }
                }
            }

            return assemblies;
        }

        public override List<ResultResearch> LoadAllResearches()
        {
            List<ResultResearch> research = new List<ResultResearch>();
            ResultResearch r = null;

            foreach (string file in Directory.GetFiles(directory, "*.xml",
                SearchOption.TopDirectoryOnly))
            {
                r = new ResultResearch();
                research.Add(r);
                using (XmlTextReader reader = new XmlTextReader(file))
                {
                    try
                    {
                        reader.WhitespaceHandling = WhitespaceHandling.None;
                        while (reader.Read())
                        {

                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.Name == "id")
                                {
                                    r.ResearchID = new Guid(reader.ReadElementString());
                                }
                                if (reader.Name == "name")
                                {
                                    r.Name = reader.ReadElementString();
                                }
                                // !исправить!
                            }
                        }
                    }
                    catch (SystemException)
                    {
                        continue;
                    }
                }
            }

            return research;
        }

        public override List<ResultAssembly> LoadAssembliesByModel(string modelName)
        {
            List<ResultAssembly> allResults = LoadAllAssemblies();
            List<ResultAssembly> results = new List<ResultAssembly>();
            foreach (ResultAssembly result in allResults)
            {
                if (result.ModelName == modelName)
                {
                    results.Add(result);
                }
            }
            return results;
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
