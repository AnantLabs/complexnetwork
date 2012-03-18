using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.WSModel.Realization;
using Model.WSModel.Result;
using Algorithms;

namespace Model.WSModel
{
    [GraphModel("Watts-Strogatz", GenerationRule.Sequential, "Watts-Strogatz graph model")]
    [AvailableAnalyzeOptions(
        AnalyseOptions.DegreeDistribution |
        AnalyseOptions.AveragePath |
        AnalyseOptions.EigenValue | 
        AnalyseOptions.ClusteringCoefficient |
        AnalyseOptions.Cycles3 |
        AnalyseOptions.Cycles4 |
        AnalyseOptions.Diameter |
        AnalyseOptions.ConnSubGraph)]
    [RequiredGenerationParam(GenerationParam.P, 10)]
    [RequiredGenerationParam(GenerationParam.Vertices, 2)]
    [RequiredGenerationParam(GenerationParam.Edges, 3)]

    public class WSModel : AbstractGraphModel
    {
        private static readonly string MODEL_NAME = "Watts-Strogatz";
        private WSGraph graph;
        public WSModel() { }

        public WSModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
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
                                AnalyseOptions.ClusteringCoefficient |
                                AnalyseOptions.Cycles3 |
                                AnalyseOptions.Cycles4 |
                                AnalyseOptions.EigenValue |
                                AnalyseOptions.ConnSubGraph;

            //Defines required input parameters for generation
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Edges);
            genParams.Add(GenerationParam.Vertices);
            genParams.Add(GenerationParam.P);
            RequiredGenerationParams = genParams;

            graph = new WSGraph((int)GenerationParamValues[GenerationParam.Vertices],
                                            (int)GenerationParamValues[GenerationParam.Edges],
                                            (double)GenerationParamValues[GenerationParam.P]);

            //Place additional initialization code here

            InvokeProgressEvent(GraphProgress.Ready);
        }


        protected override void GenerateModel()
        {
            InvokeProgressEvent(GraphProgress.StartingGeneration, 5);
            try
            {
                //Place generation initialization code here
                if (graph == null)
                {

                    graph = new WSGraph((int)GenerationParamValues[GenerationParam.Vertices],
                                            (int)GenerationParamValues[GenerationParam.Edges],
                                            (double)GenerationParamValues[GenerationParam.P]);
                }


                InvokeProgressEvent(GraphProgress.Generating, 10);

                //Place generating logic here
                //Invoke ModelProgress event if possible to show current
                //state with use of Percent and TargetItem properties

                //graph.Generate();
                //Dictionary<int, List<int>> matrix = graph.Container.getMatrix();
                InvokeProgressEvent(GraphProgress.GenerationDone, 30);

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
                ////Place generation initialization code here
                WSAnalyzer analizer = new WSAnalyzer(graph.Container);

                //Get degree distrubtion

                if ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 35, "Degree distrubution");
                    analizer.DegreeDistribution();
                    Result.VertexDegree = analizer.Result.m_degreeDistribution;
                    Result.Result[AnalyseOptions.DegreeDistribution] = 1;

                }
                //Get average path and diameter

                if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath 
                    || (AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter
                    || (AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                {
                    analizer.CountAvgPathAndDiametr();
                    if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 50, "Average path distrubution");
                        Result.Result[AnalyseOptions.AveragePath] = analizer.Result.m_avgPathLenght;
                        Result.DistanceBetweenVertices = analizer.Result.m_vertexDistances;
                    }
                    if ((AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 65, "Diameter");
                        Result.Result[AnalyseOptions.Diameter] = analizer.Result.m_diametr;
                    }
                    if ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 70, "Cycles4");
                        Result.Result[AnalyseOptions.Cycles4] = analizer.Result.m_cyclesOfOrder4;
                    }

                }


                if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient ||
                    (AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3 ||
                    (AnalizeOptions & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph)
                {
                    analizer.ClusteringCoefficient();
                    if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 75, "Clustering Coefficient");
                        Result.Result[AnalyseOptions.ClusteringCoefficient] = analizer.Result.m_clusteringCoefficient;
                        Result.Coefficient = analizer.Result.m_coefficient;
                    }

                    if ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 80, "Cycles3");
                        Result.Result[AnalyseOptions.Cycles3] = analizer.CyclesOfOrder3();
                    }

                    if ((AnalizeOptions & AnalyseOptions.ConnSubGraph) == AnalyseOptions.ConnSubGraph)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 85, "Full Subgraph");
                        Result.Result[AnalyseOptions.ConnSubGraph] = analizer.MaxFullSubGraph();
                        Result.Subgraphs = analizer.Result.m_fullSubgraphs;
                    }

                }
                if ((AnalizeOptions & AnalyseOptions.EigenValue) == AnalyseOptions.EigenValue)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 90, "Calculating EigenValue");
                    Algorithms.EigenValue ev = new EigenValue();
                    bool[,] m = GetMatrix();
                    Result.EigenVector = ev.EV(m);
                    Result.DistancesBetweenEigenValues = ev.CalcEigenValuesDist();
                }
                /*
                if ((AnalizeOptions & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 80, "Connected Subgraph");
                    Result.Subgraphs = analizer.getConnectedSubGraphs(this.tree);
                    Result.Result[AnalyseOptions.FullSubGraph] = 1;
                }

                if ((AnalizeOptions & AnalyseOptions.CycleEigen3) == AnalyseOptions.CycleEigen3)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 85, "Cycle Count By Eigen Value");
                    Result.Result[AnalyseOptions.CycleEigen3] = analizer.CalcCiclesCount(this.tree, 3);
                }
                */
                //Place analizing logic here
                //Invoke ModelProgress event if possible to show current
                //state with use of Percent and TargetItem properties

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
            //&& (int)GenerationParam.Edges > Math.Log((double)GenerationParam.Vertices);
            // (Int16)GenerationParamValues[GenerationParam.BranchIndex] < 18;
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
            //generator = null;
            //tree = null;
        }

        public override bool[,] GetMatrix()
        {
            //throw new NotImplementedException();
            return graph.Container.GetMatrix();
        }

        public bool[,] MatrixToBool(ArrayList matrix)
        {
            bool[,] result = new bool[matrix.Count, matrix.Count];
            ArrayList lst;
            for (int i = 0; i < matrix.Count; ++i)
            {
                lst = (ArrayList)matrix[i];
                for (int j = 0; j < matrix.Count; ++j)
                    result[i,j] = (bool)lst[j];
            }

            return result;
        }
    }
}
