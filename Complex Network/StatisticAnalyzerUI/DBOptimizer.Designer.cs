namespace StatisticAnalyzerUI
{
    partial class DBOptimizer
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
            this.connectionString = new System.Windows.Forms.Label();
            this.connectionStringTxt = new System.Windows.Forms.TextBox();
            this.connectionsBtn = new System.Windows.Forms.Button();
            this.sqlConnectionGrp = new System.Windows.Forms.GroupBox();
            this.AllJobsRadio = new System.Windows.Forms.RadioButton();
            this.ByJobsRadio = new System.Windows.Forms.RadioButton();
            this.availableJobs = new System.Windows.Forms.Label();
            this.availableJobsCmb = new System.Windows.Forms.ComboBox();
            this.startOptimizationBtn = new System.Windows.Forms.Button();
            this.sqlConnectionGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionString
            // 
            this.connectionString.AutoSize = true;
            this.connectionString.Location = new System.Drawing.Point(18, 29);
            this.connectionString.Name = "connectionString";
            this.connectionString.Size = new System.Drawing.Size(88, 13);
            this.connectionString.TabIndex = 25;
            this.connectionString.Text = "ConnectionString";
            // 
            // connectionStringTxt
            // 
            this.connectionStringTxt.Location = new System.Drawing.Point(21, 45);
            this.connectionStringTxt.Name = "connectionStringTxt";
            this.connectionStringTxt.Size = new System.Drawing.Size(126, 20);
            this.connectionStringTxt.TabIndex = 26;
            // 
            // connectionsBtn
            // 
            this.connectionsBtn.Location = new System.Drawing.Point(169, 43);
            this.connectionsBtn.Name = "connectionsBtn";
            this.connectionsBtn.Size = new System.Drawing.Size(82, 23);
            this.connectionsBtn.TabIndex = 24;
            this.connectionsBtn.Text = "Connections";
            this.connectionsBtn.UseVisualStyleBackColor = true;
            this.connectionsBtn.Click += new System.EventHandler(this.connectionsBtn_Click);
            // 
            // sqlConnectionGrp
            // 
            this.sqlConnectionGrp.Controls.Add(this.connectionString);
            this.sqlConnectionGrp.Controls.Add(this.connectionsBtn);
            this.sqlConnectionGrp.Controls.Add(this.connectionStringTxt);
            this.sqlConnectionGrp.Location = new System.Drawing.Point(12, 12);
            this.sqlConnectionGrp.Name = "sqlConnectionGrp";
            this.sqlConnectionGrp.Size = new System.Drawing.Size(270, 89);
            this.sqlConnectionGrp.TabIndex = 27;
            this.sqlConnectionGrp.TabStop = false;
            this.sqlConnectionGrp.Text = "SQL Connection";
            // 
            // AllJobsRadio
            // 
            this.AllJobsRadio.AutoSize = true;
            this.AllJobsRadio.Checked = true;
            this.AllJobsRadio.Location = new System.Drawing.Point(33, 116);
            this.AllJobsRadio.Name = "AllJobsRadio";
            this.AllJobsRadio.Size = new System.Drawing.Size(61, 17);
            this.AllJobsRadio.TabIndex = 28;
            this.AllJobsRadio.TabStop = true;
            this.AllJobsRadio.Text = "All Jobs";
            this.AllJobsRadio.UseVisualStyleBackColor = true;
            this.AllJobsRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // ByJobsRadio
            // 
            this.ByJobsRadio.AutoSize = true;
            this.ByJobsRadio.Location = new System.Drawing.Point(129, 116);
            this.ByJobsRadio.Name = "ByJobsRadio";
            this.ByJobsRadio.Size = new System.Drawing.Size(62, 17);
            this.ByJobsRadio.TabIndex = 29;
            this.ByJobsRadio.Text = "By Jobs";
            this.ByJobsRadio.UseVisualStyleBackColor = true;
            this.ByJobsRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // availableJobs
            // 
            this.availableJobs.AutoSize = true;
            this.availableJobs.Enabled = false;
            this.availableJobs.Location = new System.Drawing.Point(126, 148);
            this.availableJobs.Name = "availableJobs";
            this.availableJobs.Size = new System.Drawing.Size(75, 13);
            this.availableJobs.TabIndex = 30;
            this.availableJobs.Text = "Available Jobs";
            // 
            // availableJobsCmb
            // 
            this.availableJobsCmb.Enabled = false;
            this.availableJobsCmb.FormattingEnabled = true;
            this.availableJobsCmb.Location = new System.Drawing.Point(129, 164);
            this.availableJobsCmb.Name = "availableJobsCmb";
            this.availableJobsCmb.Size = new System.Drawing.Size(134, 21);
            this.availableJobsCmb.TabIndex = 31;
            // 
            // startOptimizationBtn
            // 
            this.startOptimizationBtn.Location = new System.Drawing.Point(33, 203);
            this.startOptimizationBtn.Name = "startOptimizationBtn";
            this.startOptimizationBtn.Size = new System.Drawing.Size(230, 40);
            this.startOptimizationBtn.TabIndex = 32;
            this.startOptimizationBtn.Text = "Start Optimization";
            this.startOptimizationBtn.UseVisualStyleBackColor = true;
            this.startOptimizationBtn.Click += new System.EventHandler(this.startOptimizationBtn_Click);
            // 
            // DBOptimizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 265);
            this.Controls.Add(this.startOptimizationBtn);
            this.Controls.Add(this.availableJobsCmb);
            this.Controls.Add(this.availableJobs);
            this.Controls.Add(this.ByJobsRadio);
            this.Controls.Add(this.AllJobsRadio);
            this.Controls.Add(this.sqlConnectionGrp);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBOptimizer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DB Optimizer";
            this.Load += new System.EventHandler(this.DBOptimizer_Load);
            this.sqlConnectionGrp.ResumeLayout(false);
            this.sqlConnectionGrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label connectionString;
        private System.Windows.Forms.TextBox connectionStringTxt;
        private System.Windows.Forms.Button connectionsBtn;
        private System.Windows.Forms.GroupBox sqlConnectionGrp;
        private System.Windows.Forms.RadioButton AllJobsRadio;
        private System.Windows.Forms.RadioButton ByJobsRadio;
        private System.Windows.Forms.Label availableJobs;
        private System.Windows.Forms.ComboBox availableJobsCmb;
        private System.Windows.Forms.Button startOptimizationBtn;
    }
}