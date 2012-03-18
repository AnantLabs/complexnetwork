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

namespace Model.NonRegularHierarchicModel
{
    [TargetGraphModel(typeof(NonRegularHierarchicModel))]
    public class NonRegularHierarchicGraphFactory : AbstractGraphFactory
    {

        public NonRegularHierarchicGraphFactory() { }

        public NonRegularHierarchicGraphFactory(Dictionary<GenerationParam, object> genParam, AnalyseOptions analizeOptions)
            : base(genParam, analizeOptions)
        {

        }

        public override AbstractGraphModel CreateGraphModel(int sequenceNumber)
        {
            return new NonRegularHierarchicModel(GenerationParamValues, AnalizeOptions, sequenceNumber);
        }
    }
}
