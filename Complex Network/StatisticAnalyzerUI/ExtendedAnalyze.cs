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
using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model.Attributes;
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
            int k;
            try
            {
                k = Convert.ToInt32(this.parameterKTxt.Text);
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Parameter k must be an integer!", "Error");
                this.parameterKTxt.SelectAll();
                this.parameterKTxt.Focus();
                return;
            }

            // !исправить!
            StLoader loader = new StLoader();
            loader.ModelName = "ERModel";
            loader.InitAssemblies();
            List<ResultAssembly> list = loader.SelectAllAssemblies();

            SortedDictionary<double, double> avgs = new SortedDictionary<double,double>();
            SortedDictionary<double, double> sigmas = new SortedDictionary<double,double>();

            string paramLine = "";

            Dictionary<GenerationParam, object> genParams = list[0].GenerationParams;
            Dictionary<GenerationParam, object>.KeyCollection genKeys = genParams.Keys;
            foreach (GenerationParam g in genKeys)
            {
                GenerationParamInfo paramInfo =
                    (GenerationParamInfo)(g.GetType().GetField(g.ToString()).
                    GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                paramLine += paramInfo.Name += " = ";
                paramLine += genParams[g].ToString() + "; ";
            }

            paramLine += "StepCount = " + list[0].Results[0].trajectoryStepCount.ToString() + ";";

            foreach (ResultAssembly resultAssembly in list)
            {
                SortedDictionary<double, double> r = new SortedDictionary<double, double>();
                int instanceCount = resultAssembly.Results.Count();
                for (int j = 0; j < instanceCount; ++j)
                {
                    SortedDictionary<int, double> tempDictionary = resultAssembly.Results[j].TriangleTrajectory;
                    SortedDictionary<int, double>.KeyCollection keyColl = tempDictionary.Keys;
                    int limit = 1;
                    int previousKey = -1;
                    foreach (int key in keyColl)
                    {
                        if (limit >= k)
                        {
                            if (key - 1 == previousKey)
                            {
                                if (r.Keys.Contains(key))
                                    r[key] += tempDictionary[key];
                                else
                                    r.Add(key, tempDictionary[key]);
                            }
                            else
                            {
                                for (int tempIndex = previousKey + 1; tempIndex <= key - previousKey; ++tempIndex)
                                {
                                    if (r.Keys.Contains(tempIndex))
                                        r[tempIndex] += tempDictionary[key];
                                    else
                                        r.Add(tempIndex, tempDictionary[key]);
                                }
                            }

                            previousKey = key;
                        }
                        else
                        {
                            ++limit;
                        }
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

            ExtendedGraphic avgsGraphic = new ExtendedGraphic(avgs, paramLine, "Average");
            avgsGraphic.Show();

            ExtendedGraphic sigmasGraphic = new ExtendedGraphic(sigmas, paramLine, "Sigma");
            sigmasGraphic.Show();            
        }
    }
}
