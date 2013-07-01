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

namespace ResultStorage.StorageConverter
{
    // Реализация чтения информации из внешнего файла (Analyze Results File) и создания сборки.
    // Используется только классом ResultsFileConverter.
    class DictionaryStructure
    {
        private ResultAssembly result;
        private int realizationsCount;

        public ResultAssembly Result
        {
            get { return result; }
        }

        // Чтение подкаталога с данным именем из корневого каталога 
        // Создание соответствующей сборки (job-а).
        public void ReadDirectory(string fullName)
        {
            DirectoryInfo d = new DirectoryInfo(fullName);

            result = new ResultAssembly(true);
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
                this.result.ModelType = 
                    ResultStorage.Storage.ResultStorage.GetModelType(this.result.ModelName);
                
                string contents;
                while (!(contents = streamReader.ReadLine()).Contains("RealizationsCount="))
                {
                    GetGenerationParameter(contents);
                }

                this.realizationsCount = Int32.Parse(contents.Substring(18));
                // !Исправить! размер должен быть для каждой реализации (из-за нерегулярных сетей).
                this.result.Size = Int32.Parse(streamReader.ReadLine().Substring(5));

                this.result.AnalizeOptions = (AnalyseOptions)Enum.Parse(typeof(AnalyseOptions),
                    streamReader.ReadLine().Substring(18));

                while ((contents = streamReader.ReadLine()) != "-")
                {
                    GetAnalyzeParameter(contents);
                }
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

                    // !Исправить! глобальный анализ для усредненного результата.
                    switch (this.result.AnalizeOptions)
                    {
                        case AnalyseOptions.DegreeDistribution:
                            {
                                if (this.realizationsCount == 1)
                                {
                                    res.VertexDegree.Add(int.Parse(first), int.Parse(second));
                                }
                                else
                                {
                                    this.result.VertexDegreeLocal.Add(double.Parse(first),
                                        double.Parse(second));
                                }
                                break;
                            }
                        case AnalyseOptions.ConnSubGraph:
                            {
                                if (this.realizationsCount == 1)
                                {
                                    res.Subgraphs.Add(int.Parse(first), int.Parse(second));
                                }
                                else
                                {
                                    this.result.SubgraphsLocal.Add(double.Parse(first),
                                        double.Parse(second));
                                }
                                break;
                            }
                        case AnalyseOptions.FullSubGraph:
                            {
                                if (this.realizationsCount == 1)
                                {
                                    res.FullSubgraphs.Add(int.Parse(first), int.Parse(second));
                                }
                                else
                                {
                                    // Не реализовано.
                                }
                                break;
                            }
                        case AnalyseOptions.MinPathDist:
                            {
                                if (this.realizationsCount == 1)
                                {
                                    res.DistanceBetweenVertices.Add(int.Parse(first), int.Parse(second));
                                }
                                else
                                {
                                    this.result.DistanceBetweenVerticesLocal.Add(double.Parse(first),
                                        double.Parse(second));
                                }
                                break;
                            }
                        case AnalyseOptions.TriangleCountByVertex:
                            {
                                if (this.realizationsCount == 1)
                                {
                                    res.TriangleCount.Add(int.Parse(first), int.Parse(second));
                                }
                                else
                                {
                                    // Не реализовано.
                                }
                                break;
                            }
                        case AnalyseOptions.ClusteringCoefficient:
                            {
                                if (this.realizationsCount == 1)
                                {
                                    res.Coefficient.Add(double.Parse(first), int.Parse(second));
                                }
                                else
                                {
                                    this.result.CoefficientsLocal.Add(double.Parse(first),
                                        double.Parse(second));
                                }
                                break;
                            }
                        case AnalyseOptions.DistEigenPath:
                            {
                                if (this.realizationsCount == 1)
                                {
                                    res.DistancesBetweenEigenValues.Add(double.Parse(first),
                                        int.Parse(second));
                                }
                                else
                                {
                                    this.result.DistancesBetweenEigenValuesLocal.Add(double.Parse(first),
                                        double.Parse(second));
                                }
                                break;
                            }
                        case AnalyseOptions.Cycles:
                            {
                                if (this.realizationsCount == 1)
                                {
                                    res.Cycles.Add(int.Parse(first), long.Parse(second));
                                }
                                else
                                {
                                    // Не реализовано.
                                }
                                break;
                            }
                        case AnalyseOptions.TriangleTrajectory:
                            {
                                if (this.realizationsCount == 1)
                                {
                                    res.TriangleTrajectory.Add(int.Parse(first), double.Parse(second));
                                }
                                else
                                {
                                    this.result.TriangleTrajectoryLocal.Add(double.Parse(first),
                                        double.Parse(second));
                                }
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
    }
}
