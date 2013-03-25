namespace StatisticAnalyzerUI
{
    partial class ExtendedGraphic
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
            this.resultsTab = new System.Windows.Forms.TabControl();
            this.avgsTab = new System.Windows.Forms.TabPage();
            this.sigmasTab = new System.Windows.Forms.TabPage();
            this.resultsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // resultsTab
            // 
            this.resultsTab.Controls.Add(this.avgsTab);
            this.resultsTab.Controls.Add(this.sigmasTab);
            this.resultsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsTab.Location = new System.Drawing.Point(0, 0);
            this.resultsTab.Name = "resultsTab";
            this.resultsTab.SelectedIndex = 0;
            this.resultsTab.Size = new System.Drawing.Size(853, 511);
            this.resultsTab.TabIndex = 2;
            // 
            // avgsTab
            // 
            this.avgsTab.Location = new System.Drawing.Point(4, 22);
            this.avgsTab.Name = "avgsTab";
            this.avgsTab.Padding = new System.Windows.Forms.Padding(3);
            this.avgsTab.Size = new System.Drawing.Size(845, 485);
            this.avgsTab.TabIndex = 0;
            this.avgsTab.Text = "Avgs";
            this.avgsTab.UseVisualStyleBackColor = true;
            // 
            // sigmasTab
            // 
            this.sigmasTab.Location = new System.Drawing.Point(4, 22);
            this.sigmasTab.Name = "sigmasTab";
            this.sigmasTab.Padding = new System.Windows.Forms.Padding(3);
            this.sigmasTab.Size = new System.Drawing.Size(845, 485);
            this.sigmasTab.TabIndex = 1;
            this.sigmasTab.Text = "Sigmas";
            this.sigmasTab.UseVisualStyleBackColor = true;
            // 
            // ExtendedGraphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 511);
            this.Controls.Add(this.resultsTab);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendedGraphic";
            this.Text = "Extended Graphic";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtendedGraphic_FormClosing);
            this.Load += new System.EventHandler(this.ExtendedGraphic_Load);
            this.resultsTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl resultsTab;
        private System.Windows.Forms.TabPage avgsTab;
        private System.Windows.Forms.TabPage sigmasTab;

    }
}