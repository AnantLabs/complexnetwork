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
        /// <summary>
        /// Research part.
        /// </summary>
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

        /// <summary>
        /// Statistic part.
        /// </summary>
        //public List<StEnsembleResult> StEnsembleResults { get; set; }

        public ResearchResult()
        {
            ResearchParameterValues = new Dictionary<ResearchParameter, object>();
            GenerationParameterValues = new Dictionary<GenerationParameter, object>();

            EnsembleResults = new List<EnsembleResult>();

            //StEnsembleResults = null;
        }

        /*public void GlobalAnalyze(AnalyzeOption opt)
        {
            int globalDelta = 10;

            SortedDictionary<double, double> d = new SortedDictionary<double, double>(); // get result of analyze

            SortedDictionary<double, double> res = new SortedDictionary<double, double>();
            int delta = (int)Math.Ceiling((double)d.Count / 10), step = delta;
            while (step <= d.Count)
            {
                double sum = 0;
                int index = 0;
                SortedDictionary<double, double>.KeyCollection keys = d.Keys;
                foreach (double k in keys)
                {
                    if (index < step)
                    {
                        sum += d[k];
                        ++index;
                    }
                    else
                        break;
                }

                res.Add(step, sum / step);
                step += delta;
            }

            if (d.Count % delta != 0)
            {
                double sum = 0;
                SortedDictionary<double, double>.KeyCollection keys = d.Keys;
                foreach (double k in keys)
                {
                    sum += d[k];
                }
                res.Add(d.Count, sum / d.Count);
            }

            //return res;
        }

        void LocalAnalyze(AnalyzeOption opt)
        {

        }

        private SortedDictionary<double, double> UseThickening(SortedDictionary<double, double> r, int t)
        {
            if (t == 0)
                return r;
            else
            {
                SortedDictionary<double, double> res = new SortedDictionary<double, double>();
                double[] array = r.Values.ToArray();

                int k = 1, step = t;
                double sum = 0;
                for (int i = 0; i < array.Count(); ++i)
                {
                    if (k <= t)
                    {
                        sum += array[i];
                        ++k;
                    }
                    else
                    {
                        res.Add(step, sum / t);
                        sum = array[i];
                        k = 2;
                        step += t;
                    }
                }

                res.Add(array.Count(), sum / ((array.Count() % t == 0) ? t : array.Count() % t));
                return res;
            }
        }

        private void FillMathWaitingsAndDispersions(SortedDictionary<double, double> r, AnalyzeOption option)
        {
            SortedDictionary<double, double>.KeyCollection keys = r.Keys;
            switch (option)
            {
                case AnalyzeOption.TriangleTrajectory:
                    {
                        double avg = 0, sigma = 0;

                        foreach (double key in keys)
                        {
                            avg += r[key];
                        }
                        avg /= keys.Count();

                        foreach (double key in keys)
                        {
                            sigma += Math.Pow((avg - r[key]), 2);
                        }
                        sigma /= keys.Count();
                        sigma = Math.Sqrt(sigma);

                        result.resultMathWaitings.Add(option, avg);
                        result.resultDispersions.Add(option, sigma);

                        break;
                    }
                case AnalyzeOption.EigenValue:
                case AnalyzeOption.Cycles:
                case AnalyzeOption.TriangleCountByVertex:
                    {
                        break;
                    }
                default:
                    {
                        double mathWaiting = 0, mathWaitingSquare = 0;

                        foreach (double key in keys)
                        {
                            mathWaiting += key * r[key];
                            mathWaitingSquare += Math.Pow(key, 2) * r[key];
                        }

                        result.resultMathWaitings.Add(option, mathWaiting);
                        result.resultDispersions.Add(option, (mathWaitingSquare - Math.Pow(mathWaiting, 2)));

                        break;
                    }
            }
        }*/
    }
}
