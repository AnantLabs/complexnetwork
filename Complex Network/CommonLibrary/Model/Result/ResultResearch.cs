using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RandomGraph.Common.Model.Generation;

namespace CommonLibrary.Model.Result
{
    public class SubGraphsInfo
    {
        public double avgOrder;
        public double avgOrderCount;
        public double secondMax;
        public double secondMaxCount;
        public double avgOrderRest;
    }

    public class ResultResearch
    {
        public Guid ResearchID { get; set; }
        public string Name { get; set; }
        public Type ModelType { get; set; }
        public Dictionary<GenerationParam, object> GenerationParams { get; set; }
        public int RealizationCount { get; set; }
        public double Delta { get; set; }
        public string Function { get; set; }
        public int Size { get; set; }
        public SortedDictionary<double, SortedDictionary<double, SubGraphsInfo>> Result { get; set; }

        public ResultResearch()
        {
            this.ResearchID = Guid.NewGuid();
            this.GenerationParams = new Dictionary<GenerationParam, object>();
            this.Result = new SortedDictionary<double, SortedDictionary<double, SubGraphsInfo>>();
            this.Function = "Classical";
        }
    }
}
