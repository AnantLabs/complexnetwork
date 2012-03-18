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
using log4net;

namespace Model.ERModel
{
    [TargetGraphModel(typeof(ERModel))]
    public class ERGraphFactory : AbstractGraphFactory
    {
        public ERGraphFactory() { }

        public ERGraphFactory(Dictionary<GenerationParam, object> genParam, AnalyseOptions analizeOptions)
            : base(genParam, analizeOptions)
        {
        }
        public override AbstractGraphModel CreateGraphModel(int sequenceNumber)
        {
                return new ERModel(GenerationParamValues, AnalizeOptions, sequenceNumber);
        }
    }
}