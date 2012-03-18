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
            this.Splitter = new System.Windows.Forms.Splitter();
            this.ZedGraph = new ZedGraph.ZedGraphControl();
            this.Clear = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.MathWaiting = new System.Windows.Forms.Label();
            this.MathWaitingTxt = new System.Windows.Forms.TextBox();
            this.DTxt = new System.Windows.Forms.TextBox();
            this.Dispersion = new System.Windows.Forms.Label();
            this.CommonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ValueTable = new System.Windows.Forms.Button();
            this.InformationGrp = new System.Windows.Forms.GroupBox();
            this.ModelName = new System.Windows.Forms.Label();
            this.ModelNameTxt = new System.Windows.Forms.TextBox();
            this.NetworkSizeTxt = new System.Windows.Forms.TextBox();
            this.NetworkSize = new System.Windows.Forms.Label();
            this.ApproximationTxt = new System.Windows.Forms.TextBox();
            this.Approximation = new System.Windows.Forms.Label();
            this.PropertyTxt = new System.Windows.Forms.TextBox();
            this.Property = new System.Windows.Forms.Label();
            this.ResultsGrp = new System.Windows.Forms.GroupBox();
            this.AverageTxt = new System.Windows.Forms.TextBox();
            this.Average = new System.Windows.Forms.Label();
            this.InformationGrp.SuspendLayout();
            this.ResultsGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // Splitter
            // 
            this.Splitter.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Splitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.Splitter.Location = new System.Drawing.Point(714, 0);
            this.Splitter.Name = "Splitter";
            this.Splitter.Size = new System.Drawing.Size(170, 662);
            this.Splitter.TabIndex = 0;
            this.Splitter.TabStop = false;
            // 
            // ZedGraph
            // 
            this.ZedGraph.BackColor = System.Drawing.Color.White;
            this.ZedGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZedGraph.IsAutoScrollRange = true;
            this.ZedGraph.IsShowPointValues = true;
            this.ZedGraph.IsSynchronizeXAxes = true;
            this.ZedGraph.IsSynchronizeYAxes = true;
            this.ZedGraph.Location = new System.Drawing.Point(0, 0);
            this.ZedGraph.Name = "ZedGraph";
            this.ZedGraph.ScrollGrace = 0D;
            this.ZedGraph.ScrollMaxX = 0D;
            this.ZedGraph.ScrollMaxY = 0D;
            this.ZedGraph.ScrollMaxY2 = 0D;
            this.ZedGraph.ScrollMinX = 0D;
            this.ZedGraph.ScrollMinY = 0D;
            this.ZedGraph.ScrollMinY2 = 0D;
            this.ZedGraph.Size = new System.Drawing.Size(714, 662);
            this.ZedGraph.TabIndex = 1;
            // 
            // Clear
            // 
            this.Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Clear.Location = new System.Drawing.Point(803, 627);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(65, 23);
            this.Clear.TabIndex = 2;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Save.Location = new System.Drawing.Point(732, 627);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(65, 23);
            this.Save.TabIndex = 3;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // MathWaiting
            // 
            this.MathWaiting.AutoSize = true;
            this.MathWaiting.BackColor = System.Drawing.SystemColors.AppWorkspace;
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
            // DTxt
            // 
            this.DTxt.BackColor = System.Drawing.SystemColors.Control;
            this.DTxt.Location = new System.Drawing.Point(6, 110);
            this.DTxt.Name = "DTxt";
            this.DTxt.ReadOnly = true;
            this.DTxt.Size = new System.Drawing.Size(128, 20);
            this.DTxt.TabIndex = 7;
            this.DTxt.Text = "Not Evaluated";
            this.DTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CommonToolTip.SetToolTip(this.DTxt, "Dispersion for the current option.");
            // 
            // Dispersion
            // 
            this.Dispersion.AutoSize = true;
            this.Dispersion.BackColor = System.Drawing.SystemColors.AppWorkspace;
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
            // ValueTable
            // 
            this.ValueTable.Location = new System.Drawing.Point(732, 589);
            this.ValueTable.Name = "ValueTable";
            this.ValueTable.Size = new System.Drawing.Size(136, 23);
            this.ValueTable.TabIndex = 8;
            this.ValueTable.Text = "Table of Values";
            this.ValueTable.UseVisualStyleBackColor = true;
            this.ValueTable.Click += new System.EventHandler(this.ValueTable_Click);
            // 
            // InformationGrp
            // 
            this.InformationGrp.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.InformationGrp.Controls.Add(this.PropertyTxt);
            this.InformationGrp.Controls.Add(this.Property);
            this.InformationGrp.Controls.Add(this.ApproximationTxt);
            this.InformationGrp.Controls.Add(this.Approximation);
            this.InformationGrp.Controls.Add(this.NetworkSizeTxt);
            this.InformationGrp.Controls.Add(this.NetworkSize);
            this.InformationGrp.Controls.Add(this.ModelNameTxt);
            this.InformationGrp.Controls.Add(this.ModelName);
            this.InformationGrp.Location = new System.Drawing.Point(732, 122);
            this.InformationGrp.Name = "InformationGrp";
            this.InformationGrp.Size = new System.Drawing.Size(140, 181);
            this.InformationGrp.TabIndex = 9;
            this.InformationGrp.TabStop = false;
            this.InformationGrp.Text = "Information";
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
            // ModelNameTxt
            // 
            this.ModelNameTxt.Location = new System.Drawing.Point(9, 32);
            this.ModelNameTxt.Name = "ModelNameTxt";
            this.ModelNameTxt.ReadOnly = true;
            this.ModelNameTxt.Size = new System.Drawing.Size(125, 20);
            this.ModelNameTxt.TabIndex = 1;
            this.ModelNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // ApproximationTxt
            // 
            this.ApproximationTxt.Location = new System.Drawing.Point(9, 110);
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
            this.Approximation.Location = new System.Drawing.Point(6, 94);
            this.Approximation.Name = "Approximation";
            this.Approximation.Size = new System.Drawing.Size(73, 13);
            this.Approximation.TabIndex = 4;
            this.Approximation.Text = "Approximation";
            // 
            // PropertyTxt
            // 
            this.PropertyTxt.Location = new System.Drawing.Point(9, 149);
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
            this.Property.Location = new System.Drawing.Point(6, 133);
            this.Property.Name = "Property";
            this.Property.Size = new System.Drawing.Size(46, 13);
            this.Property.TabIndex = 6;
            this.Property.Text = "Property";
            // 
            // ResultsGrp
            // 
            this.ResultsGrp.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ResultsGrp.Controls.Add(this.AverageTxt);
            this.ResultsGrp.Controls.Add(this.Average);
            this.ResultsGrp.Controls.Add(this.MathWaiting);
            this.ResultsGrp.Controls.Add(this.DTxt);
            this.ResultsGrp.Controls.Add(this.MathWaitingTxt);
            this.ResultsGrp.Controls.Add(this.Dispersion);
            this.ResultsGrp.Location = new System.Drawing.Point(732, 325);
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
            // Graphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 662);
            this.Controls.Add(this.ResultsGrp);
            this.Controls.Add(this.InformationGrp);
            this.Controls.Add(this.ValueTable);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.ZedGraph);
            this.Controls.Add(this.Splitter);
            this.Name = "Graphic";
            this.Text = "Graphic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClose);
            this.Load += new System.EventHandler(this.Graphic_Load);
            this.InformationGrp.ResumeLayout(false);
            this.InformationGrp.PerformLayout();
            this.ResultsGrp.ResumeLayout(false);
            this.ResultsGrp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter Splitter;
        private ZedGraph.ZedGraphControl ZedGraph;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label MathWaiting;
        private System.Windows.Forms.TextBox MathWaitingTxt;
        private System.Windows.Forms.TextBox DTxt;
        private System.Windows.Forms.Label Dispersion;
        private System.Windows.Forms.ToolTip CommonToolTip;
        private System.Windows.Forms.Button ValueTable;
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


    }
}