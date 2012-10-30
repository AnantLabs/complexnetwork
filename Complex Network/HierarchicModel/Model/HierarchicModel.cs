using System;
using System.Collections.Generic;
using System.Collections;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.HierarchicModel.Realization;
using log4net;

namespace Model.HierarchicModel
{
    // Атрибуты модели (Block-Hierarchic).
    [GraphModel("Block-Hierarchic","Block-hierarchic Model")]
    [AvailableAnalyzeOptions(
        AnalyseOptions.AveragePath | 
        AnalyseOptions.Cycles3 | 
        AnalyseOptions.Cycles4 | 
        AnalyseOptions.DegreeDistribution |
        AnalyseOptions.ClusteringCoefficient |
        AnalyseOptions.ConnSubGraph |
        AnalyseOptions.Cycles |
        AnalyseOptions.EigenValue |
        AnalyseOptions.DistEigenPath)]
    [RequiredGenerationParam(GenerationParam.BranchIndex, 3)]
    [RequiredGenerationParam(GenerationParam.Level, 4)]
    [RequiredGenerationParam(GenerationParam.Mu, 6)]

    // Реализация модели (Block-Hierarchic).
    public class HierarchicModel : AbstractGraphModel
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(HierarchicModel));

        private static readonly string MODEL_NAME = "Block-Hierarchic";

        public HierarchicModel() { }

        public HierarchicModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, Dictionary<String, Object> analizeOptionsValues)
            : base(genParam, options, analizeOptionsValues)
        {
            log.Info("Creating Block-Hierarchic model object with generation parameters.");
            InitModel();
        }

        public HierarchicModel(ArrayList matrix, AnalyseOptions options, Dictionary<String, Object> analizeOptionsValues)
            : base(matrix, options, analizeOptionsValues)
        {
            log.Info("Creating Block-Hierarchic object from matrix.");
            InitModel();
        }

        public override AbstractGraphModel Clone()
        {
            AbstractGraphModel model = new HierarchicModel(this.GenerationParamValues,
                this.AnalyzeOptions,
                this.AnalyzeOptionsValues);
            model.TracingPath = this.TracingPath;
            return model;
        }

        private void ValidateModelParams()
        {
            log.Info("Creating Block-Hierarchic model object from matrix.");
            InitModel();
        }

        private void InitModel()
        {
            log.Info("Started model initialization.");
            InvokeProgressEvent(GraphProgress.Initializing, 0);
            ModelName = MODEL_NAME;

            // Определение параметров генерации.
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.BranchIndex);
            genParams.Add(GenerationParam.Level);
            genParams.Add(GenerationParam.Mu);
            RequiredGenerationParams = genParams;

            // Определение доступных опций для анализа (вычисляемые характеристики для данной модели (Block-Hierarchic)).
            AvailableOptions = AnalyseOptions.AveragePath |
                AnalyseOptions.Cycles3 |
                AnalyseOptions.Cycles4 |
                AnalyseOptions.DegreeDistribution |
                AnalyseOptions.ClusteringCoefficient |
                AnalyseOptions.ConnSubGraph |
                AnalyseOptions.Cycles |
                AnalyseOptions.EigenValue |
                AnalyseOptions.DistEigenPath;

            // Определение генератора и анализатора для данной модели (Block-Hierarchic).
            log.Info("Creating generator and analyzer for model.");
            generator = new HierarchicGenerator();
            analyzer = new HierarchicAnalyzer((HierarchicContainer)generator.Container);

            InvokeProgressEvent(GraphProgress.Ready);
            log.Info("Finished model initialization");
        }

        // Проверка параметров генерации.
        public override bool CheckGenerationParams(int instances)
        {
            System.Diagnostics.PerformanceCounter ramCounter = new System.Diagnostics.PerformanceCounter("Memory", 
                "Available Bytes");
            int branch = (Int16)GenerationParamValues[GenerationParam.BranchIndex];
            int level = (Int16)GenerationParamValues[GenerationParam.Level];
            UInt32 vertexcount = (UInt32)(System.Math.Pow(branch, level));
            int processorcount = Environment.ProcessorCount;
            return processorcount*vertexcount < ramCounter.NextValue();
        }

        // Получение дополнительной информации о параметрах генерации.
        // Для данной модели (Block-Hierarchic) таковых нет.
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
