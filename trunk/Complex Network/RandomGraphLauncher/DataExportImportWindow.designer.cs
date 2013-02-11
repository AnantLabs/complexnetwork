namespace RandomGraphLauncher
{
    partial class DataExportImportWindow
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
            this.mainTab = new System.Windows.Forms.TabControl();
            this.xmlOrSqlStoreTab = new System.Windows.Forms.TabPage();
            this.externalStoreTab = new System.Windows.Forms.TabPage();
            this.xmlStoreGrp = new System.Windows.Forms.GroupBox();
            this.xmlLocation = new System.Windows.Forms.Label();
            this.xmlLocationTxt = new System.Windows.Forms.TextBox();
            this.xmlBrowse = new System.Windows.Forms.Button();
            this.externalStoreGrp = new System.Windows.Forms.GroupBox();
            this.externalBrowse = new System.Windows.Forms.Button();
            this.externalLocation = new System.Windows.Forms.Label();
            this.externalLocationTxt = new System.Windows.Forms.TextBox();
            this.fromFileSql = new System.Windows.Forms.Button();
            this.sqlStoreGrp = new System.Windows.Forms.GroupBox();
            this.connectionString = new System.Windows.Forms.Label();
            this.connectionStringTxt = new System.Windows.Forms.TextBox();
            this.Connections = new System.Windows.Forms.Button();
            this.fromFileXml = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.xmlOrSqlStoreTab.SuspendLayout();
            this.externalStoreTab.SuspendLayout();
            this.xmlStoreGrp.SuspendLayout();
            this.externalStoreGrp.SuspendLayout();
            this.sqlStoreGrp.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
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
            this.groupBox2.Location = new System.Drawing.Point(6, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 80);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SQL store";
            // 
            // XML_into_SQL_Button
            // 
            this.XML_into_SQL_Button.Location = new System.Drawing.Point(25, 183);
            this.XML_into_SQL_Button.Name = "XML_into_SQL_Button";
            this.XML_into_SQL_Button.Size = new System.Drawing.Size(150, 46);
            this.XML_into_SQL_Button.TabIndex = 25;
            this.XML_into_SQL_Button.Text = "Export data from XML into SQL Database";
            this.XML_into_SQL_Button.UseVisualStyleBackColor = true;
            this.XML_into_SQL_Button.Click += new System.EventHandler(this.XML_into_SQL_Button_Click);
            // 
            // SQL_into_XML_Button
            // 
            this.SQL_into_XML_Button.Location = new System.Drawing.Point(247, 183);
            this.SQL_into_XML_Button.Name = "SQL_into_XML_Button";
            this.SQL_into_XML_Button.Size = new System.Drawing.Size(150, 46);
            this.SQL_into_XML_Button.TabIndex = 26;
            this.SQL_into_XML_Button.Text = "Export data from SQL datatbase into XML";
            this.SQL_into_XML_Button.UseVisualStyleBackColor = true;
            this.SQL_into_XML_Button.Click += new System.EventHandler(this.SQL_into_XML_Button_Click);
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.xmlOrSqlStoreTab);
            this.mainTab.Controls.Add(this.externalStoreTab);
            this.mainTab.Location = new System.Drawing.Point(12, 12);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(442, 270);
            this.mainTab.TabIndex = 27;
            // 
            // xmlOrSqlStoreTab
            // 
            this.xmlOrSqlStoreTab.Controls.Add(this.groupBox1);
            this.xmlOrSqlStoreTab.Controls.Add(this.SQL_into_XML_Button);
            this.xmlOrSqlStoreTab.Controls.Add(this.groupBox2);
            this.xmlOrSqlStoreTab.Controls.Add(this.XML_into_SQL_Button);
            this.xmlOrSqlStoreTab.Location = new System.Drawing.Point(4, 22);
            this.xmlOrSqlStoreTab.Name = "xmlOrSqlStoreTab";
            this.xmlOrSqlStoreTab.Padding = new System.Windows.Forms.Padding(3);
            this.xmlOrSqlStoreTab.Size = new System.Drawing.Size(434, 244);
            this.xmlOrSqlStoreTab.TabIndex = 0;
            this.xmlOrSqlStoreTab.Text = "XML or SQL store";
            this.xmlOrSqlStoreTab.UseVisualStyleBackColor = true;
            // 
            // externalStoreTab
            // 
            this.externalStoreTab.Controls.Add(this.xmlStoreGrp);
            this.externalStoreTab.Controls.Add(this.externalStoreGrp);
            this.externalStoreTab.Controls.Add(this.fromFileSql);
            this.externalStoreTab.Controls.Add(this.sqlStoreGrp);
            this.externalStoreTab.Controls.Add(this.fromFileXml);
            this.externalStoreTab.Location = new System.Drawing.Point(4, 22);
            this.externalStoreTab.Name = "externalStoreTab";
            this.externalStoreTab.Padding = new System.Windows.Forms.Padding(3);
            this.externalStoreTab.Size = new System.Drawing.Size(434, 244);
            this.externalStoreTab.TabIndex = 1;
            this.externalStoreTab.Text = "External store";
            this.externalStoreTab.UseVisualStyleBackColor = true;
            // 
            // xmlStoreGrp
            // 
            this.xmlStoreGrp.Controls.Add(this.xmlLocation);
            this.xmlStoreGrp.Controls.Add(this.xmlLocationTxt);
            this.xmlStoreGrp.Controls.Add(this.xmlBrowse);
            this.xmlStoreGrp.Location = new System.Drawing.Point(6, 89);
            this.xmlStoreGrp.Name = "xmlStoreGrp";
            this.xmlStoreGrp.Size = new System.Drawing.Size(200, 80);
            this.xmlStoreGrp.TabIndex = 31;
            this.xmlStoreGrp.TabStop = false;
            this.xmlStoreGrp.Text = "XML store";
            // 
            // xmlLocation
            // 
            this.xmlLocation.AutoSize = true;
            this.xmlLocation.Location = new System.Drawing.Point(6, 23);
            this.xmlLocation.Name = "xmlLocation";
            this.xmlLocation.Size = new System.Drawing.Size(73, 13);
            this.xmlLocation.TabIndex = 22;
            this.xmlLocation.Text = "XML Location";
            // 
            // xmlLocationTxt
            // 
            this.xmlLocationTxt.Location = new System.Drawing.Point(9, 39);
            this.xmlLocationTxt.Name = "xmlLocationTxt";
            this.xmlLocationTxt.Size = new System.Drawing.Size(96, 20);
            this.xmlLocationTxt.TabIndex = 23;
            this.xmlLocationTxt.Text = "C:\\ComplexNetwork";
            // 
            // xmlBrowse
            // 
            this.xmlBrowse.Location = new System.Drawing.Point(112, 36);
            this.xmlBrowse.Name = "xmlBrowse";
            this.xmlBrowse.Size = new System.Drawing.Size(82, 23);
            this.xmlBrowse.TabIndex = 21;
            this.xmlBrowse.Text = "Browse";
            this.xmlBrowse.UseVisualStyleBackColor = true;
            this.xmlBrowse.Click += new System.EventHandler(this.xmlBrowse_Click);
            // 
            // externalStoreGrp
            // 
            this.externalStoreGrp.Controls.Add(this.externalBrowse);
            this.externalStoreGrp.Controls.Add(this.externalLocation);
            this.externalStoreGrp.Controls.Add(this.externalLocationTxt);
            this.externalStoreGrp.Location = new System.Drawing.Point(6, 6);
            this.externalStoreGrp.Name = "externalStoreGrp";
            this.externalStoreGrp.Size = new System.Drawing.Size(413, 77);
            this.externalStoreGrp.TabIndex = 27;
            this.externalStoreGrp.TabStop = false;
            this.externalStoreGrp.Text = "External store";
            // 
            // externalBrowse
            // 
            this.externalBrowse.Location = new System.Drawing.Point(276, 29);
            this.externalBrowse.Name = "externalBrowse";
            this.externalBrowse.Size = new System.Drawing.Size(82, 23);
            this.externalBrowse.TabIndex = 4;
            this.externalBrowse.Text = "Browse";
            this.externalBrowse.UseVisualStyleBackColor = true;
            this.externalBrowse.Click += new System.EventHandler(this.externalBrowse_Click);
            // 
            // externalLocation
            // 
            this.externalLocation.AutoSize = true;
            this.externalLocation.Location = new System.Drawing.Point(16, 34);
            this.externalLocation.Name = "externalLocation";
            this.externalLocation.Size = new System.Drawing.Size(89, 13);
            this.externalLocation.TabIndex = 2;
            this.externalLocation.Text = "Directory location";
            // 
            // externalLocationTxt
            // 
            this.externalLocationTxt.Location = new System.Drawing.Point(120, 30);
            this.externalLocationTxt.Name = "externalLocationTxt";
            this.externalLocationTxt.Size = new System.Drawing.Size(140, 20);
            this.externalLocationTxt.TabIndex = 3;
            this.externalLocationTxt.Text = "C:\\ComplexNetwork";
            // 
            // fromFileSql
            // 
            this.fromFileSql.Location = new System.Drawing.Point(247, 183);
            this.fromFileSql.Name = "fromFileSql";
            this.fromFileSql.Size = new System.Drawing.Size(150, 46);
            this.fromFileSql.TabIndex = 30;
            this.fromFileSql.Text = "Import data from file into SQL datatbase";
            this.fromFileSql.UseVisualStyleBackColor = true;
            this.fromFileSql.Click += new System.EventHandler(this.fromFileSql_Click);
            // 
            // sqlStoreGrp
            // 
            this.sqlStoreGrp.Controls.Add(this.connectionString);
            this.sqlStoreGrp.Controls.Add(this.connectionStringTxt);
            this.sqlStoreGrp.Controls.Add(this.Connections);
            this.sqlStoreGrp.Location = new System.Drawing.Point(219, 89);
            this.sqlStoreGrp.Name = "sqlStoreGrp";
            this.sqlStoreGrp.Size = new System.Drawing.Size(200, 80);
            this.sqlStoreGrp.TabIndex = 28;
            this.sqlStoreGrp.TabStop = false;
            this.sqlStoreGrp.Text = "SQL store";
            // 
            // connectionString
            // 
            this.connectionString.AutoSize = true;
            this.connectionString.Location = new System.Drawing.Point(6, 23);
            this.connectionString.Name = "connectionString";
            this.connectionString.Size = new System.Drawing.Size(88, 13);
            this.connectionString.TabIndex = 22;
            this.connectionString.Text = "ConnectionString";
            // 
            // connectionStringTxt
            // 
            this.connectionStringTxt.Location = new System.Drawing.Point(9, 39);
            this.connectionStringTxt.Name = "connectionStringTxt";
            this.connectionStringTxt.Size = new System.Drawing.Size(96, 20);
            this.connectionStringTxt.TabIndex = 23;
            // 
            // Connections
            // 
            this.Connections.Location = new System.Drawing.Point(112, 36);
            this.Connections.Name = "Connections";
            this.Connections.Size = new System.Drawing.Size(82, 23);
            this.Connections.TabIndex = 21;
            this.Connections.Text = "Connections";
            this.Connections.UseVisualStyleBackColor = true;
            this.Connections.Click += new System.EventHandler(this.Connections_Click);
            // 
            // fromFileXml
            // 
            this.fromFileXml.Location = new System.Drawing.Point(25, 183);
            this.fromFileXml.Name = "fromFileXml";
            this.fromFileXml.Size = new System.Drawing.Size(150, 46);
            this.fromFileXml.TabIndex = 29;
            this.fromFileXml.Text = "Import data from file into XML";
            this.fromFileXml.UseVisualStyleBackColor = true;
            this.fromFileXml.Click += new System.EventHandler(this.fromFileXml_Click);
            // 
            // DataExportImportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 294);
            this.Controls.Add(this.mainTab);
            this.Name = "DataExportImportWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Export&Import";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.mainTab.ResumeLayout(false);
            this.xmlOrSqlStoreTab.ResumeLayout(false);
            this.externalStoreTab.ResumeLayout(false);
            this.xmlStoreGrp.ResumeLayout(false);
            this.xmlStoreGrp.PerformLayout();
            this.externalStoreGrp.ResumeLayout(false);
            this.externalStoreGrp.PerformLayout();
            this.sqlStoreGrp.ResumeLayout(false);
            this.sqlStoreGrp.PerformLayout();
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
        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage xmlOrSqlStoreTab;
        private System.Windows.Forms.TabPage externalStoreTab;
        private System.Windows.Forms.GroupBox externalStoreGrp;
        private System.Windows.Forms.Button externalBrowse;
        private System.Windows.Forms.Label externalLocation;
        private System.Windows.Forms.TextBox externalLocationTxt;
        private System.Windows.Forms.Button fromFileSql;
        private System.Windows.Forms.GroupBox sqlStoreGrp;
        private System.Windows.Forms.Label connectionString;
        private System.Windows.Forms.TextBox connectionStringTxt;
        private System.Windows.Forms.Button Connections;
        private System.Windows.Forms.Button fromFileXml;
        private System.Windows.Forms.GroupBox xmlStoreGrp;
        private System.Windows.Forms.Label xmlLocation;
        private System.Windows.Forms.TextBox xmlLocationTxt;
        private System.Windows.Forms.Button xmlBrowse;

    }
}