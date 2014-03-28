using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

using Core.Enumerations;

namespace Core.Model
{
    /// <summary>
    /// Interface for analyzer of a random network.
    /// </summary>
    public interface INetworkAnalyzer
    {
        /// <summary>
        /// Container of the generated graph.
        /// </summary>
        INetworkContainer Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Object CalculateOption(AnalyzeOption option);
    }
}
