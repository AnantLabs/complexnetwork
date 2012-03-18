﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.ERModel.Realization;
using System.Threading;
using Algorithms;

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
        private static readonly string MODEL_NAME = "ERModel";
        private ERGraph ERModelGraph;

        public ERModel() { }

        public ERModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
            : base(genParam, options, sequenceNumber)
        {
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
        }

        protected override void GenerateModel()
        {
            InvokeProgressEvent(GraphProgress.StartingGeneration, 5);

            try
            {
                ERModelGraph = new ERGraph((Int32)GenerationParamValues[GenerationParam.Vertices]); //,
                            //(Int16)GenerationParamValues[GenerationParam.P]);
                Graph = ERModelGraph;
                InvokeProgressEvent(GraphProgress.Generating, 10);
                ERModelGraph.Generate((double)GenerationParamValues[GenerationParam.P]);
                InvokeProgressEvent(GraphProgress.GenerationDone, 30);
            }
            catch (ThreadAbortException) { }
            catch (Exception)
            {
                InvokeFailureProgressEvent(GraphProgress.GenerationFailed, "ENTER FAIL REASON HERE");
                //RETHROW EXCEPTION 
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
                if (((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                    || ((AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter))
                {
                    ERModelGraph.Analyze(AnalizeOptions & AnalyseOptions.Diameter);
                }

                if ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                {
                    ERModelGraph.Analyze(AnalizeOptions & AnalyseOptions.DegreeDistribution);
                    InvokeProgressEvent(GraphProgress.Analizing, 32, "Degree distrubution");
                    Result.Result[AnalyseOptions.DegreeDistribution] = ERModelGraph.Result.m_avgDegree;
                    Result.VertexDegree = ERModelGraph.Result.m_degreeDistribution;
                }

                if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 39, "Average path distrubution");
                    Result.Result[AnalyseOptions.AveragePath] = ERModelGraph.Result.m_avgPathLenght;
                    Result.DistanceBetweenVertices = ERModelGraph.Result.m_pathDistribution;
                }

                if ((AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 46, "Diameter");
                    Result.Result[AnalyseOptions.Diameter] = ERModelGraph.Result.m_diameter;
                }

                if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                {
                    ERModelGraph.Analyze(AnalizeOptions & AnalyseOptions.ClusteringCoefficient);
                    InvokeProgressEvent(GraphProgress.Analizing, 53, "Classtering Coefficient");
                    Result.Result[AnalyseOptions.ClusteringCoefficient] = ERModelGraph.Result.m_clusteringCoefficient;
                    Result.Coefficient = ERModelGraph.Result.m_vertexClusteringCoefficient;
                }

                if ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                {
                    ERModelGraph.Analyze(AnalizeOptions & AnalyseOptions.Cycles3);
                    InvokeProgressEvent(GraphProgress.Analizing, 67, "Cycles of order 3");
                    Result.Result[AnalyseOptions.Cycles3] = ERModelGraph.Result.m_cyclesOfOrder3;
                }

                if ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                {
                    ERModelGraph.Analyze(AnalizeOptions & AnalyseOptions.Cycles4);
                    InvokeProgressEvent(GraphProgress.Analizing, 75, "Cycles of order 4");
                    Result.Result[AnalyseOptions.Cycles4] = ERModelGraph.Result.m_cyclesOfOrder4;
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
                    ERModelGraph.Analyze(AnalizeOptions & AnalyseOptions.FullSubGraph);
                    InvokeProgressEvent(GraphProgress.Analizing, 60, "Full Subgraphs");
                    Result.Result[AnalyseOptions.FullSubGraph] = ERModelGraph.Result.m_maxfullsubgraph;
                }

                InvokeProgressEvent(GraphProgress.AnalizingDone, 95);

            }
            catch (Exception ex)
            {
                InvokeFailureProgressEvent(GraphProgress.AnalizingFailed, "ENTER FAIL REASON HERE");
                //RETHROW EXCEPTION
            }
            finally
            {
                InvokeProgressEvent(GraphProgress.Done, 100);
            }

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
    }
}
