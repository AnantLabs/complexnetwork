namespace StatisticAnalyzerUI
{
    partial class ExtendedAnalyze
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
            this.trajectoryAnalyze = new System.Windows.Forms.Button();
            this.parameterK = new System.Windows.Forms.Label();
            this.parameterKTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // trajectoryAnalyze
            // 
            this.trajectoryAnalyze.Location = new System.Drawing.Point(27, 83);
            this.trajectoryAnalyze.Name = "trajectoryAnalyze";
            this.trajectoryAnalyze.Size = new System.Drawing.Size(225, 45);
            this.trajectoryAnalyze.TabIndex = 0;
            this.trajectoryAnalyze.Text = "Trajectory Analyze";
            this.trajectoryAnalyze.UseVisualStyleBackColor = true;
            this.trajectoryAnalyze.Click += new System.EventHandler(this.trajectoryAnalyze_Click);
            // 
            // parameterK
            // 
            this.parameterK.AutoSize = true;
            this.parameterK.Location = new System.Drawing.Point(24, 23);
            this.parameterK.Name = "parameterK";
            this.parameterK.Size = new System.Drawing.Size(13, 13);
            this.parameterK.TabIndex = 1;
            this.parameterK.Text = "k";
            // 
            // parameterKTxt
            // 
            this.parameterKTxt.Location = new System.Drawing.Point(27, 39);
            this.parameterKTxt.Name = "parameterKTxt";
            this.parameterKTxt.Size = new System.Drawing.Size(100, 20);
            this.parameterKTxt.TabIndex = 2;
            this.parameterKTxt.Text = "0";
            this.parameterKTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtendedAnalyze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 173);
            this.Controls.Add(this.parameterKTxt);
            this.Controls.Add(this.parameterK);
            this.Controls.Add(this.trajectoryAnalyze);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendedAnalyze";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extended Analyze";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button trajectoryAnalyze;
        private System.Windows.Forms.Label parameterK;
        private System.Windows.Forms.TextBox parameterKTxt;
    }
}