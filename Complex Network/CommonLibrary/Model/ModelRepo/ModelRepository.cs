using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using RandomGraph.Common.Model;
using CommonLibrary.Model.Attributes;
using log4net;

namespace AnalyzerFramework.Manager.ModelRepo
{
    public class ModelRepository
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ModelRepository));
        private static ModelRepository instance = new ModelRepository();
        private List<Type> modelTypes = new List<Type>();

        private ModelRepository()
        {

            string dir = AppDomain.CurrentDomain.BaseDirectory + "Models";
            foreach (var dll in Directory.GetFiles(dir, "*.dll"))
	        {
                Assembly asm = Assembly.LoadFile(dll);
                Type[] types = asm.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(AbstractGraphModel)))
                    {
                            modelTypes.Add(type);
                    }
                }
	        }
        }

        public static ModelRepository GetInstance()
        {
                return instance;
        }

        public List<Type> GetAvailableModelTypes()
        {
            return modelTypes;
        }
    }
}
