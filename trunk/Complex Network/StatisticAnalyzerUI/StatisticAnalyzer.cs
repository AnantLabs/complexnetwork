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

using SettingsConfiguration;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.StatAnalyzer;
using RandomGraph.Common.Storage;
using CommonLibrary.Model.Attributes;
using CommonLibrary.Model.Result;
using ResultStorage.Storage;
using ZedGraph;
using AnalyzerFramework.Manager.ModelRepo;
using StatisticAnalyzer.Model;

using System.Xml;

namespace StatisticAnalyzerUI
{
    public partial class StatisticAnalyzer : Form
    {
        // Implementation members //
        private ModelStatisticAnalyzer m_analyzer;

        // Configuration members //
        private IResultStorage m_resultStorage;

        // GUI members //
        private ApproximationTypes m_localApproximationType;
        private Dictionary<GraphicalInformation, Graphic> m_existingGraphics;

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

            InitializeModelNameCmb();
            InitializeCurveLineCmb();
        }

        private void ModelNameSelChange(object sender, EventArgs e)
        {
            m_analyzer = new ModelStatisticAnalyzer(this.ModelNameCmb.Text);
            InitializeParameters();

            m_analyzer.LoadAssemblies(m_resultStorage);
            FillJobs();
        }

        // By Jobs mode //
        private void ByJobsRadio_CheckedChanged(object sender, EventArgs e)
        {
            this.JobsCmb.Enabled = true;
            this.DeleteJob.Enabled = true;
            this.RealizationsTxt.Enabled = true;
            this.RefreshBtn.Enabled = false;
            this.Param1Cmb.Enabled = false;
            this.Param2Cmb.Enabled = false;
            this.Param3Cmb.Enabled = false;
            this.ByAllJobsCheck.Enabled = false;

            RefreshParameters();
        }

        private void DeleteJob_Click(object sender, EventArgs e)
        {
            string name = (string)this.JobsCmb.SelectedItem;
            if (name != null)
            {
                m_analyzer.DeleteJob(name, m_resultStorage);
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
            this.RealizationsTxt.Enabled = false;
            this.RefreshBtn.Enabled = true;
            this.Param1Cmb.Enabled = true;
            this.Param2Cmb.Enabled = true;
            this.Param3Cmb.Enabled = true;
            this.ByAllJobsCheck.Enabled = true;

            this.Param1Cmb.Text = this.Param2Cmb.Text = this.Param3Cmb.Text = "";
            this.RealizationsTxt.Text = "0";
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshAssemblies();
        }

        private void Param1Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillParam2();
        }

        private void Param2Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillParam3();
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

