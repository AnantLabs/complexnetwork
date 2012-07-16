using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.WSModel.Realization;
using Algorithms;

namespace Model.WSModel
{
    // Атрибуты модели (WS).
    [GraphModel("Watts-Strogatz", GenerationRule.Sequential, "Watts-Strogatz graph model")]
    [AvailableAnalyzeOptions(
        AnalyseOptions.AveragePath |
        AnalyseOptions.Diameter |
        AnalyseOptions.Cycles3 |
        AnalyseOptions.Cycles4 |
        AnalyseOptions.EigenValue | 
        AnalyseOptions.DegreeDistribution |        
        AnalyseOptions.ClusteringCoefficient |        
        AnalyseOptions.ConnSubGraph)]
    [RequiredGenerationParam(GenerationParam.P, 10)]
    [RequiredGenerationParam(GenerationParam.Vertices, 2)]
    [RequiredGenerationParam(GenerationParam.Edges, 3)]

    // Реализация модели (WS).
    public class WSModel : AbstractGraphModel
    {
        // !Организация Работы с лог файлом!

        private static readonly string MODEL_NAME = "Watts-Strogatz";

        public WSModel() { }

        public WSModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
            : base(genParam, options, sequenceNumber)
        {
            ValidateModelParams();
            InitModel();
        }

        public WSModel(ArrayList matrix, AnalyseOptions options, int sequenceNumber)
            : base(matrix, options, sequenceNumber)
        {
            ValidateModelParams();
            InitModel();
        }

        private void ValidateModelParams()
        {
            // !Добавить проверку параметров!
        }

        private void InitModel()
        {
            InvokeProgressEvent(GraphProgress.Initializing, 0);
            ModelName = MODEL_NAME;

            // Проверить правильность
            GenerationRule = GenerationRule.Sequential;

            // Определение параметров генерации. !Добавить число шагов!
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Edges);
            genParams.Add(GenerationParam.Vertices);
            genParams.Add(GenerationParam.P);
            RequiredGenerationParams = genParams;

            // Определение доступных опций для анализа (вычисляемые характеристики для данной модели (WS)).
            AvailableOptions = AnalyseOptions.AveragePath |
                AnalyseOptions.Diameter |
                AnalyseOptions.Cycles3 |
                AnalyseOptions.Cycles4 |
                AnalyseOptions.EigenValue |
                AnalyseOptions.DegreeDistribution |
                AnalyseOptions.ClusteringCoefficient |
                AnalyseOptions.ConnSubGraph;

            // Определение генератора и анализатора для данной модели (WS).
            generator = new WSGenerator();
            analyzer = new WSAnalyzer((WSContainer)generator.Container);

            InvokeProgressEvent(GraphProgress.Ready);
        }


        protected override void GenerateModel()
        {
            InvokeProgressEvent(GraphProgress.StartingGeneration, 5);
            try
            {
                if (true)    // Динамическая генерация
                    generator.RandomGeneration(GenerationParamValues);
                else    // Статическая генерация
                    generator.StaticGeneration(NeighbourshipMatrix);

                InvokeProgressEvent(GraphProgress.Generating, 30);

                /*if (graph == null)
                {

                    graph = new WSGraph((int)GenerationParamValues[GenerationParam.Vertices],
                                            (int)GenerationParamValues[GenerationParam.Edges],
                                            (double)GenerationParamValues[GenerationParam.P]);
                }*/
            }
            catch (Exception ex)
            {
                InvokeFailureProgressEvent(GraphProgress.GenerationFailed, ex.Message);
                //RETHROW EXCEPTION 
                throw ex;
            }
            finally
            {
                //Place clean up code here
            }
        }

        protected override void AnalizeModel()
        {
            InvokeProgressEvent(GraphProgress.StartingAnalizing);
            
            try
            {
                if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 10, "Average Path");
                    Result.Result[AnalyseOptions.AveragePath] = analyzer.GetAveragePath();
                }

                if ((AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 20, "Diameter");
                    Result.Result[AnalyseOptions.Diameter] = analyzer.GetDiameter();
                }

                if ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 30, "Cycles of order 3");
                    Result.Result[AnalyseOptions.Cycles3] = analyzer.GetCycles3();
                }

                if ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 40, "Cycles of order 4");
                    Result.Result[AnalyseOptions.Cycles4] = analyzer.GetCycles4();
                }

                if ((AnalizeOptions & AnalyseOptions.EigenValue) == AnalyseOptions.EigenValue)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 50, "Calculating EigenValue");

                    // !Плохо написано!
                    Algorithms.EigenValue ev = new EigenValue();
                    ArrayList al = new ArrayList();
                    bool[,] m = GetMatrix();
                    Result.EigenVector = ev.EV(m);
                    Result.DistancesBetweenEigenValues = ev.CalcEigenValuesDist();
                }

                if ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 60, "Degree Distribution");
                    Result.VertexDegree = analyzer.GetDegreeDistribution();
                    Result.Result[AnalyseOptions.DegreeDistribution] = 1;
                }

                if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 70, "Classtering Coefficient");
                    Result.Coefficient = analyzer.GetClusteringCoefficient();
                }

                if ((AnalizeOptions & AnalyseOptions.MinPathDist) == AnalyseOptions.MinPathDist)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 80, "Minimal Path Distance Distribution");
                    Result.DistanceBetweenVertices = analyzer.GetMinPathDist();
                }

                Result.graphSize = analyzer.Container.Size;

                InvokeProgressEvent(GraphProgress.AnalizingDone, 95);
                InvokeProgressEvent(GraphProgress.Done, 100);
            }

            catch (Exception ex)
            {
                InvokeFailureProgressEvent(GraphProgress.AnalizingFailed, ex.Message);
                //RETHROW EXCEPTION
                throw ex;
            }
            finally
            {
                //Place clean up code here
            }
        }

        public override bool CheckGenerationParams(int instances)
        {
            int e = (int)GenerationParamValues[GenerationParam.Edges];
            int v = (int)GenerationParamValues[GenerationParam.Vertices];
            if (e % 2 == 0 && e > Math.Log((double)v) && e < v)
                return true;
            return false;
        }

        public override string GetParamsInfo()
        {
            int e = (int)GenerationParamValues[GenerationParam.Edges];
            int v = (int)GenerationParamValues[GenerationParam.Vertices];
            double a = Math.Log((double)v) + 1;
            return "Edges count mast be greater\n " + (int)a + " and less" + (v - 1);

        }

        public override void Dispose()
        {
            //log.Info("disposing...");
            generator = null;
            analyzer = null;
            base.Dispose();
        }

        public override bool[,] GetMatrix()
        {
            return analyzer.Container.GetMatrix();
        }
    }
}
