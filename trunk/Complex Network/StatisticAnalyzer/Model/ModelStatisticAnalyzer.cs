using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using CommonLibrary.Model.Result;
using RandomGraph.Common.Storage;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.StatAnalyzer;
using StatisticAnalyzer.Methods;

namespace StatisticAnalyzer.Model
{
    public class ModelStatisticAnalyzer
    {
        // Statistic Analyze parameters for each model //
        public string m_modelName;
        public StatAnalyzeParameters m_statisticParameters;

        // Statistic Analyze parameters for each mode //
        public AnalyseOptions m_globalParams;
        public AnalyseOptions m_localParams;
        public AnalyseOptions m_motifParams;
        public ApproximationTypes m_approximationType;

        // Common Data //
        private string MODEL_NAME;  // duplication!
        private IResultStorage m_storage;
        private List<ResultAssembly> m_assemblies;
        private string m_jobName = "";
        private List<GenerationParam> m_generationParams;
        private bool m_byAllJobsOption;

        // Statistic Analyze results for each mode //
        private Dictionary<AnalyseOptions,
            SortedDictionary<double, double>> m_globalResults;
        private Dictionary<AnalyseOptions, double> m_globalAverages;
        private Dictionary<AnalyseOptions,
            SortedDictionary<double, double>> m_localResults;
        private Dictionary<AnalyseOptions,
            SortedDictionary<double, double>> m_motifResults;

        private AbstractMethod m_method;

