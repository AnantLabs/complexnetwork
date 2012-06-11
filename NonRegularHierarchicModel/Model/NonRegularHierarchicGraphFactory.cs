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

        public NonRegularHierarchicGraphFactory(Dictionary<GenerationParam, object> genParam, AnalyseOptions analizeOptions, Dictionary<String, Object> analizeOptionsValues)
            : base(genParam, analizeOptions, analizeOptionsValues)
        {

        }
        /// <summary>
        /// Created Graphmodel instance form file. 
        /// </summary>
        /*public override void CreateInstanceFromFile()
        {
            throw new NotImplementedException();
        }*/

        /// <summary>
        /// Created random Graphmodel instance. 
        /// </summary>
        /*public override void CreateRandomInstance()
        {
            //mModel = new HierarchicModel(GenerationParamValues, AnalizeOptions, 0);
        }*/

        public override AbstractGraphModel CreateGraphModel(int sequenceNumber)
        {
            return new NonRegularHierarchicModel(GenerationParamValues, AnalizeOptions, sequenceNumber);
        }
    }
}
