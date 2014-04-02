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
    public class RealizationResult
    {
        public Dictionary<AnalyzeOption, object> Result { get; set; }

        public RealizationResult()
        {
            Result = new Dictionary<AnalyzeOption, object>();
        }
    }
}
