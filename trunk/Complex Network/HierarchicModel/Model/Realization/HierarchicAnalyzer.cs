using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CommonLibrary.Model;
using Algorithms;
using log4net;

namespace Model.HierarchicModel.Realization
{
    // Реализация анализатора (Block-Hierarchic).
    public class HierarchicAnalyzer : AbstarctGraphAnalyzer
    {
        // Организация работы с лог файлом.
        protected static readonly new ILog log = log4net.LogManager.GetLogger(typeof(HierarchicAnalyzer));

        // Контейнер, в котором содержится граф конкретной модели (Block-Hierarchic).
        private HierarchicContainer container;

        public HierarchicAnalyzer(HierarchicContainer c)
        {
            log.Info("Creating HierarchicAnalyzer object.");
            container = c;
        }

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public override IGraphContainer Container
        {
            get { return container; }
            set { container = (HierarchicContainer)value; }
        }

        // Возвращается средняя длина пути в графе. Реализовано.
        public override double GetAveragePath()
        {
            log.Info("Getting average path length.");

            long[] pathsInfo = GetSubgraphsPathInfo(0, 0);
            //int p = Engine.pathsCount;
            // !petq e bajanel chanaparhneri qanaki vra!
            return 2 * (pathsInfo[0] + pathsInfo[2]) / ((double)container.Size *
                ((double)container.Size - 1));
        }

