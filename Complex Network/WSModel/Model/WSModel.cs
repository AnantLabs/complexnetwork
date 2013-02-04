using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.WSModel.Realization;
using log4net;

namespace Model.WSModel
{
    // Test commit
    // Атрибуты модели (WS).
    [GraphModel("WSModel", "Watts-Strogatz Model")]
    [AvailableAnalyzeOptions(
        AnalyseOptions.AveragePath |
        AnalyseOptions.Diameter |
        AnalyseOptions.Cycles3 |
        AnalyseOptions.Cycles4 |
        AnalyseOptions.EigenValue |
        AnalyseOptions.DegreeDistribution |
        AnalyseOptions.ClusteringCoefficient |
        AnalyseOptions.MinPathDist |
        AnalyseOptions.ConnSubGraph |
        AnalyseOptions.TriangleCountByVertex |
        AnalyseOptions.FullSubGraph)]
    [RequiredGenerationParam(GenerationParam.Vertices, 1)]
    [RequiredGenerationParam(GenerationParam.Edges, 2)]
    [RequiredGenerationParam(GenerationParam.P, 7)]
    [RequiredGenerationParam(GenerationParam.StepCount, 8)]

    // Реализация модели (WS).
    public class WSModel : AbstractGraphModel
    {
        // Организация Работы с лог файлом
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(WSModel));

        private static readonly string MODEL_NAME = "WSModel";

        public WSModel() { }

        public WSModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, Dictionary<String, Object> analizeOptionsValues)
            : base(genParam, options, analizeOptionsValues)
        {
            log.Info("Creating WSModel object with generation parameters.");
            InitModel();
        }

        public WSModel(ArrayList matrix, AnalyseOptions options, Dictionary<String, Object> analizeOptionsValues)
            : base(matrix, options, analizeOptionsValues)
        {
            log.Info("Creating WSModel object from matrix.");
            InitModel();
        }

        public override AbstractGraphModel CloneRandom()
        {
            AbstractGraphModel model = new WSModel(this.GenerationParamValues, 
                this.AnalyzeOptions, 
                this.AnalyzeOptionsValues);
            model.TracingPath = this.TracingPath;
            return model;
        }

        public override AbstractGraphModel CloneStatic()
        {
            AbstractGraphModel model = new WSModel(this.NeighbourshipMatrix,
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

            // Определение параметров генерации. !Добавить число шагов!
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Vertices);
            genParams.Add(GenerationParam.Edges);
            genParams.Add(GenerationParam.P);
            genParams.Add(GenerationParam.StepCount);
            RequiredGenerationParams = genParams;

            // Определение доступных опций для анализа (вычисляемые характеристики для данной модели (WS)).
            AvailableOptions = AnalyseOptions.AveragePath |
                AnalyseOptions.Diameter |
                AnalyseOptions.Cycles3 |
                AnalyseOptions.Cycles4 |
                AnalyseOptions.EigenValue |
                AnalyseOptions.DegreeDistribution |
                AnalyseOptions.ClusteringCoefficient |
                AnalyseOptions.MinPathDist |
                AnalyseOptions.ConnSubGraph |
                AnalyseOptions.TriangleCountByVertex |
                AnalyseOptions.FullSubGraph;

            // Определение генератора и анализатора для данной модели (WS).
            log.Info("Creating generator and analyzer for model.");
            generator = new WSGenerator();
            analyzer = new WSAnalyzer((WSContainer)generator.Container);

            InvokeProgressEvent(GraphProgress.Ready);
            log.Info("Finished model initialization");
        }

        // Проверка параметров генерации.
        public override bool CheckGenerationParams(int instances)
        {
            int e = (int)GenerationParamValues[GenerationParam.Edges];
            int v = (int)GenerationParamValues[GenerationParam.Vertices];
            if (e % 2 == 0 && e > Math.Log((double)v) && e < v)
                return true;
            return false;
        }

        // Получение дополнительной информации о параметрах генерации.
        // Для данной модели (WS) число ребер должен быть больше логаритма от числа вершин и меньше, чем (число вершин - 1).
        public override string GetParamsInfo()
        {
            int e = (int)GenerationParamValues[GenerationParam.Edges];
            int v = (int)GenerationParamValues[GenerationParam.Vertices];
            double a = Math.Log((double)v) + 1;
            return "Edges count mast be greater\n " + (int)a + " and less" + (v - 1);
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
