using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CommonLibrary.Model
{
    /* Common interface which provide interface of algorithms.
        Each method is returned result of the appropriate  algorithm. */
    public interface IGraphAnalyzer
    {
        //Calculate degree distribution of graph.
        SortedDictionary<int, int> GetDegreeDistribution();

        //Calculate average parth of graph.
        double GetAveragePath();

        //Calculate clustering coefficient of graph.
        SortedDictionary<double, int> GetClusteringCoefficient();

        //Calculate Eigen values of graph.
        ArrayList GetEigenValue();

        //Calculate count of cycles in 3 lenght of graph.
        double GetCycles3();

        //Calculate diameter of graph.
        double GetDiameter();

        //Calculate distribution of connected subgraph of graph.
        SortedDictionary<int, int> GetConnSubGraph();

        //Calculate count of cycles in 3 lenght based in eigen valu of graph.
        double GetCycleEigen3();

        //Calculate count of cycles in 4 lenght of graph.
        double GetCycles4();

        //Calculate count of cycles in 4 lenght based in eigen valu of graph.
        double GetCycleEigen4();

        //Calculate motive of graph.
        void GetMotif();

        //Calculate distribution of minimum paths of graph.
        SortedDictionary<int, int> GetMinPathDist();

        //Calculate distribution of eigen value of graph.
        SortedDictionary<double, int> GetDistEigenPath();
        
        //Calculate distribution of connected subgraph of graph.
        SortedDictionary<int, int> GetFullSubGraph();

        //
    }

}
