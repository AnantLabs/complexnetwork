using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using Model.HierarchicModel;
using Model.HierarchicModel.Realization;
using Model.ERModel;
using Model.ERModel.Realization;

using CommonLibrary.Model.Result;
using ResultStorage.Storage;
using RandomGraph.Settings;

namespace Percolations
{
    // !исправить!
    public partial class PercolationCounting : Form
    {
        private string modName;
        private string jobName;

        public PercolationCounting(string mName, string jName)
        {
            InitializeComponent();

            this.modName = mName;
            this.jobName = jName;
        }

        // Обработчики сообщений.

        private void PercolationCounting_Load(object sender, EventArgs e)
        {
            this.modelNameCmb.Text = this.modName;
            this.optionNameCmb.SelectedIndex = 0;
            this.branchIndexCmb.SelectedIndex = 0;
            this.maxLevelCmb.SelectedIndex = 0;
            this.probabilityFunctionCmb.Items.AddRange(Enum.GetNames(typeof(GenerationProbability)));
            this.probabilityFunctionCmb.SelectedIndex = 1;

            switch (this.modName)
            {
                case "HierarchicModel":
                    InitializeHierarchicModel();
                    break;
                case "ERModel":
                    InitializeERModel();
                    break;
                default:
                    this.Controls.Clear();
                    break;
            }
        }