        // Возвращается число циклов длиной 3 в графе. Реализовано.
        public override int GetCycles3()
        {
            log.Info("Getting count of cycles - order 3.");
            return (int)Count3Cycle(0, 0)[0];
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public override int GetCycles4()
        {
            log.Info("Getting count of cycles - order 4.");
            return (int)Count4Cycle(0, 0)[0];
        }

        // Возвращается число циклов длиной 3 в графе, с помощью собственных значений. Реализовано.
        // Используется только для модели ParisiHierarchicModel.
        public override int GetCyclesEigen3()
        {
            log.Info("Getting count of cycles by eigen values - order 3.");
            return (int)CalcCyclesCount(3);
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        // Используется только для модели ParisiHierarchicModel.
        public override int GetCyclesEigen4()
        {
            log.Info("Getting count of cycles by eigen values - order 4.");
            return (int)CalcCyclesCount(4);
        }

        // Возвращается массив собственных значений матрицы смежности. Реализовано.
        public override ArrayList GetEigenValues()
        {
            log.Info("Getting eigen values array.");
            bool[,] m = container.GetMatrix();
            EigenValueUtils eg = new EigenValueUtils();
            try
            {
                return eg.CalculateEigenValue(m);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return new ArrayList();
            }

            // Правильно для модели ParisiHierarchicModel.
            //return new ArrayList(CalcEigenValue(container.TreeVector(), container.BranchIndex));
        }

        // Возвращается распределение длин между собственными значениями. Реализовано
        public override SortedDictionary<double, int> GetDistEigenPath()
        {
            log.Info("Getting distances between eigen values.");
            bool[,] m = container.GetMatrix();
            EigenValueUtils eg = new EigenValueUtils();
            try
            {
                eg.CalculateEigenValue(m);
                return eg.CalcEigenValuesDist();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return new SortedDictionary<double, int>();
            }
        }

        // Возвращается степенное распределение графа. Реализовано.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            log.Info("Getting degree distribution.");
            return ArrayCntAdjacentCntVertexes(0, 0);
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            log.Info("Getting clustering coefficients.");
            SortedDictionary<double, int> result = new SortedDictionary<double, int>();

            for (int i = 0; i < container.Size; i++)
            {
                double dresult = ClusteringCoefficientOfVertex(i);
                dresult = dresult * 10000;
                int iResult = Convert.ToInt32(dresult);
                double r = (double)iResult / 10000;
                if (result.Keys.Contains(r))
                    result[r] += 1;
                else
                    result.Add(r, 1);
            }

            return result;
        }

        // Возвращается распределение чисел связанных подграфов в графе. Реализовано.
        public override SortedDictionary<int, int> GetConnSubGraph()
        {
            log.Info("Getting connected subgraphs.");
            return AmountConnectedSubGraphs(0, 0);
        }

        // Возвращается распределение чисел циклов. Реализовано.
        public override SortedDictionary<int, long> GetCycles(int lowBound, int hightBound)
        {
            log.Info("Getting cycles.");
            EngineForCycles engForCycles = new EngineForCycles(container);

            SortedDictionary<int, long> result = new SortedDictionary<int, long>();
            for (int l = lowBound; l <= hightBound; ++l)
            {
                result.Add(l, (int)engForCycles.GetCycleCount(l));
            }

            return result;
        }


        // Закрытая часть класса (не из общего интерфейса). //

        /// <summary>
        /// Возвращает  <countAdjacent,countVertexes> 
        /// </summary>
        /// <param name="HierarchicGraph"></param>
        /// <returns></returns>
        private SortedDictionary<int, int> ArrayCntAdjacentCntVertexes(int numberNode, int level)
        {
            if (level == container.Level)
            {
                SortedDictionary<int, int> returned = new SortedDictionary<int, int>();
                returned[0] = 1;
                return returned;
            }
            else
            {
                BitArray node = container.TreeNode(level, numberNode);

                SortedDictionary<int, int> arraysReturned = new SortedDictionary<int, int>();
                SortedDictionary<int, int> array = new SortedDictionary<int, int>();
                int powPK = Convert.ToInt32(Math.Pow(container.BranchIndex, container.Level - level - 1));

                for (int i = numberNode * container.BranchIndex; i < container.BranchIndex * (numberNode + 1); i++)
                {
                    int nodeNumberi = i - numberNode * container.BranchIndex;
                    array = ArrayCntAdjacentCntVertexes(i, level + 1);
                    int countAjacentsThisnode = container.CountConnectedBlocks(node, nodeNumberi);
                    foreach (KeyValuePair<int, int> kvt in array)
                    {
                        int key = kvt.Key + countAjacentsThisnode * powPK;
                        if (arraysReturned.ContainsKey(key))
                            arraysReturned[key] += kvt.Value;
                        else
                            arraysReturned.Add(key, kvt.Value);
                    }

                }
                return arraysReturned;
            }
        }

        // Возвращает информацию о пути подграфа (реализована рекурсивным образом).
        // Используется алгоритм Флойда для вычисления минимальных путей между вершинами графа.
        private long[] GetSubgraphsPathInfo(int level, long nodeNumber)
        {
            //resultArr's and tempinfo's 
            //1 element is current paths, that can't minimized, lengths sum
            //2 temp paths count, that have chance to be minimized
            //3 >2 paths' lengths sum
            long[] resultArr = { 0, 0, 0 };
            long[] tempInfo = { 0, 0, 0 };

            // Если это не лист дерева, то проход по всем дочерным узлам (рекурсивный вызов).
            if (level < container.Level - 1)
            {
                for (int i = 0; i < container.BranchIndex; i++)
                {
                    tempInfo = GetSubgraphsPathInfo(level + 1, nodeNumber * container.BranchIndex + i);

                    resultArr[0] += tempInfo[0];
                    if (container.NodeChildAdjacentsCount(level, nodeNumber, i) > 0)
                    {
                        resultArr[0] += tempInfo[1] * 2;
                    }
                    else
                    {
                        resultArr[1] += tempInfo[1];
                        resultArr[2] += tempInfo[2];
                    }
                }
            }

            // Получение суммы длин минимальных путей (и дополнительной информации) для данного узла.
            tempInfo = Engine.FloydMinPath(container.NodeMatrix(level, nodeNumber));

            double tempPow = Math.Pow(container.BranchIndex, container.Level - level - 1);
            resultArr[0] += tempInfo[0] * Convert.ToInt64(Math.Pow(tempPow, 2));
            resultArr[1] += tempInfo[1] * Convert.ToInt64(Math.Pow(tempPow, 2));
            resultArr[2] += tempInfo[2] * Convert.ToInt64(Math.Pow(tempPow, 2));

            return resultArr;
        }

        private SortedDictionary<int, int> AmountConnectedSubGraphs(int numberNode, int level)
        {
            SortedDictionary<int, int> retArray = new SortedDictionary<int, int>();

            if (level == container.Level)
            {
                retArray[1] = 1;
                return retArray;
            }
            BitArray node = container.TreeNode(level, numberNode);

            bool haveOne = false;
            for (int i = 0; i < container.BranchIndex; i++)
            {
                if (container.CountConnectedBlocks(node, i) == 0)
                {
                    SortedDictionary<int, int> array = AmountConnectedSubGraphs(numberNode * container.BranchIndex + i, 
                        level + 1);

                    foreach (KeyValuePair<int, int> kvt in array)
                    {
                        if (retArray.Keys.Contains(kvt.Key))
                            retArray[kvt.Key] += kvt.Value;
                        else
                            retArray.Add(kvt.Key, kvt.Value);
                    }
                }
                else
                    haveOne = true;
            }
            if (haveOne)
            {
                int powPK = Convert.ToInt32(Math.Pow(container.BranchIndex, container.Level - level - 1));
                EngineForConnectedComp engForConnectedComponent = new EngineForConnectedComp();
                ArrayList arrConnComp = engForConnectedComponent.GetCountConnSGruph(container.nodeMatrixList(node), 
                    container.BranchIndex);
                for (int i = 0; i < arrConnComp.Count; i++)
                {
                    int countConnCompi = (int)arrConnComp[i];
                    if (retArray.Keys.Contains(countConnCompi * powPK))
                        retArray[countConnCompi * powPK] += 1;
                    else
                        retArray.Add(countConnCompi * powPK, 1);
                }
            }

            return retArray;
        }

        // Возвращает число циклов порядка 3 в нулевом элементе SortedDictionary<int, double>.
        // Число циклов вычисляется в данном узле данного уровня.
        private SortedDictionary<int, double> Count3Cycle(int numberNode, int level)
        {
            SortedDictionary<int, double> retArray = new SortedDictionary<int, double>();
            retArray[0] = 0; // count cycles
            retArray[1] = 0; // count edges

            if (level == container.Level)
            {
                return retArray;
            }
            else
            {
                double countCycle = 0;
                double[] countEdge = new double[container.BranchIndex];
                int countOne = 0;
                double powPK = Math.Pow(container.BranchIndex, container.Level - level - 1);
                BitArray node = container.TreeNode(level, numberNode);

                for (int i = numberNode * container.BranchIndex; i < container.BranchIndex * (numberNode + 1); i++)
                {
                    SortedDictionary<int, double> arr = new SortedDictionary<int, double>();
                    arr = Count3Cycle(i, level + 1);
                    countEdge[i - numberNode * container.BranchIndex] = arr[1];
                    retArray[0] += arr[0];
                    retArray[1] += arr[1];
                }
                for (int i = 0; i < (container.BranchIndex * (container.BranchIndex - 1) / 2); i++)
                {
                    countOne += (node[i]) ? 1 : 0;
                }
                retArray[1] += countOne * powPK * powPK;


                for (int i = numberNode * container.BranchIndex; i < container.BranchIndex * (numberNode + 1); i++)
                {
                    for (int j = (i + 1); j < container.BranchIndex * (numberNode + 1); j++)
                    {
                        if (container.IsConnectedTwoBlocks(node, i - numberNode * container.BranchIndex, 
                            j - numberNode * container.BranchIndex))
                        {
                            countCycle += (countEdge[i - numberNode * container.BranchIndex] + 
                                countEdge[j - numberNode * container.BranchIndex]) * powPK;

                            for (int k = (j + 1); k < container.BranchIndex * (numberNode + 1); k++)
                            {
                                if (container.IsConnectedTwoBlocks(node, j - numberNode * container.BranchIndex, 
                                    k - numberNode * container.BranchIndex) 
                                    && container.IsConnectedTwoBlocks(node, i - numberNode * container.BranchIndex, 
                                    k - numberNode * container.BranchIndex))
                                    countCycle += powPK * powPK * powPK;
                            }
                        }
                    }
                }
                retArray[0] += countCycle;
                
                return retArray;
            }
        }

        // Возвращает число циклов порядка 4 в нулевом элементе SortedDictionary<int, double>.
        // Число циклов вычисляется в данном узле данного уровня.
        private SortedDictionary<int, double> Count4Cycle(int numberNode, int level)
        {
            SortedDictionary<int, double> arrayRetured = new SortedDictionary<int, double>();
            arrayRetured[0] = 0; // count cycles with 4 length
            arrayRetured[1] = 0; // count way with 1 length
            arrayRetured[2] = 0; // count way with 2 length

            if (level == container.Level)
            {
                return arrayRetured;
            }
            else
            {
                SortedDictionary<int, SortedDictionary<int, double>> array = 
                    new SortedDictionary<int, SortedDictionary<int, double>>();
                double powPK = Math.Pow(container.BranchIndex, container.Level - level - 1);

                for (int i = numberNode * container.BranchIndex; i < container.BranchIndex * (numberNode + 1); i++)
                {
                    array[i] = Count4Cycle(i, level + 1);
                    arrayRetured[0] += array[i][0];
                    arrayRetured[1] += array[i][1];
                    arrayRetured[2] += array[i][2];
                }

                BitArray node = container.TreeNode(level, numberNode);

                string str = "";
                for (int b = 0; b < node.Length; b++)
                    str += node[b];

                for (int i = numberNode * container.BranchIndex; i < container.BranchIndex * (numberNode + 1); i++)
                {
                    arrayRetured[2] += container.Factorial(container.CountConnectedBlocks(node, 
                        i - numberNode * container.BranchIndex) - 1) * powPK * powPK * powPK;

                    for (int j = (i + 1); j < container.BranchIndex * (numberNode + 1); j++)
                    {
                        if (container.IsConnectedTwoBlocks(node, i - numberNode * container.BranchIndex, 
                            j - numberNode * container.BranchIndex))
                        {
                            if (level < container.Level)
                            {
                                arrayRetured[0] += 2 * array[i][1] * array[j][1];   // corrected
                                arrayRetured[0] += (array[i][2] + array[j][2]) * powPK;

                                arrayRetured[1] += powPK * powPK;

                                arrayRetured[2] += 2 * powPK * (array[i][1] + array[j][1]);

                                // correct addition
                                arrayRetured[0] += Convert.ToInt32(Math.Pow(powPK * (powPK - 1) / 2, 2));
                            }

                            for (int k = (j + 1); k < container.BranchIndex * (numberNode + 1); k++)
                            {
                                if (container.IsConnectedTwoBlocks(node, j - numberNode * container.BranchIndex, 
                                    k - numberNode * container.BranchIndex))
                                {
                                    // correct addition
                                    arrayRetured[0] += (powPK * powPK * powPK) * (powPK - 1) / 2;

                                    if (container.IsConnectedTwoBlocks(node, i - numberNode * container.BranchIndex, 
                                        k - numberNode * container.BranchIndex))
                                    {
                                        arrayRetured[0] += (array[i][1] + array[j][1] + array[k][1]) * powPK * powPK;
                                    }

                                    for (int l = (k + 1); l < container.BranchIndex * (numberNode + 1); l++)
                                        if (container.IsConnectedTwoBlocks(node, k - numberNode * container.BranchIndex, 
                                            l - numberNode * container.BranchIndex)
                                            && container.IsConnectedTwoBlocks(node, i - numberNode * container.BranchIndex, 
                                            l - numberNode * container.BranchIndex))
                                            arrayRetured[0] += powPK * powPK * powPK * powPK;
                                }
                            }
                        }
                    }
                }

                return arrayRetured;
            }
        }

        /// <summary>
        /// Возвращает коэффициент кластеризации графа.
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private double ClusteringCoefficientOfVertex(long vert)
        {
            double sum = 0;
            long adjCount = 0;
            //loop over all levels
            for (int level = container.Level - 1; level >= 0; level--)
            {
                //get vertex position in current level
                long vertNodeNum = Convert.ToInt64(Math.Floor(Convert.ToDouble(vert / container.BranchIndex)));
                int vertNodeInd = Convert.ToInt32(vert % container.BranchIndex);

                //get vertex adjacent vertexes in current node
                List<int> adjIndexes = container.NodeChildAdjacentsArray(level, vertNodeNum, vertNodeInd);

                long levelVertexCount = Convert.ToInt64(Math.Pow(container.BranchIndex, container.Level - level - 1));
                //vertex subtree vertexes with adjacent subtrees vertexes
                long vertexSubTreeWithAdjSubTrees = adjCount * levelVertexCount * adjIndexes.Count;
                sum += vertexSubTreeWithAdjSubTrees;
                //add adjacent vertexes count
                adjCount += levelVertexCount * adjIndexes.Count;
                //adjacent subtrees weights
                for (int i = 0; i < container.BranchIndex; i++)
                {
                    if (adjIndexes.IndexOf(i) != -1)
                    {
                        sum += container.CountEdges(vertNodeNum * container.BranchIndex + i, level + 1);
                    }
                }
                //connectivity of adjacent subtrees
                for (int i = 0; i < container.BranchIndex; i++)
                {
                    if (adjIndexes.IndexOf(i) != -1)
                    {
                        for (int j = i; j < container.BranchIndex; j++)
                        {
                            if (i != j && i != vertNodeInd && j != vertNodeInd)
                            {
                                sum += container.AreAdjacent(level, vertNodeNum, i, j) * Math.Pow(levelVertexCount, 2);
                            }
                        }
                    }
                }

                vert = vertNodeNum;
            }
            double vertClustCoef = 0;
            if (adjCount > 1)
            {
                vertClustCoef = 2 * sum / (adjCount * (adjCount - 1));
            }
            else if (adjCount == 1)
            {
                vertClustCoef = sum;
            }

            return vertClustCoef;
        }

        /// <summary>
        /// Возвращает собственные значения.
        /// </summary>
        /// <param name="bitArr"></param>
        /// <param name="mBase"></param>
        /// <param name="EigValue"></param>
        private List<double> CalcEigenValue(BitArray bitArr, int mBase)
        {
            List<double> EigValue = new List<double>();

            List<double> basicEigValue = new List<double>(mBase);
            List<double> eigValueE = new List<double>(mBase);
            for (int i = 1; i < mBase; ++i)
            {
                eigValueE.Add(0);
            }
            eigValueE.Add(mBase);
            int bitArrSize = bitArr.Count;
            if (bitArr[0] == false)
            {
                if (bitArr[1] == false)
                {
                    for (int i = 0; i < mBase; ++i)
                    {
                        basicEigValue.Add(0);
                    }
                }
                else
                {
                    for (int i = 0; i < mBase; ++i)
                    {
                        basicEigValue.Add(1);
                    }
                }
            }
            else
            {
                if (bitArr[1] == false)
                {
                    for (int i = 1; i < mBase; ++i)
                    {
                        basicEigValue.Add(-1);
                    }
                    basicEigValue.Add(mBase - 1);
                }
                else
                {
                    for (int i = 1; i < mBase; ++i)
                    {
                        basicEigValue.Add(0);
                    }
                    basicEigValue.Add(mBase);
                }
            }
            int size = mBase;
            BitArray BA = new BitArray(bitArrSize + 1);
            for (int i = 0; i < bitArrSize; ++i)
                BA[i] = bitArr[i];
            BA.Set(bitArrSize, false);
            int x = 1;
            while (x != bitArrSize)
            {
                foreach (int elemE in eigValueE)
                {
                    int t1, t2;
                    if (BA[x] == true)
                        t1 = 1;
                    else
                        t1 = 0;
                    if (BA[x + 1] == true)
                        t2 = 1;
                    else
                        t2 = 0;
                    foreach (int elem in basicEigValue)
                        EigValue.Add(elem * elemE - t1 + t2);
                }
                ++x;
                basicEigValue.Clear();
                basicEigValue.InsertRange(0, EigValue);
                EigValue.Clear();

            }

            EigValue.InsertRange(0, basicEigValue);
            return EigValue;
        }

        // Возвращает число циклов данного порядка, с помощью собственных значений.
        public double CalcCyclesCount(int cycleLength)
        {
            List<double> eigValue = CalcEigenValue(container.TreeVector(), container.BranchIndex);

            double total = 0;
            foreach (int i in eigValue)
            {
                total += Math.Pow(i, cycleLength);
            }
            return total / (2 * cycleLength);
        }

        // Возвращает среднее степеней. Не используется.
        public double AverageDegree()
        {
            return container.CountEdgesAllGraph() * 2 / container.Size;
        }

        // Возвращает сумму минимальных путей. Не используется.
        public double MinPathsSum()
        {
            long[] pathsInfo = GetSubgraphsPathInfo(0, 0);
            return pathsInfo[0] + pathsInfo[2];
        }
    }
}
