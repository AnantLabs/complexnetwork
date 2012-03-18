namespace StatisticAnalyzerUI
{
    partial class Information
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
            this.InformationTxt = new System.Windows.Forms.RichTextBox();
            this.Save = new System.Windows.Forms.Button();
            this.SaveInformationDlg = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // InformationTxt
            // 
            this.InformationTxt.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.InformationTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformationTxt.Location = new System.Drawing.Point(29, 25);
            this.InformationTxt.Name = "InformationTxt";
            this.InformationTxt.Size = new System.Drawing.Size(644, 172);
            this.InformationTxt.TabIndex = 0;
            this.InformationTxt.Text = "";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(703, 170);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 27);
            this.Save.TabIndex = 1;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // SaveInformationDlg
            // 
            this.SaveInformationDlg.DefaultExt = "txt";
            this.SaveInformationDlg.Filter = "Text files (*.txt)|*.txt";
            this.SaveInformationDlg.Title = "Save Information";
            // 
            // Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 223);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.InformationTxt);
            this.MaximizeBox = false;
            this.Name = "Information";
            this.Text = "Information";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox InformationTxt;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.SaveFileDialog SaveInformationDlg;
    }
}