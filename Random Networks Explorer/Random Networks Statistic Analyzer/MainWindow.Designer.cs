namespace Random_Networks_Statistic_Analyzer
{
    partial class MainWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.researchesGrp = new System.Windows.Forms.GroupBox();
            this.refresh = new System.Windows.Forms.Button();
            this.researchesTable = new System.Windows.Forms.DataGridView();
            this.researchNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.researchRealizationCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.researchSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.researchDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelTypeCmb = new System.Windows.Forms.ComboBox();
            this.modelType = new System.Windows.Forms.Label();
            this.researchTypeCmb = new System.Windows.Forms.ComboBox();
            this.researchType = new System.Windows.Forms.Label();
            this.statisticAnalyzeGrp = new System.Windows.Forms.GroupBox();
            this.analyzeTabs = new System.Windows.Forms.TabControl();
            this.GlobalAnalyzeTab = new System.Windows.Forms.TabPage();
            this.deselectGlobal = new System.Windows.Forms.Button();
            this.selectGlobal = new System.Windows.Forms.Button();
            this.valueButton = new System.Windows.Forms.Button();
            this.globalPropertiesList = new System.Windows.Forms.CheckedListBox();
            this.GetGlobalResult = new System.Windows.Forms.Button();
            this.GlobalDrawGraphics = new System.Windows.Forms.Button();
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
            this.detailsGrp = new System.Windows.Forms.GroupBox();
            this.parametersTable = new System.Windows.Forms.DataGridView();
            this.parameterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parameterValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainMenu.SuspendLayout();
            this.researchesGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.researchesTable)).BeginInit();
            this.statisticAnalyzeGrp.SuspendLayout();
            this.analyzeTabs.SuspendLayout();
            this.GlobalAnalyzeTab.SuspendLayout();
            this.LocalAnalyzeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.localAnalyzeOptionsGrd)).BeginInit();
            this.detailsGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parametersTable)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1025, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFromToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadFromToolStripMenuItem
            // 
            this.loadFromToolStripMenuItem.Name = "loadFromToolStripMenuItem";
            this.loadFromToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.loadFromToolStripMenuItem.Text = "Load from...";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // researchesGrp
            // 
            this.researchesGrp.Controls.Add(this.refresh);
            this.researchesGrp.Controls.Add(this.researchesTable);
            this.researchesGrp.Controls.Add(this.modelTypeCmb);
            this.researchesGrp.Controls.Add(this.modelType);
            this.researchesGrp.Controls.Add(this.researchTypeCmb);
            this.researchesGrp.Controls.Add(this.researchType);
            this.researchesGrp.Location = new System.Drawing.Point(12, 27);
            this.researchesGrp.Name = "researchesGrp";
            this.researchesGrp.Size = new System.Drawing.Size(617, 409);
            this.researchesGrp.TabIndex = 1;
            this.researchesGrp.TabStop = false;
            this.researchesGrp.Text = "Loaded Researches";
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(490, 23);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(121, 21);
            this.refresh.TabIndex = 10;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            // 
            // researchesTable
            // 
            this.researchesTable.AllowUserToAddRows = false;
            this.researchesTable.AllowUserToDeleteRows = false;
            this.researchesTable.AllowUserToResizeColumns = false;
            this.researchesTable.AllowUserToResizeRows = false;
            this.researchesTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.researchesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.researchesTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.researchNameColumn,
            this.researchRealizationCountColumn,
            this.researchSizeColumn,
            this.researchDateColumn});
            this.researchesTable.Location = new System.Drawing.Point(9, 50);
            this.researchesTable.Name = "researchesTable";
            this.researchesTable.ReadOnly = true;
            this.researchesTable.RowHeadersVisible = false;
            this.researchesTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.researchesTable.Size = new System.Drawing.Size(602, 352);
            this.researchesTable.TabIndex = 9;
            // 
            // researchNameColumn
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchNameColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.researchNameColumn.HeaderText = "Name";
            this.researchNameColumn.Name = "researchNameColumn";
            this.researchNameColumn.ReadOnly = true;
            this.researchNameColumn.Width = 150;
            // 
            // researchRealizationCountColumn
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchRealizationCountColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.researchRealizationCountColumn.HeaderText = "Realization Count";
            this.researchRealizationCountColumn.Name = "researchRealizationCountColumn";
            this.researchRealizationCountColumn.ReadOnly = true;
            this.researchRealizationCountColumn.Width = 150;
            // 
            // researchSizeColumn
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchSizeColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.researchSizeColumn.HeaderText = "Network Size";
            this.researchSizeColumn.Name = "researchSizeColumn";
            this.researchSizeColumn.ReadOnly = true;
            this.researchSizeColumn.Width = 150;
            // 
            // researchDateColumn
            // 
            this.researchDateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchDateColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.researchDateColumn.HeaderText = "Date";
            this.researchDateColumn.Name = "researchDateColumn";
            this.researchDateColumn.ReadOnly = true;
            // 
            // modelTypeCmb
            // 
            this.modelTypeCmb.FormattingEnabled = true;
            this.modelTypeCmb.Location = new System.Drawing.Point(305, 23);
            this.modelTypeCmb.Name = "modelTypeCmb";
            this.modelTypeCmb.Size = new System.Drawing.Size(121, 21);
            this.modelTypeCmb.TabIndex = 8;
            // 
            // modelType
            // 
            this.modelType.AutoSize = true;
            this.modelType.Location = new System.Drawing.Point(236, 26);
            this.modelType.Name = "modelType";
            this.modelType.Size = new System.Drawing.Size(63, 13);
            this.modelType.TabIndex = 7;
            this.modelType.Text = "Model Type";
            // 
            // researchTypeCmb
            // 
            this.researchTypeCmb.FormattingEnabled = true;
            this.researchTypeCmb.Location = new System.Drawing.Point(92, 23);
            this.researchTypeCmb.Name = "researchTypeCmb";
            this.researchTypeCmb.Size = new System.Drawing.Size(121, 21);
            this.researchTypeCmb.TabIndex = 6;
            // 
            // researchType
            // 
            this.researchType.AutoSize = true;
            this.researchType.Location = new System.Drawing.Point(6, 26);
            this.researchType.Name = "researchType";
            this.researchType.Size = new System.Drawing.Size(80, 13);
            this.researchType.TabIndex = 5;
            this.researchType.Text = "Research Type";
            // 
            // statisticAnalyzeGrp
            // 
            this.statisticAnalyzeGrp.Controls.Add(this.analyzeTabs);
            this.statisticAnalyzeGrp.Location = new System.Drawing.Point(12, 442);
            this.statisticAnalyzeGrp.Name = "statisticAnalyzeGrp";
            this.statisticAnalyzeGrp.Size = new System.Drawing.Size(1001, 314);
            this.statisticAnalyzeGrp.TabIndex = 2;
            this.statisticAnalyzeGrp.TabStop = false;
            this.statisticAnalyzeGrp.Text = "Statistic Analyze Options";
            // 
            // analyzeTabs
            // 
            this.analyzeTabs.Controls.Add(this.GlobalAnalyzeTab);
            this.analyzeTabs.Controls.Add(this.LocalAnalyzeTab);
            this.analyzeTabs.Location = new System.Drawing.Point(9, 19);
            this.analyzeTabs.Name = "analyzeTabs";
            this.analyzeTabs.SelectedIndex = 0;
            this.analyzeTabs.Size = new System.Drawing.Size(748, 295);
            this.analyzeTabs.TabIndex = 11;
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
            this.GlobalAnalyzeTab.Size = new System.Drawing.Size(740, 269);
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
            // 
            // selectGlobal
            // 
            this.selectGlobal.Location = new System.Drawing.Point(13, 235);
            this.selectGlobal.Name = "selectGlobal";
            this.selectGlobal.Size = new System.Drawing.Size(75, 23);
            this.selectGlobal.TabIndex = 44;
            this.selectGlobal.Text = "Select All";
            this.selectGlobal.UseVisualStyleBackColor = true;
            // 
            // valueButton
            // 
            this.valueButton.Location = new System.Drawing.Point(614, 144);
            this.valueButton.Name = "valueButton";
            this.valueButton.Size = new System.Drawing.Size(120, 37);
            this.valueButton.TabIndex = 16;
            this.valueButton.Text = "Show Values";
            this.valueButton.UseVisualStyleBackColor = true;
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
            "Cycles of Order 5",
            "Order of Maximal Subgraph",
            "Largest Connected Component",
            "Minimal Eigen Value",
            "Maximal Eigen Value"});
            this.globalPropertiesList.Location = new System.Drawing.Point(13, 25);
            this.globalPropertiesList.Name = "globalPropertiesList";
            this.globalPropertiesList.Size = new System.Drawing.Size(577, 199);
            this.globalPropertiesList.TabIndex = 15;
            // 
            // GetGlobalResult
            // 
            this.GetGlobalResult.Location = new System.Drawing.Point(614, 101);
            this.GetGlobalResult.Name = "GetGlobalResult";
            this.GetGlobalResult.Size = new System.Drawing.Size(120, 37);
            this.GetGlobalResult.TabIndex = 12;
            this.GetGlobalResult.Text = "Get Global Result";
            this.GetGlobalResult.UseVisualStyleBackColor = true;
            // 
            // GlobalDrawGraphics
            // 
            this.GlobalDrawGraphics.Location = new System.Drawing.Point(614, 187);
            this.GlobalDrawGraphics.Name = "GlobalDrawGraphics";
            this.GlobalDrawGraphics.Size = new System.Drawing.Size(120, 37);
            this.GlobalDrawGraphics.TabIndex = 6;
            this.GlobalDrawGraphics.Text = "Draw Graphics";
            this.GlobalDrawGraphics.UseVisualStyleBackColor = true;
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
            this.LocalAnalyzeTab.Size = new System.Drawing.Size(740, 269);
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
            // 
            // selectLocal
            // 
            this.selectLocal.Location = new System.Drawing.Point(13, 235);
            this.selectLocal.Name = "selectLocal";
            this.selectLocal.Size = new System.Drawing.Size(75, 23);
            this.selectLocal.TabIndex = 42;
            this.selectLocal.Text = "Select All";
            this.selectLocal.UseVisualStyleBackColor = true;
            // 
            // localValueButton
            // 
            this.localValueButton.Location = new System.Drawing.Point(613, 144);
            this.localValueButton.Name = "localValueButton";
            this.localValueButton.Size = new System.Drawing.Size(120, 37);
            this.localValueButton.TabIndex = 39;
            this.localValueButton.Text = "Show Values";
            this.localValueButton.UseVisualStyleBackColor = true;
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
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.NullValue = "0";
            this.DeltaColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.DeltaColumn.HeaderText = "Delta";
            this.DeltaColumn.Name = "DeltaColumn";
            this.DeltaColumn.Width = 80;
            // 
            // ThickeningColumn
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.NullValue = "0";
            this.ThickeningColumn.DefaultCellStyle = dataGridViewCellStyle12;
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
            // 
            // LocalDrawGraphics
            // 
            this.LocalDrawGraphics.Location = new System.Drawing.Point(614, 187);
            this.LocalDrawGraphics.Name = "LocalDrawGraphics";
            this.LocalDrawGraphics.Size = new System.Drawing.Size(120, 37);
            this.LocalDrawGraphics.TabIndex = 15;
            this.LocalDrawGraphics.Text = "Draw Graphics";
            this.LocalDrawGraphics.UseVisualStyleBackColor = true;
            // 
            // detailsGrp
            // 
            this.detailsGrp.Controls.Add(this.parametersTable);
            this.detailsGrp.Location = new System.Drawing.Point(635, 27);
            this.detailsGrp.Name = "detailsGrp";
            this.detailsGrp.Size = new System.Drawing.Size(378, 409);
            this.detailsGrp.TabIndex = 3;
            this.detailsGrp.TabStop = false;
            this.detailsGrp.Text = "Details";
            // 
            // parametersTable
            // 
            this.parametersTable.AllowUserToAddRows = false;
            this.parametersTable.AllowUserToDeleteRows = false;
            this.parametersTable.AllowUserToResizeColumns = false;
            this.parametersTable.AllowUserToResizeRows = false;
            this.parametersTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.parametersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.parametersTable.ColumnHeadersVisible = false;
            this.parametersTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.parameterNameColumn,
            this.parameterValueColumn});
            this.parametersTable.Location = new System.Drawing.Point(6, 23);
            this.parametersTable.Name = "parametersTable";
            this.parametersTable.ReadOnly = true;
            this.parametersTable.RowHeadersVisible = false;
            this.parametersTable.Size = new System.Drawing.Size(366, 379);
            this.parametersTable.TabIndex = 0;
            // 
            // parameterNameColumn
            // 
            this.parameterNameColumn.HeaderText = "Name";
            this.parameterNameColumn.Name = "parameterNameColumn";
            this.parameterNameColumn.ReadOnly = true;
            this.parameterNameColumn.Width = 150;
            // 
            // parameterValueColumn
            // 
            this.parameterValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.parameterValueColumn.HeaderText = "Value";
            this.parameterValueColumn.Name = "parameterValueColumn";
            this.parameterValueColumn.ReadOnly = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 762);
            this.Controls.Add(this.detailsGrp);
            this.Controls.Add(this.statisticAnalyzeGrp);
            this.Controls.Add(this.researchesGrp);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainWindow";
            this.Text = "Random Networks Statistic Analyzer";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.researchesGrp.ResumeLayout(false);
            this.researchesGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.researchesTable)).EndInit();
            this.statisticAnalyzeGrp.ResumeLayout(false);
            this.analyzeTabs.ResumeLayout(false);
            this.GlobalAnalyzeTab.ResumeLayout(false);
            this.LocalAnalyzeTab.ResumeLayout(false);
            this.LocalAnalyzeTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.localAnalyzeOptionsGrd)).EndInit();
            this.detailsGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.parametersTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.GroupBox researchesGrp;
        private System.Windows.Forms.DataGridView researchesTable;
        private System.Windows.Forms.ComboBox modelTypeCmb;
        private System.Windows.Forms.Label modelType;
        private System.Windows.Forms.ComboBox researchTypeCmb;
        private System.Windows.Forms.Label researchType;
        private System.Windows.Forms.DataGridViewTextBoxColumn researchNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn researchRealizationCountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn researchSizeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn researchDateColumn;
        private System.Windows.Forms.GroupBox statisticAnalyzeGrp;
        private System.Windows.Forms.TabControl analyzeTabs;
        private System.Windows.Forms.TabPage GlobalAnalyzeTab;
        private System.Windows.Forms.Button deselectGlobal;
        private System.Windows.Forms.Button selectGlobal;
        private System.Windows.Forms.Button valueButton;
        private System.Windows.Forms.CheckedListBox globalPropertiesList;
        private System.Windows.Forms.Button GetGlobalResult;
        private System.Windows.Forms.Button GlobalDrawGraphics;
        private System.Windows.Forms.TabPage LocalAnalyzeTab;
        private System.Windows.Forms.Button deselectLocal;
        private System.Windows.Forms.Button selectLocal;
        private System.Windows.Forms.Button localValueButton;
        private System.Windows.Forms.DataGridView localAnalyzeOptionsGrd;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeltaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThickeningColumn;
        private System.Windows.Forms.Label ApproximationType;
        private System.Windows.Forms.ComboBox ApproximationTypeCmb;
        private System.Windows.Forms.CheckedListBox localPropertiesList;
        private System.Windows.Forms.Button LocalDrawGraphics;
        private System.Windows.Forms.GroupBox detailsGrp;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.DataGridView parametersTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterValueColumn;
    }
}

