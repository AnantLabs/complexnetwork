namespace StatisticAnalyzerUI
{
    partial class ExtendedAnalyze
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
            this.trajectoryAnalyze = new System.Windows.Forms.Button();
            this.stepsToRemove = new System.Windows.Forms.Label();
            this.stepsToRemoveTxt = new System.Windows.Forms.TextBox();
            this.modelName = new System.Windows.Forms.Label();
            this.modelNameTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // trajectoryAnalyze
            // 
            this.trajectoryAnalyze.Location = new System.Drawing.Point(183, 62);
            this.trajectoryAnalyze.Name = "trajectoryAnalyze";
            this.trajectoryAnalyze.Size = new System.Drawing.Size(147, 35);
            this.trajectoryAnalyze.TabIndex = 0;
            this.trajectoryAnalyze.Text = "Trajectory Analyze";
            this.trajectoryAnalyze.UseVisualStyleBackColor = true;
            this.trajectoryAnalyze.Click += new System.EventHandler(this.trajectoryAnalyze_Click);
            // 
            // stepsToRemove
            // 
            this.stepsToRemove.AutoSize = true;
            this.stepsToRemove.Location = new System.Drawing.Point(12, 61);
            this.stepsToRemove.Name = "stepsToRemove";
            this.stepsToRemove.Size = new System.Drawing.Size(89, 13);
            this.stepsToRemove.TabIndex = 1;
            this.stepsToRemove.Text = "Steps to Remove";
            // 
            // stepsToRemoveTxt
            // 
            this.stepsToRemoveTxt.Location = new System.Drawing.Point(15, 77);
            this.stepsToRemoveTxt.Name = "stepsToRemoveTxt";
            this.stepsToRemoveTxt.Size = new System.Drawing.Size(118, 20);
            this.stepsToRemoveTxt.TabIndex = 2;
            this.stepsToRemoveTxt.Text = "0";
            this.stepsToRemoveTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.stepsToRemoveTxt.Leave += new System.EventHandler(this.stepsToRemoveTxt_Leave);
            // 
            // modelName
            // 
            this.modelName.AutoSize = true;
            this.modelName.Location = new System.Drawing.Point(12, 9);
            this.modelName.Name = "modelName";
            this.modelName.Size = new System.Drawing.Size(67, 13);
            this.modelName.TabIndex = 3;
            this.modelName.Text = "Model Name";
            // 
            // modelNameTxt
            // 
            this.modelNameTxt.Location = new System.Drawing.Point(12, 25);
            this.modelNameTxt.Name = "modelNameTxt";
            this.modelNameTxt.ReadOnly = true;
            this.modelNameTxt.Size = new System.Drawing.Size(121, 20);
            this.modelNameTxt.TabIndex = 4;
            this.modelNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtendedAnalyze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 122);
            this.Controls.Add(this.modelNameTxt);
            this.Controls.Add(this.modelName);
            this.Controls.Add(this.stepsToRemoveTxt);
            this.Controls.Add(this.stepsToRemove);
            this.Controls.Add(this.trajectoryAnalyze);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendedAnalyze";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extended Analyze";
            this.Load += new System.EventHandler(this.ExtendedAnalyze_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button trajectoryAnalyze;
        private System.Windows.Forms.Label stepsToRemove;
        private System.Windows.Forms.TextBox stepsToRemoveTxt;
        private System.Windows.Forms.Label modelName;
        private System.Windows.Forms.TextBox modelNameTxt;
    }
}