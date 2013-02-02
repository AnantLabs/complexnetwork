using System;
using System.Collections;
using System.Collections.Generic;

using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model;
using NumberGeneration;
using log4net;

namespace Model.NonRegularHierarchicModel.Realization
{
    // Реализация генератор (Block-Hierarchic Non Regular).
    public class NonRegularHierarchicGenerator : IGraphGenerator
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(NonRegularHierarchicGenerator));

        // Контейнер, в котором содержится граф конкретной модели (Block-Hierarchic Non Regular).
        private NonRegularHierarchicContainer container;

        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public NonRegularHierarchicGenerator()
        {
            container = new NonRegularHierarchicContainer();
        }

        // Контейнер, в котором содержится сгенерированный граф.
        public IGraphContainer Container
        {
            get { return container; }
            set { container = (NonRegularHierarchicContainer)value; }
        }

        // Случайным образом генерируется граф, на основе параметров генерации.
        public void RandomGeneration(Dictionary<GenerationParam, object> genParam)
        {
            log.Info("Random generation step started.");
            Int16 branchIndex = (Int16)genParam[GenerationParam.BranchIndex];
            Int16 level = (Int16)genParam[GenerationParam.Level];
            double mu = (Double)genParam[GenerationParam.Mu];
            ;
            Generate(branchIndex, level, mu);
            log.Info("Random generation step finished.");
        }

        // Строится граф, на основе матрицы смежности.
        public void StaticGeneration(ArrayList matrix)
        {
            log.Info("Static generation started.");
            container.SetMatrix(matrix);
            log.Info("Static generation finished.");
        }

        // Закрытая часть класса (не из общего интерфейса).

        // Генератор случайного числа.
        private Random rnd = new Random();

        // Генерирует граф с данными параметрами. Сгенеририванный граф находится в контейнере.
        private void Generate(Int16 branchIndex, Int16 level, Double mu)
        {
            // If this is to be a leave, just return.
            if (0 == level)
            {
                container.Post_generate();
                return;
            }

            container.node.children = new NonRegularHierarchicContainer[rnd.Next(2, branchIndex + 1)];
            int i;
            for (i = 0; i < container.node.children.Length; ++i)
            {
                container.node.children[i] = new NonRegularHierarchicContainer();

                /// generate further tree of graph.
                NonRegularHierarchicGenerator sub_generator = new NonRegularHierarchicGenerator();
                sub_generator.Generate(branchIndex, (Int16)(level - 1), mu);
                container.node.children[i] = sub_generator.container;
            }

            int length = (container.node.children.Length - 1) * container.node.children.Length / 2;
            container.node.data = new BitArray(length, false);
            for (i = 0; i < length; ++i)
            {
                double k = rnd.NextDouble();
                if (k <= (Math.Pow(branchIndex, -mu * level)))
                {
                    container.node.data[i] = true;
                }
                else
                {
                    container.node.data[i] = false;
                }
            }

            container.Post_generate();
        }

        #region IGraphGenerator Members


        public void PermanentGeneration(Dictionary<GenerationParam, object> genParam)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
