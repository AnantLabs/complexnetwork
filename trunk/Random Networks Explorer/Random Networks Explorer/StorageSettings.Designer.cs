namespace RandomNetworksExplorer
{
    partial class StorageSettings
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
            this.storageGroupBox = new System.Windows.Forms.GroupBox();
            this.Browse = new System.Windows.Forms.Button();
            this.SQLRadioButton = new System.Windows.Forms.RadioButton();
            this.AddConnection = new System.Windows.Forms.Button();
            this.textBoxConnStr = new System.Windows.Forms.TextBox();
            this.XMLRadioButton = new System.Windows.Forms.RadioButton();
            this.LocationTxt = new System.Windows.Forms.TextBox();
            this.LabelConnStr = new System.Windows.Forms.Label();
            this.location = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.storageGroupBox.SuspendLayout();
            this.SuspendLayout();
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
            this.storageGroupBox.Controls.Add(this.location);
            this.storageGroupBox.Location = new System.Drawing.Point(12, 12);
            this.storageGroupBox.Name = "storageGroupBox";
            this.storageGroupBox.Size = new System.Drawing.Size(421, 171);
            this.storageGroupBox.TabIndex = 1;
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
            // 
            // AddConnection
            // 
            this.AddConnection.Location = new System.Drawing.Point(315, 119);
            this.AddConnection.Name = "AddConnection";
            this.AddConnection.Size = new System.Drawing.Size(82, 23);
            this.AddConnection.TabIndex = 24;
            this.AddConnection.Text = "Connections";
            this.AddConnection.UseVisualStyleBackColor = true;
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
            // location
            // 
            this.location.AutoSize = true;
            this.location.Location = new System.Drawing.Point(48, 63);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(84, 13);
            this.location.TabIndex = 20;
            this.location.Text = "Storage location";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(358, 189);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 19;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(277, 189);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 18;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // StorageSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 222);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.storageGroupBox);
            this.Name = "StorageSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StorageSettings";
            this.storageGroupBox.ResumeLayout(false);
            this.storageGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox storageGroupBox;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.RadioButton SQLRadioButton;
        private System.Windows.Forms.Button AddConnection;
        private System.Windows.Forms.TextBox textBoxConnStr;
        private System.Windows.Forms.RadioButton XMLRadioButton;
        private System.Windows.Forms.TextBox LocationTxt;
        private System.Windows.Forms.Label LabelConnStr;
        private System.Windows.Forms.Label location;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
    }
}