namespace Random_Networks_Statistic_Analyzer
{
    partial class ChartTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.analyzeOptionChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.valueTable = new System.Windows.Forms.DataGridView();
            this.xAxis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yAxis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analyzeOptionChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueTable)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.analyzeOptionChart);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.valueTable);
            this.splitContainer.Size = new System.Drawing.Size(702, 509);
            this.splitContainer.SplitterDistance = 558;
            this.splitContainer.TabIndex = 0;
            // 
            // analyzeOptionChart
            // 
            chartArea1.Name = "analyzeOptionChartArea";
            this.analyzeOptionChart.ChartAreas.Add(chartArea1);
            this.analyzeOptionChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.analyzeOptionChart.Legends.Add(legend1);
            this.analyzeOptionChart.Location = new System.Drawing.Point(0, 0);
            this.analyzeOptionChart.Name = "analyzeOptionChart";
            series1.ChartArea = "analyzeOptionChartArea";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.analyzeOptionChart.Series.Add(series1);
            this.analyzeOptionChart.Size = new System.Drawing.Size(558, 509);
            this.analyzeOptionChart.TabIndex = 0;
            this.analyzeOptionChart.Text = "chart1";
            // 
            // valueTable
            // 
            this.valueTable.AllowUserToAddRows = false;
            this.valueTable.AllowUserToDeleteRows = false;
            this.valueTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.valueTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xAxis,
            this.yAxis});
            this.valueTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueTable.Location = new System.Drawing.Point(0, 0);
            this.valueTable.Name = "valueTable";
            this.valueTable.ReadOnly = true;
            this.valueTable.RowHeadersVisible = false;
            this.valueTable.Size = new System.Drawing.Size(140, 509);
            this.valueTable.TabIndex = 0;
            // 
            // xAxis
            // 
            this.xAxis.HeaderText = "";
            this.xAxis.Name = "xAxis";
            this.xAxis.ReadOnly = true;
            // 
            // yAxis
            // 
            this.yAxis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.yAxis.HeaderText = "";
            this.yAxis.Name = "yAxis";
            this.yAxis.ReadOnly = true;
            // 
            // ChartTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "ChartTab";
            this.Size = new System.Drawing.Size(702, 509);
            this.Load += new System.EventHandler(this.ChartTab_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.analyzeOptionChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataVisualization.Charting.Chart analyzeOptionChart;
        private System.Windows.Forms.DataGridView valueTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn xAxis;
        private System.Windows.Forms.DataGridViewTextBoxColumn yAxis;
    }
}