        private void branchIndexCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.networkSizeTxt.Text = Math.Pow(Double.Parse(this.branchIndexCmb.Text),
                    Double.Parse(this.maxLevelCmb.Text)).ToString();
            }
            catch { }
        }

        private void maxLevelCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.networkSizeTxt.Text = Math.Pow(Double.Parse(this.branchIndexCmb.Text),
                    Double.Parse(this.maxLevelCmb.Text)).ToString();
            }
            catch { }
        }

        /*private void startExtended_Click(object sender, EventArgs e)
        {
            ResultResearch result = new ResultResearch();
            result.Name = this.jobName;
            result.ModelType = typeof(HierarchicModel);
            result.Delta = Double.Parse(this.deltaExtendedTxt.Text);
            result.RealizationCount = (Int32)this.realizationCountNum.Value;
            result.Function = this.probabilityFunctionCmb.Text;
            result.GenerationParams[GenerationParam.BranchIndex] = Int16.Parse(this.branchIndexCmb.Text);
            result.GenerationParams[GenerationParam.Level] = Int16.Parse(this.maxLevelCmb.Text);
            result.Size = (int)Math.Pow(Int16.Parse(this.branchIndexCmb.Text), Int16.Parse(this.maxLevelCmb.Text));

            Int16 maxLevel = Int16.Parse(this.maxLevelCmb.Text);
            for (Int16 g = 1; g <= maxLevel; ++g)
            {
                result.Result.Add(g, AnalyzeExtendedHierarchic(g));
            }

            XMLResultStorage storage = new XMLResultStorage(Options.StorageDirectory);
            storage.SaveResearch(result);

            MessageBox.Show("Results are saved succesfully!");
        }*/

        private void startER_Click(object sender, EventArgs e)
        {
            ResultResearch result = new ResultResearch();
            result.Name = this.jobName;
            result.ModelType = typeof(ERModel);

            // TODO remove hardcoded data
            result.Delta = 0.00001;
            result.RealizationCount = 100;
            result.GenerationParams[GenerationParam.Vertices] = 65536;
            SortedDictionary<double, SubGraphsInfo> pInfo = 
                new SortedDictionary<double, SubGraphsInfo>();

            this.folderBrowserDialog1.ShowDialog();
            string dirName = this.folderBrowserDialog1.SelectedPath;

            DirectoryInfo parentDir = new DirectoryInfo(dirName);
            foreach (FileInfo f in parentDir.GetFiles())
            {
                double p = Double.Parse(f.Name.Remove(f.Name.Length - 4));
                SubGraphsInfo value = new SubGraphsInfo();

                SortedDictionary<int, double> r = new SortedDictionary<int, double>();
                using (StreamReader streamReader = new StreamReader(f.FullName, System.Text.Encoding.Default))
                {
                    string contents;

                    while ((contents = streamReader.ReadLine()) != null)
                    {
                        string first = "", second = "";
                        int j = 0;
                        while (contents[j] != ' ')
                        {
                            first += contents[j];
                            ++j;
                        }

                        second = contents.Substring(j);

                        r.Add(int.Parse(first), double.Parse(second));
                    }
                }

                value.avgOrder = r.First().Value;
                value.avgOrderCount = r.First().Key;

                if (r.Count > 1)
                {
                    r.Remove(r.First().Key);
                    value.secondMax = r.First().Value;
                    value.secondMaxCount = r.First().Key;
                }

                if (r.Count > 2)
                {
                    r.Remove(r.First().Key);
                    value.avgOrderRest = r.Average(x => x.Value);
                }

                pInfo.Add(p, value);
            }

            result.Result.Add(1, pInfo);

            XMLResultStorage storage = new XMLResultStorage(Options.StorageDirectory);
            storage.SaveResearch(result);

            MessageBox.Show("Results are saved succesfully!");
        }

        private void startHierarchic_Click(object sender, EventArgs e)
        {
            ResultResearch result = new ResultResearch();
            result.Name = this.jobName;
            result.ModelType = typeof(HierarchicModel);
            result.Delta = Double.Parse(this.deltaExtendedTxt.Text);
            result.RealizationCount = (Int32)this.realizationCountNum.Value;
            result.Function = this.probabilityFunctionCmb.Text;
            result.GenerationParams[GenerationParam.BranchIndex] = Int16.Parse(this.branchIndexCmb.Text);
            result.GenerationParams[GenerationParam.Level] = Int16.Parse(this.maxLevelCmb.Text);
            result.Size = (int)Math.Pow(Int16.Parse(this.branchIndexCmb.Text), Int16.Parse(this.maxLevelCmb.Text));

            Int16 maxLevel = Int16.Parse(this.maxLevelCmb.Text);
            for (Int16 g = 1; g <= maxLevel; ++g)
            {
                result.Result.Add(g, new SortedDictionary<double, SubGraphsInfo>());
            }

            double muLow = Double.Parse(this.muRangeLowExtendedTxt.Text);
            double muHigh = Double.Parse(this.muRangeHighExtendedTxt.Text);
            double muDelta = Double.Parse(this.deltaExtendedTxt.Text);

            double muTemp = muLow;
            SubGraphsInfo tempInfo = new SubGraphsInfo();
            while (muTemp < muHigh)
            {
                SortedDictionary<double, SubGraphsInfo> r = AnalyzeHierarchic(muTemp);
                foreach (double gamma in r.Keys)
                {
                    result.Result[gamma].Add(muTemp, r[gamma]);
                }

                muTemp += muDelta;
            }

            XMLResultStorage storage = new XMLResultStorage(Options.StorageDirectory);
            storage.SaveResearch(result);

            MessageBox.Show("Results are saved succesfully!");
        }

        private void startAvgHierarchic_Click(object sender, EventArgs e)
        {
            ResultResearch result = new ResultResearch();
            result.Name = this.jobName;
            result.ModelType = typeof(HierarchicModel);
            result.Delta = Double.Parse(this.deltaExtendedTxt.Text);
            result.RealizationCount = (Int32)this.realizationCountNum.Value;
            result.Function = this.probabilityFunctionCmb.Text;
            result.GenerationParams[GenerationParam.BranchIndex] = Int16.Parse(this.branchIndexCmb.Text);
            result.GenerationParams[GenerationParam.Level] = Int16.Parse(this.maxLevelCmb.Text);
            result.Size = (int)Math.Pow(Int16.Parse(this.branchIndexCmb.Text), Int16.Parse(this.maxLevelCmb.Text));

            Int16 maxLevel = Int16.Parse(this.maxLevelCmb.Text);
            for (Int16 g = 1; g <= maxLevel; ++g)
            {
                result.Result.Add(g, new SortedDictionary<double, SubGraphsInfo>());
            }

            double muLow = Double.Parse(this.muRangeLowExtendedTxt.Text);
            double muHigh = Double.Parse(this.muRangeHighExtendedTxt.Text);
            double muDelta = Double.Parse(this.deltaExtendedTxt.Text);

            double muTemp = muLow;
            SubGraphsInfo tempInfo = new SubGraphsInfo();
            while (muTemp < muHigh)
            {
                SortedDictionary<double, double> r = AnalyzeAvgHierarchic(muTemp);
                foreach (double gamma in r.Keys)
                {
                    tempInfo.avgOrder = r[gamma];
                    result.Result[gamma].Add(muTemp, tempInfo);
                }

                muTemp += muDelta;
            }

            XMLResultStorage storage = new XMLResultStorage(Options.StorageDirectory);
            storage.SaveResearch(result);

            MessageBox.Show("Results are saved succesfully!");
        }

        private void startGlobal_Click(object sender, EventArgs e)
        {
            Int16 p = Int16.Parse(this.branchIndexCmb.Text);
            Int16 maxLevel = Int16.Parse(this.maxLevelCmb.Text);
            int realizationCount = (Int32)this.realizationCountNum.Value;
            double muLow = Double.Parse(this.muRangeLowExtendedTxt.Text);
            double muHigh = Double.Parse(this.muRangeHighExtendedTxt.Text);
            double muDelta = Double.Parse(this.deltaExtendedTxt.Text);

            ResultResearch result = new ResultResearch();
            result.Name = this.jobName;
            result.ModelType = typeof(HierarchicModel);
            result.Delta = muDelta;
            result.RealizationCount = realizationCount;
            result.Function = this.probabilityFunctionCmb.Text;
            result.GenerationParams[GenerationParam.BranchIndex] = p;
            result.GenerationParams[GenerationParam.Level] = maxLevel;
            result.Size = (int)Math.Pow(p, maxLevel);
            result.Result.Add(maxLevel, new SortedDictionary<double, SubGraphsInfo>());

            Dictionary<GenerationParam, object> genParameters = new Dictionary<GenerationParam, object>();
            genParameters.Add(GenerationParam.BranchIndex, p);
            genParameters.Add(GenerationParam.Level, maxLevel);

            HierarchicGenerator hGenerator = new HierarchicGenerator();
            HierarchicAnalyzer hAnalyzer;

            double muTemp = muLow;
            
            SortedDictionary<int, int> subGraphInfo = new SortedDictionary<int,int>();
            while (muTemp <= muHigh)
            {
                SubGraphsInfo tempInfo = new SubGraphsInfo();
                for (int r = 0; r < realizationCount; ++r)
                {
                    genParameters[GenerationParam.Mu] = muTemp;
                    hGenerator.Generation(genParameters);
                    hAnalyzer = new HierarchicAnalyzer((HierarchicContainer)hGenerator.Container);
                    subGraphInfo = hAnalyzer.GetConnSubGraph();

                    tempInfo.avgOrder += subGraphInfo.Last().Key;
                    tempInfo.avgOrderCount += subGraphInfo.Last().Value;

                    if (subGraphInfo.Count > 1)
                    {
                        subGraphInfo.Remove(subGraphInfo.Last().Key);
                        tempInfo.secondMax += subGraphInfo.Last().Key;
                        tempInfo.secondMaxCount += subGraphInfo.Last().Value;
                    }

                    if (subGraphInfo.Count > 2)
                    {
                        subGraphInfo.Remove(subGraphInfo.Last().Key);
                        tempInfo.avgOrderRest += subGraphInfo.Average(x => x.Key);
                    }
                }
                tempInfo.avgOrder /= (double)realizationCount;
                tempInfo.avgOrder /= result.Size;
                tempInfo.avgOrderCount /= (double)realizationCount;

                tempInfo.secondMax /= (double)realizationCount;
                tempInfo.secondMax /= result.Size;
                tempInfo.secondMaxCount /= (double)realizationCount;

                tempInfo.avgOrderRest /= (double)realizationCount;
                tempInfo.avgOrderRest /= result.Size;

                result.Result[maxLevel].Add(muTemp, tempInfo);

                muTemp += muDelta;
            }

            XMLResultStorage storage = new XMLResultStorage(Options.StorageDirectory);
            storage.SaveResearch(result);

            MessageBox.Show("Results are saved succesfully!");
        }

        /*private void startSublevelsHierarchic_Click(object sender, EventArgs e)
        {
            ResultResearch result = new ResultResearch();
            result.Name = this.jobName;
            result.ModelType = typeof(HierarchicModel);
            result.Delta = Double.Parse(this.deltaExtendedTxt.Text);
            result.RealizationCount = (Int32)this.realizationCountNum.Value;
            result.Function = this.probabilityFunctionCmb.Text;
            result.GenerationParams[GenerationParam.BranchIndex] = Int16.Parse(this.branchIndexCmb.Text);
            result.GenerationParams[GenerationParam.Level] = Int16.Parse(this.maxLevelCmb.Text);
            result.Size = (int)Math.Pow(Int16.Parse(this.branchIndexCmb.Text), Int16.Parse(this.maxLevelCmb.Text));

            Int16 maxLevel = Int16.Parse(this.maxLevelCmb.Text);
            for (Int16 g = 1; g <= maxLevel; ++g)
            {
                result.Result.Add(g, new SortedDictionary<double, SubGraphsInfo>());
            }

            double muLow = Double.Parse(this.muRangeLowExtendedTxt.Text);
            double muHigh = Double.Parse(this.muRangeHighExtendedTxt.Text);
            double muDelta = Double.Parse(this.deltaExtendedTxt.Text);

            double muTemp = muLow;
            SubGraphsInfo tempInfo = new SubGraphsInfo();
            while (muTemp < muHigh)
            {
                SortedDictionary<double, double> r = AnalyzeSublevelsHierarchic(muTemp);
                foreach (double gamma in r.Keys)
                {
                    tempInfo.avgOrder = r[gamma];
                    result.Result[gamma].Add(muTemp, tempInfo);
                }

                muTemp += muDelta;
            }

            XMLResultStorage storage = new XMLResultStorage(Options.StorageDirectory);
            storage.SaveResearch(result);

            MessageBox.Show("Results are saved succesfully!");
        }*/

        // Утилиты.

        private void InitializeHierarchicModel()
        {
            this.branchIndex.Visible = true;
            this.branchIndexCmb.Visible = true;
            this.maxLevel.Visible = true;
            this.maxLevelCmb.Visible = true;
            this.hLabel.Visible = true;
            this.muRangeLowExtendedTxt.Visible = true;
            this.muRangeHighExtendedTxt.Visible = true;
            this.probabilityFunction.Visible = true;
            this.probabilityFunctionCmb.Visible = true;
            this.startHierarchic.Visible = true;
            this.startAvgHierarchic.Visible = true;
            this.startGlobal.Visible = true;
        }

        private void InitializeERModel()
        {
            this.networkSizeTxt.Text = "";
            this.erLabel.Visible = true;
            this.probRangeLowExtendedTxt.Visible = true;
            this.probRangeHighExtendedTxt.Visible = true;
            this.startER.Visible = true;
        }

        private SortedDictionary<double, SubGraphsInfo> AnalyzeExtendedHierarchic(Int16 currentLevel)
        {
            SortedDictionary<double, SubGraphsInfo> result = new SortedDictionary<double, SubGraphsInfo>();

            Int16 p = Int16.Parse(this.branchIndexCmb.Text);
            Int16 maxLevel = Int16.Parse(this.maxLevelCmb.Text);
            double muLow = Double.Parse(this.muRangeLowExtendedTxt.Text);
            double muHigh = Double.Parse(this.muRangeHighExtendedTxt.Text);
            double muDelta = Double.Parse(this.deltaExtendedTxt.Text);
            int realizationCount = (Int32)this.realizationCountNum.Value;

            HierarchicGenerator hGenerator = new HierarchicGenerator();
            HierarchicAnalyzer hAnalyzer;

            double muTemp = muLow;
            SubGraphsInfo tempInfo = new SubGraphsInfo();
            while (muTemp <= muHigh)
            {
                double avgOrder = 0;
                for (int r = 0; r < realizationCount; ++r)
                {
                    hGenerator.GenerateTreeWithProbability(p, maxLevel, 0, currentLevel,
                        muTemp, new ProbabilityCounter(ProbabilityFunctions.Classical));
                    hAnalyzer = new HierarchicAnalyzer((HierarchicContainer)hGenerator.Container);
                    avgOrder += hAnalyzer.GetConnSubGraph().Last().Key;
                }
                double avgValue = avgOrder / (double)realizationCount;
                tempInfo.avgOrder = avgValue / Math.Pow(p, currentLevel); 
                result.Add(muTemp, tempInfo);

                muTemp += muDelta;
            }

            return result;
        }

        private SortedDictionary<double, SubGraphsInfo> AnalyzeHierarchic(double mu)
        {
            Int16 p = Int16.Parse(this.branchIndexCmb.Text);
            Int16 maxLevel = Int16.Parse(this.maxLevelCmb.Text);
            int realizationCount = (Int32)this.realizationCountNum.Value;

            SortedDictionary<double, SubGraphsInfo> result = new SortedDictionary<double, SubGraphsInfo>();
            for (Int16 i = 1; i <= maxLevel; ++i)
            {
                result.Add(i, new SubGraphsInfo());
            }            

            Dictionary<GenerationParam, object> genParameters = new Dictionary<GenerationParam, object>();
            genParameters.Add(GenerationParam.BranchIndex, p);
            genParameters.Add(GenerationParam.Level, maxLevel);
            genParameters.Add(GenerationParam.Mu, mu);

            HierarchicGenerator hGenerator = new HierarchicGenerator();
            HierarchicAnalyzer hAnalyzer;

            for (int r = 0; r < realizationCount; ++r)
            {
                hGenerator.Generation(genParameters);
                hAnalyzer = new HierarchicAnalyzer((HierarchicContainer)hGenerator.Container);

                SortedDictionary<int, int> subGraphInfo;
                for (Int16 i = 1; i <= maxLevel; ++i)
                {
                    subGraphInfo = hAnalyzer.GetConnSubGraphPerLevel(i);

                    result[i].avgOrder += (subGraphInfo.Count != 0) ?
                        subGraphInfo.Last().Key / Math.Pow(p, i) : 1 / Math.Pow(p, i);
                    result[i].avgOrderCount += (subGraphInfo.Count != 0) ? 
                        subGraphInfo.Last().Value : 1;

                    if (subGraphInfo.Count > 1)
                    {
                        subGraphInfo.Remove(subGraphInfo.Last().Key);
                        result[i].secondMax += subGraphInfo.Last().Key / Math.Pow(p, i);
                        result[i].secondMaxCount += subGraphInfo.Last().Value;
                    }

                    if (subGraphInfo.Count > 2)
                    {
                        subGraphInfo.Remove(subGraphInfo.Last().Key);
                        result[i].avgOrderRest += subGraphInfo.Average(x => x.Key) / Math.Pow(p, i);
                    }
                }
            }

            for (Int16 i = 1; i <= maxLevel; ++i)
            {
                result[i].avgOrder /= (double)realizationCount;
                result[i].avgOrderCount /= (double)realizationCount;

                result[i].secondMax /= (double)realizationCount;
                result[i].secondMaxCount /= (double)realizationCount;

                result[i].avgOrderRest /= (double)realizationCount;
            }                 

            return result;
        }

        private SortedDictionary<double, double> AnalyzeAvgHierarchic(double mu)
        {
            Int16 p = Int16.Parse(this.branchIndexCmb.Text);
            Int16 maxLevel = Int16.Parse(this.maxLevelCmb.Text);
            int realizationCount = (Int32)this.realizationCountNum.Value;

            SortedDictionary<double, double> result = new SortedDictionary<double, double>();
            for (Int16 i = 1; i <= maxLevel; ++i)
            {
                result.Add(i, 0);
            }

            Dictionary<GenerationParam, object> genParameters = new Dictionary<GenerationParam, object>();
            genParameters.Add(GenerationParam.BranchIndex, p);
            genParameters.Add(GenerationParam.Level, maxLevel);
            genParameters.Add(GenerationParam.Mu, mu);

            HierarchicGenerator hGenerator = new HierarchicGenerator();
            HierarchicAnalyzer hAnalyzer;

            for (int r = 0; r < realizationCount; ++r)
            {
                hGenerator.Generation(genParameters);
                hAnalyzer = new HierarchicAnalyzer((HierarchicContainer)hGenerator.Container);

                for (Int16 i = 1; i <= maxLevel; ++i)
                {
                    double maxOrder = hAnalyzer.GetAvgConnSubGraphPerLevel(i);
                    double normMaxOrder = maxOrder / (Math.Pow(p, i));
                    result[i] += normMaxOrder;
                }
            }

            for (Int16 i = 1; i <= maxLevel; ++i)
            {
                result[i] /= realizationCount;
            }

            return result;
        }

        /*private SortedDictionary<double, SubGraphsInfo> AnalyzeExtendedER()
        {
            SortedDictionary<double, SubGraphsInfo> result = new SortedDictionary<double, SubGraphsInfo>();

            Int32 n = Int32.Parse(this.networkSizeTxt.Text);
            int realizationCount = (Int32)this.realizationCountNum.Value;
            double qLow = Double.Parse(this.probRangeLowExtendedTxt.Text);
            double qHigh = Double.Parse(this.probRangeHighExtendedTxt.Text);
            double qDelta = Double.Parse(this.deltaExtendedTxt.Text);

            Dictionary<GenerationParam, object> genParameters = new Dictionary<GenerationParam, object>();
            genParameters.Add(GenerationParam.Vertices, n);

            ERGenerator erGenerator = new ERGenerator();
            ERAnalyzer erAnalyzer;

            double qTemp = qLow;
            SubGraphsInfo tempInfo = new SubGraphsInfo();
            while (qTemp <= qHigh)
            {
                double avgOrder = 0;
                for (int r = 0; r < realizationCount; ++r)
                {
                    genParameters[GenerationParam.P] = qTemp;
                    erGenerator.Generation(genParameters);
                    erAnalyzer = new ERAnalyzer((ERContainer)erGenerator.Container);

                    avgOrder += erAnalyzer.GetConnSubGraph().Last().Key;
                }
                double avgValue = avgOrder / (double)realizationCount;
                tempInfo.avgOrder = avgValue;
                result.Add(qTemp, tempInfo);

                qTemp += qDelta;
            }

            return result;
        }*/

        private SortedDictionary<double, double> AnalyzeSublevelsHierarchic(double mu)
        {
            Int16 p = Int16.Parse(this.branchIndexCmb.Text);
            Int16 maxLevel = Int16.Parse(this.maxLevelCmb.Text);
            int realizationCount = (Int32)this.realizationCountNum.Value;

            SortedDictionary<double, double> result = new SortedDictionary<double, double>();
            for (Int16 i = 1; i <= maxLevel; ++i)
            {
                result.Add(i, 0);
            }

            Dictionary<GenerationParam, object> genParameters = new Dictionary<GenerationParam, object>();
            genParameters.Add(GenerationParam.BranchIndex, p);
            genParameters.Add(GenerationParam.Level, maxLevel);
            genParameters.Add(GenerationParam.Mu, mu);

            HierarchicGenerator hGenerator = new HierarchicGenerator();
            HierarchicAnalyzer hAnalyzer;

            for (int r = 0; r < realizationCount; ++r)
            {
                hGenerator.Generation(genParameters);
                hAnalyzer = new HierarchicAnalyzer((HierarchicContainer)hGenerator.Container);

                SortedDictionary<int, int> temp;
                for (Int16 i = 1; i <= maxLevel; ++i)
                {
                    temp = hAnalyzer.GetConnSubGraphSublevels(i);
                    double maxOrder = (temp.Count != 0) ? temp.Last().Key : 1;
                    double normMaxOrder = maxOrder / (Math.Pow(p, i));
                    result[i] += normMaxOrder;
                }
            }

            for (Int16 i = 1; i <= maxLevel; ++i)
            {
                result[i] /= realizationCount;
            }

            return result;
        }
    }
}
