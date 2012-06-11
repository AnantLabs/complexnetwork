namespace StatisticAnalyzerUI
{
    partial class ValueTable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GenerationParameters = new System.Windows.Forms.Label();
            this.GenerationParametersTxt = new System.Windows.Forms.TextBox();
            this.OptionNameTxt = new System.Windows.Forms.TextBox();
            this.OptionName = new System.Windows.Forms.Label();
            this.Values = new System.Windows.Forms.Label();
            this.ValuesGrd = new System.Windows.Forms.DataGridView();
            this.Print = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.KeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveFileDlg = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ValuesGrd)).BeginInit();
            this.SuspendLayout();
            // 
            // GenerationParameters
            // 
            this.GenerationParameters.AutoSize = true;
            this.GenerationParameters.Location = new System.Drawing.Point(12, 9);
            this.GenerationParameters.Name = "GenerationParameters";
            this.GenerationParameters.Size = new System.Drawing.Size(115, 13);
            this.GenerationParameters.TabIndex = 0;
            this.GenerationParameters.Text = "Generation Parameters";
            // 
            // GenerationParametersTxt
            // 
            this.GenerationParametersTxt.Location = new System.Drawing.Point(133, 6);
            this.GenerationParametersTxt.Name = "GenerationParametersTxt";
            this.GenerationParametersTxt.ReadOnly = true;
            this.GenerationParametersTxt.Size = new System.Drawing.Size(439, 20);
            this.GenerationParametersTxt.TabIndex = 1;
            this.GenerationParametersTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OptionNameTxt
            // 
            this.OptionNameTxt.Location = new System.Drawing.Point(133, 34);
            this.OptionNameTxt.Name = "OptionNameTxt";
            this.OptionNameTxt.ReadOnly = true;
            this.OptionNameTxt.Size = new System.Drawing.Size(439, 20);
            this.OptionNameTxt.TabIndex = 3;
            this.OptionNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OptionName
            // 
            this.OptionName.AutoSize = true;
            this.OptionName.Location = new System.Drawing.Point(12, 37);
            this.OptionName.Name = "OptionName";
            this.OptionName.Size = new System.Drawing.Size(69, 13);
            this.OptionName.TabIndex = 2;
            this.OptionName.Text = "Option Name";
            // 
            // Values
            // 
            this.Values.AutoSize = true;
            this.Values.Location = new System.Drawing.Point(12, 80);
            this.Values.Name = "Values";
            this.Values.Size = new System.Drawing.Size(39, 13);
            this.Values.TabIndex = 4;
            this.Values.Text = "Values";
            // 
            // ValuesGrd
            // 
            this.ValuesGrd.AllowUserToAddRows = false;
            this.ValuesGrd.AllowUserToDeleteRows = false;
            this.ValuesGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ValuesGrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KeyColumn,
            this.ValueColumn});
            this.ValuesGrd.Location = new System.Drawing.Point(15, 96);
            this.ValuesGrd.Name = "ValuesGrd";
            this.ValuesGrd.ReadOnly = true;
            this.ValuesGrd.Size = new System.Drawing.Size(557, 408);
            this.ValuesGrd.TabIndex = 5;
            // 
            // Print
            // 
            this.Print.Location = new System.Drawing.Point(398, 527);
            this.Print.Name = "Print";
            this.Print.Size = new System.Drawing.Size(75, 23);
            this.Print.TabIndex = 6;
            this.Print.Text = "Print";
            this.Print.UseVisualStyleBackColor = true;
            this.Print.Click += new System.EventHandler(this.Print_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(497, 527);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 7;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // KeyColumn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KeyColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.KeyColumn.HeaderText = "Column1";
            this.KeyColumn.Name = "KeyColumn";
            this.KeyColumn.ReadOnly = true;
            this.KeyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KeyColumn.Width = 255;
            // 
            // ValueColumn
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ValueColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.ValueColumn.HeaderText = "Column2";
            this.ValueColumn.Name = "ValueColumn";
            this.ValueColumn.ReadOnly = true;
            this.ValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ValueColumn.Width = 255;
            // 
            // SaveFileDlg
            // 
            this.SaveFileDlg.CheckFileExists = true;
            this.SaveFileDlg.Filter = " \"Text files (*.txt)|*.txt|All files (*.*)|*.*\"";
            this.SaveFileDlg.Title = "Save Table of Values";
            // 
            // ValueTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Print);
            this.Controls.Add(this.ValuesGrd);
            this.Controls.Add(this.Values);
            this.Controls.Add(this.OptionNameTxt);
            this.Controls.Add(this.OptionName);
            this.Controls.Add(this.GenerationParametersTxt);
            this.Controls.Add(this.GenerationParameters);
            this.MaximizeBox = false;
            this.Name = "ValueTable";
            this.Text = "Table of Values";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ValueTable_FormClosing);
            this.Load += new System.EventHandler(this.ValueTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ValuesGrd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GenerationParameters;
        private System.Windows.Forms.TextBox GenerationParametersTxt;
        private System.Windows.Forms.TextBox OptionNameTxt;
        private System.Windows.Forms.Label OptionName;
        private System.Windows.Forms.Label Values;
        private System.Windows.Forms.DataGridView ValuesGrd;
        private System.Windows.Forms.Button Print;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
        private System.Windows.Forms.SaveFileDialog SaveFileDlg;
    }
}