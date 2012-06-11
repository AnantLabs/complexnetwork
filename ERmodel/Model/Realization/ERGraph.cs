using System.Collections;

using model.ERModel.Realization;
using log4net;

namespace Model.ERModel.Realization
{
    public class ERGraph
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERGraph));

        //private int m_vertex_count;
        //private int m_edge_count; 

        private ERContainer m_container;
        private ERGenerator m_generator;
        public ERAnalyzer m_analyzer;

        public ERGraph(int vcount)
        {
            log.Info("Creating ERGraph object with given vertex count");
            //m_vertex_count = vcount;
            m_container = new ERContainer(vcount);
            m_generator = new ERGenerator(m_container);
            m_analyzer = new ERAnalyzer(m_container);
        }

        public ERGraph(ArrayList m)
        {
            log.Info("Creating ERGraph object from given matrix");
            m_container = new ERContainer(m);
            m_analyzer = new ERAnalyzer(m_container);
        }

        public void Generate(double p)
        {
            //log.Info("Generating ERGraph with given probability");
            m_generator.Generate(p);
            //m_container.FillContainerByProbability(p);
        }

        public ERContainer Container
        {
            get { return m_container; }
        }
    }
}
