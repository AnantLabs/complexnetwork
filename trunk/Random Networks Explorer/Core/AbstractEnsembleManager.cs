﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Result;

namespace Core
{
    /// <summary>
    /// Abstract class presenting ensemble manager.
    /// </summary>
    public abstract class AbstractEnsembleManager
    {
        protected int realizationsDone;

        public ModelType ModelType { protected get; set; }

        public string TracingPath { protected get; set; }

        public int RealizationCount { protected get; set; }

        public Dictionary<GenerationParameter, object> GenerationParameterValues { get; set; }

        public AnalyzeOption AnalyzeOptions { get; set; }

        public int RealizationsDone 
        {
            get { return realizationsDone; }
            protected set { realizationsDone = value; }
        }

        public EnsembleResult Result { get; protected set; }

        /// <summary>
        /// Runs generation, analyze and save for each realization in single ensemble.
        /// Blocks current thread until whole work completes.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Terminates running operations.
        /// </summary>
        public abstract void Cancel();
    }
}
