using System;
using System.Collections.Generic;
using System.Collections;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.NonRegularHierarchicModel.Realization;
using log4net;

namespace Model.NonRegularHierarchicModel
{
    // Атрибуты модели (Block-Hierarchic Non Regular).
    [GraphModel("Block-Hierarchic Non Regular", "Block-Hierarchic Non Regular Model")]
    [AvailableAnalyzeOptions(
        AnalyseOptions.AveragePath |
        AnalyseOptions.Diameter |
        AnalyseOptions.Cycles3 |
        AnalyseOptions.Cycles4 |
        AnalyseOptions.DegreeDistribution |
        AnalyseOptions.ClusteringCoefficient |
        AnalyseOptions.DistEigenPath)]
    [RequiredGenerationParam(GenerationParam.BranchIndex, 3)]
    [RequiredGenerationParam(GenerationParam.Level, 4)]
    [RequiredGenerationParam(GenerationParam.Mu, 6)]

    // Реализация модели (Block-Hierarchic Non Regular).
    public class NonRegularHierarchicModel : AbstractGraphModel
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(NonRegularHierarchicModel));

        private static readonly string MODEL_NAME = "Non-Regular Hierarchic";

        public NonRegularHierarchicModel() { }

        public NonRegularHierarchicModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, Dictionary<String, Object> analizeOptionsValues)
            : base(genParam, options, analizeOptionsValues)
        {
            log.Info("Creating Block-Hierarchic Non Regular model object from matrix.");
            InitModel();
        }

        public NonRegularHierarchicModel(ArrayList matrix, AnalyseOptions options, Dictionary<String, Object> analizeOptionsValues)
            : base(matrix, options, analizeOptionsValues)
        {
            log.Info("Creating Block-Hierarchic Non Regular model object from matrix.");
            InitModel();
        }
        
        public override AbstractGraphModel CloneRandom()
        {
            AbstractGraphModel model = new NonRegularHierarchicModel(this.GenerationParamValues,
                this.AnalyzeOptions,
                this.AnalyzeOptionsValues);
            model.TracingPath = this.TracingPath;
            return model;    
        }

        public override AbstractGraphModel CloneStatic()
        {
            AbstractGraphModel model = new NonRegularHierarchicModel(this.NeighbourshipMatrix,
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

            // Определение доступных опций для анализа 
            // (вычисляемые характеристики для данной модели (Block-Hierarchic Non Regular)).
            AvailableOptions = AnalyseOptions.AveragePath |
                AnalyseOptions.Diameter |
                AnalyseOptions.Cycles3 |
                AnalyseOptions.Cycles4 |
                AnalyseOptions.DegreeDistribution |
                AnalyseOptions.ClusteringCoefficient |
                AnalyseOptions.DistEigenPath;

            // Определение генератора и анализатора для данной модели (Block-Hierarchic Non Regular).
            log.Info("Creating generator and analyzer for model.");
            generator = new NonRegularHierarchicGenerator();
            analyzer = new NonRegularHierarchicAnalyzer((NonRegularHierarchicContainer)generator.Container);

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
        // Для данной модели (Block-Hierarchic Non Regular) таковых нет.
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