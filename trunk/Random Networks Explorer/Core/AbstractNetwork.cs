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
        public Dictionary<ResearchParameter, object> ResearchParameterValues { get; private set; }
        public Dictionary<GenerationParameter, object> GenerationParameterValues { get; private set; }
        public AnalyzeOption AnalyzeOptions { get; private set; }

        protected INetworkGenerator networkGenerator;
        protected INetworkAnalyzer networkAnalyzer;

        public bool SuccessfullyCompleted { get; private set; }
        public RealizationResult NetworkResult { get; protected set; }

        public AbstractNetwork(Dictionary<ResearchParameter, object> rParams,
            Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption AnalyzeOptions)
        {
            ResearchParameterValues = rParams;
            GenerationParameterValues = genParams;
            this.AnalyzeOptions = AnalyzeOptions;

            NetworkResult = new RealizationResult();
        }

        /// <summary>
        /// Generates random network from generation parameters.
        /// </summary>
        public void Generate()
        {
            try
            {
                if (GenerationParameterValues.ContainsKey(GenerationParameter.AdjacencyMatrixFile) &&
                    (GenerationParameterValues[GenerationParameter.AdjacencyMatrixFile] != null))
                {
                    string filePath = GenerationParameterValues[GenerationParameter.AdjacencyMatrixFile].ToString();
                    networkGenerator.StaticGeneration(FileManager.Read(filePath));
                }
                else
                {
                    networkGenerator.RandomGeneration(GenerationParameterValues);
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
                NetworkResult.NetworkSize = networkAnalyzer.Container.Size;

                Array existingOptions = Enum.GetValues(typeof(AnalyzeOption));
                foreach (AnalyzeOption opt in existingOptions)
                {
                    if ((AnalyzeOptions & opt) == opt)
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
            MatrixInfoToWrite matrixInfo = new MatrixInfoToWrite();
            matrixInfo.Matrix = networkGenerator.Container.GetMatrix();
            if (networkGenerator.Container is AbstractHierarchicContainer)
                matrixInfo.Branches = (networkGenerator.Container as AbstractHierarchicContainer).GetBranches();
            
            FileManager.Write(matrixInfo, tracingPath);   
        }
    }
}
