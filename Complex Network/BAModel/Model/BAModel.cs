using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using RandomGraph.Common.Model.Settings;
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
