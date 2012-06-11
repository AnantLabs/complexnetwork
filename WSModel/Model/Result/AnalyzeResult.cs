using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Model.WSModel.Result
{
    public struct AnalyzeResult
    {
        public double m_avgPathLenght;
        public int m_diametr;
        public double m_clusteringCoefficient;
        public SortedDictionary<int, int> m_degreeDistribution;
        public SortedDictionary<double, int> m_coefficient;
        public int m_cyclesOfOrder3;
        public int m_cyclesOfOrder4;
        public SortedDictionary<int, int> m_fullSubgraphs;
        public int m_maxfullsubgraph;
        public SortedDictionary<int, int> m_vertexDistances;
    }
}
