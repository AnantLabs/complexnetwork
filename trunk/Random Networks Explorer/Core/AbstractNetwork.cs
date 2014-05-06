using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;
using Core.Result;
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

        public bool SuccessfullyCompleted { get; private set; }
        public RealizationResult NetworkResult { get; protected set; }

        public AbstractNetwork(Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption analyzeOptions)
        {
            generationParameterValues = genParams;
            this.analyzeOptions = analyzeOptions;

            NetworkResult = new RealizationResult();
        }

        /// <summary>
        /// Generates random network from generation parameters.
        /// </summary>
        public void Generate()
        {
            try
            {
                if (generationParameterValues.ContainsKey(GenerationParameter.AdjacencyMatrixFile) &&
                    (generationParameterValues[GenerationParameter.AdjacencyMatrixFile] != null))
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
            
            try
            {
                Array existingOptions = Enum.GetValues(typeof(AnalyzeOption));
                foreach (AnalyzeOption opt in existingOptions)
                {
                    if ((analyzeOptions & opt) == opt)
                    {
                        NetworkResult.Result.Add(opt, networkAnalyzer.CalculateOption(opt));
                    }
                }

                SuccessfullyCompleted = true;
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
