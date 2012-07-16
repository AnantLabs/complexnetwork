using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CommonLibrary.Model
{
    // Общий интерфейс для анализатора графа (вычислений статистических характеристик).
    // Каждий анализатор графа любой модели должен реализовать этот интерфейс.
    public interface IGraphAnalyzer
    {
        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        IGraphContainer Container { get; set; }

        // Возвращается средняя длина пути в графе.
        double GetAveragePath();
        // Возвращается диаметр графа.
        int GetDiameter();
        // Возвращается число циклов длиной 3 в графе.
        int GetCycles3();
        // Возвращается число циклов длиной 4 в графе.
        int GetCycles4();
        // Возвращается число циклов длиной 3 в графе (вычисление с помощью собственных значений).
        int GetCyclesEigen3();
        // Возвращается число циклов длиной 4 в графе(вычисление с помощью собственных значений).
        int GetCyclesEigen4();

        // Возвращается массив собственных значений матрицы смежности.
        ArrayList GetEigenValues();
        // Возвращается распределение длин между собственными значениями.
        SortedDictionary<double, int> GetDistEigenPath();

        // Возвращается степенное распределение графа.
        SortedDictionary<int, int> GetDegreeDistribution();
        // Возвращается распределение коэффициентов кластеризации графа.
        SortedDictionary<double, int> GetClusteringCoefficient();
        // Возвращается распределение чисел связанных подграфов в графе.
        SortedDictionary<int, int> GetConnSubGraph();
        // Возвращается распределение чисел полных подграфов в графе.
        SortedDictionary<int, int> GetFullSubGraph();
        // Возвращается распределение длин минимальных путей в графе.
        SortedDictionary<int, int> GetMinPathDist();
        // Возвращается распределение чисел циклов (lowBound - минимальная длина, highBound - максимальная длина циклов).
        SortedDictionary<int, long> GetCycles(int lowBound, int highBound);

        // Возвращается распределение чисел мотивов (lowBound - минимальный порядок, highBound - максимальный порядок мативов).
        SortedDictionary<int, float> GetMotivs(int lowBound, int highBound);
    }
}
