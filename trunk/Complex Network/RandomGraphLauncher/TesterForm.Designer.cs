namespace RandomGraphLauncher
{
    partial class TesterForm
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
            this.inputMatrix = new System.Windows.Forms.Button();
            this.goldenOut = new System.Windows.Forms.Button();
            this.inputMatrixPath = new System.Windows.Forms.TextBox();
            this.goldenOutPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.comboBox_ModelType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputMatrix
            // 
            this.inputMatrix.Location = new System.Drawing.Point(309, 26);
            this.inputMatrix.Name = "inputMatrix";
            this.inputMatrix.Size = new System.Drawing.Size(75, 23);
            this.inputMatrix.TabIndex = 0;
            this.inputMatrix.Text = "Browse";
            this.inputMatrix.UseVisualStyleBackColor = true;
            this.inputMatrix.Click += new System.EventHandler(this.inputMatrix_Click);
            // 
            // goldenOut
            // 
            this.goldenOut.Location = new System.Drawing.Point(309, 76);
            this.goldenOut.Name = "goldenOut";
            this.goldenOut.Size = new System.Drawing.Size(75, 23);
            this.goldenOut.TabIndex = 1;
            this.goldenOut.Text = "Browse";
            this.goldenOut.UseVisualStyleBackColor = true;
            this.goldenOut.Click += new System.EventHandler(this.goldenOut_Click);
            // 
            // inputMatrixPath
            // 
            this.inputMatrixPath.Location = new System.Drawing.Point(118, 28);
            this.inputMatrixPath.Name = "inputMatrixPath";
            this.inputMatrixPath.Size = new System.Drawing.Size(185, 20);
            this.inputMatrixPath.TabIndex = 2;
            // 
            // goldenOutPath
            // 
            this.goldenOutPath.Location = new System.Drawing.Point(118, 79);
            this.goldenOutPath.Name = "goldenOutPath";
            this.goldenOutPath.Size = new System.Drawing.Size(185, 20);
            this.goldenOutPath.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Input matrix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Golden out";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Start Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // comboBox_ModelType
            // 
            this.comboBox_ModelType.FormattingEnabled = true;
            this.comboBox_ModelType.Location = new System.Drawing.Point(118, 137);
            this.comboBox_ModelType.Name = "comboBox_ModelType";
            this.comboBox_ModelType.Size = new System.Drawing.Size(185, 21);
            this.comboBox_ModelType.TabIndex = 7;
            this.comboBox_ModelType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ModelType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Motel to test";
            // 
            // TesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(421, 492);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_ModelType);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.goldenOutPath);
            this.Controls.Add(this.inputMatrixPath);
            this.Controls.Add(this.goldenOut);
            this.Controls.Add(this.inputMatrix);
            this.Name = "TesterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button inputMatrix;
        private System.Windows.Forms.Button goldenOut;
        private System.Windows.Forms.TextBox inputMatrixPath;
        private System.Windows.Forms.TextBox goldenOutPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox comboBox_ModelType;
        private System.Windows.Forms.Label label3;
    }
}