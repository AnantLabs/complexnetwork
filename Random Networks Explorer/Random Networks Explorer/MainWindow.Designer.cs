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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.researchTable = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.realizationCountTxt = new System.Windows.Forms.NumericUpDown();
            this.startResearch = new System.Windows.Forms.Button();
            this.stopResearch = new System.Windows.Forms.Button();
            this.researchesDoneTxt = new System.Windows.Forms.TextBox();
            this.delete = new System.Windows.Forms.Button();
            this.generationParametersGroup = new System.Windows.Forms.GroupBox();
            this.generationParametersTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.researchTableCSM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.basicNewResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.evolutionNewResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.percolationNewResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.browserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newResearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.statusTable = new System.Windows.Forms.DataGridView();
            this.topSplitLayout = new System.Windows.Forms.SplitContainer();
            this.analyzeOptionsGroup = new System.Windows.Forms.GroupBox();
            this.analyzeOptionsTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tracingColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.generationColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.storageColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.modelColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.researchColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.modelCheckingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.researchTable)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.realizationCountTxt)).BeginInit();
            this.generationParametersGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.generationParametersTable)).BeginInit();
            this.researchTableCSM.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.statusGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topSplitLayout)).BeginInit();
            this.topSplitLayout.Panel1.SuspendLayout();
            this.topSplitLayout.Panel2.SuspendLayout();
            this.topSplitLayout.SuspendLayout();
            this.analyzeOptionsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analyzeOptionsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // researchTable
            // 
            this.researchTable.AllowUserToAddRows = false;
            this.researchTable.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.researchTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.realizationCountTxt);
            this.panel1.Controls.Add(this.startResearch);
            this.panel1.Controls.Add(this.stopResearch);
            this.panel1.Controls.Add(this.researchesDoneTxt);
            this.panel1.Controls.Add(this.delete);
            this.panel1.Location = new System.Drawing.Point(3, 329);
            this.panel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 60);
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
            // 
            // researchesDoneTxt
            // 
            this.researchesDoneTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.researchesDoneTxt.Location = new System.Drawing.Point(133, 5);
            this.researchesDoneTxt.Name = "researchesDoneTxt";
            this.researchesDoneTxt.ReadOnly = true;
            this.researchesDoneTxt.Size = new System.Drawing.Size(176, 13);
            this.researchesDoneTxt.TabIndex = 30;
            this.researchesDoneTxt.Text = "0 of 1 is done";
            // 
            // delete
            // 
            this.delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.delete.Location = new System.Drawing.Point(287, 34);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(79, 23);
            this.delete.TabIndex = 33;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = true;
            // 
            // generationParametersGroup
            // 
            this.generationParametersGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.generationParametersGroup.Controls.Add(this.generationParametersTable);
            this.generationParametersGroup.Location = new System.Drawing.Point(3, 6);
            this.generationParametersGroup.Margin = new System.Windows.Forms.Padding(0);
            this.generationParametersGroup.MinimumSize = new System.Drawing.Size(0, 70);
            this.generationParametersGroup.Name = "generationParametersGroup";
            this.generationParametersGroup.Size = new System.Drawing.Size(369, 160);
            this.generationParametersGroup.TabIndex = 1;
            this.generationParametersGroup.TabStop = false;
            this.generationParametersGroup.Text = "Generation Parameters";
            // 
            // generationParametersTable
            // 
            this.generationParametersTable.AllowUserToAddRows = false;
            this.generationParametersTable.AllowUserToDeleteRows = false;
            this.generationParametersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.generationParametersTable.ColumnHeadersVisible = false;
            this.generationParametersTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewCheckBoxColumn1});
            this.generationParametersTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generationParametersTable.Location = new System.Drawing.Point(3, 16);
            this.generationParametersTable.Name = "generationParametersTable";
            this.generationParametersTable.RowHeadersVisible = false;
            this.generationParametersTable.Size = new System.Drawing.Size(363, 141);
            this.generationParametersTable.TabIndex = 26;
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
            this.basicNewResearch.Size = new System.Drawing.Size(152, 22);
            this.basicNewResearch.Text = "Basic";
            this.basicNewResearch.Click += new System.EventHandler(this.newBasicMenuItem_Click);
            // 
            // evolutionNewResearch
            // 
            this.evolutionNewResearch.Name = "evolutionNewResearch";
            this.evolutionNewResearch.Size = new System.Drawing.Size(152, 22);
            this.evolutionNewResearch.Text = "Evolution";
            this.evolutionNewResearch.Click += new System.EventHandler(this.newEvolutionMenuItem_Click);
            // 
            // percolationNewResearch
            // 
            this.percolationNewResearch.Name = "percolationNewResearch";
            this.percolationNewResearch.Size = new System.Drawing.Size(152, 22);
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
            // browserDialog
            // 
            this.browserDialog.Description = "Choose directory for tracing output";
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
            this.newResearchToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newResearchToolStripMenuItem.Text = "&New Research";
            // 
            // basicToolStripMenuItem
            // 
            this.basicToolStripMenuItem.Name = "basicToolStripMenuItem";
            this.basicToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.basicToolStripMenuItem.Text = "Basic";
            this.basicToolStripMenuItem.Click += new System.EventHandler(this.newBasicMenuItem_Click);
            // 
            // evolutionToolStripMenuItem
            // 
            this.evolutionToolStripMenuItem.Name = "evolutionToolStripMenuItem";
            this.evolutionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.evolutionToolStripMenuItem.Text = "Evolution";
            this.evolutionToolStripMenuItem.Click += new System.EventHandler(this.newEvolutionMenuItem_Click);
            // 
            // percolationToolStripMenuItem
            // 
            this.percolationToolStripMenuItem.Name = "percolationToolStripMenuItem";
            this.percolationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.percolationToolStripMenuItem.Text = "Percolation";
            this.percolationToolStripMenuItem.Click += new System.EventHandler(this.newPercolationMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "&Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelCheckingToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
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
            this.statusGroup.Controls.Add(this.statusTable);
            this.statusGroup.Location = new System.Drawing.Point(3, 392);
            this.statusGroup.Margin = new System.Windows.Forms.Padding(0);
            this.statusGroup.MinimumSize = new System.Drawing.Size(0, 70);
            this.statusGroup.Name = "statusGroup";
            this.statusGroup.Size = new System.Drawing.Size(369, 183);
            this.statusGroup.TabIndex = 37;
            this.statusGroup.TabStop = false;
            this.statusGroup.Text = "Status";
            // 
            // statusTable
            // 
            this.statusTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statusTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusTable.Location = new System.Drawing.Point(3, 16);
            this.statusTable.Name = "statusTable";
            this.statusTable.Size = new System.Drawing.Size(363, 164);
            this.statusTable.TabIndex = 0;
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
            this.topSplitLayout.Panel2.Controls.Add(this.analyzeOptionsGroup);
            this.topSplitLayout.Panel2.Controls.Add(this.generationParametersGroup);
            this.topSplitLayout.Panel2.Controls.Add(this.panel1);
            this.topSplitLayout.Panel2.Controls.Add(this.statusGroup);
            this.topSplitLayout.Panel2MinSize = 0;
            this.topSplitLayout.Size = new System.Drawing.Size(1264, 578);
            this.topSplitLayout.SplitterDistance = 885;
            this.topSplitLayout.TabIndex = 34;
            // 
            // analyzeOptionsGroup
            // 
            this.analyzeOptionsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.analyzeOptionsGroup.Controls.Add(this.analyzeOptionsTable);
            this.analyzeOptionsGroup.Location = new System.Drawing.Point(3, 166);
            this.analyzeOptionsGroup.Margin = new System.Windows.Forms.Padding(0);
            this.analyzeOptionsGroup.MinimumSize = new System.Drawing.Size(0, 70);
            this.analyzeOptionsGroup.Name = "analyzeOptionsGroup";
            this.analyzeOptionsGroup.Size = new System.Drawing.Size(369, 160);
            this.analyzeOptionsGroup.TabIndex = 35;
            this.analyzeOptionsGroup.TabStop = false;
            this.analyzeOptionsGroup.Text = "Analyze Options";
            // 
            // analyzeOptionsTable
            // 
            this.analyzeOptionsTable.AllowUserToAddRows = false;
            this.analyzeOptionsTable.AllowUserToDeleteRows = false;
            this.analyzeOptionsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.analyzeOptionsTable.ColumnHeadersVisible = false;
            this.analyzeOptionsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.analyzeOptionsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analyzeOptionsTable.Location = new System.Drawing.Point(3, 16);
            this.analyzeOptionsTable.Name = "analyzeOptionsTable";
            this.analyzeOptionsTable.Size = new System.Drawing.Size(363, 141);
            this.analyzeOptionsTable.TabIndex = 32;
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
            // statusColumn
            // 
            this.statusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            // 
            // tracingColumn
            // 
            this.tracingColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tracingColumn.HeaderText = "Tracing";
            this.tracingColumn.Name = "tracingColumn";
            this.tracingColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tracingColumn.Width = 49;
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
            // storageColumn
            // 
            this.storageColumn.HeaderText = "Storage";
            this.storageColumn.Name = "storageColumn";
            this.storageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.storageColumn.Text = "";
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
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // modelCheckingToolStripMenuItem
            // 
            this.modelCheckingToolStripMenuItem.Name = "modelCheckingToolStripMenuItem";
            this.modelCheckingToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.modelCheckingToolStripMenuItem.Text = "&Model checking...";
            this.modelCheckingToolStripMenuItem.Click += new System.EventHandler(this.modelCheckingToolStripMenuItem_Click);
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
            ((System.ComponentModel.ISupportInitialize)(this.realizationCountTxt)).EndInit();
            this.generationParametersGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.generationParametersTable)).EndInit();
            this.researchTableCSM.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusTable)).EndInit();
            this.topSplitLayout.Panel1.ResumeLayout(false);
            this.topSplitLayout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topSplitLayout)).EndInit();
            this.topSplitLayout.ResumeLayout(false);
            this.analyzeOptionsGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.analyzeOptionsTable)).EndInit();
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
        private System.Windows.Forms.FolderBrowserDialog browserDialog;
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
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.GroupBox generationParametersGroup;
        private System.Windows.Forms.DataGridView generationParametersTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox statusGroup;
        private System.Windows.Forms.DataGridView statusTable;
        private System.Windows.Forms.SplitContainer topSplitLayout;
        private System.Windows.Forms.GroupBox analyzeOptionsGroup;
        private System.Windows.Forms.DataGridView analyzeOptionsTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewComboBoxColumn researchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn modelColumn;
        private System.Windows.Forms.DataGridViewButtonColumn storageColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn generationColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn tracingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.ToolStripMenuItem modelCheckingToolStripMenuItem;
    }
}

