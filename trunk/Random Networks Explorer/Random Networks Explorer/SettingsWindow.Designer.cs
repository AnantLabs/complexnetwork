namespace RandomNetworksExplorer
{
    partial class SettingsWindow
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
            this.settingsTab = new System.Windows.Forms.TabControl();
            this.generalPage = new System.Windows.Forms.TabPage();
            this.tracingSettingsGrp = new System.Windows.Forms.GroupBox();
            this.tracingDirectory = new System.Windows.Forms.Label();
            this.tracingDirectoryTxt = new System.Windows.Forms.TextBox();
            this.tracingBrowseBtn = new System.Windows.Forms.Button();
            this.loggingSettingsGrp = new System.Windows.Forms.GroupBox();
            this.loggingDirectory = new System.Windows.Forms.Label();
            this.loggingDirectoryTxt = new System.Windows.Forms.TextBox();
            this.loggingBrowseButton = new System.Windows.Forms.Button();
            this.onlyDebugCheckBox = new System.Windows.Forms.CheckBox();
            this.storageSettingsGrp = new System.Windows.Forms.GroupBox();
            this.databaseBrowseButton = new System.Windows.Forms.Button();
            this.storageBrowseButton = new System.Windows.Forms.Button();
            this.databaseTxt = new System.Windows.Forms.TextBox();
            this.storageDirectoryTxt = new System.Windows.Forms.TextBox();
            this.database = new System.Windows.Forms.Label();
            this.storageDirectory = new System.Windows.Forms.Label();
            this.workingModePage = new System.Windows.Forms.TabPage();
            this.distributingSettingsGrp = new System.Windows.Forms.GroupBox();
            this.managerTypeCmb = new System.Windows.Forms.ComboBox();
            this.distributedLabel = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.discoveredServices = new System.Windows.Forms.CheckedListBox();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.CancelSettingsButton = new System.Windows.Forms.Button();
            this.browserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.settingsTab.SuspendLayout();
            this.generalPage.SuspendLayout();
            this.tracingSettingsGrp.SuspendLayout();
            this.loggingSettingsGrp.SuspendLayout();
            this.storageSettingsGrp.SuspendLayout();
            this.workingModePage.SuspendLayout();
            this.distributingSettingsGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.generalPage);
            this.settingsTab.Controls.Add(this.workingModePage);
            this.settingsTab.Location = new System.Drawing.Point(12, 12);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.SelectedIndex = 0;
            this.settingsTab.Size = new System.Drawing.Size(610, 289);
            this.settingsTab.TabIndex = 1;
            // 
            // generalPage
            // 
            this.generalPage.Controls.Add(this.tracingSettingsGrp);
            this.generalPage.Controls.Add(this.loggingSettingsGrp);
            this.generalPage.Controls.Add(this.storageSettingsGrp);
            this.generalPage.Location = new System.Drawing.Point(4, 22);
            this.generalPage.Name = "generalPage";
            this.generalPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalPage.Size = new System.Drawing.Size(602, 263);
            this.generalPage.TabIndex = 0;
            this.generalPage.Text = "General";
            this.generalPage.UseVisualStyleBackColor = true;
            // 
            // tracingSettingsGrp
            // 
            this.tracingSettingsGrp.Controls.Add(this.tracingDirectory);
            this.tracingSettingsGrp.Controls.Add(this.tracingDirectoryTxt);
            this.tracingSettingsGrp.Controls.Add(this.tracingBrowseBtn);
            this.tracingSettingsGrp.Location = new System.Drawing.Point(6, 180);
            this.tracingSettingsGrp.Name = "tracingSettingsGrp";
            this.tracingSettingsGrp.Size = new System.Drawing.Size(590, 69);
            this.tracingSettingsGrp.TabIndex = 4;
            this.tracingSettingsGrp.TabStop = false;
            this.tracingSettingsGrp.Text = "Tracing";
            // 
            // tracingDirectory
            // 
            this.tracingDirectory.AutoSize = true;
            this.tracingDirectory.Location = new System.Drawing.Point(24, 31);
            this.tracingDirectory.Name = "tracingDirectory";
            this.tracingDirectory.Size = new System.Drawing.Size(85, 13);
            this.tracingDirectory.TabIndex = 1;
            this.tracingDirectory.Text = "Output directory:";
            // 
            // tracingDirectoryTxt
            // 
            this.tracingDirectoryTxt.Location = new System.Drawing.Point(115, 28);
            this.tracingDirectoryTxt.Name = "tracingDirectoryTxt";
            this.tracingDirectoryTxt.Size = new System.Drawing.Size(388, 20);
            this.tracingDirectoryTxt.TabIndex = 1;
            // 
            // tracingBrowseBtn
            // 
            this.tracingBrowseBtn.Location = new System.Drawing.Point(509, 25);
            this.tracingBrowseBtn.Name = "tracingBrowseBtn";
            this.tracingBrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.tracingBrowseBtn.TabIndex = 2;
            this.tracingBrowseBtn.Text = "Browse...";
            this.tracingBrowseBtn.UseVisualStyleBackColor = true;
            this.tracingBrowseBtn.Click += new System.EventHandler(this.tracingBrowseBtn_Click);
            // 
            // loggingSettingsGrp
            // 
            this.loggingSettingsGrp.Controls.Add(this.loggingDirectory);
            this.loggingSettingsGrp.Controls.Add(this.loggingDirectoryTxt);
            this.loggingSettingsGrp.Controls.Add(this.loggingBrowseButton);
            this.loggingSettingsGrp.Controls.Add(this.onlyDebugCheckBox);
            this.loggingSettingsGrp.Location = new System.Drawing.Point(6, 6);
            this.loggingSettingsGrp.Name = "loggingSettingsGrp";
            this.loggingSettingsGrp.Size = new System.Drawing.Size(590, 83);
            this.loggingSettingsGrp.TabIndex = 2;
            this.loggingSettingsGrp.TabStop = false;
            this.loggingSettingsGrp.Text = "Logging";
            // 
            // loggingDirectory
            // 
            this.loggingDirectory.AutoSize = true;
            this.loggingDirectory.Location = new System.Drawing.Point(24, 44);
            this.loggingDirectory.Name = "loggingDirectory";
            this.loggingDirectory.Size = new System.Drawing.Size(85, 13);
            this.loggingDirectory.TabIndex = 1;
            this.loggingDirectory.Text = "Output directory:";
            // 
            // loggingDirectoryTxt
            // 
            this.loggingDirectoryTxt.Location = new System.Drawing.Point(115, 41);
            this.loggingDirectoryTxt.Name = "loggingDirectoryTxt";
            this.loggingDirectoryTxt.Size = new System.Drawing.Size(388, 20);
            this.loggingDirectoryTxt.TabIndex = 1;
            // 
            // loggingBrowseButton
            // 
            this.loggingBrowseButton.Location = new System.Drawing.Point(509, 38);
            this.loggingBrowseButton.Name = "loggingBrowseButton";
            this.loggingBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.loggingBrowseButton.TabIndex = 2;
            this.loggingBrowseButton.Text = "Browse...";
            this.loggingBrowseButton.UseVisualStyleBackColor = true;
            this.loggingBrowseButton.Click += new System.EventHandler(this.loggingBrowseButton_Click);
            // 
            // onlyDebugCheckBox
            // 
            this.onlyDebugCheckBox.AutoSize = true;
            this.onlyDebugCheckBox.Location = new System.Drawing.Point(115, 19);
            this.onlyDebugCheckBox.Name = "onlyDebugCheckBox";
            this.onlyDebugCheckBox.Size = new System.Drawing.Size(149, 17);
            this.onlyDebugCheckBox.TabIndex = 0;
            this.onlyDebugCheckBox.Text = "Log only debug messages";
            this.onlyDebugCheckBox.UseVisualStyleBackColor = true;
            // 
            // storageSettingsGrp
            // 
            this.storageSettingsGrp.Controls.Add(this.databaseBrowseButton);
            this.storageSettingsGrp.Controls.Add(this.storageBrowseButton);
            this.storageSettingsGrp.Controls.Add(this.databaseTxt);
            this.storageSettingsGrp.Controls.Add(this.storageDirectoryTxt);
            this.storageSettingsGrp.Controls.Add(this.database);
            this.storageSettingsGrp.Controls.Add(this.storageDirectory);
            this.storageSettingsGrp.Location = new System.Drawing.Point(6, 95);
            this.storageSettingsGrp.Name = "storageSettingsGrp";
            this.storageSettingsGrp.Size = new System.Drawing.Size(590, 79);
            this.storageSettingsGrp.TabIndex = 3;
            this.storageSettingsGrp.TabStop = false;
            this.storageSettingsGrp.Text = "Default storage locations";
            // 
            // databaseBrowseButton
            // 
            this.databaseBrowseButton.Location = new System.Drawing.Point(509, 48);
            this.databaseBrowseButton.Name = "databaseBrowseButton";
            this.databaseBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.databaseBrowseButton.TabIndex = 3;
            this.databaseBrowseButton.Text = "Browse...";
            this.databaseBrowseButton.UseVisualStyleBackColor = true;
            // 
            // storageBrowseButton
            // 
            this.storageBrowseButton.Location = new System.Drawing.Point(509, 21);
            this.storageBrowseButton.Name = "storageBrowseButton";
            this.storageBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.storageBrowseButton.TabIndex = 1;
            this.storageBrowseButton.Text = "Browse...";
            this.storageBrowseButton.UseVisualStyleBackColor = true;
            this.storageBrowseButton.Click += new System.EventHandler(this.storageBrowseButton_Click);
            // 
            // databaseTxt
            // 
            this.databaseTxt.Location = new System.Drawing.Point(115, 50);
            this.databaseTxt.Name = "databaseTxt";
            this.databaseTxt.Size = new System.Drawing.Size(388, 20);
            this.databaseTxt.TabIndex = 2;
            // 
            // storageDirectoryTxt
            // 
            this.storageDirectoryTxt.Location = new System.Drawing.Point(115, 24);
            this.storageDirectoryTxt.Name = "storageDirectoryTxt";
            this.storageDirectoryTxt.Size = new System.Drawing.Size(388, 20);
            this.storageDirectoryTxt.TabIndex = 0;
            // 
            // database
            // 
            this.database.AutoSize = true;
            this.database.Location = new System.Drawing.Point(52, 53);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(56, 13);
            this.database.TabIndex = 1;
            this.database.Text = "Database:";
            // 
            // storageDirectory
            // 
            this.storageDirectory.AutoSize = true;
            this.storageDirectory.Location = new System.Drawing.Point(39, 27);
            this.storageDirectory.Name = "storageDirectory";
            this.storageDirectory.Size = new System.Drawing.Size(69, 13);
            this.storageDirectory.TabIndex = 0;
            this.storageDirectory.Text = "File directory:";
            // 
            // workingModePage
            // 
            this.workingModePage.Controls.Add(this.distributingSettingsGrp);
            this.workingModePage.Location = new System.Drawing.Point(4, 22);
            this.workingModePage.Name = "workingModePage";
            this.workingModePage.Padding = new System.Windows.Forms.Padding(3);
            this.workingModePage.Size = new System.Drawing.Size(602, 263);
            this.workingModePage.TabIndex = 1;
            this.workingModePage.Text = "Working Mode";
            this.workingModePage.UseVisualStyleBackColor = true;
            // 
            // distributingSettingsGrp
            // 
            this.distributingSettingsGrp.Controls.Add(this.managerTypeCmb);
            this.distributingSettingsGrp.Controls.Add(this.distributedLabel);
            this.distributingSettingsGrp.Controls.Add(this.RefreshButton);
            this.distributingSettingsGrp.Controls.Add(this.discoveredServices);
            this.distributingSettingsGrp.Location = new System.Drawing.Point(6, 6);
            this.distributingSettingsGrp.Name = "distributingSettingsGrp";
            this.distributingSettingsGrp.Size = new System.Drawing.Size(590, 251);
            this.distributingSettingsGrp.TabIndex = 12;
            this.distributingSettingsGrp.TabStop = false;
            this.distributingSettingsGrp.Text = "Distributing";
            // 
            // managerTypeCmb
            // 
            this.managerTypeCmb.FormattingEnabled = true;
            this.managerTypeCmb.Location = new System.Drawing.Point(23, 28);
            this.managerTypeCmb.Name = "managerTypeCmb";
            this.managerTypeCmb.Size = new System.Drawing.Size(121, 21);
            this.managerTypeCmb.TabIndex = 11;
            this.managerTypeCmb.SelectedIndexChanged += new System.EventHandler(this.managerTypeCmb_SelectedIndexChanged);
            // 
            // distributedLabel
            // 
            this.distributedLabel.Location = new System.Drawing.Point(212, 31);
            this.distributedLabel.Name = "distributedLabel";
            this.distributedLabel.Size = new System.Drawing.Size(372, 20);
            this.distributedLabel.TabIndex = 7;
            this.distributedLabel.Text = "Please select computers which will be used during distributed calculation.";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(23, 54);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 10;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // discoveredServices
            // 
            this.discoveredServices.BackColor = System.Drawing.SystemColors.Control;
            this.discoveredServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.discoveredServices.FormattingEnabled = true;
            this.discoveredServices.Location = new System.Drawing.Point(215, 54);
            this.discoveredServices.Name = "discoveredServices";
            this.discoveredServices.Size = new System.Drawing.Size(369, 180);
            this.discoveredServices.TabIndex = 8;
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(462, 307);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSettingsButton.TabIndex = 3;
            this.SaveSettingsButton.Text = "Save";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // CancelSettingsButton
            // 
            this.CancelSettingsButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelSettingsButton.Location = new System.Drawing.Point(543, 307);
            this.CancelSettingsButton.Name = "CancelSettingsButton";
            this.CancelSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.CancelSettingsButton.TabIndex = 4;
            this.CancelSettingsButton.Text = "Cancel";
            this.CancelSettingsButton.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.AcceptButton = this.SaveSettingsButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelSettingsButton;
            this.ClientSize = new System.Drawing.Size(634, 337);
            this.Controls.Add(this.SaveSettingsButton);
            this.Controls.Add(this.CancelSettingsButton);
            this.Controls.Add(this.settingsTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.settingsTab.ResumeLayout(false);
            this.generalPage.ResumeLayout(false);
            this.tracingSettingsGrp.ResumeLayout(false);
            this.tracingSettingsGrp.PerformLayout();
            this.loggingSettingsGrp.ResumeLayout(false);
            this.loggingSettingsGrp.PerformLayout();
            this.storageSettingsGrp.ResumeLayout(false);
            this.storageSettingsGrp.PerformLayout();
            this.workingModePage.ResumeLayout(false);
            this.distributingSettingsGrp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingsTab;
        private System.Windows.Forms.TabPage generalPage;
        private System.Windows.Forms.GroupBox tracingSettingsGrp;
        private System.Windows.Forms.Label tracingDirectory;
        private System.Windows.Forms.TextBox tracingDirectoryTxt;
        private System.Windows.Forms.Button tracingBrowseBtn;
        private System.Windows.Forms.GroupBox loggingSettingsGrp;
        private System.Windows.Forms.Label loggingDirectory;
        private System.Windows.Forms.TextBox loggingDirectoryTxt;
        private System.Windows.Forms.Button loggingBrowseButton;
        private System.Windows.Forms.CheckBox onlyDebugCheckBox;
        private System.Windows.Forms.GroupBox storageSettingsGrp;
        private System.Windows.Forms.Button databaseBrowseButton;
        private System.Windows.Forms.Button storageBrowseButton;
        private System.Windows.Forms.TextBox databaseTxt;
        private System.Windows.Forms.TextBox storageDirectoryTxt;
        private System.Windows.Forms.Label database;
        private System.Windows.Forms.Label storageDirectory;
        private System.Windows.Forms.TabPage workingModePage;
        private System.Windows.Forms.GroupBox distributingSettingsGrp;
        private System.Windows.Forms.Label distributedLabel;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.CheckedListBox discoveredServices;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.Button CancelSettingsButton;
        private System.Windows.Forms.ComboBox managerTypeCmb;
        private System.Windows.Forms.FolderBrowserDialog browserDlg;

    }
}