using log4net;

namespace Model.ERModel.Realization
{
    public class ERGenerator
    {        
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERGenerator));

        private ERContainer m_container;

        public ERGenerator(ERContainer c)
        {
            log.Info("Creating ERGenerator object");
            m_container = c;
        }

        public void Generate(double p)
        {
            log.Info("Generating ERGraph with given probability");
            m_container.FillContainerByProbability(p);
        }
    }
}
