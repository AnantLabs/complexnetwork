using System;
using System.Collections.Generic;
using System.Collections;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.ParisiHierarchicModel.Realization;
using Model.HierarchicModel.Realization;
using log4net;

namespace Model.ParisiHierarchicModel
{
    // Атрибуты модели (Block-Hierarchic Parisi).
    [GraphModel("Block-Hierarchic Parisi", "Block-hierarchic Parisi Model")]
    [AvailableAnalyzeOptions(
        AnalyseOptions.AveragePath |
        AnalyseOptions.Cycles3 |
        AnalyseOptions.Cycles4 |
        AnalyseOptions.CycleEigen3 |
        AnalyseOptions.CycleEigen4 |
        AnalyseOptions.EigenValue |
        AnalyseOptions.DegreeDistribution |
        AnalyseOptions.ClusteringCoefficient |
        AnalyseOptions.ConnSubGraph |
        AnalyseOptions.Cycles)]
    [RequiredGenerationParam(GenerationParam.BranchIndex, 3)]
    [RequiredGenerationParam(GenerationParam.Level, 4)]
    [RequiredGenerationParam(GenerationParam.Mu, 6)]

    // Реализация модели (Block-Hierarchic Parisi).
    public class ParisiHierarchicModel : AbstractGraphModel
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ParisiHierarchicModel));

        private static readonly string MODEL_NAME = "Block-Hierarchic Parisi";

        public ParisiHierarchicModel() { }

        public ParisiHierarchicModel(Dictionary<GenerationParam, object> genParam, 
            AnalyseOptions options, Dictionary<AnalyzeOptionParam, Object> analizeOptionsValues)
            : base(genParam, options, analizeOptionsValues)
        {
            log.Info("Creating Block-Hierarchic Parisi model object from matrix.");
            InitModel();
        }

        public override int GetNetworkSize()
        {
            return (int)Math.Pow((Int16)this.GenerationParamValues[GenerationParam.BranchIndex],
                (Int16)this.GenerationParamValues[GenerationParam.Level]);
        }

        public override AbstractGraphModel Clone()
        {
            AbstractGraphModel model = new ParisiHierarchicModel(this.GenerationParamValues,
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
            genParams.Add(GenerationParam.BranchIndex);
            genParams.Add(GenerationParam.Level);
            genParams.Add(GenerationParam.Mu);
            RequiredGenerationParams = genParams;

            //Defines available options for analizer
            AvailableOptions = AnalyseOptions.AveragePath |
                AnalyseOptions.Cycles3 |
                AnalyseOptions.Cycles4 |
                AnalyseOptions.CycleEigen3 |
                AnalyseOptions.CycleEigen4 |
                AnalyseOptions.EigenValue |
                AnalyseOptions.DegreeDistribution |
                AnalyseOptions.ClusteringCoefficient |
                AnalyseOptions.ConnSubGraph |
                AnalyseOptions.Cycles;

            // Определение генератора и анализатора для данной модели (Block-Hierarchic Parisi).
            log.Info("Creating generator and analyzer for model.");
            generator = new ParisiHierarchicGenerator();
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
            return processorcount * vertexcount < ramCounter.NextValue();
        }

        // Получение дополнительной информации о параметрах генерации.
        // Для данной модели (Block-Hierarchic Parisi) таковых нет.
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
