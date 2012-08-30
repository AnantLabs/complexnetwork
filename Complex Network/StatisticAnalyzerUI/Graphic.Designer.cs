namespace StatisticAnalyzerUI
{
    partial class Graphic
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
            this.components = new System.ComponentModel.Container();
            this.MathWaiting = new System.Windows.Forms.Label();
            this.MathWaitingTxt = new System.Windows.Forms.TextBox();
            this.DispertionTxt = new System.Windows.Forms.TextBox();
            this.Dispersion = new System.Windows.Forms.Label();
            this.CommonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.valueTableButton = new System.Windows.Forms.Button();
            this.InformationGrp = new System.Windows.Forms.GroupBox();
            this.realCoutTxt = new System.Windows.Forms.TextBox();
            this.realCount = new System.Windows.Forms.Label();
            this.PropertyTxt = new System.Windows.Forms.TextBox();
            this.Property = new System.Windows.Forms.Label();
            this.ApproximationTxt = new System.Windows.Forms.TextBox();
            this.Approximation = new System.Windows.Forms.Label();
            this.NetworkSizeTxt = new System.Windows.Forms.TextBox();
            this.NetworkSize = new System.Windows.Forms.Label();
            this.ModelNameTxt = new System.Windows.Forms.TextBox();
            this.ModelName = new System.Windows.Forms.Label();
            this.ResultsGrp = new System.Windows.Forms.GroupBox();
            this.AverageTxt = new System.Windows.Forms.TextBox();
            this.Average = new System.Windows.Forms.Label();
            this.optionTabs = new System.Windows.Forms.TabControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.InformationGrp.SuspendLayout();
            this.ResultsGrp.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MathWaiting
            // 
            this.MathWaiting.AutoSize = true;
            this.MathWaiting.BackColor = System.Drawing.Color.LightSlateGray;
            this.MathWaiting.Location = new System.Drawing.Point(6, 55);
            this.MathWaiting.Name = "MathWaiting";
            this.MathWaiting.Size = new System.Drawing.Size(70, 13);
            this.MathWaiting.TabIndex = 4;
            this.MathWaiting.Text = "Math Waiting";
            // 
            // MathWaitingTxt
            // 
            this.MathWaitingTxt.BackColor = System.Drawing.SystemColors.Control;
            this.MathWaitingTxt.Location = new System.Drawing.Point(6, 71);
            this.MathWaitingTxt.Name = "MathWaitingTxt";
            this.MathWaitingTxt.ReadOnly = true;
            this.MathWaitingTxt.Size = new System.Drawing.Size(128, 20);
            this.MathWaitingTxt.TabIndex = 5;
            this.MathWaitingTxt.Text = "Not Evaluated";
            this.MathWaitingTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CommonToolTip.SetToolTip(this.MathWaitingTxt, "Mathematical waiting for the current option.");
            // 
            // DispertionTxt
            // 
            this.DispertionTxt.BackColor = System.Drawing.SystemColors.Control;
            this.DispertionTxt.Location = new System.Drawing.Point(6, 110);
            this.DispertionTxt.Name = "DispertionTxt";
            this.DispertionTxt.ReadOnly = true;
            this.DispertionTxt.Size = new System.Drawing.Size(128, 20);
            this.DispertionTxt.TabIndex = 7;
            this.DispertionTxt.Text = "Not Evaluated";
            this.DispertionTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CommonToolTip.SetToolTip(this.DispertionTxt, "Dispersion for the current option.");
            // 
            // Dispersion
            // 
            this.Dispersion.AutoSize = true;
            this.Dispersion.BackColor = System.Drawing.Color.LightSlateGray;
            this.Dispersion.Location = new System.Drawing.Point(6, 94);
            this.Dispersion.Name = "Dispersion";
            this.Dispersion.Size = new System.Drawing.Size(56, 13);
            this.Dispersion.TabIndex = 6;
            this.Dispersion.Text = "Dispersion";
            // 
            // CommonToolTip
            // 
            this.CommonToolTip.IsBalloon = true;
            this.CommonToolTip.ShowAlways = true;
            // 
            // valueTableButton
            // 
            this.valueTableButton.Location = new System.Drawing.Point(84, 442);
            this.valueTableButton.Name = "valueTableButton";
            this.valueTableButton.Size = new System.Drawing.Size(65, 23);
            this.valueTableButton.TabIndex = 8;
            this.valueTableButton.Text = "Values";
            this.valueTableButton.UseVisualStyleBackColor = true;
            this.valueTableButton.Click += new System.EventHandler(this.ValueTable_Click);
            // 
            // InformationGrp
            // 
            this.InformationGrp.BackColor = System.Drawing.Color.LightSlateGray;
            this.InformationGrp.Controls.Add(this.realCoutTxt);
            this.InformationGrp.Controls.Add(this.realCount);
            this.InformationGrp.Controls.Add(this.PropertyTxt);
            this.InformationGrp.Controls.Add(this.Property);
            this.InformationGrp.Controls.Add(this.ApproximationTxt);
            this.InformationGrp.Controls.Add(this.Approximation);
            this.InformationGrp.Controls.Add(this.NetworkSizeTxt);
            this.InformationGrp.Controls.Add(this.NetworkSize);
            this.InformationGrp.Controls.Add(this.ModelNameTxt);
            this.InformationGrp.Controls.Add(this.ModelName);
            this.InformationGrp.Location = new System.Drawing.Point(9, 31);
            this.InformationGrp.Name = "InformationGrp";
            this.InformationGrp.Size = new System.Drawing.Size(140, 223);
            this.InformationGrp.TabIndex = 9;
            this.InformationGrp.TabStop = false;
            this.InformationGrp.Text = "Information";
            // 
            // realCoutTxt
            // 
            this.realCoutTxt.Location = new System.Drawing.Point(9, 109);
            this.realCoutTxt.Name = "realCoutTxt";
            this.realCoutTxt.ReadOnly = true;
            this.realCoutTxt.Size = new System.Drawing.Size(125, 20);
            this.realCoutTxt.TabIndex = 9;
            this.realCoutTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // realCount
            // 
            this.realCount.AutoSize = true;
            this.realCount.Location = new System.Drawing.Point(6, 93);
            this.realCount.Name = "realCount";
            this.realCount.Size = new System.Drawing.Size(95, 13);
            this.realCount.TabIndex = 8;
            this.realCount.Text = "Realizations Count";
            // 
            // PropertyTxt
            // 
            this.PropertyTxt.Location = new System.Drawing.Point(9, 187);
            this.PropertyTxt.Name = "PropertyTxt";
            this.PropertyTxt.ReadOnly = true;
            this.PropertyTxt.Size = new System.Drawing.Size(125, 20);
            this.PropertyTxt.TabIndex = 7;
            this.PropertyTxt.Text = "Not Set";
            this.PropertyTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Property
            // 
            this.Property.AutoSize = true;
            this.Property.Location = new System.Drawing.Point(6, 171);
            this.Property.Name = "Property";
            this.Property.Size = new System.Drawing.Size(46, 13);
            this.Property.TabIndex = 6;
            this.Property.Text = "Property";
            // 
            // ApproximationTxt
            // 
            this.ApproximationTxt.Location = new System.Drawing.Point(9, 148);
            this.ApproximationTxt.Name = "ApproximationTxt";
            this.ApproximationTxt.ReadOnly = true;
            this.ApproximationTxt.Size = new System.Drawing.Size(125, 20);
            this.ApproximationTxt.TabIndex = 5;
            this.ApproximationTxt.Text = "No Approximation";
            this.ApproximationTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Approximation
            // 
            this.Approximation.AutoSize = true;
            this.Approximation.Location = new System.Drawing.Point(6, 132);
            this.Approximation.Name = "Approximation";
            this.Approximation.Size = new System.Drawing.Size(73, 13);
            this.Approximation.TabIndex = 4;
            this.Approximation.Text = "Approximation";
            // 
            // NetworkSizeTxt
            // 
            this.NetworkSizeTxt.Location = new System.Drawing.Point(9, 71);
            this.NetworkSizeTxt.Name = "NetworkSizeTxt";
            this.NetworkSizeTxt.ReadOnly = true;
            this.NetworkSizeTxt.Size = new System.Drawing.Size(125, 20);
            this.NetworkSizeTxt.TabIndex = 3;
            this.NetworkSizeTxt.Text = "0";
            this.NetworkSizeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NetworkSize
            // 
            this.NetworkSize.AutoSize = true;
            this.NetworkSize.Location = new System.Drawing.Point(6, 55);
            this.NetworkSize.Name = "NetworkSize";
            this.NetworkSize.Size = new System.Drawing.Size(70, 13);
            this.NetworkSize.TabIndex = 2;
            this.NetworkSize.Text = "Network Size";
            // 
            // ModelNameTxt
            // 
            this.ModelNameTxt.Location = new System.Drawing.Point(9, 32);
            this.ModelNameTxt.Name = "ModelNameTxt";
            this.ModelNameTxt.ReadOnly = true;
            this.ModelNameTxt.Size = new System.Drawing.Size(125, 20);
            this.ModelNameTxt.TabIndex = 1;
            this.ModelNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ModelName
            // 
            this.ModelName.AutoSize = true;
            this.ModelName.Location = new System.Drawing.Point(6, 16);
            this.ModelName.Name = "ModelName";
            this.ModelName.Size = new System.Drawing.Size(67, 13);
            this.ModelName.TabIndex = 0;
            this.ModelName.Text = "Model Name";
            // 
            // ResultsGrp
            // 
            this.ResultsGrp.BackColor = System.Drawing.Color.LightSlateGray;
            this.ResultsGrp.Controls.Add(this.AverageTxt);
            this.ResultsGrp.Controls.Add(this.Average);
            this.ResultsGrp.Controls.Add(this.MathWaiting);
            this.ResultsGrp.Controls.Add(this.DispertionTxt);
            this.ResultsGrp.Controls.Add(this.MathWaitingTxt);
            this.ResultsGrp.Controls.Add(this.Dispersion);
            this.ResultsGrp.Location = new System.Drawing.Point(9, 277);
            this.ResultsGrp.Name = "ResultsGrp";
            this.ResultsGrp.Size = new System.Drawing.Size(140, 142);
            this.ResultsGrp.TabIndex = 10;
            this.ResultsGrp.TabStop = false;
            this.ResultsGrp.Text = "Results";
            // 
            // AverageTxt
            // 
            this.AverageTxt.Location = new System.Drawing.Point(9, 32);
            this.AverageTxt.Name = "AverageTxt";
            this.AverageTxt.ReadOnly = true;
            this.AverageTxt.Size = new System.Drawing.Size(125, 20);
            this.AverageTxt.TabIndex = 3;
            this.AverageTxt.Text = "Not Evaluated";
            this.AverageTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Average
            // 
            this.Average.AutoSize = true;
            this.Average.Location = new System.Drawing.Point(6, 16);
            this.Average.Name = "Average";
            this.Average.Size = new System.Drawing.Size(77, 13);
            this.Average.TabIndex = 2;
            this.Average.Text = "Average Value\r\n";
            // 
            // optionTabs
            // 
            this.optionTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionTabs.Location = new System.Drawing.Point(0, 0);
            this.optionTabs.Name = "optionTabs";
            this.optionTabs.SelectedIndex = 0;
            this.optionTabs.Size = new System.Drawing.Size(693, 511);
            this.optionTabs.TabIndex = 11;
            this.optionTabs.SelectedIndexChanged += new System.EventHandler(this.optionTabs_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSlateGray;
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Controls.Add(this.InformationGrp);
            this.panel1.Controls.Add(this.ResultsGrp);
            this.panel1.Controls.Add(this.valueTableButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 511);
            this.panel1.TabIndex = 10;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(9, 442);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(65, 23);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.Save_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.optionTabs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(853, 511);
            this.splitContainer1.SplitterDistance = 693;
            this.splitContainer1.TabIndex = 12;
            // 
            // Graphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 511);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.Name = "Graphic";
            this.Text = "Graphic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Graphic_FormClosing);
            this.Load += new System.EventHandler(this.Graphic_Load);
            this.InformationGrp.ResumeLayout(false);
            this.InformationGrp.PerformLayout();
            this.ResultsGrp.ResumeLayout(false);
            this.ResultsGrp.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label MathWaiting;
        private System.Windows.Forms.TextBox MathWaitingTxt;
        private System.Windows.Forms.TextBox DispertionTxt;
        private System.Windows.Forms.Label Dispersion;
        private System.Windows.Forms.ToolTip CommonToolTip;
        private System.Windows.Forms.Button valueTableButton;
        private System.Windows.Forms.GroupBox InformationGrp;
        private System.Windows.Forms.TextBox PropertyTxt;
        private System.Windows.Forms.Label Property;
        private System.Windows.Forms.TextBox ApproximationTxt;
        private System.Windows.Forms.Label Approximation;
        private System.Windows.Forms.TextBox NetworkSizeTxt;
        private System.Windows.Forms.Label NetworkSize;
        private System.Windows.Forms.TextBox ModelNameTxt;
        private System.Windows.Forms.Label ModelName;
        private System.Windows.Forms.GroupBox ResultsGrp;
        private System.Windows.Forms.TextBox AverageTxt;
        private System.Windows.Forms.Label Average;
        private System.Windows.Forms.TabControl optionTabs;
        private System.Windows.Forms.TextBox realCoutTxt;
        private System.Windows.Forms.Label realCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SplitContainer splitContainer1;


    }
}