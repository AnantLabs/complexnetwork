using System;
using System.Collections.Generic;
using System.Text;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using System.Threading;
using Model.NonRegularHierarchicModel.Realization;


namespace Model.NonRegularHierarchicModel
{
    [GraphModel("Non Regular Block-Hierarchic", GenerationRule.Separate, "Non regular block-hierarchic graph model")]
    [AvailableAnalyzeOptions(AnalyseOptions.DegreeDistribution |
        AnalyseOptions.AveragePath |
        AnalyseOptions.Cycles3 |
        AnalyseOptions.FullSubGraph |
        AnalyseOptions.Cycles4 |
        AnalyseOptions.MinPathDist |
        AnalyseOptions.DistEigenPath |
        AnalyseOptions.EigenValue |
    AnalyseOptions.ClusteringCoefficient)]
    [RequiredGenerationParam(GenerationParam.BranchIndex, 1)]
    [RequiredGenerationParam(GenerationParam.Level, 2)]
    [RequiredGenerationParam(GenerationParam.Mu, 3)]

    public class NonRegularHierarchicModel : AbstractGraphModel
    {
        private static readonly string MODEL_NAME = "Non-Regular Hierarchic";
        private NonRegularHierarchicGraph graph = new NonRegularHierarchicGraph();

        public NonRegularHierarchicModel() { }

        public NonRegularHierarchicModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
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
            GenerationRule = GenerationRule.Separate;

            //Defines available options for analizer
            AvailableOptions = AnalyseOptions.DegreeDistribution |
                                AnalyseOptions.AveragePath |
                                AnalyseOptions.Cycles3 |
                                AnalyseOptions.ClusteringCoefficient |
                                AnalyseOptions.FullSubGraph |
                                AnalyseOptions.MinPathDist |
                                AnalyseOptions.DistEigenPath |
                                AnalyseOptions.EigenValue |
                                AnalyseOptions.Cycles4;

            //Defines required input parameters for generation
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Level);
            genParams.Add(GenerationParam.BranchIndex);
            genParams.Add(GenerationParam.Mu);
            RequiredGenerationParams = genParams;

            //Place additional initialization code here

            InvokeProgressEvent(GraphProgress.Ready);
        }

        protected override void GenerateModel()
        {
            InvokeProgressEvent(GraphProgress.StartingGeneration, 0, "Generating");
            try
            {
                //Place generation initialization code here

                InvokeProgressEvent(GraphProgress.Generating, 8);

                Int16 a = (Int16)GenerationParamValues[GenerationParam.BranchIndex];
                Int16 b = (Int16)GenerationParamValues[GenerationParam.Level];
                Double doooooooo = (Double)GenerationParamValues[GenerationParam.Mu];

                graph.generate_with((Int16)GenerationParamValues[GenerationParam.BranchIndex],
                                                        (Int16)GenerationParamValues[GenerationParam.Level],
                                                        (Double)GenerationParamValues[GenerationParam.Mu]);

                //Graph assignment is not needed for HEIRARCHIC(NOT NEEDED IF GENERATION 
                //RULE IS SEPARATE) 
                InvokeProgressEvent(GraphProgress.GenerationDone, 8);

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
                NonRegularHierarchicAnalyzer analizer = new NonRegularHierarchicAnalyzer(graph);
                int tasksTimes = 8;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4) ? 150 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient) ? 20 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3) ? 55 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath) ? 30 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution) ? 20 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph) ? 5 : 0;
                int timer = 8;

                if ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Degree distrubution");
                    timer += 100 * 20 / tasksTimes;
                    Result.VertexDegree = analizer.GetDegreeDistribution();
                    Result.Result[AnalyseOptions.DegreeDistribution] = 1;
                    Result.Result[AnalyseOptions.MinPathDist] = 1;
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }
                //Get average path

                if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Average path distrubution");
                    timer += 100 * 30 / tasksTimes;

                    /// KM TODO, create separate block for average path distribution.
                    Result.DistanceBetweenVertices = analizer.GetMinPathDist();

                    Result.Result[AnalyseOptions.AveragePath] = analizer.GetAveragePath();
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }
                if ((AnalizeOptions & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Connected Subgraph");
                    timer += 100 * 5 / tasksTimes;
                    Result.FullSubgraphs = analizer.GetConnSubGraph();
                    Result.Result[AnalyseOptions.FullSubGraph] = 1;
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }
                if ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Cycles count");
                    timer += 100 * 55 / tasksTimes;
                    Result.Result[AnalyseOptions.Cycles3] = analizer.GetCycles3();
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }
                if ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Count cycles with 4 length");
                    timer += 100 * 150 / tasksTimes;
                    Result.Result[AnalyseOptions.Cycles4] = analizer.GetCycles4();
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }

                if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Clustering Coefficient");
                    timer += 100 * 20 / tasksTimes;
                    Result.Coefficient = analizer.GetClusteringCoefficient();
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }
                if ((AnalizeOptions & AnalyseOptions.EigenValue) == AnalyseOptions.EigenValue)
                {
                    // InvokeProgressEvent(GraphProgress.Analizing, 90, "EigenValue");
                    // Algorithms.EigenValue ev = new EigenValue();
                    // bool[,] m = GetMatrix();
                    //Result.EigenVector = ev.EV(m);
                    //Result.DistancesBetweenEigenValues = ev.CalcEigenValuesDist();
                }
                //Place analizing logic here
                //Invoke ModelProgress event if possible to show current
                //state with use of Percent and TargetItem properties

                //InvokeProgressEvent(GraphProgress.AnalizingDone, 95);
                InvokeProgressEvent(GraphProgress.Done, 100);

            }
            catch (ThreadAbortException) { }
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
            System.Diagnostics.PerformanceCounter ramCounter = new System.Diagnostics.PerformanceCounter("Memory", "Available Bytes");
            int branch = (Int16)GenerationParamValues[GenerationParam.BranchIndex];
            int level = (Int16)GenerationParamValues[GenerationParam.Level];
            UInt32 vertexcount = (UInt32)(System.Math.Pow(branch, level));
            int processorcount = Environment.ProcessorCount;
            return processorcount * vertexcount < ramCounter.NextValue();
        }

        public override void Dispose()
        {
            // KM TODO
        }

        public override string GetParamsInfo()
        {
            return "";
        }

        public override bool[,] GetMatrix()
        {
            // KM TODO
            /*int primeNumber = generator.prime;
            int maxlevel = generator.degree;
            int nodeDataLength = (primeNumber - 1) * primeNumber / 2;
            long dataLength = Convert.ToInt64(Math.Pow(primeNumber, maxlevel) * nodeDataLength);
            bool[,] matrix = new bool[maxlevel, dataLength];

            for (int i = 0; i < maxlevel; i++)
            {
                for (int j = 0; j < dataLength; j++)
                {
                    if (j < generator.treeMatrix[i][0].Length)
                    {
                        matrix[i, j] = (generator.treeMatrix[i][0][j]) ? true : false;
                    }
                    else
                    {
                        matrix[i, j] = false;
                    }
                }
            }
            return matrix;*/
            return new bool[10, 10];
        }
    }
}
