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
using GenericAlgorithms;

namespace Model.BAModel
{
    // Атрибуты модели (BA).
    [GraphModel("Barabasi-Albert", GenerationRule.Sequential, "Barabasi-Albert graph model")]
    [AvailableAnalyzeOptions(
        AnalyseOptions.AveragePath |
        AnalyseOptions.Diameter |
        AnalyseOptions.Cycles3 |
        AnalyseOptions.Cycles4 |
        AnalyseOptions.EigenValue |
        AnalyseOptions.DegreeDistribution |
        AnalyseOptions.ClusteringCoefficient |
        AnalyseOptions.MinPathDist |
        AnalyseOptions.Cycles |
        AnalyseOptions.Motifs)]
    [RequiredGenerationParam(GenerationParam.Vertices, 1)]
    [RequiredGenerationParam(GenerationParam.MaxEdges, 5)]
    [RequiredGenerationParam(GenerationParam.AddVertices, 9)]

    // Реализация модели (BA).
    public class BAModel : AbstractGraphModel
    {
        // Организация Работы с лог файлом.

        private static readonly string MODEL_NAME = "BAModel";
        
        public BAModel() { }

        public BAModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
            : base(genParam, options, sequenceNumber)
        {
            ValidateModelParams();
            InitModel();
        }

        public BAModel(ArrayList matrix, AnalyseOptions options, int sequenceNumber)
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

            // Определение параметров генерации.
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Vertices);
            genParams.Add(GenerationParam.MaxEdges);
            genParams.Add(GenerationParam.AddVertices);
            RequiredGenerationParams = genParams;

            // Определение доступных опций для анализа (вычисляемые характеристики для данной модели (BA)).
            AvailableOptions = AnalyseOptions.AveragePath |
                AnalyseOptions.Diameter |
                AnalyseOptions.Cycles3 |
                AnalyseOptions.Cycles4 |
                AnalyseOptions.EigenValue |
                AnalyseOptions.DegreeDistribution |
                AnalyseOptions.ClusteringCoefficient |
                AnalyseOptions.MinPathDist |
                AnalyseOptions.Cycles |
                AnalyseOptions.Motifs;

            // Определение генератора и анализатора для данной модели (ER).
            generator = new BAGenerator();
            analyzer = new BAAnalyzer((BAContainer)generator.Container);
          
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

                if ((AnalizeOptions & AnalyseOptions.Cycles) == AnalyseOptions.Cycles)
                {
                    int maxValue = Int32.Parse((String)AnalizeOptionsValues["cyclesHi"]);
                    int minvalue = Int32.Parse((String)AnalizeOptionsValues["cyclesLow"]);

                    InvokeProgressEvent(GraphProgress.Analizing, 85, "Cycles of " + minvalue + "-" + maxValue + "degree");
                    //Result.Cycles = BAModelGraph.m_analyzer.getNCyclesCount(minvalue, maxValue); !Исправить!
                    //calculate cycles here
                    //Result.CyclesCount = 
                }

                if ((AnalizeOptions & AnalyseOptions.Motifs) == AnalyseOptions.Motifs)
                {
                    int maxValue = Int32.Parse((String)AnalizeOptionsValues["motiveHi"]);
                    int minvalue = Int32.Parse((String)AnalizeOptionsValues["motiveLow"]);
                    InvokeProgressEvent(GraphProgress.Analizing, 90, "Motiv of " + minvalue + "-" + maxValue + "degree");
                    Result.MotivesCount = analyzer.GetMotivs(minvalue, maxValue);
                }

                Result.graphSize = analyzer.Container.Size;

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
            return analyzer.Container.GetMatrix();
        }

        public override void Dispose()
        {
            //log.Info("disposing...");
            generator = null;
            analyzer = null;
            base.Dispose();
        }
    }
}
