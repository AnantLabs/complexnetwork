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
        public Dictionary<AnalyzeOption, object> Result { get; set; }

        public static EnsembleResult AverageResults(List<RealizationResult> results)
        {
            EnsembleResult r = new EnsembleResult();
            r.Result = new Dictionary<AnalyzeOption, object>();

            // TODO

            /*foreach (RealizationResult res in results)
            {
                foreach (AnalyzeOption opt in res.Result.Keys)
                {
                    AnalyzeOptionInfo info = ((AnalyzeOptionInfo[])opt.GetType().GetCustomAttributes(false))[0];
                    Type t = info.RealizationResultType;

                    // TODO write using LINQ algorithms if possible.
                    if (t.Equals(typeof(Double)))
                    {
                        r.Result[opt] = Convert.ToDouble(res.Result[opt]);
                    }
                    else if (t.Equals(typeof(UInt32)))
                    {
                        result.Result.Add(opt, Convert.ToInt32(r1.Result[opt]) +
                            Convert.ToInt32(r2.Result[opt]));
                    }
                    else if (t.Equals(typeof(BigInteger)))
                    {
                        result.Result.Add(opt, BigInteger.Parse(r1.Result[opt].ToString()) +
                            BigInteger.Parse(r2.Result[opt].ToString()));
                    }
                    else if (t.Equals(typeof(List<Double>)))
                    {
                        List<Double> l = new List<double>();
                        List<Double> l1 = r1.Result[opt] as List<Double>;
                        List<Double> l2 = r2.Result[opt] as List<Double>;
                        for (int i = 0; i < l1.Count; ++i)
                        {
                            l.Add(l1[i] + l2[i]);
                        }
                    }
                    else if (t.Equals(typeof(SortedDictionary<UInt32, UInt32>)))
                    {
                        SortedDictionary<UInt32, UInt32> d = new SortedDictionary<uint, uint>();
                        SortedDictionary<UInt32, UInt32> d1 = r1.Result[opt] as SortedDictionary<UInt32, UInt32>;
                        SortedDictionary<UInt32, UInt32> d2 = r1.Result[opt] as SortedDictionary<UInt32, UInt32>;
                    }
                    else if (t.Equals(typeof(SortedDictionary<Double, UInt32>)))
                    {
                    }
                    else if (t.Equals(typeof(SortedDictionary<UInt16, BigInteger>)))
                    {
                    }
                }
            }*/

            return r;
        }

        /*public Double AvgPathLength { get; set; }
        public UInt32 Diameter { get; set; }
        public Double AvgDegree { get; set; }
        public Double AvgClusteringCoefficient { get; set; }
        public BigInteger Cycles3 { get; set; }
        public BigInteger Cycles4 { get; set; }
        public List<Double> EigenValues { get; set; }
        public BigInteger Cycles3Eigen { get; set; }
        public BigInteger Cycles4Eigen { get; set; }
        public SortedDictionary<Double, UInt32> EigenDistanceDistribution { get; set; }
        public SortedDictionary<UInt32, UInt32> DegreeDistribution { get; set; }
        public SortedDictionary<Double, UInt32> ClusteringCoefficientDistribution { get; set; }
        public SortedDictionary<UInt32, UInt32> ConnectedComponentDistribution { get; set; }
        public SortedDictionary<UInt32, UInt32> CompleteComponentDistribution { get; set; }
        public SortedDictionary<UInt32, UInt32> DistanceDistribution { get; set; }
        public SortedDictionary<UInt32, UInt32> TriangleByVertexDistribution { get; set; }
        public SortedDictionary<UInt16, BigInteger> CycleDistribution { get; set; }*/
    }
}
