using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model.Result;
using RandomGraph.Common.Model.Result;
using CommonLibrary.Model.Attributes;

using Model.HierarchicModel;
using Model.ParisiHierarchicModel;
using Model.NonRegularHierarchicModel;
using Model.BAModel;
using Model.ERModel;
using Model.WSModel;

namespace ResultStorage.StorageConverter
{
    // Реализация чтения информации из внешнего файла (Analyze Results File) и создания сборки.
    // Используется только классом ResultsFileConverter.
    class DictionaryStructure
    {
        private ResultAssembly result;

        public ResultAssembly Result
        {
            get { return result; }
        }

        // Чтение подкаталога с данным именем из корневого каталога 
        // Создание соответствующей сборки (job-а).
        public void ReadDirectory(string fullName)
        {
            DirectoryInfo d = new DirectoryInfo(fullName);

            result = new ResultAssembly();
            // !Исправить!
            result.Name = result.ID.ToString(); 
            result.FileName = d.Name;

            ReadHeader(d.GetFiles()[0].FullName);

            FileInfo[] f = d.GetFiles();
            foreach (FileInfo fInfo in f)
            {
                AnalizeResult r = new AnalizeResult();
                ReadBody(r, fInfo.FullName);
                this.result.Results.Add(r);
            }
        }

        // Утилиты.

        // Чтение header-части из внешнего файла с данным именем.
        private void ReadHeader(string firstFileFullName)
        {
            using (StreamReader streamReader =
                new StreamReader(firstFileFullName, System.Text.Encoding.Default))
            {
                this.result.ModelName = (streamReader.ReadLine().Substring(10));
                this.result.ModelType = GetModelType();
                
                string contents;
                while (!(contents = streamReader.ReadLine()).Contains("AnalyzeOptionName="))
                {
                    GetGenerationParameter(contents);
                }

                this.result.Size = GetSize();

                this.result.AnalizeOptions = (AnalyseOptions)Enum.Parse(typeof(AnalyseOptions),
                    contents.Substring(18));

                while ((contents = streamReader.ReadLine()) != "-")
                {
                    GetAnalyzeParameter(contents);
                }

                // !Исправить! число реализаций
            }
        }

        // Чтение body-части из внешнего файла с данным именем.
        private void ReadBody(AnalizeResult res, string fileFullName)
        {
            // Получение пар значений из файлов данного каталога.
            using (StreamReader streamReader = 
                new StreamReader(fileFullName, System.Text.Encoding.Default))
            {
                while (streamReader.ReadLine() != "-") { }

                string contents;
                while ((contents = streamReader.ReadLine()) != null)
                {
                    string first = "", second = "";
                    int j = 0;
                    while (contents[j] != ' ')
                    {
                        first += contents[j];
                        ++j;
                    }

                    second = contents.Substring(j);

                    // !Исправить! получать информацию в соответствии с числом реализаций...
                    switch (this.result.AnalizeOptions)
                    {
                        case AnalyseOptions.DegreeDistribution:
                            {
                                res.VertexDegree.Add(int.Parse(first), int.Parse(second));
                                break;
                            }
                        case AnalyseOptions.ConnSubGraph:
                            {
                                res.Subgraphs.Add(int.Parse(first), int.Parse(second));
                                break;
                            }
                        case AnalyseOptions.FullSubGraph:
                            {
                                res.FullSubgraphs.Add(int.Parse(first), int.Parse(second));
                                break;
                            }
                        case AnalyseOptions.MinPathDist:
                            {
                                res.DistanceBetweenVertices.Add(int.Parse(first), int.Parse(second));
                                break;
                            }
                        case AnalyseOptions.TriangleCountByVertex:
                            {
                                res.TriangleCount.Add(int.Parse(first), int.Parse(second));
                                break;
                            }
                        case AnalyseOptions.ClusteringCoefficient:
                            {
                                res.Coefficient.Add(double.Parse(first), int.Parse(second));
                                break;
                            }
                        case AnalyseOptions.DistEigenPath:
                            {
                                res.DistancesBetweenEigenValues.Add(double.Parse(first), int.Parse(second));
                                break;
                            }
                        case AnalyseOptions.Cycles:
                            {
                                res.Cycles.Add(int.Parse(first), long.Parse(second));
                                break;
                            }
                        case AnalyseOptions.TriangleTrajectory:
                            {
                                res.TriangleTrajectory.Add(int.Parse(first), double.Parse(second));
                                break;
                            }
                        default:
                            throw new SystemException("Not correct Analyze Option.");
                    }
                }
            }
        }

