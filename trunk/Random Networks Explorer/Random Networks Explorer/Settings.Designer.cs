namespace RandomNetworksExplorer
{
    partial class Settings
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
            this.LoggingCheckBox = new System.Windows.Forms.CheckBox();
            this.LoggingSettingsGroup = new System.Windows.Forms.GroupBox();
            this.DirectoryLabel = new System.Windows.Forms.Label();
            this.DirectoryPathTextBox = new System.Windows.Forms.TextBox();
            this.BrowseLogDirButton = new System.Windows.Forms.Button();
            this.StorageSettingsGroup = new System.Windows.Forms.GroupBox();
            this.BrowseDatabaseButton = new System.Windows.Forms.Button();
            this.BrowseFileStorageButton = new System.Windows.Forms.Button();
            this.DatabaseTextBox = new System.Windows.Forms.TextBox();
            this.TextStorageTextBox = new System.Windows.Forms.TextBox();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.StorageDirLabel = new System.Windows.Forms.Label();
            this.TopFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.WorkingModeGroup = new System.Windows.Forms.GroupBox();
            this.NetworkDistributeRadio = new System.Windows.Forms.RadioButton();
            this.LocalDistributeRadio = new System.Windows.Forms.RadioButton();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.SefaultSettingsButton = new System.Windows.Forms.Button();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.CancelSettingsButton = new System.Windows.Forms.Button();
            this.LoggingSettingsGroup.SuspendLayout();
            this.StorageSettingsGroup.SuspendLayout();
            this.TopFlowLayout.SuspendLayout();
            this.WorkingModeGroup.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            this.SuspendLayout();
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
            // LoggingSettingsGroup
            // 
            this.LoggingSettingsGroup.Controls.Add(this.DirectoryLabel);
            this.LoggingSettingsGroup.Controls.Add(this.DirectoryPathTextBox);
            this.LoggingSettingsGroup.Controls.Add(this.BrowseLogDirButton);
            this.LoggingSettingsGroup.Controls.Add(this.LoggingCheckBox);
            this.LoggingSettingsGroup.Location = new System.Drawing.Point(3, 3);
            this.LoggingSettingsGroup.Name = "LoggingSettingsGroup";
            this.LoggingSettingsGroup.Size = new System.Drawing.Size(625, 67);
            this.LoggingSettingsGroup.TabIndex = 0;
            this.LoggingSettingsGroup.TabStop = false;
            this.LoggingSettingsGroup.Text = "Logging";
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
            // DirectoryPathTextBox
            // 
            this.DirectoryPathTextBox.Location = new System.Drawing.Point(115, 41);
            this.DirectoryPathTextBox.Name = "DirectoryPathTextBox";
            this.DirectoryPathTextBox.Size = new System.Drawing.Size(423, 20);
            this.DirectoryPathTextBox.TabIndex = 1;
            // 
            // BrowseLogDirButton
            // 
            this.BrowseLogDirButton.Location = new System.Drawing.Point(544, 39);
            this.BrowseLogDirButton.Name = "BrowseLogDirButton";
            this.BrowseLogDirButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseLogDirButton.TabIndex = 2;
            this.BrowseLogDirButton.Text = "Browse...";
            this.BrowseLogDirButton.UseVisualStyleBackColor = true;
            // 
            // StorageSettingsGroup
            // 
            this.StorageSettingsGroup.Controls.Add(this.BrowseDatabaseButton);
            this.StorageSettingsGroup.Controls.Add(this.BrowseFileStorageButton);
            this.StorageSettingsGroup.Controls.Add(this.DatabaseTextBox);
            this.StorageSettingsGroup.Controls.Add(this.TextStorageTextBox);
            this.StorageSettingsGroup.Controls.Add(this.DatabaseLabel);
            this.StorageSettingsGroup.Controls.Add(this.StorageDirLabel);
            this.StorageSettingsGroup.Location = new System.Drawing.Point(3, 76);
            this.StorageSettingsGroup.Name = "StorageSettingsGroup";
            this.StorageSettingsGroup.Size = new System.Drawing.Size(625, 76);
            this.StorageSettingsGroup.TabIndex = 1;
            this.StorageSettingsGroup.TabStop = false;
            this.StorageSettingsGroup.Text = "Default storage locations";
            // 
            // BrowseDatabaseButton
            // 
            this.BrowseDatabaseButton.Location = new System.Drawing.Point(544, 48);
            this.BrowseDatabaseButton.Name = "BrowseDatabaseButton";
            this.BrowseDatabaseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseDatabaseButton.TabIndex = 3;
            this.BrowseDatabaseButton.Text = "Browse...";
            this.BrowseDatabaseButton.UseVisualStyleBackColor = true;
            // 
            // BrowseFileStorageButton
            // 
            this.BrowseFileStorageButton.Location = new System.Drawing.Point(544, 21);
            this.BrowseFileStorageButton.Name = "BrowseFileStorageButton";
            this.BrowseFileStorageButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseFileStorageButton.TabIndex = 1;
            this.BrowseFileStorageButton.Text = "Browse...";
            this.BrowseFileStorageButton.UseVisualStyleBackColor = true;
            // 
            // DatabaseTextBox
            // 
            this.DatabaseTextBox.Location = new System.Drawing.Point(115, 50);
            this.DatabaseTextBox.Name = "DatabaseTextBox";
            this.DatabaseTextBox.Size = new System.Drawing.Size(423, 20);
            this.DatabaseTextBox.TabIndex = 2;
            // 
            // TextStorageTextBox
            // 
            this.TextStorageTextBox.Location = new System.Drawing.Point(115, 24);
            this.TextStorageTextBox.Name = "TextStorageTextBox";
            this.TextStorageTextBox.Size = new System.Drawing.Size(423, 20);
            this.TextStorageTextBox.TabIndex = 0;
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
            // TopFlowLayout
            // 
            this.TopFlowLayout.Controls.Add(this.LoggingSettingsGroup);
            this.TopFlowLayout.Controls.Add(this.StorageSettingsGroup);
            this.TopFlowLayout.Controls.Add(this.WorkingModeGroup);
            this.TopFlowLayout.Controls.Add(this.ButtonPanel);
            this.TopFlowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopFlowLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.TopFlowLayout.Location = new System.Drawing.Point(0, 0);
            this.TopFlowLayout.Name = "TopFlowLayout";
            this.TopFlowLayout.Size = new System.Drawing.Size(634, 244);
            this.TopFlowLayout.TabIndex = 0;
            this.TopFlowLayout.WrapContents = false;
            // 
            // WorkingModeGroup
            // 
            this.WorkingModeGroup.Controls.Add(this.NetworkDistributeRadio);
            this.WorkingModeGroup.Controls.Add(this.LocalDistributeRadio);
            this.WorkingModeGroup.Location = new System.Drawing.Point(3, 158);
            this.WorkingModeGroup.Name = "WorkingModeGroup";
            this.WorkingModeGroup.Size = new System.Drawing.Size(625, 44);
            this.WorkingModeGroup.TabIndex = 2;
            this.WorkingModeGroup.TabStop = false;
            this.WorkingModeGroup.Text = "Working mode";
            // 
            // NetworkDistributeRadio
            // 
            this.NetworkDistributeRadio.AutoSize = true;
            this.NetworkDistributeRadio.Location = new System.Drawing.Point(349, 19);
            this.NetworkDistributeRadio.Name = "NetworkDistributeRadio";
            this.NetworkDistributeRadio.Size = new System.Drawing.Size(231, 17);
            this.NetworkDistributeRadio.TabIndex = 1;
            this.NetworkDistributeRadio.TabStop = true;
            this.NetworkDistributeRadio.Text = "Distribute jobs in available hosts on network";
            this.NetworkDistributeRadio.UseVisualStyleBackColor = true;
            // 
            // LocalDistributeRadio
            // 
            this.LocalDistributeRadio.AutoSize = true;
            this.LocalDistributeRadio.Location = new System.Drawing.Point(115, 19);
            this.LocalDistributeRadio.Name = "LocalDistributeRadio";
            this.LocalDistributeRadio.Size = new System.Drawing.Size(174, 17);
            this.LocalDistributeRadio.TabIndex = 0;
            this.LocalDistributeRadio.TabStop = true;
            this.LocalDistributeRadio.Text = "Distribute jobs on local machine";
            this.LocalDistributeRadio.UseVisualStyleBackColor = true;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Controls.Add(this.SefaultSettingsButton);
            this.ButtonPanel.Controls.Add(this.SaveSettingsButton);
            this.ButtonPanel.Controls.Add(this.CancelSettingsButton);
            this.ButtonPanel.Location = new System.Drawing.Point(3, 208);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(625, 26);
            this.ButtonPanel.TabIndex = 3;
            // 
            // SefaultSettingsButton
            // 
            this.SefaultSettingsButton.Location = new System.Drawing.Point(9, 3);
            this.SefaultSettingsButton.Name = "SefaultSettingsButton";
            this.SefaultSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.SefaultSettingsButton.TabIndex = 2;
            this.SefaultSettingsButton.Text = "Defaults";
            this.SefaultSettingsButton.UseVisualStyleBackColor = true;
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(463, 3);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSettingsButton.TabIndex = 0;
            this.SaveSettingsButton.Text = "Save";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            // 
            // CancelSettingsButton
            // 
            this.CancelSettingsButton.Location = new System.Drawing.Point(544, 3);
            this.CancelSettingsButton.Name = "CancelSettingsButton";
            this.CancelSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.CancelSettingsButton.TabIndex = 1;
            this.CancelSettingsButton.Text = "Cancel";
            this.CancelSettingsButton.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 244);
            this.Controls.Add(this.TopFlowLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.LoggingSettingsGroup.ResumeLayout(false);
            this.LoggingSettingsGroup.PerformLayout();
            this.StorageSettingsGroup.ResumeLayout(false);
            this.StorageSettingsGroup.PerformLayout();
            this.TopFlowLayout.ResumeLayout(false);
            this.WorkingModeGroup.ResumeLayout(false);
            this.WorkingModeGroup.PerformLayout();
            this.ButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox LoggingCheckBox;
        private System.Windows.Forms.GroupBox LoggingSettingsGroup;
        private System.Windows.Forms.Label DirectoryLabel;
        private System.Windows.Forms.GroupBox StorageSettingsGroup;
        private System.Windows.Forms.TextBox DirectoryPathTextBox;
        private System.Windows.Forms.Button BrowseLogDirButton;
        private System.Windows.Forms.FlowLayoutPanel TopFlowLayout;
        private System.Windows.Forms.Button BrowseDatabaseButton;
        private System.Windows.Forms.Button BrowseFileStorageButton;
        private System.Windows.Forms.TextBox DatabaseTextBox;
        private System.Windows.Forms.TextBox TextStorageTextBox;
        private System.Windows.Forms.Label DatabaseLabel;
        private System.Windows.Forms.Label StorageDirLabel;
        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.Button SefaultSettingsButton;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.Button CancelSettingsButton;
        private System.Windows.Forms.GroupBox WorkingModeGroup;
        private System.Windows.Forms.RadioButton NetworkDistributeRadio;
        private System.Windows.Forms.RadioButton LocalDistributeRadio;
    }
}