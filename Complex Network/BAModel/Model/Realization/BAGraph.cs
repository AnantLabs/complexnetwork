using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using RandomGraph.Common.Model;

namespace Model.BAModel.Realization
{
    public class BAGraph
    {
        // Analyze options //
        // public AnalyseOptions m_analyzeOptions;

        // Initialization members //
        private int m_size;             // number of initial vertices
        private int m_numberOfEdges;    // number of edges when adding new vertex

        // Implementation memebers //
        private BAContainer m_container;
        public BAAnalyzer m_analyzer;

        public BAGraph(int size, int numberOfEdges)
        {

            m_size = size;
            m_numberOfEdges = numberOfEdges;
            m_container = new BAContainer(m_size);
        }

        public BAGraph(int size, int numberOfEdges, ArrayList matrix)
        {
            m_size = size;
            m_numberOfEdges = numberOfEdges;
            m_container = new BAContainer(matrix);
        }

        public void Analyze()
        {
            m_analyzer = new BAAnalyzer(m_container);
        }

        // Get functions //
        public BAContainer Container
        {
           get {return m_container;}
        }
    }
}