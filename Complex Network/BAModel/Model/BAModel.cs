using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.BAModel.Realization;
using System.Threading;
using Algorithms;

namespace Model.BAModel
{
    [GraphModel("Barabasi-Albert", GenerationRule.Sequential, "Barabasi-Albert graph model")]
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
     AnalyseOptions.Cycles |
     AnalyseOptions.Motifs |
     AnalyseOptions.MaxFullSubgraph)]
    [RequiredGenerationParam(GenerationParam.Vertices, 2)]
    [RequiredGenerationParam(GenerationParam.MaxEdges, 3)]
    [RequiredGenerationParam(GenerationParam.AddVertices, 10)]

    public class BAModel : AbstractGraphModel
    {
        private static readonly string MODEL_NAME = "BAModel";
        private BAGraph BAModelGraph;
        private BAGenerator BAModelGenerator;
        // private ArrayList mMatrix;
        // private Dictionary<int, List<int>> mNegList;
        bool cout = true;

        public BAModel() { }

        public BAModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
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
            genParams.Add(GenerationParam.AddVertices);
            RequiredGenerationParams = genParams;

            //Place additional initialization code here

            InvokeProgressEvent(GraphProgress.Ready);
        }


        protected override void GenerateModel()
        {
            InvokeProgressEvent(GraphProgress.StartingGeneration, 5);

            try
            {
                BAModelGraph = new BAGraph((Int32)GenerationParamValues[GenerationParam.Vertices],
                  (Int16)GenerationParamValues[GenerationParam.MaxEdges]);
                Graph = BAModelGraph;
                InvokeProgressEvent(GraphProgress.Generating, 10);
                BAModelGenerator = new BAGenerator(BAModelGraph);
                BAModelGenerator.Generate((Int32)GenerationParamValues[GenerationParam.AddVertices]);
                this.BAModelGraph = BAModelGenerator.Graph;
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
            Result.graphSize = BAModelGraph.Container.Size;
            try
            {
                //Get degree distrubtion

                BAModelGraph.Analyze();
                if (((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath) ||
             ((AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter) ||
             ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4) || ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient) || ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3))
                {
                    BAModelGraph.m_analyzer.CountAnalyzeOptions();

                    if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 25, "Average path distrubution");
                        Result.Result[AnalyseOptions.AveragePath] = BAModelGraph.m_analyzer.GetAveragePath();
                    }
                    //Get diameter

                    if ((AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 30, "Diameter");
                        Result.Result[AnalyseOptions.Diameter] = BAModelGraph.m_analyzer.GetDiameter();
                    }
                    // Get classtering coefficient

                    if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 35, "Classtering Coefficient");
                        Result.Coefficient = BAModelGraph.m_analyzer.GetClusteringCoefficient();
                    }
                    if ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 40, "Diameter");
                        Result.Result[AnalyseOptions.Cycles3] = BAModelGraph.m_analyzer.GetCycles3();
                    }
                    if ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                    {
                        InvokeProgressEvent(GraphProgress.Analizing, 45, "Diameter");
                    }
                }
                if ((AnalizeOptions & AnalyseOptions.Cycles) == AnalyseOptions.Cycles)
                {
                    int maxValue = Int32.Parse((String)AnalizeOptionsValues["cyclesHi"]);
                    int minvalue = Int32.Parse((String)AnalizeOptionsValues["cyclesLow"]);

                    InvokeProgressEvent(GraphProgress.Analizing, 70, "Cycles of " + minvalue + "-" + maxValue + "degree");
                    // CORRECT ME!! //
                    Result.Cycles = BAModelGraph.m_analyzer.getNCyclesCount(minvalue, maxValue);
                    //calculate cycles here
                    //Result.CyclesCount = 
                }
                if ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                {
                    SortedDictionary<int, int> degree = BAModelGraph.m_analyzer.GetDegreeDistribution();
                    InvokeProgressEvent(GraphProgress.Analizing, 50, "Degree distrubution");
                    double avgDegree = 0;
                    foreach (KeyValuePair<int, int> pair in degree)
                    {
                        avgDegree += pair.Key;
                    }
                    Result.Result[AnalyseOptions.DegreeDistribution] = avgDegree / degree.Count;
                    Result.VertexDegree = degree;

                }
                if ((AnalizeOptions & AnalyseOptions.EigenValue) == AnalyseOptions.EigenValue)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 55, "Calculating EigenValue");
                    //  BAModelGraph.Analyze(AnalizeOptions & AnalyseOptions.EigenValue);
                    //   Result.EigenVector = BAModelGraph.Result.ArrayOfEigVal;
                    Algorithms.EigenValue ev = new EigenValue();
                    ArrayList al = new ArrayList();
                    bool[,] m = GetMatrix();
                    Result.EigenVector = ev.EV(m);
                    Result.DistancesBetweenEigenValues = ev.CalcEigenValuesDist();
                }

                if ((AnalizeOptions & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 60, "Full Subgraphs");
                    Result.Result[AnalyseOptions.FullSubGraph] = BAModelGraph.m_analyzer.GetMaxFullSubgraph();
                }

               


                if ((AnalizeOptions & AnalyseOptions.Motifs) == AnalyseOptions.Motifs)
                {
                    int maxValue = Int32.Parse((String)AnalizeOptionsValues["motiveHi"]);
                    int minvalue = Int32.Parse((String)AnalizeOptionsValues["motiveLow"]);
                    InvokeProgressEvent(GraphProgress.Analizing, 80, "Motiv of " + minvalue + "-" + maxValue + "degree");
                    //calculate motives here
                    Result.MotivesCount = new SortedDictionary<int, int>(); //for test
                    Result.MotivesCount.Add(5, 10);
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
            int vertex = (Int32)GenerationParamValues[GenerationParam.Vertices];
            int edges = (Int16)GenerationParamValues[GenerationParam.MaxEdges];
            int assamblecount = (Int32)GenerationParamValues[GenerationParam.AddVertices];
            if (vertex < edges || (vertex * 40 / 100) > assamblecount)
                return false;

            return true;
        }
        public override string GetParamsInfo()
        {
            int edges = (Int16)GenerationParamValues[GenerationParam.MaxEdges];
            int vertex = (Int32)GenerationParamValues[GenerationParam.Vertices];
            int assamblecount = (Int32)GenerationParamValues[GenerationParam.AddVertices];
            if (edges > vertex)
                return "Initial vertex count mast be greater then edges count";
            if ((vertex * 40 / 100) > assamblecount)
                return "Add vertex count must be greater then 40 percent of initial vertex count";
            return "";

        }
        public override bool[,] GetMatrix()
        {
            return BAModelGraph.Container.GetMatrix();
        }
    }
}
