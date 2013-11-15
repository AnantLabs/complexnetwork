namespace Percolations
{
    partial class PercolationCounting
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
            this.networkSize = new System.Windows.Forms.Label();
            this.networkSizeTxt = new System.Windows.Forms.TextBox();
            this.modelName = new System.Windows.Forms.Label();
            this.modelNameCmb = new System.Windows.Forms.ComboBox();
            this.optionName = new System.Windows.Forms.Label();
            this.optionNameCmb = new System.Windows.Forms.ComboBox();
            this.branchIndex = new System.Windows.Forms.Label();
            this.branchIndexCmb = new System.Windows.Forms.ComboBox();
            this.maxLevel = new System.Windows.Forms.Label();
            this.maxLevelCmb = new System.Windows.Forms.ComboBox();
            this.realizationCount = new System.Windows.Forms.Label();
            this.realizationCountNum = new System.Windows.Forms.NumericUpDown();
            this.probabilityFunctionCmb = new System.Windows.Forms.ComboBox();
            this.probabilityFunction = new System.Windows.Forms.Label();
            this.startER = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.deltaExtendedTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.hLabel = new System.Windows.Forms.Label();
            this.muRangeLowExtendedTxt = new System.Windows.Forms.TextBox();
            this.muRangeHighExtendedTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.erLabel = new System.Windows.Forms.Label();
            this.probRangeLowExtendedTxt = new System.Windows.Forms.TextBox();
            this.probRangeHighExtendedTxt = new System.Windows.Forms.TextBox();
            this.startExtended = new System.Windows.Forms.Button();
            this.startHierarchic = new System.Windows.Forms.Button();
            this.startAvgHierarchic = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.realizationCountNum)).BeginInit();
            this.SuspendLayout();
            // 
            // networkSize
            // 
            this.networkSize.AutoSize = true;
            this.networkSize.Location = new System.Drawing.Point(300, 9);
            this.networkSize.Name = "networkSize";
            this.networkSize.Size = new System.Drawing.Size(70, 13);
            this.networkSize.TabIndex = 27;
            this.networkSize.Text = "Network Size";
            // 
            // networkSizeTxt
            // 
            this.networkSizeTxt.Location = new System.Drawing.Point(303, 25);
            this.networkSizeTxt.Name = "networkSizeTxt";
            this.networkSizeTxt.Size = new System.Drawing.Size(86, 20);
            this.networkSizeTxt.TabIndex = 26;
            this.networkSizeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // modelName
            // 
            this.modelName.AutoSize = true;
            this.modelName.Location = new System.Drawing.Point(12, 9);
            this.modelName.Name = "modelName";
            this.modelName.Size = new System.Drawing.Size(67, 13);
            this.modelName.TabIndex = 0;
            this.modelName.Text = "Model Name";
            // 
            // modelNameCmb
            // 
            this.modelNameCmb.Enabled = false;
            this.modelNameCmb.FormattingEnabled = true;
            this.modelNameCmb.Location = new System.Drawing.Point(15, 25);
            this.modelNameCmb.Name = "modelNameCmb";
            this.modelNameCmb.Size = new System.Drawing.Size(153, 21);
            this.modelNameCmb.TabIndex = 1;
            // 
            // optionName
            // 
            this.optionName.AutoSize = true;
            this.optionName.Location = new System.Drawing.Point(12, 61);
            this.optionName.Name = "optionName";
            this.optionName.Size = new System.Drawing.Size(69, 13);
            this.optionName.TabIndex = 2;
            this.optionName.Text = "Option Name";
            // 
            // optionNameCmb
            // 
            this.optionNameCmb.Enabled = false;
            this.optionNameCmb.FormattingEnabled = true;
            this.optionNameCmb.Items.AddRange(new object[] {
            "Connected Subgraph"});
            this.optionNameCmb.Location = new System.Drawing.Point(12, 77);
            this.optionNameCmb.Name = "optionNameCmb";
            this.optionNameCmb.Size = new System.Drawing.Size(156, 21);
            this.optionNameCmb.TabIndex = 3;
            // 
            // branchIndex
            // 
            this.branchIndex.AutoSize = true;
            this.branchIndex.Location = new System.Drawing.Point(208, 9);
            this.branchIndex.Name = "branchIndex";
            this.branchIndex.Size = new System.Drawing.Size(70, 13);
            this.branchIndex.TabIndex = 4;
            this.branchIndex.Text = "Branch Index";
            this.branchIndex.Visible = false;
            // 
            // branchIndexCmb
            // 
            this.branchIndexCmb.FormattingEnabled = true;
            this.branchIndexCmb.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
            this.branchIndexCmb.Location = new System.Drawing.Point(211, 25);
            this.branchIndexCmb.Name = "branchIndexCmb";
            this.branchIndexCmb.Size = new System.Drawing.Size(72, 21);
            this.branchIndexCmb.TabIndex = 5;
            this.branchIndexCmb.Visible = false;
            this.branchIndexCmb.SelectedIndexChanged += new System.EventHandler(this.branchIndexCmb_SelectedIndexChanged);
            // 
            // maxLevel
            // 
            this.maxLevel.AutoSize = true;
            this.maxLevel.Location = new System.Drawing.Point(208, 61);
            this.maxLevel.Name = "maxLevel";
            this.maxLevel.Size = new System.Drawing.Size(56, 13);
            this.maxLevel.TabIndex = 5;
            this.maxLevel.Text = "Max Level";
            this.maxLevel.Visible = false;
            // 
            // maxLevelCmb
            // 
            this.maxLevelCmb.FormattingEnabled = true;
            this.maxLevelCmb.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.maxLevelCmb.Location = new System.Drawing.Point(211, 77);
            this.maxLevelCmb.Name = "maxLevelCmb";
            this.maxLevelCmb.Size = new System.Drawing.Size(72, 21);
            this.maxLevelCmb.TabIndex = 6;
            this.maxLevelCmb.Visible = false;
            this.maxLevelCmb.SelectedIndexChanged += new System.EventHandler(this.maxLevelCmb_SelectedIndexChanged);
            // 
            // realizationCount
            // 
            this.realizationCount.AutoSize = true;
            this.realizationCount.Location = new System.Drawing.Point(300, 61);
            this.realizationCount.Name = "realizationCount";
            this.realizationCount.Size = new System.Drawing.Size(90, 13);
            this.realizationCount.TabIndex = 7;
            this.realizationCount.Text = "Realization Count";
            // 
            // realizationCountNum
            // 
            this.realizationCountNum.Location = new System.Drawing.Point(303, 77);
            this.realizationCountNum.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.realizationCountNum.Name = "realizationCountNum";
            this.realizationCountNum.Size = new System.Drawing.Size(87, 20);
            this.realizationCountNum.TabIndex = 8;
            this.realizationCountNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.realizationCountNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // probabilityFunctionCmb
            // 
            this.probabilityFunctionCmb.FormattingEnabled = true;
            this.probabilityFunctionCmb.Location = new System.Drawing.Point(268, 150);
            this.probabilityFunctionCmb.Name = "probabilityFunctionCmb";
            this.probabilityFunctionCmb.Size = new System.Drawing.Size(121, 21);
            this.probabilityFunctionCmb.TabIndex = 54;
            this.probabilityFunctionCmb.Visible = false;
            // 
            // probabilityFunction
            // 
            this.probabilityFunction.AutoSize = true;
            this.probabilityFunction.Location = new System.Drawing.Point(265, 134);
            this.probabilityFunction.Name = "probabilityFunction";
            this.probabilityFunction.Size = new System.Drawing.Size(99, 13);
            this.probabilityFunction.TabIndex = 53;
            this.probabilityFunction.Text = "Probability Function";
            this.probabilityFunction.Visible = false;
            // 
            // startER
            // 
            this.startER.Location = new System.Drawing.Point(414, 12);
            this.startER.Name = "startER";
            this.startER.Size = new System.Drawing.Size(100, 34);
            this.startER.TabIndex = 52;
            this.startER.Text = "Start ER";
            this.startER.UseVisualStyleBackColor = true;
            this.startER.Visible = false;
            this.startER.Click += new System.EventHandler(this.startER_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "-";
            // 
            // deltaExtendedTxt
            // 
            this.deltaExtendedTxt.Location = new System.Drawing.Point(154, 150);
            this.deltaExtendedTxt.Name = "deltaExtendedTxt";
            this.deltaExtendedTxt.Size = new System.Drawing.Size(101, 20);
            this.deltaExtendedTxt.TabIndex = 50;
            this.deltaExtendedTxt.Text = "0.1";
            this.deltaExtendedTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Delta";
            // 
            // hLabel
            // 
            this.hLabel.AutoSize = true;
            this.hLabel.Location = new System.Drawing.Point(12, 134);
            this.hLabel.Name = "hLabel";
            this.hLabel.Size = new System.Drawing.Size(69, 13);
            this.hLabel.TabIndex = 46;
            this.hLabel.Text = "Range of Mu";
            this.hLabel.Visible = false;
            // 
            // muRangeLowExtendedTxt
            // 
            this.muRangeLowExtendedTxt.Location = new System.Drawing.Point(12, 150);
            this.muRangeLowExtendedTxt.Name = "muRangeLowExtendedTxt";
            this.muRangeLowExtendedTxt.Size = new System.Drawing.Size(47, 20);
            this.muRangeLowExtendedTxt.TabIndex = 47;
            this.muRangeLowExtendedTxt.Text = "0";
            this.muRangeLowExtendedTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.muRangeLowExtendedTxt.Visible = false;
            // 
            // muRangeHighExtendedTxt
            // 
            this.muRangeHighExtendedTxt.Location = new System.Drawing.Point(69, 150);
            this.muRangeHighExtendedTxt.Name = "muRangeHighExtendedTxt";
            this.muRangeHighExtendedTxt.Size = new System.Drawing.Size(47, 20);
            this.muRangeHighExtendedTxt.TabIndex = 48;
            this.muRangeHighExtendedTxt.Text = "1";
            this.muRangeHighExtendedTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.muRangeHighExtendedTxt.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "-";
            this.label2.Visible = false;
            // 
            // erLabel
            // 
            this.erLabel.AutoSize = true;
            this.erLabel.Location = new System.Drawing.Point(12, 134);
            this.erLabel.Name = "erLabel";
            this.erLabel.Size = new System.Drawing.Size(110, 13);
            this.erLabel.TabIndex = 42;
            this.erLabel.Text = "Range of Probabilities";
            this.erLabel.Visible = false;
            // 
            // probRangeLowExtendedTxt
            // 
            this.probRangeLowExtendedTxt.Location = new System.Drawing.Point(12, 150);
            this.probRangeLowExtendedTxt.Name = "probRangeLowExtendedTxt";
            this.probRangeLowExtendedTxt.Size = new System.Drawing.Size(47, 20);
            this.probRangeLowExtendedTxt.TabIndex = 43;
            this.probRangeLowExtendedTxt.Text = "0";
            this.probRangeLowExtendedTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.probRangeLowExtendedTxt.Visible = false;
            // 
            // probRangeHighExtendedTxt
            // 
            this.probRangeHighExtendedTxt.Location = new System.Drawing.Point(69, 150);
            this.probRangeHighExtendedTxt.Name = "probRangeHighExtendedTxt";
            this.probRangeHighExtendedTxt.Size = new System.Drawing.Size(47, 20);
            this.probRangeHighExtendedTxt.TabIndex = 44;
            this.probRangeHighExtendedTxt.Text = "1";
            this.probRangeHighExtendedTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.probRangeHighExtendedTxt.Visible = false;
            // 
            // startExtended
            // 
            this.startExtended.Location = new System.Drawing.Point(414, 64);
            this.startExtended.Name = "startExtended";
            this.startExtended.Size = new System.Drawing.Size(100, 34);
            this.startExtended.TabIndex = 41;
            this.startExtended.Text = "Start Hierarchic";
            this.startExtended.UseVisualStyleBackColor = true;
            this.startExtended.Visible = false;
            this.startExtended.Click += new System.EventHandler(this.startExtended_Click);
            // 
            // startHierarchic
            // 
            this.startHierarchic.Location = new System.Drawing.Point(414, 114);
            this.startHierarchic.Name = "startHierarchic";
            this.startHierarchic.Size = new System.Drawing.Size(100, 34);
            this.startHierarchic.TabIndex = 55;
            this.startHierarchic.Text = "Start Hierarchic (Opt)";
            this.startHierarchic.UseVisualStyleBackColor = true;
            this.startHierarchic.Visible = false;
            this.startHierarchic.Click += new System.EventHandler(this.startHierarchic_Click);
            // 
            // startAvgHierarchic
            // 
            this.startAvgHierarchic.Location = new System.Drawing.Point(414, 161);
            this.startAvgHierarchic.Name = "startAvgHierarchic";
            this.startAvgHierarchic.Size = new System.Drawing.Size(100, 34);
            this.startAvgHierarchic.TabIndex = 56;
            this.startAvgHierarchic.Text = "Start Hierarchic (Avg)";
            this.startAvgHierarchic.UseVisualStyleBackColor = true;
            this.startAvgHierarchic.Visible = false;
            this.startAvgHierarchic.Click += new System.EventHandler(this.startAvgHierarchic_Click);
            // 
            // PercolationCounting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 207);
            this.Controls.Add(this.startAvgHierarchic);
            this.Controls.Add(this.startHierarchic);
            this.Controls.Add(this.probabilityFunctionCmb);
            this.Controls.Add(this.probabilityFunction);
            this.Controls.Add(this.startER);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.deltaExtendedTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.hLabel);
            this.Controls.Add(this.muRangeLowExtendedTxt);
            this.Controls.Add(this.muRangeHighExtendedTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.erLabel);
            this.Controls.Add(this.probRangeLowExtendedTxt);
            this.Controls.Add(this.probRangeHighExtendedTxt);
            this.Controls.Add(this.startExtended);
            this.Controls.Add(this.realizationCountNum);
            this.Controls.Add(this.realizationCount);
            this.Controls.Add(this.maxLevelCmb);
            this.Controls.Add(this.maxLevel);
            this.Controls.Add(this.branchIndexCmb);
            this.Controls.Add(this.branchIndex);
            this.Controls.Add(this.optionNameCmb);
            this.Controls.Add(this.optionName);
            this.Controls.Add(this.modelNameCmb);
            this.Controls.Add(this.modelName);
            this.Controls.Add(this.networkSizeTxt);
            this.Controls.Add(this.networkSize);
            this.Name = "PercolationCounting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Percolations";
            this.Load += new System.EventHandler(this.PercolationCounting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.realizationCountNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label modelName;
        private System.Windows.Forms.ComboBox modelNameCmb;
        private System.Windows.Forms.Label optionName;
        private System.Windows.Forms.ComboBox optionNameCmb;
        private System.Windows.Forms.Label branchIndex;
        private System.Windows.Forms.ComboBox branchIndexCmb;
        private System.Windows.Forms.Label maxLevel;
        private System.Windows.Forms.ComboBox maxLevelCmb;
        private System.Windows.Forms.Label realizationCount;
        private System.Windows.Forms.NumericUpDown realizationCountNum;
        private System.Windows.Forms.Label networkSize;
        private System.Windows.Forms.TextBox networkSizeTxt;
        private System.Windows.Forms.ComboBox probabilityFunctionCmb;
        private System.Windows.Forms.Label probabilityFunction;
        private System.Windows.Forms.Button startER;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox deltaExtendedTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label hLabel;
        private System.Windows.Forms.TextBox muRangeLowExtendedTxt;
        private System.Windows.Forms.TextBox muRangeHighExtendedTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label erLabel;
        private System.Windows.Forms.TextBox probRangeLowExtendedTxt;
        private System.Windows.Forms.TextBox probRangeHighExtendedTxt;
        private System.Windows.Forms.Button startExtended;
        private System.Windows.Forms.Button startHierarchic;
        private System.Windows.Forms.Button startAvgHierarchic;
    }
}

