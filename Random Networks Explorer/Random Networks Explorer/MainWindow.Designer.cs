namespace RandomNetworksExplorer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.researchesTable = new System.Windows.Forms.DataGridView();
            this.researchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.storageColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.generationColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tracingColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.realizationCountTxt = new System.Windows.Forms.NumericUpDown();
            this.startResearch = new System.Windows.Forms.Button();
            this.stopResearch = new System.Windows.Forms.Button();
            this.researchesDoneTxt = new System.Windows.Forms.TextBox();
            this.generationParametersGrp = new System.Windows.Forms.GroupBox();
            this.generationParametersTable = new System.Windows.Forms.DataGridView();
            this.generationParameterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.generationParameterValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.researchTableCSM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.basicNewResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.evolutionNewResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.percolationNewResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.browserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newResearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.percolationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelCheckingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataConvertionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.statisticAnalyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusGrp = new System.Windows.Forms.GroupBox();
            this.statusTable = new System.Windows.Forms.DataGridView();
            this.statusStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStopColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.topSplitLayout = new System.Windows.Forms.SplitContainer();
            this.analyzeOptionsGrp = new System.Windows.Forms.GroupBox();
            this.analyzeOptionsTable = new System.Windows.Forms.DataGridView();
            this.analyzeOptionNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.analyzeOptionCheckedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.researchesTable)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.realizationCountTxt)).BeginInit();
            this.generationParametersGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.generationParametersTable)).BeginInit();
            this.researchTableCSM.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.statusGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topSplitLayout)).BeginInit();
            this.topSplitLayout.Panel1.SuspendLayout();
            this.topSplitLayout.Panel2.SuspendLayout();
            this.topSplitLayout.SuspendLayout();
            this.analyzeOptionsGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analyzeOptionsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // researchesTable
            // 
            this.researchesTable.AllowUserToAddRows = false;
            this.researchesTable.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.researchesTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.researchesTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.researchesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.researchesTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.researchColumn,
            this.nameColumn,
            this.modelColumn,
            this.storageColumn,
            this.generationColumn,
            this.tracingColumn,
            this.statusColumn});
            this.researchesTable.Location = new System.Drawing.Point(6, 6);
            this.researchesTable.MinimumSize = new System.Drawing.Size(500, 0);
            this.researchesTable.MultiSelect = false;
            this.researchesTable.Name = "researchesTable";
            this.researchesTable.RowHeadersVisible = false;
            this.researchesTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.researchesTable.Size = new System.Drawing.Size(776, 679);
            this.researchesTable.TabIndex = 0;
            this.researchesTable.TabStop = false;
            this.researchesTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.researchTable_CellClick);
            this.researchesTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.researchesTable_CellEndEdit);
            this.researchesTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.researchTable_CellValueChanged);
            this.researchesTable.CurrentCellDirtyStateChanged += new System.EventHandler(this.researchTable_CurrentCellDirtyStateChanged);
            this.researchesTable.SelectionChanged += new System.EventHandler(this.researchTable_SelectionChanged);
            this.researchesTable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.researchTable_MouseDown);
            // 
            // researchColumn
            // 
            this.researchColumn.HeaderText = "Research";
            this.researchColumn.Name = "researchColumn";
            this.researchColumn.ReadOnly = true;
            this.researchColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.researchColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nameColumn
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // modelColumn
            // 
            this.modelColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.modelColumn.HeaderText = "Model";
            this.modelColumn.Items.AddRange(new object[] {
            "ERModel"});
            this.modelColumn.Name = "modelColumn";
            this.modelColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // storageColumn
            // 
            this.storageColumn.HeaderText = "Storage";
            this.storageColumn.Name = "storageColumn";
            this.storageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.storageColumn.Text = "";
            // 
            // generationColumn
            // 
            this.generationColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.generationColumn.HeaderText = "Generation";
            this.generationColumn.Name = "generationColumn";
            this.generationColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tracingColumn
            // 
            this.tracingColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tracingColumn.HeaderText = "Tracing";
            this.tracingColumn.Name = "tracingColumn";
            this.tracingColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tracingColumn.Width = 49;
            // 
            // statusColumn
            // 
            this.statusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.realizationCountTxt);
            this.panel1.Controls.Add(this.startResearch);
            this.panel1.Controls.Add(this.stopResearch);
            this.panel1.Controls.Add(this.researchesDoneTxt);
            this.panel1.Location = new System.Drawing.Point(3, 390);
            this.panel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 60);
            this.panel1.TabIndex = 36;
            // 
            // realizationCountTxt
            // 
            this.realizationCountTxt.Location = new System.Drawing.Point(3, 3);
            this.realizationCountTxt.Name = "realizationCountTxt";
            this.realizationCountTxt.Size = new System.Drawing.Size(124, 20);
            this.realizationCountTxt.TabIndex = 27;
            this.realizationCountTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.realizationCountTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.realizationCountTxt.ValueChanged += new System.EventHandler(this.realizationCountTxt_ValueChanged);
            // 
            // startResearch
            // 
            this.startResearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startResearch.Location = new System.Drawing.Point(3, 34);
            this.startResearch.Name = "startResearch";
            this.startResearch.Size = new System.Drawing.Size(79, 23);
            this.startResearch.TabIndex = 28;
            this.startResearch.Text = "Start";
            this.startResearch.UseVisualStyleBackColor = true;
            this.startResearch.Click += new System.EventHandler(this.startResearch_Click);
            // 
            // stopResearch
            // 
            this.stopResearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopResearch.Location = new System.Drawing.Point(88, 34);
            this.stopResearch.Name = "stopResearch";
            this.stopResearch.Size = new System.Drawing.Size(79, 23);
            this.stopResearch.TabIndex = 29;
            this.stopResearch.Text = "Stop";
            this.stopResearch.UseVisualStyleBackColor = true;
            this.stopResearch.Click += new System.EventHandler(this.stopResearch_Click);
            // 
            // researchesDoneTxt
            // 
            this.researchesDoneTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.researchesDoneTxt.Location = new System.Drawing.Point(133, 5);
            this.researchesDoneTxt.Name = "researchesDoneTxt";
            this.researchesDoneTxt.ReadOnly = true;
            this.researchesDoneTxt.Size = new System.Drawing.Size(333, 13);
            this.researchesDoneTxt.TabIndex = 30;
            this.researchesDoneTxt.Text = "0 of 1 is done";
            // 
            // generationParametersGrp
            // 
            this.generationParametersGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.generationParametersGrp.Controls.Add(this.generationParametersTable);
            this.generationParametersGrp.Location = new System.Drawing.Point(3, 6);
            this.generationParametersGrp.Margin = new System.Windows.Forms.Padding(0);
            this.generationParametersGrp.MinimumSize = new System.Drawing.Size(0, 70);
            this.generationParametersGrp.Name = "generationParametersGrp";
            this.generationParametersGrp.Size = new System.Drawing.Size(469, 160);
            this.generationParametersGrp.TabIndex = 1;
            this.generationParametersGrp.TabStop = false;
            this.generationParametersGrp.Text = "Generation Parameters";
            // 
            // generationParametersTable
            // 
            this.generationParametersTable.AllowUserToAddRows = false;
            this.generationParametersTable.AllowUserToDeleteRows = false;
            this.generationParametersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.generationParametersTable.ColumnHeadersVisible = false;
            this.generationParametersTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.generationParameterNameColumn,
            this.generationParameterValueColumn});
            this.generationParametersTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generationParametersTable.Location = new System.Drawing.Point(3, 16);
            this.generationParametersTable.Name = "generationParametersTable";
            this.generationParametersTable.RowHeadersVisible = false;
            this.generationParametersTable.Size = new System.Drawing.Size(463, 141);
            this.generationParametersTable.TabIndex = 26;
            this.generationParametersTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.generationParametersTable_CellClick);
            this.generationParametersTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.generationParametersTable_CellEndEdit);
            // 
            // generationParameterNameColumn
            // 
            this.generationParameterNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.generationParameterNameColumn.FillWeight = 98.47716F;
            this.generationParameterNameColumn.HeaderText = "Name";
            this.generationParameterNameColumn.Name = "generationParameterNameColumn";
            this.generationParameterNameColumn.ReadOnly = true;
            // 
            // generationParameterValueColumn
            // 
            this.generationParameterValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = null;
            this.generationParameterValueColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.generationParameterValueColumn.FillWeight = 101.5228F;
            this.generationParameterValueColumn.HeaderText = "Value";
            this.generationParameterValueColumn.Name = "generationParameterValueColumn";
            this.generationParameterValueColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // researchTableCSM
            // 
            this.researchTableCSM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newResearch,
            this.deleteResearch,
            this.cloneResearch});
            this.researchTableCSM.Name = "contextMenuStrip1";
            this.researchTableCSM.Size = new System.Drawing.Size(158, 70);
            // 
            // newResearch
            // 
            this.newResearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.basicNewResearch,
            this.evolutionNewResearch,
            this.percolationNewResearch});
            this.newResearch.Name = "newResearch";
            this.newResearch.Size = new System.Drawing.Size(157, 22);
            this.newResearch.Text = "New Research";
            // 
            // basicNewResearch
            // 
            this.basicNewResearch.Name = "basicNewResearch";
            this.basicNewResearch.Size = new System.Drawing.Size(134, 22);
            this.basicNewResearch.Text = "Basic";
            this.basicNewResearch.Click += new System.EventHandler(this.newBasicMenuItem_Click);
            // 
            // evolutionNewResearch
            // 
            this.evolutionNewResearch.Name = "evolutionNewResearch";
            this.evolutionNewResearch.Size = new System.Drawing.Size(134, 22);
            this.evolutionNewResearch.Text = "Evolution";
            this.evolutionNewResearch.Click += new System.EventHandler(this.newEvolutionMenuItem_Click);
            // 
            // percolationNewResearch
            // 
            this.percolationNewResearch.Name = "percolationNewResearch";
            this.percolationNewResearch.Size = new System.Drawing.Size(134, 22);
            this.percolationNewResearch.Text = "Percolation";
            this.percolationNewResearch.Click += new System.EventHandler(this.newPercolationMenuItem_Click);
            // 
            // deleteResearch
            // 
            this.deleteResearch.Name = "deleteResearch";
            this.deleteResearch.Size = new System.Drawing.Size(157, 22);
            this.deleteResearch.Text = "Delete Research";
            this.deleteResearch.Click += new System.EventHandler(this.deleteResearchMenuItem_Click);
            // 
            // cloneResearch
            // 
            this.cloneResearch.Name = "cloneResearch";
            this.cloneResearch.Size = new System.Drawing.Size(157, 22);
            this.cloneResearch.Text = "Clone Research";
            this.cloneResearch.Click += new System.EventHandler(this.cloneResearchMenuItem_Click);
            // 
            // browserDlg
            // 
            this.browserDlg.Description = "Choose directory for tracing output";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newResearchToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newResearchToolStripMenuItem
            // 
            this.newResearchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.basicToolStripMenuItem,
            this.evolutionToolStripMenuItem,
            this.percolationToolStripMenuItem});
            this.newResearchToolStripMenuItem.Name = "newResearchToolStripMenuItem";
            this.newResearchToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.newResearchToolStripMenuItem.Text = "&New Research";
            // 
            // basicToolStripMenuItem
            // 
            this.basicToolStripMenuItem.Name = "basicToolStripMenuItem";
            this.basicToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.basicToolStripMenuItem.Text = "Basic";
            this.basicToolStripMenuItem.Click += new System.EventHandler(this.newBasicMenuItem_Click);
            // 
            // evolutionToolStripMenuItem
            // 
            this.evolutionToolStripMenuItem.Name = "evolutionToolStripMenuItem";
            this.evolutionToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.evolutionToolStripMenuItem.Text = "Evolution";
            this.evolutionToolStripMenuItem.Click += new System.EventHandler(this.newEvolutionMenuItem_Click);
            // 
            // percolationToolStripMenuItem
            // 
            this.percolationToolStripMenuItem.Name = "percolationToolStripMenuItem";
            this.percolationToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.percolationToolStripMenuItem.Text = "Percolation";
            this.percolationToolStripMenuItem.Click += new System.EventHandler(this.newPercolationMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.settingsToolStripMenuItem.Text = "&Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelCheckingToolStripMenuItem,
            this.dataConvertionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // modelCheckingToolStripMenuItem
            // 
            this.modelCheckingToolStripMenuItem.Name = "modelCheckingToolStripMenuItem";
            this.modelCheckingToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.modelCheckingToolStripMenuItem.Text = "&Model checking...";
            this.modelCheckingToolStripMenuItem.Click += new System.EventHandler(this.modelCheckingToolStripMenuItem_Click);
            // 
            // dataConvertionsToolStripMenuItem
            // 
            this.dataConvertionsToolStripMenuItem.Name = "dataConvertionsToolStripMenuItem";
            this.dataConvertionsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.dataConvertionsToolStripMenuItem.Text = "&Data Convertions...";
            this.dataConvertionsToolStripMenuItem.Click += new System.EventHandler(this.dataConvertionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.statisticAnalyzerToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1264, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "MainMenu";
            // 
            // statisticAnalyzerToolStripMenuItem
            // 
            this.statisticAnalyzerToolStripMenuItem.Name = "statisticAnalyzerToolStripMenuItem";
            this.statisticAnalyzerToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.statisticAnalyzerToolStripMenuItem.Text = "&Statistic Analyzer";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Status";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Column8";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "Column9";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // statusGrp
            // 
            this.statusGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusGrp.Controls.Add(this.statusTable);
            this.statusGrp.Location = new System.Drawing.Point(3, 453);
            this.statusGrp.Margin = new System.Windows.Forms.Padding(0);
            this.statusGrp.MinimumSize = new System.Drawing.Size(0, 70);
            this.statusGrp.Name = "statusGrp";
            this.statusGrp.Size = new System.Drawing.Size(469, 235);
            this.statusGrp.TabIndex = 37;
            this.statusGrp.TabStop = false;
            this.statusGrp.Text = "Status";
            // 
            // statusTable
            // 
            this.statusTable.AllowUserToAddRows = false;
            this.statusTable.AllowUserToDeleteRows = false;
            this.statusTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statusTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.statusStatusColumn,
            this.statusStopColumn});
            this.statusTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusTable.Location = new System.Drawing.Point(3, 16);
            this.statusTable.Name = "statusTable";
            this.statusTable.RowHeadersVisible = false;
            this.statusTable.Size = new System.Drawing.Size(463, 216);
            this.statusTable.TabIndex = 0;
            // 
            // statusStatusColumn
            // 
            this.statusStatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.statusStatusColumn.HeaderText = "Status";
            this.statusStatusColumn.Name = "statusStatusColumn";
            this.statusStatusColumn.ReadOnly = true;
            // 
            // statusStopColumn
            // 
            this.statusStopColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.statusStopColumn.HeaderText = "";
            this.statusStopColumn.Name = "statusStopColumn";
            // 
            // topSplitLayout
            // 
            this.topSplitLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topSplitLayout.Location = new System.Drawing.Point(0, 24);
            this.topSplitLayout.Name = "topSplitLayout";
            // 
            // topSplitLayout.Panel1
            // 
            this.topSplitLayout.Panel1.Controls.Add(this.researchesTable);
            this.topSplitLayout.Panel1MinSize = 0;
            // 
            // topSplitLayout.Panel2
            // 
            this.topSplitLayout.Panel2.Controls.Add(this.analyzeOptionsGrp);
            this.topSplitLayout.Panel2.Controls.Add(this.generationParametersGrp);
            this.topSplitLayout.Panel2.Controls.Add(this.panel1);
            this.topSplitLayout.Panel2.Controls.Add(this.statusGrp);
            this.topSplitLayout.Panel2MinSize = 0;
            this.topSplitLayout.Size = new System.Drawing.Size(1264, 691);
            this.topSplitLayout.SplitterDistance = 785;
            this.topSplitLayout.TabIndex = 34;
            // 
            // analyzeOptionsGrp
            // 
            this.analyzeOptionsGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.analyzeOptionsGrp.Controls.Add(this.analyzeOptionsTable);
            this.analyzeOptionsGrp.Location = new System.Drawing.Point(3, 166);
            this.analyzeOptionsGrp.Margin = new System.Windows.Forms.Padding(0);
            this.analyzeOptionsGrp.MinimumSize = new System.Drawing.Size(0, 70);
            this.analyzeOptionsGrp.Name = "analyzeOptionsGrp";
            this.analyzeOptionsGrp.Size = new System.Drawing.Size(469, 221);
            this.analyzeOptionsGrp.TabIndex = 35;
            this.analyzeOptionsGrp.TabStop = false;
            this.analyzeOptionsGrp.Text = "Analyze Options";
            // 
            // analyzeOptionsTable
            // 
            this.analyzeOptionsTable.AllowUserToAddRows = false;
            this.analyzeOptionsTable.AllowUserToDeleteRows = false;
            this.analyzeOptionsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.analyzeOptionsTable.ColumnHeadersVisible = false;
            this.analyzeOptionsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.analyzeOptionNameColumn,
            this.analyzeOptionCheckedColumn});
            this.analyzeOptionsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analyzeOptionsTable.Location = new System.Drawing.Point(3, 16);
            this.analyzeOptionsTable.Name = "analyzeOptionsTable";
            this.analyzeOptionsTable.RowHeadersVisible = false;
            this.analyzeOptionsTable.Size = new System.Drawing.Size(463, 202);
            this.analyzeOptionsTable.TabIndex = 32;
            this.analyzeOptionsTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.analyzeOptionsTable_CellValueChanged);
            this.analyzeOptionsTable.CurrentCellDirtyStateChanged += new System.EventHandler(this.analyzeOptionsTable_CurrentCellDirtyStateChanged);
            // 
            // analyzeOptionNameColumn
            // 
            this.analyzeOptionNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.analyzeOptionNameColumn.HeaderText = "Name";
            this.analyzeOptionNameColumn.Name = "analyzeOptionNameColumn";
            this.analyzeOptionNameColumn.ReadOnly = true;
            // 
            // analyzeOptionCheckedColumn
            // 
            this.analyzeOptionCheckedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = "False";
            this.analyzeOptionCheckedColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.analyzeOptionCheckedColumn.HeaderText = "";
            this.analyzeOptionCheckedColumn.Name = "analyzeOptionCheckedColumn";
            this.analyzeOptionCheckedColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.analyzeOptionCheckedColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // openFileDlg
            // 
            this.openFileDlg.Filter = "\"txt files (*.txt)|*.txt|All files (*.*)|*.*\"";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 715);
            this.Controls.Add(this.topSplitLayout);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.researchesTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.realizationCountTxt)).EndInit();
            this.generationParametersGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.generationParametersTable)).EndInit();
            this.researchTableCSM.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusTable)).EndInit();
            this.topSplitLayout.Panel1.ResumeLayout(false);
            this.topSplitLayout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topSplitLayout)).EndInit();
            this.topSplitLayout.ResumeLayout(false);
            this.analyzeOptionsGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.analyzeOptionsTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView researchesTable;
        private System.Windows.Forms.ToolStripMenuItem newResearch;
        private System.Windows.Forms.ToolStripMenuItem deleteResearch;
        private System.Windows.Forms.ToolStripMenuItem cloneResearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.FolderBrowserDialog browserDlg;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newResearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem basicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem evolutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem percolationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem basicNewResearch;
        private System.Windows.Forms.ToolStripMenuItem evolutionNewResearch;
        private System.Windows.Forms.ToolStripMenuItem percolationNewResearch;
        protected System.Windows.Forms.ContextMenuStrip researchTableCSM;
        private System.Windows.Forms.NumericUpDown realizationCountTxt;
        private System.Windows.Forms.Button startResearch;
        private System.Windows.Forms.Button stopResearch;
        private System.Windows.Forms.TextBox researchesDoneTxt;
        private System.Windows.Forms.GroupBox generationParametersGrp;
        private System.Windows.Forms.DataGridView generationParametersTable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox statusGrp;
        private System.Windows.Forms.DataGridView statusTable;
        private System.Windows.Forms.SplitContainer topSplitLayout;
        private System.Windows.Forms.GroupBox analyzeOptionsGrp;
        private System.Windows.Forms.DataGridView analyzeOptionsTable;
        private System.Windows.Forms.ToolStripMenuItem modelCheckingToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn generationParameterNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn generationParameterValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusStatusColumn;
        private System.Windows.Forms.DataGridViewButtonColumn statusStopColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn analyzeOptionNameColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn analyzeOptionCheckedColumn;
        private System.Windows.Forms.ToolStripMenuItem dataConvertionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticAnalyzerToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn researchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn modelColumn;
        private System.Windows.Forms.DataGridViewButtonColumn storageColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn generationColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn tracingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
    }
}

