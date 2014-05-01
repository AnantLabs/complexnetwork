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
    public class NonRegularHierarchicGenerator : AbstractGraphGenerator
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
        public override AbstractGraphContainer Container
        {
            get { return container; }
            set { container = (NonRegularHierarchicContainer)value; }
        }

        // Permanet Generation
        public override void PermanentGeneration(Dictionary<GenerationParam, object> genParam)
        {
            throw new NotImplementedException();
        }

        // Случайным образом генерируется граф, на основе параметров генерации.
        protected override void RandomGeneration(Dictionary<GenerationParam, object> genParam)
        {
            log.Info("Random generation step started.");
            Int16 branchIndex = (Int16)genParam[GenerationParam.BranchIndex];
            Int32 vertices = (Int32)genParam[GenerationParam.Vertices];
            double mu = (Double)genParam[GenerationParam.Mu];

            container.BranchIndex = branchIndex;
            container.Vertices = vertices;
            container.TreeMatrix = GenerateByVertices(mu);
            log.Info("Random generation step finished.");
        }

        // Строится граф, на основе матрицы смежности.
        protected override void StaticGeneration(string fileName)
        {
            log.Info("Static generation started.");
            container.SetMatrix(fileName);
            log.Info("Static generation finished.");
        }

        // Закрытая часть класса (не из общего интерфейса).

        // Генератор случайного числа.
        private RNGCrypto rand = new RNGCrypto();
        private const int ARRAY_MAX_SIZE = 2000000000;

        // Генерирует граф с данными параметрами(число вершин и мю). Сгенеририванный граф находится в контейнере.

        private BitArray[][] GenerateByVertices(Double m)
        {
            List<List<int>> branchList = GenerateBranchList();
            container.Level = branchList.Count;
            container.Branches = new int[container.Level][];
            BitArray[][] treeMatrix = new BitArray[container.Level][];

            for (int i = 0; i < container.Level; ++i)
            {
                int levelVertexCount = branchList[container.Level - 1 - i].Count;
                container.Branches[i] = new int[levelVertexCount];
                long dataLength = 0;
                for (int j = 0; j < levelVertexCount; ++j)
                {
                    int nodeDataLength = branchList[container.Level - 1 - i][j];
                    container.Branches[i][j] = nodeDataLength;
                    dataLength += nodeDataLength * (nodeDataLength - 1) / 2;
                }

                GenerateTreeMatrixForLevel(treeMatrix, i, dataLength);
            }

            GenerateData(treeMatrix, m);
            return treeMatrix;
        }

        // Генерирует граф с данными параметрами(уровень и мю). Сгенеририванный граф находится в контейнере.
        private BitArray[][] GenerateByLevel(Double m)
        {
            BitArray[][] treeMatrix = new BitArray[container.Level][];
            container.Branches = new int[container.Level][];

            //create datas for every, started from the root
            for (int i = 0; i < container.Level; ++i)
            {
                int levelVertexCount = 0;
                long dataLength = 0;
                if (i == 0)
                {
                    container.Branches[0] = new int[1];
                    int nodeDataLength = rand.Next(1, container.BranchIndex + 1);
                    container.Branches[0][0] = nodeDataLength;
                    dataLength = nodeDataLength * (nodeDataLength - 1) / 2;
                    ++levelVertexCount;
                }
                else
                {
                    for (int j = 0; j < container.Branches[i - 1].Length; ++j)
                    {
                        levelVertexCount += container.Branches[i - 1][j];
                    }

                    container.Branches[i] = new int[levelVertexCount];
                    for (int j = 0; j < levelVertexCount; ++j)
                    {
                        int nodeDataLength = rand.Next(1, container.BranchIndex + 1);
                        container.Branches[i][j] = nodeDataLength;
                        dataLength += nodeDataLength * (nodeDataLength - 1) / 2;
                    }
                }

                GenerateTreeMatrixForLevel(treeMatrix, i, dataLength);
            }

            GenerateData(treeMatrix, m);
            return treeMatrix;
        }

        private List<List<int>> GenerateBranchList()
        {
            List<List<int>> branchList = new List<List<int>>();
            int left = container.Size;

            while (left != 1)
            {
                List<int> list = new List<int>();
                while (left != 0)
                {
                    int randomInt = rand.Next(1, container.BranchIndex + 1);
                    if (randomInt > left)
                    {
                        randomInt = left;
                    }
                    left -= randomInt;
                    list.Add(randomInt);
                }
                branchList.Add(list);
                left = branchList[branchList.Count - 1].Count;
            }
            return branchList;
        }

        private void GenerateTreeMatrixForLevel(BitArray[][] treeMatrix, int level, long dataLength)
        {
            int arrCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataLength) / ARRAY_MAX_SIZE));
            treeMatrix[level] = new BitArray[arrCount];
            if (arrCount > 0)
            {
                int t;
                for (t = 0; t < arrCount - 1; ++t)
                {
                    treeMatrix[level][t] = new BitArray(ARRAY_MAX_SIZE);
                }
                treeMatrix[level][t] = new BitArray(Convert.ToInt32(dataLength - (arrCount - 1) * ARRAY_MAX_SIZE));
            }
        }

        private void GenerateData(BitArray[][] treeMatrix, double m)
        {
            for (int currentLevel = 0; currentLevel < container.Level; ++currentLevel)
            {
                if (treeMatrix[currentLevel].Length > 0)
                {
                    int branchSize = container.Branches[currentLevel][0];
                    int counter = 0, nodeNumber = 0;
                    for (int i = 0; i < treeMatrix[currentLevel].Length; i++)
                    {
                        for (int j = 0; j < treeMatrix[currentLevel][i].Length; j++)
                        {
                            if (counter == (branchSize * (branchSize - 1) / 2))
                            {
                                ++nodeNumber;
                                counter = 0;
                                branchSize = container.Branches[currentLevel][nodeNumber];
                            }
                            double k = rand.NextDouble();
                            if (k <= (1 / Math.Pow(container.CountLeaves(currentLevel, nodeNumber), m)))
                            {
                                treeMatrix[currentLevel][i][j] = true;
                            }
                            else
                            {
                                treeMatrix[currentLevel][i][j] = false;
                            }
                            ++counter;
                        }
                    }
                }
            }
        }
    }
}
