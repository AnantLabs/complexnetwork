using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;
using Core.Utility;

namespace Core
{
    /// <summary>
    /// Abstract class presenting random network.
    /// </summary>
    public abstract class AbstractNetwork
    {
        protected INetworkGenerator networkGenerator;
        protected INetworkAnalyzer networkAnalyzer;

        protected Dictionary<GenerationParameter, object> generationParameterValues;
        protected AnalyzeOption analyzeOptions;

        public AbstractNetwork(Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption analyzeOptions)
        {
            generationParameterValues = genParams;
            analyzeOptions = analyzeOptions;
        }

        /// <summary>
        /// Generates random network from generation parameters.
        /// </summary>
        public void Generate()
        {
            try
            {
                if (generationParameterValues.ContainsKey(GenerationParameter.AdjacencyMatrixFile))
                {
                    string filePath = generationParameterValues[GenerationParameter.AdjacencyMatrixFile].ToString();
                    networkGenerator.StaticGeneration(FileManager.MatrixReader(filePath));
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

        /// <summary>
        /// Calculates specified analyze options values.
        /// </summary>
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
                        object o = networkAnalyzer.CalculateOption(opt);
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Traces the adjacency matrix of generated network to file.
        /// </summary>
        public void Trace(string tracingPath)
        {
            FileManager.MatrixWriter(networkGenerator.Container.GetMatrix(), tracingPath);
        }
    }
}
