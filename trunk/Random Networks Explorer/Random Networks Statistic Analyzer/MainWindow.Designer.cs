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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.researchTableCSM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteResearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDeselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelTypeCmb = new System.Windows.Forms.ComboBox();
            this.modelType = new System.Windows.Forms.Label();
            this.researchTypeCmb = new System.Windows.Forms.ComboBox();
            this.researchType = new System.Windows.Forms.Label();
            this.statisticAnalyzeGrp = new System.Windows.Forms.GroupBox();
            this.optionsTabs = new System.Windows.Forms.TabControl();
            this.distributedOptionsTab = new System.Windows.Forms.TabPage();
            this.trajectoryOptionsTab = new System.Windows.Forms.TabPage();
            this.detailsGrp = new System.Windows.Forms.GroupBox();
            this.parametersTable = new System.Windows.Forms.DataGridView();
            this.parameterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parameterValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueButton = new System.Windows.Forms.Button();
            this.GlobalDrawGraphics = new System.Windows.Forms.Button();
            this.selectAll = new System.Windows.Forms.Button();
            this.deselectAll = new System.Windows.Forms.Button();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.approximationColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.thickeningColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deltaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.distributedOptionsTable = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.globalOptionsTab = new System.Windows.Forms.TabPage();
            this.checkedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.optionNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.globalOptionsTable = new System.Windows.Forms.DataGridView();
            this.drawOptionsGrp = new System.Windows.Forms.GroupBox();
            this.colorDlg = new System.Windows.Forms.ColorDialog();
            this.color = new System.Windows.Forms.Button();
            this.pointsCheck = new System.Windows.Forms.CheckBox();
            this.groupCheck = new System.Windows.Forms.CheckBox();
            this.lineColor = new System.Windows.Forms.Label();
            this.mainMenu.SuspendLayout();
            this.researchesGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.researchesTable)).BeginInit();
            this.researchTableCSM.SuspendLayout();
            this.statisticAnalyzeGrp.SuspendLayout();
            this.optionsTabs.SuspendLayout();
            this.distributedOptionsTab.SuspendLayout();
            this.trajectoryOptionsTab.SuspendLayout();
            this.detailsGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parametersTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distributedOptionsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.globalOptionsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.globalOptionsTable)).BeginInit();
            this.drawOptionsGrp.SuspendLayout();
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
            this.loadFromToolStripMenuItem.Click += new System.EventHandler(this.loadFromToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // researchesGrp
            // 
            this.researchesGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.researchesGrp.Controls.Add(this.refresh);
            this.researchesGrp.Controls.Add(this.researchesTable);
            this.researchesGrp.Controls.Add(this.modelTypeCmb);
            this.researchesGrp.Controls.Add(this.modelType);
            this.researchesGrp.Controls.Add(this.researchTypeCmb);
            this.researchesGrp.Controls.Add(this.researchType);
            this.researchesGrp.Location = new System.Drawing.Point(12, 27);
            this.researchesGrp.Name = "researchesGrp";
            this.researchesGrp.Size = new System.Drawing.Size(617, 369);
            this.researchesGrp.TabIndex = 1;
            this.researchesGrp.TabStop = false;
            this.researchesGrp.Text = "Loaded Researches";
            // 
            // refresh
            // 
            this.refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh.Location = new System.Drawing.Point(532, 23);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(79, 23);
            this.refresh.TabIndex = 10;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // researchesTable
            // 
            this.researchesTable.AllowUserToAddRows = false;
            this.researchesTable.AllowUserToDeleteRows = false;
            this.researchesTable.AllowUserToResizeColumns = false;
            this.researchesTable.AllowUserToResizeRows = false;
            this.researchesTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.researchesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.researchesTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.researchNameColumn,
            this.researchRealizationCountColumn,
            this.researchSizeColumn,
            this.researchDateColumn});
            this.researchesTable.ContextMenuStrip = this.researchTableCSM;
            this.researchesTable.Location = new System.Drawing.Point(9, 50);
            this.researchesTable.Name = "researchesTable";
            this.researchesTable.ReadOnly = true;
            this.researchesTable.RowHeadersVisible = false;
            this.researchesTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.researchesTable.Size = new System.Drawing.Size(602, 312);
            this.researchesTable.TabIndex = 9;
            this.researchesTable.SelectionChanged += new System.EventHandler(this.researchesTable_SelectionChanged);
            // 
            // researchNameColumn
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchNameColumn.DefaultCellStyle = dataGridViewCellStyle21;
            this.researchNameColumn.HeaderText = "Name";
            this.researchNameColumn.Name = "researchNameColumn";
            this.researchNameColumn.ReadOnly = true;
            this.researchNameColumn.Width = 150;
            // 
            // researchRealizationCountColumn
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchRealizationCountColumn.DefaultCellStyle = dataGridViewCellStyle22;
            this.researchRealizationCountColumn.HeaderText = "Realization Count";
            this.researchRealizationCountColumn.Name = "researchRealizationCountColumn";
            this.researchRealizationCountColumn.ReadOnly = true;
            this.researchRealizationCountColumn.Width = 150;
            // 
            // researchSizeColumn
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchSizeColumn.DefaultCellStyle = dataGridViewCellStyle23;
            this.researchSizeColumn.HeaderText = "Network Size";
            this.researchSizeColumn.Name = "researchSizeColumn";
            this.researchSizeColumn.ReadOnly = true;
            this.researchSizeColumn.Width = 150;
            // 
            // researchDateColumn
            // 
            this.researchDateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchDateColumn.DefaultCellStyle = dataGridViewCellStyle24;
            this.researchDateColumn.HeaderText = "Date";
            this.researchDateColumn.Name = "researchDateColumn";
            this.researchDateColumn.ReadOnly = true;
            // 
            // researchTableCSM
            // 
            this.researchTableCSM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteResearchToolStripMenuItem,
            this.selectDeselectAllToolStripMenuItem});
            this.researchTableCSM.Name = "researchTableCSM";
            this.researchTableCSM.Size = new System.Drawing.Size(158, 48);
            // 
            // deleteResearchToolStripMenuItem
            // 
            this.deleteResearchToolStripMenuItem.Name = "deleteResearchToolStripMenuItem";
            this.deleteResearchToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.deleteResearchToolStripMenuItem.Text = "Delete Research";
            this.deleteResearchToolStripMenuItem.Click += new System.EventHandler(this.deleteResearchToolStripMenuItem_Click);
            // 
            // selectDeselectAllToolStripMenuItem
            // 
            this.selectDeselectAllToolStripMenuItem.Name = "selectDeselectAllToolStripMenuItem";
            this.selectDeselectAllToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.selectDeselectAllToolStripMenuItem.Text = "Select All";
            this.selectDeselectAllToolStripMenuItem.Click += new System.EventHandler(this.selectDeselectAllToolStripMenuItem_Click);
            // 
            // modelTypeCmb
            // 
            this.modelTypeCmb.FormattingEnabled = true;
            this.modelTypeCmb.Location = new System.Drawing.Point(305, 23);
            this.modelTypeCmb.Name = "modelTypeCmb";
            this.modelTypeCmb.Size = new System.Drawing.Size(121, 21);
            this.modelTypeCmb.TabIndex = 8;
            this.modelTypeCmb.SelectedIndexChanged += new System.EventHandler(this.modelTypeCmb_SelectedIndexChanged);
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
            this.researchTypeCmb.SelectedIndexChanged += new System.EventHandler(this.researchTypeCmb_SelectedIndexChanged);
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
            this.statisticAnalyzeGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statisticAnalyzeGrp.Controls.Add(this.drawOptionsGrp);
            this.statisticAnalyzeGrp.Controls.Add(this.selectAll);
            this.statisticAnalyzeGrp.Controls.Add(this.deselectAll);
            this.statisticAnalyzeGrp.Controls.Add(this.valueButton);
            this.statisticAnalyzeGrp.Controls.Add(this.GlobalDrawGraphics);
            this.statisticAnalyzeGrp.Controls.Add(this.optionsTabs);
            this.statisticAnalyzeGrp.Location = new System.Drawing.Point(12, 402);
            this.statisticAnalyzeGrp.Name = "statisticAnalyzeGrp";
            this.statisticAnalyzeGrp.Size = new System.Drawing.Size(1001, 308);
            this.statisticAnalyzeGrp.TabIndex = 2;
            this.statisticAnalyzeGrp.TabStop = false;
            this.statisticAnalyzeGrp.Text = "Statistic Analyze Options";
            // 
            // optionsTabs
            // 
            this.optionsTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsTabs.Controls.Add(this.globalOptionsTab);
            this.optionsTabs.Controls.Add(this.distributedOptionsTab);
            this.optionsTabs.Controls.Add(this.trajectoryOptionsTab);
            this.optionsTabs.Location = new System.Drawing.Point(9, 19);
            this.optionsTabs.Name = "optionsTabs";
            this.optionsTabs.SelectedIndex = 0;
            this.optionsTabs.Size = new System.Drawing.Size(810, 253);
            this.optionsTabs.TabIndex = 11;
            // 
            // distributedOptionsTab
            // 
            this.distributedOptionsTab.Controls.Add(this.distributedOptionsTable);
            this.distributedOptionsTab.Location = new System.Drawing.Point(4, 22);
            this.distributedOptionsTab.Name = "distributedOptionsTab";
            this.distributedOptionsTab.Size = new System.Drawing.Size(600, 227);
            this.distributedOptionsTab.TabIndex = 2;
            this.distributedOptionsTab.Text = "Distributed Options";
            this.distributedOptionsTab.UseVisualStyleBackColor = true;
            // 
            // trajectoryOptionsTab
            // 
            this.trajectoryOptionsTab.Controls.Add(this.dataGridView1);
            this.trajectoryOptionsTab.Location = new System.Drawing.Point(4, 22);
            this.trajectoryOptionsTab.Name = "trajectoryOptionsTab";
            this.trajectoryOptionsTab.Size = new System.Drawing.Size(600, 227);
            this.trajectoryOptionsTab.TabIndex = 3;
            this.trajectoryOptionsTab.Text = "Trajectory Options";
            this.trajectoryOptionsTab.UseVisualStyleBackColor = true;
            // 
            // detailsGrp
            // 
            this.detailsGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsGrp.Controls.Add(this.parametersTable);
            this.detailsGrp.Location = new System.Drawing.Point(635, 27);
            this.detailsGrp.Name = "detailsGrp";
            this.detailsGrp.Size = new System.Drawing.Size(378, 369);
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
            this.parametersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.parametersTable.ColumnHeadersVisible = false;
            this.parametersTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.parameterNameColumn,
            this.parameterValueColumn});
            this.parametersTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parametersTable.Location = new System.Drawing.Point(3, 16);
            this.parametersTable.Name = "parametersTable";
            this.parametersTable.ReadOnly = true;
            this.parametersTable.RowHeadersVisible = false;
            this.parametersTable.Size = new System.Drawing.Size(372, 350);
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
            // valueButton
            // 
            this.valueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.valueButton.Location = new System.Drawing.Point(825, 180);
            this.valueButton.Name = "valueButton";
            this.valueButton.Size = new System.Drawing.Size(170, 37);
            this.valueButton.TabIndex = 50;
            this.valueButton.Text = "Show Values";
            this.valueButton.UseVisualStyleBackColor = true;
            // 
            // GlobalDrawGraphics
            // 
            this.GlobalDrawGraphics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GlobalDrawGraphics.Location = new System.Drawing.Point(825, 231);
            this.GlobalDrawGraphics.Name = "GlobalDrawGraphics";
            this.GlobalDrawGraphics.Size = new System.Drawing.Size(170, 37);
            this.GlobalDrawGraphics.TabIndex = 48;
            this.GlobalDrawGraphics.Text = "Draw Graphics";
            this.GlobalDrawGraphics.UseVisualStyleBackColor = true;
            // 
            // selectAll
            // 
            this.selectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectAll.Location = new System.Drawing.Point(13, 278);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(79, 23);
            this.selectAll.TabIndex = 52;
            this.selectAll.Text = "Select All";
            this.selectAll.UseVisualStyleBackColor = true;
            // 
            // deselectAll
            // 
            this.deselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deselectAll.Location = new System.Drawing.Point(98, 278);
            this.deselectAll.Name = "deselectAll";
            this.deselectAll.Size = new System.Drawing.Size(79, 23);
            this.deselectAll.TabIndex = 51;
            this.deselectAll.Text = "Deselect All";
            this.deselectAll.UseVisualStyleBackColor = true;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // approximationColumn
            // 
            this.approximationColumn.HeaderText = "Approximation";
            this.approximationColumn.Name = "approximationColumn";
            this.approximationColumn.Width = 130;
            // 
            // thickeningColumn
            // 
            this.thickeningColumn.HeaderText = "Thickening (in %)";
            this.thickeningColumn.Name = "thickeningColumn";
            this.thickeningColumn.Width = 130;
            // 
            // deltaColumn
            // 
            this.deltaColumn.HeaderText = "Delta";
            this.deltaColumn.Name = "deltaColumn";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // distributedOptionsTable
            // 
            this.distributedOptionsTable.AllowUserToAddRows = false;
            this.distributedOptionsTable.AllowUserToDeleteRows = false;
            this.distributedOptionsTable.AllowUserToResizeColumns = false;
            this.distributedOptionsTable.AllowUserToResizeRows = false;
            this.distributedOptionsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.distributedOptionsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.deltaColumn,
            this.thickeningColumn,
            this.approximationColumn,
            this.dataGridViewCheckBoxColumn1});
            this.distributedOptionsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.distributedOptionsTable.Location = new System.Drawing.Point(0, 0);
            this.distributedOptionsTable.Name = "distributedOptionsTable";
            this.distributedOptionsTable.RowHeadersVisible = false;
            this.distributedOptionsTable.Size = new System.Drawing.Size(600, 227);
            this.distributedOptionsTable.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewCheckBoxColumn2});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(600, 227);
            this.dataGridView1.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = "";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            // 
            // globalOptionsTab
            // 
            this.globalOptionsTab.Controls.Add(this.globalOptionsTable);
            this.globalOptionsTab.Location = new System.Drawing.Point(4, 22);
            this.globalOptionsTab.Name = "globalOptionsTab";
            this.globalOptionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.globalOptionsTab.Size = new System.Drawing.Size(802, 227);
            this.globalOptionsTab.TabIndex = 0;
            this.globalOptionsTab.Text = "Global Options";
            this.globalOptionsTab.UseVisualStyleBackColor = true;
            // 
            // checkedColumn
            // 
            this.checkedColumn.HeaderText = "";
            this.checkedColumn.Name = "checkedColumn";
            // 
            // optionNameColumn
            // 
            this.optionNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.optionNameColumn.HeaderText = "Name";
            this.optionNameColumn.Name = "optionNameColumn";
            this.optionNameColumn.ReadOnly = true;
            // 
            // globalOptionsTable
            // 
            this.globalOptionsTable.AllowUserToAddRows = false;
            this.globalOptionsTable.AllowUserToDeleteRows = false;
            this.globalOptionsTable.AllowUserToResizeColumns = false;
            this.globalOptionsTable.AllowUserToResizeRows = false;
            this.globalOptionsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.globalOptionsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.optionNameColumn,
            this.checkedColumn});
            this.globalOptionsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.globalOptionsTable.Location = new System.Drawing.Point(3, 3);
            this.globalOptionsTable.Name = "globalOptionsTable";
            this.globalOptionsTable.RowHeadersVisible = false;
            this.globalOptionsTable.Size = new System.Drawing.Size(796, 221);
            this.globalOptionsTable.TabIndex = 0;
            // 
            // drawOptionsGrp
            // 
            this.drawOptionsGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawOptionsGrp.Controls.Add(this.lineColor);
            this.drawOptionsGrp.Controls.Add(this.groupCheck);
            this.drawOptionsGrp.Controls.Add(this.pointsCheck);
            this.drawOptionsGrp.Controls.Add(this.color);
            this.drawOptionsGrp.Location = new System.Drawing.Point(825, 19);
            this.drawOptionsGrp.Name = "drawOptionsGrp";
            this.drawOptionsGrp.Size = new System.Drawing.Size(170, 144);
            this.drawOptionsGrp.TabIndex = 53;
            this.drawOptionsGrp.TabStop = false;
            this.drawOptionsGrp.Text = "Drawing Options";
            // 
            // colorDlg
            // 
            this.colorDlg.FullOpen = true;
            // 
            // color
            // 
            this.color.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.color.Location = new System.Drawing.Point(74, 25);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(55, 23);
            this.color.TabIndex = 0;
            this.color.UseVisualStyleBackColor = false;
            this.color.Click += new System.EventHandler(this.color_Click);
            // 
            // pointsCheck
            // 
            this.pointsCheck.AutoSize = true;
            this.pointsCheck.Checked = true;
            this.pointsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pointsCheck.Location = new System.Drawing.Point(17, 70);
            this.pointsCheck.Name = "pointsCheck";
            this.pointsCheck.Size = new System.Drawing.Size(55, 17);
            this.pointsCheck.TabIndex = 1;
            this.pointsCheck.Text = "Points";
            this.pointsCheck.UseVisualStyleBackColor = true;
            // 
            // groupCheck
            // 
            this.groupCheck.AutoSize = true;
            this.groupCheck.Location = new System.Drawing.Point(17, 112);
            this.groupCheck.Name = "groupCheck";
            this.groupCheck.Size = new System.Drawing.Size(148, 17);
            this.groupCheck.TabIndex = 2;
            this.groupCheck.Text = "Group Graphics by Option";
            this.groupCheck.UseVisualStyleBackColor = true;
            // 
            // lineColor
            // 
            this.lineColor.AutoSize = true;
            this.lineColor.Location = new System.Drawing.Point(14, 31);
            this.lineColor.Name = "lineColor";
            this.lineColor.Size = new System.Drawing.Size(54, 13);
            this.lineColor.TabIndex = 3;
            this.lineColor.Text = "Line Color";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 722);
            this.Controls.Add(this.detailsGrp);
            this.Controls.Add(this.researchesGrp);
            this.Controls.Add(this.statisticAnalyzeGrp);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Random Networks Statistic Analyzer";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.researchesGrp.ResumeLayout(false);
            this.researchesGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.researchesTable)).EndInit();
            this.researchTableCSM.ResumeLayout(false);
            this.statisticAnalyzeGrp.ResumeLayout(false);
            this.optionsTabs.ResumeLayout(false);
            this.distributedOptionsTab.ResumeLayout(false);
            this.trajectoryOptionsTab.ResumeLayout(false);
            this.detailsGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.parametersTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distributedOptionsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.globalOptionsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.globalOptionsTable)).EndInit();
            this.drawOptionsGrp.ResumeLayout(false);
            this.drawOptionsGrp.PerformLayout();
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
        private System.Windows.Forms.TabControl optionsTabs;
        private System.Windows.Forms.TabPage distributedOptionsTab;
        private System.Windows.Forms.GroupBox detailsGrp;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.DataGridView parametersTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterValueColumn;
        private System.Windows.Forms.ContextMenuStrip researchTableCSM;
        private System.Windows.Forms.ToolStripMenuItem deleteResearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectDeselectAllToolStripMenuItem;
        private System.Windows.Forms.TabPage trajectoryOptionsTab;
        private System.Windows.Forms.Button valueButton;
        private System.Windows.Forms.Button GlobalDrawGraphics;
        private System.Windows.Forms.Button selectAll;
        private System.Windows.Forms.Button deselectAll;
        private System.Windows.Forms.DataGridView distributedOptionsTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn deltaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn thickeningColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn approximationColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.TabPage globalOptionsTab;
        private System.Windows.Forms.DataGridView globalOptionsTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn optionNameColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkedColumn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.GroupBox drawOptionsGrp;
        private System.Windows.Forms.Button color;
        private System.Windows.Forms.ColorDialog colorDlg;
        private System.Windows.Forms.CheckBox groupCheck;
        private System.Windows.Forms.CheckBox pointsCheck;
        private System.Windows.Forms.Label lineColor;
    }
}

