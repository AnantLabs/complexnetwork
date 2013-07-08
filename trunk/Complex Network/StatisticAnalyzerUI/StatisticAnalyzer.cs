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
    // Реализация формы для статистического анализа.
    // "By Jobs" mode - выбор анализируемой сборки по имени job-а.
    // "By Parameters" mode - выбор множества анализируемых сборок по параметрам генерации.
    // "Global Analyze" tab - статистический анализ глобальных характеристик (свойств).
    // "Local Analyze" tab - статистический анализ локальных ьарактеристик (распределений).
    // "Extended Analyze" tab - статистический анализ средних значений и отклонений.
    // "Extended Analyze" реализован только для свойства Triangle Trajectory (Avg, Sigma).
    public partial class StatisticAnalyzer : Form
    {
        // Организация GUI-части.

        // Control-и и значения параметров генерации (для выбранной модели).
        private Dictionary<GenerationParam, ComboBox> generationParamatersControls =
            new Dictionary<GenerationParam, ComboBox>();

        // Организация функциональной части.

        // Информация о графиках соответственно глобального, локального и расширенного анализа.
        private GraphicCondition globalGraphic;
        private GraphicCondition localGraphic;
        private ExtendedGraphicCondition extendedGraphic;

        // Загрузчик сборок и информации из хранилища данных.
        private AbstractStLoader loader;
        // Анализатор загруженных сборок.
        private AbstractStAnalyzer analyzer;

        // Конструктор по умолчанию.
        public StatisticAnalyzer()
        {
            InitializeComponent();

            InitializeConfigurationMembers();
        }

        // Обработчики сообщений.

        private void OnLoad(object sender, EventArgs e)
        {
            InitializeModelNameCmb();
            InitializeGUIMembers();
        }

        // Выбор/изменение имени модели.
        private void ModelNameSelChange(object sender, EventArgs e)
        {
            FillGenerationParameters();
            this.loader.ModelName = this.modelNameCmb.Text;
            RefreshInformation();
        }

        // Выбор/изменение имени job-а.
        private void JobsCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshParameters();
        }

        // Выбор/изменение mode-а.
        private void ModeRadio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton mode = (RadioButton)sender;
            // Выбор "By Jobs" mode.
            if (mode.Name == "byJobsRadio")
            {
                this.jobsCmb.Enabled = true;
                this.DeleteJob.Enabled = true;
                this.generationParametersGrp.Enabled = false;
                this.analyzeOptionsParamsGrp.Enabled = false;
                this.ByAllJobsCheck.Enabled = false;

                FillJobs();
            }
            // Выбор "By Parameters" mode.
            else
            {
                this.jobsCmb.Enabled = false;
                this.DeleteJob.Enabled = false;
                this.generationParametersGrp.Enabled = true;
                this.analyzeOptionsParamsGrp.Enabled = true;
                this.ByAllJobsCheck.Enabled = true;

                FillParameters();
            }
        }

        // Обнавление сборок (из хранилища данных).
        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshInformation();
        }

        // Удаление сборки по имени job-а (из хранилища данных).
        private void DeleteJob_Click(object sender, EventArgs e)
        {
            if (this.jobsCmb.Text != null)
            {
                this.loader.DeleteJob(this.jobsCmb.Text);
                FillJobs();
            }
        }

        // Выбор/изменение значения параметра генерации.
        private void control_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            FillNextGenerationParameterCombos((int)cmb.Tag);
        }

        // Check/uncheck локального свойства в "Local Analyze" tab-е.
        private void LocalPropertiesList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = 0;
            if (e.NewValue == CheckState.Checked)
            {
                index = this.localAnalyzeOptionsGrd.Rows.Add();
                this.localAnalyzeOptionsGrd.Rows[index].Cells[0].Value =
                    this.localPropertiesList.Items[e.Index].ToString();
                this.localAnalyzeOptionsGrd.Rows[index].Cells[1].Value = 0;
                this.localAnalyzeOptionsGrd.Rows[index].Cells[2].Value = 0;
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                for (int i = 0; i < this.localAnalyzeOptionsGrd.Rows.Count; ++i)
                {
                    if (this.localAnalyzeOptionsGrd.Rows[i].Cells[0].Value.ToString() ==
                        this.localPropertiesList.Items[e.Index].ToString())
                    {
                        index = i;
                        break;
                    }
                }

                this.localAnalyzeOptionsGrd.Rows.RemoveAt(index);
            }
        }

        // Check/uncheck локального свойства в "Extended Analyze" tab-е.
        private void ExtendedPropertiesList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = 0;
            if (e.NewValue == CheckState.Checked)
            {
                index = this.extendedAnalyzeOptionsGrd.Rows.Add();
                this.extendedAnalyzeOptionsGrd.Rows[index].Cells[0].Value =
                    this.extendedPropertiesList.Items[e.Index].ToString();
                this.extendedAnalyzeOptionsGrd.Rows[index].Cells[1].Value = 0;
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                for (int i = 0; i < this.extendedAnalyzeOptionsGrd.Rows.Count; ++i)
                {
                    if (this.extendedAnalyzeOptionsGrd.Rows[i].Cells[0].Value.ToString() ==
                        this.extendedPropertiesList.Items[e.Index].ToString())
                    {
                        index = i;
                        break;
                    }
                }

                this.extendedAnalyzeOptionsGrd.Rows.RemoveAt(index);
            }
        }

        // Ввод значения параметра статистического анализа в "Local Analyze" tab-е.
        private void LocalAnalyzeOptionsGrd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellCollection cells = this.localAnalyzeOptionsGrd.Rows[e.RowIndex].Cells;
            if (cells[e.ColumnIndex].Value.ToString() != "0")
            {
                for (int i = 1; i < cells.Count; ++i)
                {
                    if (i != e.ColumnIndex)
                        cells[i].Value = "0";
                }
            }
        }

        // Check всех свойств для статистического анализа.
        private void selectAll_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "selectGlobal":
                    {
                        for (int i = 0; i < this.globalPropertiesList.Items.Count; ++i)
                        {
                            this.globalPropertiesList.SetItemChecked(i, true);
                        }
                        break;
                    }
                case "selectLocal":
                    {
                        for (int i = 0; i < this.localPropertiesList.Items.Count; ++i)
                        {
                            this.localPropertiesList.SetItemChecked(i, true);
                        }
                        break;
                    }
                case "selectExtended":
                    {
                        for (int i = 0; i < this.extendedPropertiesList.Items.Count; ++i)
                        {
                            this.extendedPropertiesList.SetItemChecked(i, true);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        // Uncheck всех свойств для статистического анализа.
        private void deselectAll_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "deselectGlobal":
                    {
                        for (int i = 0; i < this.globalPropertiesList.Items.Count; ++i)
                        {
                            this.globalPropertiesList.SetItemChecked(i, false);
                        }
                        break;
                    }
                case "deselectLocal":
                    {
                        for (int i = 0; i < this.localPropertiesList.Items.Count; ++i)
                        {
                            this.localPropertiesList.SetItemChecked(i, false);
                        }
                        break;
                    }
                case "deselectExtended":
                    {
                        for (int i = 0; i < this.extendedPropertiesList.Items.Count; ++i)
                        {
                            this.extendedPropertiesList.SetItemChecked(i, false);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        // !Исправить! Анализаторы.
        private void GlobalDrawGraphics_Click(object sender, EventArgs e)
        {
            if (!ValidateGraphicConditions())
                return;

            string provider = ConfigurationManager.AppSettings["Storage"];
            if (provider == "XmlProvider")
            {
                analyzer = new StAnalyzer(GetAssembliesToAnalyze());
            }
            else
            {
                analyzer = new StAnalyzerDB(GetAssembliesToAnalyze());
            }
            List<AnalyseOptions> checkedOptions = GetCheckedOptionsGlobal(analyzer);
            analyzer.GlobalAnalyze();
            if (analyzer.Result.result.Keys.Count == 0)
            {
                MessageBox.Show("There are no results!");
                return;
            }
            StAnalyzeResult stResult = analyzer.Result;
            stResult.type = StAnalyzeType.Global;

            ShowWarning(checkedOptions, stResult);


            if (this.globalGraphic.isOpen)
            {
                if (GroupByOptionCheck.Checked == false)
                {
                    Graphic graphic = new Graphic(stResult, (Color)this.CurveLineCmb.SelectedItem,
                        !PointsCheck.Checked, null);
                    graphic.Show();
                }

                else
                {
                    this.globalGraphic.graphic.Add(stResult, (Color)this.CurveLineCmb.SelectedItem, !PointsCheck.Checked);
                }
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

            string provider = ConfigurationManager.AppSettings["Storage"];
            if (provider == "XmlProvider")
            {
                analyzer = new StAnalyzer(GetAssembliesToAnalyze());
            }
            else
            {
                analyzer = new StAnalyzerDB(GetAssembliesToAnalyze());
            }
            List<AnalyseOptions> checkedOptions = GetCheckedOptionsGlobal(analyzer);
            analyzer.GlobalAnalyze();
            if (analyzer.Result.result.Keys.Count == 0)
            {
                MessageBox.Show("There are no results!");
                return;
            }
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

            string provider = ConfigurationManager.AppSettings["Storage"];
            if (provider == "XmlProvider")
            {
                analyzer = new StAnalyzer(GetAssembliesToAnalyze());
            }
            else
            {
                analyzer = new StAnalyzerDB(GetAssembliesToAnalyze());
            }
            List<AnalyseOptions> checkedOptions = GetCheckedOptionsGlobal(analyzer);
            analyzer.GlobalAnalyze();
            if (analyzer.Result.result.Keys.Count == 0)
            {
                MessageBox.Show("There are no results!");
                return;
            }
            StAnalyzeResult result = analyzer.Result;

            ShowWarning(checkedOptions, analyzer.Result);

            Averages averagesWnd = new Averages(result);
            averagesWnd.Show();
        }

        private void LocalDrawGraphics_Click(object sender, EventArgs e)
        {
            if (!ValidateGraphicConditions())
                return;

            string provider = ConfigurationManager.AppSettings["Storage"];
            if (provider == "XmlProvider")
            {
                analyzer = new StAnalyzer(GetAssembliesToAnalyze());
            }
            else
            {
                analyzer = new StAnalyzerDB(GetAssembliesToAnalyze());
            }
            List<AnalyseOptions> checkedOptions = GetCheckedOptionsLocal(analyzer);
            MakeParameters(analyzer);
            analyzer.LocalAnalyze();
            if (analyzer.Result.result.Keys.Count == 0)
            {
                MessageBox.Show("There are no results!");
                return;
            }
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

                if (GroupByOptionCheck.Checked == false)
                {
                    Graphic graphic = new Graphic(result, (Color)this.CurveLineCmb.SelectedItem,
                                    !PointsCheck.Checked, null);
                    graphic.Show();
                }

                else
                {
                    this.localGraphic.graphic.Add(result, (Color)this.CurveLineCmb.SelectedItem, !PointsCheck.Checked);
                }
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

            string provider = ConfigurationManager.AppSettings["Storage"];
            if (provider == "XmlProvider")
            {
                analyzer = new StAnalyzer(GetAssembliesToAnalyze());
            }
            else
            {
                analyzer = new StAnalyzerDB(GetAssembliesToAnalyze());
            }
            List<AnalyseOptions> checkedOptions = GetCheckedOptionsLocal(analyzer);
            MakeParameters(analyzer);
            analyzer.LocalAnalyze();
            if (analyzer.Result.result.Keys.Count == 0)
            {
                MessageBox.Show("There are no results!");
                return;
            }
            StAnalyzeResult result = analyzer.Result;
            result.type = StAnalyzeType.Local;
            result.approximationType = (ApproximationTypes)Enum.Parse(typeof(ApproximationTypes),
                this.ApproximationTypeCmb.SelectedItem.ToString());

            ShowWarning(checkedOptions, result);

            ValueTable valueTable = new ValueTable(result);
            valueTable.Show();
        }

        private void extendedDrawGraphics_Click(object sender, EventArgs e)
        {
            if (!ValidateGraphicConditions())
                return;

            string provider = ConfigurationManager.AppSettings["Storage"];
            if (provider == "XmlProvider")
            {
                analyzer = new StAnalyzer(GetAssembliesToAnalyze());
            }
            else
            {
                analyzer = new StAnalyzerDB(GetAssembliesToAnalyze());
            }
            analyzer.options |= AnalyseOptions.TriangleTrajectory;
            analyzer.ExtendedAnalyze(Convert.ToUInt32(this.extendedAnalyzeOptionsGrd.Rows[0].Cells[1].Value));
            StAnalyzeResult result = analyzer.Result;
            if (result.trajectoryAvgs.Keys.Count == 0)
            {
                MessageBox.Show("There are no results!");
                return;
            }

            if (this.extendedGraphic.isOpen)
            {
                this.extendedGraphic.graphic.Add(result,
                    (Color)this.CurveLineCmb.SelectedItem,
                    !PointsCheck.Checked);
            }
            else
            {
                this.extendedGraphic.isOpen = true;
                this.extendedGraphic.graphic = new ExtendedGraphic(result,
                    (Color)this.CurveLineCmb.SelectedItem,
                    !PointsCheck.Checked, this.extendedGraphic);
                this.extendedGraphic.graphic.Show();
            }
        }

        // Выбор хранилища данных (Menu->Settings->Set/Change Storage Provider).
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

                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.RefreshSection("connectionStrings");

                    this.loader = new StLoaderXML();
                }
                else
                {
                    config.AppSettings.Settings["Storage"].Value = "SQLProvider";
                    config.ConnectionStrings.ConnectionStrings[config.AppSettings.Settings["SQLProvider"].Value].ConnectionString = window.ConnectionString;

                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.RefreshSection("connectionStrings");

                    this.loader = new StLoaderDB();
                }
            }

            FillGenerationParameters();
            this.loader.ModelName = this.modelNameCmb.Text;
            RefreshInformation();
        }

        // Ручной вызов усреднений для БД (Menu->Tools->DB Optimizer).
        private void dBOptimizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBOptimizer dbOptimizerWnd = new DBOptimizer();
            dbOptimizerWnd.ShowDialog();
        }

        // Утилиты. Загрузка.

        // Инициализация функциональной части.
        private void InitializeConfigurationMembers()
        {
            // Инициализация информации о графиках.
            this.globalGraphic = new GraphicCondition();
            this.localGraphic = new GraphicCondition();
            this.extendedGraphic = new ExtendedGraphicCondition();

            // Инициализация загрузчика по типу хранилища данных.
            if ("XmlProvider" == ConfigurationManager.AppSettings["Storage"])
            {
                this.loader = new StLoaderXML();
            }
            else
            {
                this.loader = new StLoaderDB();
            }
        }

        // Инициализация GUI-части.
        private void InitializeGUIMembers()
        {
            // Список доступных типов аппроксимаций.
            this.ApproximationTypeCmb.Items.AddRange(Enum.GetNames(typeof(ApproximationTypes)));
            this.ApproximationTypeCmb.SelectedIndex = 0;

            // Список доступных цветов графиков.
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

            // Выбран "By Jobs" mode.
            this.jobsCmb.Enabled = true;
            this.DeleteJob.Enabled = true;
            this.generationParametersGrp.Enabled = false;
            this.analyzeOptionsParamsGrp.Enabled = false;
            this.ByAllJobsCheck.Enabled = false;
        }

        // Получения списка доступных имен моделей.
        private void InitializeModelNameCmb()
        {
            this.modelNameCmb.Items.AddRange(AvailableModels.GetAvailableModelNames());
            this.modelNameCmb.SelectedIndex = 0;
        }

        // Получение списка параметров генерации для выбранной модели.
        private void FillGenerationParameters()
        {
            this.generationParamatersControls.Clear();
            this.generationParametersGrp.Controls.Clear();

            Type modelType = AvailableModels.models[this.modelNameCmb.Text];
            List<RequiredGenerationParam> generationParameters =
                new List<RequiredGenerationParam>((RequiredGenerationParam[])modelType.
                GetCustomAttributes(typeof(RequiredGenerationParam), false));
            generationParameters.Sort(delegate(RequiredGenerationParam arg1,
                RequiredGenerationParam arg2)
            {
                return arg1.Index.CompareTo(arg2.Index);
            });

            int position = 30;
            int index = 1;
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
                control.Tag = index++;
                control.SelectedIndexChanged += new EventHandler(control_SelectedIndexChanged);
                generationParamatersControls[requiredGenerationParam.GenParam] = control;

                this.generationParametersGrp.Controls.Add(control);
                this.generationParametersGrp.Controls.Add(comboBoxLabel);
                position += 30;
            }
        }

        // Обновление информации в соответсвтии с выбранным mode-ом.
        private void RefreshInformation()
        {
            // При "By Jobs" mode.
            if (this.byJobsRadio.Checked)
            {
                FillJobs();
            }
            // При "By Parameters" mode.
            else
            {
                FillParameters();
            }
        }

        // Получение списка имен job-ов для выбранной модели.
        private void FillJobs()
        {
            this.jobsCmb.Text = "";

            this.jobsCmb.Items.Clear();
            this.jobsCmb.Items.AddRange(loader.GetAvailableJobNames());
            if (this.jobsCmb.Items.Count != 0)
                this.jobsCmb.SelectedIndex = 0;
        }

        // Получение списков значений параметров генерации для выбранной модели.
        private void FillParameters()
        {
            FillFirstGenerationParameterCombo();
        }

        // Получение/обновление списков значений параметров генерации для выбранного job-а.
        // Получение/обновление числа реализаций для выбранного job-а.
        private void RefreshParameters()
        {
            if (this.jobsCmb.Text != null)
            {
                Dictionary<GenerationParam, ComboBox>.KeyCollection keys =
                    this.generationParamatersControls.Keys;
                foreach (GenerationParam g in keys)
                {
                    this.generationParamatersControls[g].Text =
                        this.loader.GetParameterValue(this.jobsCmb.Text, g);
                }

                this.RealizationsTxt.Text =
                    this.loader.GetRealizationCount(this.jobsCmb.Text).ToString();
            }
        }

        // Получение списка значений для первого параметра генерации.
        private void FillFirstGenerationParameterCombo()
        {
            Dictionary<GenerationParam, ComboBox>.KeyCollection keys =
                this.generationParamatersControls.Keys;
            foreach (GenerationParam g in keys)
            {
                this.generationParamatersControls[g].Text = "";
                this.generationParamatersControls[g].Items.Clear();

                List<string> valuesStr = this.loader.GetParameterValues(g);
                for (int i = 0; i < valuesStr.Count; ++i)
                    this.generationParamatersControls[g].Items.Add(valuesStr[i]);
                if (this.generationParamatersControls[g].Items.Count != 0)
                    this.generationParamatersControls[g].SelectedIndex = 0;

                break;
            }
        }

        // Получение списка значений для последующего параметра генерации.
        private void FillNextGenerationParameterCombos(int firstComboIndex)
        {
            Dictionary<GenerationParam, ComboBox>.KeyCollection keys =
                this.generationParamatersControls.Keys;
            Dictionary<GenerationParam, string> values = new Dictionary<GenerationParam, string>();
            foreach (GenerationParam g in keys)
            {
                if ((int)this.generationParamatersControls[g].Tag <= firstComboIndex)
                {
                    values[g] = this.generationParamatersControls[g].Text;
                    continue;
                }

                this.generationParamatersControls[g].Text = "";
                this.generationParamatersControls[g].Items.Clear();

                List<string> valuesStr = this.loader.GetParameterValues(values, g);

                for (int i = 0; i < valuesStr.Count; ++i)
                    this.generationParamatersControls[g].Items.Add(valuesStr[i]);
                if (this.generationParamatersControls[g].Items.Count != 0)
                    this.generationParamatersControls[g].SelectedIndex = 0;
            }
        }

        // Утилиты. Анализ.

        // ??
        private bool ValidateGraphicConditions()
        {
            if (this.byJobsRadio.Checked == true)
            {
                if (this.jobsCmb.Text == string.Empty)
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

        // Получение списка сборок для анализа.
        // "By Jobs" mode - единственная сборка с выбранным именем job-а.
        // "By Parameters" mode - список сборок по выбранным параметрам (генерации и анализа).
        private List<ResultAssembly> GetAssembliesToAnalyze()
        {
            // При "By Jobs" mode.
            List<ResultAssembly> res = new List<ResultAssembly>();
            if (this.byJobsRadio.Checked)
                res.Add(this.loader.SelectAssemblyByJob(this.jobsCmb.Text));
            // При "By Parameters" mode.
            else
            {
                Dictionary<GenerationParam, string> gValues = new Dictionary<GenerationParam, string>();
                Dictionary<GenerationParam, ComboBox>.KeyCollection keys =
                    this.generationParamatersControls.Keys;
                foreach (GenerationParam g in keys)
                {
                    gValues.Add(g, this.generationParamatersControls[g].Text);
                }

                Dictionary<AnalyzeOptionParam, string> aValues = new Dictionary<AnalyzeOptionParam, string>();
                if (this.byFirstParamCheck.Checked == true)
                    aValues.Add(AnalyzeOptionParam.TrajectoryMu, this.byFirstParamCmb.Text);
                if (this.bySecondParamCheck.Checked == true)
                    aValues.Add(AnalyzeOptionParam.TrajectoryStepCount, this.bySecondParamCmb.Text);

                res = this.loader.SelectAssemblyByParameters(gValues, aValues,
                    this.ByAllJobsCheck.Checked);
            }

            return res;
        }

        private List<AnalyseOptions> GetCheckedOptionsGlobal(AbstractStAnalyzer analyzer)
        {
            List<AnalyseOptions> checkedOptions = new List<AnalyseOptions>();
            if (this.globalPropertiesList.GetItemChecked(0))
            {
                analyzer.options |= AnalyseOptions.AveragePath;
                checkedOptions.Add(AnalyseOptions.AveragePath);
            }
            if (this.globalPropertiesList.GetItemChecked(1))
            {
                analyzer.options |= AnalyseOptions.Diameter;
                checkedOptions.Add(AnalyseOptions.Diameter);
            }
            if (this.globalPropertiesList.GetItemChecked(2))
            {
                analyzer.options |= AnalyseOptions.ClusteringCoefficient;
                checkedOptions.Add(AnalyseOptions.ClusteringCoefficient);
            }
            if (this.globalPropertiesList.GetItemChecked(3))
            {
                analyzer.options |= AnalyseOptions.DegreeDistribution;
                checkedOptions.Add(AnalyseOptions.DegreeDistribution);
            }
            if (this.globalPropertiesList.GetItemChecked(4))
            {
                analyzer.options |= AnalyseOptions.Cycles3;
                checkedOptions.Add(AnalyseOptions.Cycles3);
            }
            if (this.globalPropertiesList.GetItemChecked(5))
            {
                analyzer.options |= AnalyseOptions.Cycles4;
                checkedOptions.Add(AnalyseOptions.Cycles4);
            }
            if (this.globalPropertiesList.GetItemChecked(6))
            {
                analyzer.options |= AnalyseOptions.MaxFullSubgraph;
                checkedOptions.Add(AnalyseOptions.MaxFullSubgraph);
            }
            if (this.globalPropertiesList.GetItemChecked(7))
            {
                analyzer.options |= AnalyseOptions.LargestConnectedComponent;
                checkedOptions.Add(AnalyseOptions.LargestConnectedComponent);
            }
            if (this.globalPropertiesList.GetItemChecked(8))
            {
                analyzer.options |= AnalyseOptions.MinEigenValue;
                checkedOptions.Add(AnalyseOptions.MinEigenValue);
            }
            if (this.globalPropertiesList.GetItemChecked(9))
            {
                analyzer.options |= AnalyseOptions.MaxEigenValue;
                checkedOptions.Add(AnalyseOptions.MaxEigenValue);
            }

            return checkedOptions;
        }

        private List<AnalyseOptions> GetCheckedOptionsLocal(AbstractStAnalyzer analyzer)
        {
            List<AnalyseOptions> checkedOptions = new List<AnalyseOptions>();
            if (this.localPropertiesList.GetItemChecked(0))
            {
                analyzer.options |= AnalyseOptions.ClusteringCoefficient;
                checkedOptions.Add(AnalyseOptions.ClusteringCoefficient);
            }
            if (this.localPropertiesList.GetItemChecked(1))
            {
                analyzer.options |= AnalyseOptions.DegreeDistribution;
                checkedOptions.Add(AnalyseOptions.DegreeDistribution);
            }
            if (this.localPropertiesList.GetItemChecked(2))
            {
                analyzer.options |= AnalyseOptions.ConnSubGraph;
                checkedOptions.Add(AnalyseOptions.ConnSubGraph);
            }
            if (this.localPropertiesList.GetItemChecked(3))
            {
                analyzer.options |= AnalyseOptions.MinPathDist;
                checkedOptions.Add(AnalyseOptions.MinPathDist);
            }
            if (this.localPropertiesList.GetItemChecked(4))
            {
                analyzer.options |= AnalyseOptions.EigenValue;
                checkedOptions.Add(AnalyseOptions.EigenValue);
            }
            if (this.localPropertiesList.GetItemChecked(5))
            {
                analyzer.options |= AnalyseOptions.DistEigenPath;
                checkedOptions.Add(AnalyseOptions.DistEigenPath);
            }
            if (this.localPropertiesList.GetItemChecked(6))
            {
                analyzer.options |= AnalyseOptions.Cycles;
                checkedOptions.Add(AnalyseOptions.Cycles);
            }
            if (this.localPropertiesList.GetItemChecked(7))
            {
                analyzer.options |= AnalyseOptions.TriangleCountByVertex;
                checkedOptions.Add(AnalyseOptions.TriangleCountByVertex);
            }
            if (this.localPropertiesList.GetItemChecked(8))
            {
                analyzer.options |= AnalyseOptions.TriangleTrajectory;
                checkedOptions.Add(AnalyseOptions.TriangleTrajectory);
            }

            return checkedOptions;
        }

        private void MakeParameters(AbstractStAnalyzer analyzer)
        {
            Dictionary<AnalyseOptions, StAnalyzeOptions> localOptions =
                new Dictionary<AnalyseOptions, StAnalyzeOptions>();
            int index = 0;
            for (int i = 0; i < this.localPropertiesList.Items.Count; ++i)
            {
                if (this.localPropertiesList.GetItemChecked(i))
                {
                    index = FindIndexByPropertyName(this.localPropertiesList.Items[i].ToString());
                    DataGridViewRow row = this.localAnalyzeOptionsGrd.Rows[index];
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
                        case 7:
                            {
                                param = AnalyseOptions.TriangleCountByVertex;
                                break;
                            }
                        case 8:
                            {
                                param = AnalyseOptions.TriangleTrajectory;
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
            for (int i = 0; i < this.localAnalyzeOptionsGrd.Rows.Count; ++i)
            {
                if (this.localAnalyzeOptionsGrd.Rows[i].Cells[0].Value.ToString() == name)
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

        private void byFirstParamCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.byFirstParamCheck.Checked == true)
            {
                this.byFirstParamCmb.Enabled = true;
                Dictionary<GenerationParam, ComboBox>.KeyCollection keys = generationParamatersControls.Keys;
                Dictionary<GenerationParam, string> values = new Dictionary<GenerationParam, string>();
                foreach (GenerationParam g in keys)
                {
                    values[g] = generationParamatersControls[g].Text;
                }

                List<string> v = loader.GetOptionParameterValues(values, AnalyzeOptionParam.TrajectoryMu);
                for (int i = 0; i < v.Count; ++i)
                    this.byFirstParamCmb.Items.Add(v[i]);
                if (this.byFirstParamCmb.Items.Count != 0)
                    this.byFirstParamCmb.SelectedIndex = 0;
            }
            else
            {
                this.byFirstParamCmb.Items.Clear();
                this.byFirstParamCmb.Enabled = false;
            }
        }

        private void bySecondParamCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bySecondParamCheck.Checked == true)
            {
                this.bySecondParamCmb.Enabled = true;
                Dictionary<GenerationParam, ComboBox>.KeyCollection keys = generationParamatersControls.Keys;
                Dictionary<GenerationParam, string> values = new Dictionary<GenerationParam, string>();
                foreach (GenerationParam g in keys)
                {
                    values[g] = generationParamatersControls[g].Text;
                }

                List<string> v = loader.GetOptionParameterValues(values, AnalyzeOptionParam.TrajectoryStepCount);
                for (int i = 0; i < v.Count; ++i)
                    this.bySecondParamCmb.Items.Add(v[i]);
                if (this.bySecondParamCmb.Items.Count != 0)
                    this.bySecondParamCmb.SelectedIndex = 0;
            }
            else
            {
                this.bySecondParamCmb.Items.Clear();
                this.bySecondParamCmb.Enabled = false;
            }
        }
    }

    public class GraphicCondition
    {
        public bool isOpen;
        public Graphic graphic;
    }

    public class ExtendedGraphicCondition
    {
        public bool isOpen;
        public ExtendedGraphic graphic;
    }
}
