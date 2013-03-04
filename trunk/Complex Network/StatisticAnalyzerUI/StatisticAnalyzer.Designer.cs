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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticAnalyzer));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSetProvider = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extendedAnalyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.ModelName = new System.Windows.Forms.Label();
            this.ModelNameCmb = new System.Windows.Forms.ComboBox();
            this.GenerationParametersGrp = new System.Windows.Forms.GroupBox();
            this.ByAllJobsCheck = new System.Windows.Forms.CheckBox();
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
            this.deselectLocal = new System.Windows.Forms.Button();
            this.selectLocal = new System.Windows.Forms.Button();
            this.localValueButton = new System.Windows.Forms.Button();
            this.LocalAnalyzeOptionsGrd = new System.Windows.Forms.DataGridView();
            this.PropertyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeltaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThickeningColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApproximationType = new System.Windows.Forms.Label();
            this.ApproximationTypeCmb = new System.Windows.Forms.ComboBox();
            this.LocalPropertiesList = new System.Windows.Forms.CheckedListBox();
            this.LocalDrawGraphics = new System.Windows.Forms.Button();
            this.GlobalAnalyzeTab = new System.Windows.Forms.TabPage();
            this.deselectGlobal = new System.Windows.Forms.Button();
            this.selectGlobal = new System.Windows.Forms.Button();
            this.valueButton = new System.Windows.Forms.Button();
            this.GlobalPropertiesList = new System.Windows.Forms.CheckedListBox();
            this.GetGlobalResult = new System.Windows.Forms.Button();
            this.GlobalDrawGraphics = new System.Windows.Forms.Button();
            this.selectTabControl = new System.Windows.Forms.TabControl();
            this.MotifAnalyzeTab = new System.Windows.Forms.TabPage();
            this.MotifDrowGraphics = new System.Windows.Forms.Button();
            this.MotifPropertiesList = new System.Windows.Forms.CheckedListBox();
            this.CommonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PointsCheck = new System.Windows.Forms.CheckBox();
            this.GraphicsGrp = new System.Windows.Forms.GroupBox();
            this.GroupByOptionCheck = new System.Windows.Forms.CheckBox();
            this.MenuStrip.SuspendLayout();
            this.LocalAnalyzeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalAnalyzeOptionsGrd)).BeginInit();
            this.GlobalAnalyzeTab.SuspendLayout();
            this.selectTabControl.SuspendLayout();
            this.MotifAnalyzeTab.SuspendLayout();
            this.GraphicsGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSettings,
            this.toolsToolStripMenuItem,
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
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extendedAnalyzeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // extendedAnalyzeToolStripMenuItem
            // 
            this.extendedAnalyzeToolStripMenuItem.Name = "extendedAnalyzeToolStripMenuItem";
            this.extendedAnalyzeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.extendedAnalyzeToolStripMenuItem.Text = "Extended Analyze";
            this.extendedAnalyzeToolStripMenuItem.Click += new System.EventHandler(this.extendedAnalyzeToolStripMenuItem_Click);
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
            this.GenerationParametersGrp.Location = new System.Drawing.Point(325, 51);
            this.GenerationParametersGrp.Name = "GenerationParametersGrp";
            this.GenerationParametersGrp.Size = new System.Drawing.Size(374, 206);
            this.GenerationParametersGrp.TabIndex = 4;
            this.GenerationParametersGrp.TabStop = false;
            this.GenerationParametersGrp.Text = "Generation Parameters";
            this.CommonToolTip.SetToolTip(this.GenerationParametersGrp, "Generation parameters for choosed model.");
            // 
            // ByAllJobsCheck
            // 
            this.ByAllJobsCheck.AutoSize = true;
            this.ByAllJobsCheck.Location = new System.Drawing.Point(717, 240);
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
            this.RefreshBtn.Location = new System.Drawing.Point(182, 120);
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
            this.LocalAnalyzeTab.Controls.Add(this.deselectLocal);
            this.LocalAnalyzeTab.Controls.Add(this.selectLocal);
            this.LocalAnalyzeTab.Controls.Add(this.localValueButton);
            this.LocalAnalyzeTab.Controls.Add(this.LocalAnalyzeOptionsGrd);
            this.LocalAnalyzeTab.Controls.Add(this.ApproximationType);
            this.LocalAnalyzeTab.Controls.Add(this.ApproximationTypeCmb);
            this.LocalAnalyzeTab.Controls.Add(this.LocalPropertiesList);
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
            this.deselectLocal.Location = new System.Drawing.Point(109, 238);
            this.deselectLocal.Name = "deselectLocal";
            this.deselectLocal.Size = new System.Drawing.Size(75, 23);
            this.deselectLocal.TabIndex = 43;
            this.deselectLocal.Text = "Deselect All";
            this.deselectLocal.UseVisualStyleBackColor = true;
            this.deselectLocal.Click += new System.EventHandler(this.deselectLocal_Click);
            // 
            // selectLocal
            // 
            this.selectLocal.Location = new System.Drawing.Point(13, 238);
            this.selectLocal.Name = "selectLocal";
            this.selectLocal.Size = new System.Drawing.Size(75, 23);
            this.selectLocal.TabIndex = 42;
            this.selectLocal.Text = "Select All";
            this.selectLocal.UseVisualStyleBackColor = true;
            this.selectLocal.Click += new System.EventHandler(this.selectLocal_Click);
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
            this.LocalAnalyzeOptionsGrd.Size = new System.Drawing.Size(383, 198);
            this.LocalAnalyzeOptionsGrd.TabIndex = 16;
            this.CommonToolTip.SetToolTip(this.LocalAnalyzeOptionsGrd, "Global analyze options properties.");
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
            "Cycles",
            "Triangle Trajectory"});
            this.LocalPropertiesList.Location = new System.Drawing.Point(13, 25);
            this.LocalPropertiesList.Name = "LocalPropertiesList";
            this.LocalPropertiesList.Size = new System.Drawing.Size(183, 199);
            this.LocalPropertiesList.TabIndex = 36;
            this.CommonToolTip.SetToolTip(this.LocalPropertiesList, "Local analyze options.");
            this.LocalPropertiesList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LocalPropertiesList_ItemCheck);
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
            this.GlobalAnalyzeTab.Controls.Add(this.GlobalPropertiesList);
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
            this.deselectGlobal.Click += new System.EventHandler(this.deselectGlobal_Click);
            // 
            // selectGlobal
            // 
            this.selectGlobal.Location = new System.Drawing.Point(13, 235);
            this.selectGlobal.Name = "selectGlobal";
            this.selectGlobal.Size = new System.Drawing.Size(75, 23);
            this.selectGlobal.TabIndex = 44;
            this.selectGlobal.Text = "Select All";
            this.selectGlobal.UseVisualStyleBackColor = true;
            this.selectGlobal.Click += new System.EventHandler(this.selectGlobal_Click);
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
            this.GlobalPropertiesList.Size = new System.Drawing.Size(577, 199);
            this.GlobalPropertiesList.TabIndex = 15;
            this.CommonToolTip.SetToolTip(this.GlobalPropertiesList, "Global analyze options.");
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
            // selectTabControl
            // 
            this.selectTabControl.Controls.Add(this.GlobalAnalyzeTab);
            this.selectTabControl.Controls.Add(this.LocalAnalyzeTab);
            this.selectTabControl.Controls.Add(this.MotifAnalyzeTab);
            this.selectTabControl.Location = new System.Drawing.Point(31, 360);
            this.selectTabControl.Name = "selectTabControl";
            this.selectTabControl.SelectedIndex = 0;
            this.selectTabControl.Size = new System.Drawing.Size(767, 290);
            this.selectTabControl.TabIndex = 9;
            // 
            // MotifAnalyzeTab
            // 
            this.MotifAnalyzeTab.Controls.Add(this.MotifDrowGraphics);
            this.MotifAnalyzeTab.Controls.Add(this.MotifPropertiesList);
            this.MotifAnalyzeTab.Location = new System.Drawing.Point(4, 22);
            this.MotifAnalyzeTab.Name = "MotifAnalyzeTab";
            this.MotifAnalyzeTab.Padding = new System.Windows.Forms.Padding(3);
            this.MotifAnalyzeTab.Size = new System.Drawing.Size(759, 264);
            this.MotifAnalyzeTab.TabIndex = 3;
            this.MotifAnalyzeTab.Text = "Motif Analyze";
            this.MotifAnalyzeTab.UseVisualStyleBackColor = true;
            // 
            // MotifDrowGraphics
            // 
            this.MotifDrowGraphics.Location = new System.Drawing.Point(614, 187);
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
            this.MotifPropertiesList.Size = new System.Drawing.Size(577, 199);
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
            // StatisticAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(848, 662);
            this.Controls.Add(this.ByAllJobsCheck);
            this.Controls.Add(this.GraphicsGrp);
            this.Controls.Add(this.RealizationsTxt);
            this.Controls.Add(this.DeleteJob);
            this.Controls.Add(this.JobsCmb);
            this.Controls.Add(this.ByParametersRadio);
            this.Controls.Add(this.Jobs);
            this.Controls.Add(this.Realizations);
            this.Controls.Add(this.ByJobsRadio);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.selectTabControl);
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
            this.LocalAnalyzeTab.ResumeLayout(false);
            this.LocalAnalyzeTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalAnalyzeOptionsGrd)).EndInit();
            this.GlobalAnalyzeTab.ResumeLayout(false);
            this.selectTabControl.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox RealizationsTxt;
        private System.Windows.Forms.Label Realizations;
        private System.Windows.Forms.ComboBox JobsCmb;
        private System.Windows.Forms.Label Jobs;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.RadioButton ByJobsRadio;
        private System.Windows.Forms.RadioButton ByParametersRadio;
        private System.Windows.Forms.Button DeleteJob;
        private System.Windows.Forms.CheckBox ByAllJobsCheck;
        private System.Windows.Forms.TabPage LocalAnalyzeTab;
        private System.Windows.Forms.Button LocalDrawGraphics;
        private System.Windows.Forms.TabPage GlobalAnalyzeTab;
        private System.Windows.Forms.Button GlobalDrawGraphics;
        private System.Windows.Forms.TabControl selectTabControl;
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
        private System.Windows.Forms.Button valueButton;
        private System.Windows.Forms.Button localValueButton;
        private System.Windows.Forms.Button deselectLocal;
        private System.Windows.Forms.Button selectLocal;
        private System.Windows.Forms.Button deselectGlobal;
        private System.Windows.Forms.Button selectGlobal;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extendedAnalyzeToolStripMenuItem;
    }
}

