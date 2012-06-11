using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using log4net;

namespace CommonLibrary.Model
{
    public abstract class AbstarctGraphAnalyzer : IGraphAnalyzer
    {
        //Calculate degree distribution of graph.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(AbstarctGraphAnalyzer));
        public virtual SortedDictionary<int, int> GetDegreeDistribution()
        {
            log.Error("This model did not support GetDegreeDistribution algorithm");
            return new SortedDictionary<int, int>();
        }

        //Calculate average parth of graph.
        public virtual double GetAveragePath()
        {
            log.Error("This model did not support GetDegreeDistribution algorithm");
            return 0;
        }

        //Calculate clustering coefficient of graph.
        public virtual SortedDictionary<double, int> GetClusteringCoefficient()
        {
            log.Error("This model did not support GetClusteringCoefficient algorithm");
            return new SortedDictionary<double, int>();
        }

        //Calculate Eigen values of graph.
        public virtual ArrayList GetEigenValue()
        {
            log.Error("This model did not support GetEigenValue algorithm");
            return new ArrayList();
        }

        //Calculate count of cycles in 3 lenght of graph.
        public virtual int GetCycles3()
        {
            log.Error("This model did not support GetCycles3 algorithm");
            return 0;
        }

        //Calculate diameter of graph.
        public virtual int GetDiameter()
        {
            log.Error("This model did not support GetDiameter algorithm");
            return 0;
        }

        //Calculate distribution of connected subgraph of graph.
        public virtual SortedDictionary<int, int> GetConnSubGraph()
        {
            log.Error("This model did not support GetConnSubGraph algorithm");
            return new SortedDictionary<int, int>();
        }

        //Calculate count of cycles in 3 lenght based in eigen valu of graph.
        public virtual int GetCycleEigen3()
        {
            log.Error("This model did not support GetCycleEigen3 algorithm");
            return 0;
        }

        //Calculate count of cycles in 4 lenght of graph.
        public virtual int GetCycles4()
        {
            log.Error("This model did not support GetCycles4 algorithm");
            return 0;
        }

        //Calculate count of cycles in 4 lenght based in eigen valu of graph.
        public virtual int GetCycleEigen4()
        {
            log.Error("This model did not support GetCycleEigen4 algorithm");
            return 0;
        }

        //Calculate motive of graph.
        public virtual SortedDictionary<int, float> GetMotif(int minMotiv, int maxMotiv)
        {
            log.Error("This model did not support GetMotif algorithm");
            return new SortedDictionary<int, float>();
        }

        //Calculate distribution of minimum paths of graph.
        public virtual SortedDictionary<int, int> GetMinPathDist()
        {
            log.Error("This model did not support GetMinPathDist algorithm");
            return new SortedDictionary<int, int>();
        }

        //Calculate distribution of eigen value of graph.
        public virtual SortedDictionary<double, int> GetDistEigenPath()
        {
            log.Error("This model did not support GetDistEigenPath algorithm");
            return new SortedDictionary<double, int>();
        }

        //Calculate distribution of connected subgraph of graph.
        public virtual SortedDictionary<int, int> GetFullSubGraph()
        {
            log.Error("This model did not support GetFullSubGraph algorithm");
            return new SortedDictionary<int, int>();
        }

        //Calculate distribution of cycles of graph.
        public virtual SortedDictionary<int, long> GetCycles(int lowBound, int hightBound)
        {
            log.Error("This model did not support GetCycles algorithm");
            return new SortedDictionary<int, long>();
        }

        //Calculate distribution of motives subgraph of graph.
        public virtual SortedDictionary<int, double> GetMotivs(int lowBound, int hightBound)
        {
            log.Error("This model did not support GetMotivs algorithm");
            return new SortedDictionary<int, double>();
        }
    }
}
