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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.researchesGrp = new System.Windows.Forms.GroupBox();
            this.refresh = new System.Windows.Forms.Button();
            this.researchesTable = new System.Windows.Forms.DataGridView();
            this.researchIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.researchNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.researchRealizationCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.researchSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.researchDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.researchTableCSM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eraseResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.selectGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.modelTypeCmb = new System.Windows.Forms.ComboBox();
            this.modelType = new System.Windows.Forms.Label();
            this.researchTypeCmb = new System.Windows.Forms.ComboBox();
            this.researchType = new System.Windows.Forms.Label();
            this.statisticAnalyzeGrp = new System.Windows.Forms.GroupBox();
            this.drawOptionsGrp = new System.Windows.Forms.GroupBox();
            this.lineColor = new System.Windows.Forms.Label();
            this.groupCheck = new System.Windows.Forms.CheckBox();
            this.pointsCheck = new System.Windows.Forms.CheckBox();
            this.color = new System.Windows.Forms.Button();
            this.selectAll = new System.Windows.Forms.Button();
            this.deselectAll = new System.Windows.Forms.Button();
            this.valueButton = new System.Windows.Forms.Button();
            this.GlobalDrawGraphics = new System.Windows.Forms.Button();
            this.optionsTabs = new System.Windows.Forms.TabControl();
            this.globalOptionsTab = new System.Windows.Forms.TabPage();
            this.globalOptionsTable = new System.Windows.Forms.DataGridView();
            this.optionNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.globalCheckedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.distributedOptionsTab = new System.Windows.Forms.TabPage();
            this.distributedOptionsTable = new System.Windows.Forms.DataGridView();
            this.distributedNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deltaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thickeningColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.approximationColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.distributedCheckedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.detailsGrp = new System.Windows.Forms.GroupBox();
            this.parametersTable = new System.Windows.Forms.DataGridView();
            this.colorDlg = new System.Windows.Forms.ColorDialog();
            this.parameterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parameterValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainMenu.SuspendLayout();
            this.researchesGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.researchesTable)).BeginInit();
            this.researchTableCSM.SuspendLayout();
            this.statisticAnalyzeGrp.SuspendLayout();
            this.drawOptionsGrp.SuspendLayout();
            this.optionsTabs.SuspendLayout();
            this.globalOptionsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.globalOptionsTable)).BeginInit();
            this.distributedOptionsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.distributedOptionsTable)).BeginInit();
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
            this.researchIdColumn,
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
            this.researchesTable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.researchesTable_MouseDown);
            // 
            // researchIdColumn
            // 
            this.researchIdColumn.HeaderText = "Id";
            this.researchIdColumn.Name = "researchIdColumn";
            this.researchIdColumn.ReadOnly = true;
            this.researchIdColumn.Visible = false;
            // 
            // researchNameColumn
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchNameColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.researchNameColumn.HeaderText = "Name";
            this.researchNameColumn.Name = "researchNameColumn";
            this.researchNameColumn.ReadOnly = true;
            this.researchNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.researchNameColumn.Width = 150;
            // 
            // researchRealizationCountColumn
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchRealizationCountColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.researchRealizationCountColumn.HeaderText = "Realization Count";
            this.researchRealizationCountColumn.Name = "researchRealizationCountColumn";
            this.researchRealizationCountColumn.ReadOnly = true;
            this.researchRealizationCountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.researchRealizationCountColumn.Width = 150;
            // 
            // researchSizeColumn
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchSizeColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.researchSizeColumn.HeaderText = "Network Size";
            this.researchSizeColumn.Name = "researchSizeColumn";
            this.researchSizeColumn.ReadOnly = true;
            this.researchSizeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.researchSizeColumn.Width = 150;
            // 
            // researchDateColumn
            // 
            this.researchDateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.researchDateColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.researchDateColumn.HeaderText = "Date";
            this.researchDateColumn.Name = "researchDateColumn";
            this.researchDateColumn.ReadOnly = true;
            this.researchDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // researchTableCSM
            // 
            this.researchTableCSM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eraseResearch,
            this.selectGroup});
            this.researchTableCSM.Name = "researchTableCSM";
            this.researchTableCSM.Size = new System.Drawing.Size(224, 70);
            // 
            // eraseResearch
            // 
            this.eraseResearch.Name = "eraseResearch";
            this.eraseResearch.Size = new System.Drawing.Size(223, 22);
            this.eraseResearch.Text = "Erase Research from Storage";
            this.eraseResearch.Click += new System.EventHandler(this.eraseResearchToolStripMenuItem_Click);
            // 
            // selectGroup
            // 
            this.selectGroup.Name = "selectGroup";
            this.selectGroup.Size = new System.Drawing.Size(223, 22);
            this.selectGroup.Text = "Select Group";
            this.selectGroup.Click += new System.EventHandler(this.selectGroupToolStripMenuItem_Click);
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
            // lineColor
            // 
            this.lineColor.AutoSize = true;
            this.lineColor.Location = new System.Drawing.Point(14, 31);
            this.lineColor.Name = "lineColor";
            this.lineColor.Size = new System.Drawing.Size(54, 13);
            this.lineColor.TabIndex = 3;
            this.lineColor.Text = "Line Color";
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
            // selectAll
            // 
            this.selectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectAll.Location = new System.Drawing.Point(648, 278);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(79, 23);
            this.selectAll.TabIndex = 52;
            this.selectAll.Text = "Select All";
            this.selectAll.UseVisualStyleBackColor = true;
            this.selectAll.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // deselectAll
            // 
            this.deselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deselectAll.Location = new System.Drawing.Point(733, 278);
            this.deselectAll.Name = "deselectAll";
            this.deselectAll.Size = new System.Drawing.Size(79, 23);
            this.deselectAll.TabIndex = 51;
            this.deselectAll.Text = "Deselect All";
            this.deselectAll.UseVisualStyleBackColor = true;
            this.deselectAll.Click += new System.EventHandler(this.deselectAll_Click);
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
            // optionsTabs
            // 
            this.optionsTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsTabs.Controls.Add(this.globalOptionsTab);
            this.optionsTabs.Controls.Add(this.distributedOptionsTab);
            this.optionsTabs.Location = new System.Drawing.Point(9, 19);
            this.optionsTabs.Name = "optionsTabs";
            this.optionsTabs.SelectedIndex = 0;
            this.optionsTabs.Size = new System.Drawing.Size(810, 253);
            this.optionsTabs.TabIndex = 11;
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
            // globalOptionsTable
            // 
            this.globalOptionsTable.AllowUserToAddRows = false;
            this.globalOptionsTable.AllowUserToDeleteRows = false;
            this.globalOptionsTable.AllowUserToResizeColumns = false;
            this.globalOptionsTable.AllowUserToResizeRows = false;
            this.globalOptionsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.globalOptionsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.optionNameColumn,
            this.globalCheckedColumn});
            this.globalOptionsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.globalOptionsTable.Location = new System.Drawing.Point(3, 3);
            this.globalOptionsTable.Name = "globalOptionsTable";
            this.globalOptionsTable.RowHeadersVisible = false;
            this.globalOptionsTable.Size = new System.Drawing.Size(796, 221);
            this.globalOptionsTable.TabIndex = 0;
            // 
            // optionNameColumn
            // 
            this.optionNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.optionNameColumn.HeaderText = "Name";
            this.optionNameColumn.Name = "optionNameColumn";
            this.optionNameColumn.ReadOnly = true;
            // 
            // globalCheckedColumn
            // 
            this.globalCheckedColumn.HeaderText = "";
            this.globalCheckedColumn.Name = "globalCheckedColumn";
            // 
            // distributedOptionsTab
            // 
            this.distributedOptionsTab.Controls.Add(this.distributedOptionsTable);
            this.distributedOptionsTab.Location = new System.Drawing.Point(4, 22);
            this.distributedOptionsTab.Name = "distributedOptionsTab";
            this.distributedOptionsTab.Size = new System.Drawing.Size(802, 227);
            this.distributedOptionsTab.TabIndex = 2;
            this.distributedOptionsTab.Text = "Distributed Options";
            this.distributedOptionsTab.UseVisualStyleBackColor = true;
            // 
            // distributedOptionsTable
            // 
            this.distributedOptionsTable.AllowUserToAddRows = false;
            this.distributedOptionsTable.AllowUserToDeleteRows = false;
            this.distributedOptionsTable.AllowUserToResizeColumns = false;
            this.distributedOptionsTable.AllowUserToResizeRows = false;
            this.distributedOptionsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.distributedOptionsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.distributedNameColumn,
            this.deltaColumn,
            this.thickeningColumn,
            this.approximationColumn,
            this.distributedCheckedColumn});
            this.distributedOptionsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.distributedOptionsTable.Location = new System.Drawing.Point(0, 0);
            this.distributedOptionsTable.Name = "distributedOptionsTable";
            this.distributedOptionsTable.RowHeadersVisible = false;
            this.distributedOptionsTable.Size = new System.Drawing.Size(802, 227);
            this.distributedOptionsTable.TabIndex = 1;
            // 
            // distributedNameColumn
            // 
            this.distributedNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.distributedNameColumn.HeaderText = "Name";
            this.distributedNameColumn.Name = "distributedNameColumn";
            this.distributedNameColumn.ReadOnly = true;
            // 
            // deltaColumn
            // 
            this.deltaColumn.HeaderText = "Delta";
            this.deltaColumn.Name = "deltaColumn";
            // 
            // thickeningColumn
            // 
            this.thickeningColumn.HeaderText = "Thickening (in %)";
            this.thickeningColumn.Name = "thickeningColumn";
            this.thickeningColumn.Width = 130;
            // 
            // approximationColumn
            // 
            this.approximationColumn.HeaderText = "Approximation";
            this.approximationColumn.Name = "approximationColumn";
            this.approximationColumn.Width = 130;
            // 
            // distributedCheckedColumn
            // 
            this.distributedCheckedColumn.HeaderText = "";
            this.distributedCheckedColumn.Name = "distributedCheckedColumn";
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
            // colorDlg
            // 
            this.colorDlg.FullOpen = true;
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
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.parameterValueColumn.DefaultCellStyle = dataGridViewCellStyle15;
            this.parameterValueColumn.HeaderText = "Value";
            this.parameterValueColumn.Name = "parameterValueColumn";
            this.parameterValueColumn.ReadOnly = true;
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
            this.drawOptionsGrp.ResumeLayout(false);
            this.drawOptionsGrp.PerformLayout();
            this.optionsTabs.ResumeLayout(false);
            this.globalOptionsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.globalOptionsTable)).EndInit();
            this.distributedOptionsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.distributedOptionsTable)).EndInit();
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
        private System.Windows.Forms.GroupBox statisticAnalyzeGrp;
        private System.Windows.Forms.TabControl optionsTabs;
        private System.Windows.Forms.TabPage distributedOptionsTab;
        private System.Windows.Forms.GroupBox detailsGrp;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.DataGridView parametersTable;
        private System.Windows.Forms.ContextMenuStrip researchTableCSM;
        private System.Windows.Forms.ToolStripMenuItem eraseResearch;
        private System.Windows.Forms.ToolStripMenuItem selectGroup;
        private System.Windows.Forms.Button valueButton;
        private System.Windows.Forms.Button GlobalDrawGraphics;
        private System.Windows.Forms.Button selectAll;
        private System.Windows.Forms.Button deselectAll;
        private System.Windows.Forms.DataGridView distributedOptionsTable;
        private System.Windows.Forms.TabPage globalOptionsTab;
        private System.Windows.Forms.DataGridView globalOptionsTable;
        private System.Windows.Forms.GroupBox drawOptionsGrp;
        private System.Windows.Forms.Button color;
        private System.Windows.Forms.ColorDialog colorDlg;
        private System.Windows.Forms.CheckBox groupCheck;
        private System.Windows.Forms.CheckBox pointsCheck;
        private System.Windows.Forms.Label lineColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn distributedNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deltaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn thickeningColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn approximationColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn distributedCheckedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn optionNameColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn globalCheckedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn researchIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn researchNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn researchRealizationCountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn researchSizeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn researchDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterValueColumn;
    }
}

