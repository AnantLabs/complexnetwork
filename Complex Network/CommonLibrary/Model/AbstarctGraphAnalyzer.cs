using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CommonLibrary.Model
{
    public abstract class AbstarctGraphAnalyzer : IGraphAnalyzer
    {
        //Calculate degree distribution of graph.
        public virtual SortedDictionary<int, int> GetDegreeDistribution()
        {
            return new SortedDictionary<int, int>();
        }
        //Calculate average parth of graph.
        public virtual double GetAveragePath()
        {
            return 0;
        }
        //Calculate clustering coefficient of graph.
        public virtual SortedDictionary<double, int> GetClusteringCoefficient()
        {
            return new SortedDictionary<double, int>();
        }
        //Calculate Eigen values of graph.
        public virtual ArrayList GetEigenValue()
        {
            return new ArrayList();
        }
        //Calculate count of cycles in 3 lenght of graph.
        public virtual double GetCycles3()
        {
            return 0;
        }

        //Calculate diameter of graph.
        public virtual double GetDiameter()
        {
            return 0;
        }

        //Calculate distribution of connected subgraph of graph.
        public virtual SortedDictionary<int, int> GetConnSubGraph()
        {
            return new SortedDictionary<int, int>();
        }
        //Calculate count of cycles in 3 lenght based in eigen valu of graph.
        public virtual double GetCycleEigen3()
        {
            return 0;
        }

        //Calculate count of cycles in 4 lenght of graph.
        public virtual double GetCycles4()
        {
            return 0;
        }

        //Calculate count of cycles in 4 lenght based in eigen valu of graph.
        public virtual double GetCycleEigen4()
        {
            return 0;
        }
        //Calculate motive of graph.
        public virtual void GetMotif()
        {
        }
        //Calculate distribution of minimum paths of graph.
        public virtual SortedDictionary<int, int> GetMinPathDist()
        {
            return new SortedDictionary<int, int>();
        }
        //Calculate distribution of eigen value of graph.
        public virtual SortedDictionary<double, int> GetDistEigenPath()
        {
            return new SortedDictionary<double, int>();
        }
        //Calculate distribution of connected subgraph of graph.
        public virtual SortedDictionary<int, int> GetFullSubGraph()
        {
            return new SortedDictionary<int, int>();
        }
    }
}
