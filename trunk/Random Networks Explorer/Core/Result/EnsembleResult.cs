using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

using Core.Enumerations;
using Core.Attributes;

namespace Core.Result
{
    /// <summary>
    /// Represents the result of analyze for single Ensemble.
    /// </summary>
    public class EnsembleResult
    {
        public UInt32 NetworkSize { get; set; }
        public Double EdgesCount { get; set; }
        public Dictionary<AnalyzeOption, object> Result { get; set; }

        /// <summary>
        /// Averages specified results by realization count.
        /// </summary>
        /// <param name="results">List of results for realizations.</param>
        /// <returns>Ensemble result, which represents avereged values of realizations results.</returns>
        public static EnsembleResult AverageResults(List<RealizationResult> results)
        {
            EnsembleResult r = new EnsembleResult();
            r.NetworkSize = results[0].NetworkSize;
            r.Result = new Dictionary<AnalyzeOption, object>();

            double rCount = results.Count;
            foreach (RealizationResult res in results)
            {
                r.EdgesCount += res.EdgesCount;
            }
            r.EdgesCount /= rCount;

            foreach (AnalyzeOption option in results[0].Result.Keys)
            {
                AnalyzeOptionInfo[] info = (AnalyzeOptionInfo[])option.GetType().GetField(option.ToString()).GetCustomAttributes(typeof(AnalyzeOptionInfo), false);
                Type t = info[0].RealizationResultType;

                if(t.Equals(typeof(Double)))
                {
                    double temp = 0;
                    foreach (RealizationResult res in results)
                    {
                        temp += (double)(res.Result[option]) / rCount;
                    }
                    r.Result.Add(option, temp);
                }
                else if(t.Equals(typeof(UInt32)))
                {
                    double temp = 0;
                    uint uTemp = 0;
                    foreach (RealizationResult res in results)
                    {
                        uTemp = (UInt32)res.Result[option];
                        temp += (double)uTemp / rCount;
                    }
                    r.Result.Add(option, temp);
                }
                else if(t.Equals(typeof(List<Double>)))
                {
                    List<Double> temp = new List<double>(results[0].Result[option] as List<Double>);
                    for (int i = 0; i < temp.Count; ++i)
                        temp[i] /= rCount;

                    // TODO check the theory logic of averaging eigen values
                    /*for (int i = 1; i < results.Count; ++i)
                    {
                        List<Double> l = results[i].Result[option] as List<Double>;
                        for (int j = 0; j < l.Count; ++j)
                            temp[j] += l[j] / rCount;
                    }*/
                    r.Result.Add(option, temp);
                }
                else if (t.Equals(typeof(SortedDictionary<Double, UInt32>)))
                {
                    SortedDictionary<Double, Double> temp = new SortedDictionary<double, double>();
                    foreach (RealizationResult res in results)
                    {
                        SortedDictionary<Double, UInt32> d = res.Result[option] as SortedDictionary<Double, UInt32>;
                        foreach (double k in d.Keys)
                        {
                            if (temp.ContainsKey(k))
                                temp[k] += (double)d[k] / rCount;
                            else
                                temp.Add(k, (double)d[k] / rCount);
                        }
                    }
                    r.Result.Add(option, temp);
                }
                else if (t.Equals(typeof(SortedDictionary<UInt32, UInt32>)))
                {
                    SortedDictionary<UInt32, Double> temp = new SortedDictionary<uint, double>();
                    foreach (RealizationResult res in results)
                    {
                        SortedDictionary<UInt32, UInt32> d = res.Result[option] as SortedDictionary<UInt32, UInt32>;
                        foreach (uint k in d.Keys)
                        {
                            if (temp.ContainsKey(k))
                                temp[k] += (double)d[k] / rCount;
                            else
                                temp.Add(k, (double)d[k] / rCount);
                        }
                    }
                    r.Result.Add(option, temp);
                }
                else if (t.Equals(typeof(SortedDictionary<UInt32, Double>)))		
	            {		
	                SortedDictionary<UInt32, Double> temp = new SortedDictionary<uint, double>();		
	                foreach (RealizationResult res in results)		
	                {
                        SortedDictionary<UInt32, Double> d = res.Result[option] as SortedDictionary<UInt32, Double>;		
	                    foreach (uint k in d.Keys)		
	                    {		
	                        if (temp.ContainsKey(k))		
	                            temp[k] += (double)d[k] / rCount;		
	                        else		
	                            temp.Add(k, (double)d[k] / rCount);		
	                    }		
	                }		
	                r.Result.Add(option, temp);		
	            }		
	            else if (t.Equals(typeof(SortedDictionary<UInt16, Double>)))		
	            {		
	                SortedDictionary<UInt16, Double> temp = new SortedDictionary<UInt16, Double>();		
	                foreach (RealizationResult res in results)		
	                {
                        SortedDictionary<UInt16, Double> d = res.Result[option] as SortedDictionary<UInt16, Double>;		
	                    foreach (UInt16 k in d.Keys)		
	                    {		
	                        if (temp.ContainsKey(k))		
	                            temp[k] += (double)d[k] / rCount;		
	                        else		
	                            temp.Add(k, (double)d[k] / rCount);		
	                    }		
	                }		
	                r.Result.Add(option, temp);		
	            }
            }

            return r;
        }
    }
}
