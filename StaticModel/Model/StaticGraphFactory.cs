using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using System.Runtime.Serialization;
using System.Collections;

namespace Model.StaticModel
{
    [TargetGraphModel(typeof(StaticModel))]
    public class StaticGraphFactory : AbstractGraphFactory
    {
        public StaticGraphFactory() { }

        public StaticGraphFactory(Dictionary<GenerationParam, object> genParam, AnalyseOptions analizeOptions, Dictionary<String, Object> analizeOptionsValues)
            : base(genParam, analizeOptions,analizeOptionsValues)
        {

        }
        public override AbstractGraphModel CreateGraphModel(int sequenceNumber)
        {
            return new StaticModel(GenerationParamValues, AnalizeOptions, sequenceNumber);
        }
    }
}