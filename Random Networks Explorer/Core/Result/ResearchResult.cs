using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;

namespace Core.Result
{
    /// <summary>
    /// 
    /// </summary>
    public class ResearchResult
    {
        public Guid ResearchID { get; set; }
        public string ResearchName { get; set; }
        public ResearchType ResearchType { get; set; }
        public ModelType ModelType { get; set; }
        public int RealizationCount { get; set; }
        public UInt32 Size { get; set; }
        public DateTime Date { get; set; }

        public Dictionary<ResearchParameter, object> ResearchParameterValues { get; set; }
        public Dictionary<GenerationParameter, object> GenerationParameterValues { get; set; }

        public List<EnsembleResult> EnsembleResults { get; set; }

        public ResearchResult()
        {
            ResearchParameterValues = new Dictionary<ResearchParameter, object>();
            GenerationParameterValues = new Dictionary<GenerationParameter, object>();

            EnsembleResults = new List<EnsembleResult>();
        }
    }
}
