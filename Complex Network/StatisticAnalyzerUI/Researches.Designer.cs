namespace StatisticAnalyzerUI
{
    partial class Researches
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
            this.showGraphics = new System.Windows.Forms.Button();
            this.researchNames = new System.Windows.Forms.Label();
            this.researchNamesCmb = new System.Windows.Forms.ComboBox();
            this.information = new System.Windows.Forms.Label();
            this.informationGrd = new System.Windows.Forms.DataGridView();
            this.field = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.informationGrd)).BeginInit();
            this.SuspendLayout();
            // 
            // showGraphics
            // 
            this.showGraphics.Location = new System.Drawing.Point(15, 310);
            this.showGraphics.Name = "showGraphics";
            this.showGraphics.Size = new System.Drawing.Size(377, 29);
            this.showGraphics.TabIndex = 0;
            this.showGraphics.Text = "Show Graphics";
            this.showGraphics.UseVisualStyleBackColor = true;
            this.showGraphics.Click += new System.EventHandler(this.showGraphics_Click);
            // 
            // researchNames
            // 
            this.researchNames.AutoSize = true;
            this.researchNames.Location = new System.Drawing.Point(12, 9);
            this.researchNames.Name = "researchNames";
            this.researchNames.Size = new System.Drawing.Size(89, 13);
            this.researchNames.TabIndex = 1;
            this.researchNames.Text = "Research Names";
            // 
            // researchNamesCmb
            // 
            this.researchNamesCmb.FormattingEnabled = true;
            this.researchNamesCmb.Location = new System.Drawing.Point(15, 25);
            this.researchNamesCmb.Name = "researchNamesCmb";
            this.researchNamesCmb.Size = new System.Drawing.Size(121, 21);
            this.researchNamesCmb.TabIndex = 2;
            this.researchNamesCmb.SelectedValueChanged += new System.EventHandler(this.researchNamesCmb_SelectedValueChanged);
            // 
            // information
            // 
            this.information.AutoSize = true;
            this.information.Location = new System.Drawing.Point(12, 59);
            this.information.Name = "information";
            this.information.Size = new System.Drawing.Size(59, 13);
            this.information.TabIndex = 3;
            this.information.Text = "Information";
            // 
            // informationGrd
            // 
            this.informationGrd.AllowUserToAddRows = false;
            this.informationGrd.AllowUserToDeleteRows = false;
            this.informationGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.informationGrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.field,
            this.value});
            this.informationGrd.Location = new System.Drawing.Point(15, 75);
            this.informationGrd.Name = "informationGrd";
            this.informationGrd.ReadOnly = true;
            this.informationGrd.Size = new System.Drawing.Size(377, 229);
            this.informationGrd.TabIndex = 4;
            // 
            // field
            // 
            this.field.HeaderText = "Field";
            this.field.Name = "field";
            this.field.ReadOnly = true;
            // 
            // value
            // 
            this.value.HeaderText = "Value";
            this.value.Name = "value";
            this.value.ReadOnly = true;
            this.value.Width = 230;
            // 
            // Researches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 351);
            this.Controls.Add(this.informationGrd);
            this.Controls.Add(this.information);
            this.Controls.Add(this.researchNamesCmb);
            this.Controls.Add(this.researchNames);
            this.Controls.Add(this.showGraphics);
            this.Name = "Researches";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Researches";
            this.Load += new System.EventHandler(this.Researches_Load);
            ((System.ComponentModel.ISupportInitialize)(this.informationGrd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button showGraphics;
        private System.Windows.Forms.Label researchNames;
        private System.Windows.Forms.ComboBox researchNamesCmb;
        private System.Windows.Forms.Label information;
        private System.Windows.Forms.DataGridView informationGrd;
        private System.Windows.Forms.DataGridViewTextBoxColumn field;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
    }
}