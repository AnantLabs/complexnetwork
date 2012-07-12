namespace RandomGraphLauncher
{
    partial class SettingsOptionsWindow
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
            this.tracingModeCheckBox = new System.Windows.Forms.CheckBox();
            this.trainingModeCheckBox = new System.Windows.Forms.CheckBox();
            this.generationModeGroupBox = new System.Windows.Forms.GroupBox();
            this.randomGenerationRadioButton = new System.Windows.Forms.RadioButton();
            this.staticGenerationRadioButton = new System.Windows.Forms.RadioButton();
            this.OptionsTabControl = new System.Windows.Forms.TabControl();
            this.storageTabPage = new System.Windows.Forms.TabPage();
            this.storageGroupBox = new System.Windows.Forms.GroupBox();
            this.Browse = new System.Windows.Forms.Button();
            this.SQLRadioButton = new System.Windows.Forms.RadioButton();
            this.AddConnection = new System.Windows.Forms.Button();
            this.textBoxConnStr = new System.Windows.Forms.TextBox();
            this.XMLRadioButton = new System.Windows.Forms.RadioButton();
            this.LocationTxt = new System.Windows.Forms.TextBox();
            this.LabelConnStr = new System.Windows.Forms.Label();
            this.Location = new System.Windows.Forms.Label();
            this.distributedTabPage = new System.Windows.Forms.TabPage();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.distributedCheckBox = new System.Windows.Forms.CheckBox();
            this.DiscoveredServices = new System.Windows.Forms.CheckedListBox();
            this.distributedLabel = new System.Windows.Forms.Label();
            this.LoggerSettingsTabPage = new System.Windows.Forms.TabPage();
            this.loggerSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.debugCheckBox = new System.Windows.Forms.CheckBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.loggerPathTextBox = new System.Windows.Forms.TextBox();
            this.savingButton = new System.Windows.Forms.Button();
            this.tracingModetabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tracingBrowseButton = new System.Windows.Forms.Button();
            this.tracingLabel = new System.Windows.Forms.Label();
            this.tracingPathTxtBox = new System.Windows.Forms.TextBox();
            this.generationTabPage = new System.Windows.Forms.TabPage();
            this.BrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.LoggerBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tracingBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.distributedGroupBox = new System.Windows.Forms.GroupBox();
            this.generationModeGroupBox.SuspendLayout();
            this.OptionsTabControl.SuspendLayout();
            this.storageTabPage.SuspendLayout();
            this.storageGroupBox.SuspendLayout();
            this.distributedTabPage.SuspendLayout();
            this.LoggerSettingsTabPage.SuspendLayout();
            this.loggerSettingsGroupBox.SuspendLayout();
            this.tracingModetabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.generationTabPage.SuspendLayout();
            this.distributedGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(483, 293);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(402, 293);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 16;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.Ok_Click);
            // 
            // tracingModeCheckBox
            // 
            this.tracingModeCheckBox.AutoSize = true;
            this.tracingModeCheckBox.Location = new System.Drawing.Point(20, 40);
            this.tracingModeCheckBox.Name = "tracingModeCheckBox";
            this.tracingModeCheckBox.Size = new System.Drawing.Size(91, 17);
            this.tracingModeCheckBox.TabIndex = 2;
            this.tracingModeCheckBox.Text = "Tracing mode";
            this.tracingModeCheckBox.UseVisualStyleBackColor = true;
            this.tracingModeCheckBox.CheckedChanged += new System.EventHandler(this.tracingModeCheckBox_CheckedChanged);
            // 
            // trainingModeCheckBox
            // 
            this.trainingModeCheckBox.AutoSize = true;
            this.trainingModeCheckBox.Location = new System.Drawing.Point(18, 118);
            this.trainingModeCheckBox.Name = "trainingModeCheckBox";
            this.trainingModeCheckBox.Size = new System.Drawing.Size(93, 17);
            this.trainingModeCheckBox.TabIndex = 1;
            this.trainingModeCheckBox.Text = "Training mode";
            this.trainingModeCheckBox.UseVisualStyleBackColor = true;
            this.trainingModeCheckBox.CheckedChanged += new System.EventHandler(this.trainingModeCheckBox_CheckedChanged);
            // 
            // generationModeGroupBox
            // 
            this.generationModeGroupBox.Controls.Add(this.randomGenerationRadioButton);
            this.generationModeGroupBox.Controls.Add(this.staticGenerationRadioButton);
            this.generationModeGroupBox.Location = new System.Drawing.Point(39, 24);
            this.generationModeGroupBox.Name = "generationModeGroupBox";
            this.generationModeGroupBox.Size = new System.Drawing.Size(421, 171);
            this.generationModeGroupBox.TabIndex = 13;
            this.generationModeGroupBox.TabStop = false;
            this.generationModeGroupBox.Text = "Choose generation mode";
            // 
            // randomGenerationRadioButton
            // 
            this.randomGenerationRadioButton.AutoSize = true;
            this.randomGenerationRadioButton.Location = new System.Drawing.Point(20, 40);
            this.randomGenerationRadioButton.Name = "randomGenerationRadioButton";
            this.randomGenerationRadioButton.Size = new System.Drawing.Size(118, 17);
            this.randomGenerationRadioButton.TabIndex = 4;
            this.randomGenerationRadioButton.Text = "Random generation";
            this.randomGenerationRadioButton.UseVisualStyleBackColor = true;
            this.randomGenerationRadioButton.CheckedChanged += new System.EventHandler(this.randomGenerationRadioButton_CheckedChanged);
            // 
            // staticGenerationRadioButton
            // 
            this.staticGenerationRadioButton.AutoSize = true;
            this.staticGenerationRadioButton.Location = new System.Drawing.Point(20, 73);
            this.staticGenerationRadioButton.Name = "staticGenerationRadioButton";
            this.staticGenerationRadioButton.Size = new System.Drawing.Size(105, 17);
            this.staticGenerationRadioButton.TabIndex = 3;
            this.staticGenerationRadioButton.Text = "Static generation";
            this.staticGenerationRadioButton.UseVisualStyleBackColor = true;
            this.staticGenerationRadioButton.CheckedChanged += new System.EventHandler(this.staticGenerationRadioButton_CheckedChanged);
            // 
            // OptionsTabControl
            // 
            this.OptionsTabControl.Controls.Add(this.storageTabPage);
            this.OptionsTabControl.Controls.Add(this.distributedTabPage);
            this.OptionsTabControl.Controls.Add(this.LoggerSettingsTabPage);
            this.OptionsTabControl.Controls.Add(this.tracingModetabPage);
            this.OptionsTabControl.Controls.Add(this.generationTabPage);
            this.OptionsTabControl.Location = new System.Drawing.Point(39, 24);
            this.OptionsTabControl.Name = "OptionsTabControl";
            this.OptionsTabControl.SelectedIndex = 0;
            this.OptionsTabControl.Size = new System.Drawing.Size(519, 252);
            this.OptionsTabControl.TabIndex = 14;
            // 
            // storageTabPage
            // 
            this.storageTabPage.Controls.Add(this.storageGroupBox);
            this.storageTabPage.Location = new System.Drawing.Point(4, 22);
            this.storageTabPage.Name = "storageTabPage";
            this.storageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.storageTabPage.Size = new System.Drawing.Size(511, 226);
            this.storageTabPage.TabIndex = 0;
            this.storageTabPage.Text = "Storage";
            this.storageTabPage.UseVisualStyleBackColor = true;
            // 
            // storageGroupBox
            // 
            this.storageGroupBox.Controls.Add(this.Browse);
            this.storageGroupBox.Controls.Add(this.SQLRadioButton);
            this.storageGroupBox.Controls.Add(this.AddConnection);
            this.storageGroupBox.Controls.Add(this.textBoxConnStr);
            this.storageGroupBox.Controls.Add(this.XMLRadioButton);
            this.storageGroupBox.Controls.Add(this.LocationTxt);
            this.storageGroupBox.Controls.Add(this.LabelConnStr);
            this.storageGroupBox.Controls.Add(this.Location);
            this.storageGroupBox.Location = new System.Drawing.Point(39, 24);
            this.storageGroupBox.Name = "storageGroupBox";
            this.storageGroupBox.Size = new System.Drawing.Size(421, 171);
            this.storageGroupBox.TabIndex = 0;
            this.storageGroupBox.TabStop = false;
            this.storageGroupBox.Text = "Choose data store";
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(315, 59);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(82, 23);
            this.Browse.TabIndex = 22;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // SQLRadioButton
            // 
            this.SQLRadioButton.AutoSize = true;
            this.SQLRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SQLRadioButton.Location = new System.Drawing.Point(27, 92);
            this.SQLRadioButton.Name = "SQLRadioButton";
            this.SQLRadioButton.Size = new System.Drawing.Size(72, 17);
            this.SQLRadioButton.TabIndex = 23;
            this.SQLRadioButton.TabStop = true;
            this.SQLRadioButton.Text = "SQL store";
            this.SQLRadioButton.UseVisualStyleBackColor = true;
            this.SQLRadioButton.CheckedChanged += new System.EventHandler(this.SQLRadioButton_CheckedChanged);
            // 
            // AddConnection
            // 
            this.AddConnection.Location = new System.Drawing.Point(315, 119);
            this.AddConnection.Name = "AddConnection";
            this.AddConnection.Size = new System.Drawing.Size(82, 23);
            this.AddConnection.TabIndex = 24;
            this.AddConnection.Text = "Connections";
            this.AddConnection.UseVisualStyleBackColor = true;
            this.AddConnection.Click += new System.EventHandler(this.AddConnection_Click);
            // 
            // textBoxConnStr
            // 
            this.textBoxConnStr.Location = new System.Drawing.Point(152, 119);
            this.textBoxConnStr.Name = "textBoxConnStr";
            this.textBoxConnStr.Size = new System.Drawing.Size(140, 20);
            this.textBoxConnStr.TabIndex = 26;
            // 
            // XMLRadioButton
            // 
            this.XMLRadioButton.AutoSize = true;
            this.XMLRadioButton.Location = new System.Drawing.Point(27, 33);
            this.XMLRadioButton.Name = "XMLRadioButton";
            this.XMLRadioButton.Size = new System.Drawing.Size(73, 17);
            this.XMLRadioButton.TabIndex = 19;
            this.XMLRadioButton.Text = "XML store";
            this.XMLRadioButton.UseVisualStyleBackColor = true;
            this.XMLRadioButton.CheckedChanged += new System.EventHandler(this.XMLRadioButton_CheckedChanged);
            // 
            // LocationTxt
            // 
            this.LocationTxt.Location = new System.Drawing.Point(152, 59);
            this.LocationTxt.Name = "LocationTxt";
            this.LocationTxt.Size = new System.Drawing.Size(140, 20);
            this.LocationTxt.TabIndex = 21;
            this.LocationTxt.Text = "C:\\ComplexNetwork";
            // 
            // LabelConnStr
            // 
            this.LabelConnStr.AutoSize = true;
            this.LabelConnStr.Location = new System.Drawing.Point(47, 123);
            this.LabelConnStr.Name = "LabelConnStr";
            this.LabelConnStr.Size = new System.Drawing.Size(88, 13);
            this.LabelConnStr.TabIndex = 25;
            this.LabelConnStr.Text = "ConnectionString";
            // 
            // Location
            // 
            this.Location.AutoSize = true;
            this.Location.Location = new System.Drawing.Point(48, 63);
            this.Location.Name = "Location";
            this.Location.Size = new System.Drawing.Size(84, 13);
            this.Location.TabIndex = 20;
            this.Location.Text = "Storage location";
            // 
            // distributedTabPage
            // 
            this.distributedTabPage.Controls.Add(this.distributedGroupBox);
            this.distributedTabPage.Location = new System.Drawing.Point(4, 22);
            this.distributedTabPage.Name = "distributedTabPage";
            this.distributedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.distributedTabPage.Size = new System.Drawing.Size(511, 226);
            this.distributedTabPage.TabIndex = 1;
            this.distributedTabPage.Text = "Distributed";
            this.distributedTabPage.UseVisualStyleBackColor = true;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(23, 111);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 10;
            this.RefreshButton.Text = "Refresh list";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
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
            // DiscoveredServices
            // 
            this.DiscoveredServices.BackColor = System.Drawing.SystemColors.Control;
            this.DiscoveredServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DiscoveredServices.FormattingEnabled = true;
            this.DiscoveredServices.Location = new System.Drawing.Point(267, 20);
            this.DiscoveredServices.Name = "DiscoveredServices";
            this.DiscoveredServices.Size = new System.Drawing.Size(137, 135);
            this.DiscoveredServices.TabIndex = 8;
            // 
            // distributedLabel
            // 
            this.distributedLabel.Location = new System.Drawing.Point(20, 62);
            this.distributedLabel.Name = "distributedLabel";
            this.distributedLabel.Size = new System.Drawing.Size(250, 31);
            this.distributedLabel.TabIndex = 7;
            this.distributedLabel.Text = "Please select computers which will be used during \ndistributed calculation.";
            // 
            // LoggerSettingsTabPage
            // 
            this.LoggerSettingsTabPage.Controls.Add(this.loggerSettingsGroupBox);
            this.LoggerSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.LoggerSettingsTabPage.Name = "LoggerSettingsTabPage";
            this.LoggerSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LoggerSettingsTabPage.Size = new System.Drawing.Size(511, 226);
            this.LoggerSettingsTabPage.TabIndex = 2;
            this.LoggerSettingsTabPage.Text = "Logger Settings";
            this.LoggerSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // loggerSettingsGroupBox
            // 
            this.loggerSettingsGroupBox.Controls.Add(this.debugCheckBox);
            this.loggerSettingsGroupBox.Controls.Add(this.pathLabel);
            this.loggerSettingsGroupBox.Controls.Add(this.loggerPathTextBox);
            this.loggerSettingsGroupBox.Controls.Add(this.savingButton);
            this.loggerSettingsGroupBox.Location = new System.Drawing.Point(39, 24);
            this.loggerSettingsGroupBox.Name = "loggerSettingsGroupBox";
            this.loggerSettingsGroupBox.Size = new System.Drawing.Size(421, 171);
            this.loggerSettingsGroupBox.TabIndex = 1;
            this.loggerSettingsGroupBox.TabStop = false;
            this.loggerSettingsGroupBox.Text = "Set logger settings";
            // 
            // debugCheckBox
            // 
            this.debugCheckBox.AutoSize = true;
            this.debugCheckBox.Location = new System.Drawing.Point(25, 98);
            this.debugCheckBox.Name = "debugCheckBox";
            this.debugCheckBox.Size = new System.Drawing.Size(101, 17);
            this.debugCheckBox.TabIndex = 12;
            this.debugCheckBox.Text = "Only debug files";
            this.debugCheckBox.UseVisualStyleBackColor = true;
            this.debugCheckBox.CheckedChanged += new System.EventHandler(this.debugCheckBox_CheckedChanged);
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(22, 33);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(80, 13);
            this.pathLabel.TabIndex = 11;
            this.pathLabel.Text = "Path for Logger";
            // 
            // loggerPathTextBox
            // 
            this.loggerPathTextBox.Location = new System.Drawing.Point(25, 59);
            this.loggerPathTextBox.Name = "loggerPathTextBox";
            this.loggerPathTextBox.Size = new System.Drawing.Size(280, 20);
            this.loggerPathTextBox.TabIndex = 7;
            // 
            // savingButton
            // 
            this.savingButton.Location = new System.Drawing.Point(320, 59);
            this.savingButton.Name = "savingButton";
            this.savingButton.Size = new System.Drawing.Size(75, 23);
            this.savingButton.TabIndex = 6;
            this.savingButton.Text = "Save to ...";
            this.savingButton.UseVisualStyleBackColor = true;
            this.savingButton.Click += new System.EventHandler(this.savingButton_Click);
            // 
            // tracingModetabPage
            // 
            this.tracingModetabPage.Controls.Add(this.groupBox1);
            this.tracingModetabPage.Location = new System.Drawing.Point(4, 22);
            this.tracingModetabPage.Name = "tracingModetabPage";
            this.tracingModetabPage.Padding = new System.Windows.Forms.Padding(3);
            this.tracingModetabPage.Size = new System.Drawing.Size(511, 226);
            this.tracingModetabPage.TabIndex = 3;
            this.tracingModetabPage.Text = "Tracing and Training Modes";
            this.tracingModetabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trainingModeCheckBox);
            this.groupBox1.Controls.Add(this.tracingBrowseButton);
            this.groupBox1.Controls.Add(this.tracingModeCheckBox);
            this.groupBox1.Controls.Add(this.tracingLabel);
            this.groupBox1.Controls.Add(this.tracingPathTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(39, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 171);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Set Tracing and Training configurations";
            // 
            // tracingBrowseButton
            // 
            this.tracingBrowseButton.Location = new System.Drawing.Point(286, 74);
            this.tracingBrowseButton.Name = "tracingBrowseButton";
            this.tracingBrowseButton.Size = new System.Drawing.Size(82, 23);
            this.tracingBrowseButton.TabIndex = 25;
            this.tracingBrowseButton.Text = "Browse";
            this.tracingBrowseButton.UseVisualStyleBackColor = true;
            this.tracingBrowseButton.Click += new System.EventHandler(this.tracingBrowseButton_Click);
            // 
            // tracingLabel
            // 
            this.tracingLabel.AutoSize = true;
            this.tracingLabel.Location = new System.Drawing.Point(17, 77);
            this.tracingLabel.Name = "tracingLabel";
            this.tracingLabel.Size = new System.Drawing.Size(72, 13);
            this.tracingLabel.TabIndex = 23;
            this.tracingLabel.Text = "Tracing folder";
            // 
            // tracingPathTxtBox
            // 
            this.tracingPathTxtBox.Location = new System.Drawing.Point(95, 74);
            this.tracingPathTxtBox.Name = "tracingPathTxtBox";
            this.tracingPathTxtBox.Size = new System.Drawing.Size(176, 20);
            this.tracingPathTxtBox.TabIndex = 24;
            // 
            // generationTabPage
            // 
            this.generationTabPage.Controls.Add(this.generationModeGroupBox);
            this.generationTabPage.Location = new System.Drawing.Point(4, 22);
            this.generationTabPage.Name = "generationTabPage";
            this.generationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.generationTabPage.Size = new System.Drawing.Size(511, 226);
            this.generationTabPage.TabIndex = 4;
            this.generationTabPage.Text = "Generation";
            this.generationTabPage.UseVisualStyleBackColor = true;
            // 
            // distributedGroupBox
            // 
            this.distributedGroupBox.Controls.Add(this.distributedLabel);
            this.distributedGroupBox.Controls.Add(this.RefreshButton);
            this.distributedGroupBox.Controls.Add(this.DiscoveredServices);
            this.distributedGroupBox.Controls.Add(this.distributedCheckBox);
            this.distributedGroupBox.Location = new System.Drawing.Point(39, 24);
            this.distributedGroupBox.Name = "distributedGroupBox";
            this.distributedGroupBox.Size = new System.Drawing.Size(421, 171);
            this.distributedGroupBox.TabIndex = 11;
            this.distributedGroupBox.TabStop = false;
            this.distributedGroupBox.Text = "Set distributed mode settings";
            // 
            // SettingsOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 338);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.OptionsTabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsOptionsWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.SettingsOptionsWindow_Load);
            this.generationModeGroupBox.ResumeLayout(false);
            this.generationModeGroupBox.PerformLayout();
            this.OptionsTabControl.ResumeLayout(false);
            this.storageTabPage.ResumeLayout(false);
            this.storageGroupBox.ResumeLayout(false);
            this.storageGroupBox.PerformLayout();
            this.distributedTabPage.ResumeLayout(false);
            this.LoggerSettingsTabPage.ResumeLayout(false);
            this.loggerSettingsGroupBox.ResumeLayout(false);
            this.loggerSettingsGroupBox.PerformLayout();
            this.tracingModetabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.generationTabPage.ResumeLayout(false);
            this.distributedGroupBox.ResumeLayout(false);
            this.distributedGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.CheckBox tracingModeCheckBox;
        private System.Windows.Forms.CheckBox trainingModeCheckBox;
        private System.Windows.Forms.GroupBox generationModeGroupBox;
        private System.Windows.Forms.RadioButton randomGenerationRadioButton;
        private System.Windows.Forms.RadioButton staticGenerationRadioButton;
        private System.Windows.Forms.TabControl OptionsTabControl;
        private System.Windows.Forms.TabPage storageTabPage;
        private System.Windows.Forms.GroupBox storageGroupBox;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.RadioButton SQLRadioButton;
        private System.Windows.Forms.Button AddConnection;
        private System.Windows.Forms.TextBox textBoxConnStr;
        private System.Windows.Forms.RadioButton XMLRadioButton;
        private System.Windows.Forms.TextBox LocationTxt;
        private System.Windows.Forms.Label LabelConnStr;
        private System.Windows.Forms.Label Location;
        private System.Windows.Forms.TabPage distributedTabPage;
        private System.Windows.Forms.Button RefreshButton;
        public System.Windows.Forms.CheckBox distributedCheckBox;
        private System.Windows.Forms.CheckedListBox DiscoveredServices;
        private System.Windows.Forms.Label distributedLabel;
        private System.Windows.Forms.TabPage LoggerSettingsTabPage;
        private System.Windows.Forms.GroupBox loggerSettingsGroupBox;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.TextBox loggerPathTextBox;
        private System.Windows.Forms.Button savingButton;
        private System.Windows.Forms.FolderBrowserDialog BrowserDialog;
        private System.Windows.Forms.FolderBrowserDialog LoggerBrowserDialog;
        private System.Windows.Forms.TabPage tracingModetabPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button tracingBrowseButton;
        private System.Windows.Forms.Label tracingLabel;
        private System.Windows.Forms.TextBox tracingPathTxtBox;
        private System.Windows.Forms.FolderBrowserDialog tracingBrowserDialog;
        private System.Windows.Forms.TabPage generationTabPage;
        private System.Windows.Forms.CheckBox debugCheckBox;
        private System.Windows.Forms.GroupBox distributedGroupBox;
    }
}