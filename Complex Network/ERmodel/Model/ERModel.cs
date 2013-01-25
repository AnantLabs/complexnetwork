using System;
using System.Collections.Generic;
using System.Collections;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.ERModel.Realization;
using log4net;

namespace Model.ERModel
{
    // Атрибуты модели (ER).
    [GraphModel("ERModel", "Erdos-Renyi Model")]
    [AvailableAnalyzeOptions(
         AnalyseOptions.AveragePath |
         AnalyseOptions.Diameter |
         AnalyseOptions.Cycles3 |
         AnalyseOptions.Cycles4 |
         AnalyseOptions.EigenValue |
         AnalyseOptions.DistEigenPath |
         AnalyseOptions.DegreeDistribution |
         AnalyseOptions.ClusteringCoefficient |
         AnalyseOptions.MinPathDist |
         AnalyseOptions.Cycles | 
         AnalyseOptions.Motifs |
         AnalyseOptions.TriangleTrajectory)]
    [RequiredGenerationParam(GenerationParam.Vertices, 2)]
    [RequiredGenerationParam(GenerationParam.P, 3)]
    [RequiredGenerationParam(GenerationParam.InitialStep, 4)]

    // Реализация модели (ER).
    public class ERModel : AbstractGraphModel
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERModel));

        private static readonly string MODEL_NAME = "ERModel";

        public ERModel() { }

        public ERModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, Dictionary<String, Object> analizeOptionsValues)
            : base(genParam, options, analizeOptionsValues)
        {
            log.Info("Creating ERModel object with generation parameters.");
            InitModel();
        }

        public ERModel(ArrayList matrix, AnalyseOptions options, Dictionary<String, Object> analizeOptionsValues)
            : base(matrix, options, analizeOptionsValues)
        {
            log.Info("Creating ERModel object from matrix.");
            InitModel();
        }

        public override AbstractGraphModel Clone()
        {
            AbstractGraphModel model = new ERModel(this.GenerationParamValues,
                this.AnalyzeOptions,
                this.AnalyzeOptionsValues);
            model.TracingPath = this.TracingPath;
            return model;
        }

        private void InitModel()
        {
            log.Info("Started model initialization.");
            InvokeProgressEvent(GraphProgress.Initializing, 0);
            ModelName = MODEL_NAME;

            // Определение параметров генерации.
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Vertices);
            genParams.Add(GenerationParam.P);
            RequiredGenerationParams = genParams;

            // Определение доступных опций для анализа (вычисляемые характеристики для данной модели (ER)).
            AvailableOptions = AnalyseOptions.AveragePath |
                AnalyseOptions.Diameter |
                AnalyseOptions.Cycles |
                AnalyseOptions.Cycles3 |
                AnalyseOptions.Cycles4 |
                AnalyseOptions.EigenValue |
                AnalyseOptions.DistEigenPath |
                AnalyseOptions.DegreeDistribution |
                AnalyseOptions.ClusteringCoefficient |
                AnalyseOptions.MinPathDist |
                AnalyseOptions.TriangleTrajectory;

            // Определение генератора и анализатора для данной модели (ER).
            log.Info("Creating generator and analyzer for model.");
            generator = new ERGenerator();
            analyzer = new ERAnalyzer((ERContainer)generator.Container);

            InvokeProgressEvent(GraphProgress.Ready);
            log.Info("Finished model initialization");
        }

        // Проверка параметров генерации.
        public override bool CheckGenerationParams(int instances)
        {
            System.Diagnostics.PerformanceCounter ramCounter = new System.Diagnostics.PerformanceCounter("Memory", "Available Bytes");
            UInt64 vertex = UInt64.Parse(GenerationParamValues[GenerationParam.Vertices].ToString());
            UInt64 vertexmemory = vertex * (vertex - 1) / 16;
            int processorcount = Environment.ProcessorCount;
            return vertexmemory < ramCounter.NextValue() / processorcount
                   && (int)GenerationParamValues[GenerationParam.Vertices] < 32000;
        }

        // Получение дополнительной информации о параметрах генерации.
        // Для данной модели (ER) таковых нет.
        public override string GetParamsInfo()
        {
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
