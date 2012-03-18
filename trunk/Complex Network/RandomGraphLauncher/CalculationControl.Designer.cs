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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Gen_Rule = new System.Windows.Forms.Label();
            this.Description = new System.Windows.Forms.Label();
            this.model_Name = new System.Windows.Forms.Label();
            this.groupBox_Gen_params = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox_Options = new System.Windows.Forms.GroupBox();
            this.checkedListBox_Options = new System.Windows.Forms.CheckedListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.numericUpDown_Instances_Count = new System.Windows.Forms.NumericUpDown();
            this.startButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pauseButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percentColumn = new RandomGraphLauncher.controls.DataGridViewProgressColumn();
            this.taskColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Host = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stopColumn = new RandomGraphLauncher.controls.DataGridViewDisableButtonColumn();
            this.manageColumn = new RandomGraphLauncher.controls.DataGridViewDisableButtonColumn();
            this.axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.backgroundStartWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundStopWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundPauseWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundContinueWorker = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewDisableButtonColumn1 = new RandomGraphLauncher.controls.DataGridViewDisableButtonColumn();
            this.dataGridViewDisableButtonColumn2 = new RandomGraphLauncher.controls.DataGridViewDisableButtonColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox_Gen_params.SuspendLayout();
            this.groupBox_Options.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Instances_Count)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox_Gen_params);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox_Options);
            this.splitContainer1.Panel1MinSize = 1;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(769, 408);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Gen_Rule);
            this.groupBox4.Controls.Add(this.Description);
            this.groupBox4.Controls.Add(this.model_Name);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(209, 212);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Info";
            // 
            // Gen_Rule
            // 
            this.Gen_Rule.AutoSize = true;
            this.Gen_Rule.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Gen_Rule.Location = new System.Drawing.Point(7, 111);
            this.Gen_Rule.Name = "Gen_Rule";
            this.Gen_Rule.Size = new System.Drawing.Size(107, 17);
            this.Gen_Rule.TabIndex = 2;
            this.Gen_Rule.Text = "Generation rule";
            // 
            // Description
            // 
            this.Description.AutoSize = true;
            this.Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Description.Location = new System.Drawing.Point(7, 66);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(79, 17);
            this.Description.TabIndex = 1;
            this.Description.Text = "Description";
            // 
            // model_Name
            // 
            this.model_Name.AutoSize = true;
            this.model_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.model_Name.Location = new System.Drawing.Point(7, 20);
            this.model_Name.Name = "model_Name";
            this.model_Name.Size = new System.Drawing.Size(85, 17);
            this.model_Name.TabIndex = 0;
            this.model_Name.Text = "Model name";
            // 
            // groupBox_Gen_params
            // 
            this.groupBox_Gen_params.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Gen_params.Controls.Add(this.label2);
            this.groupBox_Gen_params.Location = new System.Drawing.Point(218, 3);
            this.groupBox_Gen_params.Name = "groupBox_Gen_params";
            this.groupBox_Gen_params.Size = new System.Drawing.Size(336, 212);
            this.groupBox_Gen_params.TabIndex = 5;
            this.groupBox_Gen_params.TabStop = false;
            this.groupBox_Gen_params.Text = "Generation parameters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 0;
            // 
            // groupBox_Options
            // 
            this.groupBox_Options.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Options.Controls.Add(this.checkedListBox_Options);
            this.groupBox_Options.Location = new System.Drawing.Point(560, 3);
            this.groupBox_Options.Name = "groupBox_Options";
            this.groupBox_Options.Size = new System.Drawing.Size(204, 212);
            this.groupBox_Options.TabIndex = 4;
            this.groupBox_Options.TabStop = false;
            this.groupBox_Options.Text = "Analize options";
            // 
            // checkedListBox_Options
            // 
            this.checkedListBox_Options.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox_Options.CheckOnClick = true;
            this.checkedListBox_Options.FormattingEnabled = true;
            this.checkedListBox_Options.Location = new System.Drawing.Point(7, 20);
            this.checkedListBox_Options.Name = "checkedListBox_Options";
            this.checkedListBox_Options.Size = new System.Drawing.Size(191, 135);
            this.checkedListBox_Options.Sorted = true;
            this.checkedListBox_Options.TabIndex = 0;
            this.checkedListBox_Options.ThreeDCheckBoxes = true;
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
            this.splitContainer2.Panel1.Controls.Add(this.numericUpDown_Instances_Count);
            this.splitContainer2.Panel1.Controls.Add(this.startButton);
            this.splitContainer2.Panel1.Controls.Add(this.continueButton);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.pauseButton);
            this.splitContainer2.Panel1.Controls.Add(this.stopButton);
            this.splitContainer2.Panel1MinSize = 1;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel2.Controls.Add(this.axShockwaveFlash1);
            this.splitContainer2.Size = new System.Drawing.Size(769, 182);
            this.splitContainer2.SplitterDistance = 71;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 19;
            // 
            // numericUpDown_Instances_Count
            // 
            this.numericUpDown_Instances_Count.Location = new System.Drawing.Point(84, 25);
            this.numericUpDown_Instances_Count.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Instances_Count.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Instances_Count.Name = "numericUpDown_Instances_Count";
            this.numericUpDown_Instances_Count.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Instances_Count.TabIndex = 16;
            this.numericUpDown_Instances_Count.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // startButton
            // 
            this.startButton.BackgroundImage = global::RandomGraphLauncher.Properties.Resources.Start;
            this.startButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.startButton.Location = new System.Drawing.Point(218, 11);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(40, 40);
            this.startButton.TabIndex = 12;
            this.toolTip1.SetToolTip(this.startButton, "Start calculation");
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.EnabledChanged += new System.EventHandler(this.StartButtonEnableChanged);
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.BackgroundImage = global::RandomGraphLauncher.Properties.Resources.Cont_dis;
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.continueButton.Enabled = false;
            this.continueButton.Location = new System.Drawing.Point(367, 11);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(40, 40);
            this.continueButton.TabIndex = 15;
            this.toolTip1.SetToolTip(this.continueButton, "Continue calculation");
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.EnabledChanged += new System.EventHandler(this.ContinueButtonEnableChanged);
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Implementation";
            // 
            // pauseButton
            // 
            this.pauseButton.BackgroundImage = global::RandomGraphLauncher.Properties.Resources.Pause_dis;
            this.pauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(315, 11);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(40, 40);
            this.pauseButton.TabIndex = 14;
            this.toolTip1.SetToolTip(this.pauseButton, "Pause calculation");
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.EnabledChanged += new System.EventHandler(this.PauseButtonEnableChanged);
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.BackgroundImage = global::RandomGraphLauncher.Properties.Resources.Stop_dis;
            this.stopButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(267, 11);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(40, 40);
            this.stopButton.TabIndex = 13;
            this.toolTip1.SetToolTip(this.stopButton, "Stop calculation");
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.EnabledChanged += new System.EventHandler(this.StopButtonEnableChanged);
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(767, 103);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current progress";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 75);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(761, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.statusColumn,
            this.percentColumn,
            this.taskColumn,
            this.Host,
            this.stopColumn,
            this.manageColumn});
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 35;
            this.dataGridView1.Size = new System.Drawing.Size(761, 56);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableCellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 24;
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
            // Host
            // 
            this.Host.HeaderText = "Host";
            this.Host.Name = "Host";
            this.Host.ReadOnly = true;
            this.Host.Visible = false;
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
            this.axShockwaveFlash1.Size = new System.Drawing.Size(767, 103);
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
            this.Size = new System.Drawing.Size(769, 408);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox_Gen_params.ResumeLayout(false);
            this.groupBox_Gen_params.PerformLayout();
            this.groupBox_Options.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Instances_Count)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label Gen_Rule;
        private System.Windows.Forms.Label Description;
        private System.Windows.Forms.Label model_Name;
        private System.Windows.Forms.GroupBox groupBox_Gen_params;
        private System.Windows.Forms.GroupBox groupBox_Options;
        private System.Windows.Forms.CheckedListBox checkedListBox_Options;
        private System.Windows.Forms.NumericUpDown numericUpDown_Instances_Count;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.ComponentModel.BackgroundWorker backgroundStartWorker;
        private System.ComponentModel.BackgroundWorker backgroundStopWorker;
        private System.ComponentModel.BackgroundWorker backgroundPauseWorker;
        private System.ComponentModel.BackgroundWorker backgroundContinueWorker;
        private controls.DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn1;
        private controls.DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private controls.DataGridViewProgressColumn percentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Host;
        private controls.DataGridViewDisableButtonColumn stopColumn;
        private controls.DataGridViewDisableButtonColumn manageColumn;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Label label2;
    }
}
