namespace RandomGraphLauncher
{
    partial class DataExportImport
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
            this.LocationTxt = new System.Windows.Forms.TextBox();
            this.Location = new System.Windows.Forms.Label();
            this.Browse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxConnStr = new System.Windows.Forms.TextBox();
            this.LabelConnStr = new System.Windows.Forms.Label();
            this.AddConnection = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.XML_into_SQL_Button = new System.Windows.Forms.Button();
            this.SQL_into_XML_Button = new System.Windows.Forms.Button();
            this.BrowseDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LocationTxt
            // 
            this.LocationTxt.Location = new System.Drawing.Point(120, 30);
            this.LocationTxt.Name = "LocationTxt";
            this.LocationTxt.Size = new System.Drawing.Size(140, 20);
            this.LocationTxt.TabIndex = 3;
            this.LocationTxt.Text = "C:\\ComplexNetwork";
            // 
            // Location
            // 
            this.Location.AutoSize = true;
            this.Location.Location = new System.Drawing.Point(16, 34);
            this.Location.Name = "Location";
            this.Location.Size = new System.Drawing.Size(84, 13);
            this.Location.TabIndex = 2;
            this.Location.Text = "Storage location";
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(276, 29);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(82, 23);
            this.Browse.TabIndex = 4;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Browse);
            this.groupBox1.Controls.Add(this.Location);
            this.groupBox1.Controls.Add(this.LocationTxt);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 77);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "XML store";
            // 
            // textBoxConnStr
            // 
            this.textBoxConnStr.Location = new System.Drawing.Point(121, 31);
            this.textBoxConnStr.Name = "textBoxConnStr";
            this.textBoxConnStr.Size = new System.Drawing.Size(140, 20);
            this.textBoxConnStr.TabIndex = 23;
            // 
            // LabelConnStr
            // 
            this.LabelConnStr.AutoSize = true;
            this.LabelConnStr.Location = new System.Drawing.Point(16, 35);
            this.LabelConnStr.Name = "LabelConnStr";
            this.LabelConnStr.Size = new System.Drawing.Size(88, 13);
            this.LabelConnStr.TabIndex = 22;
            this.LabelConnStr.Text = "ConnectionString";
            // 
            // AddConnection
            // 
            this.AddConnection.Location = new System.Drawing.Point(276, 30);
            this.AddConnection.Name = "AddConnection";
            this.AddConnection.Size = new System.Drawing.Size(82, 23);
            this.AddConnection.TabIndex = 21;
            this.AddConnection.Text = "Connections";
            this.AddConnection.UseVisualStyleBackColor = true;
            this.AddConnection.Click += new System.EventHandler(this.AddConnection_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LabelConnStr);
            this.groupBox2.Controls.Add(this.textBoxConnStr);
            this.groupBox2.Controls.Add(this.AddConnection);
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 80);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SQL store";
            // 
            // XML_into_SQL_Button
            // 
            this.XML_into_SQL_Button.Location = new System.Drawing.Point(31, 189);
            this.XML_into_SQL_Button.Name = "XML_into_SQL_Button";
            this.XML_into_SQL_Button.Size = new System.Drawing.Size(150, 46);
            this.XML_into_SQL_Button.TabIndex = 25;
            this.XML_into_SQL_Button.Text = "Export data from XML into SQL Database";
            this.XML_into_SQL_Button.UseVisualStyleBackColor = true;
            this.XML_into_SQL_Button.Click += new System.EventHandler(this.XML_into_SQL_Button_Click);
            // 
            // SQL_into_XML_Button
            // 
            this.SQL_into_XML_Button.Location = new System.Drawing.Point(220, 189);
            this.SQL_into_XML_Button.Name = "SQL_into_XML_Button";
            this.SQL_into_XML_Button.Size = new System.Drawing.Size(150, 46);
            this.SQL_into_XML_Button.TabIndex = 26;
            this.SQL_into_XML_Button.Text = "Export data from SQL datatbase into XML";
            this.SQL_into_XML_Button.UseVisualStyleBackColor = true;
            this.SQL_into_XML_Button.Click += new System.EventHandler(this.SQL_into_XML_Button_Click);
            // 
            // DataExportImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 258);
            this.Controls.Add(this.SQL_into_XML_Button);
            this.Controls.Add(this.XML_into_SQL_Button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DataExportImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Export&Import";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox LocationTxt;
        private System.Windows.Forms.Label Location;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxConnStr;
        private System.Windows.Forms.Label LabelConnStr;
        private System.Windows.Forms.Button AddConnection;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button XML_into_SQL_Button;
        private System.Windows.Forms.Button SQL_into_XML_Button;
        private System.Windows.Forms.FolderBrowserDialog BrowseDlg;

    }
}