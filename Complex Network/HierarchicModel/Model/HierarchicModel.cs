using System;
using System.Collections.Generic;
using System.Text;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using System.Threading;
using Algorithms;
using Model.HierarchicModel.Realization;
//using RandomGraph.Common.Model;

namespace Model.HierarchicModel
{
    [GraphModel("Block-Hierarchic", GenerationRule.Separate, "Block-hierarchic graph model")]
    [AvailableAnalyzeOptions(AnalyseOptions.DegreeDistribution | 
        AnalyseOptions.AveragePath | 
        AnalyseOptions.Cycles3 | 
        AnalyseOptions.FullSubGraph |
        AnalyseOptions.Cycles4 |
        AnalyseOptions.MinPathDist |
        AnalyseOptions.DistEigenPath |
        AnalyseOptions.EigenValue |
        AnalyseOptions.Cycles |
    AnalyseOptions.ClusteringCoefficient)]
    [RequiredGenerationParam(GenerationParam.BranchIndex, 1)]
    [RequiredGenerationParam(GenerationParam.Level, 2)]
    [RequiredGenerationParam(GenerationParam.Mu, 3)]

    public class HierarchicModel : AbstractGraphModel
    {
        private static readonly string MODEL_NAME = "Hierarchic";
        private HierarchicGraph tree;

        private HierarchicGraphGenerator generator;

        public HierarchicModel() { }

        public HierarchicModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
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

                generator = new HierarchicGenerator((Int16)GenerationParamValues[GenerationParam.BranchIndex],
                                                        (Int16)GenerationParamValues[GenerationParam.Level],
                                                        (Double)GenerationParamValues[GenerationParam.Mu]);

                this.tree = new HierarchicGraph((Int16)GenerationParamValues[GenerationParam.BranchIndex],
                                                    (Int16)GenerationParamValues[GenerationParam.Level], generator.treeMatrix);

                InvokeProgressEvent(GraphProgress.Generating, 8);

                //Place generating logic here
                //Invoke ModelProgress event if possible to show current
                //state with use of Percent and TargetItem properties

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
            Result.graphSize = tree.getGraphSize();
            try
            {
                ////Place generation initialization code here
                HierarchicAnalyzer analyzer = new HierarchicAnalyzer(tree);
                int tasksTimes = 8;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4) ? 150 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient) ? 20 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3) ? 55 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath) ? 30 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution) ? 20 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph) ? 5 : 0;
                int timer = 8;

                // Getting degree distribution and distanes between verices
                if ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                {
                    //InvokeProgressEvent(GraphProgress.Analizing, timer, "Degree distrubution");
                    timer += 100 * 20 / tasksTimes;
                    analyzer.Tree = tree;
                    SortedDictionary<int, int> res = analyzer.GetDegreeDistribution();
                    Result.VertexDegree = res;
                    Result.DistanceBetweenVertices = res;
                    Result.Result[AnalyseOptions.DegreeDistribution] = 1;
                    Result.Result[AnalyseOptions.MinPathDist] = 1;
                    //InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                    InvokeProgressEvent(GraphProgress.Analizing, 10, "");
                }

                // Getting average path
                if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                {
                    //InvokeProgressEvent(GraphProgress.Analizing, timer, "Average path distrubution");
                    timer += 100 * 30 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Result[AnalyseOptions.AveragePath] = analyzer.GetAveragePath();
                    //InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                    InvokeProgressEvent(GraphProgress.Analizing, 20, "");
                }

                // Getting connected subgraph graph
                if ((AnalizeOptions & AnalyseOptions.ConnSubGraph) == AnalyseOptions.ConnSubGraph)
                {
                    //InvokeProgressEvent(GraphProgress.Analizing, timer, "Connected Subgraph");
                    timer += 100 * 5 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.FullSubgraphs = analyzer.GetConnSubGraph();
                    Result.Result[AnalyseOptions.ConnSubGraph] = 1;
                    //InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                    InvokeProgressEvent(GraphProgress.Analizing, 30, "");
                }

                // Getting count of 3 cycles
                if ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                {
                    //InvokeProgressEvent(GraphProgress.Analizing, timer, "Count cycles with 3 length");
                    timer += 100 * 55 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Result[AnalyseOptions.Cycles3] = analyzer.GetCycles3();
                   // InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                    InvokeProgressEvent(GraphProgress.Analizing, 40, "");
                }

                // Getting count of 4 cycles
                if ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                {
                   // InvokeProgressEvent(GraphProgress.Analizing, timer, "Count cycles with 4 length");
                    timer += 100 * 150 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Result[AnalyseOptions.Cycles4] = analyzer.GetCycles4();
                   // InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                    InvokeProgressEvent(GraphProgress.Analizing, 50, "");
                }

                // Getting clusterring coefficient
                if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                {
                    //InvokeProgressEvent(GraphProgress.Analizing, timer, "Clustering Coefficient");
                    timer += 100 * 20 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Coefficient = analyzer.GetClusteringCoefficient();
                   // InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                    InvokeProgressEvent(GraphProgress.Analizing, 60, "");
                }

                // Getting cycle counts
                if ((AnalizeOptions & AnalyseOptions.Cycles) == AnalyseOptions.Cycles)
                {
                    int maxValue = Int32.Parse((String)AnalizeOptionsValues["cyclesHi"]);
                    int minvalue = Int32.Parse((String)AnalizeOptionsValues["cyclesLow"]);
                    InvokeProgressEvent(GraphProgress.Analizing, 70, "Cycles of " + minvalue + "-" + maxValue + "degree");
                    analyzer.Tree = tree;
                    Result.Cycles = analyzer.GetCycles(minvalue, maxValue);
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
            return processorcount*vertexcount < ramCounter.NextValue();
        }

        public override void Dispose()
        {
            generator = null;
            tree = null;
        }

        public override string GetParamsInfo()
        {
            return "";
        }

        public override bool[,] GetMatrix()
        {
            int vertexCount = (int)Math.Pow(generator.prime, generator.degree);
            bool[,] matrix = new bool[vertexCount, vertexCount];

            for (int i = 0; i < vertexCount; ++i)
            {
                for (int j = 0; j < vertexCount; ++j)
                    matrix[i, j] = (tree[i, j] == 1) ? true : false;
            }
            
            return matrix;

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
        }

        protected override void StaticGenerateModel()
        {
            throw new NotImplementedException();
        }
    }
}
