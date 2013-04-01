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
            this.components = new System.ComponentModel.Container();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.resultsTab = new System.Windows.Forms.TabControl();
            this.avgTab = new System.Windows.Forms.TabPage();
            this.sigmaTab = new System.Windows.Forms.TabPage();
            this.CommonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.resultsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(9, 15);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(135, 31);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSlateGray;
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 511);
            this.panel1.TabIndex = 10;
            // 
            // resultsTab
            // 
            this.resultsTab.Controls.Add(this.avgTab);
            this.resultsTab.Controls.Add(this.sigmaTab);
            this.resultsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsTab.Location = new System.Drawing.Point(0, 0);
            this.resultsTab.Name = "resultsTab";
            this.resultsTab.SelectedIndex = 0;
            this.resultsTab.Size = new System.Drawing.Size(693, 511);
            this.resultsTab.TabIndex = 11;
            // 
            // avgTab
            // 
            this.avgTab.Location = new System.Drawing.Point(4, 22);
            this.avgTab.Name = "avgTab";
            this.avgTab.Size = new System.Drawing.Size(685, 485);
            this.avgTab.TabIndex = 0;
            this.avgTab.Text = "Avg";
            this.avgTab.UseVisualStyleBackColor = true;
            // 
            // sigmaTab
            // 
            this.sigmaTab.Location = new System.Drawing.Point(4, 22);
            this.sigmaTab.Name = "sigmaTab";
            this.sigmaTab.Size = new System.Drawing.Size(685, 485);
            this.sigmaTab.TabIndex = 1;
            this.sigmaTab.Text = "Sigma";
            this.sigmaTab.UseVisualStyleBackColor = true;
            // 
            // CommonToolTip
            // 
            this.CommonToolTip.IsBalloon = true;
            this.CommonToolTip.ShowAlways = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.resultsTab);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(853, 511);
            this.splitContainer1.SplitterDistance = 693;
            this.splitContainer1.TabIndex = 13;
            // 
            // ExtendedGraphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 511);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.Name = "ExtendedGraphic";
            this.Text = "Extended Graphic";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtendedGraphic_FormClosing);
            this.Load += new System.EventHandler(this.ExtendedGraphic_Load);
            this.panel1.ResumeLayout(false);
            this.resultsTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl resultsTab;
        private System.Windows.Forms.TabPage avgTab;
        private System.Windows.Forms.TabPage sigmaTab;
        private System.Windows.Forms.ToolTip CommonToolTip;
        private System.Windows.Forms.SplitContainer splitContainer1;



    }
}