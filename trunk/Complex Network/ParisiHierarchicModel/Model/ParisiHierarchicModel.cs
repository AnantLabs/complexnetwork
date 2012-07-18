using System;
using System.Collections.Generic;
using System.Text;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using System.Threading;
using Model.HierarchicModel.Realization;

namespace Model.ParisiHierarchicModel
{
    [GraphModel("Block-Hierarchic Parizi", GenerationRule.Separate, "Block-hierarchic Parizi graph model")]
    [AvailableAnalyzeOptions(
        AnalyseOptions.DegreeDistribution |
        AnalyseOptions.EigenValue |
        AnalyseOptions.AveragePath |
        AnalyseOptions.Cycles3 |
        AnalyseOptions.ClusteringCoefficient |
        AnalyseOptions.FullSubGraph |
        AnalyseOptions.MinPathDist |
        AnalyseOptions.DistEigenPath |
        AnalyseOptions.EigenValue |
        AnalyseOptions.CycleEigen3 |
        AnalyseOptions.Cycles4 |
        AnalyseOptions.CycleEigen4)]
    [RequiredGenerationParam(GenerationParam.BranchIndex, 1)]
    [RequiredGenerationParam(GenerationParam.Level, 2)]
    [RequiredGenerationParam(GenerationParam.Mu, 3)]

    public class ParisiHierarchicModel : AbstractGraphModel
    {
        private static readonly string MODEL_NAME = "Hierarchic Parizi";
        private HierarchicGraph tree;
        private HierarchicGraphGenerator generator;

        public ParisiHierarchicModel() { }

        public ParisiHierarchicModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
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
                                            AnalyseOptions.CycleEigen3 |
                                            AnalyseOptions.Cycles4 |
                                            AnalyseOptions.CycleEigen4;



            //Defines required input parameters for generation
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Level);
            genParams.Add(GenerationParam.BranchIndex);
            RequiredGenerationParams = genParams;

            //Place additional initialization code here

            InvokeProgressEvent(GraphProgress.Ready);
        }

        /*protected override void GenerateModel()
        {
            InvokeProgressEvent(GraphProgress.StartingGeneration, 5);
            try
            {
                //Place generation initialization code here

                generator = new HierarchicGeneratorPerLevel((Int16)GenerationParamValues[GenerationParam.BranchIndex],
                                                        (Int16)GenerationParamValues[GenerationParam.Level],
                                                        (Double)GenerationParamValues[GenerationParam.Mu]);

                this.tree = new HierarchicGraph((Int16)GenerationParamValues[GenerationParam.BranchIndex],
                                                    (Int16)GenerationParamValues[GenerationParam.Level], generator.treeMatrix);

                InvokeProgressEvent(GraphProgress.Generating, 10);

                //Place generating logic here
                //Invoke ModelProgress event if possible to show current
                //state with use of Percent and TargetItem properties

                //Graph assignment is not needed for HEIRARCHIC(NOT NEEDED IF GENERATION 
                //RULE IS SEPARATE) 
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
            Result.graphSize = tree.getGraphSize();
            try
            {
                ////Place generation initialization code here
                HierarchicAnalyzer analyzer = new HierarchicAnalyzer(tree);
                int tasksTimes = 8;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient) ? 20 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.CycleEigen3) ? 30 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4) ? 150 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3) ? 55 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath) ? 30 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution) ? 20 : 0;
                tasksTimes += ((AnalizeOptions & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph) ? 5 : 0;
                int timer = 8;

                // Getting degree distribution
                if ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Degree distrubution");
                    timer += 100 * 20 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.VertexDegree = analyzer.GetDegreeDistribution();
                    Result.Result[AnalyseOptions.DegreeDistribution] = 1;
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }

                // Getting average path
                if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Average path distrubution");
                    timer += 100 * 30 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Result[AnalyseOptions.AveragePath] = analyzer.GetAveragePath();
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }

                // Getting coonected subgraphs
                if ((AnalizeOptions & AnalyseOptions.ConnSubGraph) == AnalyseOptions.ConnSubGraph)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Connected Subgraph");
                    timer += 100 * 5 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Subgraphs = analyzer.GetConnSubGraph();
                    Result.Result[AnalyseOptions.ConnSubGraph] = 1;
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }

                // Getting cycles 3
                if ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Cycles count");
                    timer += 100 * 55 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Result[AnalyseOptions.Cycles3] = analyzer.GetCycles3();
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }

                // Getting cycles 4
                if ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Count cycles with 4 length");
                    timer += 100 * 150 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Result[AnalyseOptions.Cycles4] = analyzer.GetCycles4();
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }

                // correct the using with interface
                if ((AnalizeOptions & AnalyseOptions.CycleEigen3) == AnalyseOptions.CycleEigen3)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "3 Cycle Count By Eigen Value");
                    timer += 100 * 30 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Result[AnalyseOptions.CycleEigen3] = analyzer.CalcCyclesCount(this.tree, 3);
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }

                // correct the using with interface
                if ((AnalizeOptions & AnalyseOptions.CycleEigen4) == AnalyseOptions.CycleEigen4)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "4 Cycle Count By Eigen Value");
                    timer += 100 * 30 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Result[AnalyseOptions.CycleEigen4] = analyzer.CalcCyclesCount(this.tree, 4);
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }

                // Getting clustering coefficient
                if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "Clustering Coefficient");
                    timer += 100 * 20 / tasksTimes;
                    analyzer.Tree = tree;
                    Result.Coefficient = analyzer.GetClusteringCoefficient();
                    InvokeProgressEvent(GraphProgress.Analizing, timer, "");
                }
                //Place analizing logic here
                //Invoke ModelProgress event if possible to show current
                //state with use of Percent and TargetItem properties

                InvokeProgressEvent(GraphProgress.AnalizingDone, 95);
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
        }*/

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
            generator = null;
            tree = null;
        }

        public override string GetParamsInfo()
        {
            return "";
        }

        public override bool[,] GetMatrix()
        {
            Random random = new Random();
            bool[,] matrix = new bool[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = random.NextDouble() > 0.5;
                }
            }
            return matrix;
        }
    }
}
