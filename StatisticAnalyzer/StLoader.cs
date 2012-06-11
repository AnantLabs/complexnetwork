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
        private IResultStorage resultStorage;
        private List<ResultAssembly> assemblies;
        private List<string> assembliesID;
        private string modelName = "";

        private StatAnalyzeParameters statAnalyzeParameters = new StatAnalyzeParameters();
        private List<GenerationParam> generationParams = new List<GenerationParam>();

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
                // CHECK THIS SWITCH //
                switch (value)
                {
                    case "Barabasi-Albert":
                        {
                            modelName = "BAModel";
                            InitializeBAModel();
                            break;
                        }
                    case "Watts-Strogatz":
                        {
                            modelName = "WSModel";
                            InitializeWSModel();
                            break;
                        }
                    case "ERModel":
                        {
                            modelName = "ERModel";
                            InitializeERModel();
                            break;
                        }
                    case "Block-Hierarchic":
                        {
                            modelName = "HierarchicModel";
                            InitializeHierarchicModel();
                            break;
                        }
                    case "Block-Hierarchic Parizi":
                        {
                            modelName = "HierarchicModelParizi";
                            InitializeHierarchicModel();
                            break;
                        }
                    case "Non Regular Block Hierarchic Model":
                        {
                            modelName = "Non regular HierarchicModel";
                            InitializeHierarchicModel();
                            break;
                        }
                    case "Static Model":
                        {
                            modelName = "StaticModel";
                            // initialization
                            break;
                        }
                }
            }
        }

        public StatAnalyzeParameters StatAnalyzeParameters
        {
            get { return statAnalyzeParameters; }
        }

        public static object[] GetAvailableModelNames()
        {
            ArrayList result = new ArrayList();
            List<Type> availableModelFactoryTypes = ModelRepository.GetInstance().GetAvailableModelFactoryTypes();
            foreach (Type modelFactoryType in availableModelFactoryTypes)
            {
                object[] attr = modelFactoryType.GetCustomAttributes(typeof(TargetGraphModel), false);
                TargetGraphModel targetGraphMetadata = (TargetGraphModel)attr[0];
                Type modelType = targetGraphMetadata.GraphModelType;

                attr = modelType.GetCustomAttributes(typeof(GraphModel), false);
                string modelName = ((GraphModel)attr[0]).Name;

                result.Add(modelName);
            }
            return result.ToArray();
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

        public string GetParameterValue(string name, int id)
        {
            ResultAssembly result = resultStorage.Load(assemblies.Find(i => i.Name == name).ID);

            switch (id)
            {
                case 1:
                    {
                        return result.GenerationParams[generationParams[0]].ToString();
                    }
                case 2:
                    {
                        return result.GenerationParams[generationParams[1]].ToString();
                    }
                case 3:
                    {
                        if (modelName != "BAModel" && modelName != "ERModel")
                            return result.GenerationParams[generationParams[2]].ToString();
                        else
                            return "";
                    }
                default:
                    break;
            }

            return "";
        }

        public List<string> GetParameterValues()
        {
            List<string> result = new List<string>();
            foreach (string resultName in assembliesID)
            {
                ResultAssembly r = resultStorage.Load(assemblies.Find(i => i.Name == resultName).ID);
                result.Add(r.GenerationParams[generationParams[0]].ToString());
            }
            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        public List<string> GetParameterValues(string param1)
        {
            if (generationParams.Count() >= 2)
            {
                Dictionary<GenerationParam, string> d = new Dictionary<GenerationParam, string>();
                d.Add(generationParams[0], param1);
                return GetValues(d, generationParams[1]);
            }
            else
                return new List<string>();
        }

        public List<string> GetParameterValues(string param1, string param2)
        {
            if (generationParams.Count() == 3)
            {
                Dictionary<GenerationParam, string> d = new Dictionary<GenerationParam, string>();
                d.Add(generationParams[0], param1);
                d.Add(generationParams[1], param2);
                return GetValues(d, generationParams[2]);
            }
            else
                return new List<string>();
        }

        public ResultAssembly SelectAssemblyByJob(string jobName)
        {
            return resultStorage.Load(assemblies.Find(i => i.Name == jobName).ID);
        }

        // CORRECT FOR EACH MODEL //
        public ResultAssembly SelectAssemblyByParameters()
        {
            return new ResultAssembly();
        }

        // Utilities //

        private void InitAssembliesID()
        {
            assembliesID = new List<string>();
            foreach (ResultAssembly result in assemblies)
                assembliesID.Add(result.Name);
        }

        private void InitializeHierarchicModel()
        {
            generationParams.Clear();
            generationParams.Add(GenerationParam.BranchIndex);
            generationParams.Add(GenerationParam.Level);
            generationParams.Add(GenerationParam.Mu);

            statAnalyzeParameters.m_param1 = "Hierarchic Base";
            statAnalyzeParameters.m_param2 = "Block Degree";
            statAnalyzeParameters.m_param3 = "Lambda Parameter";
        }

        private void InitializeBAModel()
        {
            generationParams.Clear();
            generationParams.Add(GenerationParam.Vertices);
            generationParams.Add(GenerationParam.MaxEdges);

            statAnalyzeParameters.m_param1 = "Initial Count of Vertices";
            statAnalyzeParameters.m_param2 = "Maximal Connections";
            statAnalyzeParameters.m_param3 = "";
        }

        private void InitializeWSModel()
        {
            generationParams.Clear();
            generationParams.Add(GenerationParam.Vertices);
            generationParams.Add(GenerationParam.Edges);
            generationParams.Add(GenerationParam.P);

            statAnalyzeParameters.m_param1 = "Number Of Vertices";
            statAnalyzeParameters.m_param2 = "Number Of Edges";
            statAnalyzeParameters.m_param3 = "Probability";
        }

        private void InitializeERModel()
        {
            generationParams.Clear();
            generationParams.Add(GenerationParam.Vertices);
            generationParams.Add(GenerationParam.P);

            statAnalyzeParameters.m_param1 = "Number Of Vertices";
            statAnalyzeParameters.m_param2 = "Probability";
            statAnalyzeParameters.m_param3 = "";
        }

        private List<string> GetValues(Dictionary<GenerationParam, string> values,
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
    }
}
