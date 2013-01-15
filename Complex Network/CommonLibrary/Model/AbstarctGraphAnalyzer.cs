using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using log4net;

namespace CommonLibrary.Model
{
    public abstract class AbstarctGraphAnalyzer
    {
        // Организация Работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(AbstarctGraphAnalyzer));

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public abstract IGraphContainer Container { get; set; }

        // Возвращается средняя длина пути в графе.
        public virtual double GetAveragePath()
        {
            log.Error("This model does not support AveragePath counting algorithm.");
            return 0;
        }

        // Возвращается диаметр графа.
        public virtual int GetDiameter()
        {
            log.Error("This model does not support Diameter counting algorithm.");
            return 0;
        }

        // Возвращается число циклов длиной 3 в графе.
        public virtual int GetCycles3()
        {
            log.Error("This model does not support Cycles3 counting algorithm.");
            return 0;
        }

        // Возвращается число циклов длиной 4 в графе.
        public virtual int GetCycles4()
        {
            log.Error("This model does not support Cycles4 algorithm.");
            return 0;
        }

        // Возвращается число циклов длиной 3 в графе (вычисление с помощью собственных значений).
        public virtual int GetCyclesEigen3()
        {
            log.Error("This model does not support CyclesEigen3 counting algorithm.");
            return 0;
        }

        // Возвращается число циклов длиной 4 в графе(вычисление с помощью собственных значений).
        public virtual int GetCyclesEigen4()
        {
            log.Error("This model does not support CyclesEigen4 counting algorithm.");
            return 0;
        }

        // Возвращается массив собственных значений матрицы смежности.
        public virtual ArrayList GetEigenValues()
        {
            log.Error("This model does not support EigenValues counting algorithm.");
            return new ArrayList();
        }

        // Возвращается распределение длин между собственными значениями.
        public virtual SortedDictionary<double, int> GetDistEigenPath()
        {
            log.Error("This model does not support DistEigenPath counting algorithm.");
            return new SortedDictionary<double, int>();
        }

        // Возвращается степенное распределение графа.
        public virtual SortedDictionary<int, int> GetDegreeDistribution()
        {
            log.Error("This model does not support DegreeDistribution counting algorithm.");
            return new SortedDictionary<int, int>();
        }

        // Возвращается распределение коэффициентов кластеризации графа.
        public virtual SortedDictionary<double, int> GetClusteringCoefficient()
        {
            log.Error("This model does not support ClusteringCoefficient counting algorithm.");
            return new SortedDictionary<double, int>();
        }

        // Возвращается распределение чисел связанных подграфов в графе.
        public virtual SortedDictionary<int, int> GetConnSubGraph()
        {
            log.Error("This model does not support ConnSubGraph counting algorithm.");
            return new SortedDictionary<int, int>();
        }

        // Возвращается распределение чисел полных подграфов в графе.
        public virtual SortedDictionary<int, int> GetFullSubGraph()
        {
            log.Error("This model does not support FullSubGraph counting algorithm.");
            return new SortedDictionary<int, int>();
        }

        // Возвращается распределение длин минимальных путей в графе.
        public virtual SortedDictionary<int, int> GetMinPathDist()
        {
            log.Error("This model does not support MinPathDist counting algorithm.");
            return new SortedDictionary<int, int>();
        }

        // Возвращается распределение чисел циклов (lowBound - минимальная длина, highBound - максимальная длина циклов).
        public virtual SortedDictionary<int, long> GetCycles(int lowBound, int hightBound)
        {
            log.Error("This model does not support Cycles counting algorithm.");
            return new SortedDictionary<int, long>();
        }

        // Возвращается распределение чисел триугольников, связанных с вершиной.
        public virtual SortedDictionary<int, int> GetTrianglesDistribution()
        {
            log.Error("This model does not support Triangles counting algorithm.");
            return new SortedDictionary<int, int>();
        }

        // Возвращается троектория триугольников (зависимость числа триугольников от времени).
        public virtual SortedDictionary<int, long> GetTrianglesTraectory()
        {
            log.Error("This model does not support Triangle Traectory counting algorithm.");
            return new SortedDictionary<int, long>();
        }

        // Возвращается распределение чисел мотивов (lowBound - минимальный порядок, highBound - максимальный порядок мативов).
        public virtual SortedDictionary<int, float> GetMotivs(int lowBound, int hightBound)
        {
            log.Error("This model does not support Motivs counting algorithm.");
            return new SortedDictionary<int, float>();
        }
    }
}
