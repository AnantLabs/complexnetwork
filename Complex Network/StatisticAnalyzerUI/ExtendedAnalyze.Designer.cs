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
            this.SuspendLayout();
            // 
            // trajectoryAnalyze
            // 
            this.trajectoryAnalyze.Location = new System.Drawing.Point(27, 27);
            this.trajectoryAnalyze.Name = "trajectoryAnalyze";
            this.trajectoryAnalyze.Size = new System.Drawing.Size(138, 45);
            this.trajectoryAnalyze.TabIndex = 0;
            this.trajectoryAnalyze.Text = "Trajectory Analyze";
            this.trajectoryAnalyze.UseVisualStyleBackColor = true;
            this.trajectoryAnalyze.Click += new System.EventHandler(this.trajectoryAnalyze_Click);
            // 
            // ExtendedAnalyze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.trajectoryAnalyze);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendedAnalyze";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extended Analyze";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button trajectoryAnalyze;
    }
}