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

            container.BranchIndex = branchIndex;
            container.Level = level;
            Generate(mu);
            log.Info("Random generation step finished.");
        }

        // Строится граф, на основе матрицы смежности.
        public void StaticGeneration(ArrayList matrix)
        {
            log.Info("Static generation started.");
            //container.SetMatrix(matrix);
            log.Info("Static generation finished.");
        }

        // Закрытая часть класса (не из общего интерфейса).

        // Генератор случайного числа.
        private Random rnd = new Random();

        private const int ARRAY_MAX_SIZE = 2000000000;    

        // Генерирует граф с данными параметрами. Сгенеририванный граф находится в контейнере.
        private void Generate(Double m)
        {
            container.TreeMatrix = new BitArray[container.Level][];

            //for every level create datas, started with root
            for (int i = 0; i < container.Level; ++i)
            {
                int levelVertexCount = 0;
                if (i == 0)
                {
                    container.Branches[0] = new int[1];
                    container.Branches[0][0] = rnd.Next(container.BranchIndex + 1);
                    levelVertexCount = 1;
                }
                else
                {
                    for (int j = 0; j < container.Branches[i - 1].Length; ++j)
                    {
                        for (int k = 0; k < container.Branches[i - 1][j]; ++k)
                        {
                            ++levelVertexCount;
                        }
                    }
                    container.Branches[i] = new int[levelVertexCount];
                    for (int j = 0; j < levelVertexCount; ++j)
                    {
                        container.Branches[i][j] = rnd.Next(container.BranchIndex + 1);
                    }
                }

                long dataLength = 0;
                for (int j = 0; j < levelVertexCount; ++j)
                {
                    int nodeDataLength = container.Branches[i][j];
                    dataLength += nodeDataLength * (nodeDataLength - 1) / 2;
                }
                int arrCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataLength) / ARRAY_MAX_SIZE));
                
                container.TreeMatrix[i] = new BitArray[arrCount];
                int t;
                for (t = 0 ; t < arrCount - 1; ++t)
                {
                    container.TreeMatrix[i][t] = new BitArray(ARRAY_MAX_SIZE);
                }
                container.TreeMatrix[i][t] = new BitArray(Convert.ToInt32(dataLength - (arrCount - 1) * ARRAY_MAX_SIZE));

                //genereates data for current level nodes
                GenerateData(i, m);
            }
        }

        private void GenerateData(int level, double m)
        {
            //loop over all elements of given level and generate him values
            for (int i = 0; i < container.TreeMatrix[container.Level - level].Length; i++)
            {
                for (int j = 0; j < container.TreeMatrix[container.Level - level][i].Length; j++)
                {
                    double k = rnd.NextDouble();
                    if (k <= (1 / Math.Pow(container.BranchIndex, level * m)))
                    {
                        container.TreeMatrix[container.Level - level][i][j] = true;
                    }
                    else
                    {
                        container.TreeMatrix[container.Level - level][i][j] = false;
                    }
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
