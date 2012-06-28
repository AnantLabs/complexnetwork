using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using AnalyzerFramework.Manager.ModelRepo;

using CommonLibrary.Model.Result;
using CommonLibrary.Model.Attributes;

using RandomGraph.Common.Storage;
using RandomGraph.Common.Model.Generation;

using ResultStorage.Storage;

namespace StatisticAnalyzer
{
    public class StLoader
    {
        // Static Members //
        public static Dictionary<string, Tuple<Type, Type>> models;
        public static object[] GetAvailableModelNames()
        {
            models = new Dictionary<string, Tuple<Type, Type>>();
            List<Type> availableModelFactoryTypes = ModelRepository.GetInstance().GetAvailableModelFactoryTypes();
            foreach (Type modelFactoryType in availableModelFactoryTypes)
            {
                object[] attr = modelFactoryType.GetCustomAttributes(typeof(TargetGraphModel), false);
                TargetGraphModel targetGraphMetadata = (TargetGraphModel)attr[0];
                Type modelType = targetGraphMetadata.GraphModelType;

                attr = modelType.GetCustomAttributes(typeof(GraphModel), false);
                string modelName = ((GraphModel)attr[0]).Name;

                models.Add(modelName, Tuple.Create<Type, Type>(modelFactoryType, modelType));
            }

            return models.Keys.ToArray();
        }
        
        // Object Members //
        private IResultStorage resultStorage; 
        private List<ResultAssembly> assemblies;
        private List<string> assembliesID;
 
        private string modelName = "";

        public StLoader()
        {
            InitStorage();
            InitAssemblies();
        }

        public List<ResultAssembly> Assemblies
        {
            get { return assemblies; }
        }

        public string ModelName
        {
            set
            {
                modelName = models[value].Item2.Name;
            }
        }

        public void InitStorage()
        {
            string provider = ConfigurationManager.AppSettings["Storage"];
            if (provider == "XmlProvider")
            {
                resultStorage = new XMLResultStorage(ConfigurationManager.AppSettings[provider]);
            }
            else
            {
                resultStorage = new SQLResultStorage(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[provider]]);
            }
        }

        public void InitAssemblies()
        {
            assemblies = resultStorage.LoadAssembliesByModel(modelName);
            InitAssembliesID();
        }

        public object[] GetAvailableJobs()
        {
            InitAssemblies();

            ArrayList result = new ArrayList();
            foreach (ResultAssembly r in assemblies)
                result.Add(r.Name);
            return result.ToArray();
        }

        public void DeleteJob(string jobName)
        {
            InitAssemblies();

            ResultAssembly result = assemblies.Find(i => i.Name == jobName);
            resultStorage.Delete(result.ID);
            InitAssemblies();
        }

        public string GetParameterValue(string jobName, GenerationParam p)
        {
            try
            {
                ResultAssembly result = resultStorage.Load(assemblies.Find(i => i.Name == jobName).ID);
                return result.GenerationParams[p].ToString();
            }
            catch
            {
                return "Generation Parameter Error!";
            }
        }

        public List<string> GetParameterValues(GenerationParam p)
        {
            List<string> result = new List<string>();
            foreach (string resultName in assembliesID)
            {
                ResultAssembly r = resultStorage.Load(assemblies.Find(i => i.Name == resultName).ID);
                result.Add(r.GenerationParams[p].ToString());
            }
            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        public List<string> GetParameterValues(Dictionary<GenerationParam, string> values,
            GenerationParam parameter)
        {
            List<string> result = new List<string>();
            foreach (string resultName in assembliesID)
            {
                ResultAssembly r = resultStorage.Load(assemblies.Find(i => i.Name == resultName).ID);
                Dictionary<GenerationParam, string>.KeyCollection keys = values.Keys;
                bool b = true;
                foreach (GenerationParam key in keys)
                {
                    b = b && (r.GenerationParams[key].ToString() == values[key]);
                }
                if (b)
                    result.Add(r.GenerationParams[parameter].ToString());
            }
            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        public ResultAssembly SelectAssemblyByJob(string jobName)
        {
            return resultStorage.Load(assemblies.Find(i => i.Name == jobName).ID);
        }

        public ResultAssembly SelectAssemblyByParameters(Dictionary<GenerationParam, string> values)
        {
            foreach (string resultName in assembliesID)
            {
                ResultAssembly result = resultStorage.Load(assemblies.Find(i => i.Name == resultName).ID);
                
                Dictionary<GenerationParam, string>.KeyCollection keys = values.Keys;
                bool b = true;
                foreach (GenerationParam key in keys)
                {
                    b = b && (result.GenerationParams[key].ToString() == values[key]);
                }
                if (b)
                    return result;
            }
            
            throw new SystemException("There is no assembly with these values!");
        }

        // Utilities //

        private void InitAssembliesID()
        {
            assembliesID = new List<string>();
            foreach (ResultAssembly result in assemblies)
                assembliesID.Add(result.Name);
        }
    }
}
