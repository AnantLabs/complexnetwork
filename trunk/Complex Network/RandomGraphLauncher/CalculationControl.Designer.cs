namespace RandomGraphLauncher
{
    partial class CalculationControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculationControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.infoGrp = new System.Windows.Forms.GroupBox();
            this.description = new System.Windows.Forms.Label();
            this.modelName = new System.Windows.Forms.Label();
            this.checkModel = new System.Windows.Forms.Label();
            this.genParamsGrp = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.optionsGrp = new System.Windows.Forms.GroupBox();
            this.deselectAllCheck = new System.Windows.Forms.CheckBox();
            this.selectAllCheck = new System.Windows.Forms.CheckBox();
            this.cycles = new System.Windows.Forms.Label();
            this.motives = new System.Windows.Forms.Label();
            this.cyclesHighCmb = new System.Windows.Forms.ComboBox();
            this.cyclesLowCmb = new System.Windows.Forms.ComboBox();
            this.motiveHighCmb = new System.Windows.Forms.ComboBox();
            this.constantInput = new System.Windows.Forms.TextBox();
            this.constantInputLabel = new System.Windows.Forms.Label();
            this.stepcountInput = new System.Windows.Forms.TextBox();
            this.stepcountLabel = new System.Windows.Forms.Label();
            this.motiveLowCmb = new System.Windows.Forms.ComboBox();
            this.optionsCheckList = new System.Windows.Forms.CheckedListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.implementationCountNumeric = new System.Windows.Forms.NumericUpDown();
            this.startBtn = new System.Windows.Forms.Button();
            this.continueBtn = new System.Windows.Forms.Button();
            this.implemetation = new System.Windows.Forms.Label();
            this.pauseBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.calculationStatusGrp = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.calculationStatusGrd = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percentColumn = new RandomGraphLauncher.controls.DataGridViewProgressColumn();
            this.taskColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hostColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stopColumn = new RandomGraphLauncher.Controls.DataGridViewDisableButtonColumn();
            this.manageColumn = new RandomGraphLauncher.Controls.DataGridViewDisableButtonColumn();
            this.axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.backgroundStartWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundStopWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundPauseWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundContinueWorker = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewDisableButtonColumn1 = new RandomGraphLauncher.Controls.DataGridViewDisableButtonColumn();
            this.dataGridViewDisableButtonColumn2 = new RandomGraphLauncher.Controls.DataGridViewDisableButtonColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.infoGrp.SuspendLayout();
            this.genParamsGrp.SuspendLayout();
            this.optionsGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.implementationCountNumeric)).BeginInit();
            this.calculationStatusGrp.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calculationStatusGrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.infoGrp);
            this.splitContainer1.Panel1.Controls.Add(this.genParamsGrp);
            this.splitContainer1.Panel1.Controls.Add(this.optionsGrp);
            this.splitContainer1.Panel1MinSize = 1;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(820, 476);
            this.splitContainer1.SplitterDistance = 293;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // infoGrp
            // 
            this.infoGrp.Controls.Add(this.description);
            this.infoGrp.Controls.Add(this.modelName);
            this.infoGrp.Location = new System.Drawing.Point(3, 3);
            this.infoGrp.Name = "infoGrp";
            this.infoGrp.Size = new System.Drawing.Size(150, 285);
            this.infoGrp.TabIndex = 6;
            this.infoGrp.TabStop = false;
            this.infoGrp.Text = "Model Information";
            // 
            // description
            // 
            this.description.AutoSize = true;
            this.description.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.description.Location = new System.Drawing.Point(7, 66);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(79, 17);
            this.description.TabIndex = 1;
            this.description.Text = "Description";
            // 
            // modelName
            // 
            this.modelName.AutoSize = true;
            this.modelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.modelName.Location = new System.Drawing.Point(7, 20);
            this.modelName.Name = "modelName";
            this.modelName.Size = new System.Drawing.Size(85, 17);
            this.modelName.TabIndex = 0;
            this.modelName.Text = "Model name";
            //
           
            // 
            // genParamsGrp
            // 
            this.genParamsGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.genParamsGrp.Controls.Add(this.label2);
            this.genParamsGrp.Location = new System.Drawing.Point(160, 3);
            this.genParamsGrp.Name = "genParamsGrp";
            this.genParamsGrp.Size = new System.Drawing.Size(446, 285);
            this.genParamsGrp.TabIndex = 5;
            this.genParamsGrp.TabStop = false;
            this.genParamsGrp.Text = "Generation Parameters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 0;
            // 
            // optionsGrp
            // 
            this.optionsGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsGrp.Controls.Add(this.deselectAllCheck);
            this.optionsGrp.Controls.Add(this.selectAllCheck);
            this.optionsGrp.Controls.Add(this.cycles);
            this.optionsGrp.Controls.Add(this.motives);
            this.optionsGrp.Controls.Add(this.constantInputLabel);
            this.optionsGrp.Controls.Add(this.constantInput);
            this.optionsGrp.Controls.Add(this.stepcountInput);
            this.optionsGrp.Controls.Add(this.stepcountLabel);
            this.optionsGrp.Controls.Add(this.cyclesHighCmb);
            this.optionsGrp.Controls.Add(this.cyclesLowCmb);
            this.optionsGrp.Controls.Add(this.motiveHighCmb);
            this.optionsGrp.Controls.Add(this.motiveLowCmb);
            this.optionsGrp.Controls.Add(this.optionsCheckList);
            this.optionsGrp.Location = new System.Drawing.Point(611, 3);
            this.optionsGrp.Name = "optionsGrp";
            this.optionsGrp.Size = new System.Drawing.Size(204, 300);
            this.optionsGrp.TabIndex = 4;
            this.optionsGrp.TabStop = false;
            this.optionsGrp.Text = "Analize Options";
            // 
            // deselectAllCheck
            // 
            this.deselectAllCheck.AutoSize = true;
            this.deselectAllCheck.Location = new System.Drawing.Point(82, 18);
            this.deselectAllCheck.Name = "deselectAllCheck";
            this.deselectAllCheck.Size = new System.Drawing.Size(84, 17);
            this.deselectAllCheck.TabIndex = 8;
            this.deselectAllCheck.Text = "DeSelect All";
            this.deselectAllCheck.UseVisualStyleBackColor = true;
            this.deselectAllCheck.CheckedChanged += new System.EventHandler(this.deselectallcheckBox_CheckedChanged);
            // 
            // selectAllCheck
            // 
            this.selectAllCheck.AutoSize = true;
            this.selectAllCheck.Checked = true;
            this.selectAllCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.selectAllCheck.Enabled = false;
            this.selectAllCheck.Location = new System.Drawing.Point(6, 18);
            this.selectAllCheck.Name = "selectAllCheck";
            this.selectAllCheck.Size = new System.Drawing.Size(70, 17);
            this.selectAllCheck.TabIndex = 7;
            this.selectAllCheck.Text = "Select All";
            this.selectAllCheck.UseVisualStyleBackColor = true;
            this.selectAllCheck.CheckedChanged += new System.EventHandler(this.selectallcheckBox_CheckedChanged);
            // 
            // cycles
            // 
            this.cycles.AutoSize = true;
            this.cycles.Location = new System.Drawing.Point(6, 209);
            this.cycles.Name = "cycles";
            this.cycles.Size = new System.Drawing.Size(38, 13);
            this.cycles.TabIndex = 6;
            this.cycles.Text = "Cycles";
            this.cycles.Visible = false;
            // 
            // motives
            // 
            this.motives.AutoSize = true;
            this.motives.Location = new System.Drawing.Point(6, 183);
            this.motives.Name = "motives";
            this.motives.Size = new System.Drawing.Size(44, 13);
            this.motives.TabIndex = 5;
            this.motives.Text = "Motives";
            this.motives.Visible = false;
            //
            //constantInput
            //

            this.constantInput.Location  = new System.Drawing.Point(90, 228);
            this.constantInput.Size =  new System.Drawing.Size(80, 21);
            this.constantInput.TextChanged += new System.EventHandler(this.constant_InputChange);


            this.constantInputLabel.Location = new System.Drawing.Point(6, 228);
            this.constantInputLabel.Size = new System.Drawing.Size(80, 13);

            this.constantInputLabel.Text = "Nu for Trajectory";

            //
            //stepcount
            //

            this.stepcountInput.Location = new System.Drawing.Point(90, 250);
            this.stepcountInput.Size = new System.Drawing.Size(80, 21);
            this.stepcountInput.TextChanged += new System.EventHandler(this.stepcount_InputChange);


            this.stepcountLabel.Location = new System.Drawing.Point(6, 250);
            this.stepcountLabel.Size = new System.Drawing.Size(80, 13);

            this.stepcountLabel.Text = "Steps for Trajectory";

            // 
            // cyclesHighCmb
            //  
            this.cyclesHighCmb.FormatString = "N2";
            this.cyclesHighCmb.FormattingEnabled = true;
            this.cyclesHighCmb.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cyclesHighCmb.Location = new System.Drawing.Point(136, 206);
            this.cyclesHighCmb.MaxDropDownItems = 4;
            this.cyclesHighCmb.Name = "cyclesHighCmb";
            this.cyclesHighCmb.Size = new System.Drawing.Size(51, 21);
            this.cyclesHighCmb.TabIndex = 4;
            this.cyclesHighCmb.Visible = false;
            this.cyclesHighCmb.SelectedIndexChanged += new System.EventHandler(this.cyclesHi_SelectedIndexChanged);
            // 
            // cyclesLowCmb
            // 
            this.cyclesLowCmb.FormatString = "N2";
            this.cyclesLowCmb.FormattingEnabled = true;
            this.cyclesLowCmb.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cyclesLowCmb.Location = new System.Drawing.Point(66, 206);
            this.cyclesLowCmb.MaxDropDownItems = 4;
            this.cyclesLowCmb.Name = "cyclesLowCmb";
            this.cyclesLowCmb.Size = new System.Drawing.Size(49, 21);
            this.cyclesLowCmb.TabIndex = 3;
            this.cyclesLowCmb.Visible = false;
            this.cyclesLowCmb.SelectedIndexChanged += new System.EventHandler(this.cyclesLow_SelectedIndexChanged);
            // 
            // motiveHighCmb
            // 
            this.motiveHighCmb.FormatString = "N2";
            this.motiveHighCmb.FormattingEnabled = true;
            this.motiveHighCmb.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.motiveHighCmb.Location = new System.Drawing.Point(136, 180);
            this.motiveHighCmb.MaxDropDownItems = 4;
            this.motiveHighCmb.Name = "motiveHighCmb";
            this.motiveHighCmb.Size = new System.Drawing.Size(51, 21);
            this.motiveHighCmb.TabIndex = 2;
            this.motiveHighCmb.Visible = false;
            this.motiveHighCmb.SelectedIndexChanged += new System.EventHandler(this.motiveHi_SelectedIndexChanged);
            // 
            // motiveLowCmb
            // 
            this.motiveLowCmb.DisplayMember = "1";
            this.motiveLowCmb.FormatString = "N2";
            this.motiveLowCmb.FormattingEnabled = true;
            this.motiveLowCmb.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.motiveLowCmb.Location = new System.Drawing.Point(66, 180);
            this.motiveLowCmb.MaxDropDownItems = 4;
            this.motiveLowCmb.Name = "motiveLowCmb";
            this.motiveLowCmb.Size = new System.Drawing.Size(49, 21);
            this.motiveLowCmb.TabIndex = 1;
            this.motiveLowCmb.ValueMember = "1";
            this.motiveLowCmb.Visible = false;
            this.motiveLowCmb.SelectedIndexChanged += new System.EventHandler(this.motiveLow_SelectedIndexChanged);
            // 
            // optionsCheckList
            // 
            this.optionsCheckList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.optionsCheckList.CheckOnClick = true;
            this.optionsCheckList.FormattingEnabled = true;
            this.optionsCheckList.Location = new System.Drawing.Point(6, 41);
            this.optionsCheckList.Name = "optionsCheckList";
            this.optionsCheckList.Size = new System.Drawing.Size(191, 150);
            this.optionsCheckList.Sorted = true;
            this.optionsCheckList.TabIndex = 0;
            this.optionsCheckList.ThreeDCheckBoxes = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.implementationCountNumeric);
            this.splitContainer2.Panel1.Controls.Add(this.startBtn);
            this.splitContainer2.Panel1.Controls.Add(this.continueBtn);
            this.splitContainer2.Panel1.Controls.Add(this.implemetation);
            this.splitContainer2.Panel1.Controls.Add(this.pauseBtn);
            this.splitContainer2.Panel1.Controls.Add(this.stopBtn);
            this.splitContainer2.Panel1MinSize = 1;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.calculationStatusGrp);
            this.splitContainer2.Panel2.Controls.Add(this.axShockwaveFlash1);
            this.splitContainer2.Size = new System.Drawing.Size(820, 177);
            this.splitContainer2.SplitterDistance = 71;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 19;
            // 
            // implementationCountNumeric
            // 
            this.implementationCountNumeric.Location = new System.Drawing.Point(84, 25);
            this.implementationCountNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.implementationCountNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.implementationCountNumeric.Name = "implementationCountNumeric";
            this.implementationCountNumeric.Size = new System.Drawing.Size(120, 20);
            this.implementationCountNumeric.TabIndex = 16;
            this.implementationCountNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // startBtn
            // 
            this.startBtn.BackgroundImage = global::RandomGraphLauncher.Properties.Resources.Start;
            this.startBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.startBtn.Location = new System.Drawing.Point(218, 11);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(40, 40);
            this.startBtn.TabIndex = 12;
            this.toolTip1.SetToolTip(this.startBtn, "Start calculation");
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.EnabledChanged += new System.EventHandler(this.StartButtonEnableChanged);
            this.startBtn.Click += new System.EventHandler(this.startButton_Click);
            // 
            // continueBtn
            // 
            this.continueBtn.BackgroundImage = global::RandomGraphLauncher.Properties.Resources.Cont_dis;
            this.continueBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.continueBtn.Enabled = false;
            this.continueBtn.Location = new System.Drawing.Point(367, 11);
            this.continueBtn.Name = "continueBtn";
            this.continueBtn.Size = new System.Drawing.Size(40, 40);
            this.continueBtn.TabIndex = 15;
            this.toolTip1.SetToolTip(this.continueBtn, "Continue calculation");
            this.continueBtn.UseVisualStyleBackColor = true;
            this.continueBtn.EnabledChanged += new System.EventHandler(this.ContinueButtonEnableChanged);
            this.continueBtn.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // implemetation
            // 
            this.implemetation.AutoSize = true;
            this.implemetation.Location = new System.Drawing.Point(4, 28);
            this.implemetation.Name = "implemetation";
            this.implemetation.Size = new System.Drawing.Size(78, 13);
            this.implemetation.TabIndex = 11;
            this.implemetation.Text = "Implementation";
            // 
            // pauseBtn
            // 
            this.pauseBtn.BackgroundImage = global::RandomGraphLauncher.Properties.Resources.Pause_dis;
            this.pauseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pauseBtn.Enabled = false;
            this.pauseBtn.Location = new System.Drawing.Point(315, 11);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(40, 40);
            this.pauseBtn.TabIndex = 14;
            this.toolTip1.SetToolTip(this.pauseBtn, "Pause calculation");
            this.pauseBtn.UseVisualStyleBackColor = true;
            this.pauseBtn.EnabledChanged += new System.EventHandler(this.PauseButtonEnableChanged);
            this.pauseBtn.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.BackgroundImage = global::RandomGraphLauncher.Properties.Resources.Stop_dis;
            this.stopBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.stopBtn.Enabled = false;
            this.stopBtn.Location = new System.Drawing.Point(267, 11);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(40, 40);
            this.stopBtn.TabIndex = 13;
            this.toolTip1.SetToolTip(this.stopBtn, "Stop calculation");
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.EnabledChanged += new System.EventHandler(this.StopButtonEnableChanged);
            this.stopBtn.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // calculationStatusGrp
            // 
            this.calculationStatusGrp.Controls.Add(this.toolStrip1);
            this.calculationStatusGrp.Controls.Add(this.calculationStatusGrd);
            this.calculationStatusGrp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calculationStatusGrp.Location = new System.Drawing.Point(0, 0);
            this.calculationStatusGrp.Name = "calculationStatusGrp";
            this.calculationStatusGrp.Size = new System.Drawing.Size(818, 98);
            this.calculationStatusGrp.TabIndex = 17;
            this.calculationStatusGrp.TabStop = false;
            this.calculationStatusGrp.Text = "Current Progress";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 70);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(812, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            // 
            // calculationStatusGrd
            // 
            this.calculationStatusGrd.AllowUserToAddRows = false;
            this.calculationStatusGrd.AllowUserToDeleteRows = false;
            this.calculationStatusGrd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.calculationStatusGrd.BackgroundColor = System.Drawing.Color.White;
            this.calculationStatusGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.calculationStatusGrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.statusColumn,
            this.percentColumn,
            this.taskColumn,
            this.hostColumn,
            this.stopColumn,
            this.manageColumn});
            this.calculationStatusGrd.Location = new System.Drawing.Point(4, 19);
            this.calculationStatusGrd.Name = "calculationStatusGrd";
            this.calculationStatusGrd.ReadOnly = true;
            this.calculationStatusGrd.RowHeadersVisible = false;
            this.calculationStatusGrd.RowTemplate.Height = 35;
            this.calculationStatusGrd.Size = new System.Drawing.Size(812, 51);
            this.calculationStatusGrd.TabIndex = 4;
            this.calculationStatusGrd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableCellClick);
            // 
            // idColumn
            // 
            this.idColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.idColumn.HeaderText = "ID";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.idColumn.Width = 24;
            // 
            // statusColumn
            // 
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            this.statusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // percentColumn
            // 
            this.percentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.percentColumn.HeaderText = "Percent";
            this.percentColumn.Name = "percentColumn";
            this.percentColumn.ProgressBarColor = System.Drawing.Color.Green;
            this.percentColumn.ReadOnly = true;
            this.percentColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.percentColumn.Width = 50;
            // 
            // taskColumn
            // 
            this.taskColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.taskColumn.HeaderText = "Task";
            this.taskColumn.Name = "taskColumn";
            this.taskColumn.ReadOnly = true;
            this.taskColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // hostColumn
            // 
            this.hostColumn.HeaderText = "Host";
            this.hostColumn.Name = "hostColumn";
            this.hostColumn.ReadOnly = true;
            this.hostColumn.Visible = false;
            // 
            // stopColumn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            this.stopColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.stopColumn.HeaderText = "Stop";
            this.stopColumn.Name = "stopColumn";
            this.stopColumn.ReadOnly = true;
            this.stopColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.stopColumn.Text = "Stop";
            this.stopColumn.UseColumnTextForButtonValue = true;
            // 
            // manageColumn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            this.manageColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.manageColumn.HeaderText = "Manage";
            this.manageColumn.Name = "manageColumn";
            this.manageColumn.ReadOnly = true;
            this.manageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.manageColumn.Text = "Pause";
            this.manageColumn.UseColumnTextForButtonValue = true;
            // 
            // axShockwaveFlash1
            // 
            this.axShockwaveFlash1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axShockwaveFlash1.Enabled = true;
            this.axShockwaveFlash1.Location = new System.Drawing.Point(0, 0);
            this.axShockwaveFlash1.Name = "axShockwaveFlash1";
            this.axShockwaveFlash1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash1.OcxState")));
            this.axShockwaveFlash1.Size = new System.Drawing.Size(818, 98);
            this.axShockwaveFlash1.TabIndex = 18;
            // 
            // backgroundStartWorker
            // 
            this.backgroundStartWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.StartWork);
            // 
            // backgroundStopWorker
            // 
            this.backgroundStopWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.StopWork);
            // 
            // backgroundPauseWorker
            // 
            this.backgroundPauseWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PauseWork);
            // 
            // backgroundContinueWorker
            // 
            this.backgroundContinueWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ContiuneWork);
            // 
            // dataGridViewDisableButtonColumn1
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            this.dataGridViewDisableButtonColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewDisableButtonColumn1.HeaderText = "Stop";
            this.dataGridViewDisableButtonColumn1.Name = "dataGridViewDisableButtonColumn1";
            this.dataGridViewDisableButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDisableButtonColumn1.Text = "Stop";
            this.dataGridViewDisableButtonColumn1.UseColumnTextForButtonValue = true;
            // 
            // dataGridViewDisableButtonColumn2
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(2);
            this.dataGridViewDisableButtonColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewDisableButtonColumn2.HeaderText = "Manage";
            this.dataGridViewDisableButtonColumn2.Name = "dataGridViewDisableButtonColumn2";
            this.dataGridViewDisableButtonColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDisableButtonColumn2.Text = "Pause";
            this.dataGridViewDisableButtonColumn2.UseColumnTextForButtonValue = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // CalculationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "CalculationControl";
            this.Size = new System.Drawing.Size(820, 476);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.infoGrp.ResumeLayout(false);
            this.infoGrp.PerformLayout();
            this.genParamsGrp.ResumeLayout(false);
            this.genParamsGrp.PerformLayout();
            this.optionsGrp.ResumeLayout(false);
            this.optionsGrp.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.implementationCountNumeric)).EndInit();
            this.calculationStatusGrp.ResumeLayout(false);
            this.calculationStatusGrp.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calculationStatusGrd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox infoGrp;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.Label modelName;
        private System.Windows.Forms.GroupBox genParamsGrp;
        private System.Windows.Forms.GroupBox optionsGrp;
        private System.Windows.Forms.CheckedListBox optionsCheckList;
        private System.Windows.Forms.NumericUpDown implementationCountNumeric;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button continueBtn;
        private System.Windows.Forms.Label implemetation;
        private System.Windows.Forms.Button pauseBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.GroupBox calculationStatusGrp;
        private System.Windows.Forms.DataGridView calculationStatusGrd;
        private System.ComponentModel.BackgroundWorker backgroundStartWorker;
        private System.ComponentModel.BackgroundWorker backgroundStopWorker;
        private System.ComponentModel.BackgroundWorker backgroundPauseWorker;
        private System.ComponentModel.BackgroundWorker backgroundContinueWorker;
        private Controls.DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn1;
        private Controls.DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cyclesHighCmb;
        private System.Windows.Forms.ComboBox cyclesLowCmb;
        private System.Windows.Forms.TextBox constantInput;
        private System.Windows.Forms.Label constantInputLabel;
        private System.Windows.Forms.TextBox stepcountInput;
        private System.Windows.Forms.Label stepcountLabel;
        private System.Windows.Forms.ComboBox motiveHighCmb;
        private System.Windows.Forms.ComboBox motiveLowCmb;
        private System.Windows.Forms.Label cycles;
        private System.Windows.Forms.Label motives;
        private System.Windows.Forms.CheckBox selectAllCheck;
        private System.Windows.Forms.CheckBox deselectAllCheck;
        private System.Windows.Forms.Label checkModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private controls.DataGridViewProgressColumn percentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hostColumn;
        private Controls.DataGridViewDisableButtonColumn stopColumn;
        private Controls.DataGridViewDisableButtonColumn manageColumn;
    }
}
