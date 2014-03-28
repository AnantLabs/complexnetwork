using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;

namespace Core
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractNetwork
    {
        protected INetworkGenerator networkGenerator;
        protected INetworkAnalyzer networkAnalyzer;

        protected Dictionary<GenerationParameter, object> generationParameterValues;
        protected AnalyzeOption analyzeOptions;
        protected string tracingPath;

        public AbstractNetwork(Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption analyzeOpts, string trPath)
        {
            generationParameterValues = genParams;
            analyzeOptions = analyzeOpts;
            tracingPath = trPath;
        }

        public void Generate()
        {
            try
            {
                if (generationParameterValues.ContainsKey(GenerationParameter.AdjacencyMatrixFile))
                {
                    // TODO read matrix from file.
                    ArrayList matrix = new ArrayList();
                    networkGenerator.StaticGeneration(matrix);
                }
                else
                {
                    networkGenerator.RandomGeneration(generationParameterValues);
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Analyze()
        {
            networkAnalyzer.Container = networkGenerator.Container;
            
            // TODO get analyze results.
            try
            {
                Array existingOptions = Enum.GetValues(typeof(AnalyzeOption));
                foreach (AnalyzeOption opt in existingOptions)
                {
                    if ((analyzeOptions & opt) == opt)
                    {
                        networkAnalyzer.CalculateOption(opt);
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Trace()
        {
        }
    }
}
