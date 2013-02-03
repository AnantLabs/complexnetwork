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
            this.save = new System.Windows.Forms.Button();
            this.graphicPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(728, 411);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 1;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // graphicPanel
            // 
            this.graphicPanel.Location = new System.Drawing.Point(12, 12);
            this.graphicPanel.Name = "graphicPanel";
            this.graphicPanel.Size = new System.Drawing.Size(710, 422);
            this.graphicPanel.TabIndex = 2;
            // 
            // ExtendedGraphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 446);
            this.Controls.Add(this.graphicPanel);
            this.Controls.Add(this.save);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendedGraphic";
            this.Text = "Extended Graphic";
            this.Load += new System.EventHandler(this.ExtendedGraphic_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Panel graphicPanel;

    }
}