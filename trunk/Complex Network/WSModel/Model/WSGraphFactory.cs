using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using System.Collections;

namespace Model.WSModel
{
    [TargetGraphModel(typeof(WSModel))]
    public class WSGraphFactory : AbstractGraphFactory
    {
        public WSGraphFactory() { }

        public WSGraphFactory(Dictionary<GenerationParam, object> genParam, AnalyseOptions analizeOptions, Dictionary<String, Object> analizeOptionsValues)
            : base(genParam, analizeOptions, analizeOptionsValues)
        {

        }
        public override AbstractGraphModel CreateGraphModel(int sequenceNumber)
        {
            return new WSModel(GenerationParamValues, AnalizeOptions, sequenceNumber);
        }
    }
}
