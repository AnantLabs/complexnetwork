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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.DatabaseTextBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.StorageDirLabel = new System.Windows.Forms.Label();
            this.TopFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CancelSettingsButton = new System.Windows.Forms.Button();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.SefaultSettingsButton = new System.Windows.Forms.Button();
            this.LoggingSettingsGroup.SuspendLayout();
            this.StorageSettingsGroup.SuspendLayout();
            this.TopFlowLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoggingCheckBox
            // 
            this.LoggingCheckBox.AutoSize = true;
            this.LoggingCheckBox.Location = new System.Drawing.Point(97, 19);
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
            this.LoggingSettingsGroup.Size = new System.Drawing.Size(631, 67);
            this.LoggingSettingsGroup.TabIndex = 1;
            this.LoggingSettingsGroup.TabStop = false;
            this.LoggingSettingsGroup.Text = "Logging";
            // 
            // DirectoryLabel
            // 
            this.DirectoryLabel.AutoSize = true;
            this.DirectoryLabel.Location = new System.Drawing.Point(10, 44);
            this.DirectoryLabel.Name = "DirectoryLabel";
            this.DirectoryLabel.Size = new System.Drawing.Size(85, 13);
            this.DirectoryLabel.TabIndex = 1;
            this.DirectoryLabel.Text = "Output directory:";
            // 
            // DirectoryPathTextBox
            // 
            this.DirectoryPathTextBox.Location = new System.Drawing.Point(97, 41);
            this.DirectoryPathTextBox.Name = "DirectoryPathTextBox";
            this.DirectoryPathTextBox.Size = new System.Drawing.Size(447, 20);
            this.DirectoryPathTextBox.TabIndex = 2;
            // 
            // BrowseLogDirButton
            // 
            this.BrowseLogDirButton.Location = new System.Drawing.Point(550, 39);
            this.BrowseLogDirButton.Name = "BrowseLogDirButton";
            this.BrowseLogDirButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseLogDirButton.TabIndex = 3;
            this.BrowseLogDirButton.Text = "Browse...";
            this.BrowseLogDirButton.UseVisualStyleBackColor = true;
            // 
            // StorageSettingsGroup
            // 
            this.StorageSettingsGroup.Controls.Add(this.button2);
            this.StorageSettingsGroup.Controls.Add(this.button1);
            this.StorageSettingsGroup.Controls.Add(this.DatabaseTextBox);
            this.StorageSettingsGroup.Controls.Add(this.textBox1);
            this.StorageSettingsGroup.Controls.Add(this.DatabaseLabel);
            this.StorageSettingsGroup.Controls.Add(this.StorageDirLabel);
            this.StorageSettingsGroup.Location = new System.Drawing.Point(3, 76);
            this.StorageSettingsGroup.Name = "StorageSettingsGroup";
            this.StorageSettingsGroup.Size = new System.Drawing.Size(631, 76);
            this.StorageSettingsGroup.TabIndex = 2;
            this.StorageSettingsGroup.TabStop = false;
            this.StorageSettingsGroup.Text = "Default storage locations";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(550, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Browse...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(550, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // DatabaseTextBox
            // 
            this.DatabaseTextBox.Location = new System.Drawing.Point(97, 50);
            this.DatabaseTextBox.Name = "DatabaseTextBox";
            this.DatabaseTextBox.Size = new System.Drawing.Size(447, 20);
            this.DatabaseTextBox.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(447, 20);
            this.textBox1.TabIndex = 2;
            // 
            // DatabaseLabel
            // 
            this.DatabaseLabel.AutoSize = true;
            this.DatabaseLabel.Location = new System.Drawing.Point(10, 53);
            this.DatabaseLabel.Name = "DatabaseLabel";
            this.DatabaseLabel.Size = new System.Drawing.Size(56, 13);
            this.DatabaseLabel.TabIndex = 1;
            this.DatabaseLabel.Text = "Database:";
            // 
            // StorageDirLabel
            // 
            this.StorageDirLabel.AutoSize = true;
            this.StorageDirLabel.Location = new System.Drawing.Point(10, 27);
            this.StorageDirLabel.Name = "StorageDirLabel";
            this.StorageDirLabel.Size = new System.Drawing.Size(69, 13);
            this.StorageDirLabel.TabIndex = 0;
            this.StorageDirLabel.Text = "File directory:";
            // 
            // TopFlowLayout
            // 
            this.TopFlowLayout.Controls.Add(this.LoggingSettingsGroup);
            this.TopFlowLayout.Controls.Add(this.StorageSettingsGroup);
            this.TopFlowLayout.Controls.Add(this.panel1);
            this.TopFlowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopFlowLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.TopFlowLayout.Location = new System.Drawing.Point(0, 0);
            this.TopFlowLayout.Name = "TopFlowLayout";
            this.TopFlowLayout.Size = new System.Drawing.Size(634, 204);
            this.TopFlowLayout.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SefaultSettingsButton);
            this.panel1.Controls.Add(this.SaveSettingsButton);
            this.panel1.Controls.Add(this.CancelSettingsButton);
            this.panel1.Location = new System.Drawing.Point(3, 158);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 41);
            this.panel1.TabIndex = 3;
            // 
            // CancelSettingsButton
            // 
            this.CancelSettingsButton.Location = new System.Drawing.Point(550, 18);
            this.CancelSettingsButton.Name = "CancelSettingsButton";
            this.CancelSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.CancelSettingsButton.TabIndex = 4;
            this.CancelSettingsButton.Text = "Cancel";
            this.CancelSettingsButton.UseVisualStyleBackColor = true;
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(469, 18);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSettingsButton.TabIndex = 5;
            this.SaveSettingsButton.Text = "Save";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            // 
            // SefaultSettingsButton
            // 
            this.SefaultSettingsButton.Location = new System.Drawing.Point(9, 18);
            this.SefaultSettingsButton.Name = "SefaultSettingsButton";
            this.SefaultSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.SefaultSettingsButton.TabIndex = 6;
            this.SefaultSettingsButton.Text = "Defaults";
            this.SefaultSettingsButton.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 204);
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
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox DatabaseTextBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label DatabaseLabel;
        private System.Windows.Forms.Label StorageDirLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SefaultSettingsButton;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.Button CancelSettingsButton;
    }
}