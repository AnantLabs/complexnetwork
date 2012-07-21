using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RandomGraph.Common.Model.Generation;
using System.Runtime.Serialization;
using AnalyzerFramework.Manager.ModelRepo;

namespace RandomGraph.Common.Model
{
    /// <summary>
    /// Base class of graph model generation factory class.
    /// Used for secure graph model gerenation with use of AbstractFactory 
    /// design pattern
    /// </summary>
    [KnownType("GetKnownFactoryTypes")]
    public abstract class AbstractGraphFactory
    {
        public AbstractGraphFactory() { }
        /// <summary>
        /// Only constructor with imput parameters
        /// </summary>
        /// <param name="genParam">Generation params, that should be than passed to models</param>
        /// <param name="analizeOptions">Analyze options selected by user, again should
        /// be passed to model</param>
        public AbstractGraphFactory(Dictionary<GenerationParam, object> genParam, 
            AnalyseOptions analizeOptions, 
            Dictionary<String, Object> analizeOptionsValues)
        {
            GenerationParamValues = genParam;
            AnalizeOptions = analizeOptions;
            AnalizeOptionsValues = analizeOptionsValues;
        }

        /// <summary>
        /// Property for storing GenerationParamValues passed 
        /// to constructor
        /// </summary>
        public Dictionary<GenerationParam, object> GenerationParamValues { get; set; }

        /// <summary>
        /// Property for storing AnalyzeOptions passed 
        /// to constructor
        /// </summary>
        public AnalyseOptions AnalizeOptions { get; set; }

        /// <summary>
        /// Values of AnalyseOptions
        /// </summary>
        public Dictionary<String, Object> AnalizeOptionsValues { get; set; }

        /// <summary>
        /// Abstract method that should create instances of Graph
        /// models and returns them to caller.
        /// This method implementation should use AnalyzeOptions
        /// and GenerationParamsValues from this class for model creation.
        /// </summary>
        /// <param name="sequenceNumber">number in the assembly sequence</param>
        /// <returns>Instance of AbstractGraphModel implementation</returns>
        public abstract AbstractGraphModel CreateGraphModel(int sequenceNumber);

        private static Type[] GetKnownFactoryTypes()
        {
            Type[] types = new Type[5];
            types[0] = Type.GetType("HierarchicGraphFactory");
            types[1] = Type.GetType("HierarchicGraphFactory");
            types[2] = Type.GetType("BAGraphFactory");
            types[3] = Type.GetType("ERGraphGraphFactory");
            types[4] = Type.GetType("WSGraphGraphFactory");
            return ModelRepository.GetInstance().GetAvailableModelFactoryTypes().ToArray();
        }
    }
}
