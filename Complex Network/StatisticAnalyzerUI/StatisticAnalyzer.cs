using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;
using System.Runtime.InteropServices;

using StatisticAnalyzer;
using StatisticAnalyzer.Loader;
using StatisticAnalyzer.Analyzer;
using StatisticAnalyzer.Viewer;

using SettingsConfiguration;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model.Attributes;
using CommonLibrary.Model.Result;
using ZedGraph;

namespace StatisticAnalyzerUI
{
    public partial class StatisticAnalyzer : Form
    {
        // Implementation members //
        private StLoader loader;

        // GUI members //
        private Dictionary<GenerationParam, ComboBox> generationParamatersControls = 
            new Dictionary<GenerationParam, ComboBox>();

        private GraphicCondition globalGraphic;
        private GraphicCondition localGraphic;
        private GraphicCondition motifGraphic;

        public StatisticAnalyzer()
        {
            InitializeComponent();
            InitializeConfigurationMembers();
            InitializeGUIMembers();
        }

        // Event Handlers //

        private void OnLoad(object sender, EventArgs e)
        {
            this.ByJobsRadio.Checked = true;
            this.ApproximationTypeCmb.SelectedIndex = 0;

            this.globalGraphic = new GraphicCondition();
            this.localGraphic = new GraphicCondition();
            this.motifGraphic = new GraphicCondition();

            InitializeCurveLineCmb();
            InitializeModelNameCmb();
        }

        private void ModelNameSelChange(object sender, EventArgs e)
        {
            loader.ModelName = this.ModelNameCmb.Text;
            loader.InitAssemblies();
            InitializeGenerationParameters();
            FillJobs();
        }

        // By Jobs mode //
        private void ByJobsRadio_CheckedChanged(object sender, EventArgs e)
        {
            this.JobsCmb.Enabled = true;
            this.DeleteJob.Enabled = true;
            this.RefreshBtn.Enabled = false;
            this.GenerationParametersGrp.Enabled = false;
            this.ByAllJobsCheck.Enabled = false;

            RefreshParameters();
        }

        private void DeleteJob_Click(object sender, EventArgs e)
        {
            string name = (string)this.JobsCmb.SelectedItem;
            if (name != null)
            {
                loader.DeleteJob(name);
                FillJobs();
            }
        }

        private void JobsCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshParameters();
        }

        // By Parameters mode //
        private void ByParametersRadio_CheckedChanged(object sender, EventArgs e)
        {
            this.JobsCmb.Enabled = false;
            this.DeleteJob.Enabled = false;
            this.RefreshBtn.Enabled = true;
            this.GenerationParametersGrp.Enabled = true;
            this.ByAllJobsCheck.Enabled = true;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshAssemblies();
        }

