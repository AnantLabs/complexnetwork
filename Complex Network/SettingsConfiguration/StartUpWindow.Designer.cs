namespace SettingsConfiguration
{
    partial class StartUpWindow
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
            this.XMLRadio = new System.Windows.Forms.RadioButton();
            this.SQLRadio = new System.Windows.Forms.RadioButton();
            this.Location = new System.Windows.Forms.Label();
            this.LocationTxt = new System.Windows.Forms.TextBox();
            this.Browse = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.BrowseDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.AddConnection = new System.Windows.Forms.Button();
            this.LabelConnStr = new System.Windows.Forms.Label();
            this.textBoxConnStr = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // XMLRadio
            // 
            this.XMLRadio.AutoSize = true;
            this.XMLRadio.Location = new System.Drawing.Point(23, 27);
            this.XMLRadio.Name = "XMLRadio";
            this.XMLRadio.Size = new System.Drawing.Size(73, 17);
            this.XMLRadio.TabIndex = 1;
            this.XMLRadio.Text = "XML store";
            this.XMLRadio.UseVisualStyleBackColor = true;
            this.XMLRadio.CheckedChanged += new System.EventHandler(this.XMLRadio_CheckedChanged);
            // 
            // SQLRadio
            // 
            this.SQLRadio.AutoSize = true;
            this.SQLRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SQLRadio.Location = new System.Drawing.Point(23, 86);
            this.SQLRadio.Name = "SQLRadio";
            this.SQLRadio.Size = new System.Drawing.Size(72, 17);
            this.SQLRadio.TabIndex = 5;
            this.SQLRadio.TabStop = true;
            this.SQLRadio.Text = "SQL store";
            this.SQLRadio.UseVisualStyleBackColor = true;
            this.SQLRadio.CheckedChanged += new System.EventHandler(this.SQLRadio_CheckedChanged);
            // 
            // Location
            // 
            this.Location.AutoSize = true;
            this.Location.Location = new System.Drawing.Point(44, 57);
            this.Location.Name = "Location";
            this.Location.Size = new System.Drawing.Size(84, 13);
            this.Location.TabIndex = 2;
            this.Location.Text = "Storage location";
            // 
            // LocationTxt
            // 
            this.LocationTxt.Location = new System.Drawing.Point(148, 53);
            this.LocationTxt.Name = "LocationTxt";
            this.LocationTxt.Size = new System.Drawing.Size(140, 20);
            this.LocationTxt.TabIndex = 3;
            this.LocationTxt.Text = "C:\\ComplexNetwork";
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(304, 52);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(82, 23);
            this.Browse.TabIndex = 4;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // Ok
            // 
            this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok.Location = new System.Drawing.Point(128, 191);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(74, 23);
            this.Ok.TabIndex = 14;
            this.Ok.Text = "OK";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(229, 191);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(74, 23);
            this.Cancel.TabIndex = 15;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // AddConnection
            // 
            this.AddConnection.Location = new System.Drawing.Point(303, 112);
            this.AddConnection.Name = "AddConnection";
            this.AddConnection.Size = new System.Drawing.Size(82, 23);
            this.AddConnection.TabIndex = 16;
            this.AddConnection.Text = "Connections";
            this.AddConnection.UseVisualStyleBackColor = true;
            this.AddConnection.Click += new System.EventHandler(this.AddConnection_Click);
            // 
            // LabelConnStr
            // 
            this.LabelConnStr.AutoSize = true;
            this.LabelConnStr.Location = new System.Drawing.Point(43, 117);
            this.LabelConnStr.Name = "LabelConnStr";
            this.LabelConnStr.Size = new System.Drawing.Size(88, 13);
            this.LabelConnStr.TabIndex = 17;
            this.LabelConnStr.Text = "ConnectionString";
            // 
            // textBoxConnStr
            // 
            this.textBoxConnStr.Location = new System.Drawing.Point(148, 113);
            this.textBoxConnStr.Name = "textBoxConnStr";
            this.textBoxConnStr.Size = new System.Drawing.Size(140, 20);
            this.textBoxConnStr.TabIndex = 18;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SQLRadio);
            this.groupBox1.Controls.Add(this.textBoxConnStr);
            this.groupBox1.Controls.Add(this.XMLRadio);
            this.groupBox1.Controls.Add(this.LabelConnStr);
            this.groupBox1.Controls.Add(this.Browse);
            this.groupBox1.Controls.Add(this.AddConnection);
            this.groupBox1.Controls.Add(this.Location);
            this.groupBox1.Controls.Add(this.LocationTxt);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 161);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose data store";
            // 
            // StartUpWindow
            // 
            this.AcceptButton = this.Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(430, 226);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartUpWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose Data Store";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton XMLRadio;
        private System.Windows.Forms.RadioButton SQLRadio;
        private System.Windows.Forms.Label Location;
        private System.Windows.Forms.TextBox LocationTxt;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.FolderBrowserDialog BrowseDlg;
        private System.Windows.Forms.Button AddConnection;
        private System.Windows.Forms.Label LabelConnStr;
        private System.Windows.Forms.TextBox textBoxConnStr;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}