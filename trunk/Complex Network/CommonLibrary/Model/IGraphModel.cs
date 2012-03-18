namespace RandomGraph.Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RandomGraph.Common.Model.Generation;

    /// Defines main interface for all graph models,
    /// Includes several methods for controlling graph model 
    /// computational processes
    public interface IGraphModel
    {
        /// Triggers analyze start process,
        /// and can be called only in case when 
        /// generation is fully completed
        void StartAnalize();

        /// Starts generation of graph model for separate 
        /// generation rule and for the first time of 
        /// sequential generation 
        void StartGenerate();

        /// Starts generation of graph model for sequential
        /// generation rule starting from second to the last model in the
        /// queue
        void StartGenerate(Object graphObj);
    }
}

