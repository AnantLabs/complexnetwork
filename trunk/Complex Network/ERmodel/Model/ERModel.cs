using System;
using System.Collections.Generic;
using System.Collections;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.ERModel.Realization;
using System.Threading;
using Algorithms;
using log4net;

namespace Model.ERModel
{
    // Атрибуты модели (ER).
    [GraphModel("ERModel", GenerationRule.Sequential, "ERModel graph model")]
    [AvailableAnalyzeOptions(
         AnalyseOptions.AveragePath |
         AnalyseOptions.Diameter |
         AnalyseOptions.Cycles3 |
         AnalyseOptions.Cycles4 |
         AnalyseOptions.EigenValue |
         AnalyseOptions.DistEigenPath |
         AnalyseOptions.DegreeDistribution |
         AnalyseOptions.ClusteringCoefficient |
         AnalyseOptions.MinPathDist)]
    [RequiredGenerationParam(GenerationParam.Vertices, 2)]
    [RequiredGenerationParam(GenerationParam.P, 3)]

    // Реализация модели (ER).
    public class ERModel : AbstractGraphModel
    {
        // Организация Работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERModel));

        private static readonly string MODEL_NAME = "ERModel";

        public ERModel() { }

        public ERModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
            : base(genParam, options, sequenceNumber)
        {
            log.Info("Creating ERModel object");
            ValidateModelParams();
            InitModel();
        }

        public ERModel(ArrayList matrix, AnalyseOptions options, int sequenceNumber)
            :base(matrix, options, sequenceNumber)
        {
            log.Info("Creating ERModel object");
            ValidateModelParams();
            InitModel();
        }

        private void ValidateModelParams()
        {
            // !Добавить проверку параметров!
        }

        private void InitModel()
        {
            log.Info("Started model initialization");
            InvokeProgressEvent(GraphProgress.Initializing, 0);
            ModelName = MODEL_NAME;

            // Проверить правильность
            GenerationRule = GenerationRule.Sequential;

            // Определение параметров генерации. !Добавить число шагов!
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Vertices);
            genParams.Add(GenerationParam.P);
            RequiredGenerationParams = genParams;

            // Определение доступных опций для анализа (вычисляемые характеристики для данной модели (ER)).
            AvailableOptions = AnalyseOptions.AveragePath |
                AnalyseOptions.Diameter |
                AnalyseOptions.Cycles3 |
                AnalyseOptions.Cycles4 |
                AnalyseOptions.EigenValue |
                AnalyseOptions.DistEigenPath |
                AnalyseOptions.DegreeDistribution |
                AnalyseOptions.ClusteringCoefficient |
                AnalyseOptions.MinPathDist;   
         
            // Определение генератора и анализатора для данной модели (ER).
            generator = new ERGenerator();
            analyzer = new ERAnalyzer((ERContainer)generator.Container);
          
            InvokeProgressEvent(GraphProgress.Ready);
            log.Info("Finished model initialization");
        }

        protected override void GenerateModel()
        {
            log.Info("Started model generation");

            InvokeProgressEvent(GraphProgress.StartingGeneration, 5);

            try
            {
                if (true)    // Динамическая генерация
                    generator.RandomGeneration(GenerationParamValues);
                else    // Статическая генерация
                    generator.StaticGeneration(NeighbourshipMatrix);

                InvokeProgressEvent(GraphProgress.GenerationDone, 30);
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                log.Info("An Exception is occured during model generation");
                log.Fatal(ex);
                InvokeFailureProgressEvent(GraphProgress.GenerationFailed, "ENTER FAIL REASON HERE");
                //RETHROW EXCEPTION 
            }
            finally
            {
                //Place clean up code here
            }

            log.Info("Finished model generation");
        }

        protected override void AnalizeModel()
        {
            log.Info("Started analizing model");
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

            }
            catch (Exception ex)
            {
                log.Info("An Exception is occured during model analizing");
                log.Fatal(ex);

                InvokeFailureProgressEvent(GraphProgress.AnalizingFailed, "ENTER FAIL REASON HERE");
                //RETHROW EXCEPTION
            }
            finally
            {
                InvokeProgressEvent(GraphProgress.Done, 100);
            }

            log.Info("Finished analizing model");
        }

        public override bool CheckGenerationParams(int instances)
        {
            System.Diagnostics.PerformanceCounter ramCounter = new System.Diagnostics.PerformanceCounter("Memory", "Available Bytes");
            UInt64 vertex = UInt64.Parse(GenerationParamValues[GenerationParam.Vertices].ToString());
            UInt64 vertexmemory = vertex * (vertex - 1) / 16;
            int processorcount = Environment.ProcessorCount;
            return vertexmemory < ramCounter.NextValue() / processorcount
                   && (int)GenerationParamValues[GenerationParam.Vertices] < 32000;
        }

        public override string GetParamsInfo()
        {
            return "";
        }

        public override bool[,] GetMatrix()
        {
            return analyzer.Container.GetMatrix();
        }

        public override void Dispose()
        {
            log.Info("disposing...");
            generator = null;
            analyzer = null;
            base.Dispose();
        }
    }
}
