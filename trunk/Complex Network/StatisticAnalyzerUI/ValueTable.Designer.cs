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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GenerationParameters = new System.Windows.Forms.Label();
            this.OptionNameLabel = new System.Windows.Forms.Label();
            this.Values = new System.Windows.Forms.Label();
            this.Print = new System.Windows.Forms.Button();
            this.excelButton = new System.Windows.Forms.Button();
            this.approximationTxt = new System.Windows.Forms.TextBox();
            this.apprLabel = new System.Windows.Forms.Label();
            this.ValuesGrd = new System.Windows.Forms.DataGridView();
            this.KeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.generationCmbBox = new System.Windows.Forms.ComboBox();
            this.optionCmbBox = new System.Windows.Forms.ComboBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ValuesGrd)).BeginInit();
            this.SuspendLayout();
            // 
            // GenerationParameters
            // 
            this.GenerationParameters.AutoSize = true;
            this.GenerationParameters.Location = new System.Drawing.Point(12, 19);
            this.GenerationParameters.Name = "GenerationParameters";
            this.GenerationParameters.Size = new System.Drawing.Size(115, 13);
            this.GenerationParameters.TabIndex = 0;
            this.GenerationParameters.Text = "Generation Parameters";
            // 
            // OptionNameLabel
            // 
            this.OptionNameLabel.AutoSize = true;
            this.OptionNameLabel.Location = new System.Drawing.Point(12, 47);
            this.OptionNameLabel.Name = "OptionNameLabel";
            this.OptionNameLabel.Size = new System.Drawing.Size(69, 13);
            this.OptionNameLabel.TabIndex = 2;
            this.OptionNameLabel.Text = "Option Name";
            // 
            // Values
            // 
            this.Values.AutoSize = true;
            this.Values.Location = new System.Drawing.Point(12, 107);
            this.Values.Name = "Values";
            this.Values.Size = new System.Drawing.Size(39, 13);
            this.Values.TabIndex = 4;
            this.Values.Text = "Values";
            // 
            // Print
            // 
            this.Print.Location = new System.Drawing.Point(398, 527);
            this.Print.Name = "Print";
            this.Print.Size = new System.Drawing.Size(75, 23);
            this.Print.TabIndex = 6;
            this.Print.Text = "Print";
            this.Print.UseVisualStyleBackColor = true;
            // 
            // excelButton
            // 
            this.excelButton.Location = new System.Drawing.Point(497, 527);
            this.excelButton.Name = "excelButton";
            this.excelButton.Size = new System.Drawing.Size(75, 23);
            this.excelButton.TabIndex = 7;
            this.excelButton.Text = "Save";
            this.excelButton.UseVisualStyleBackColor = true;
            this.excelButton.Click += new System.EventHandler(this.excelButton_Click);
            // 
            // approximationTxt
            // 
            this.approximationTxt.Location = new System.Drawing.Point(133, 74);
            this.approximationTxt.Name = "approximationTxt";
            this.approximationTxt.ReadOnly = true;
            this.approximationTxt.Size = new System.Drawing.Size(439, 20);
            this.approximationTxt.TabIndex = 9;
            // 
            // apprLabel
            // 
            this.apprLabel.AutoSize = true;
            this.apprLabel.Location = new System.Drawing.Point(12, 77);
            this.apprLabel.Name = "apprLabel";
            this.apprLabel.Size = new System.Drawing.Size(73, 13);
            this.apprLabel.TabIndex = 8;
            this.apprLabel.Text = "Approximation";
            // 
            // ValuesGrd
            // 
            this.ValuesGrd.AllowUserToAddRows = false;
            this.ValuesGrd.AllowUserToDeleteRows = false;
            this.ValuesGrd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ValuesGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ValuesGrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KeyColumn,
            this.ValueColumn});
            this.ValuesGrd.Location = new System.Drawing.Point(15, 123);
            this.ValuesGrd.Name = "ValuesGrd";
            this.ValuesGrd.ReadOnly = true;
            this.ValuesGrd.Size = new System.Drawing.Size(557, 389);
            this.ValuesGrd.TabIndex = 5;
            // 
            // KeyColumn
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KeyColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.KeyColumn.HeaderText = "Column1";
            this.KeyColumn.Name = "KeyColumn";
            this.KeyColumn.ReadOnly = true;
            this.KeyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ValueColumn
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ValueColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.ValueColumn.HeaderText = "Column2";
            this.ValueColumn.Name = "ValueColumn";
            this.ValueColumn.ReadOnly = true;
            this.ValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // generationCmbBox
            // 
            this.generationCmbBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.generationCmbBox.FormattingEnabled = true;
            this.generationCmbBox.Location = new System.Drawing.Point(133, 16);
            this.generationCmbBox.Name = "generationCmbBox";
            this.generationCmbBox.Size = new System.Drawing.Size(439, 21);
            this.generationCmbBox.TabIndex = 10;
            this.generationCmbBox.SelectedIndexChanged += new System.EventHandler(this.generationCmbBox_SelectedIndexChanged);
            // 
            // optionCmbBox
            // 
            this.optionCmbBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.optionCmbBox.FormattingEnabled = true;
            this.optionCmbBox.Location = new System.Drawing.Point(133, 44);
            this.optionCmbBox.Name = "optionCmbBox";
            this.optionCmbBox.Size = new System.Drawing.Size(439, 21);
            this.optionCmbBox.TabIndex = 11;
            this.optionCmbBox.SelectedIndexChanged += new System.EventHandler(this.optionCmbBox_SelectedIndexChanged);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Excel files (*.xls)|*.xls\"";
            this.saveFileDialog.InitialDirectory = "C:\\";
            this.saveFileDialog.RestoreDirectory = true;
            this.saveFileDialog.Title = "Export Excel File To";
            // 
            // ValueTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.optionCmbBox);
            this.Controls.Add(this.generationCmbBox);
            this.Controls.Add(this.ValuesGrd);
            this.Controls.Add(this.approximationTxt);
            this.Controls.Add(this.apprLabel);
            this.Controls.Add(this.excelButton);
            this.Controls.Add(this.Print);
            this.Controls.Add(this.Values);
            this.Controls.Add(this.OptionNameLabel);
            this.Controls.Add(this.GenerationParameters);
            this.MaximizeBox = false;
            this.Name = "ValueTable";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Table of Values";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ValueTable_FormClosing);
            this.Load += new System.EventHandler(this.ValueTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ValuesGrd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GenerationParameters;
        private System.Windows.Forms.Label OptionNameLabel;
        private System.Windows.Forms.Label Values;
        private System.Windows.Forms.Button Print;
        private System.Windows.Forms.Button excelButton;
        private System.Windows.Forms.TextBox approximationTxt;
        private System.Windows.Forms.Label apprLabel;
        private System.Windows.Forms.DataGridView ValuesGrd;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
        private System.Windows.Forms.ComboBox generationCmbBox;
        private System.Windows.Forms.ComboBox optionCmbBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}