        // Чтение параметров генерации из header-части внешнего файла.
        private void GetGenerationParameter(string p)
        {
            string genParamName = p.Substring(0, p.IndexOf('='));
            string genParamValue = p.Substring(p.IndexOf('=') + 1);

            GenerationParam param = (GenerationParam)Enum.Parse(typeof(GenerationParam), 
                genParamName);

            GenerationParamInfo paramInfo = (GenerationParamInfo)(param.GetType().GetField(param.ToString()).GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
            if (paramInfo.Type.Equals(typeof(Double)))
            {
                this.result.GenerationParams.Add(param, Double.Parse(genParamValue));
            }
            else if (paramInfo.Type.Equals(typeof(Int16)))
            {
                this.result.GenerationParams.Add(param, Int16.Parse(genParamValue));
            }
            else if (paramInfo.Type.Equals(typeof(Int32)))
            {
                this.result.GenerationParams.Add(param, Int32.Parse(genParamValue));
            }
            else if (paramInfo.Type.Equals(typeof(bool)))
            {
                this.result.GenerationParams.Add(param, Boolean.Parse(genParamValue));
            }
            else if (paramInfo.Type.Equals(typeof(String)))
            {
                this.result.GenerationParams.Add(param, genParamValue);
            }
        }

        // Чтение параметров анализа из header-части внешнего файла.
        private void GetAnalyzeParameter(string p)
        {
            string analyzeParamName = p.Substring(0, p.IndexOf('='));
            string analyzeParamValue = p.Substring(p.IndexOf('=') + 1);

            AnalyzeOptionParam param = (AnalyzeOptionParam)Enum.Parse(typeof(AnalyzeOptionParam),
                analyzeParamName);

            AnalyzeOptionParamInfo paramInfo = (AnalyzeOptionParamInfo)(param.GetType().GetField(param.ToString()).GetCustomAttributes(typeof(AnalyzeOptionParamInfo), false)[0]);
            if (paramInfo.Type.Equals(typeof(Double)))
            {
                this.result.AnalyzeOptionParams[param] = Double.Parse(analyzeParamValue);
            }
            else if (paramInfo.Type.Equals(typeof(Int16)))
            {
                this.result.AnalyzeOptionParams[param] = Int16.Parse(analyzeParamValue);
            }
            else if (paramInfo.Type.Equals(typeof(BigInteger)))
            {
                this.result.AnalyzeOptionParams[param] = BigInteger.Parse(analyzeParamValue);
            }
            else if (paramInfo.Type.Equals(typeof(bool)))
            {
                this.result.AnalyzeOptionParams[param] = Boolean.Parse(analyzeParamValue);
            }
        }

        // !Исправить!. Этот метод должен быть в другом классе!
        // Возвращает тип по имени модели графа.
        private Type GetModelType()
        {
            switch (this.result.ModelName)
            {
                case "HierarchicModel":
                    return typeof(HierarchicModel);
                case "BAModel":
                    return typeof(Model.BAModel.BAModel);
                case "HierarchicModelParizi":
                    return typeof(ParisiHierarchicModel);
                case "WSModel":
                    return typeof(WSModel);
                case "ERModel":
                    return typeof(ERModel);
                case "NonRegularHierarchicModel":
                    return typeof(NonRegularHierarchicModel);
                default:
                    throw new SystemException("Model Type is not recognized.");
            }
        }

        // !Исправить! Лучше получать размер сети из файла (из-за не регулярной сети).
        private int GetSize()
        {
            switch (this.result.ModelName)
            {
                case "HierarchicModel":
                    return (int)Math.Pow((Int16)this.result.GenerationParams[GenerationParam.BranchIndex],
                        (Int16)this.result.GenerationParams[GenerationParam.Level]);
                case "BAModel":
                    return (Int32)this.result.GenerationParams[GenerationParam.Vertices] +
                        (Int32)this.result.GenerationParams[GenerationParam.StepCount];
                case "HierarchicModelParizi":
                    return (int)Math.Pow((Int16)this.result.GenerationParams[GenerationParam.BranchIndex],
                        (Int16)this.result.GenerationParams[GenerationParam.Level]);
                case "WSModel":
                    return (Int32)this.result.GenerationParams[GenerationParam.Vertices];
                case "ERModel":
                    return (Int32)this.result.GenerationParams[GenerationParam.Vertices];
                case "NonRegularHierarchicModel":
                    return 0;
                default:
                    throw new SystemException("Model Type is not recognized.");
            }
        }
    }
}
