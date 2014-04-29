namespace RandomNetworksExplorer
{
    partial class StorageSettingsWindow
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.xmlBrowseButton = new System.Windows.Forms.Button();
            this.sqlRadioButton = new System.Windows.Forms.RadioButton();
            this.connectionsButton = new System.Windows.Forms.Button();
            this.connectionStrTxt = new System.Windows.Forms.TextBox();
            this.xmlRadioButton = new System.Windows.Forms.RadioButton();
            this.xmlOutputDirectoryTxt = new System.Windows.Forms.TextBox();
            this.connectionStr = new System.Windows.Forms.Label();
            this.xmlOutputDirectory = new System.Windows.Forms.Label();
            this.txtBrowseButton = new System.Windows.Forms.Button();
            this.txtRadioButton = new System.Windows.Forms.RadioButton();
            this.txtOutputDirectoryTxt = new System.Windows.Forms.TextBox();
            this.txtOutputDirectory = new System.Windows.Forms.Label();
            this.browserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(538, 194);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 19;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(457, 194);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 18;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // xmlBrowseButton
            // 
            this.xmlBrowseButton.Location = new System.Drawing.Point(531, 35);
            this.xmlBrowseButton.Name = "xmlBrowseButton";
            this.xmlBrowseButton.Size = new System.Drawing.Size(82, 23);
            this.xmlBrowseButton.TabIndex = 30;
            this.xmlBrowseButton.Text = "Browse";
            this.xmlBrowseButton.UseVisualStyleBackColor = true;
            this.xmlBrowseButton.Click += new System.EventHandler(this.xmlBrowseButton_Click);
            // 
            // sqlRadioButton
            // 
            this.sqlRadioButton.AutoSize = true;
            this.sqlRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sqlRadioButton.Location = new System.Drawing.Point(12, 130);
            this.sqlRadioButton.Name = "sqlRadioButton";
            this.sqlRadioButton.Size = new System.Drawing.Size(72, 17);
            this.sqlRadioButton.TabIndex = 31;
            this.sqlRadioButton.TabStop = true;
            this.sqlRadioButton.Text = "SQL store";
            this.sqlRadioButton.UseVisualStyleBackColor = true;
            this.sqlRadioButton.CheckedChanged += new System.EventHandler(this.store_checkedChanged);
            // 
            // connectionsButton
            // 
            this.connectionsButton.Location = new System.Drawing.Point(531, 154);
            this.connectionsButton.Name = "connectionsButton";
            this.connectionsButton.Size = new System.Drawing.Size(82, 23);
            this.connectionsButton.TabIndex = 32;
            this.connectionsButton.Text = "Connections";
            this.connectionsButton.UseVisualStyleBackColor = true;
            this.connectionsButton.Click += new System.EventHandler(this.connectionsButton_Click);
            // 
            // connectionStrTxt
            // 
            this.connectionStrTxt.Location = new System.Drawing.Point(137, 157);
            this.connectionStrTxt.Name = "connectionStrTxt";
            this.connectionStrTxt.Size = new System.Drawing.Size(388, 20);
            this.connectionStrTxt.TabIndex = 34;
            // 
            // xmlRadioButton
            // 
            this.xmlRadioButton.AutoSize = true;
            this.xmlRadioButton.Location = new System.Drawing.Point(12, 12);
            this.xmlRadioButton.Name = "xmlRadioButton";
            this.xmlRadioButton.Size = new System.Drawing.Size(73, 17);
            this.xmlRadioButton.TabIndex = 27;
            this.xmlRadioButton.Text = "XML store";
            this.xmlRadioButton.UseVisualStyleBackColor = true;
            this.xmlRadioButton.CheckedChanged += new System.EventHandler(this.store_checkedChanged);
            // 
            // xmlOutputDirectoryTxt
            // 
            this.xmlOutputDirectoryTxt.Location = new System.Drawing.Point(137, 38);
            this.xmlOutputDirectoryTxt.Name = "xmlOutputDirectoryTxt";
            this.xmlOutputDirectoryTxt.Size = new System.Drawing.Size(388, 20);
            this.xmlOutputDirectoryTxt.TabIndex = 29;
            // 
            // connectionStr
            // 
            this.connectionStr.AutoSize = true;
            this.connectionStr.Location = new System.Drawing.Point(32, 161);
            this.connectionStr.Name = "connectionStr";
            this.connectionStr.Size = new System.Drawing.Size(88, 13);
            this.connectionStr.TabIndex = 33;
            this.connectionStr.Text = "ConnectionString";
            // 
            // xmlOutputDirectory
            // 
            this.xmlOutputDirectory.AutoSize = true;
            this.xmlOutputDirectory.Location = new System.Drawing.Point(33, 42);
            this.xmlOutputDirectory.Name = "xmlOutputDirectory";
            this.xmlOutputDirectory.Size = new System.Drawing.Size(84, 13);
            this.xmlOutputDirectory.TabIndex = 28;
            this.xmlOutputDirectory.Text = "Output Directory";
            // 
            // txtBrowseButton
            // 
            this.txtBrowseButton.Location = new System.Drawing.Point(531, 94);
            this.txtBrowseButton.Name = "txtBrowseButton";
            this.txtBrowseButton.Size = new System.Drawing.Size(82, 23);
            this.txtBrowseButton.TabIndex = 38;
            this.txtBrowseButton.Text = "Browse";
            this.txtBrowseButton.UseVisualStyleBackColor = true;
            this.txtBrowseButton.Click += new System.EventHandler(this.txtBrowseButton_Click);
            // 
            // txtRadioButton
            // 
            this.txtRadioButton.AutoSize = true;
            this.txtRadioButton.Location = new System.Drawing.Point(12, 71);
            this.txtRadioButton.Name = "txtRadioButton";
            this.txtRadioButton.Size = new System.Drawing.Size(72, 17);
            this.txtRadioButton.TabIndex = 35;
            this.txtRadioButton.Text = "TXT store";
            this.txtRadioButton.UseVisualStyleBackColor = true;
            this.txtRadioButton.CheckedChanged += new System.EventHandler(this.store_checkedChanged);
            // 
            // txtOutputDirectoryTxt
            // 
            this.txtOutputDirectoryTxt.Location = new System.Drawing.Point(137, 97);
            this.txtOutputDirectoryTxt.Name = "txtOutputDirectoryTxt";
            this.txtOutputDirectoryTxt.Size = new System.Drawing.Size(388, 20);
            this.txtOutputDirectoryTxt.TabIndex = 37;
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.AutoSize = true;
            this.txtOutputDirectory.Location = new System.Drawing.Point(33, 101);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(84, 13);
            this.txtOutputDirectory.TabIndex = 36;
            this.txtOutputDirectory.Text = "Storage location";
            // 
            // StorageSettingsWindow
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(624, 226);
            this.Controls.Add(this.txtBrowseButton);
            this.Controls.Add(this.txtRadioButton);
            this.Controls.Add(this.txtOutputDirectoryTxt);
            this.Controls.Add(this.txtOutputDirectory);
            this.Controls.Add(this.xmlBrowseButton);
            this.Controls.Add(this.sqlRadioButton);
            this.Controls.Add(this.connectionsButton);
            this.Controls.Add(this.connectionStrTxt);
            this.Controls.Add(this.xmlRadioButton);
            this.Controls.Add(this.xmlOutputDirectoryTxt);
            this.Controls.Add(this.connectionStr);
            this.Controls.Add(this.xmlOutputDirectory);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StorageSettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Storage Settings";
            this.Load += new System.EventHandler(this.StorageSettingsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button xmlBrowseButton;
        private System.Windows.Forms.RadioButton sqlRadioButton;
        private System.Windows.Forms.Button connectionsButton;
        private System.Windows.Forms.TextBox connectionStrTxt;
        private System.Windows.Forms.RadioButton xmlRadioButton;
        private System.Windows.Forms.TextBox xmlOutputDirectoryTxt;
        private System.Windows.Forms.Label connectionStr;
        private System.Windows.Forms.Label xmlOutputDirectory;
        private System.Windows.Forms.Button txtBrowseButton;
        private System.Windows.Forms.RadioButton txtRadioButton;
        private System.Windows.Forms.TextBox txtOutputDirectoryTxt;
        private System.Windows.Forms.Label txtOutputDirectory;
        private System.Windows.Forms.FolderBrowserDialog browserDlg;
    }
}