using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Attributes;
using Core.Enumerations;

namespace Core.Result
{
    using CalculationResult = KeyValuePair<CalculationOption, SortedDictionary<double, double>>;

    public class CalculationOption
    {
        public ThickeningType thickeningType;
        public Int32 thickeningValue;
        public ApproximationType approximationType;

        public static bool operator==(CalculationOption l, CalculationOption r)
        {
            return (l.thickeningType == r.thickeningType &&
                l.thickeningValue == r.thickeningValue &&
                l.approximationType == r.approximationType);
        }

        public static bool operator !=(CalculationOption l, CalculationOption r)
        {
            return !(l == r);
        }
    }

    public class StatisticResult
    {
        private ResearchResult researchResult;
        private Dictionary<AnalyzeOption, double> globalOptionsResult;
        private Dictionary<AnalyzeOption, CalculationResult> distributedOptionsResult;

        public StatisticResult(ResearchResult r)
        {
            researchResult = r;

            globalOptionsResult = new Dictionary<AnalyzeOption, double>();
            distributedOptionsResult = new Dictionary<AnalyzeOption, CalculationResult>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        /// /// Does ResearchResult contain that option???
        public double GetGlobalResult(AnalyzeOption opt)
        {
            if (!globalOptionsResult.Keys.Contains(opt))
                CalculateAndSetGlobalOption(opt);
            return globalOptionsResult[opt];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="cOpt"></param>
        /// <returns></returns>
        /// Does ResearchResult contain that option???
        public SortedDictionary<double, double> GetDistributionResult(AnalyzeOption opt, CalculationOption cOpt)
        {
            if (!distributedOptionsResult.Keys.Contains(opt) ||
                cOpt != distributedOptionsResult[opt].Key)
                CalculateAndSetDistributionOption(opt, cOpt);
            return distributedOptionsResult[opt].Value;
        }

        private void CalculateAndSetGlobalOption(AnalyzeOption opt)
        {
            double value = 0;
            // calculate
            globalOptionsResult.Add(opt, value);
        }

        private void CalculateAndSetDistributionOption(AnalyzeOption opt, CalculationOption cOpt)
        {
            SortedDictionary<double, double> res = new SortedDictionary<double, double>();

            AnalyzeOptionInfo[] info = (AnalyzeOptionInfo[])opt.GetType().GetField(opt.ToString()).GetCustomAttributes(typeof(AnalyzeOptionInfo), false);
            Type t = info[0].EnsembleResultType;
            if (t.Equals(typeof(SortedDictionary<UInt32, Double>)))
            {
                SortedDictionary<UInt32, Double> r = researchResult.EnsembleResults[0].Result[opt] as SortedDictionary<UInt32, Double>;
                res = ApplyThickening<UInt32>(r, cOpt);
            }
            else if (t.Equals(typeof(SortedDictionary<Double, Double>)))
            {
                SortedDictionary<Double, Double> r = researchResult.EnsembleResults[0].Result[opt] as SortedDictionary<Double, Double>;
                res = ApplyThickening<Double>(r, cOpt);
            }
            else if (t.Equals(typeof(SortedDictionary<UInt16, Double>)))
            {
                SortedDictionary<UInt16, Double> r = researchResult.EnsembleResults[0].Result[opt] as SortedDictionary<UInt16, Double>;
                res = ApplyThickening<UInt16>(r, cOpt);
            }

            if (distributedOptionsResult.Keys.Contains(opt))
                distributedOptionsResult.Add(opt, new CalculationResult(cOpt, res));
            else
                distributedOptionsResult[opt] = new CalculationResult(cOpt, res);
        }

        private SortedDictionary<double, double> ApplyThickening<T>(SortedDictionary<T, double> r, CalculationOption cOpt)
        {
            int t = cOpt.thickeningValue;
            if (cOpt.thickeningType == ThickeningType.Percent)
                t = (int)(r.Values.Count() * cOpt.thickeningValue / 100.0);

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
}
