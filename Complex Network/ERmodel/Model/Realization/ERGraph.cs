using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using RandomGraph.Common.Model;
using Model.ERModel.Result;
using model.ERModel.Realization;

namespace Model.ERModel.Realization
{
    public class ERGraph
    {
        private int m_vertex_count;
        private int m_edge_count; 

        private ERContainer m_container;
        //private ERGenerator m_generator;
        private ERAnalyzer m_analyzer;

        private AnalyzeResult m_analyzeResult;

        public ERGraph(int vcount)
        {

            m_vertex_count = vcount;
            m_container = new ERContainer(m_vertex_count);
            //m_generator = new ERGenerator();
            m_analyzer = new ERAnalyzer(m_container);
        }

        public ERGraph(ArrayList m)
        {
            m_container = new ERContainer(m);
            m_analyzer = new ERAnalyzer(m_container);
        }

        public void Generate(double p)
        {
            m_container.FillContainerByPropability(p);
        }

        public void Analyze(AnalyseOptions m_analyzeOptions)
        {
            m_analyzeResult = m_analyzer.Analyze(m_analyzeOptions);
        }

        public AnalyzeResult Result
        {
            get { return m_analyzeResult; }
        }

        public ERContainer Container
        {
            get { return m_container; }
        }
    }
}
