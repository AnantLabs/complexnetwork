namespace RandomGraphLauncher
{
    partial class MatrixMixerWindow
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
            this.filePath = new System.Windows.Forms.Label();
            this.filePathTxt = new System.Windows.Forms.TextBox();
            this.browse = new System.Windows.Forms.Button();
            this.percentTxt = new System.Windows.Forms.TextBox();
            this.mixMatrix = new System.Windows.Forms.Button();
            this.percent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // filePath
            // 
            this.filePath.AutoSize = true;
            this.filePath.Location = new System.Drawing.Point(12, 9);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(48, 13);
            this.filePath.TabIndex = 0;
            this.filePath.Text = "File Path";
            // 
            // filePathTxt
            // 
            this.filePathTxt.Location = new System.Drawing.Point(15, 25);
            this.filePathTxt.Name = "filePathTxt";
            this.filePathTxt.Size = new System.Drawing.Size(125, 20);
            this.filePathTxt.TabIndex = 1;
            this.filePathTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(166, 23);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(95, 23);
            this.browse.TabIndex = 2;
            this.browse.Text = "Browse";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // percentTxt
            // 
            this.percentTxt.Location = new System.Drawing.Point(16, 81);
            this.percentTxt.Name = "percentTxt";
            this.percentTxt.Size = new System.Drawing.Size(125, 20);
            this.percentTxt.TabIndex = 8;
            this.percentTxt.Text = "0";
            this.percentTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mixMatrix
            // 
            this.mixMatrix.Location = new System.Drawing.Point(168, 79);
            this.mixMatrix.Name = "mixMatrix";
            this.mixMatrix.Size = new System.Drawing.Size(95, 23);
            this.mixMatrix.TabIndex = 9;
            this.mixMatrix.Text = "Mix Matrix";
            this.mixMatrix.UseVisualStyleBackColor = true;
            this.mixMatrix.Click += new System.EventHandler(this.MixMatrix_Click);
            // 
            // percent
            // 
            this.percent.AutoSize = true;
            this.percent.Location = new System.Drawing.Point(15, 65);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(61, 13);
            this.percent.TabIndex = 10;
            this.percent.Text = "Percent (%)";
            // 
            // MatrixMixerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 132);
            this.Controls.Add(this.percent);
            this.Controls.Add(this.mixMatrix);
            this.Controls.Add(this.percentTxt);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.filePathTxt);
            this.Controls.Add(this.filePath);
            this.Name = "MatrixMixerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Matrix Mixer";
            this.Load += new System.EventHandler(this.MatrixMixerWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label filePath;
        private System.Windows.Forms.TextBox filePathTxt;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.TextBox percentTxt;
        private System.Windows.Forms.Button mixMatrix;
        private System.Windows.Forms.Label percent;
    }
}