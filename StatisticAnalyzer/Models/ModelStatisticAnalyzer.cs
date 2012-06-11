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
        public Dictionary<StatAnalyzeGlobalParameters, bool> m_existingGlobalOptions;
        public Dictionary<StatAnalyzeLocalParameters, bool> m_existingLocalOptions;

        // Statistic Analyze parameters for each mode //
        public StatAnalyzeGlobalParameters m_globalParams;
        public StatAnalyzeLocalParameters m_localParams;
        public ApproximationTypes m_approximationType;

        // Common Data //
        private string MODEL_NAME;  // duplication!
        private IResultStorage m_storage;
        private List<ResultAssembly> m_assemblies;
        private string m_jobName = "";

        // Statistic Analyze results for each mode //
        private Dictionary<StatAnalyzeGlobalParameters,
            SortedDictionary<double, double>> m_globalResults;
        private Dictionary<StatAnalyzeGlobalParameters, double> m_globalAverages;
        private Dictionary<StatAnalyzeLocalParameters,
            SortedDictionary<double, double>> m_localResults;

        private AbstractMethod m_method;

        public ModelStatisticAnalyzer(string modelName)
        {
            m_modelName = modelName;

            m_statisticParameters = new StatAnalyzeParameters();

            m_existingGlobalOptions = new Dictionary<StatAnalyzeGlobalParameters, bool>();
            m_existingLocalOptions = new Dictionary<StatAnalyzeLocalParameters, bool>();

            m_globalResults = new Dictionary<StatAnalyzeGlobalParameters,SortedDictionary<double,double>>();
            m_globalAverages = new Dictionary<StatAnalyzeGlobalParameters,double>();
            m_localResults = new Dictionary<StatAnalyzeLocalParameters,SortedDictionary<double,double>>();

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

        public void SetGenerationParameters(Dictionary<GenerationParam, object> parameters)
        {
            List<object> p = new List<object>();
            Dictionary<GenerationParam, object>.KeyCollection keys = parameters.Keys;
            foreach (GenerationParam key in keys)
                p.Add(parameters[key]);

            m_method.SetParameters(p);
        }

        public List<object> GetParameterValues(GenerationParam parameter)
        {
            m_method.RefreshParameters(m_storage, m_assemblies);
            return m_method.GetValues(parameter);
        }

        public List<object> GetParameterValues(Dictionary<GenerationParam, object> values,
            GenerationParam parameter)
        {
            m_method.RefreshParameters(m_storage, m_assemblies);
            return m_method.GetValues(values, parameter);
        }

        public void GlobalAnalyze()
        {
            m_method.RefreshParameters(m_storage, m_assemblies);
            m_method.Parameters = m_statisticParameters;
            m_globalResults.Clear();
            m_globalAverages.Clear();

            ByGlobalOption(StatAnalyzeGlobalParameters.AveragePathLength, AnalyseOptions.AveragePath);
            ByGlobalOption(StatAnalyzeGlobalParameters.Diameter, AnalyseOptions.Diameter);
            ByGlobalOption(StatAnalyzeGlobalParameters.ClusteringCoefficient, AnalyseOptions.ClusteringCoefficient);
            ByGlobalOption(StatAnalyzeGlobalParameters.DegreeDistribution, AnalyseOptions.DegreeDistribution);
            ByGlobalOption(StatAnalyzeGlobalParameters.Cycles3, AnalyseOptions.Cycles3);
            ByGlobalOption(StatAnalyzeGlobalParameters.Cycles4, AnalyseOptions.Cycle4);
            ByGlobalOption(StatAnalyzeGlobalParameters.MaxSubgraph, AnalyseOptions.FullSubGraph);   // AnalyseOption is not correct
        }

        public void LocalAnalyze()
        {
            m_method.RefreshParameters(m_storage, m_assemblies);
            m_method.Parameters = m_statisticParameters;
            m_localResults.Clear();

            ByLocalOption(StatAnalyzeLocalParameters.ClusteringCoefficient, AnalyseOptions.ClusteringCoefficient);
            ByLocalOption(StatAnalyzeLocalParameters.DegreeDistribution, AnalyseOptions.DegreeDistribution);
            ByLocalOption(StatAnalyzeLocalParameters.ConnectedSubgraphsByOrders, AnalyseOptions.FullSubGraph);
        }

        public double MathWaiting(StatAnalyzeLocalParameters p)
        {
            return m_method.CountM(p);
        }

        public double Dispersion(StatAnalyzeLocalParameters p)
        {
            return m_method.CountD(p);
        }

        public string GetParameterLine()
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

        public Dictionary<StatAnalyzeGlobalParameters, SortedDictionary<double, double>> GlobalResults
        {
            get { return m_globalResults; }
        }

        public Dictionary<StatAnalyzeGlobalParameters, double> GlobalAverages
        {
            get { return m_globalAverages; }
        }

        public Dictionary<StatAnalyzeLocalParameters, SortedDictionary<double, double>> LocalResults
        {
            get { return m_localResults; }
        }

        public StatAnalyzeParameters Parameters
        {
            set { m_statisticParameters = value; }
        }

        // Utilities //

        private void InitializeHierarchicModel(bool isParizi)
        {
            MODEL_NAME = isParizi ? "HierarchicModelParizi" : "HierarchicModel";

            m_statisticParameters.m_param1 = "Hierarchic Base";
            m_statisticParameters.m_param2 = "Block Degree";
            m_statisticParameters.m_param3 = "Lambda Parameter";

            // Global Options //
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.AveragePathLength] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Diameter] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.ClusteringCoefficient] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.DegreeDistribution] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Cycles3] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Cycles4] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.MaxSubgraph] = false;

            // Local Options
            m_existingLocalOptions[StatAnalyzeLocalParameters.ClusteringCoefficient] = true;
            m_existingLocalOptions[StatAnalyzeLocalParameters.DegreeDistribution] = true;
            m_existingLocalOptions[StatAnalyzeLocalParameters.ConnectedSubgraphsByOrders] = true;

            // Method
            m_method = new SequentialMethodForHierarchic();
        }

        private void InitializeBAModel()
        {
            MODEL_NAME = "BAModel";

            m_statisticParameters.m_param1 = "Initial Count of Vertices";
            m_statisticParameters.m_param2 = "Maximal Connections";
            m_statisticParameters.m_param3 = "";

            // Global Options //
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.AveragePathLength] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Diameter] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.ClusteringCoefficient] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.DegreeDistribution] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Cycles3] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Cycles4] = true;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.MaxSubgraph] = true;

            // Local Options
            m_existingLocalOptions[StatAnalyzeLocalParameters.ClusteringCoefficient] = false;
            m_existingLocalOptions[StatAnalyzeLocalParameters.DegreeDistribution] = true;
            m_existingLocalOptions[StatAnalyzeLocalParameters.ConnectedSubgraphsByOrders] = false;

            // Method
            m_method = new SequentialMethodForBA();
        }

        private void InitializeWSModel()
        {
            MODEL_NAME = "WSModel";

            m_statisticParameters.m_param1 = "Parameter";
            m_statisticParameters.m_param2 = "Parameter";
            m_statisticParameters.m_param3 = "Parameter";

            // Global Options //
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.AveragePathLength] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Diameter] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.ClusteringCoefficient] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.DegreeDistribution] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Cycles3] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Cycles4] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.MaxSubgraph] = false;

            // Local Options
            m_existingLocalOptions[StatAnalyzeLocalParameters.ClusteringCoefficient] = false;
            m_existingLocalOptions[StatAnalyzeLocalParameters.DegreeDistribution] = false;
            m_existingLocalOptions[StatAnalyzeLocalParameters.ConnectedSubgraphsByOrders] = false;

            // Method
            // add
        }

        private void InitializeERModel()
        {
            MODEL_NAME = "ERModel";

            m_statisticParameters.m_param1 = "Parameter";
            m_statisticParameters.m_param2 = "Parameter";
            m_statisticParameters.m_param3 = "Parameter";

            // Global Options //
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.AveragePathLength] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Diameter] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.ClusteringCoefficient] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.DegreeDistribution] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Cycles3] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.Cycles4] = false;
            m_existingGlobalOptions[StatAnalyzeGlobalParameters.MaxSubgraph] = false;

            // Local Options
            m_existingLocalOptions[StatAnalyzeLocalParameters.ClusteringCoefficient] = false;
            m_existingLocalOptions[StatAnalyzeLocalParameters.DegreeDistribution] = false;
            m_existingLocalOptions[StatAnalyzeLocalParameters.ConnectedSubgraphsByOrders] = false;

            // Method
            // add
        }

        private void ByGlobalOption(StatAnalyzeGlobalParameters sOption, AnalyseOptions option)
        {
            if ((m_globalParams & sOption) == sOption)
            {
                if (m_jobName == "")
                {
                    KeyValuePair<SortedDictionary<double, double>, double> v = m_method.GlobalAnalyzeByOption(option);
                    m_globalResults[sOption] = Approximate(v.Key);
                    m_globalAverages[sOption] = v.Value;
                }
                else
                {
                    KeyValuePair<SortedDictionary<double, double>, double> v = m_method.GlobalAnalyzeByOption(m_jobName, option);
                    m_globalResults[sOption] = Approximate(v.Key);
                    m_globalAverages[sOption] = v.Value;
                }
            }
        }

        private void ByLocalOption(StatAnalyzeLocalParameters sOption, AnalyseOptions option)
        {
            if ((m_localParams & sOption) == sOption)
            {
                if (m_jobName == "")
                    m_localResults[sOption] = Approximate(m_method.LocalAnalyzeByOption(option));
                else
                    m_localResults[sOption] = Approximate(m_method.LocalAnalyzeByOption(m_jobName, option));
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
                            result.Add(Math.Log(key), Math.Log(d[key]));
                        }
                        break;
                    }
                case ApproximationTypes.Exponential:
                    {
                        foreach (double key in keys)
                        {
                            result.Add(key, Math.Log(d[key]));
                        }
                        break;
                    }
                case ApproximationTypes.Gaus:
                    {
                        foreach (double key in keys)
                        {
                            result.Add(Math.Pow(key, 2), Math.Log(d[key]));
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
