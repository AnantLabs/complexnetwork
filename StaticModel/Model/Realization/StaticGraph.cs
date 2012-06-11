using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using RandomGraph.Common.Model;
using Model.StaticModel.Result;

namespace Model.StaticModel.Realization
{
    public class StaticGraph
    {
        // Analyze options //
        // public AnalyseOptions m_analyzeOptions;

        // Initialization members //
       // private int m_size;             // number of initial vertices
      //  private int m_numberOfEdges;    // number of edges when adding new vertex

        // Implementation memebers //
        private StaticContainer m_container;
        private StaticGenerator m_generator;
        public StaticAnalyzer m_analyzer;

        // Result members //
        private AnalyzeResult m_analyzeResult;

        public StaticGraph(ArrayList matrix)
        {
            m_container = new StaticContainer(matrix);
        }

        public void Generate()
        {
            double[] probabilyArray = m_container.CountProbabilities();
            m_container.AddVertex();
            m_container.RefreshNeighbourships(m_generator.MakeGenerationStep(probabilyArray));
        }

        public void Analyze(AnalyseOptions m_analyzeOptions)
        {
            m_analyzer = new StaticAnalyzer(m_container);
            m_analyzeResult = m_analyzer.Analyze(m_analyzeOptions);
        }

        // Get functions //
        public AnalyzeResult Result
        {
            get { return m_analyzeResult; }
        }
        public StaticContainer Container
        {
           get {return m_container;}
        }
    }
}