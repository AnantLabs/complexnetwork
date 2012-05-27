﻿namespace StatisticAnalyzerUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticAnalyzer));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSetProvider = new System.Windows.Forms.ToolStripMenuItem();
            this.saveExcelPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.ModelName = new System.Windows.Forms.Label();
            this.ModelNameCmb = new System.Windows.Forms.ComboBox();
            this.GenerationParametersGrp = new System.Windows.Forms.GroupBox();
            this.ByAllJobsCheck = new System.Windows.Forms.CheckBox();
            this.Param3Cmb = new System.Windows.Forms.ComboBox();
            this.Param3 = new System.Windows.Forms.Label();
            this.Param2Cmb = new System.Windows.Forms.ComboBox();
            this.Param2 = new System.Windows.Forms.Label();
            this.Param1Cmb = new System.Windows.Forms.ComboBox();
            this.Param1 = new System.Windows.Forms.Label();
            this.RealizationsTxt = new System.Windows.Forms.TextBox();
            this.JobsCmb = new System.Windows.Forms.ComboBox();
            this.Jobs = new System.Windows.Forms.Label();
            this.Realizations = new System.Windows.Forms.Label();
            this.CurveLineCmb = new System.Windows.Forms.ComboBox();
            this.CurveLine = new System.Windows.Forms.Label();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.ByJobsRadio = new System.Windows.Forms.RadioButton();
            this.ByParametersRadio = new System.Windows.Forms.RadioButton();
            this.DeleteJob = new System.Windows.Forms.Button();
            this.LocalAnalyzeTab = new System.Windows.Forms.TabPage();
            this.LocalAnalyzeOptionsGrd = new System.Windows.Forms.DataGridView();
            this.PropertyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeltaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThickeningColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApproximationType = new System.Windows.Forms.Label();
            this.ApproximationTypeCmb = new System.Windows.Forms.ComboBox();
            this.LocalPropertiesList = new System.Windows.Forms.CheckedListBox();
            this.LocalDrawGraphics = new System.Windows.Forms.Button();
            this.GlobalAnalyzeTab = new System.Windows.Forms.TabPage();
            this.GlobalPropertiesList = new System.Windows.Forms.CheckedListBox();
            this.GetGlobalResult = new System.Windows.Forms.Button();
            this.GlobalDrawGraphics = new System.Windows.Forms.Button();
            this.StatisticAnalyzeTab = new System.Windows.Forms.TabControl();
            this.MotifAnalyzeTab = new System.Windows.Forms.TabPage();
            this.MotifDrowGraphics = new System.Windows.Forms.Button();
            this.MotifPropertiesList = new System.Windows.Forms.CheckedListBox();
            this.CommonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PointsCheck = new System.Windows.Forms.CheckBox();
            this.GraphicsGrp = new System.Windows.Forms.GroupBox();
            this.GroupByOptionCheck = new System.Windows.Forms.CheckBox();
            this.MenuStrip.SuspendLayout();
            this.GenerationParametersGrp.SuspendLayout();
            this.LocalAnalyzeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalAnalyzeOptionsGrd)).BeginInit();
            this.GlobalAnalyzeTab.SuspendLayout();
            this.StatisticAnalyzeTab.SuspendLayout();
            this.MotifAnalyzeTab.SuspendLayout();
            this.GraphicsGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSettings,
            this.MenuHelp});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(828, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // MenuSettings
            // 
            this.MenuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSetProvider,
            this.saveExcelPageToolStripMenuItem});
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
            // saveExcelPageToolStripMenuItem
            // 
            this.saveExcelPageToolStripMenuItem.Enabled = false;
            this.saveExcelPageToolStripMenuItem.Name = "saveExcelPageToolStripMenuItem";
            this.saveExcelPageToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.saveExcelPageToolStripMenuItem.Text = "Excel Page";
            // 
            // MenuHelp
            // 
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Size = new System.Drawing.Size(41, 20);
            this.MenuHelp.Text = "Help";
            // 
            // ModelName
            // 
            this.ModelName.AutoSize = true;
            this.ModelName.Location = new System.Drawing.Point(28, 51);
            this.ModelName.Name = "ModelName";
            this.ModelName.Size = new System.Drawing.Size(67, 13);
            this.ModelName.TabIndex = 1;
            this.ModelName.Text = "Model Name";
            // 
            // ModelNameCmb
            // 
            this.ModelNameCmb.FormattingEnabled = true;
            this.ModelNameCmb.Location = new System.Drawing.Point(31, 67);
            this.ModelNameCmb.Name = "ModelNameCmb";
            this.ModelNameCmb.Size = new System.Drawing.Size(121, 21);
            this.ModelNameCmb.TabIndex = 2;
            this.CommonToolTip.SetToolTip(this.ModelNameCmb, "Choose the model of random network.");
            this.ModelNameCmb.SelectedIndexChanged += new System.EventHandler(this.ModelNameSelChange);
            // 
            // GenerationParametersGrp
            // 
            this.GenerationParametersGrp.Controls.Add(this.ByAllJobsCheck);
            this.GenerationParametersGrp.Controls.Add(this.Param3Cmb);
            this.GenerationParametersGrp.Controls.Add(this.Param3);
            this.GenerationParametersGrp.Controls.Add(this.Param2Cmb);
            this.GenerationParametersGrp.Controls.Add(this.Param2);
            this.GenerationParametersGrp.Controls.Add(this.Param1Cmb);
            this.GenerationParametersGrp.Controls.Add(this.Param1);
            this.GenerationParametersGrp.Location = new System.Drawing.Point(325, 94);
            this.GenerationParametersGrp.Name = "GenerationParametersGrp";
            this.GenerationParametersGrp.Size = new System.Drawing.Size(473, 124);
            this.GenerationParametersGrp.TabIndex = 4;
            this.GenerationParametersGrp.TabStop = false;
            this.GenerationParametersGrp.Text = "Generation Parameters";
            // 
            // ByAllJobsCheck
            // 
            this.ByAllJobsCheck.AutoSize = true;
            this.ByAllJobsCheck.Location = new System.Drawing.Point(368, 88);
            this.ByAllJobsCheck.Name = "ByAllJobsCheck";
            this.ByAllJobsCheck.Size = new System.Drawing.Size(77, 17);
            this.ByAllJobsCheck.TabIndex = 7;
            this.ByAllJobsCheck.Text = "By All Jobs";
            this.CommonToolTip.SetToolTip(this.ByAllJobsCheck, "Specifies if analyze is by all jobs defined with generation parameters.");
            this.ByAllJobsCheck.UseVisualStyleBackColor = true;
            // 
            // Param3Cmb
            // 
            this.Param3Cmb.FormattingEnabled = true;
            this.Param3Cmb.Location = new System.Drawing.Point(324, 42);
            this.Param3Cmb.Name = "Param3Cmb";
            this.Param3Cmb.Size = new System.Drawing.Size(121, 21);
            this.Param3Cmb.TabIndex = 5;
            this.CommonToolTip.SetToolTip(this.Param3Cmb, "Choose parameter value.");
            // 
            // Param3
            // 
            this.Param3.AutoSize = true;
            this.Param3.Location = new System.Drawing.Point(321, 26);
            this.Param3.Name = "Param3";
            this.Param3.Size = new System.Drawing.Size(43, 13);
            this.Param3.TabIndex = 4;
            this.Param3.Text = "Param3";
            // 
            // Param2Cmb
            // 
            this.Param2Cmb.FormattingEnabled = true;
            this.Param2Cmb.Location = new System.Drawing.Point(179, 42);
            this.Param2Cmb.Name = "Param2Cmb";
            this.Param2Cmb.Size = new System.Drawing.Size(121, 21);
            this.Param2Cmb.TabIndex = 3;
            this.CommonToolTip.SetToolTip(this.Param2Cmb, "Choose parameter value");
            this.Param2Cmb.SelectedIndexChanged += new System.EventHandler(this.Param2Cmb_SelectedIndexChanged);
            // 
            // Param2
            // 
            this.Param2.AutoSize = true;
            this.Param2.Location = new System.Drawing.Point(176, 26);
            this.Param2.Name = "Param2";
            this.Param2.Size = new System.Drawing.Size(43, 13);
            this.Param2.TabIndex = 2;
            this.Param2.Text = "Param2";
            // 
            // Param1Cmb
            // 
            this.Param1Cmb.FormattingEnabled = true;
            this.Param1Cmb.Location = new System.Drawing.Point(32, 42);
            this.Param1Cmb.Name = "Param1Cmb";
            this.Param1Cmb.Size = new System.Drawing.Size(121, 21);
            this.Param1Cmb.TabIndex = 1;
            this.CommonToolTip.SetToolTip(this.Param1Cmb, "Choose parameter value.");
            this.Param1Cmb.SelectedIndexChanged += new System.EventHandler(this.Param1Cmb_SelectedIndexChanged);
            // 
            // Param1
            // 
            this.Param1.AutoSize = true;
            this.Param1.Location = new System.Drawing.Point(29, 26);
            this.Param1.Name = "Param1";
            this.Param1.Size = new System.Drawing.Size(43, 13);
            this.Param1.TabIndex = 0;
            this.Param1.Text = "Param1";
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
            // JobsCmb
            // 
            this.JobsCmb.FormattingEnabled = true;
            this.JobsCmb.Location = new System.Drawing.Point(31, 136);
            this.JobsCmb.Name = "JobsCmb";
            this.JobsCmb.Size = new System.Drawing.Size(121, 21);
            this.JobsCmb.Sorted = true;
            this.JobsCmb.TabIndex = 7;
            this.CommonToolTip.SetToolTip(this.JobsCmb, "Choose a job to work with.");
            this.JobsCmb.SelectedIndexChanged += new System.EventHandler(this.JobsCmb_SelectedIndexChanged);
            // 
            // Jobs
            // 
            this.Jobs.AutoSize = true;
            this.Jobs.Location = new System.Drawing.Point(28, 120);
            this.Jobs.Name = "Jobs";
            this.Jobs.Size = new System.Drawing.Size(29, 13);
            this.Jobs.TabIndex = 6;
            this.Jobs.Text = "Jobs";
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
            this.RefreshBtn.Location = new System.Drawing.Point(325, 51);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(121, 37);
            this.RefreshBtn.TabIndex = 10;
            this.RefreshBtn.Text = "Refresh";
            this.CommonToolTip.SetToolTip(this.RefreshBtn, "Refresh the set of existing assemblies.");
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // ByJobsRadio
            // 
            this.ByJobsRadio.AutoSize = true;
            this.ByJobsRadio.Location = new System.Drawing.Point(210, 51);
            this.ByJobsRadio.Name = "ByJobsRadio";
            this.ByJobsRadio.Size = new System.Drawing.Size(62, 17);
            this.ByJobsRadio.TabIndex = 11;
            this.ByJobsRadio.Text = "By Jobs";
            this.CommonToolTip.SetToolTip(this.ByJobsRadio, "Statistic analyze for the job selected in \"Jobs\" combo box.");
            this.ByJobsRadio.UseVisualStyleBackColor = true;
            this.ByJobsRadio.CheckedChanged += new System.EventHandler(this.ByJobsRadio_CheckedChanged);
            // 
            // ByParametersRadio
            // 
            this.ByParametersRadio.AutoSize = true;
            this.ByParametersRadio.Location = new System.Drawing.Point(210, 74);
            this.ByParametersRadio.Name = "ByParametersRadio";
            this.ByParametersRadio.Size = new System.Drawing.Size(93, 17);
            this.ByParametersRadio.TabIndex = 12;
            this.ByParametersRadio.TabStop = true;
            this.ByParametersRadio.Text = "By Parameters";
            this.CommonToolTip.SetToolTip(this.ByParametersRadio, "Statistic analyze for ansamble defined with generation parameters.");
            this.ByParametersRadio.UseVisualStyleBackColor = true;
            this.ByParametersRadio.CheckedChanged += new System.EventHandler(this.ByParametersRadio_CheckedChanged);
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
            this.LocalAnalyzeTab.Controls.Add(this.LocalAnalyzeOptionsGrd);
            this.LocalAnalyzeTab.Controls.Add(this.ApproximationType);
            this.LocalAnalyzeTab.Controls.Add(this.ApproximationTypeCmb);
            this.LocalAnalyzeTab.Controls.Add(this.LocalPropertiesList);
            this.LocalAnalyzeTab.Controls.Add(this.LocalDrawGraphics);
            this.LocalAnalyzeTab.Location = new System.Drawing.Point(4, 22);
            this.LocalAnalyzeTab.Name = "LocalAnalyzeTab";
            this.LocalAnalyzeTab.Size = new System.Drawing.Size(759, 196);
            this.LocalAnalyzeTab.TabIndex = 2;
            this.LocalAnalyzeTab.Text = "Local Analyze";
            this.LocalAnalyzeTab.UseVisualStyleBackColor = true;
            // 
            // LocalAnalyzeOptionsGrd
            // 
            this.LocalAnalyzeOptionsGrd.AllowUserToAddRows = false;
            this.LocalAnalyzeOptionsGrd.AllowUserToDeleteRows = false;
            this.LocalAnalyzeOptionsGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LocalAnalyzeOptionsGrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropertyNameColumn,
            this.DeltaColumn,
            this.ThickeningColumn});
            this.LocalAnalyzeOptionsGrd.Location = new System.Drawing.Point(207, 26);
            this.LocalAnalyzeOptionsGrd.Name = "LocalAnalyzeOptionsGrd";
            this.LocalAnalyzeOptionsGrd.Size = new System.Drawing.Size(383, 138);
            this.LocalAnalyzeOptionsGrd.TabIndex = 16;
            this.LocalAnalyzeOptionsGrd.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.LocalAnalyzeOptionsGrd_CellEndEdit);
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
            this.ApproximationType.Location = new System.Drawing.Point(611, 26);
            this.ApproximationType.Name = "ApproximationType";
            this.ApproximationType.Size = new System.Drawing.Size(100, 13);
            this.ApproximationType.TabIndex = 38;
            this.ApproximationType.Text = "Approximation Type";
            // 
            // ApproximationTypeCmb
            // 
            this.ApproximationTypeCmb.FormattingEnabled = true;
            this.ApproximationTypeCmb.Items.AddRange(new object[] {
            "None",
            "Degree",
            "Exponential",
            "Gaus"});
            this.ApproximationTypeCmb.Location = new System.Drawing.Point(613, 42);
            this.ApproximationTypeCmb.Name = "ApproximationTypeCmb";
            this.ApproximationTypeCmb.Size = new System.Drawing.Size(121, 21);
            this.ApproximationTypeCmb.TabIndex = 37;
            this.ApproximationTypeCmb.SelectedIndexChanged += new System.EventHandler(this.ApproximationTypeCmb_SelectedIndexChanged);
            // 
            // LocalPropertiesList
            // 
            this.LocalPropertiesList.CheckOnClick = true;
            this.LocalPropertiesList.FormattingEnabled = true;
            this.LocalPropertiesList.Items.AddRange(new object[] {
            "Clustering Coefficient",
            "Degree Distribution",
            "Connected Subgraphs by Order",
            "Distance between Vertices",
            "Eigen Values",
            "Distance between Eigen Values",
            "Cycles"});
            this.LocalPropertiesList.Location = new System.Drawing.Point(13, 25);
            this.LocalPropertiesList.Name = "LocalPropertiesList";
            this.LocalPropertiesList.Size = new System.Drawing.Size(183, 139);
            this.LocalPropertiesList.TabIndex = 36;
            this.LocalPropertiesList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LocalPropertiesList_ItemCheck);
            // 
            // LocalDrawGraphics
            // 
            this.LocalDrawGraphics.Location = new System.Drawing.Point(614, 127);
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
            this.GlobalAnalyzeTab.Controls.Add(this.GlobalPropertiesList);
            this.GlobalAnalyzeTab.Controls.Add(this.GetGlobalResult);
            this.GlobalAnalyzeTab.Controls.Add(this.GlobalDrawGraphics);
            this.GlobalAnalyzeTab.Location = new System.Drawing.Point(4, 22);
            this.GlobalAnalyzeTab.Name = "GlobalAnalyzeTab";
            this.GlobalAnalyzeTab.Padding = new System.Windows.Forms.Padding(3);
            this.GlobalAnalyzeTab.Size = new System.Drawing.Size(759, 196);
            this.GlobalAnalyzeTab.TabIndex = 0;
            this.GlobalAnalyzeTab.Text = "Global Analyze";
            this.GlobalAnalyzeTab.UseVisualStyleBackColor = true;
            // 
            // GlobalPropertiesList
            // 
            this.GlobalPropertiesList.CheckOnClick = true;
            this.GlobalPropertiesList.FormattingEnabled = true;
            this.GlobalPropertiesList.Items.AddRange(new object[] {
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
            this.GlobalPropertiesList.Location = new System.Drawing.Point(13, 25);
            this.GlobalPropertiesList.Name = "GlobalPropertiesList";
            this.GlobalPropertiesList.Size = new System.Drawing.Size(577, 139);
            this.GlobalPropertiesList.TabIndex = 15;
            // 
            // GetGlobalResult
            // 
            this.GetGlobalResult.Location = new System.Drawing.Point(614, 78);
            this.GetGlobalResult.Name = "GetGlobalResult";
            this.GetGlobalResult.Size = new System.Drawing.Size(120, 37);
            this.GetGlobalResult.TabIndex = 12;
            this.GetGlobalResult.Text = "Get Global Result";
            this.CommonToolTip.SetToolTip(this.GetGlobalResult, "Shows the global analyze result graphic.");
            this.GetGlobalResult.UseVisualStyleBackColor = true;
            this.GetGlobalResult.Click += new System.EventHandler(this.GetGlobalResult_Click);
            // 
            // GlobalDrawGraphics
            // 
            this.GlobalDrawGraphics.Location = new System.Drawing.Point(615, 127);
            this.GlobalDrawGraphics.Name = "GlobalDrawGraphics";
            this.GlobalDrawGraphics.Size = new System.Drawing.Size(120, 37);
            this.GlobalDrawGraphics.TabIndex = 6;
            this.GlobalDrawGraphics.Text = "Draw Graphics";
            this.CommonToolTip.SetToolTip(this.GlobalDrawGraphics, "Shows the global analyze result graphic.");
            this.GlobalDrawGraphics.UseVisualStyleBackColor = true;
            this.GlobalDrawGraphics.Click += new System.EventHandler(this.GlobalDrawGraphics_Click);
            // 
            // StatisticAnalyzeTab
            // 
            this.StatisticAnalyzeTab.Controls.Add(this.GlobalAnalyzeTab);
            this.StatisticAnalyzeTab.Controls.Add(this.LocalAnalyzeTab);
            this.StatisticAnalyzeTab.Controls.Add(this.MotifAnalyzeTab);
            this.StatisticAnalyzeTab.Location = new System.Drawing.Point(31, 360);
            this.StatisticAnalyzeTab.Name = "StatisticAnalyzeTab";
            this.StatisticAnalyzeTab.SelectedIndex = 0;
            this.StatisticAnalyzeTab.Size = new System.Drawing.Size(767, 222);
            this.StatisticAnalyzeTab.TabIndex = 9;
            // 
            // MotifAnalyzeTab
            // 
            this.MotifAnalyzeTab.Controls.Add(this.MotifDrowGraphics);
            this.MotifAnalyzeTab.Controls.Add(this.MotifPropertiesList);
            this.MotifAnalyzeTab.Location = new System.Drawing.Point(4, 22);
            this.MotifAnalyzeTab.Name = "MotifAnalyzeTab";
            this.MotifAnalyzeTab.Padding = new System.Windows.Forms.Padding(3);
            this.MotifAnalyzeTab.Size = new System.Drawing.Size(759, 196);
            this.MotifAnalyzeTab.TabIndex = 3;
            this.MotifAnalyzeTab.Text = "Motif Analyze";
            this.MotifAnalyzeTab.UseVisualStyleBackColor = true;
            // 
            // MotifDrowGraphics
            // 
            this.MotifDrowGraphics.Location = new System.Drawing.Point(615, 127);
            this.MotifDrowGraphics.Name = "MotifDrowGraphics";
            this.MotifDrowGraphics.Size = new System.Drawing.Size(120, 37);
            this.MotifDrowGraphics.TabIndex = 17;
            this.MotifDrowGraphics.Text = "Draw Graphics";
            this.CommonToolTip.SetToolTip(this.MotifDrowGraphics, "Shows the global analyze result graphic.");
            this.MotifDrowGraphics.UseVisualStyleBackColor = true;
            this.MotifDrowGraphics.Click += new System.EventHandler(this.MotifDrawGraphics_Click);
            // 
            // MotifPropertiesList
            // 
            this.MotifPropertiesList.CheckOnClick = true;
            this.MotifPropertiesList.FormattingEnabled = true;
            this.MotifPropertiesList.Items.AddRange(new object[] {
            "Motifs of Order 3",
            "Motifs of Order 4",
            "Motifs of Order 5",
            "Motifs of Order 6"});
            this.MotifPropertiesList.Location = new System.Drawing.Point(13, 25);
            this.MotifPropertiesList.Name = "MotifPropertiesList";
            this.MotifPropertiesList.Size = new System.Drawing.Size(577, 139);
            this.MotifPropertiesList.TabIndex = 16;
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
            this.PointsCheck.UseVisualStyleBackColor = true;
            // 
            // GraphicsGrp
            // 
            this.GraphicsGrp.Controls.Add(this.GroupByOptionCheck);
            this.GraphicsGrp.Controls.Add(this.CurveLine);
            this.GraphicsGrp.Controls.Add(this.PointsCheck);
            this.GraphicsGrp.Controls.Add(this.CurveLineCmb);
            this.GraphicsGrp.Location = new System.Drawing.Point(325, 239);
            this.GraphicsGrp.Name = "GraphicsGrp";
            this.GraphicsGrp.Size = new System.Drawing.Size(473, 106);
            this.GraphicsGrp.TabIndex = 16;
            this.GraphicsGrp.TabStop = false;
            this.GraphicsGrp.Text = "Graphics";
            // 
            // GroupByOptionCheck
            // 
            this.GroupByOptionCheck.AutoSize = true;
            this.GroupByOptionCheck.Location = new System.Drawing.Point(267, 44);
            this.GroupByOptionCheck.Name = "GroupByOptionCheck";
            this.GroupByOptionCheck.Size = new System.Drawing.Size(148, 17);
            this.GroupByOptionCheck.TabIndex = 16;
            this.GroupByOptionCheck.Text = "Group Graphics by Option";
            this.GroupByOptionCheck.UseVisualStyleBackColor = true;
            // 
            // StatisticAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(828, 613);
            this.Controls.Add(this.GraphicsGrp);
            this.Controls.Add(this.RealizationsTxt);
            this.Controls.Add(this.DeleteJob);
            this.Controls.Add(this.JobsCmb);
            this.Controls.Add(this.ByParametersRadio);
            this.Controls.Add(this.Jobs);
            this.Controls.Add(this.Realizations);
            this.Controls.Add(this.ByJobsRadio);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.StatisticAnalyzeTab);
            this.Controls.Add(this.GenerationParametersGrp);
            this.Controls.Add(this.ModelNameCmb);
            this.Controls.Add(this.ModelName);
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
            this.GenerationParametersGrp.ResumeLayout(false);
            this.GenerationParametersGrp.PerformLayout();
            this.LocalAnalyzeTab.ResumeLayout(false);
            this.LocalAnalyzeTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalAnalyzeOptionsGrd)).EndInit();
            this.GlobalAnalyzeTab.ResumeLayout(false);
            this.StatisticAnalyzeTab.ResumeLayout(false);
            this.MotifAnalyzeTab.ResumeLayout(false);
            this.GraphicsGrp.ResumeLayout(false);
            this.GraphicsGrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuSettings;
        private System.Windows.Forms.Label ModelName;
        private System.Windows.Forms.ComboBox ModelNameCmb;
        private System.Windows.Forms.GroupBox GenerationParametersGrp;
        private System.Windows.Forms.ComboBox CurveLineCmb;
        private System.Windows.Forms.Label CurveLine;
        private System.Windows.Forms.ToolStripMenuItem MenuSetProvider;
        private System.Windows.Forms.ComboBox Param1Cmb;
        private System.Windows.Forms.Label Param1;
        private System.Windows.Forms.ComboBox Param3Cmb;
        private System.Windows.Forms.Label Param3;
        private System.Windows.Forms.ComboBox Param2Cmb;
        private System.Windows.Forms.Label Param2;
        private System.Windows.Forms.TextBox RealizationsTxt;
        private System.Windows.Forms.Label Realizations;
        private System.Windows.Forms.ComboBox JobsCmb;
        private System.Windows.Forms.Label Jobs;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.RadioButton ByJobsRadio;
        private System.Windows.Forms.RadioButton ByParametersRadio;
        private System.Windows.Forms.Button DeleteJob;
        private System.Windows.Forms.ToolStripMenuItem saveExcelPageToolStripMenuItem;
        private System.Windows.Forms.CheckBox ByAllJobsCheck;
        private System.Windows.Forms.TabPage LocalAnalyzeTab;
        private System.Windows.Forms.Button LocalDrawGraphics;
        private System.Windows.Forms.TabPage GlobalAnalyzeTab;
        private System.Windows.Forms.Button GlobalDrawGraphics;
        private System.Windows.Forms.TabControl StatisticAnalyzeTab;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.ToolTip CommonToolTip;
        private System.Windows.Forms.Button GetGlobalResult;
        private System.Windows.Forms.CheckBox PointsCheck;
        private System.Windows.Forms.CheckedListBox GlobalPropertiesList;
        private System.Windows.Forms.TabPage MotifAnalyzeTab;
        private System.Windows.Forms.CheckedListBox LocalPropertiesList;
        private System.Windows.Forms.Label ApproximationType;
        private System.Windows.Forms.ComboBox ApproximationTypeCmb;
        private System.Windows.Forms.DataGridView LocalAnalyzeOptionsGrd;
        private System.Windows.Forms.Button MotifDrowGraphics;
        private System.Windows.Forms.CheckedListBox MotifPropertiesList;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeltaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThickeningColumn;
        private System.Windows.Forms.GroupBox GraphicsGrp;
        private System.Windows.Forms.CheckBox GroupByOptionCheck;
    }
}
