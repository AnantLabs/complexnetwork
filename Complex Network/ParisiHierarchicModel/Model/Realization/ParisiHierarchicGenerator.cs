using System;
using System.Collections;
using System.Collections.Generic;

using Model.HierarchicModel.Realization;
using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model;
using NumberGeneration;
using log4net;

namespace Model.ParisiHierarchicModel.Realization
{
    // Реализация генератор (Block-Hierarchic Parisi).
    class ParisiHierarchicGenerator : IGraphGenerator
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ParisiHierarchicGenerator));

        // Контейнер, в котором содержится граф конкретной модели (Block-Hierarchic Parisi).
        private HierarchicContainer container;

        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public ParisiHierarchicGenerator()
        {
            container = new HierarchicContainer();
        }

        // Контейнер, в котором содержится сгенерированный граф.
        public IGraphContainer Container
        {
            get { return container; }
            set { container = (HierarchicContainer)value; }
        }

        // Случайным образом генерируется граф, на основе параметров генерации.
        public void RandomGeneration(Dictionary<GenerationParam, object> genParam)
        {
            log.Info("Random generation step started.");
            Int16 branchIndex = (Int16)genParam[GenerationParam.BranchIndex];
            Int16 level = (Int16)genParam[GenerationParam.Level];
            double mu = (Double)genParam[GenerationParam.Mu];

            container.BranchIndex = branchIndex;
            container.Level = level;
            container.TreeMatrix = GenerateTree(branchIndex, level, mu);
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
        private RNGCrypto rand = new RNGCrypto();
        private const int ARRAY_MAX_SIZE = 2000000000;

        /// <summary>
        /// Создает дерево (рекурсивно).
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private BitArray[][] GenerateTree(int b, int d, double m)
        {
            BitArray[][] treeMatrix = new BitArray[d][];

            //for every level create datas, started with root
            for (int i = d; i > 0; i--)
            {
                //get current level data length and bitArrays count
                int nodeDataLength = (b - 1) * b / 2;
                long dataLength = Convert.ToInt64(Math.Pow(b, d - i) * nodeDataLength);
                int arrCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataLength) / ARRAY_MAX_SIZE));

                treeMatrix[d - i] = new BitArray[arrCount];
                int j;
                for (j = 0; j < arrCount - 1; j++)
                {
                    treeMatrix[d - i][j] = new BitArray(ARRAY_MAX_SIZE);
                }
                treeMatrix[d - i][j] = new BitArray(Convert.ToInt32(dataLength - (arrCount - 1) * ARRAY_MAX_SIZE));
                
                //genereates data for current level nodes
                GenerateData(treeMatrix, i, b, d, m);
            }

            return treeMatrix;
        }

        /// <summary>
        /// Generates a data of tree level 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private void GenerateData(BitArray[][] treeMatrix, int level, int b, int d, double m)
        {
            //loop over all elements of given level and generate him values
            for (int i = 0; i < treeMatrix[d - level].Length; i++)
            {
                double k = rand.NextDouble();
                Boolean val;
                if (k <= (1 / Math.Pow(b, level * m)))
                {
                    val = true;
                }
                else
                {
                    val = false;
                }
                for (int j = 0; j < treeMatrix[d - level][i].Length; j++)
                {
                    treeMatrix[d - level][i][j] = val;
                }
            }
        }

        #region IGraphGenerator Members


        public void PermanentGeneration(Dictionary<GenerationParam, object> genParam)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
