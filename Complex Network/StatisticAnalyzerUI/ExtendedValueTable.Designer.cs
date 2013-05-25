namespace StatisticAnalyzerUI
{
    partial class ExtendedValueTable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.valuesGrd = new System.Windows.Forms.DataGridView();
            this.KeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.values = new System.Windows.Forms.Label();
            this.generationParameters = new System.Windows.Forms.Label();
            this.generationParametersTxt = new System.Windows.Forms.TextBox();
            this.excelButton = new System.Windows.Forms.Button();
            this.Print = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.valuesGrd)).BeginInit();
            this.SuspendLayout();
            // 
            // valuesGrd
            // 
            this.valuesGrd.AllowUserToAddRows = false;
            this.valuesGrd.AllowUserToDeleteRows = false;
            this.valuesGrd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.valuesGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.valuesGrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KeyColumn,
            this.ValueColumn});
            this.valuesGrd.Location = new System.Drawing.Point(15, 72);
            this.valuesGrd.Name = "valuesGrd";
            this.valuesGrd.ReadOnly = true;
            this.valuesGrd.Size = new System.Drawing.Size(557, 471);
            this.valuesGrd.TabIndex = 13;
            // 
            // KeyColumn
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KeyColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.KeyColumn.HeaderText = "Column1";
            this.KeyColumn.Name = "KeyColumn";
            this.KeyColumn.ReadOnly = true;
            this.KeyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ValueColumn
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ValueColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.ValueColumn.HeaderText = "Column2";
            this.ValueColumn.Name = "ValueColumn";
            this.ValueColumn.ReadOnly = true;
            this.ValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // values
            // 
            this.values.AutoSize = true;
            this.values.Location = new System.Drawing.Point(12, 56);
            this.values.Name = "values";
            this.values.Size = new System.Drawing.Size(39, 13);
            this.values.TabIndex = 12;
            this.values.Text = "Values";
            // 
            // generationParameters
            // 
            this.generationParameters.AutoSize = true;
            this.generationParameters.Location = new System.Drawing.Point(12, 19);
            this.generationParameters.Name = "generationParameters";
            this.generationParameters.Size = new System.Drawing.Size(115, 13);
            this.generationParameters.TabIndex = 11;
            this.generationParameters.Text = "Generation Parameters";
            // 
            // generationParametersTxt
            // 
            this.generationParametersTxt.Location = new System.Drawing.Point(133, 16);
            this.generationParametersTxt.Name = "generationParametersTxt";
            this.generationParametersTxt.ReadOnly = true;
            this.generationParametersTxt.Size = new System.Drawing.Size(439, 20);
            this.generationParametersTxt.TabIndex = 14;
            // 
            // excelButton
            // 
            this.excelButton.Location = new System.Drawing.Point(497, 560);
            this.excelButton.Name = "excelButton";
            this.excelButton.Size = new System.Drawing.Size(75, 23);
            this.excelButton.TabIndex = 16;
            this.excelButton.Text = "Save";
            this.excelButton.UseVisualStyleBackColor = true;
            this.excelButton.Click += new System.EventHandler(this.excelButton_Click);
            // 
            // Print
            // 
            this.Print.Location = new System.Drawing.Point(403, 560);
            this.Print.Name = "Print";
            this.Print.Size = new System.Drawing.Size(75, 23);
            this.Print.TabIndex = 15;
            this.Print.Text = "Print";
            this.Print.UseVisualStyleBackColor = true;
            this.Print.Click += new System.EventHandler(this.Print_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Excel files (*.xls)|*.xls\"";
            this.saveFileDialog.InitialDirectory = "C:\\";
            this.saveFileDialog.RestoreDirectory = true;
            this.saveFileDialog.Title = "Export Excel File To";
            // 
            // ExtendedValueTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 595);
            this.Controls.Add(this.excelButton);
            this.Controls.Add(this.Print);
            this.Controls.Add(this.generationParametersTxt);
            this.Controls.Add(this.valuesGrd);
            this.Controls.Add(this.values);
            this.Controls.Add(this.generationParameters);
            this.MaximizeBox = false;
            this.Name = "ExtendedValueTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Table";
            this.Load += new System.EventHandler(this.ExtendedValueTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.valuesGrd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView valuesGrd;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
        private System.Windows.Forms.Label values;
        private System.Windows.Forms.Label generationParameters;
        private System.Windows.Forms.TextBox generationParametersTxt;
        private System.Windows.Forms.Button excelButton;
        private System.Windows.Forms.Button Print;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}