        private void control_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            FillNextGenerationParameterCombos(cmb.TabIndex + 1);
        }

        private void LocalPropertiesList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = 0;
            if (e.NewValue == CheckState.Checked)
            {
                index = this.LocalAnalyzeOptionsGrd.Rows.Add();
                this.LocalAnalyzeOptionsGrd.Rows[index].Cells[0].Value = this.LocalPropertiesList.Items[e.Index].ToString();
                this.LocalAnalyzeOptionsGrd.Rows[index].Cells[1].Value = 0;
                this.LocalAnalyzeOptionsGrd.Rows[index].Cells[2].Value = 0;
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                for (int i = 0; i < this.LocalAnalyzeOptionsGrd.Rows.Count; ++i)
                {
                    if (this.LocalAnalyzeOptionsGrd.Rows[i].Cells[0].Value.ToString() ==
                        this.LocalPropertiesList.Items[e.Index].ToString())
                    {
                        index = i;
                        break;
                    }
                }

                this.LocalAnalyzeOptionsGrd.Rows.RemoveAt(index);
            }
        }

        private void LocalAnalyzeOptionsGrd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellCollection cells = this.LocalAnalyzeOptionsGrd.Rows[e.RowIndex].Cells;
            if (cells[e.ColumnIndex].Value.ToString() != "0")
            {
                for (int i = 1; i < cells.Count; ++i)
                {
                    if (i != e.ColumnIndex)
                        cells[i].Value = "0";
                }
            }
        }

        // Analyzers //
        private void GlobalDrawGraphics_Click(object sender, EventArgs e)
        {
            if (!ValidateGraphicConditions())
                return;

            StAnalyzer analyzer = new StAnalyzer(GetAssembliesToAnalyze());
            List<AnalyseOptions> checkedOptions = GetCheckedOptionsGlobal(analyzer);
            analyzer.GlobalAnalyze();
            if (analyzer.Result.result.Keys.Count == 0)
                return;
            StAnalyzeResult stResult = analyzer.Result;
            stResult.type = StAnalyzeType.Global;

            ShowWarning(checkedOptions, stResult);

            if (this.globalGraphic.isOpen)
            {
                this.globalGraphic.graphic.Add(stResult, (Color)this.CurveLineCmb.SelectedItem, !PointsCheck.Checked);
            }
            else
            {
                this.globalGraphic.isOpen = true;
                this.globalGraphic.graphic = new Graphic(stResult, (Color)this.CurveLineCmb.SelectedItem,
                    !PointsCheck.Checked, this.globalGraphic);
                this.globalGraphic.graphic.Show();
            }
        }

        private void valueButton_Click(object sender, EventArgs e)
        {
            if (!ValidateGraphicConditions())
                return;

            StAnalyzer analyzer = new StAnalyzer(GetAssembliesToAnalyze());
            List<AnalyseOptions> checkedOptions = GetCheckedOptionsGlobal(analyzer);
            analyzer.GlobalAnalyze();
            if (analyzer.Result.result.Keys.Count == 0)
                return;
            StAnalyzeResult result = analyzer.Result;
            result.type = StAnalyzeType.Global;

            ShowWarning(checkedOptions, result);

            ValueTable valueTable = new ValueTable(result);
            valueTable.ShowDialog();
        }

        private void GetGlobalResult_Click(object sender, EventArgs e)
        {
            if (!ValidateGraphicConditions())
                return;

            StAnalyzer analyzer = new StAnalyzer(GetAssembliesToAnalyze());
            List<AnalyseOptions> checkedOptions = GetCheckedOptionsGlobal(analyzer);
            analyzer.GlobalAnalyze();

            ShowWarning(checkedOptions, analyzer.Result);
        }

        private void LocalDrawGraphics_Click(object sender, EventArgs e)
        {
            if (!ValidateGraphicConditions())
                return;

            StAnalyzer analyzer = new StAnalyzer(GetAssembliesToAnalyze());

            List<AnalyseOptions> checkedOptions = GetCheckedOptionsLocal(analyzer);
            MakeParameters(analyzer);
            analyzer.LocalAnalyze();
            if (analyzer.Result.result.Keys.Count == 0)
                return;
            StAnalyzeResult result = analyzer.Result;
            result.type = StAnalyzeType.Local;
            result.approximationType = (ApproximationTypes)Enum.Parse(typeof(ApproximationTypes),
                this.ApproximationTypeCmb.SelectedItem.ToString());

            ShowWarning(checkedOptions, result);

            if (this.localGraphic.isOpen)
            {
                if (result.approximationType != this.localGraphic.graphic.GetApproximation())
                {
                    MessageBox.Show("Approximation types don't match!", "Error");
                    return;
                }

                this.localGraphic.graphic.Add(result, (Color)this.CurveLineCmb.SelectedItem, !PointsCheck.Checked);
            }
            else
            {
                this.localGraphic.isOpen = true;
                this.localGraphic.graphic = new Graphic(result, (Color)this.CurveLineCmb.SelectedItem,
                    !PointsCheck.Checked, this.localGraphic);
                this.localGraphic.graphic.Show();
            }
        }

        private void localValueButton_Click(object sender, EventArgs e)
        {
            if (!ValidateGraphicConditions())
                return;

            StAnalyzer analyzer = new StAnalyzer(GetAssembliesToAnalyze());
            List<AnalyseOptions> checkedOptions = GetCheckedOptionsLocal(analyzer);
            MakeParameters(analyzer);
            analyzer.LocalAnalyze();
            if (analyzer.Result.result.Keys.Count == 0)
                return;
            StAnalyzeResult result = analyzer.Result;
            result.type = StAnalyzeType.Local;
            result.approximationType = (ApproximationTypes)Enum.Parse(typeof(ApproximationTypes),
                this.ApproximationTypeCmb.SelectedItem.ToString());

            ShowWarning(checkedOptions, result);

            ValueTable valueTable = new ValueTable(result);
            valueTable.Show();
        }

        private void MotifDrawGraphics_Click(object sender, EventArgs e)
        {
            if (!ValidateGraphicConditions())
                return;
        }

        // Menu Event Handlers //
        private void MenuSetProvider_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            StorgeProvider storageProvider;
            if (config.AppSettings.Settings["Storage"].Value == "XmlProvider")
            {
                storageProvider = StorgeProvider.XMLProvider;
            }
            else
            {
                storageProvider = StorgeProvider.SQLProvider;
            }
            string storageDirectory = config.AppSettings.Settings["XmlProvider"].Value;
            string connection = config.AppSettings.Settings["SQLProvider"].Value;
            string connectionString = config.ConnectionStrings.ConnectionStrings[connection].ConnectionString;

            StartUpWindow window = new StartUpWindow(storageProvider, storageDirectory, connectionString);

            if (window.ShowDialog() == DialogResult.OK)
            {
                if (window.Storge == StorgeProvider.XMLProvider)
                {
                    config.AppSettings.Settings["Storage"].Value = "XmlProvider";
                    config.AppSettings.Settings["XmlProvider"].Value = window.StorageDirectory;
                }
                else
                {
                    config.AppSettings.Settings["Storage"].Value = "SQLProvider";
                    config.ConnectionStrings.ConnectionStrings[config.AppSettings.Settings["SQLProvider"].Value].ConnectionString = window.ConnectionString;
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                ConfigurationManager.RefreshSection("connectionStrings");
            }

            loader.InitStorage();
            loader.InitAssemblies();
            FillJobs();
        }

        // Utilities //

        private void InitializeGUIMembers()
        {
            // Список доступных типов аппроксимаций.
            this.ApproximationTypeCmb.Items.AddRange(Enum.GetNames(typeof(ApproximationTypes)));
        }

        private void InitializeConfigurationMembers()
        {
            loader = new StLoader();
        }

        private void InitializeModelNameCmb()
        {
            this.ModelNameCmb.Items.AddRange(StLoader.GetAvailableModelNames());
            this.ModelNameCmb.SelectedIndex = 0;
        }

        private void InitializeCurveLineCmb()
        {
            this.CurveLineCmb.Items.Add(Color.Black);
            this.CurveLineCmb.Items.Add(Color.Blue);
            this.CurveLineCmb.Items.Add(Color.Brown);
            this.CurveLineCmb.Items.Add(Color.Chocolate);
            this.CurveLineCmb.Items.Add(Color.Crimson);
            this.CurveLineCmb.Items.Add(Color.DarkGreen);
            this.CurveLineCmb.Items.Add(Color.DarkOliveGreen);
            this.CurveLineCmb.Items.Add(Color.DarkRed);
            this.CurveLineCmb.Items.Add(Color.DarkViolet);
            this.CurveLineCmb.Items.Add(Color.ForestGreen);

            this.CurveLineCmb.SelectedIndex = 0;
        }

        private void InitializeGenerationParameters()
        {
            generationParamatersControls.Clear();
            this.GenerationParametersGrp.Controls.Clear();

            Type modelType = StLoader.models[this.ModelNameCmb.Text];
            List<RequiredGenerationParam> generationParameters =
                new List<RequiredGenerationParam>((RequiredGenerationParam[])modelType.
                GetCustomAttributes(typeof(RequiredGenerationParam), false));
            generationParameters.Sort(delegate(RequiredGenerationParam arg1, RequiredGenerationParam arg2)
            {
                return arg1.Index.CompareTo(arg2.Index);
            });

            int position = 30;
            int index = 0;
            foreach (RequiredGenerationParam requiredGenerationParam in generationParameters)
            {
                GenerationParamInfo paramInfo =
                    (GenerationParamInfo)(requiredGenerationParam.GenParam.GetType().GetField(
                    requiredGenerationParam.GenParam.ToString()).GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);

                System.Windows.Forms.Label comboBoxLabel = new System.Windows.Forms.Label() { Width = 100 };
                comboBoxLabel.Location = new Point(20, position);
                comboBoxLabel.Text = paramInfo.Name;

                ComboBox control = new ComboBox();
                control.Name = "GenerationParameterCombo";
                control.Width = 150;
                control.Location = new Point(150, position);
                control.TabIndex = index++;
                control.SelectedIndexChanged += new EventHandler(control_SelectedIndexChanged);
                generationParamatersControls[requiredGenerationParam.GenParam] = control;

                this.GenerationParametersGrp.Controls.Add(control);
                this.GenerationParametersGrp.Controls.Add(comboBoxLabel);
                position += 30;
            }
        }

        private void FillJobs()
        {
            this.JobsCmb.Text = "";

            this.JobsCmb.Items.Clear();
            this.JobsCmb.Items.AddRange(loader.GetAvailableJobs());
            if (this.JobsCmb.Items.Count != 0)
                this.JobsCmb.SelectedIndex = 0;
        }

        private void RefreshParameters()
        {
            string name = (string)this.JobsCmb.SelectedItem;
            if (name != null)
            {
                Dictionary<GenerationParam, ComboBox>.KeyCollection keys = generationParamatersControls.Keys;
                foreach (GenerationParam g in keys)
                {
                    generationParamatersControls[g].Text = loader.GetParameterValue(name, g);
                }

                this.RealizationsTxt.Text = loader.GetRealizationCount(name).ToString();
            }
        }

        private void RefreshAssemblies()
        {
            loader.InitAssemblies();
            FillFirstGenerationParameterCombo();
        }

        // CHECK THE LOGIC AND CORRECT TABINDEX PART //
        private void FillFirstGenerationParameterCombo()
        {
            Dictionary<GenerationParam, ComboBox>.KeyCollection keys = generationParamatersControls.Keys;
            foreach (GenerationParam g in keys)
            {
                generationParamatersControls[g].Text = "";
                generationParamatersControls[g].Items.Clear();

                List<string> valuesStr = loader.GetParameterValues(g);
                for (int i = 0; i < valuesStr.Count; ++i)
                    generationParamatersControls[g].Items.Add(valuesStr[i]);

                if (generationParamatersControls[g].Items.Count != 0)
                    generationParamatersControls[g].SelectedIndex = 0;

                break;
            }
        }

        private void FillNextGenerationParameterCombos(int firstComboIndex)
        {
            /*for (int i = firstComboIndex; i < generationParametersComboBoxes.Count; ++i)
            {
                generationParametersComboBoxes[i].Text = "";

                generationParametersComboBoxes[i].Items.Clear();
                Type modelType = StLoader.models[this.ModelNameCmb.Text];
                List<RequiredGenerationParam> generationParameters =
                    new List<RequiredGenerationParam>((RequiredGenerationParam[])modelType.
                    GetCustomAttributes(typeof(RequiredGenerationParam), false));
                List<string> valuesStr = loader.GetParameterValues(Values(firstComboIndex, i),
                    generationParameters[i].GenParam);
                foreach (string v in valuesStr)
                    generationParametersComboBoxes[i].Items.Add(v);
                if (generationParametersComboBoxes[i].Items.Count != 0)
                    generationParametersComboBoxes[i].SelectedIndex = 0;
            }*/
        }

        private Dictionary<GenerationParam, string> Values(int firstIndex, int lastIndex)
        {
            Dictionary<GenerationParam, string> values = new Dictionary<GenerationParam, string>();
            /*for (int i = firstIndex; i < lastIndex; ++i)
            {
                Type modelType = StLoader.models[this.ModelNameCmb.Text];
                List<RequiredGenerationParam> generationParameters =
                    new List<RequiredGenerationParam>((RequiredGenerationParam[])modelType.
                    GetCustomAttributes(typeof(RequiredGenerationParam), false));
                values.Add(generationParameters[i].GenParam, generationParametersComboBoxes[i].Text);
            }*/
            return values;
        }
        // CHECK THE LOGIC AND CORRECT TABINDEX PART //

        private bool ValidateGraphicConditions()
        {
            if (this.ByJobsRadio.Checked == true)
            {
                if(this.JobsCmb.Text == string.Empty)
                {
                    MessageBox.Show("Choose a job!", "Error");
                    return false;
                }
                else
                    return true;
            }
            else
            {
                Dictionary<GenerationParam, ComboBox>.KeyCollection keys = generationParamatersControls.Keys;
                foreach (GenerationParam g in keys)
                {
                    if (generationParamatersControls[g].Text == string.Empty)
                    {
                        MessageBox.Show("Choose generation parameter values!", "Error");
                        return false;
                    }
                }

                return true;
            }
        }

        private List<ResultAssembly> GetAssembliesToAnalyze()
        {
            List<ResultAssembly> res = new List<ResultAssembly>();
            if (this.ByJobsRadio.Checked)
                res.Add(loader.SelectAssemblyByJob(this.JobsCmb.Text));
            else
                res = loader.SelectAssemblyByParameters(Values(0, generationParamatersControls.Count),
                    this.ByAllJobsCheck.Checked);

            return res;
        }

        private List<AnalyseOptions> GetCheckedOptionsGlobal(StAnalyzer analyzer)
        {
            List<AnalyseOptions> checkedOptions = new List<AnalyseOptions>();
            if (this.GlobalPropertiesList.GetItemChecked(0))
            {
                analyzer.options |= AnalyseOptions.AveragePath;
                checkedOptions.Add(AnalyseOptions.AveragePath);
            }
            if (this.GlobalPropertiesList.GetItemChecked(1))
            {
                analyzer.options |= AnalyseOptions.Diameter;
                checkedOptions.Add(AnalyseOptions.Diameter);
            }
            if (this.GlobalPropertiesList.GetItemChecked(2))
            {
                analyzer.options |= AnalyseOptions.ClusteringCoefficient;
                checkedOptions.Add(AnalyseOptions.ClusteringCoefficient);
            }
            if (this.GlobalPropertiesList.GetItemChecked(3))
            {
                analyzer.options |= AnalyseOptions.DegreeDistribution;
                checkedOptions.Add(AnalyseOptions.DegreeDistribution);
            }
            if (this.GlobalPropertiesList.GetItemChecked(4))
            {
                analyzer.options |= AnalyseOptions.Cycles3;
                checkedOptions.Add(AnalyseOptions.Cycles3);
            }
            if (this.GlobalPropertiesList.GetItemChecked(5))
            {
                analyzer.options |= AnalyseOptions.Cycles4;
                checkedOptions.Add(AnalyseOptions.Cycles4);
            }
            if (this.GlobalPropertiesList.GetItemChecked(6))
            {
                analyzer.options |= AnalyseOptions.MaxFullSubgraph;
                checkedOptions.Add(AnalyseOptions.MaxFullSubgraph);
            }
            if (this.GlobalPropertiesList.GetItemChecked(7))
            {
                analyzer.options |= AnalyseOptions.LargestConnectedComponent;
                checkedOptions.Add(AnalyseOptions.LargestConnectedComponent);
            }
            if (this.GlobalPropertiesList.GetItemChecked(8))
            {
                analyzer.options |= AnalyseOptions.MinEigenValue;
                checkedOptions.Add(AnalyseOptions.MinEigenValue);
            }
            if (this.GlobalPropertiesList.GetItemChecked(9))
            {
                analyzer.options |= AnalyseOptions.MaxEigenValue;
                checkedOptions.Add(AnalyseOptions.MaxEigenValue);
            }

            return checkedOptions;
        }

        private List<AnalyseOptions> GetCheckedOptionsLocal(StAnalyzer analyzer)
        {
            List<AnalyseOptions> checkedOptions = new List<AnalyseOptions>();
            if (this.LocalPropertiesList.GetItemChecked(0))
            {
                analyzer.options |= AnalyseOptions.ClusteringCoefficient;
                checkedOptions.Add(AnalyseOptions.ClusteringCoefficient);
            }
            if (this.LocalPropertiesList.GetItemChecked(1))
            {
                analyzer.options |= AnalyseOptions.DegreeDistribution;
                checkedOptions.Add(AnalyseOptions.DegreeDistribution);
            }
            if (this.LocalPropertiesList.GetItemChecked(2))
            {
                analyzer.options |= AnalyseOptions.ConnSubGraph;
                checkedOptions.Add(AnalyseOptions.ConnSubGraph);
            }
            if (this.LocalPropertiesList.GetItemChecked(3))
            {
                analyzer.options |= AnalyseOptions.MinPathDist;
                checkedOptions.Add(AnalyseOptions.MinPathDist);
            }
            if (this.LocalPropertiesList.GetItemChecked(4))
            {
                analyzer.options |= AnalyseOptions.EigenValue;
                checkedOptions.Add(AnalyseOptions.EigenValue);
            }
            if (this.LocalPropertiesList.GetItemChecked(5))
            {
                analyzer.options |= AnalyseOptions.DistEigenPath;
                checkedOptions.Add(AnalyseOptions.DistEigenPath);
            }
            if (this.LocalPropertiesList.GetItemChecked(6))
            {
                analyzer.options |= AnalyseOptions.Cycles;
                checkedOptions.Add(AnalyseOptions.Cycles);
            }

            return checkedOptions;
        }

        private void MakeParameters(StAnalyzer analyzer)
        {
            Dictionary<AnalyseOptions, StAnalyzeOptions> localOptions =
                new Dictionary<AnalyseOptions, StAnalyzeOptions>();
            int index = 0;
            for (int i = 0; i < this.LocalPropertiesList.Items.Count; ++i)
            {
                if (this.LocalPropertiesList.GetItemChecked(i))
                {
                    index = FindIndexByPropertyName(this.LocalPropertiesList.Items[i].ToString());
                    DataGridViewRow row = this.LocalAnalyzeOptionsGrd.Rows[index];
                    double delta = Convert.ToDouble(row.Cells[1].Value), thickening = Convert.ToDouble(row.Cells[2].Value);
                    double value = delta > 0 ? delta : thickening;
                    bool useDelta = delta > 0 ? true : false;

                    AnalyseOptions param = AnalyseOptions.None;
                    switch (i)
                    {
                        case 0:
                            {
                                param = AnalyseOptions.ClusteringCoefficient;
                                break;
                            }
                        case 1:
                            {
                                param = AnalyseOptions.DegreeDistribution;
                                break;
                            }
                        case 2:
                            {
                                param = AnalyseOptions.ConnSubGraph;
                                break;
                            }
                        case 3:
                            {
                                param = AnalyseOptions.MinPathDist;
                                break;
                            }
                        case 4:
                            {
                                param = AnalyseOptions.EigenValue;
                                break;
                            }
                        case 5:
                            {
                                param = AnalyseOptions.DistEigenPath;
                                break;
                            }
                        case 6:
                            {
                                param = AnalyseOptions.Cycles;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    localOptions[param] = new StAnalyzeOptions(useDelta, value);
                }
            }
            analyzer.AnalyzeOptions = localOptions;
        }

        private int FindIndexByPropertyName(string name)
        {
            for (int i = 0; i < this.LocalAnalyzeOptionsGrd.Rows.Count; ++i)
            {
                if (this.LocalAnalyzeOptionsGrd.Rows[i].Cells[0].Value.ToString() == name)
                {
                    return i;
                }
            }

            return -1;
        }

        private void ShowWarning(List<AnalyseOptions> options, StAnalyzeResult result)
        {
            bool show = false;
            string message = "The following options can not be displayed:\n";
            foreach (AnalyseOptions option in options)
            {
                if (!result.result.ContainsKey(option))
                {
                    message += "\n" + option.ToString();
                    show = true;
                }
            }

            if (show == true)
            {
                MessageBox.Show(message);
            }
        }
    }

    public class GraphicCondition
    {
        public bool isOpen;
        public Graphic graphic;

        public GraphicCondition()
        {
            isOpen = false;
            graphic = null;
        }
    }
}