        private void ApproximationTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.ApproximationTypeCmb.SelectedIndex)
            {
                case 0:
                    {
                        m_localApproximationType = ApproximationTypes.None;
                        break;
                    }
                case 1:
                    {
                        m_localApproximationType = ApproximationTypes.Degree;
                        break;
                    }
                case 2:
                    {
                        m_localApproximationType = ApproximationTypes.Exponential;
                        break;
                    }
                case 3:
                    {
                        m_localApproximationType = ApproximationTypes.Gaus;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        // Analyzers //
        private void GlobalDrawGraphics_Click(object sender, EventArgs e)
        {
            GlobalAnalyze();

            Dictionary<AnalyseOptions,
                SortedDictionary<double, double>>.KeyCollection keys = m_analyzer.GlobalResults.Keys;
            foreach (AnalyseOptions key in keys)
            {
                GraphicalInformation inform = new GraphicalInformation(key, StatAnalyzeMode.GlobalMode);
                if (!this.GroupByOptionCheck.Checked || m_existingGraphics[inform] == null)
                {
                    m_existingGraphics[inform] = new Graphic((Color)this.CurveLineCmb.SelectedItem, this.PointsCheck.Checked,
                        m_analyzer.GetParameterLine(), inform, m_analyzer.GlobalResults[key], ApproximationTypes.None);
                }
                else
                {
                    m_existingGraphics[inform].SetAll((Color)this.CurveLineCmb.SelectedItem, this.PointsCheck.Checked,
                        m_analyzer.GetParameterLine(), inform, m_analyzer.GlobalResults[key], ApproximationTypes.None);
                }
                m_existingGraphics[inform].m_parent = this;
                m_existingGraphics[inform].modelName = this.ModelNameCmb.Text;

                m_existingGraphics[inform].RefreshGraphic();
                m_existingGraphics[inform].Show();
            }
        }

        private void GetGlobalResult_Click(object sender, EventArgs e)
        {
            GlobalAnalyze();

            Information inform = new Information(m_analyzer.GlobalAverages);
            //inform.m_parameterLine = m_analyzer.GetParameterLine();
            //inform.m_parameterLine += " Size = 1000;";   // correct Size
            //if (this.ByJobsRadio.Checked)
            //    inform.m_parameterLine += " Realization Count = " + this.RealizationsTxt.Text;

            inform.RefreshInformation();
            inform.Show();
        }

        private void LocalDrawGraphics_Click(object sender, EventArgs e)
        {
            m_analyzer.m_localParams = AnalyseOptions.None;
            m_analyzer.JobName = "";

            if (this.ByJobsRadio.Checked)
            {
                m_analyzer.JobName = (string)this.JobsCmb.SelectedItem;
            }

            m_analyzer.SetGenerationParameters(this.Param1Cmb.Text, this.Param2Cmb.Text, this.Param3Cmb.Text);
            m_analyzer.m_approximationType = m_localApproximationType;

            if (this.LocalPropertiesList.GetItemChecked(0))
                m_analyzer.m_localParams |= AnalyseOptions.ClusteringCoefficient;
            if (this.LocalPropertiesList.GetItemChecked(1))
                m_analyzer.m_localParams |= AnalyseOptions.DegreeDistribution;
            if (this.LocalPropertiesList.GetItemChecked(2))
                m_analyzer.m_localParams |= AnalyseOptions.ConnSubGraph;
            if (this.LocalPropertiesList.GetItemChecked(3))
                m_analyzer.m_localParams |= AnalyseOptions.MinPathDist;
            if (this.LocalPropertiesList.GetItemChecked(4))
                m_analyzer.m_localParams |= AnalyseOptions.EigenValue;
            if (this.LocalPropertiesList.GetItemChecked(5))
                m_analyzer.m_localParams |= AnalyseOptions.DistEigenPath;
            if (this.LocalPropertiesList.GetItemChecked(6))
                m_analyzer.m_localParams |= AnalyseOptions.Cycles;

            MakeParameters();

            m_analyzer.LocalAnalyze();
            Dictionary<AnalyseOptions,
                SortedDictionary<double, double>>.KeyCollection keys = m_analyzer.LocalResults.Keys;
            foreach (AnalyseOptions key in keys)
            {
                GraphicalInformation inform = new GraphicalInformation(key, StatAnalyzeMode.LocalMode);
                if (!this.GroupByOptionCheck.Checked || m_existingGraphics[inform] == null)
                {
                    m_existingGraphics[inform] = new Graphic((Color)this.CurveLineCmb.SelectedItem, this.PointsCheck.Checked,
                        m_analyzer.GetParameterLine(), inform, m_analyzer.LocalResults[key], m_localApproximationType);
                }
                else
                {
                    m_existingGraphics[inform].SetAll((Color)this.CurveLineCmb.SelectedItem, this.PointsCheck.Checked,
                        m_analyzer.GetParameterLine(), inform, m_analyzer.GlobalResults[key], m_localApproximationType);
                }
                m_existingGraphics[inform].m_parent = this;
                m_existingGraphics[inform].modelName = this.ModelNameCmb.Text;

                m_existingGraphics[inform].RefreshGraphic();
                m_existingGraphics[inform].Show();
            }
        }

        private void MotifDrawGraphics_Click(object sender, EventArgs e)
        {
        }

        public void DestroyGraphic(GraphicalInformation gr)
        {
            m_existingGraphics[gr] = null;
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

                    m_resultStorage = new XMLResultStorage(window.StorageDirectory);
                }
                else
                {
                    config.AppSettings.Settings["Storage"].Value = "SQLProvider";
                    config.ConnectionStrings.ConnectionStrings[config.AppSettings.Settings["SQLProvider"].Value].ConnectionString = window.ConnectionString;

                    m_resultStorage = new SQLResultStorage(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["SQLProvider"]]);
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                ConfigurationManager.RefreshSection("connectionStrings");
            }

            m_analyzer.LoadAssemblies(m_resultStorage);
            FillJobs();
        }
        
        // Utilities //

        private void InitializeGUIMembers()
        {
            m_existingGraphics = new Dictionary<GraphicalInformation, Graphic>();

            // Global Graphics //
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.AveragePath, StatAnalyzeMode.GlobalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.ClusteringCoefficient, StatAnalyzeMode.GlobalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.CycleEigen3, StatAnalyzeMode.GlobalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.CycleEigen4, StatAnalyzeMode.GlobalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.DegreeDistribution, StatAnalyzeMode.GlobalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.Diameter, StatAnalyzeMode.GlobalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.LargestConnectedComponent, StatAnalyzeMode.GlobalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.MaxEigenValue, StatAnalyzeMode.GlobalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.MinEigenValue, StatAnalyzeMode.GlobalMode)] = null;

            // Local Graphics //
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.ClusteringCoefficient, StatAnalyzeMode.LocalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.ConnSubGraph, StatAnalyzeMode.LocalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.Cycles, StatAnalyzeMode.LocalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.DegreeDistribution, StatAnalyzeMode.LocalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.DistEigenPath, StatAnalyzeMode.LocalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.EigenValue, StatAnalyzeMode.LocalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.FullSubGraph, StatAnalyzeMode.LocalMode)] = null;
            m_existingGraphics[new GraphicalInformation(AnalyseOptions.MinPathDist, StatAnalyzeMode.LocalMode)] = null;

            // Motif Graphics //
        }

        private void InitializeConfigurationMembers()
        {
            // Result Storage Initialization //
            string provider = ConfigurationManager.AppSettings["Storage"];
            if (provider == "XmlProvider")
                m_resultStorage = new XMLResultStorage(ConfigurationManager.AppSettings[provider]);
            else
                m_resultStorage = new SQLResultStorage(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[provider]]);
        }

        private void InitializeModelNameCmb()
        {
            List<Type> availableModelFactoryTypes = ModelRepository.GetInstance().GetAvailableModelFactoryTypes();
            foreach (Type modelFactoryType in availableModelFactoryTypes)
            {
                object[] attr = modelFactoryType.GetCustomAttributes(typeof(TargetGraphModel), false);
                TargetGraphModel targetGraphMetadata = (TargetGraphModel)attr[0];
                Type modelType = targetGraphMetadata.GraphModelType;

                attr = modelType.GetCustomAttributes(typeof(GraphModel), false);
                string modelName = ((GraphModel)attr[0]).Name;

                this.ModelNameCmb.Items.Add(modelName);
            }

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

        private void InitializeParameters()
        {
            InitializeGenerationParameters();
        }

        private void InitializeGenerationParameters()
        {
            this.Param1.Text = m_analyzer.m_statisticParameters.m_param1;
            this.Param2.Text = m_analyzer.m_statisticParameters.m_param2;
            this.Param3.Text = m_analyzer.m_statisticParameters.m_param3;
            this.ByAllJobsCheck.Visible = m_analyzer.ByAllJobsOptionValidation;

            if (this.Param3.Text == "")
            {
                this.Param3.Visible = false;
                this.Param3Cmb.Visible = false;
            }
            else
            {
                this.Param3.Visible = true;
                this.Param3Cmb.Visible = true;
            }
        }

        private void FillJobs()
        {
            this.JobsCmb.Text = "";
            this.RealizationsTxt.Text = "0";

            this.JobsCmb.Items.Clear();
            foreach (ResultAssembly result in m_analyzer.Assemblies)
                this.JobsCmb.Items.Add(result.Name);
            if (this.JobsCmb.Items.Count != 0)
                this.JobsCmb.SelectedIndex = 0;
        }

        private void RefreshParameters()
        {
            string name = (string)this.JobsCmb.SelectedItem;
            if (name != null)
            {
                this.Param1Cmb.Text = m_analyzer.GetParameterValue(name, 1);
                this.Param2Cmb.Text = m_analyzer.GetParameterValue(name, 2);
                this.Param3Cmb.Text = m_analyzer.GetParameterValue(name, 3);
                this.RealizationsTxt.Text = m_analyzer.GetRealizationsCount(name);
            }
        }

        private void RefreshAssemblies()
        {
            m_analyzer.LoadAssemblies(m_resultStorage);
            FillParam1();
        }

        private void FillParam1()
        {
            this.Param1Cmb.Text = "";

            this.Param1Cmb.Items.Clear();
            List<string> valuesStr = m_analyzer.GetParameterValues();
            foreach (string v in valuesStr)
                this.Param1Cmb.Items.Add(v);
            if (this.Param1Cmb.Items.Count != 0)
                this.Param1Cmb.SelectedIndex = 0;
        }

        private void FillParam2()
        {
            this.Param2Cmb.Text = "";

            this.Param2Cmb.Items.Clear();
            List<string> valuesStr = m_analyzer.GetParameterValues(this.Param1Cmb.Text);
            foreach (string v in valuesStr)
                this.Param2Cmb.Items.Add(v);
            if (this.Param2Cmb.Items.Count != 0)
                this.Param2Cmb.SelectedIndex = 0;
        }

        private void FillParam3()
        {
            this.Param3Cmb.Text = "";

            this.Param3Cmb.Items.Clear();
            List<string> valuesStr = m_analyzer.GetParameterValues(this.Param1Cmb.Text, this.Param2Cmb.Text);
            foreach (string v in valuesStr)
                this.Param3Cmb.Items.Add(v);
            if (this.Param3Cmb.Items.Count != 0)
                this.Param3Cmb.SelectedIndex = 0;
        }

        private void GlobalAnalyze()
        {
            m_analyzer.m_globalParams = AnalyseOptions.None;
            m_analyzer.JobName = "";

            if (this.ByJobsRadio.Checked)
            {
                m_analyzer.JobName = (string)this.JobsCmb.SelectedItem;
            }

            m_analyzer.SetGenerationParameters(this.Param1Cmb.Text, this.Param2Cmb.Text, this.Param3Cmb.Text);
            m_analyzer.m_approximationType = ApproximationTypes.None;

            if (this.GlobalPropertiesList.GetItemChecked(0))
                m_analyzer.m_globalParams |= AnalyseOptions.AveragePath;
            if (this.GlobalPropertiesList.GetItemChecked(1))
                m_analyzer.m_globalParams |= AnalyseOptions.Diameter;
            if (this.GlobalPropertiesList.GetItemChecked(2))
                m_analyzer.m_globalParams |= AnalyseOptions.ClusteringCoefficient;
            if (this.GlobalPropertiesList.GetItemChecked(3))
                m_analyzer.m_globalParams |= AnalyseOptions.DegreeDistribution;
            if (this.GlobalPropertiesList.GetItemChecked(4))
                m_analyzer.m_globalParams |= AnalyseOptions.Cycles3;
            if (this.GlobalPropertiesList.GetItemChecked(5))
                m_analyzer.m_globalParams |= AnalyseOptions.Cycles4;
            if (this.GlobalPropertiesList.GetItemChecked(6))
                m_analyzer.m_globalParams |= AnalyseOptions.MaxFullSubgraph;
            if (this.GlobalPropertiesList.GetItemChecked(7))
                m_analyzer.m_globalParams |= AnalyseOptions.LargestConnectedComponent;
            if (this.GlobalPropertiesList.GetItemChecked(8))
                m_analyzer.m_globalParams |= AnalyseOptions.MinEigenValue;
            if (this.GlobalPropertiesList.GetItemChecked(9))
                m_analyzer.m_globalParams |= AnalyseOptions.MaxEigenValue;

            MakeParameters();

            m_analyzer.GlobalAnalyze();
        }

        private void MakeParameters()
        {
            Dictionary<AnalyseOptions, StatAnalyzeOptions> localOptions =
                new Dictionary<AnalyseOptions, StatAnalyzeOptions>();
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

                    localOptions[param] = new StatAnalyzeOptions(useDelta, value);
                }
            }

            m_analyzer.SetAnalyzeParameters(this.ByAllJobsCheck.Checked, localOptions);
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
    }
}