        public ModelStatisticAnalyzer(string modelName)
        {
            m_modelName = modelName;

            m_statisticParameters = new StatAnalyzeParameters();

            m_globalResults = new Dictionary<AnalyseOptions, SortedDictionary<double, double>>();
            m_globalAverages = new Dictionary<AnalyseOptions, double>();
            m_localResults = new Dictionary<AnalyseOptions, SortedDictionary<double, double>>();
            m_motifResults = new Dictionary<AnalyseOptions, SortedDictionary<double, double>>();

            m_generationParams = new List<GenerationParam>();

            switch (m_modelName)
            {
                case "Block-Hierarchic":
                    {
                        InitializeHierarchicModel(false);
                        break;
                    }
                case "Block-Hierarchic Parizi":
                    {
                        InitializeHierarchicModel(true);
                        break;
                    }
                case "Barabasi-Albert":
                    {
                        InitializeBAModel();
                        break;
                    }
                case "Watts-Strogatz":
                    {
                        InitializeWSModel();
                        break;
                    }
                case "ERModel":
                    {
                        InitializeERModel();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        // Common methods //

        public void LoadAssemblies(IResultStorage storage)
        {
            m_storage = storage;
            m_assemblies = m_storage.LoadAssembliesByModel(MODEL_NAME);
        }

        public void DeleteJob(string name, IResultStorage storage)
        {
            ResultAssembly result = m_assemblies.Find(i => i.Name == name);
            m_storage.Delete(result.ID);
            this.LoadAssemblies(storage);
        }

        public void SetGenerationParameters(string param1, string param2, string param3)
        {
            switch (m_modelName)
            {
                case "Block-Hierarchic":
                case "Block-Hierarchic Parizi":
                    {
                        SetHierarchicParameters(param1, param2, param3);
                        break;
                    }
                case "Barabasi-Albert":
                    {
                        SetBAParameters(param1, param2);
                        break;
                    }
                case "Watts-Strogatz":
                    {
                        SetWSParameters(param1, param2, param3);
                        break;
                    }
                case "ERModel":
                    {
                        SetERParameters(param1, param2);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public void SetAnalyzeParameters(bool byAllJobs,
            Dictionary<AnalyseOptions, StatAnalyzeOptions> localOptions)
        {
            m_statisticParameters.m_byAllAssemblies = byAllJobs;
            m_statisticParameters.m_localAnalyzeOptions = localOptions;
        }

        public string GetParameterValue(string name, int id)
        {
            ResultAssembly result = m_storage.Load(m_assemblies.Find(i => i.Name == name).ID);

            switch (id)
            {
                case 1:
                    {
                        return result.GenerationParams[m_generationParams[0]].ToString();
                    }
                case 2:
                    {
                        return result.GenerationParams[m_generationParams[1]].ToString();
                    }
                case 3:
                    {
                        if (m_modelName != "Barabasi-Albert" && m_modelName != "ERModel")
                            return result.GenerationParams[m_generationParams[2]].ToString();
                        else
                            return "";
                    }
                default:
                    break;
            }

            return "";
        }

        public string GetRealizationsCount(string name)
        {
            ResultAssembly result = m_storage.Load(m_assemblies.Find(i => i.Name == name).ID);
            return result.Results.Count.ToString();
        }

        public List<string> GetParameterValues()
        {
            m_method.RefreshParameters(m_storage, m_assemblies);
            return m_method.GetValues(m_generationParams[0]);
        }

        public List<string> GetParameterValues(string param1)
        {
            if (m_generationParams.Count() >= 2)
            {
                Dictionary<GenerationParam, string> d = new Dictionary<GenerationParam, string>();
                d.Add(m_generationParams[0], param1);
                return m_method.GetValues(d, m_generationParams[1]);
            }
            else
                return new List<string>();
        }

        public List<string> GetParameterValues(string param1, string param2)
        {
            if (m_generationParams.Count() == 3)
            {
                Dictionary<GenerationParam, string> d = new Dictionary<GenerationParam, string>();
                d.Add(m_generationParams[0], param1);
                d.Add(m_generationParams[1], param2);
                return m_method.GetValues(d, m_generationParams[2]);
            }
            else
                return new List<string>();
        }

        public void GlobalAnalyze()
        {
            m_method.RefreshParameters(m_storage, m_assemblies);
            m_method.Parameters = m_statisticParameters;
            m_method.SetSize();
            m_globalResults.Clear();
            m_globalAverages.Clear();

            ByGlobalOption(AnalyseOptions.AveragePath);
            ByGlobalOption(AnalyseOptions.Diameter);
            ByGlobalOption(AnalyseOptions.ClusteringCoefficient);
            ByGlobalOption(AnalyseOptions.DegreeDistribution);
            ByGlobalOption(AnalyseOptions.Cycles3);
            ByGlobalOption(AnalyseOptions.Cycles4);
            ByGlobalOption(AnalyseOptions.MaxFullSubgraph);
            ByGlobalOption(AnalyseOptions.LargestConnectedComponent);
            ByGlobalOption(AnalyseOptions.MinEigenValue);
            ByGlobalOption(AnalyseOptions.MaxEigenValue);
        }

        public void LocalAnalyze()
        {
            m_method.RefreshParameters(m_storage, m_assemblies);
            m_method.Parameters = m_statisticParameters;
            m_method.SetSize();
            m_localResults.Clear();

            ByLocalOption(AnalyseOptions.ClusteringCoefficient);
            ByLocalOption(AnalyseOptions.DegreeDistribution);
            ByLocalOption(AnalyseOptions.ConnSubGraph);
            ByLocalOption(AnalyseOptions.MinPathDist);
            ByLocalOption(AnalyseOptions.EigenValue);
            ByLocalOption(AnalyseOptions.DistEigenPath);
            ByLocalOption(AnalyseOptions.Cycles);
        }

        public void MotifAnalyze()
        {
            m_method.RefreshParameters(m_storage, m_assemblies);
            m_method.Parameters = m_statisticParameters;
            m_method.SetSize();
            m_motifResults.Clear();

            ByMotifOption(AnalyseOptions.Motifs);
        }

        public double LocalMathWaiting(AnalyseOptions p)
        {
            return m_method.GetLocalM(p);
        }

        public double LocalDispersion(AnalyseOptions p)
        {
            return m_method.GetLocalD(p);
        }

        public Dictionary<GenerationParam, string> GetParameterLine()
        {
            return m_method.GetParameterLine();
        }

        // Get/Set functions //
        public List<ResultAssembly> Assemblies
        {
            get { return m_assemblies; }
        }

        public string JobName
        {
            set { m_jobName = value; }
        }

        public Dictionary<AnalyseOptions, SortedDictionary<double, double>> GlobalResults
        {
            get { return m_globalResults; }
        }

        public Dictionary<AnalyseOptions, double> GlobalAverages
        {
            get { return m_globalAverages; }
        }

        public Dictionary<AnalyseOptions, SortedDictionary<double, double>> LocalResults
        {
            get { return m_localResults; }
        }

        public Dictionary<AnalyseOptions, SortedDictionary<double, double>> MotifResults
        {
            get { return m_motifResults; }
        }

        public StatAnalyzeParameters Parameters
        {
            set { m_statisticParameters = value; }
        }

        public bool ByAllJobsOptionValidation
        {
            get { return m_byAllJobsOption; }
        }

        // Utilities //

        private void InitializeHierarchicModel(bool isParizi)
        {
            MODEL_NAME = isParizi ? "HierarchicModelParizi" : "HierarchicModel";

            // Generation Params //
            m_generationParams.Clear();
            m_generationParams.Add(GenerationParam.BranchIndex);
            m_generationParams.Add(GenerationParam.Level);
            m_generationParams.Add(GenerationParam.Mu);

            m_byAllJobsOption = true;

            m_statisticParameters.m_param1 = "Hierarchic Base";
            m_statisticParameters.m_param2 = "Block Degree";
            m_statisticParameters.m_param3 = "Lambda Parameter";

            // Method
            m_method = new MethodForHierarchic();
        }

        private void InitializeBAModel()
        {
            MODEL_NAME = "BAModel";

            // Generation Params //
            m_generationParams.Clear();
            m_generationParams.Add(GenerationParam.Vertices);
            m_generationParams.Add(GenerationParam.MaxEdges);

            m_byAllJobsOption = false;

            m_statisticParameters.m_param1 = "Initial Count of Vertices";
            m_statisticParameters.m_param2 = "Maximal Connections";
            m_statisticParameters.m_param3 = "";

            // Method
            m_method = new MethodForBA();
        }

        private void InitializeWSModel()
        {
            MODEL_NAME = "WSModel";

            // Generation Params //
            m_generationParams.Clear();
            m_generationParams.Add(GenerationParam.Vertices);
            m_generationParams.Add(GenerationParam.Edges);
            m_generationParams.Add(GenerationParam.P);

            m_byAllJobsOption = false;

            m_statisticParameters.m_param1 = "Number Of Vertices";
            m_statisticParameters.m_param2 = "Number Of Edges";
            m_statisticParameters.m_param3 = "Probability";

            // Method
            m_method = new MethodForWS();
        }

        private void InitializeERModel()
        {
            MODEL_NAME = "ERModel";

            // Generation Params //
            m_generationParams.Clear();
            m_generationParams.Add(GenerationParam.Vertices);
            m_generationParams.Add(GenerationParam.P);

            m_byAllJobsOption = false;

            m_statisticParameters.m_param1 = "Number Of Vertices";
            m_statisticParameters.m_param2 = "Probability";
            m_statisticParameters.m_param3 = "";

            // Method
            m_method = new MethodForER();
        }

        private void SetHierarchicParameters(string param1, string param2, string param3)
        {
            m_statisticParameters.m_hierarchicBase = Convert.ToInt16(param1);
            m_statisticParameters.m_blockDegree = Convert.ToInt16(param2);
            m_statisticParameters.m_lambdaParam = Convert.ToDouble(param3);
        }

        private void SetBAParameters(string param1, string param2)
        {
            m_statisticParameters.m_initialCount = Convert.ToInt32(param1);
            m_statisticParameters.m_maximalConnections = Convert.ToInt16(param2);
        }

        private void SetWSParameters(string param1, string param2, string param3)
        {
            m_statisticParameters.m_numberOfVerticesWS = Convert.ToInt32(param1);
            m_statisticParameters.m_numberOfEdges = Convert.ToInt32(param2);
            m_statisticParameters.m_probabilityWS = Convert.ToDouble(param3);
        }

        private void SetERParameters(string param1, string param2)
        {
            m_statisticParameters.m_numberOfVerticesER = Convert.ToInt32(param1);
            m_statisticParameters.m_probabilityER = Convert.ToDouble(param2);
        }

        private void ByGlobalOption(AnalyseOptions sOption)
        {
            if ((m_globalParams & sOption) == sOption)
            {
                if (m_jobName == "")
                {
                    KeyValuePair<SortedDictionary<double, double>, double> v = m_method.GlobalAnalyzeByOption(sOption);
                    m_globalResults[sOption] = Approximate(v.Key);
                    m_globalAverages[sOption] = v.Value;
                }
                else
                {
                    KeyValuePair<SortedDictionary<double, double>, double> v = m_method.GlobalAnalyzeByOption(m_jobName, sOption);
                    m_globalResults[sOption] = Approximate(v.Key);
                    m_globalAverages[sOption] = v.Value;
                }
            }
        }

        private void ByLocalOption(AnalyseOptions sOption)
        {
            if ((m_localParams & sOption) == sOption)
            {
                if (m_jobName == "")
                    m_localResults[sOption] = Approximate(m_method.LocalAnalyzeByOption(sOption));
                else
                    m_localResults[sOption] = Approximate(m_method.LocalAnalyzeByOption(m_jobName, sOption));
            }
        }

        private void ByMotifOption(AnalyseOptions sOption)
        {
            if ((m_motifParams & sOption) == sOption)
            {
                if (m_jobName == "")
                    m_motifResults[sOption] = Approximate(m_method.MotifAnalyzeByOption(sOption));
                else
                    m_motifResults[sOption] = Approximate(m_method.MotifAnalyzeByOption(m_jobName, sOption));
            }
        }

        private SortedDictionary<double, double> Approximate(SortedDictionary<double, double> d)
        {
            SortedDictionary<double, double> result = new SortedDictionary<double, double>();
            SortedDictionary<double, double>.KeyCollection keys = d.Keys;

            switch (m_approximationType)
            {
                case ApproximationTypes.None:
                    {
                        result = d;
                        break;
                    }
                case ApproximationTypes.Degree:
                    {
                        foreach (double key in keys)
                        {
                            if (!result.ContainsKey(Math.Log(key)))
                                result.Add(Math.Log10(key), Math.Log10(d[key]));
                        }
                        break;
                    }
                case ApproximationTypes.Exponential:
                    {
                        foreach (double key in keys)
                        {
                            if (!result.ContainsKey(key))
                                result.Add(key, Math.Log10(d[key]));
                        }
                        break;
                    }
                case ApproximationTypes.Gaus:
                    {
                        foreach (double key in keys)
                        {
                            if (!result.ContainsKey(Math.Pow(key, 2)))
                                result.Add(Math.Pow(key, 2), Math.Log10(d[key]));
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return result;
        }
    }
}
