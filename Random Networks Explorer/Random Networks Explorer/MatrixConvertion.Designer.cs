namespace RandomNetworksExplorer
{
    partial class MatrixConvertion
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
            this.inputFileName = new System.Windows.Forms.Label();
            this.inputFileNameTxt = new System.Windows.Forms.TextBox();
            this.inputBrowse = new System.Windows.Forms.Button();
            this.outputBrowse = new System.Windows.Forms.Button();
            this.outputFileNameTxt = new System.Windows.Forms.TextBox();
            this.outputFileName = new System.Windows.Forms.Label();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.convert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputFileName
            // 
            this.inputFileName.AutoSize = true;
            this.inputFileName.Location = new System.Drawing.Point(12, 9);
            this.inputFileName.Name = "inputFileName";
            this.inputFileName.Size = new System.Drawing.Size(106, 13);
            this.inputFileName.TabIndex = 0;
            this.inputFileName.Text = "Matrix-input file name";
            // 
            // inputFileNameTxt
            // 
            this.inputFileNameTxt.Location = new System.Drawing.Point(12, 25);
            this.inputFileNameTxt.Name = "inputFileNameTxt";
            this.inputFileNameTxt.Size = new System.Drawing.Size(390, 20);
            this.inputFileNameTxt.TabIndex = 1;
            this.inputFileNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // inputBrowse
            // 
            this.inputBrowse.Location = new System.Drawing.Point(425, 23);
            this.inputBrowse.Name = "inputBrowse";
            this.inputBrowse.Size = new System.Drawing.Size(75, 23);
            this.inputBrowse.TabIndex = 2;
            this.inputBrowse.Text = "Browse...";
            this.inputBrowse.UseVisualStyleBackColor = true;
            this.inputBrowse.Click += new System.EventHandler(this.inputBrowse_Click);
            // 
            // outputBrowse
            // 
            this.outputBrowse.Location = new System.Drawing.Point(424, 73);
            this.outputBrowse.Name = "outputBrowse";
            this.outputBrowse.Size = new System.Drawing.Size(75, 23);
            this.outputBrowse.TabIndex = 5;
            this.outputBrowse.Text = "Browse...";
            this.outputBrowse.UseVisualStyleBackColor = true;
            this.outputBrowse.Click += new System.EventHandler(this.outputBrowse_Click);
            // 
            // outputFileNameTxt
            // 
            this.outputFileNameTxt.Location = new System.Drawing.Point(11, 75);
            this.outputFileNameTxt.Name = "outputFileNameTxt";
            this.outputFileNameTxt.Size = new System.Drawing.Size(390, 20);
            this.outputFileNameTxt.TabIndex = 4;
            this.outputFileNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // outputFileName
            // 
            this.outputFileName.AutoSize = true;
            this.outputFileName.Location = new System.Drawing.Point(11, 59);
            this.outputFileName.Name = "outputFileName";
            this.outputFileName.Size = new System.Drawing.Size(125, 13);
            this.outputFileName.TabIndex = 3;
            this.outputFileName.Text = "Degrees-output file name";
            // 
            // openFileDlg
            // 
            this.openFileDlg.Filter = "\"txt files (*.txt)|*.txt|All files (*.*)|*.*\"";
            // 
            // saveFileDlg
            // 
            this.saveFileDlg.Filter = "\"txt files (*.txt)|*.txt|All files (*.*)|*.*\"";
            // 
            // convert
            // 
            this.convert.Location = new System.Drawing.Point(425, 112);
            this.convert.Name = "convert";
            this.convert.Size = new System.Drawing.Size(75, 23);
            this.convert.TabIndex = 6;
            this.convert.Text = "Convert";
            this.convert.UseVisualStyleBackColor = true;
            this.convert.Click += new System.EventHandler(this.convert_Click);
            // 
            // MatrixConvertion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 147);
            this.Controls.Add(this.convert);
            this.Controls.Add(this.outputBrowse);
            this.Controls.Add(this.outputFileNameTxt);
            this.Controls.Add(this.outputFileName);
            this.Controls.Add(this.inputBrowse);
            this.Controls.Add(this.inputFileNameTxt);
            this.Controls.Add(this.inputFileName);
            this.Name = "MatrixConvertion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Matrix Convertion Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inputFileName;
        private System.Windows.Forms.TextBox inputFileNameTxt;
        private System.Windows.Forms.Button inputBrowse;
        private System.Windows.Forms.Button outputBrowse;
        private System.Windows.Forms.TextBox outputFileNameTxt;
        private System.Windows.Forms.Label outputFileName;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDlg;
        private System.Windows.Forms.Button convert;
    }
}