using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;

using CommonLibrary.Model.Result;
using StatisticAnalyzer;
using StatisticAnalyzer.Loader;

namespace StatisticAnalyzerUI
{
    public partial class ExtendedAnalyze : Form
    {
        public ExtendedAnalyze()
        {
            InitializeComponent();
        }

        private void trajectoryAnalyze_Click(object sender, EventArgs e)
        {
            // !исправить!
            StLoader loader = new StLoader();
            loader.ModelName = "ERModel";
            loader.InitAssemblies();
            List<ResultAssembly> list = loader.SelectAllAssemblies();

            SortedDictionary<BigInteger, double> avgs = new SortedDictionary<BigInteger,double>();
            SortedDictionary<BigInteger, double> sigmas = new SortedDictionary<BigInteger,double>();

            foreach (ResultAssembly resultAssembly in list)
            {
                SortedDictionary<double, double> r = new SortedDictionary<double, double>();
                int instanceCount = resultAssembly.Results.Count();
                for (int j = 0; j < instanceCount; ++j)
                {
                    SortedDictionary<int, double> tempDictionary = resultAssembly.Results[j].TriangleTrajectory;
                    SortedDictionary<int, double>.KeyCollection keyColl = tempDictionary.Keys;
                    foreach (int key in keyColl)
                    {
                        if (r.Keys.Contains(key))
                            r[key] += tempDictionary[key];
                        else
                            r.Add(key, tempDictionary[key]);
                    }
                }

                SortedDictionary<double, double> result = new SortedDictionary<double, double>();
                SortedDictionary<double, double>.KeyCollection keys = r.Keys;
                foreach (double key in keys)
                {
                    result.Add(key, r[key] / instanceCount);
                }

                SortedDictionary<double, double>.KeyCollection resultKeys = result.Keys;
                double avg = 0, sigma = 0;

                foreach (double key in resultKeys)
                {
                    avg += result[key];
                }
                avg /= resultKeys.Count();

                foreach (double key in resultKeys)
                {
                    sigma += Math.Pow((avg - result[key]), 2); 
                }
                sigma /= resultKeys.Count();
                sigma = Math.Sqrt(sigma);

                avgs.Add(resultAssembly.Results[0].trajectoryMu, avg);
                sigmas.Add(resultAssembly.Results[0].trajectoryMu, sigma);
            }

            ExtendedGraphic avgsGraphic = new ExtendedGraphic(avgs, "Average");
            avgsGraphic.Show();

            ExtendedGraphic sigmasGraphic = new ExtendedGraphic(sigmas, "Sigma");
            sigmasGraphic.Show();            
        }
    }
}
