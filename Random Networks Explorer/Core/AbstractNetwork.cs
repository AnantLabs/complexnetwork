using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Events;
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

        public event NetworkStatusUpdateHandler OnUpdateStatus;

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
                UpdateStatus(RealizationStatus.Generating, "Generating.");

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

                UpdateStatus(RealizationStatus.GenerationCompleted, "Generation Completed.");
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);

                UpdateStatus(RealizationStatus.Failed, "Generation Failed.");
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
                UpdateStatus(RealizationStatus.Analyzing, "Analyzing.");

                NetworkResult.NetworkSize = networkAnalyzer.Container.Size;

                Array existingOptions = Enum.GetValues(typeof(AnalyzeOption));
                foreach (AnalyzeOption opt in existingOptions)
                {
                    if (opt != AnalyzeOption.None && (AnalyzeOptions & opt) == opt)
                    {
                        UpdateStatus(RealizationStatus.Analyzing, 
                            "Calculating " + opt.ToString() + ".");

                        NetworkResult.Result.Add(opt, networkAnalyzer.CalculateOption(opt));
                    }
                }

                SuccessfullyCompleted = true;

                UpdateStatus(RealizationStatus.AnalyzingCompleted, "Analyzing Completed.");
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);

                UpdateStatus(RealizationStatus.Failed, "Analyzing Failed.");
            }
        }

        /// <summary>
        /// Traces the adjacency matrix of generated network to file.
        /// </summary>
        public void Trace(string tracingPath)
        {
            try
            {
                UpdateStatus(RealizationStatus.Tracing, "Tracing.");

                MatrixInfoToWrite matrixInfo = new MatrixInfoToWrite();
                matrixInfo.Matrix = networkGenerator.Container.GetMatrix();
                if (networkGenerator.Container is AbstractHierarchicContainer)
                    matrixInfo.Branches = (networkGenerator.Container as AbstractHierarchicContainer).GetBranches();

                FileManager.Write(matrixInfo, tracingPath);

                UpdateStatus(RealizationStatus.TracingCompleted, "Tracing Completed.");
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);

                UpdateStatus(RealizationStatus.Failed, "Tracing Failed.");
            }
        }

        private void UpdateStatus(RealizationStatus status, string extendedInfo)
        {
            // Make sure someone is listening to event
            if (OnUpdateStatus == null) 
                return;

            OnUpdateStatus(this, new NetworkEventArgs(status, extendedInfo));
        }
    }
}
