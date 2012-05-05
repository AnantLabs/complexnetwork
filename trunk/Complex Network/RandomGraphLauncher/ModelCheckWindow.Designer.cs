namespace RandomGraphLauncher
{
    partial class ModelCheckWindow
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
            this.modelName = new System.Windows.Forms.Label();
            this.modelNameCmb = new System.Windows.Forms.ComboBox();
            this.degrees = new System.Windows.Forms.Label();
            this.degreesTxt = new System.Windows.Forms.TextBox();
            this.checkBtn = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.Label();
            this.resultTxt = new System.Windows.Forms.TextBox();
            this.dataGrp = new System.Windows.Forms.GroupBox();
            this.degreesRadio = new System.Windows.Forms.RadioButton();
            this.fromFileRadio = new System.Windows.Forms.RadioButton();
            this.checkGrp = new System.Windows.Forms.GroupBox();
            this.exactCheckRadio = new System.Windows.Forms.RadioButton();
            this.notExactCheckRadio = new System.Windows.Forms.RadioButton();
            this.filePathTxt = new System.Windows.Forms.TextBox();
            this.filePath = new System.Windows.Forms.Label();
            this.browse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGrp.SuspendLayout();
            this.checkGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // modelName
            // 
            this.modelName.AutoSize = true;
            this.modelName.Location = new System.Drawing.Point(12, 9);
            this.modelName.Name = "modelName";
            this.modelName.Size = new System.Drawing.Size(67, 13);
            this.modelName.TabIndex = 0;
            this.modelName.Text = "Model Name";
            // 
            // modelNameCmb
            // 
            this.modelNameCmb.FormattingEnabled = true;
            this.modelNameCmb.Location = new System.Drawing.Point(12, 25);
            this.modelNameCmb.Name = "modelNameCmb";
            this.modelNameCmb.Size = new System.Drawing.Size(199, 21);
            this.modelNameCmb.TabIndex = 1;
            this.modelNameCmb.SelectedIndexChanged += new System.EventHandler(this.modelNameCmb_SelectedIndexChanged);
            // 
            // degrees
            // 
            this.degrees.AutoSize = true;
            this.degrees.Location = new System.Drawing.Point(99, 30);
            this.degrees.Name = "degrees";
            this.degrees.Size = new System.Drawing.Size(134, 13);
            this.degrees.TabIndex = 2;
            this.degrees.Text = "Degrees (saparated with , )";
            // 
            // degreesTxt
            // 
            this.degreesTxt.Location = new System.Drawing.Point(102, 46);
            this.degreesTxt.Multiline = true;
            this.degreesTxt.Name = "degreesTxt";
            this.degreesTxt.Size = new System.Drawing.Size(431, 60);
            this.degreesTxt.TabIndex = 3;
            // 
            // checkBtn
            // 
            this.checkBtn.Location = new System.Drawing.Point(6, 54);
            this.checkBtn.Name = "checkBtn";
            this.checkBtn.Size = new System.Drawing.Size(196, 30);
            this.checkBtn.TabIndex = 4;
            this.checkBtn.Text = "Check";
            this.checkBtn.UseVisualStyleBackColor = true;
            this.checkBtn.Click += new System.EventHandler(this.checkBtn_Click);
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Location = new System.Drawing.Point(232, 57);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(37, 13);
            this.result.TabIndex = 5;
            this.result.Text = "Result";
            // 
            // resultTxt
            // 
            this.resultTxt.Location = new System.Drawing.Point(275, 54);
            this.resultTxt.Name = "resultTxt";
            this.resultTxt.ReadOnly = true;
            this.resultTxt.Size = new System.Drawing.Size(258, 20);
            this.resultTxt.TabIndex = 6;
            this.resultTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataGrp
            // 
            this.dataGrp.Controls.Add(this.browse);
            this.dataGrp.Controls.Add(this.filePath);
            this.dataGrp.Controls.Add(this.filePathTxt);
            this.dataGrp.Controls.Add(this.fromFileRadio);
            this.dataGrp.Controls.Add(this.degreesRadio);
            this.dataGrp.Controls.Add(this.degrees);
            this.dataGrp.Controls.Add(this.degreesTxt);
            this.dataGrp.Location = new System.Drawing.Point(12, 52);
            this.dataGrp.Name = "dataGrp";
            this.dataGrp.Size = new System.Drawing.Size(552, 156);
            this.dataGrp.TabIndex = 7;
            this.dataGrp.TabStop = false;
            this.dataGrp.Text = "Data";
            // 
            // degreesRadio
            // 
            this.degreesRadio.AutoSize = true;
            this.degreesRadio.Location = new System.Drawing.Point(6, 28);
            this.degreesRadio.Name = "degreesRadio";
            this.degreesRadio.Size = new System.Drawing.Size(65, 17);
            this.degreesRadio.TabIndex = 0;
            this.degreesRadio.TabStop = true;
            this.degreesRadio.Text = "Degrees";
            this.degreesRadio.UseVisualStyleBackColor = true;
            this.degreesRadio.CheckedChanged += new System.EventHandler(this.degreesRadio_CheckedChanged);
            // 
            // fromFileRadio
            // 
            this.fromFileRadio.AutoSize = true;
            this.fromFileRadio.Location = new System.Drawing.Point(6, 116);
            this.fromFileRadio.Name = "fromFileRadio";
            this.fromFileRadio.Size = new System.Drawing.Size(67, 17);
            this.fromFileRadio.TabIndex = 1;
            this.fromFileRadio.TabStop = true;
            this.fromFileRadio.Text = "From File";
            this.fromFileRadio.UseVisualStyleBackColor = true;
            this.fromFileRadio.CheckedChanged += new System.EventHandler(this.fromFileRadio_CheckedChanged);
            // 
            // checkGrp
            // 
            this.checkGrp.Controls.Add(this.notExactCheckRadio);
            this.checkGrp.Controls.Add(this.exactCheckRadio);
            this.checkGrp.Controls.Add(this.resultTxt);
            this.checkGrp.Controls.Add(this.checkBtn);
            this.checkGrp.Controls.Add(this.result);
            this.checkGrp.Location = new System.Drawing.Point(12, 224);
            this.checkGrp.Name = "checkGrp";
            this.checkGrp.Size = new System.Drawing.Size(552, 115);
            this.checkGrp.TabIndex = 8;
            this.checkGrp.TabStop = false;
            this.checkGrp.Text = "Check";
            // 
            // exactCheckRadio
            // 
            this.exactCheckRadio.AutoSize = true;
            this.exactCheckRadio.Location = new System.Drawing.Point(6, 19);
            this.exactCheckRadio.Name = "exactCheckRadio";
            this.exactCheckRadio.Size = new System.Drawing.Size(86, 17);
            this.exactCheckRadio.TabIndex = 0;
            this.exactCheckRadio.TabStop = true;
            this.exactCheckRadio.Text = "Exact Check";
            this.exactCheckRadio.UseVisualStyleBackColor = true;
            // 
            // notExactCheckRadio
            // 
            this.notExactCheckRadio.AutoSize = true;
            this.notExactCheckRadio.Location = new System.Drawing.Point(102, 19);
            this.notExactCheckRadio.Name = "notExactCheckRadio";
            this.notExactCheckRadio.Size = new System.Drawing.Size(106, 17);
            this.notExactCheckRadio.TabIndex = 1;
            this.notExactCheckRadio.TabStop = true;
            this.notExactCheckRadio.Text = "Not Exact Check";
            this.notExactCheckRadio.UseVisualStyleBackColor = true;
            // 
            // filePathTxt
            // 
            this.filePathTxt.Location = new System.Drawing.Point(153, 113);
            this.filePathTxt.Name = "filePathTxt";
            this.filePathTxt.Size = new System.Drawing.Size(217, 20);
            this.filePathTxt.TabIndex = 4;
            // 
            // filePath
            // 
            this.filePath.AutoSize = true;
            this.filePath.Location = new System.Drawing.Point(99, 118);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(48, 13);
            this.filePath.TabIndex = 5;
            this.filePath.Text = "File Path";
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(391, 112);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(142, 23);
            this.browse.TabIndex = 6;
            this.browse.Text = "Browse";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ModelCheckWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 361);
            this.Controls.Add(this.checkGrp);
            this.Controls.Add(this.dataGrp);
            this.Controls.Add(this.modelNameCmb);
            this.Controls.Add(this.modelName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelCheckWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model Check Window";
            this.Load += new System.EventHandler(this.ModelCheckWindow_Load);
            this.dataGrp.ResumeLayout(false);
            this.dataGrp.PerformLayout();
            this.checkGrp.ResumeLayout(false);
            this.checkGrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label modelName;
        private System.Windows.Forms.ComboBox modelNameCmb;
        private System.Windows.Forms.Label degrees;
        private System.Windows.Forms.TextBox degreesTxt;
        private System.Windows.Forms.Button checkBtn;
        private System.Windows.Forms.Label result;
        private System.Windows.Forms.TextBox resultTxt;
        private System.Windows.Forms.GroupBox dataGrp;
        private System.Windows.Forms.RadioButton fromFileRadio;
        private System.Windows.Forms.RadioButton degreesRadio;
        private System.Windows.Forms.GroupBox checkGrp;
        private System.Windows.Forms.RadioButton notExactCheckRadio;
        private System.Windows.Forms.RadioButton exactCheckRadio;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.Label filePath;
        private System.Windows.Forms.TextBox filePathTxt;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}