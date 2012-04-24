using MyControlLibrary;
using System.Windows.Forms;
namespace RandomGraphLauncher
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distributedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainingModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traceingModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticAnalyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataExportIMportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userControl11 = new MyControlLibrary.TabCtlEx();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1028, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newJobToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newJobToolStripMenuItem
            // 
            this.newJobToolStripMenuItem.Name = "newJobToolStripMenuItem";
            this.newJobToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.newJobToolStripMenuItem.Text = "New Job";
            this.newJobToolStripMenuItem.Click += new System.EventHandler(this.newJobToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.storageToolStripMenuItem,
            this.distributedToolStripMenuItem,
            this.trainingModeToolStripMenuItem,
            this.traceingModeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // storageToolStripMenuItem
            // 
            this.storageToolStripMenuItem.Name = "storageToolStripMenuItem";
            this.storageToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.storageToolStripMenuItem.Text = "Storage";
            this.storageToolStripMenuItem.Click += new System.EventHandler(this.storageToolStripMenuItem_Click);
            // 
            // distributedToolStripMenuItem
            // 
            this.distributedToolStripMenuItem.Name = "distributedToolStripMenuItem";
            this.distributedToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.distributedToolStripMenuItem.Text = "Distributed";
            this.distributedToolStripMenuItem.Click += new System.EventHandler(this.distributedToolStripMenuItem_Click);
            // 
            // trainingModeToolStripMenuItem
            // 
            this.trainingModeToolStripMenuItem.CheckOnClick = true;
            this.trainingModeToolStripMenuItem.Name = "trainingModeToolStripMenuItem";
            this.trainingModeToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.trainingModeToolStripMenuItem.Text = "TrainingMode";
            this.trainingModeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.TrainingModeChanged);
            // 
            // traceingModeToolStripMenuItem
            // 
            this.traceingModeToolStripMenuItem.CheckOnClick = true;
            this.traceingModeToolStripMenuItem.Name = "traceingModeToolStripMenuItem";
            this.traceingModeToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.traceingModeToolStripMenuItem.Text = "Traceing Mode";
            this.traceingModeToolStripMenuItem.Click += new System.EventHandler(this.traceingModeToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statisticAnalyzerToolStripMenuItem,
            this.dataExportIMportToolStripMenuItem,
            this.testerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // statisticAnalyzerToolStripMenuItem
            // 
            this.statisticAnalyzerToolStripMenuItem.Name = "statisticAnalyzerToolStripMenuItem";
            this.statisticAnalyzerToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.statisticAnalyzerToolStripMenuItem.Text = "Statistic Analyzer";
            this.statisticAnalyzerToolStripMenuItem.Click += new System.EventHandler(this.statisticAnalyzerToolStripMenuItem_Click);
            // 
            // dataExportIMportToolStripMenuItem
            // 
            this.dataExportIMportToolStripMenuItem.Name = "dataExportIMportToolStripMenuItem";
            this.dataExportIMportToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.dataExportIMportToolStripMenuItem.Text = "Data Export/Import";
            this.dataExportIMportToolStripMenuItem.Click += new System.EventHandler(this.dataExportIMportToolStripMenuItem_Click);
            // 
            // testerToolStripMenuItem
            // 
            this.testerToolStripMenuItem.Name = "testerToolStripMenuItem";
            this.testerToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.testerToolStripMenuItem.Text = "Tester";
            this.testerToolStripMenuItem.Click += new System.EventHandler(this.testerToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // userControl11
            // 
            this.userControl11.ConfirmOnClose = false;
            this.userControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControl11.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.userControl11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userControl11.ItemSize = new System.Drawing.Size(330, 24);
            this.userControl11.Location = new System.Drawing.Point(0, 24);
            this.userControl11.Name = "userControl11";
            this.userControl11.SelectedIndex = 0;
            this.userControl11.Size = new System.Drawing.Size(1028, 578);
            this.userControl11.TabIndex = 0;
            this.userControl11.TabStop = false;
            this.userControl11.OnClose += new MyControlLibrary.TabCtlEx.OnHeaderCloseDelegate(this.userControl11_OnClose);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1028, 602);
            this.Controls.Add(this.userControl11);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Complex Network";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing_Event);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;

        private MyControlLibrary.TabCtlEx userControl11;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem statisticAnalyzerToolStripMenuItem;
        private ToolStripMenuItem dataExportIMportToolStripMenuItem;
        private ToolStripMenuItem distributedToolStripMenuItem;
        private ToolStripMenuItem trainingModeToolStripMenuItem;
        private ToolStripMenuItem testerToolStripMenuItem;
        private ToolStripMenuItem traceingModeToolStripMenuItem;

    }
}