using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Model.StaticModel.Result
{
    public struct AnalyzeResult
    {
        public double m_avgPathLenght;
        public int m_diametr;
        public double m_clusteringCoefficient;
        public SortedDictionary<double, int> m_iclusteringCoefficient;
        public SortedDictionary<int, int> m_degreeDistribution;//count the number of vertex that have i degrees
        public SortedDictionary<int, int> m_pathDistribution;
        public int m_cyclesOfOrder3;
        public int m_cyclesOfOrder4;
        public int m_maxfullsubgraph;
        public ArrayList ArrayOfEigVal;
    }
}
