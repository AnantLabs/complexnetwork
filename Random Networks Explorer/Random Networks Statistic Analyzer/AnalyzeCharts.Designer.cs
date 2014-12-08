namespace Random_Networks_Statistic_Analyzer
{
    partial class AnalyzeCharts
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
            this.chartTabs = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // chartTabs
            // 
            this.chartTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTabs.Location = new System.Drawing.Point(0, 0);
            this.chartTabs.Name = "chartTabs";
            this.chartTabs.SelectedIndex = 0;
            this.chartTabs.Size = new System.Drawing.Size(984, 722);
            this.chartTabs.TabIndex = 0;
            // 
            // AnalyzeCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 722);
            this.Controls.Add(this.chartTabs);
            this.Name = "AnalyzeCharts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AnalyzeCharts";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AnalyzeCharts_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl chartTabs;
    }
}