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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.researchTable = new System.Windows.Forms.DataGridView();
            this.researchColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.storageColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.generationColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tracingColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.generationParametersGroup = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.analyzeOptionsGroup = new System.Windows.Forms.GroupBox();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.researchTableCSM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.trajectoryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.percolationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newResearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trajectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.percolationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusGroup = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.topSplitLayout = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.researchTable)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.generationParametersGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.analyzeOptionsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.researchTableCSM.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.statusGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topSplitLayout)).BeginInit();
            this.topSplitLayout.Panel1.SuspendLayout();
            this.topSplitLayout.Panel2.SuspendLayout();
            this.topSplitLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // researchTable
            // 
            this.researchTable.AllowUserToAddRows = false;
            this.researchTable.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.researchTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.researchTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.researchTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.researchTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.researchColumn,
            this.nameColumn,
            this.modelColumn,
            this.storageColumn,
            this.generationColumn,
            this.tracingColumn,
            this.statusColumn});
            this.researchTable.Location = new System.Drawing.Point(6, 6);
            this.researchTable.MinimumSize = new System.Drawing.Size(500, 0);
            this.researchTable.MultiSelect = false;
            this.researchTable.Name = "researchTable";
            this.researchTable.RowHeadersVisible = false;
            this.researchTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.researchTable.Size = new System.Drawing.Size(876, 566);
            this.researchTable.TabIndex = 0;
            this.researchTable.TabStop = false;
            this.researchTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.researchTable_CellClick);
            this.researchTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.researchTable_CellValueChanged);
            this.researchTable.CurrentCellDirtyStateChanged += new System.EventHandler(this.researchTable_CurrentCellDirtyStateChanged);
            this.researchTable.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.researchTable_RowLeave);
            this.researchTable.SelectionChanged += new System.EventHandler(this.researchTable_SelectionChanged);
            this.researchTable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.researchTable_MouseDown);
            // 
            // researchColumn
            // 
            this.researchColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.researchColumn.HeaderText = "Research";
            this.researchColumn.Items.AddRange(new object[] {
            "Basic",
            "Evolution",
            "Percolation"});
            this.researchColumn.Name = "researchColumn";
            this.researchColumn.ReadOnly = true;
            this.researchColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // modelColumn
            // 
            this.modelColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.modelColumn.HeaderText = "Model";
            this.modelColumn.Items.AddRange(new object[] {
            "Erdos-Renyi",
            "Watts-Strogatz",
            "Barabasi-Albert",
            "Block-Hierarchical",
            "NR Block-Hierarchical"});
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
            this.generationColumn.Items.AddRange(new object[] {
            "Random",
            "Static"});
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
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Location = new System.Drawing.Point(3, 329);
            this.panel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 60);
            this.panel1.TabIndex = 36;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(3, 3);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(124, 20);
            this.numericUpDown1.TabIndex = 27;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(3, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(88, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 23);
            this.button2.TabIndex = 29;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(133, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(176, 13);
            this.textBox1.TabIndex = 30;
            this.textBox1.Text = "0 of 1 is done";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(287, 34);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 23);
            this.button3.TabIndex = 33;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // generationParametersGroup
            // 
            this.generationParametersGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.generationParametersGroup.Controls.Add(this.dataGridView3);
            this.generationParametersGroup.Location = new System.Drawing.Point(3, 166);
            this.generationParametersGroup.Margin = new System.Windows.Forms.Padding(0);
            this.generationParametersGroup.MinimumSize = new System.Drawing.Size(0, 70);
            this.generationParametersGroup.Name = "generationParametersGroup";
            this.generationParametersGroup.Size = new System.Drawing.Size(369, 160);
            this.generationParametersGroup.TabIndex = 1;
            this.generationParametersGroup.TabStop = false;
            this.generationParametersGroup.Text = "Generation Parameters";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.ColumnHeadersVisible = false;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewCheckBoxColumn1});
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(3, 16);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.Size = new System.Drawing.Size(363, 141);
            this.dataGridView3.TabIndex = 26;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "Column8";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewCheckBoxColumn1.HeaderText = "Column9";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // analyzeOptionsGroup
            // 
            this.analyzeOptionsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analyzeOptionsGroup.Controls.Add(this.dataGridView4);
            this.analyzeOptionsGroup.Location = new System.Drawing.Point(3, 6);
            this.analyzeOptionsGroup.Margin = new System.Windows.Forms.Padding(0);
            this.analyzeOptionsGroup.MinimumSize = new System.Drawing.Size(0, 70);
            this.analyzeOptionsGroup.Name = "analyzeOptionsGroup";
            this.analyzeOptionsGroup.Size = new System.Drawing.Size(369, 160);
            this.analyzeOptionsGroup.TabIndex = 34;
            this.analyzeOptionsGroup.TabStop = false;
            this.analyzeOptionsGroup.Text = "Analyze Options";
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.ColumnHeadersVisible = false;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4.Location = new System.Drawing.Point(3, 16);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(363, 141);
            this.dataGridView4.TabIndex = 32;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Column8";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.HeaderText = "Column9";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn4.Text = "Stop";
            this.dataGridViewTextBoxColumn4.UseColumnTextForButtonValue = true;
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
            this.analyzeToolStripMenuItem1,
            this.trajectoryToolStripMenuItem1,
            this.percolationToolStripMenuItem1});
            this.newResearch.Name = "newResearch";
            this.newResearch.Size = new System.Drawing.Size(157, 22);
            this.newResearch.Text = "New Research";
            // 
            // analyzeToolStripMenuItem1
            // 
            this.analyzeToolStripMenuItem1.Name = "analyzeToolStripMenuItem1";
            this.analyzeToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.analyzeToolStripMenuItem1.Text = "Basic";
            this.analyzeToolStripMenuItem1.Click += new System.EventHandler(this.newAnalyzeMenuItem_Click);
            // 
            // trajectoryToolStripMenuItem1
            // 
            this.trajectoryToolStripMenuItem1.Name = "trajectoryToolStripMenuItem1";
            this.trajectoryToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.trajectoryToolStripMenuItem1.Text = "Evolution";
            this.trajectoryToolStripMenuItem1.Click += new System.EventHandler(this.newTrajectoryMenuItem_Click);
            // 
            // percolationToolStripMenuItem1
            // 
            this.percolationToolStripMenuItem1.Name = "percolationToolStripMenuItem1";
            this.percolationToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.percolationToolStripMenuItem1.Text = "Percolation";
            this.percolationToolStripMenuItem1.Click += new System.EventHandler(this.newPercolationMenuItem_Click);
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
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Choose directory for tracing output";
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
            this.analyzeToolStripMenuItem,
            this.trajectoryToolStripMenuItem,
            this.percolationToolStripMenuItem});
            this.newResearchToolStripMenuItem.Name = "newResearchToolStripMenuItem";
            this.newResearchToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.newResearchToolStripMenuItem.Text = "&New Research";
            // 
            // analyzeToolStripMenuItem
            // 
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.analyzeToolStripMenuItem.Text = "Basic";
            this.analyzeToolStripMenuItem.Click += new System.EventHandler(this.newAnalyzeMenuItem_Click);
            // 
            // trajectoryToolStripMenuItem
            // 
            this.trajectoryToolStripMenuItem.Name = "trajectoryToolStripMenuItem";
            this.trajectoryToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.trajectoryToolStripMenuItem.Text = "Evolution";
            this.trajectoryToolStripMenuItem.Click += new System.EventHandler(this.newTrajectoryMenuItem_Click);
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
            this.settingsToolStripMenuItem.Text = "&Settings";
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
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1264, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "MainMenu";
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
            // statusGroup
            // 
            this.statusGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusGroup.Controls.Add(this.dataGridView1);
            this.statusGroup.Location = new System.Drawing.Point(3, 392);
            this.statusGroup.Margin = new System.Windows.Forms.Padding(0);
            this.statusGroup.MinimumSize = new System.Drawing.Size(0, 70);
            this.statusGroup.Name = "statusGroup";
            this.statusGroup.Size = new System.Drawing.Size(369, 183);
            this.statusGroup.TabIndex = 37;
            this.statusGroup.TabStop = false;
            this.statusGroup.Text = "Status";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(363, 164);
            this.dataGridView1.TabIndex = 0;
            // 
            // topSplitLayout
            // 
            this.topSplitLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topSplitLayout.Location = new System.Drawing.Point(0, 24);
            this.topSplitLayout.Name = "topSplitLayout";
            // 
            // topSplitLayout.Panel1
            // 
            this.topSplitLayout.Panel1.Controls.Add(this.researchTable);
            this.topSplitLayout.Panel1MinSize = 0;
            // 
            // topSplitLayout.Panel2
            // 
            this.topSplitLayout.Panel2.Controls.Add(this.generationParametersGroup);
            this.topSplitLayout.Panel2.Controls.Add(this.analyzeOptionsGroup);
            this.topSplitLayout.Panel2.Controls.Add(this.panel1);
            this.topSplitLayout.Panel2.Controls.Add(this.statusGroup);
            this.topSplitLayout.Panel2MinSize = 0;
            this.topSplitLayout.Size = new System.Drawing.Size(1264, 578);
            this.topSplitLayout.SplitterDistance = 885;
            this.topSplitLayout.TabIndex = 34;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 602);
            this.Controls.Add(this.topSplitLayout);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.researchTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.generationParametersGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.analyzeOptionsGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.researchTableCSM.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.topSplitLayout.Panel1.ResumeLayout(false);
            this.topSplitLayout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topSplitLayout)).EndInit();
            this.topSplitLayout.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView researchTable;
        private System.Windows.Forms.ToolStripMenuItem newResearch;
        private System.Windows.Forms.ToolStripMenuItem deleteResearch;
        private System.Windows.Forms.ToolStripMenuItem cloneResearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newResearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trajectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem percolationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem trajectoryToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem percolationToolStripMenuItem1;
        protected System.Windows.Forms.ContextMenuStrip researchTableCSM;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox generationParametersGroup;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.GroupBox analyzeOptionsGroup;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox statusGroup;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewComboBoxColumn researchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn modelColumn;
        private System.Windows.Forms.DataGridViewButtonColumn storageColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn generationColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn tracingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.SplitContainer topSplitLayout;
    }
}

