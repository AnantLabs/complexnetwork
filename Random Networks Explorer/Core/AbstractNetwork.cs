using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Exceptions;
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
        public String ResearchName { get; private set; }
        public Dictionary<ResearchParameter, object> ResearchParameterValues { get; private set; }
        public Dictionary<GenerationParameter, object> GenerationParameterValues { get; private set; }
        public AnalyzeOption AnalyzeOptions { get; private set; }

        protected INetworkGenerator networkGenerator;
        protected INetworkAnalyzer networkAnalyzer;

        public int NetworkID { get; set; }
        public bool SuccessfullyCompleted { get; private set; }
        public RealizationResult NetworkResult { get; protected set; }

        public event NetworkStatusUpdateHandler OnUpdateStatus;

        public AbstractNetwork(String rName,
            Dictionary<ResearchParameter, object> rParams,
            Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption AnalyzeOptions)
        {
            ResearchName = rName;
            ResearchParameterValues = rParams;
            GenerationParameterValues = genParams;
            this.AnalyzeOptions = AnalyzeOptions;

            NetworkResult = new RealizationResult();
        }

        /// <summary>
        /// Generates random network from generation parameters.
        /// </summary>
        public bool Generate()
        {
            UpdateStatus(NetworkStatus.NotStarted, "Not started.");

            try
            {
                UpdateStatus(NetworkStatus.Generating, "Generating.");

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

                UpdateStatus(NetworkStatus.GenerationCompleted, "Generation Completed.");
            }
            catch (MatrixFormatException mEx)
            {
                UpdateStatus(NetworkStatus.Failed, "Generation Failed. " + mEx.Message);
                return false;
            }
            catch (BranchesFormatException bEx)
            {
                UpdateStatus(NetworkStatus.Failed, "Generation Failed. " + bEx.Message);
                return false;
            }
            catch(ApplicationException)
            {
                UpdateStatus(NetworkStatus.Failed, "Generation Failed.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calculates specified analyze options values.
        /// </summary>
        public bool Analyze()
        {
            networkAnalyzer.Container = networkGenerator.Container;
            
            try
            {
                UpdateStatus(NetworkStatus.Analyzing, "Analyzing.");

                NetworkResult.NetworkSize = networkAnalyzer.Container.Size;
                NetworkResult.EdgesCount = networkAnalyzer.CalculateEdgesCount();

                Array existingOptions = Enum.GetValues(typeof(AnalyzeOption));
                foreach (AnalyzeOption opt in existingOptions)
                {
                    if (opt != AnalyzeOption.None && (AnalyzeOptions & opt) == opt)
                    {
                        UpdateStatus(NetworkStatus.Analyzing, 
                            "Calculating " + opt.ToString() + ".");

                        NetworkResult.Result.Add(opt, networkAnalyzer.CalculateOption(opt));
                    }
                }

                SuccessfullyCompleted = true;

                UpdateStatus(NetworkStatus.AnalyzingCompleted, "Analyzing Completed.");
            }
            catch (SystemException ex)
            {
                UpdateStatus(NetworkStatus.Failed, "Analyzing Failed.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Traces the adjacency matrix of generated network to file.
        /// </summary>
        public bool Trace(string tracingPath)
        {
            try
            {
                UpdateStatus(NetworkStatus.Tracing, "Tracing.");

                MatrixInfoToWrite matrixInfo = new MatrixInfoToWrite();
                matrixInfo.Matrix = networkGenerator.Container.GetMatrix();
                if (networkGenerator.Container is AbstractHierarchicContainer)
                    matrixInfo.Branches = (networkGenerator.Container as AbstractHierarchicContainer).GetBranches();

                FileManager.Write(matrixInfo, tracingPath);

                UpdateStatus(NetworkStatus.TracingCompleted, "Tracing Completed.");
            }
            catch (SystemException ex)
            {
                UpdateStatus(NetworkStatus.Failed, "Tracing Failed.");
                return false;
            }

            return true;
        }

        private void UpdateStatus(NetworkStatus status, string extendedInfo)
        {
            // Make sure someone is listening to event
            if (OnUpdateStatus == null) 
                return;

            // Invoke event for AbstractEnsembleManager
            OnUpdateStatus(this, new NetworkEventArgs(NetworkID, status, extendedInfo));
        }
    }
}
