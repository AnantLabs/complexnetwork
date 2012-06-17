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
    [GraphModel("ERModel", GenerationRule.Sequential, "ERModel graph model")]
    [AvailableAnalyzeOptions(
         AnalyseOptions.DegreeDistribution |
         AnalyseOptions.AveragePath |
         AnalyseOptions.Cycles3 |
         AnalyseOptions.Cycles4 |
         AnalyseOptions.ClusteringCoefficient |
         AnalyseOptions.Diameter |
         AnalyseOptions.MinPathDist |
         AnalyseOptions.DistEigenPath |
         AnalyseOptions.EigenValue |
         AnalyseOptions.MaxFullSubgraph)]
    [RequiredGenerationParam(GenerationParam.Vertices, 2)]
    [RequiredGenerationParam(GenerationParam.P, 3)]

    public class ERModel : AbstractGraphModel
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERModel));

        private static readonly string MODEL_NAME = "ERModel";
        private ERGraph ERModelGraph;

        public ERModel() { }

        public ERModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
            : base(genParam, options, sequenceNumber)
        {
            log.Info("Creating ERModel object");
            ValidateModelParams();
            InitModel();
        }

        private void ValidateModelParams()
        {
            //TODO Put input params validation here
            //and throw WrongModelParamsException
        }

        private void InitModel()
        {
            log.Info("Started model initialization");
            InvokeProgressEvent(GraphProgress.Initializing, 0);
            ModelName = MODEL_NAME;
            //Defines separate generation rule
            GenerationRule = GenerationRule.Sequential;

            //Defines available options for analizer
            AvailableOptions = AnalyseOptions.DegreeDistribution |
                                AnalyseOptions.AveragePath |
                                AnalyseOptions.Diameter |
                                AnalyseOptions.Cycles3 |
                                AnalyseOptions.Cycles4 |
                                AnalyseOptions.FullSubGraph |
                                AnalyseOptions.MinPathDist |
                                AnalyseOptions.DistEigenPath |
                                AnalyseOptions.EigenValue |
                                AnalyseOptions.ClusteringCoefficient;

            //Defines required input parameters for generation
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Vertices);
            genParams.Add(GenerationParam.P);
            RequiredGenerationParams = genParams;

            //Place additional initialization code here

            InvokeProgressEvent(GraphProgress.Ready);

            log.Info("Finished model initialization");
        }

        protected override void GenerateModel()
        {
            log.Info("Started model generation");

            InvokeProgressEvent(GraphProgress.StartingGeneration, 5);

            try
            {
                ERModelGraph = new ERGraph((Int32)GenerationParamValues[GenerationParam.Vertices]);
                Graph = ERModelGraph;
                InvokeProgressEvent(GraphProgress.Generating, 10);
                ERModelGraph.Generate((double)GenerationParamValues[GenerationParam.P]);
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
                if ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 32, "Degree distrubution");
                    Result.Result[AnalyseOptions.DegreeDistribution] = ERModelGraph.m_analyzer.GetAverageDegree();
                    Result.VertexDegree = ERModelGraph.m_analyzer.GetDegreeDistribution();
                }

                if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 39, "Average path distrubution");
                    Result.Result[AnalyseOptions.AveragePath] = ERModelGraph.m_analyzer.GetAveragePath();
                    Result.DistanceBetweenVertices = ERModelGraph.m_analyzer.GetMinPathDist();
                }

                if ((AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 46, "Diameter");
                    Result.Result[AnalyseOptions.Diameter] = ERModelGraph.m_analyzer.GetDiameter();
                }

                if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 53, "Classtering Coefficient");
                    Result.Result[AnalyseOptions.ClusteringCoefficient] = ERModelGraph.m_analyzer.GetAvgClusteringCoefficient();
                    Result.Coefficient = ERModelGraph.m_analyzer.GetClusteringCoefficient();
                }

                if ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 67, "Cycles of order 3");
                    Result.Result[AnalyseOptions.Cycles3] = ERModelGraph.m_analyzer.GetCycles3();
                }

                if ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 75, "Cycles of order 4");
                    Result.Result[AnalyseOptions.Cycles4] = ERModelGraph.m_analyzer.GetCycles4();
                }

                if ((AnalizeOptions & AnalyseOptions.EigenValue) == AnalyseOptions.EigenValue)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 90, "Calculating EigenValue");
                    Algorithms.EigenValue ev = new EigenValue();
                    ArrayList al = new ArrayList();
                    bool[,] m = GetMatrix();
                    Result.EigenVector = ev.EV(m);
                    Result.DistancesBetweenEigenValues = ev.CalcEigenValuesDist();
                }

                if ((AnalizeOptions & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 60, "Full Subgraphs");
                    Result.Result[AnalyseOptions.FullSubGraph] = ERModelGraph.m_analyzer.GetMaxFullSubgraph();
                }

                Result.graphSize = ERModelGraph.Container.Size;

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
            //throw new NotImplementedException();
            return ERModelGraph.Container.GetMatrix();
        }

        //public override void Dispose()
        //{
        //    log.Info("disposing...");
        //    ERModelGraph = null;
        //    base.Dispose();
        //}

        protected override void StaticGenerateModel()
        {
            throw new NotImplementedException();
        }
    }
}
