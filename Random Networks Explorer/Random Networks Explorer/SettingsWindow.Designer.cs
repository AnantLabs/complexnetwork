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
            this.workingModePage = new System.Windows.Forms.TabPage();
            this.loggingSettingsGroup = new System.Windows.Forms.GroupBox();
            this.DirectoryLabel = new System.Windows.Forms.Label();
            this.directoryPathTxt = new System.Windows.Forms.TextBox();
            this.BrowseLogDirButton = new System.Windows.Forms.Button();
            this.LoggingCheckBox = new System.Windows.Forms.CheckBox();
            this.StorageSettingsGroup = new System.Windows.Forms.GroupBox();
            this.BrowseDatabaseButton = new System.Windows.Forms.Button();
            this.BrowseFileStorageButton = new System.Windows.Forms.Button();
            this.databaseTxt = new System.Windows.Forms.TextBox();
            this.textStorageTxt = new System.Windows.Forms.TextBox();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.StorageDirLabel = new System.Windows.Forms.Label();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.CancelSettingsButton = new System.Windows.Forms.Button();
            this.tracingSettingsGrp = new System.Windows.Forms.GroupBox();
            this.tracingDirectory = new System.Windows.Forms.Label();
            this.tracingDirectoryTxt = new System.Windows.Forms.TextBox();
            this.browseTracingBtn = new System.Windows.Forms.Button();
            this.distributingSettingsGrp = new System.Windows.Forms.GroupBox();
            this.distributedLabel = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.discoveredServices = new System.Windows.Forms.CheckedListBox();
            this.distributedCheckBox = new System.Windows.Forms.CheckBox();
            this.settingsTab.SuspendLayout();
            this.generalPage.SuspendLayout();
            this.workingModePage.SuspendLayout();
            this.loggingSettingsGroup.SuspendLayout();
            this.StorageSettingsGroup.SuspendLayout();
            this.tracingSettingsGrp.SuspendLayout();
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
            this.generalPage.Controls.Add(this.loggingSettingsGroup);
            this.generalPage.Controls.Add(this.StorageSettingsGroup);
            this.generalPage.Location = new System.Drawing.Point(4, 22);
            this.generalPage.Name = "generalPage";
            this.generalPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalPage.Size = new System.Drawing.Size(602, 263);
            this.generalPage.TabIndex = 0;
            this.generalPage.Text = "General";
            this.generalPage.UseVisualStyleBackColor = true;
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
            // loggingSettingsGroup
            // 
            this.loggingSettingsGroup.Controls.Add(this.DirectoryLabel);
            this.loggingSettingsGroup.Controls.Add(this.directoryPathTxt);
            this.loggingSettingsGroup.Controls.Add(this.BrowseLogDirButton);
            this.loggingSettingsGroup.Controls.Add(this.LoggingCheckBox);
            this.loggingSettingsGroup.Location = new System.Drawing.Point(6, 6);
            this.loggingSettingsGroup.Name = "loggingSettingsGroup";
            this.loggingSettingsGroup.Size = new System.Drawing.Size(590, 83);
            this.loggingSettingsGroup.TabIndex = 2;
            this.loggingSettingsGroup.TabStop = false;
            this.loggingSettingsGroup.Text = "Logging";
            // 
            // DirectoryLabel
            // 
            this.DirectoryLabel.AutoSize = true;
            this.DirectoryLabel.Location = new System.Drawing.Point(24, 44);
            this.DirectoryLabel.Name = "DirectoryLabel";
            this.DirectoryLabel.Size = new System.Drawing.Size(85, 13);
            this.DirectoryLabel.TabIndex = 1;
            this.DirectoryLabel.Text = "Output directory:";
            // 
            // directoryPathTxt
            // 
            this.directoryPathTxt.Location = new System.Drawing.Point(115, 41);
            this.directoryPathTxt.Name = "directoryPathTxt";
            this.directoryPathTxt.Size = new System.Drawing.Size(388, 20);
            this.directoryPathTxt.TabIndex = 1;
            // 
            // BrowseLogDirButton
            // 
            this.BrowseLogDirButton.Location = new System.Drawing.Point(509, 38);
            this.BrowseLogDirButton.Name = "BrowseLogDirButton";
            this.BrowseLogDirButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseLogDirButton.TabIndex = 2;
            this.BrowseLogDirButton.Text = "Browse...";
            this.BrowseLogDirButton.UseVisualStyleBackColor = true;
            this.BrowseLogDirButton.Click += new System.EventHandler(this.BrowseLogDirButton_Click);
            // 
            // LoggingCheckBox
            // 
            this.LoggingCheckBox.AutoSize = true;
            this.LoggingCheckBox.Location = new System.Drawing.Point(115, 19);
            this.LoggingCheckBox.Name = "LoggingCheckBox";
            this.LoggingCheckBox.Size = new System.Drawing.Size(96, 17);
            this.LoggingCheckBox.TabIndex = 0;
            this.LoggingCheckBox.Text = "Enable logging";
            this.LoggingCheckBox.UseVisualStyleBackColor = true;
            // 
            // StorageSettingsGroup
            // 
            this.StorageSettingsGroup.Controls.Add(this.BrowseDatabaseButton);
            this.StorageSettingsGroup.Controls.Add(this.BrowseFileStorageButton);
            this.StorageSettingsGroup.Controls.Add(this.databaseTxt);
            this.StorageSettingsGroup.Controls.Add(this.textStorageTxt);
            this.StorageSettingsGroup.Controls.Add(this.DatabaseLabel);
            this.StorageSettingsGroup.Controls.Add(this.StorageDirLabel);
            this.StorageSettingsGroup.Location = new System.Drawing.Point(6, 95);
            this.StorageSettingsGroup.Name = "StorageSettingsGroup";
            this.StorageSettingsGroup.Size = new System.Drawing.Size(590, 79);
            this.StorageSettingsGroup.TabIndex = 3;
            this.StorageSettingsGroup.TabStop = false;
            this.StorageSettingsGroup.Text = "Default storage locations";
            // 
            // BrowseDatabaseButton
            // 
            this.BrowseDatabaseButton.Location = new System.Drawing.Point(509, 48);
            this.BrowseDatabaseButton.Name = "BrowseDatabaseButton";
            this.BrowseDatabaseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseDatabaseButton.TabIndex = 3;
            this.BrowseDatabaseButton.Text = "Browse...";
            this.BrowseDatabaseButton.UseVisualStyleBackColor = true;
            this.BrowseDatabaseButton.Click += new System.EventHandler(this.BrowseDatabaseButton_Click);
            // 
            // BrowseFileStorageButton
            // 
            this.BrowseFileStorageButton.Location = new System.Drawing.Point(509, 21);
            this.BrowseFileStorageButton.Name = "BrowseFileStorageButton";
            this.BrowseFileStorageButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseFileStorageButton.TabIndex = 1;
            this.BrowseFileStorageButton.Text = "Browse...";
            this.BrowseFileStorageButton.UseVisualStyleBackColor = true;
            this.BrowseFileStorageButton.Click += new System.EventHandler(this.BrowseFileStorageButton_Click);
            // 
            // databaseTxt
            // 
            this.databaseTxt.Location = new System.Drawing.Point(115, 50);
            this.databaseTxt.Name = "databaseTxt";
            this.databaseTxt.Size = new System.Drawing.Size(388, 20);
            this.databaseTxt.TabIndex = 2;
            // 
            // textStorageTxt
            // 
            this.textStorageTxt.Location = new System.Drawing.Point(115, 24);
            this.textStorageTxt.Name = "textStorageTxt";
            this.textStorageTxt.Size = new System.Drawing.Size(388, 20);
            this.textStorageTxt.TabIndex = 0;
            // 
            // DatabaseLabel
            // 
            this.DatabaseLabel.AutoSize = true;
            this.DatabaseLabel.Location = new System.Drawing.Point(52, 53);
            this.DatabaseLabel.Name = "DatabaseLabel";
            this.DatabaseLabel.Size = new System.Drawing.Size(56, 13);
            this.DatabaseLabel.TabIndex = 1;
            this.DatabaseLabel.Text = "Database:";
            // 
            // StorageDirLabel
            // 
            this.StorageDirLabel.AutoSize = true;
            this.StorageDirLabel.Location = new System.Drawing.Point(39, 27);
            this.StorageDirLabel.Name = "StorageDirLabel";
            this.StorageDirLabel.Size = new System.Drawing.Size(69, 13);
            this.StorageDirLabel.TabIndex = 0;
            this.StorageDirLabel.Text = "File directory:";
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
            this.CancelSettingsButton.Location = new System.Drawing.Point(543, 307);
            this.CancelSettingsButton.Name = "CancelSettingsButton";
            this.CancelSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.CancelSettingsButton.TabIndex = 4;
            this.CancelSettingsButton.Text = "Cancel";
            this.CancelSettingsButton.UseVisualStyleBackColor = true;
            this.CancelSettingsButton.Click += new System.EventHandler(this.CancelSettingsButton_Click);
            // 
            // tracingSettingsGrp
            // 
            this.tracingSettingsGrp.Controls.Add(this.tracingDirectory);
            this.tracingSettingsGrp.Controls.Add(this.tracingDirectoryTxt);
            this.tracingSettingsGrp.Controls.Add(this.browseTracingBtn);
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
            // browseTracingBtn
            // 
            this.browseTracingBtn.Location = new System.Drawing.Point(509, 25);
            this.browseTracingBtn.Name = "browseTracingBtn";
            this.browseTracingBtn.Size = new System.Drawing.Size(75, 23);
            this.browseTracingBtn.TabIndex = 2;
            this.browseTracingBtn.Text = "Browse...";
            this.browseTracingBtn.UseVisualStyleBackColor = true;
            this.browseTracingBtn.Click += new System.EventHandler(this.browseTracingBtn_Click);
            // 
            // distributingSettingsGrp
            // 
            this.distributingSettingsGrp.Controls.Add(this.distributedLabel);
            this.distributingSettingsGrp.Controls.Add(this.RefreshButton);
            this.distributingSettingsGrp.Controls.Add(this.discoveredServices);
            this.distributingSettingsGrp.Controls.Add(this.distributedCheckBox);
            this.distributingSettingsGrp.Location = new System.Drawing.Point(6, 6);
            this.distributingSettingsGrp.Name = "distributingSettingsGrp";
            this.distributingSettingsGrp.Size = new System.Drawing.Size(590, 251);
            this.distributingSettingsGrp.TabIndex = 12;
            this.distributingSettingsGrp.TabStop = false;
            this.distributingSettingsGrp.Text = "Distributing";
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
            // distributedCheckBox
            // 
            this.distributedCheckBox.AutoSize = true;
            this.distributedCheckBox.Location = new System.Drawing.Point(23, 30);
            this.distributedCheckBox.Name = "distributedCheckBox";
            this.distributedCheckBox.Size = new System.Drawing.Size(156, 17);
            this.distributedCheckBox.TabIndex = 9;
            this.distributedCheckBox.Text = "Use distributed calculation?";
            this.distributedCheckBox.UseVisualStyleBackColor = true;
            this.distributedCheckBox.CheckedChanged += new System.EventHandler(this.distributedCheckBox_CheckedChanged);
            // 
            // Settings
            // 
            this.AcceptButton = this.SaveSettingsButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 337);
            this.Controls.Add(this.SaveSettingsButton);
            this.Controls.Add(this.CancelSettingsButton);
            this.Controls.Add(this.settingsTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.settingsTab.ResumeLayout(false);
            this.generalPage.ResumeLayout(false);
            this.workingModePage.ResumeLayout(false);
            this.loggingSettingsGroup.ResumeLayout(false);
            this.loggingSettingsGroup.PerformLayout();
            this.StorageSettingsGroup.ResumeLayout(false);
            this.StorageSettingsGroup.PerformLayout();
            this.tracingSettingsGrp.ResumeLayout(false);
            this.tracingSettingsGrp.PerformLayout();
            this.distributingSettingsGrp.ResumeLayout(false);
            this.distributingSettingsGrp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingsTab;
        private System.Windows.Forms.TabPage generalPage;
        private System.Windows.Forms.GroupBox tracingSettingsGrp;
        private System.Windows.Forms.Label tracingDirectory;
        private System.Windows.Forms.TextBox tracingDirectoryTxt;
        private System.Windows.Forms.Button browseTracingBtn;
        private System.Windows.Forms.GroupBox loggingSettingsGroup;
        private System.Windows.Forms.Label DirectoryLabel;
        private System.Windows.Forms.TextBox directoryPathTxt;
        private System.Windows.Forms.Button BrowseLogDirButton;
        private System.Windows.Forms.CheckBox LoggingCheckBox;
        private System.Windows.Forms.GroupBox StorageSettingsGroup;
        private System.Windows.Forms.Button BrowseDatabaseButton;
        private System.Windows.Forms.Button BrowseFileStorageButton;
        private System.Windows.Forms.TextBox databaseTxt;
        private System.Windows.Forms.TextBox textStorageTxt;
        private System.Windows.Forms.Label DatabaseLabel;
        private System.Windows.Forms.Label StorageDirLabel;
        private System.Windows.Forms.TabPage workingModePage;
        private System.Windows.Forms.GroupBox distributingSettingsGrp;
        private System.Windows.Forms.Label distributedLabel;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.CheckedListBox discoveredServices;
        public System.Windows.Forms.CheckBox distributedCheckBox;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.Button CancelSettingsButton;

    }
}