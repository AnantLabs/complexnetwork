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

namespace Model.BAModel
{
    [TargetGraphModel(typeof(BAModel))]
    public class BAGraphFactory : AbstractGraphFactory
    {
        public BAGraphFactory() { }

        public BAGraphFactory(Dictionary<GenerationParam, object> genParam, AnalyseOptions analizeOptions)
            : base(genParam, analizeOptions)
        {

        }
        public override AbstractGraphModel CreateGraphModel(int sequenceNumber)
        {
                return new BAModel(GenerationParamValues, AnalizeOptions, sequenceNumber);
        }
    }
}