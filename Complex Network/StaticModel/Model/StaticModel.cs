using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.StaticModel.Realization;
using System.Threading;
using Algorithms;
using GenericAlgorithms;

namespace Model.StaticModel
{
    [GraphModel("Static Model", GenerationRule.Sequential, "Static Model")]
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
     AnalyseOptions.FullSubGraph)]
    [RequiredGenerationParam(GenerationParam.FilePath, 1)]

    public class StaticModel : AbstractGraphModel
    {
        private static readonly string MODEL_NAME = "StaticModel";
        private StaticGraph StaticModelGraph;
       // bool cout = true;

        public StaticModel() { }

        public StaticModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
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
            genParams.Add(GenerationParam.MaxEdges);
            RequiredGenerationParams = genParams;

            //Place additional initialization code here

            InvokeProgressEvent(GraphProgress.Ready);
        }


        protected override void GenerateModel()
        {
            InvokeProgressEvent(GraphProgress.StartingGeneration, 5);

            try
            {
                if (Graph == null)
                {
                    //Place generation initialization code here
                    String filePath = (String)GenerationParamValues[GenerationParam.FilePath];
                    ArrayList matrix = MatrixFileReader.MatrixReader(filePath);
                    StaticModelGraph = new StaticGraph(matrix);
                    Graph = StaticModelGraph;
                }
                else
                {
                    StaticModelGraph = (StaticGraph)Graph;//((BAGraph)Graph).Copy();
                    Graph = StaticModelGraph;
                    System.Console.WriteLine("Copy PROCESS STARTED");
                }
                InvokeProgressEvent(GraphProgress.Generating, 10);
                //Place generating logic here
                //Invoke ModelProgress event if possible to show current
                //state with use of Percent and TargetItem properties

                //Graph assignment is not needed for HEIRARCHIC(NOT NEEDED IF GENERATION 
                //RULE IS SEPARATE) 
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
                //Get degree distrubtion

                // BAModelGraph.Analyze();
             if (((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath) ||
             ((AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter) ||
             ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4) || ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient) || ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3))
                {
                    StaticModelGraph.Analyze(AnalizeOptions & AnalyseOptions.AveragePath);
                    InvokeProgressEvent(GraphProgress.Analizing, 39, "Average path distrubution");
                    if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                    {
                        Result.Result[AnalyseOptions.AveragePath] = StaticModelGraph.Result.m_avgPathLenght;
                    }
                    //Get diameter

                    if ((AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter)
                    {

                        StaticModelGraph.Analyze(AnalizeOptions & AnalyseOptions.Diameter);
                        InvokeProgressEvent(GraphProgress.Analizing, 46, "Diameter");
                        Result.Result[AnalyseOptions.Diameter] = StaticModelGraph.Result.m_diametr;

                    }
                    // Get classtering coefficient

                    if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                    {
                        StaticModelGraph.Analyze(AnalizeOptions & AnalyseOptions.ClusteringCoefficient);
                        InvokeProgressEvent(GraphProgress.Analizing, 53, "Classtering Coefficient");
                        Result.Result[AnalyseOptions.ClusteringCoefficient] = StaticModelGraph.Result.m_clusteringCoefficient;
                        Result.Coefficient = StaticModelGraph.Result.m_iclusteringCoefficient;
                    }
                    if ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                    {
                        StaticModelGraph.Analyze(AnalizeOptions & AnalyseOptions.Cycles3);
                        InvokeProgressEvent(GraphProgress.Analizing, 67, "Diameter");
                        Result.Result[AnalyseOptions.Cycles3] = StaticModelGraph.Result.m_cyclesOfOrder3;

                    }
                    if ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                    {
                        StaticModelGraph.Analyze(AnalizeOptions & AnalyseOptions.Cycles4);
                        InvokeProgressEvent(GraphProgress.Analizing, 75, "Diameter");
                        Result.Result[AnalyseOptions.Cycles4] = StaticModelGraph.Result.m_cyclesOfOrder4;

                    }
                }

                if ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                {
                    StaticModelGraph.Analyze(AnalizeOptions & AnalyseOptions.DegreeDistribution);
                    InvokeProgressEvent(GraphProgress.Analizing, 32, "Degree distrubution");
                    //  double[] degress = BAModelGraph.m_analyzeOptions
                    double avgDegree = 0;
                    foreach (KeyValuePair<int, int> pair in StaticModelGraph.Result.m_degreeDistribution)
                    {
                        avgDegree += pair.Key;
                    }
                    Result.Result[AnalyseOptions.DegreeDistribution] = avgDegree / StaticModelGraph.Result.m_degreeDistribution.Count;
                    Result.VertexDegree = StaticModelGraph.Result.m_degreeDistribution;

                }
                if ((AnalizeOptions & AnalyseOptions.EigenValue) == AnalyseOptions.EigenValue)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 90, "Calculating EigenValue");
                  //  BAModelGraph.Analyze(AnalizeOptions & AnalyseOptions.EigenValue);
                 //   Result.EigenVector = BAModelGraph.Result.ArrayOfEigVal;
                     Algorithms.EigenValue ev = new EigenValue();
                     ArrayList al = new ArrayList();
                     bool[,] m = GetMatrix();
                     Result.EigenVector = ev.EV(m);
                     Result.DistancesBetweenEigenValues = ev.CalcEigenValuesDist();
                }
                //Get average path.


                //Get diameter


                // Get classtering coefficient



                // Get full Subgraphs array

                if ((AnalizeOptions & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph)
                {
                    StaticModelGraph.Analyze(AnalizeOptions & AnalyseOptions.FullSubGraph);
                    InvokeProgressEvent(GraphProgress.Analizing, 60, "Full Subgraphs");
                    Result.Result[AnalyseOptions.FullSubGraph] = StaticModelGraph.Result.m_maxfullsubgraph;
                }

                //Get cycles3
                //Place analizing logic here
                //Invoke ModelProgress event if possible to show current
                //state with use of Percent and TargetItem properties

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
            return true;
        }
        public override string GetParamsInfo()
        {
            return "";
        }
        public override bool[,] GetMatrix()
        {
            return StaticModelGraph.Container.GetMatrix();
        }
    }
}
