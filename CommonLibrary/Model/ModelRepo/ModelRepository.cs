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
        private static ModelRepository instance = null;
        private List<Type> modelFactoryTypes = new List<Type>();
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
                    if (type.IsSubclassOf(typeof(AbstractGraphFactory)))
                    {
                        object[] attr = type.GetCustomAttributes(typeof(TargetGraphModel), false);
                        if(attr.Length == 1)
                        {
                            modelFactoryTypes.Add(type);
                            object[] attributes = type.GetCustomAttributes(typeof(TargetGraphModel), false);
                            TargetGraphModel targetGraphMetadata = (TargetGraphModel)attr[0];
                            modelTypes.Add(targetGraphMetadata.GraphModelType);
                        }
                    }
                }
	        }
        }

        public static ModelRepository GetInstance()
        {
            if (instance != null)
            {
                return instance;
            }
            return new ModelRepository();
        }

        public List<Type> GetAvailableModelFactoryTypes()
        {
            return modelFactoryTypes;
        }

        public List<Type> GetAvailableModelTypes()
        {
            return modelTypes;
        }
    }
}
