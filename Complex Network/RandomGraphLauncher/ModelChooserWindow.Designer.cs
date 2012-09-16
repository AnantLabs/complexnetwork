namespace RandomGraphLauncher
{
    partial class modelChooserWindow
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
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.jobName = new System.Windows.Forms.Label();
            this.model = new System.Windows.Forms.Label();
            this.modelCmb = new System.Windows.Forms.ComboBox();
            this.jobNameTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(75, 112);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 0;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.OK_ButtonClick);
            // 
            // button_Cancel
            // 
            this.button_Cancel.CausesValidation = false;
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(180, 112);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // jobName
            // 
            this.jobName.AutoSize = true;
            this.jobName.Location = new System.Drawing.Point(16, 25);
            this.jobName.Name = "jobName";
            this.jobName.Size = new System.Drawing.Size(55, 13);
            this.jobName.TabIndex = 2;
            this.jobName.Text = "Job Name";
            // 
            // model
            // 
            this.model.AutoSize = true;
            this.model.Location = new System.Drawing.Point(33, 63);
            this.model.Name = "model";
            this.model.Size = new System.Drawing.Size(36, 13);
            this.model.TabIndex = 3;
            this.model.Text = "Model";
            // 
            // modelCmb
            // 
            this.modelCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modelCmb.Location = new System.Drawing.Point(75, 60);
            this.modelCmb.Name = "modelCmb";
            this.modelCmb.Size = new System.Drawing.Size(180, 21);
            this.modelCmb.TabIndex = 4;
            // 
            // jobNameTxt
            // 
            this.jobNameTxt.Location = new System.Drawing.Point(75, 22);
            this.jobNameTxt.Name = "jobNameTxt";
            this.jobNameTxt.Size = new System.Drawing.Size(180, 20);
            this.jobNameTxt.TabIndex = 1;
            // 
            // modelChooserWindow
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(304, 148);
            this.Controls.Add(this.jobNameTxt);
            this.Controls.Add(this.modelCmb);
            this.Controls.Add(this.model);
            this.Controls.Add(this.jobName);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "modelChooserWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Model";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label jobName;
        private System.Windows.Forms.Label model;
        public System.Windows.Forms.ComboBox modelCmb;
        public System.Windows.Forms.TextBox jobNameTxt;
    }
}