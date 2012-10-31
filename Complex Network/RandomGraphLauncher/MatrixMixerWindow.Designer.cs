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
            this.firstIndex = new System.Windows.Forms.Label();
            this.firstIndexTxt = new System.Windows.Forms.TextBox();
            this.secondIndex = new System.Windows.Forms.Label();
            this.secondIndexTxt = new System.Windows.Forms.TextBox();
            this.mix = new System.Windows.Forms.Button();
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
            this.browse.Size = new System.Drawing.Size(96, 23);
            this.browse.TabIndex = 2;
            this.browse.Text = "Browse";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // firstIndex
            // 
            this.firstIndex.AutoSize = true;
            this.firstIndex.Location = new System.Drawing.Point(12, 67);
            this.firstIndex.Name = "firstIndex";
            this.firstIndex.Size = new System.Drawing.Size(55, 13);
            this.firstIndex.TabIndex = 3;
            this.firstIndex.Text = "First Index";
            // 
            // firstIndexTxt
            // 
            this.firstIndexTxt.Location = new System.Drawing.Point(15, 83);
            this.firstIndexTxt.Name = "firstIndexTxt";
            this.firstIndexTxt.Size = new System.Drawing.Size(125, 20);
            this.firstIndexTxt.TabIndex = 4;
            this.firstIndexTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // secondIndex
            // 
            this.secondIndex.AutoSize = true;
            this.secondIndex.Location = new System.Drawing.Point(12, 127);
            this.secondIndex.Name = "secondIndex";
            this.secondIndex.Size = new System.Drawing.Size(73, 13);
            this.secondIndex.TabIndex = 5;
            this.secondIndex.Text = "Second Index";
            // 
            // secondIndexTxt
            // 
            this.secondIndexTxt.Location = new System.Drawing.Point(15, 143);
            this.secondIndexTxt.Name = "secondIndexTxt";
            this.secondIndexTxt.Size = new System.Drawing.Size(125, 20);
            this.secondIndexTxt.TabIndex = 6;
            this.secondIndexTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mix
            // 
            this.mix.Location = new System.Drawing.Point(166, 140);
            this.mix.Name = "mix";
            this.mix.Size = new System.Drawing.Size(96, 23);
            this.mix.TabIndex = 7;
            this.mix.Text = "Mix";
            this.mix.UseVisualStyleBackColor = true;
            this.mix.Click += new System.EventHandler(this.mix_Click);
            // 
            // MatrixMixerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 202);
            this.Controls.Add(this.mix);
            this.Controls.Add(this.secondIndexTxt);
            this.Controls.Add(this.secondIndex);
            this.Controls.Add(this.firstIndexTxt);
            this.Controls.Add(this.firstIndex);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.filePathTxt);
            this.Controls.Add(this.filePath);
            this.Name = "MatrixMixerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Matrix Mixer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label filePath;
        private System.Windows.Forms.TextBox filePathTxt;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.Label firstIndex;
        private System.Windows.Forms.TextBox firstIndexTxt;
        private System.Windows.Forms.Label secondIndex;
        private System.Windows.Forms.TextBox secondIndexTxt;
        private System.Windows.Forms.Button mix;
    }
}