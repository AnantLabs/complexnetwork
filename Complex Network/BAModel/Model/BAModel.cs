using System;
using System.Collections.Generic;
using System.Collections;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.BAModel.Realization;
using log4net;

namespace Model.BAModel
{
    // Атрибуты модели (BA).
    [GraphModel("BAModel", GenerationRule.Sequential, "Barabasi-Albert Model")]
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
    [RequiredGenerationParam(GenerationParam.StepCount, 8)]

    // Реализация модели (BA).
    public class BAModel : AbstractGraphModel
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(BAModel));

        private static readonly string MODEL_NAME = "BAModel";
        
        public BAModel() { }

        public BAModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
            : base(genParam, options, sequenceNumber)
        {
            log.Info("Creating BAModel object with generation parameters.");
            InitModel();
        }

        public BAModel(ArrayList matrix, AnalyseOptions options, int sequenceNumber)
            : base(matrix, options, sequenceNumber)
        {
            log.Info("Creating BAModel object from matrix.");
            InitModel();
        }

        private void InitModel()
        {
            log.Info("Started model initialization.");
            InvokeProgressEvent(GraphProgress.Initializing, 0);
            ModelName = MODEL_NAME;

            // Проверить правильность
            GenerationRule = GenerationRule.Sequential;

            // Определение параметров генерации.
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Vertices);
            genParams.Add(GenerationParam.MaxEdges);
            genParams.Add(GenerationParam.StepCount);
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

            // Определение генератора и анализатора для данной модели (BA).
            log.Info("Creating generator and analyzer for model.");
            generator = new BAGenerator();
            analyzer = new BAAnalyzer((BAContainer)generator.Container);
          
            InvokeProgressEvent(GraphProgress.Ready);
            log.Info("Finished model initialization");
        }

        // Проверка параметров генерации.
        public override bool CheckGenerationParams(int instances)
        {
            int vertex = (Int32)GenerationParamValues[GenerationParam.Vertices];
            int edges = (Int16)GenerationParamValues[GenerationParam.MaxEdges];
            int assamblecount = (Int32)GenerationParamValues[GenerationParam.StepCount];
            if (vertex < edges || (vertex * 40 / 100) > assamblecount)
                return false;

            return true;
        }

        // Получение дополнительной информации о параметрах генерации.
        // Для данной модели (BA) число добавляемиых вершин не должно превышать число начальных вершин,
        // а число шагов должно выть больше, чем 40% от числа начальных вершин.
        public override string GetParamsInfo()
        {
            int edges = (Int16)GenerationParamValues[GenerationParam.MaxEdges];
            int vertex = (Int32)GenerationParamValues[GenerationParam.Vertices];
            int assamblecount = (Int32)GenerationParamValues[GenerationParam.StepCount];
            if (edges > vertex)
                return "Initial vertex count mast be greater then edges count";
            if ((vertex * 40 / 100) > assamblecount)
                return "Add vertex count must be greater then 40 percent of initial vertex count";
            return "";

        }

        public override void Dispose()
        {
            log.Info("Disposing...");
            generator = null;
            analyzer = null;
            base.Dispose();
        }
    }
}
