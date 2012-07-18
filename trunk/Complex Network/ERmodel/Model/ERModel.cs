using System;
using System.Collections.Generic;
using System.Collections;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.ERModel.Realization;
using System.Threading;
using Algorithms;
using log4net;

namespace Model.ERModel
{
    // Атрибуты модели (ER).
    [GraphModel("ERModel", GenerationRule.Sequential, "ERModel graph model")]
    [AvailableAnalyzeOptions(
         AnalyseOptions.AveragePath |
         AnalyseOptions.Diameter |
         AnalyseOptions.Cycles3 |
         AnalyseOptions.Cycles4 |
         AnalyseOptions.EigenValue |
         AnalyseOptions.DistEigenPath |
         AnalyseOptions.DegreeDistribution |
         AnalyseOptions.ClusteringCoefficient |
         AnalyseOptions.MinPathDist)]
    [RequiredGenerationParam(GenerationParam.Vertices, 2)]
    [RequiredGenerationParam(GenerationParam.P, 3)]

    // Реализация модели (ER).
    public class ERModel : AbstractGraphModel
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERModel));

        private static readonly string MODEL_NAME = "ERModel";

        public ERModel() { }

        public ERModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
            : base(genParam, options, sequenceNumber)
        {
            log.Info("Creating ERModel object");
            ValidateModelParams();
            InitModel();
        }

        public ERModel(ArrayList matrix, AnalyseOptions options, int sequenceNumber)
            :base(matrix, options, sequenceNumber)
        {
            log.Info("Creating ERModel object");
            ValidateModelParams();
            InitModel();
        }

        private void ValidateModelParams()
        {
            // !Добавить проверку параметров!
        }

        private void InitModel()
        {
            log.Info("Started model initialization");
            InvokeProgressEvent(GraphProgress.Initializing, 0);
            ModelName = MODEL_NAME;

            // Проверить правильность
            GenerationRule = GenerationRule.Sequential;

            // Определение параметров генерации. !Добавить число шагов!
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Vertices);
            genParams.Add(GenerationParam.P);
            RequiredGenerationParams = genParams;

            // Определение доступных опций для анализа (вычисляемые характеристики для данной модели (ER)).
            AvailableOptions = AnalyseOptions.AveragePath |
                AnalyseOptions.Diameter |
                AnalyseOptions.Cycles3 |
                AnalyseOptions.Cycles4 |
                AnalyseOptions.EigenValue |
                AnalyseOptions.DistEigenPath |
                AnalyseOptions.DegreeDistribution |
                AnalyseOptions.ClusteringCoefficient |
                AnalyseOptions.MinPathDist;   
         
            // Определение генератора и анализатора для данной модели (ER).
            generator = new ERGenerator();
            analyzer = new ERAnalyzer((ERContainer)generator.Container);
          
            InvokeProgressEvent(GraphProgress.Ready);
            log.Info("Finished model initialization");
        }
        
        public override bool CheckGenerationParams(int instances)
        {
            System.Diagnostics.PerformanceCounter ramCounter = new System.Diagnostics.PerformanceCounter("Memory", "Available Bytes");
            UInt64 vertex = UInt64.Parse(GenerationParamValues[GenerationParam.Vertices].ToString());
            UInt64 vertexmemory = vertex * (vertex - 1) / 16;
            int processorcount = Environment.ProcessorCount;
            return vertexmemory < ramCounter.NextValue() / processorcount
                   && (int)GenerationParamValues[GenerationParam.Vertices] < 32000;
        }

        public override string GetParamsInfo()
        {
            return "";
        }

        public override bool[,] GetMatrix()
        {
            return analyzer.Container.GetMatrix();
        }

        public override void Dispose()
        {
            log.Info("disposing...");
            generator = null;
            analyzer = null;
            base.Dispose();
        }
    }
}
