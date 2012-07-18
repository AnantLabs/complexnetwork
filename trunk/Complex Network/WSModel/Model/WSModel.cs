using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using Model.WSModel.Realization;
using Algorithms;

namespace Model.WSModel
{
    // Атрибуты модели (WS).
    [GraphModel("Watts-Strogatz", GenerationRule.Sequential, "Watts-Strogatz graph model")]
    [AvailableAnalyzeOptions(
        AnalyseOptions.AveragePath |
        AnalyseOptions.Diameter |
        AnalyseOptions.Cycles3 |
        AnalyseOptions.Cycles4 |
        AnalyseOptions.EigenValue | 
        AnalyseOptions.DegreeDistribution |        
        AnalyseOptions.ClusteringCoefficient |        
        AnalyseOptions.ConnSubGraph)]
    [RequiredGenerationParam(GenerationParam.P, 10)]
    [RequiredGenerationParam(GenerationParam.Vertices, 2)]
    [RequiredGenerationParam(GenerationParam.Edges, 3)]

    // Реализация модели (WS).
    public class WSModel : AbstractGraphModel
    {
        // !Организация Работы с лог файлом!

        private static readonly string MODEL_NAME = "Watts-Strogatz";

        public WSModel() { }

        public WSModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
            : base(genParam, options, sequenceNumber)
        {
            ValidateModelParams();
            InitModel();
        }

        public WSModel(ArrayList matrix, AnalyseOptions options, int sequenceNumber)
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

            // Определение параметров генерации. !Добавить число шагов!
            List<GenerationParam> genParams = new List<GenerationParam>();
            genParams.Add(GenerationParam.Edges);
            genParams.Add(GenerationParam.Vertices);
            genParams.Add(GenerationParam.P);
            RequiredGenerationParams = genParams;

            // Определение доступных опций для анализа (вычисляемые характеристики для данной модели (WS)).
            AvailableOptions = AnalyseOptions.AveragePath |
                AnalyseOptions.Diameter |
                AnalyseOptions.Cycles3 |
                AnalyseOptions.Cycles4 |
                AnalyseOptions.EigenValue |
                AnalyseOptions.DegreeDistribution |
                AnalyseOptions.ClusteringCoefficient |
                AnalyseOptions.ConnSubGraph;

            // Определение генератора и анализатора для данной модели (WS).
            generator = new WSGenerator();
            analyzer = new WSAnalyzer((WSContainer)generator.Container);

            InvokeProgressEvent(GraphProgress.Ready);
        }

        public override bool CheckGenerationParams(int instances)
        {
            int e = (int)GenerationParamValues[GenerationParam.Edges];
            int v = (int)GenerationParamValues[GenerationParam.Vertices];
            if (e % 2 == 0 && e > Math.Log((double)v) && e < v)
                return true;
            return false;
        }

        public override string GetParamsInfo()
        {
            int e = (int)GenerationParamValues[GenerationParam.Edges];
            int v = (int)GenerationParamValues[GenerationParam.Vertices];
            double a = Math.Log((double)v) + 1;
            return "Edges count mast be greater\n " + (int)a + " and less" + (v - 1);

        }

        public override void Dispose()
        {
            //log.Info("disposing...");
            generator = null;
            analyzer = null;
            base.Dispose();
        }

        public override bool[,] GetMatrix()
        {
            return analyzer.Container.GetMatrix();
        }
    }
}
