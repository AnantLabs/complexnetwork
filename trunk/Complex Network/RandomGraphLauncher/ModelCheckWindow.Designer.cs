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
            // 
            // degrees
            // 
            this.degrees.AutoSize = true;
            this.degrees.Location = new System.Drawing.Point(12, 66);
            this.degrees.Name = "degrees";
            this.degrees.Size = new System.Drawing.Size(134, 13);
            this.degrees.TabIndex = 2;
            this.degrees.Text = "Degrees (saparated with , )";
            // 
            // degreesTxt
            // 
            this.degreesTxt.Location = new System.Drawing.Point(15, 82);
            this.degreesTxt.Multiline = true;
            this.degreesTxt.Name = "degreesTxt";
            this.degreesTxt.Size = new System.Drawing.Size(196, 63);
            this.degreesTxt.TabIndex = 3;
            // 
            // checkBtn
            // 
            this.checkBtn.Location = new System.Drawing.Point(15, 169);
            this.checkBtn.Name = "checkBtn";
            this.checkBtn.Size = new System.Drawing.Size(196, 30);
            this.checkBtn.TabIndex = 4;
            this.checkBtn.Text = "Check";
            this.checkBtn.UseVisualStyleBackColor = true;
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Location = new System.Drawing.Point(12, 221);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(37, 13);
            this.result.TabIndex = 5;
            this.result.Text = "Result";
            // 
            // resultTxt
            // 
            this.resultTxt.Location = new System.Drawing.Point(53, 218);
            this.resultTxt.Name = "resultTxt";
            this.resultTxt.ReadOnly = true;
            this.resultTxt.Size = new System.Drawing.Size(158, 20);
            this.resultTxt.TabIndex = 6;
            this.resultTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ModelCheckWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 257);
            this.Controls.Add(this.resultTxt);
            this.Controls.Add(this.result);
            this.Controls.Add(this.checkBtn);
            this.Controls.Add(this.degreesTxt);
            this.Controls.Add(this.degrees);
            this.Controls.Add(this.modelNameCmb);
            this.Controls.Add(this.modelName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelCheckWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model Check Window";
            this.Load += new System.EventHandler(this.ModelCheckWindow_Load);
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
    }
}