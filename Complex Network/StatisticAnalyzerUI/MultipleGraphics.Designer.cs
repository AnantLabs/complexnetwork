namespace Percolations
{
    partial class MultipleGraphics
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
            this.graphicsTab = new System.Windows.Forms.TabControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveJPG = new System.Windows.Forms.Button();
            this.saveOpj = new System.Windows.Forms.Button();
            this.saveTxt = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphicsTab
            // 
            this.graphicsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicsTab.Location = new System.Drawing.Point(0, 0);
            this.graphicsTab.Name = "graphicsTab";
            this.graphicsTab.SelectedIndex = 0;
            this.graphicsTab.Size = new System.Drawing.Size(1148, 688);
            this.graphicsTab.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.graphicsTab);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1148, 688);
            this.panel1.TabIndex = 1;
            // 
            // saveJPG
            // 
            this.saveJPG.Location = new System.Drawing.Point(1166, 12);
            this.saveJPG.Name = "saveJPG";
            this.saveJPG.Size = new System.Drawing.Size(116, 46);
            this.saveJPG.TabIndex = 2;
            this.saveJPG.Text = "Save .jpg";
            this.saveJPG.UseVisualStyleBackColor = true;
            this.saveJPG.Click += new System.EventHandler(this.save_Click);
            // 
            // saveOpj
            // 
            this.saveOpj.Location = new System.Drawing.Point(1166, 64);
            this.saveOpj.Name = "saveOpj";
            this.saveOpj.Size = new System.Drawing.Size(116, 46);
            this.saveOpj.TabIndex = 3;
            this.saveOpj.Text = "Save .opj";
            this.saveOpj.UseVisualStyleBackColor = true;
            this.saveOpj.Click += new System.EventHandler(this.saveOpj_Click);
            // 
            // saveTxt
            // 
            this.saveTxt.Location = new System.Drawing.Point(1166, 116);
            this.saveTxt.Name = "saveTxt";
            this.saveTxt.Size = new System.Drawing.Size(116, 46);
            this.saveTxt.TabIndex = 4;
            this.saveTxt.Text = "Save .txt";
            this.saveTxt.UseVisualStyleBackColor = true;
            this.saveTxt.Click += new System.EventHandler(this.saveTxt_Click);
            // 
            // MultipleGraphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 712);
            this.Controls.Add(this.saveTxt);
            this.Controls.Add(this.saveOpj);
            this.Controls.Add(this.saveJPG);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "MultipleGraphics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl graphicsTab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button saveJPG;
        private System.Windows.Forms.Button saveOpj;
        private System.Windows.Forms.Button saveTxt;
    }
}