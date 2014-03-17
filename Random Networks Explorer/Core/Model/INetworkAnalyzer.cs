using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Core.Model
{
    public interface INetworkAnalyzer
    {
        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        INetworkContainer Container { get; set; }

        // Возвращается средняя длина пути в графе.
        Double GetAveragePath();

        // Возвращается диаметр графа.
        Int32 GetDiameter();

        Double GetAverageDegree();
        Double GetAverageClusteringCoefficient();

        // Возвращается число циклов длиной 3 в графе.
        BigInteger GetCycles3();

        // Возвращается число циклов длиной 4 в графе.
        BigInteger GetCycles4();

        // Возвращается массив собственных значений матрицы смежности.
        List<Double> GetEigenValues();

        // Возвращается число циклов длиной 3 в графе (вычисление с помощью собственных значений).
        BigInteger GetCycles3Eigen();

        // Возвращается число циклов длиной 4 в графе(вычисление с помощью собственных значений).
        BigInteger GetCycles4Eigen();

        // Возвращается распределение длин между собственными значениями.
        SortedDictionary<Double, Int32> GetEigenDistanceDistribution();

        // Возвращается степенное распределение графа.
        SortedDictionary<Int32, Int32> GetDegreeDistribution();

        // Возвращается распределение коэффициентов кластеризации графа.
        SortedDictionary<Double, Int32> GetClusteringCoefficientDistribution();

        // Возвращается распределение чисел связанных подграфов в графе.
        SortedDictionary<Int32, Int32> GetConnectedComponentDistribution();

        // Возвращается распределение чисел полных подграфов в графе.
        SortedDictionary<Int32, Int32> GetCompleteComponentDistribution();

        // Возвращается распределение длин минимальных путей в графе.
        SortedDictionary<Int32, Int32> GetDistanceDistribution();

        // Возвращается распределение чисел триугольников, связанных с вершиной.
        SortedDictionary<Int32, Int32> GetTriangleByVertexDistribution();

        // Возвращается распределение чисел циклов (lowBound - минимальная длина, highBound - максимальная длина циклов).
        SortedDictionary<Int32, BigInteger> GetCycleDistribution(Int16 lowBound, Int16 hightBound);
    }
}
