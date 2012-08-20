using System;
using System.Collections.Generic;
using System.Text;

using RandomGraph.Core.Events;
using RandomGraph.Common.Storage;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;

namespace RandomGraph.Core.Manager.Interface
{
    /// <summary>
    /// Interface for working with Graph managers,
    /// defines main methods for starting, stopping,
    /// pausing or resuming computation process execution
    /// </summary>
    public interface IGraphManager
    {
        /// <summary>
        /// Method  for stopping all active calculation and 
        /// free all used recources
        /// </summary>
        void Stop();

        /// <summary>
        /// Method  for stopping current active calculation and 
        /// free all used recources
        /// </summary>
        void Stop(int instanceID);

        /// <summary>
        /// Suspends active jobs and frees computation resources, but not memory.
        /// Resume of jobs could be triggered by calling Continue() method
        /// </summary>
        void Pause();

        /// <summary>
        /// Suspends current active jobs and frees computation resources, but not memory.
        /// Resume of jobs could be triggered by calling Continue() method
        /// </summary>
        void Pause(int instanceID);

        /// <summary>
        /// Continues all jobs that were suspended
        /// </summary>
        void Continue();

        /// <summary>
        /// Continues crrent jobs that were suspended
        /// </summary>
        void Continue(int instanceID);

        /// <summary>
        /// Starts computational process of graph models statistic properties
        /// building
        /// </summary>
        /// <param name="graphFactory">Factory of graph model that was choosen
        /// Used for generation models
        /// </param>
        /// <param name="iterations">Number of times that graph manager should create 
        /// instances of graph model</param>
        void Start(AbstractGraphModel graphModel, int iterations, string name);
    }
}

