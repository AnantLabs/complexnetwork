using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Model.ERModel.Result
{
    public struct AnalyzeResult
    {
        public double m_avgPathLenght;
        public double m_avgDegree;
        public int m_diameter;
        public double m_clusteringCoefficient;
        public SortedDictionary<double, int> m_vertexClusteringCoefficient;
        public SortedDictionary<int, int> m_degreeDistribution;//count the number of vertex that have i degrees
        public SortedDictionary<int, int> m_pathDistribution;
        public int m_cyclesOfOrder3;
        public int m_cyclesOfOrder4;
        public int m_maxfullsubgraph;
        public ArrayList ArrayOfEigVal;
    }
}
