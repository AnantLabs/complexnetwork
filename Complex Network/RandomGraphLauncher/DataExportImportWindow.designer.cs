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
            this.xmlStorageLocationTxt = new System.Windows.Forms.TextBox();
            this.xmlStorageLocation = new System.Windows.Forms.Label();
            this.xmlBrowse = new System.Windows.Forms.Button();
            this.xmlStorageGrp = new System.Windows.Forms.GroupBox();
            this.sqlStorageConnTxt = new System.Windows.Forms.TextBox();
            this.sqlStorageConn = new System.Windows.Forms.Label();
            this.sqlConnection = new System.Windows.Forms.Button();
            this.sqlStorageGrp = new System.Windows.Forms.GroupBox();
            this.xmlToSql = new System.Windows.Forms.Button();
            this.sqlToXml = new System.Windows.Forms.Button();
            this.BrowseDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.mainTab = new System.Windows.Forms.TabControl();
            this.xmlOrSqlStoreTab = new System.Windows.Forms.TabPage();
            this.externalStoreTab = new System.Windows.Forms.TabPage();
            this.externalStoreGrp = new System.Windows.Forms.GroupBox();
            this.externalBrowse = new System.Windows.Forms.Button();
            this.externalStoreLocation = new System.Windows.Forms.Label();
            this.externalStoreLocationTxt = new System.Windows.Forms.TextBox();
            this.fromFileSql = new System.Windows.Forms.Button();
            this.sqlStoreGrp = new System.Windows.Forms.GroupBox();
            this.connectionString = new System.Windows.Forms.Label();
            this.connectionStringTxt = new System.Windows.Forms.TextBox();
            this.connections = new System.Windows.Forms.Button();
            this.xmlStorageGrp.SuspendLayout();
            this.sqlStorageGrp.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.xmlOrSqlStoreTab.SuspendLayout();
            this.externalStoreTab.SuspendLayout();
            this.externalStoreGrp.SuspendLayout();
            this.sqlStoreGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // xmlStorageLocationTxt
            // 
            this.xmlStorageLocationTxt.Location = new System.Drawing.Point(120, 30);
            this.xmlStorageLocationTxt.Name = "xmlStorageLocationTxt";
            this.xmlStorageLocationTxt.Size = new System.Drawing.Size(140, 20);
            this.xmlStorageLocationTxt.TabIndex = 3;
            this.xmlStorageLocationTxt.Text = "C:\\ComplexNetwork";
            // 
            // xmlStorageLocation
            // 
            this.xmlStorageLocation.AutoSize = true;
            this.xmlStorageLocation.Location = new System.Drawing.Point(16, 34);
            this.xmlStorageLocation.Name = "xmlStorageLocation";
            this.xmlStorageLocation.Size = new System.Drawing.Size(84, 13);
            this.xmlStorageLocation.TabIndex = 2;
            this.xmlStorageLocation.Text = "Storage location";
            // 
            // xmlBrowse
            // 
            this.xmlBrowse.Location = new System.Drawing.Point(276, 29);
            this.xmlBrowse.Name = "xmlBrowse";
            this.xmlBrowse.Size = new System.Drawing.Size(82, 23);
            this.xmlBrowse.TabIndex = 4;
            this.xmlBrowse.Text = "Browse";
            this.xmlBrowse.UseVisualStyleBackColor = true;
            this.xmlBrowse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // xmlStorageGrp
            // 
            this.xmlStorageGrp.Controls.Add(this.xmlBrowse);
            this.xmlStorageGrp.Controls.Add(this.xmlStorageLocation);
            this.xmlStorageGrp.Controls.Add(this.xmlStorageLocationTxt);
            this.xmlStorageGrp.Location = new System.Drawing.Point(6, 6);
            this.xmlStorageGrp.Name = "xmlStorageGrp";
            this.xmlStorageGrp.Size = new System.Drawing.Size(413, 77);
            this.xmlStorageGrp.TabIndex = 20;
            this.xmlStorageGrp.TabStop = false;
            this.xmlStorageGrp.Text = "XML store";
            // 
            // sqlStorageConnTxt
            // 
            this.sqlStorageConnTxt.Location = new System.Drawing.Point(121, 31);
            this.sqlStorageConnTxt.Name = "sqlStorageConnTxt";
            this.sqlStorageConnTxt.Size = new System.Drawing.Size(140, 20);
            this.sqlStorageConnTxt.TabIndex = 23;
            // 
            // sqlStorageConn
            // 
            this.sqlStorageConn.AutoSize = true;
            this.sqlStorageConn.Location = new System.Drawing.Point(16, 35);
            this.sqlStorageConn.Name = "sqlStorageConn";
            this.sqlStorageConn.Size = new System.Drawing.Size(88, 13);
            this.sqlStorageConn.TabIndex = 22;
            this.sqlStorageConn.Text = "ConnectionString";
            // 
            // sqlConnection
            // 
            this.sqlConnection.Location = new System.Drawing.Point(276, 30);
            this.sqlConnection.Name = "sqlConnection";
            this.sqlConnection.Size = new System.Drawing.Size(82, 23);
            this.sqlConnection.TabIndex = 21;
            this.sqlConnection.Text = "Connections";
            this.sqlConnection.UseVisualStyleBackColor = true;
            this.sqlConnection.Click += new System.EventHandler(this.sqlConnection_Click);
            // 
            // sqlStorageGrp
            // 
            this.sqlStorageGrp.Controls.Add(this.sqlStorageConn);
            this.sqlStorageGrp.Controls.Add(this.sqlStorageConnTxt);
            this.sqlStorageGrp.Controls.Add(this.sqlConnection);
            this.sqlStorageGrp.Location = new System.Drawing.Point(6, 89);
            this.sqlStorageGrp.Name = "sqlStorageGrp";
            this.sqlStorageGrp.Size = new System.Drawing.Size(413, 80);
            this.sqlStorageGrp.TabIndex = 24;
            this.sqlStorageGrp.TabStop = false;
            this.sqlStorageGrp.Text = "SQL store";
            // 
            // xmlToSql
            // 
            this.xmlToSql.Location = new System.Drawing.Point(25, 183);
            this.xmlToSql.Name = "xmlToSql";
            this.xmlToSql.Size = new System.Drawing.Size(150, 46);
            this.xmlToSql.TabIndex = 25;
            this.xmlToSql.Text = "Export data from XML into SQL Database";
            this.xmlToSql.UseVisualStyleBackColor = true;
            this.xmlToSql.Click += new System.EventHandler(this.xmlToSql_Click);
            // 
            // sqlToXml
            // 
            this.sqlToXml.Location = new System.Drawing.Point(247, 183);
            this.sqlToXml.Name = "sqlToXml";
            this.sqlToXml.Size = new System.Drawing.Size(150, 46);
            this.sqlToXml.TabIndex = 26;
            this.sqlToXml.Text = "Export data from SQL datatbase into XML";
            this.sqlToXml.UseVisualStyleBackColor = true;
            this.sqlToXml.Click += new System.EventHandler(this.sqlToXml_Click);
            // 
            // BrowseDlg
            // 
            this.BrowseDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
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
            this.xmlOrSqlStoreTab.Controls.Add(this.xmlStorageGrp);
            this.xmlOrSqlStoreTab.Controls.Add(this.sqlToXml);
            this.xmlOrSqlStoreTab.Controls.Add(this.sqlStorageGrp);
            this.xmlOrSqlStoreTab.Controls.Add(this.xmlToSql);
            this.xmlOrSqlStoreTab.Location = new System.Drawing.Point(4, 22);
            this.xmlOrSqlStoreTab.Name = "xmlOrSqlStoreTab";
            this.xmlOrSqlStoreTab.Padding = new System.Windows.Forms.Padding(3);
            this.xmlOrSqlStoreTab.Size = new System.Drawing.Size(434, 244);
            this.xmlOrSqlStoreTab.TabIndex = 0;
            this.xmlOrSqlStoreTab.Text = "XML/SQL store";
            this.xmlOrSqlStoreTab.UseVisualStyleBackColor = true;
            // 
            // externalStoreTab
            // 
            this.externalStoreTab.Controls.Add(this.externalStoreGrp);
            this.externalStoreTab.Controls.Add(this.fromFileSql);
            this.externalStoreTab.Controls.Add(this.sqlStoreGrp);
            this.externalStoreTab.Location = new System.Drawing.Point(4, 22);
            this.externalStoreTab.Name = "externalStoreTab";
            this.externalStoreTab.Padding = new System.Windows.Forms.Padding(3);
            this.externalStoreTab.Size = new System.Drawing.Size(434, 244);
            this.externalStoreTab.TabIndex = 1;
            this.externalStoreTab.Text = "External store";
            this.externalStoreTab.UseVisualStyleBackColor = true;
            // 
            // externalStoreGrp
            // 
            this.externalStoreGrp.Controls.Add(this.externalBrowse);
            this.externalStoreGrp.Controls.Add(this.externalStoreLocation);
            this.externalStoreGrp.Controls.Add(this.externalStoreLocationTxt);
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
            this.externalBrowse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // externalStoreLocation
            // 
            this.externalStoreLocation.AutoSize = true;
            this.externalStoreLocation.Location = new System.Drawing.Point(16, 34);
            this.externalStoreLocation.Name = "externalStoreLocation";
            this.externalStoreLocation.Size = new System.Drawing.Size(89, 13);
            this.externalStoreLocation.TabIndex = 2;
            this.externalStoreLocation.Text = "Directory location";
            // 
            // externalStoreLocationTxt
            // 
            this.externalStoreLocationTxt.Location = new System.Drawing.Point(120, 30);
            this.externalStoreLocationTxt.Name = "externalStoreLocationTxt";
            this.externalStoreLocationTxt.Size = new System.Drawing.Size(140, 20);
            this.externalStoreLocationTxt.TabIndex = 3;
            this.externalStoreLocationTxt.Text = "C:\\ComplexNetwork";
            // 
            // fromFileSql
            // 
            this.fromFileSql.Location = new System.Drawing.Point(25, 183);
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
            this.sqlStoreGrp.Controls.Add(this.connections);
            this.sqlStoreGrp.Location = new System.Drawing.Point(6, 89);
            this.sqlStoreGrp.Name = "sqlStoreGrp";
            this.sqlStoreGrp.Size = new System.Drawing.Size(413, 80);
            this.sqlStoreGrp.TabIndex = 28;
            this.sqlStoreGrp.TabStop = false;
            this.sqlStoreGrp.Text = "SQL store";
            // 
            // connectionString
            // 
            this.connectionString.AutoSize = true;
            this.connectionString.Location = new System.Drawing.Point(16, 35);
            this.connectionString.Name = "connectionString";
            this.connectionString.Size = new System.Drawing.Size(88, 13);
            this.connectionString.TabIndex = 22;
            this.connectionString.Text = "ConnectionString";
            // 
            // connectionStringTxt
            // 
            this.connectionStringTxt.Location = new System.Drawing.Point(121, 31);
            this.connectionStringTxt.Name = "connectionStringTxt";
            this.connectionStringTxt.Size = new System.Drawing.Size(139, 20);
            this.connectionStringTxt.TabIndex = 23;
            // 
            // connections
            // 
            this.connections.Location = new System.Drawing.Point(276, 30);
            this.connections.Name = "connections";
            this.connections.Size = new System.Drawing.Size(82, 23);
            this.connections.TabIndex = 21;
            this.connections.Text = "Connections";
            this.connections.UseVisualStyleBackColor = true;
            this.connections.Click += new System.EventHandler(this.sqlConnection_Click);
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
            this.xmlStorageGrp.ResumeLayout(false);
            this.xmlStorageGrp.PerformLayout();
            this.sqlStorageGrp.ResumeLayout(false);
            this.sqlStorageGrp.PerformLayout();
            this.mainTab.ResumeLayout(false);
            this.xmlOrSqlStoreTab.ResumeLayout(false);
            this.externalStoreTab.ResumeLayout(false);
            this.externalStoreGrp.ResumeLayout(false);
            this.externalStoreGrp.PerformLayout();
            this.sqlStoreGrp.ResumeLayout(false);
            this.sqlStoreGrp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox xmlStorageLocationTxt;
        private System.Windows.Forms.Label xmlStorageLocation;
        private System.Windows.Forms.Button xmlBrowse;
        private System.Windows.Forms.GroupBox xmlStorageGrp;
        private System.Windows.Forms.TextBox sqlStorageConnTxt;
        private System.Windows.Forms.Label sqlStorageConn;
        private System.Windows.Forms.Button sqlConnection;
        private System.Windows.Forms.GroupBox sqlStorageGrp;
        private System.Windows.Forms.Button xmlToSql;
        private System.Windows.Forms.Button sqlToXml;
        private System.Windows.Forms.FolderBrowserDialog BrowseDlg;
        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage xmlOrSqlStoreTab;
        private System.Windows.Forms.TabPage externalStoreTab;
        private System.Windows.Forms.GroupBox externalStoreGrp;
        private System.Windows.Forms.Button externalBrowse;
        private System.Windows.Forms.Label externalStoreLocation;
        private System.Windows.Forms.TextBox externalStoreLocationTxt;
        private System.Windows.Forms.Button fromFileSql;
        private System.Windows.Forms.GroupBox sqlStoreGrp;
        private System.Windows.Forms.Label connectionString;
        private System.Windows.Forms.TextBox connectionStringTxt;
        private System.Windows.Forms.Button connections;

    }
}