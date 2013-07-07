namespace StatisticAnalyzerUI
{
    partial class StatisticAnalyzer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticAnalyzer));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSetProvider = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.dBOptimizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.modelName = new System.Windows.Forms.Label();
            this.modelNameCmb = new System.Windows.Forms.ComboBox();
            this.generationParametersGrp = new System.Windows.Forms.GroupBox();
            this.ByAllJobsCheck = new System.Windows.Forms.CheckBox();
            this.RealizationsTxt = new System.Windows.Forms.TextBox();
            this.jobsCmb = new System.Windows.Forms.ComboBox();
            this.jobs = new System.Windows.Forms.Label();
            this.Realizations = new System.Windows.Forms.Label();
            this.CurveLineCmb = new System.Windows.Forms.ComboBox();
            this.CurveLine = new System.Windows.Forms.Label();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.byJobsRadio = new System.Windows.Forms.RadioButton();
            this.byParametersRadio = new System.Windows.Forms.RadioButton();
            this.DeleteJob = new System.Windows.Forms.Button();
            this.LocalAnalyzeTab = new System.Windows.Forms.TabPage();
            this.deselectLocal = new System.Windows.Forms.Button();
            this.selectLocal = new System.Windows.Forms.Button();
            this.localValueButton = new System.Windows.Forms.Button();
            this.localAnalyzeOptionsGrd = new System.Windows.Forms.DataGridView();
            this.PropertyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeltaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThickeningColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApproximationType = new System.Windows.Forms.Label();
            this.ApproximationTypeCmb = new System.Windows.Forms.ComboBox();
            this.localPropertiesList = new System.Windows.Forms.CheckedListBox();
            this.LocalDrawGraphics = new System.Windows.Forms.Button();
            this.GlobalAnalyzeTab = new System.Windows.Forms.TabPage();
            this.deselectGlobal = new System.Windows.Forms.Button();
            this.selectGlobal = new System.Windows.Forms.Button();
            this.valueButton = new System.Windows.Forms.Button();
            this.globalPropertiesList = new System.Windows.Forms.CheckedListBox();
            this.GetGlobalResult = new System.Windows.Forms.Button();
            this.GlobalDrawGraphics = new System.Windows.Forms.Button();
            this.analyzeTabs = new System.Windows.Forms.TabControl();
            this.ExtendedAnalyzeTab = new System.Windows.Forms.TabPage();
            this.deselectExtended = new System.Windows.Forms.Button();
            this.selectExtended = new System.Windows.Forms.Button();
            this.extendedAnalyzeOptionsGrd = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extendedPropertiesList = new System.Windows.Forms.CheckedListBox();
            this.extendedDrawGraphics = new System.Windows.Forms.Button();
            this.CommonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PointsCheck = new System.Windows.Forms.CheckBox();
            this.GraphicsGrp = new System.Windows.Forms.GroupBox();
            this.GroupByOptionCheck = new System.Windows.Forms.CheckBox();
            this.analyzeOptionsParamsGrp = new System.Windows.Forms.GroupBox();
            this.bySecondParamCmb = new System.Windows.Forms.ComboBox();
            this.bySecondParamCheck = new System.Windows.Forms.CheckBox();
            this.byFirstParamCmb = new System.Windows.Forms.ComboBox();
            this.byFirstParamCheck = new System.Windows.Forms.CheckBox();
            this.MenuStrip.SuspendLayout();
            this.LocalAnalyzeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.localAnalyzeOptionsGrd)).BeginInit();
            this.GlobalAnalyzeTab.SuspendLayout();
            this.analyzeTabs.SuspendLayout();
            this.ExtendedAnalyzeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extendedAnalyzeOptionsGrd)).BeginInit();
            this.GraphicsGrp.SuspendLayout();
            this.analyzeOptionsParamsGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSettings,
            this.MenuTools,
            this.MenuHelp});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(848, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // MenuSettings
            // 
            this.MenuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSetProvider});
            this.MenuSettings.Name = "MenuSettings";
            this.MenuSettings.Size = new System.Drawing.Size(57, 20);
            this.MenuSettings.Text = "Settings";
            // 
            // MenuSetProvider
            // 
            this.MenuSetProvider.Name = "MenuSetProvider";
            this.MenuSetProvider.Size = new System.Drawing.Size(223, 22);
            this.MenuSetProvider.Text = "Set/Change Storage Provider...";
            this.MenuSetProvider.Click += new System.EventHandler(this.MenuSetProvider_Click);
            // 
            // MenuTools
            // 
            this.MenuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dBOptimizerToolStripMenuItem});
            this.MenuTools.Name = "MenuTools";
            this.MenuTools.Size = new System.Drawing.Size(45, 20);
            this.MenuTools.Text = "Tools";
            // 
            // dBOptimizerToolStripMenuItem
            // 
            this.dBOptimizerToolStripMenuItem.Name = "dBOptimizerToolStripMenuItem";
            this.dBOptimizerToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.dBOptimizerToolStripMenuItem.Text = "DB Optimizer";
            this.dBOptimizerToolStripMenuItem.Click += new System.EventHandler(this.dBOptimizerToolStripMenuItem_Click);
            // 
            // MenuHelp
            // 
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Size = new System.Drawing.Size(41, 20);
            this.MenuHelp.Text = "Help";
            // 
            // modelName
            // 
            this.modelName.AutoSize = true;
            this.modelName.Location = new System.Drawing.Point(28, 51);
            this.modelName.Name = "modelName";
            this.modelName.Size = new System.Drawing.Size(67, 13);
            this.modelName.TabIndex = 1;
            this.modelName.Text = "Model Name";
            // 
            // modelNameCmb
            // 
            this.modelNameCmb.FormattingEnabled = true;
            this.modelNameCmb.Location = new System.Drawing.Point(31, 67);
            this.modelNameCmb.Name = "modelNameCmb";
            this.modelNameCmb.Size = new System.Drawing.Size(121, 21);
            this.modelNameCmb.TabIndex = 2;
            this.CommonToolTip.SetToolTip(this.modelNameCmb, "Choose the model of random network.");
            this.modelNameCmb.SelectedIndexChanged += new System.EventHandler(this.ModelNameSelChange);
            // 
            // generationParametersGrp
            // 
            this.generationParametersGrp.Location = new System.Drawing.Point(325, 51);
            this.generationParametersGrp.Name = "generationParametersGrp";
            this.generationParametersGrp.Size = new System.Drawing.Size(330, 206);
            this.generationParametersGrp.TabIndex = 4;
            this.generationParametersGrp.TabStop = false;
            this.generationParametersGrp.Text = "Generation Parameters";
            this.CommonToolTip.SetToolTip(this.generationParametersGrp, "Generation parameters for choosed model.");
            // 
            // ByAllJobsCheck
            // 
            this.ByAllJobsCheck.AutoSize = true;
            this.ByAllJobsCheck.Location = new System.Drawing.Point(674, 240);
            this.ByAllJobsCheck.Name = "ByAllJobsCheck";
            this.ByAllJobsCheck.Size = new System.Drawing.Size(77, 17);
            this.ByAllJobsCheck.TabIndex = 7;
            this.ByAllJobsCheck.Text = "By All Jobs";
            this.CommonToolTip.SetToolTip(this.ByAllJobsCheck, "Specifies if analyze is by all jobs defined with generation parameters.");
            this.ByAllJobsCheck.UseVisualStyleBackColor = true;
            // 
            // RealizationsTxt
            // 
            this.RealizationsTxt.Location = new System.Drawing.Point(31, 198);
            this.RealizationsTxt.Name = "RealizationsTxt";
            this.RealizationsTxt.ReadOnly = true;
            this.RealizationsTxt.Size = new System.Drawing.Size(121, 20);
            this.RealizationsTxt.TabIndex = 9;
            this.RealizationsTxt.Text = "0";
            this.RealizationsTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CommonToolTip.SetToolTip(this.RealizationsTxt, "Count of realizations for the job selected in \"Jobs\" combo box.");
            // 
            // jobsCmb
            // 
            this.jobsCmb.FormattingEnabled = true;
            this.jobsCmb.Location = new System.Drawing.Point(31, 136);
            this.jobsCmb.Name = "jobsCmb";
            this.jobsCmb.Size = new System.Drawing.Size(121, 21);
            this.jobsCmb.Sorted = true;
            this.jobsCmb.TabIndex = 7;
            this.CommonToolTip.SetToolTip(this.jobsCmb, "Choose a job to work with.");
            this.jobsCmb.SelectedIndexChanged += new System.EventHandler(this.JobsCmb_SelectedIndexChanged);
            // 
            // jobs
            // 
            this.jobs.AutoSize = true;
            this.jobs.Location = new System.Drawing.Point(28, 120);
            this.jobs.Name = "jobs";
            this.jobs.Size = new System.Drawing.Size(29, 13);
            this.jobs.TabIndex = 6;
            this.jobs.Text = "Jobs";
            // 
            // Realizations
            // 
            this.Realizations.AutoSize = true;
            this.Realizations.Location = new System.Drawing.Point(28, 177);
            this.Realizations.Name = "Realizations";
            this.Realizations.Size = new System.Drawing.Size(64, 13);
            this.Realizations.TabIndex = 8;
            this.Realizations.Text = "Realizations";
            // 
            // CurveLineCmb
            // 
            this.CurveLineCmb.FormattingEnabled = true;
            this.CurveLineCmb.Location = new System.Drawing.Point(32, 44);
            this.CurveLineCmb.Name = "CurveLineCmb";
            this.CurveLineCmb.Size = new System.Drawing.Size(120, 21);
            this.CurveLineCmb.TabIndex = 8;
            this.CommonToolTip.SetToolTip(this.CurveLineCmb, "Choose the color of graphic line.");
            // 
            // CurveLine
            // 
            this.CurveLine.AutoSize = true;
            this.CurveLine.Location = new System.Drawing.Point(29, 28);
            this.CurveLine.Name = "CurveLine";
            this.CurveLine.Size = new System.Drawing.Size(58, 13);
            this.CurveLine.TabIndex = 7;
            this.CurveLine.Text = "Curve Line";
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Location = new System.Drawing.Point(182, 120);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(121, 37);
            this.RefreshBtn.TabIndex = 10;
            this.RefreshBtn.Text = "Refresh";
            this.CommonToolTip.SetToolTip(this.RefreshBtn, "Refresh the set of existing assemblies.");
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // byJobsRadio
            // 
            this.byJobsRadio.AutoSize = true;
            this.byJobsRadio.Checked = true;
            this.byJobsRadio.Location = new System.Drawing.Point(210, 51);
            this.byJobsRadio.Name = "byJobsRadio";
            this.byJobsRadio.Size = new System.Drawing.Size(62, 17);
            this.byJobsRadio.TabIndex = 11;
            this.byJobsRadio.TabStop = true;
            this.byJobsRadio.Text = "By Jobs";
            this.CommonToolTip.SetToolTip(this.byJobsRadio, "Statistic analyze for the job selected in \"Jobs\" combo box.");
            this.byJobsRadio.UseVisualStyleBackColor = true;
            this.byJobsRadio.CheckedChanged += new System.EventHandler(this.ModeRadio_CheckedChanged);
            // 
            // byParametersRadio
            // 
            this.byParametersRadio.AutoSize = true;
            this.byParametersRadio.Location = new System.Drawing.Point(210, 74);
            this.byParametersRadio.Name = "byParametersRadio";
            this.byParametersRadio.Size = new System.Drawing.Size(93, 17);
            this.byParametersRadio.TabIndex = 12;
            this.byParametersRadio.Text = "By Parameters";
            this.CommonToolTip.SetToolTip(this.byParametersRadio, "Statistic analyze for ansamble defined with generation parameters.");
            this.byParametersRadio.UseVisualStyleBackColor = true;
            this.byParametersRadio.CheckedChanged += new System.EventHandler(this.ModeRadio_CheckedChanged);
            // 
            // DeleteJob
            // 
            this.DeleteJob.Location = new System.Drawing.Point(182, 181);
            this.DeleteJob.Name = "DeleteJob";
            this.DeleteJob.Size = new System.Drawing.Size(121, 37);
            this.DeleteJob.TabIndex = 13;
            this.DeleteJob.Text = "Delete Current Job";
            this.CommonToolTip.SetToolTip(this.DeleteJob, "Delete the job selected in \"Jobs\" combo box.");
            this.DeleteJob.UseVisualStyleBackColor = true;
            this.DeleteJob.Click += new System.EventHandler(this.DeleteJob_Click);
            // 
            // LocalAnalyzeTab
            // 
            this.LocalAnalyzeTab.Controls.Add(this.deselectLocal);
            this.LocalAnalyzeTab.Controls.Add(this.selectLocal);
            this.LocalAnalyzeTab.Controls.Add(this.localValueButton);
            this.LocalAnalyzeTab.Controls.Add(this.localAnalyzeOptionsGrd);
            this.LocalAnalyzeTab.Controls.Add(this.ApproximationType);
            this.LocalAnalyzeTab.Controls.Add(this.ApproximationTypeCmb);
            this.LocalAnalyzeTab.Controls.Add(this.localPropertiesList);
            this.LocalAnalyzeTab.Controls.Add(this.LocalDrawGraphics);
            this.LocalAnalyzeTab.Location = new System.Drawing.Point(4, 22);
            this.LocalAnalyzeTab.Name = "LocalAnalyzeTab";
            this.LocalAnalyzeTab.Size = new System.Drawing.Size(759, 264);
            this.LocalAnalyzeTab.TabIndex = 2;
            this.LocalAnalyzeTab.Text = "Local Analyze";
            this.LocalAnalyzeTab.UseVisualStyleBackColor = true;
            // 
            // deselectLocal
            // 
            this.deselectLocal.Location = new System.Drawing.Point(109, 235);
            this.deselectLocal.Name = "deselectLocal";
            this.deselectLocal.Size = new System.Drawing.Size(75, 23);
            this.deselectLocal.TabIndex = 43;
            this.deselectLocal.Text = "Deselect All";
            this.deselectLocal.UseVisualStyleBackColor = true;
            this.deselectLocal.Click += new System.EventHandler(this.deselectAll_Click);
            // 
            // selectLocal
            // 
            this.selectLocal.Location = new System.Drawing.Point(13, 235);
            this.selectLocal.Name = "selectLocal";
            this.selectLocal.Size = new System.Drawing.Size(75, 23);
            this.selectLocal.TabIndex = 42;
            this.selectLocal.Text = "Select All";
            this.selectLocal.UseVisualStyleBackColor = true;
            this.selectLocal.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // localValueButton
            // 
            this.localValueButton.Location = new System.Drawing.Point(613, 144);
            this.localValueButton.Name = "localValueButton";
            this.localValueButton.Size = new System.Drawing.Size(120, 37);
            this.localValueButton.TabIndex = 39;
            this.localValueButton.Text = "Show Values";
            this.CommonToolTip.SetToolTip(this.localValueButton, "Shows the local analyze result values.");
            this.localValueButton.UseVisualStyleBackColor = true;
            this.localValueButton.Click += new System.EventHandler(this.localValueButton_Click);
            // 
            // localAnalyzeOptionsGrd
            // 
            this.localAnalyzeOptionsGrd.AllowUserToAddRows = false;
            this.localAnalyzeOptionsGrd.AllowUserToDeleteRows = false;
            this.localAnalyzeOptionsGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.localAnalyzeOptionsGrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropertyNameColumn,
            this.DeltaColumn,
            this.ThickeningColumn});
            this.localAnalyzeOptionsGrd.Location = new System.Drawing.Point(207, 26);
            this.localAnalyzeOptionsGrd.Name = "localAnalyzeOptionsGrd";
            this.localAnalyzeOptionsGrd.Size = new System.Drawing.Size(383, 198);
            this.localAnalyzeOptionsGrd.TabIndex = 16;
            this.CommonToolTip.SetToolTip(this.localAnalyzeOptionsGrd, "Global analyze options properties.");
            this.localAnalyzeOptionsGrd.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.LocalAnalyzeOptionsGrd_CellEndEdit);
            // 
            // PropertyNameColumn
            // 
            this.PropertyNameColumn.HeaderText = "Property Name";
            this.PropertyNameColumn.Name = "PropertyNameColumn";
            this.PropertyNameColumn.ReadOnly = true;
            this.PropertyNameColumn.Width = 140;
            // 
            // DeltaColumn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "0";
            this.DeltaColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.DeltaColumn.HeaderText = "Delta";
            this.DeltaColumn.Name = "DeltaColumn";
            this.DeltaColumn.Width = 80;
            // 
            // ThickeningColumn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "0";
            this.ThickeningColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ThickeningColumn.HeaderText = "Thickening (in %)";
            this.ThickeningColumn.Name = "ThickeningColumn";
            this.ThickeningColumn.Width = 120;
            // 
            // ApproximationType
            // 
            this.ApproximationType.AutoSize = true;
            this.ApproximationType.Location = new System.Drawing.Point(611, 86);
            this.ApproximationType.Name = "ApproximationType";
            this.ApproximationType.Size = new System.Drawing.Size(100, 13);
            this.ApproximationType.TabIndex = 38;
            this.ApproximationType.Text = "Approximation Type";
            // 
            // ApproximationTypeCmb
            // 
            this.ApproximationTypeCmb.FormattingEnabled = true;
            this.ApproximationTypeCmb.Location = new System.Drawing.Point(613, 102);
            this.ApproximationTypeCmb.Name = "ApproximationTypeCmb";
            this.ApproximationTypeCmb.Size = new System.Drawing.Size(121, 21);
            this.ApproximationTypeCmb.TabIndex = 37;
            this.CommonToolTip.SetToolTip(this.ApproximationTypeCmb, "Choose approximation type.");
            // 
            // localPropertiesList
            // 
            this.localPropertiesList.CheckOnClick = true;
            this.localPropertiesList.FormattingEnabled = true;
            this.localPropertiesList.Items.AddRange(new object[] {
            "Clustering Coefficient",
            "Degree Distribution",
            "Connected Subgraphs by Order",
            "Distance between Vertices",
            "Eigen Values",
            "Distance between Eigen Values",
            "Cycles",
            "Triangle Count by Vertex",
            "Triangle Trajectory"});
            this.localPropertiesList.Location = new System.Drawing.Point(13, 25);
            this.localPropertiesList.Name = "localPropertiesList";
            this.localPropertiesList.Size = new System.Drawing.Size(183, 199);
            this.localPropertiesList.TabIndex = 36;
            this.CommonToolTip.SetToolTip(this.localPropertiesList, "Local analyze options.");
            this.localPropertiesList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LocalPropertiesList_ItemCheck);
            // 
            // LocalDrawGraphics
            // 
            this.LocalDrawGraphics.Location = new System.Drawing.Point(614, 187);
            this.LocalDrawGraphics.Name = "LocalDrawGraphics";
            this.LocalDrawGraphics.Size = new System.Drawing.Size(120, 37);
            this.LocalDrawGraphics.TabIndex = 15;
            this.LocalDrawGraphics.Text = "Draw Graphics";
            this.CommonToolTip.SetToolTip(this.LocalDrawGraphics, "Shows the local analyze result graphic.");
            this.LocalDrawGraphics.UseVisualStyleBackColor = true;
            this.LocalDrawGraphics.Click += new System.EventHandler(this.LocalDrawGraphics_Click);
            // 
            // GlobalAnalyzeTab
            // 
            this.GlobalAnalyzeTab.Controls.Add(this.deselectGlobal);
            this.GlobalAnalyzeTab.Controls.Add(this.selectGlobal);
            this.GlobalAnalyzeTab.Controls.Add(this.valueButton);
            this.GlobalAnalyzeTab.Controls.Add(this.globalPropertiesList);
            this.GlobalAnalyzeTab.Controls.Add(this.GetGlobalResult);
            this.GlobalAnalyzeTab.Controls.Add(this.GlobalDrawGraphics);
            this.GlobalAnalyzeTab.Location = new System.Drawing.Point(4, 22);
            this.GlobalAnalyzeTab.Name = "GlobalAnalyzeTab";
            this.GlobalAnalyzeTab.Padding = new System.Windows.Forms.Padding(3);
            this.GlobalAnalyzeTab.Size = new System.Drawing.Size(759, 264);
            this.GlobalAnalyzeTab.TabIndex = 0;
            this.GlobalAnalyzeTab.Text = "Global Analyze";
            this.GlobalAnalyzeTab.UseVisualStyleBackColor = true;
            // 
            // deselectGlobal
            // 
            this.deselectGlobal.Location = new System.Drawing.Point(109, 235);
            this.deselectGlobal.Name = "deselectGlobal";
            this.deselectGlobal.Size = new System.Drawing.Size(75, 23);
            this.deselectGlobal.TabIndex = 45;
            this.deselectGlobal.Text = "Deselect All";
            this.deselectGlobal.UseVisualStyleBackColor = true;
            this.deselectGlobal.Click += new System.EventHandler(this.deselectAll_Click);
            // 
            // selectGlobal
            // 
            this.selectGlobal.Location = new System.Drawing.Point(13, 235);
            this.selectGlobal.Name = "selectGlobal";
            this.selectGlobal.Size = new System.Drawing.Size(75, 23);
            this.selectGlobal.TabIndex = 44;
            this.selectGlobal.Text = "Select All";
            this.selectGlobal.UseVisualStyleBackColor = true;
            this.selectGlobal.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // valueButton
            // 
            this.valueButton.Location = new System.Drawing.Point(614, 144);
            this.valueButton.Name = "valueButton";
            this.valueButton.Size = new System.Drawing.Size(120, 37);
            this.valueButton.TabIndex = 16;
            this.valueButton.Text = "Show Values";
            this.CommonToolTip.SetToolTip(this.valueButton, "Shows the global analyze result values.");
            this.valueButton.UseVisualStyleBackColor = true;
            this.valueButton.Click += new System.EventHandler(this.valueButton_Click);
            // 
            // globalPropertiesList
            // 
            this.globalPropertiesList.CheckOnClick = true;
            this.globalPropertiesList.FormattingEnabled = true;
            this.globalPropertiesList.Items.AddRange(new object[] {
            "Average Path Length",
            "Diameter",
            "Clustering Coefficient",
            "Degree Distribution",
            "Cycles of Order 3",
            "Cycles of Order 4",
            "Order of Maximal Subgraph",
            "Largest Connected Component",
            "Minimal Eigen Value",
            "Maximal Eigen Value"});
            this.globalPropertiesList.Location = new System.Drawing.Point(13, 25);
            this.globalPropertiesList.Name = "globalPropertiesList";
            this.globalPropertiesList.Size = new System.Drawing.Size(577, 199);
            this.globalPropertiesList.TabIndex = 15;
            this.CommonToolTip.SetToolTip(this.globalPropertiesList, "Global analyze options.");
            // 
            // GetGlobalResult
            // 
            this.GetGlobalResult.Location = new System.Drawing.Point(614, 101);
            this.GetGlobalResult.Name = "GetGlobalResult";
            this.GetGlobalResult.Size = new System.Drawing.Size(120, 37);
            this.GetGlobalResult.TabIndex = 12;
            this.GetGlobalResult.Text = "Get Global Result";
            this.CommonToolTip.SetToolTip(this.GetGlobalResult, "Shows the global analyze result averages.");
            this.GetGlobalResult.UseVisualStyleBackColor = true;
            this.GetGlobalResult.Click += new System.EventHandler(this.GetGlobalResult_Click);
            // 
            // GlobalDrawGraphics
            // 
            this.GlobalDrawGraphics.Location = new System.Drawing.Point(614, 187);
            this.GlobalDrawGraphics.Name = "GlobalDrawGraphics";
            this.GlobalDrawGraphics.Size = new System.Drawing.Size(120, 37);
            this.GlobalDrawGraphics.TabIndex = 6;
            this.GlobalDrawGraphics.Text = "Draw Graphics";
            this.CommonToolTip.SetToolTip(this.GlobalDrawGraphics, "Shows the global analyze result graphic.");
            this.GlobalDrawGraphics.UseVisualStyleBackColor = true;
            this.GlobalDrawGraphics.Click += new System.EventHandler(this.GlobalDrawGraphics_Click);
            // 
            // analyzeTabs
            // 
            this.analyzeTabs.Controls.Add(this.GlobalAnalyzeTab);
            this.analyzeTabs.Controls.Add(this.LocalAnalyzeTab);
            this.analyzeTabs.Controls.Add(this.ExtendedAnalyzeTab);
            this.analyzeTabs.Location = new System.Drawing.Point(31, 360);
            this.analyzeTabs.Name = "analyzeTabs";
            this.analyzeTabs.SelectedIndex = 0;
            this.analyzeTabs.Size = new System.Drawing.Size(767, 290);
            this.analyzeTabs.TabIndex = 9;
            // 
            // ExtendedAnalyzeTab
            // 
            this.ExtendedAnalyzeTab.Controls.Add(this.deselectExtended);
            this.ExtendedAnalyzeTab.Controls.Add(this.selectExtended);
            this.ExtendedAnalyzeTab.Controls.Add(this.extendedAnalyzeOptionsGrd);
            this.ExtendedAnalyzeTab.Controls.Add(this.extendedPropertiesList);
            this.ExtendedAnalyzeTab.Controls.Add(this.extendedDrawGraphics);
            this.ExtendedAnalyzeTab.Location = new System.Drawing.Point(4, 22);
            this.ExtendedAnalyzeTab.Name = "ExtendedAnalyzeTab";
            this.ExtendedAnalyzeTab.Padding = new System.Windows.Forms.Padding(3);
            this.ExtendedAnalyzeTab.Size = new System.Drawing.Size(759, 264);
            this.ExtendedAnalyzeTab.TabIndex = 3;
            this.ExtendedAnalyzeTab.Text = "Extended Analyze";
            this.ExtendedAnalyzeTab.UseVisualStyleBackColor = true;
            // 
            // deselectExtended
            // 
            this.deselectExtended.Location = new System.Drawing.Point(109, 235);
            this.deselectExtended.Name = "deselectExtended";
            this.deselectExtended.Size = new System.Drawing.Size(75, 23);
            this.deselectExtended.TabIndex = 45;
            this.deselectExtended.Text = "Deselect All";
            this.deselectExtended.UseVisualStyleBackColor = true;
            this.deselectExtended.Click += new System.EventHandler(this.deselectAll_Click);
            // 
            // selectExtended
            // 
            this.selectExtended.Location = new System.Drawing.Point(13, 235);
            this.selectExtended.Name = "selectExtended";
            this.selectExtended.Size = new System.Drawing.Size(75, 23);
            this.selectExtended.TabIndex = 44;
            this.selectExtended.Text = "Select All";
            this.selectExtended.UseVisualStyleBackColor = true;
            this.selectExtended.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // extendedAnalyzeOptionsGrd
            // 
            this.extendedAnalyzeOptionsGrd.AllowUserToAddRows = false;
            this.extendedAnalyzeOptionsGrd.AllowUserToDeleteRows = false;
            this.extendedAnalyzeOptionsGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.extendedAnalyzeOptionsGrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.extendedAnalyzeOptionsGrd.Location = new System.Drawing.Point(207, 26);
            this.extendedAnalyzeOptionsGrd.Name = "extendedAnalyzeOptionsGrd";
            this.extendedAnalyzeOptionsGrd.Size = new System.Drawing.Size(383, 198);
            this.extendedAnalyzeOptionsGrd.TabIndex = 42;
            this.CommonToolTip.SetToolTip(this.extendedAnalyzeOptionsGrd, "Global analyze options properties.");
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Property Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 160;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "0";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn2.HeaderText = "Steps to Remove";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 160;
            // 
            // extendedPropertiesList
            // 
            this.extendedPropertiesList.CheckOnClick = true;
            this.extendedPropertiesList.FormattingEnabled = true;
            this.extendedPropertiesList.Items.AddRange(new object[] {
            "Triangle Trajectory"});
            this.extendedPropertiesList.Location = new System.Drawing.Point(13, 25);
            this.extendedPropertiesList.Name = "extendedPropertiesList";
            this.extendedPropertiesList.Size = new System.Drawing.Size(183, 199);
            this.extendedPropertiesList.TabIndex = 41;
            this.CommonToolTip.SetToolTip(this.extendedPropertiesList, "Local analyze options.");
            this.extendedPropertiesList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ExtendedPropertiesList_ItemCheck);
            // 
            // extendedDrawGraphics
            // 
            this.extendedDrawGraphics.Location = new System.Drawing.Point(614, 187);
            this.extendedDrawGraphics.Name = "extendedDrawGraphics";
            this.extendedDrawGraphics.Size = new System.Drawing.Size(120, 37);
            this.extendedDrawGraphics.TabIndex = 17;
            this.extendedDrawGraphics.Text = "Draw Graphics";
            this.CommonToolTip.SetToolTip(this.extendedDrawGraphics, "Shows the extended analyze result graphic.");
            this.extendedDrawGraphics.UseVisualStyleBackColor = true;
            this.extendedDrawGraphics.Click += new System.EventHandler(this.extendedDrawGraphics_Click);
            // 
            // CommonToolTip
            // 
            this.CommonToolTip.IsBalloon = true;
            this.CommonToolTip.ShowAlways = true;
            // 
            // PointsCheck
            // 
            this.PointsCheck.AutoSize = true;
            this.PointsCheck.Location = new System.Drawing.Point(179, 44);
            this.PointsCheck.Name = "PointsCheck";
            this.PointsCheck.Size = new System.Drawing.Size(55, 17);
            this.PointsCheck.TabIndex = 15;
            this.PointsCheck.Text = "Points";
            this.CommonToolTip.SetToolTip(this.PointsCheck, "Line or only points.");
            this.PointsCheck.UseVisualStyleBackColor = true;
            // 
            // GraphicsGrp
            // 
            this.GraphicsGrp.Controls.Add(this.GroupByOptionCheck);
            this.GraphicsGrp.Controls.Add(this.CurveLine);
            this.GraphicsGrp.Controls.Add(this.PointsCheck);
            this.GraphicsGrp.Controls.Add(this.CurveLineCmb);
            this.GraphicsGrp.Location = new System.Drawing.Point(325, 263);
            this.GraphicsGrp.Name = "GraphicsGrp";
            this.GraphicsGrp.Size = new System.Drawing.Size(473, 87);
            this.GraphicsGrp.TabIndex = 16;
            this.GraphicsGrp.TabStop = false;
            this.GraphicsGrp.Text = "Graphics";
            this.CommonToolTip.SetToolTip(this.GraphicsGrp, "Settings for graphics.");
            // 
            // GroupByOptionCheck
            // 
            this.GroupByOptionCheck.AutoSize = true;
            this.GroupByOptionCheck.Location = new System.Drawing.Point(267, 44);
            this.GroupByOptionCheck.Name = "GroupByOptionCheck";
            this.GroupByOptionCheck.Size = new System.Drawing.Size(148, 17);
            this.GroupByOptionCheck.TabIndex = 16;
            this.GroupByOptionCheck.Text = "Group Graphics by Option";
            this.CommonToolTip.SetToolTip(this.GroupByOptionCheck, "Graphics in the one or saparate windows.");
            this.GroupByOptionCheck.UseVisualStyleBackColor = true;
            // 
            // analyzeOptionsParamsGrp
            // 
            this.analyzeOptionsParamsGrp.Controls.Add(this.bySecondParamCmb);
            this.analyzeOptionsParamsGrp.Controls.Add(this.bySecondParamCheck);
            this.analyzeOptionsParamsGrp.Controls.Add(this.byFirstParamCmb);
            this.analyzeOptionsParamsGrp.Controls.Add(this.byFirstParamCheck);
            this.analyzeOptionsParamsGrp.Location = new System.Drawing.Point(668, 51);
            this.analyzeOptionsParamsGrp.Name = "analyzeOptionsParamsGrp";
            this.analyzeOptionsParamsGrp.Size = new System.Drawing.Size(130, 177);
            this.analyzeOptionsParamsGrp.TabIndex = 17;
            this.analyzeOptionsParamsGrp.TabStop = false;
            this.analyzeOptionsParamsGrp.Text = "Analyze Option Params";
            // 
            // bySecondParamCmb
            // 
            this.bySecondParamCmb.Enabled = false;
            this.bySecondParamCmb.FormattingEnabled = true;
            this.bySecondParamCmb.Location = new System.Drawing.Point(5, 121);
            this.bySecondParamCmb.Name = "bySecondParamCmb";
            this.bySecondParamCmb.Size = new System.Drawing.Size(121, 21);
            this.bySecondParamCmb.TabIndex = 3;
            // 
            // bySecondParamCheck
            // 
            this.bySecondParamCheck.AutoSize = true;
            this.bySecondParamCheck.Location = new System.Drawing.Point(6, 98);
            this.bySecondParamCheck.Name = "bySecondParamCheck";
            this.bySecondParamCheck.Size = new System.Drawing.Size(94, 17);
            this.bySecondParamCheck.TabIndex = 2;
            this.bySecondParamCheck.Text = "By Step Count";
            this.bySecondParamCheck.UseVisualStyleBackColor = true;
            this.bySecondParamCheck.CheckedChanged += new System.EventHandler(this.bySecondParamCheck_CheckedChanged);
            // 
            // byFirstParamCmb
            // 
            this.byFirstParamCmb.Enabled = false;
            this.byFirstParamCmb.FormattingEnabled = true;
            this.byFirstParamCmb.Location = new System.Drawing.Point(5, 60);
            this.byFirstParamCmb.Name = "byFirstParamCmb";
            this.byFirstParamCmb.Size = new System.Drawing.Size(121, 21);
            this.byFirstParamCmb.TabIndex = 1;
            // 
            // byFirstParamCheck
            // 
            this.byFirstParamCheck.AutoSize = true;
            this.byFirstParamCheck.Location = new System.Drawing.Point(6, 37);
            this.byFirstParamCheck.Name = "byFirstParamCheck";
            this.byFirstParamCheck.Size = new System.Drawing.Size(56, 17);
            this.byFirstParamCheck.TabIndex = 0;
            this.byFirstParamCheck.Text = "By Mu";
            this.byFirstParamCheck.UseVisualStyleBackColor = true;
            this.byFirstParamCheck.CheckedChanged += new System.EventHandler(this.byFirstParamCheck_CheckedChanged);
            // 
            // StatisticAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(848, 662);
            this.Controls.Add(this.analyzeOptionsParamsGrp);
            this.Controls.Add(this.ByAllJobsCheck);
            this.Controls.Add(this.GraphicsGrp);
            this.Controls.Add(this.RealizationsTxt);
            this.Controls.Add(this.DeleteJob);
            this.Controls.Add(this.jobsCmb);
            this.Controls.Add(this.byParametersRadio);
            this.Controls.Add(this.jobs);
            this.Controls.Add(this.Realizations);
            this.Controls.Add(this.byJobsRadio);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.analyzeTabs);
            this.Controls.Add(this.generationParametersGrp);
            this.Controls.Add(this.modelNameCmb);
            this.Controls.Add(this.modelName);
            this.Controls.Add(this.MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "StatisticAnalyzer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistic Analyzer";
            this.Load += new System.EventHandler(this.OnLoad);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.LocalAnalyzeTab.ResumeLayout(false);
            this.LocalAnalyzeTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.localAnalyzeOptionsGrd)).EndInit();
            this.GlobalAnalyzeTab.ResumeLayout(false);
            this.analyzeTabs.ResumeLayout(false);
            this.ExtendedAnalyzeTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.extendedAnalyzeOptionsGrd)).EndInit();
            this.GraphicsGrp.ResumeLayout(false);
            this.GraphicsGrp.PerformLayout();
            this.analyzeOptionsParamsGrp.ResumeLayout(false);
            this.analyzeOptionsParamsGrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuSettings;
        private System.Windows.Forms.Label modelName;
        private System.Windows.Forms.ComboBox modelNameCmb;
        private System.Windows.Forms.GroupBox generationParametersGrp;
        private System.Windows.Forms.ComboBox CurveLineCmb;
        private System.Windows.Forms.Label CurveLine;
        private System.Windows.Forms.ToolStripMenuItem MenuSetProvider;
        private System.Windows.Forms.TextBox RealizationsTxt;
        private System.Windows.Forms.Label Realizations;
        private System.Windows.Forms.ComboBox jobsCmb;
        private System.Windows.Forms.Label jobs;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.RadioButton byJobsRadio;
        private System.Windows.Forms.RadioButton byParametersRadio;
        private System.Windows.Forms.Button DeleteJob;
        private System.Windows.Forms.CheckBox ByAllJobsCheck;
        private System.Windows.Forms.TabPage LocalAnalyzeTab;
        private System.Windows.Forms.Button LocalDrawGraphics;
        private System.Windows.Forms.TabPage GlobalAnalyzeTab;
        private System.Windows.Forms.Button GlobalDrawGraphics;
        private System.Windows.Forms.TabControl analyzeTabs;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.ToolTip CommonToolTip;
        private System.Windows.Forms.Button GetGlobalResult;
        private System.Windows.Forms.CheckBox PointsCheck;
        private System.Windows.Forms.CheckedListBox globalPropertiesList;
        private System.Windows.Forms.TabPage ExtendedAnalyzeTab;
        private System.Windows.Forms.CheckedListBox localPropertiesList;
        private System.Windows.Forms.Label ApproximationType;
        private System.Windows.Forms.ComboBox ApproximationTypeCmb;
        private System.Windows.Forms.DataGridView localAnalyzeOptionsGrd;
        private System.Windows.Forms.Button extendedDrawGraphics;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeltaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThickeningColumn;
        private System.Windows.Forms.GroupBox GraphicsGrp;
        private System.Windows.Forms.CheckBox GroupByOptionCheck;
        private System.Windows.Forms.Button valueButton;
        private System.Windows.Forms.Button localValueButton;
        private System.Windows.Forms.Button deselectLocal;
        private System.Windows.Forms.Button selectLocal;
        private System.Windows.Forms.Button deselectGlobal;
        private System.Windows.Forms.Button selectGlobal;
        private System.Windows.Forms.GroupBox analyzeOptionsParamsGrp;
        private System.Windows.Forms.ComboBox bySecondParamCmb;
        private System.Windows.Forms.CheckBox bySecondParamCheck;
        private System.Windows.Forms.ComboBox byFirstParamCmb;
        private System.Windows.Forms.CheckBox byFirstParamCheck;
        private System.Windows.Forms.DataGridView extendedAnalyzeOptionsGrd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.CheckedListBox extendedPropertiesList;
        private System.Windows.Forms.ToolStripMenuItem MenuTools;
        private System.Windows.Forms.ToolStripMenuItem dBOptimizerToolStripMenuItem;
        private System.Windows.Forms.Button deselectExtended;
        private System.Windows.Forms.Button selectExtended;
    }
}

