using System;
using System.Collections;
using System.Collections.Generic;

using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model;
using NumberGeneration;
using log4net;

namespace Model.HierarchicModel.Realization
{
    // Реализация генератор (Block-Hierarchic).
    public class HierarchicGenerator : AbstractGraphGenerator
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(HierarchicGenerator));

        // Контейнер, в котором содержится граф конкретной модели (Block-Hierarchic).
        private HierarchicContainer container;

        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public HierarchicGenerator()
        {
            container = new HierarchicContainer();
        }

        // Контейнер, в котором содержится сгенерированный граф.
        public override AbstractGraphContainer Container
        {
            get { return container; }
            set { container = (HierarchicContainer)value; }
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
            Int16 level = (Int16)genParam[GenerationParam.Level];
            double mu = (Double)genParam[GenerationParam.Mu];

            container.BranchIndex = branchIndex;
            container.Level = level;
            container.TreeMatrix = GenerateTree(branchIndex, level, mu);
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

                //generates data for current level nodes
                GenerateData(treeMatrix, i, b, d, m);
            }

            return treeMatrix;
        }

        private void GenerateData(BitArray[][] treeMatrix, int level, int b, int d, double m)
        {
            //loop over all elements of given level and generate him values
            for (int i = 0; i < treeMatrix[d - level].Length; i++)
            {
                for (int j = 0; j < treeMatrix[d - level][i].Length; j++)
                {
                    double k = rand.NextDouble();
                    if (k <= (1 / Math.Pow(b, level * m)))
                    {
                        treeMatrix[d - level][i][j] = true;
                    }
                    else
                    {
                        treeMatrix[d - level][i][j] = false;
                    }
                }
            }
        }

        // !Исправить! подумать о включении в общий интерфейс

        // Генерация иерархического дерева. 
        public void GenerateTreeWithProbability(
            Int16 branchingIndex,   // индекс ветвления
            int maxLevel,   // максимальный уровень иерархии
            double mainProbability, // вероятность появления соединений на всех уровнях (кроме currentLevel) 
            Int16 currentLevel, // номер уровня (с особым видом генерации) - реальный
            double probabilityCounting,    // параметр для вычисления вероятности генерации (в частном случае mu)
            ProbabilityCounter currentProbability   // делегат на функцию вычисления вероятности генерации
            )
        {
            BitArray[][] treeMatrix = new BitArray[maxLevel][];

            // Для каждого уровня, начиная из корня, создаются данные (метки).
            for (int i = maxLevel; i > 0; i--)
            {
                // Вычисление длины и числа данных (меток) для текущего уровня.
                int nodeDataLength = (branchingIndex - 1) * branchingIndex / 2;
                long dataLength = Convert.ToInt64(Math.Pow(branchingIndex, maxLevel - i) * nodeDataLength);
                int arrCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataLength) / ARRAY_MAX_SIZE));

                treeMatrix[maxLevel - i] = new BitArray[arrCount];
                int j;
                for (j = 0; j < arrCount - 1; j++)
                {
                    treeMatrix[maxLevel - i][j] = new BitArray(ARRAY_MAX_SIZE);
                }
                treeMatrix[maxLevel - i][j] = 
                    new BitArray(Convert.ToInt32(dataLength - (arrCount - 1) * ARRAY_MAX_SIZE));

                // Генерация для текущего уровня.
                if (i == currentLevel)   // если генерируется уровень с особым видом генерации
                {
                    GenerateDataWithProbability(treeMatrix, maxLevel, i, 
                        currentProbability(branchingIndex, currentLevel, probabilityCounting));
                }
                else
                {
                    GenerateDataWithProbability(treeMatrix, maxLevel, i, mainProbability);
                }
            }

            container.BranchIndex = branchingIndex;
            container.Level = maxLevel;
            container.TreeMatrix = treeMatrix;
        }

        private void GenerateDataWithProbability(
            BitArray[][] treeMatrix,   // иерархическое дерево
            int maxLevel,   // максимальный уровень иерархии
            int currentLevel,   // номер текущего уровня
            double currentProbability   // вероятность появления соединений для текущего уровня
            )
        {
            // Обход всех элементов данного уровня и генерация значений.
            for (int i = 0; i < treeMatrix[maxLevel - currentLevel].Length; i++)
            {
                for (int j = 0; j < treeMatrix[maxLevel - currentLevel][i].Length; j++)
                {
                    double k = rand.NextDouble();
                    if (k <= currentProbability)
                    {
                        treeMatrix[maxLevel - currentLevel][i][j] = true;
                    }
                    else
                    {
                        treeMatrix[maxLevel - currentLevel][i][j] = false;
                    }
                }
            }
        }
    }
}
