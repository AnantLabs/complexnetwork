using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticAnalyzer.Methods
{
    public class Interpolation
    {
        private List<double> m_values;
        private List<double> m_lagranjePolinoms;

        public Interpolation(List<double> values)
        {
            m_values = new List<double>(values);
        }

        public double Interpolate(double value)
        {
            double res = 0;
            FillLagranjePolinoms(value);
            for (int i = 0; i < m_values.Count; ++i)
            {
                res += m_values[i] * m_lagranjePolinoms[i];
            }
            return res;
        }

        private void FillLagranjePolinoms(double value)
        {
            m_lagranjePolinoms = new List<double>();
            for (int i = 0; i < m_values.Count; ++i)
                m_lagranjePolinoms.Add(CountPolinomValue(value, i));
        }

        private double CountPolinomValue(double value, int i)
        {
            double res = 1;

            for (int j = 0; j < m_values.Count; ++j)
            {
                if (i == j)
                    continue;

                res *= (double)(value - j) / (double)(i - j);
            }

            return res;
        }
    }
}
