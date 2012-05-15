﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using System.Runtime.Serialization;
using System.Collections;
using GenericAlgorithms;
using Model.HierarchicModel.Realization;

namespace Model.HierarchicModel
{

    [TargetGraphModel(typeof(HierarchicModel))]
    public class HierarchicGraphFactory : AbstractGraphFactory
    {

        public HierarchicGraphFactory() { }

        public HierarchicGraphFactory(Dictionary<GenerationParam, object> genParam, AnalyseOptions analizeOptions, Dictionary<String, Object> analizeOptionsValues)
            : base(genParam, analizeOptions,analizeOptionsValues)
        {

        }
        public override AbstractGraphModel CreateGraphModel(int sequenceNumber)
        {
            HierarchicModel model = new HierarchicModel(GenerationParamValues, AnalizeOptions, sequenceNumber);
            model.AnalizeOptionsValues = AnalizeOptionsValues;
            return model;
        }
    }
}