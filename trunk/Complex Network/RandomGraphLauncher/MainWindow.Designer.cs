using MyControlLibrary;
using System.Windows.Forms;
using System.Drawing;
namespace RandomGraphLauncher
{
    partial class mainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataExportIMportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelCheckingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matrixMixerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticAnalyzerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainControl = new MyControlLibrary.TabCtlEx();
            this.newResearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.statisticAnalyzerToolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1134, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newJobToolStripMenuItem,
            this.newResearchToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newJobToolStripMenuItem
            // 
            this.newJobToolStripMenuItem.Name = "newJobToolStripMenuItem";
            this.newJobToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newJobToolStripMenuItem.Text = "New Job";
            this.newJobToolStripMenuItem.Click += new System.EventHandler(this.newJobToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionToolStripMenuItem.Text = "Options";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataExportIMportToolStripMenuItem,
            this.testerToolStripMenuItem,
            this.modelCheckingToolStripMenuItem,
            this.matrixMixerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
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
            // modelCheckingToolStripMenuItem
            // 
            this.modelCheckingToolStripMenuItem.Name = "modelCheckingToolStripMenuItem";
            this.modelCheckingToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.modelCheckingToolStripMenuItem.Text = "Model Checking";
            this.modelCheckingToolStripMenuItem.Click += new System.EventHandler(this.modelCheckingToolStripMenuItem_Click);
            // 
            // matrixMixerToolStripMenuItem
            // 
            this.matrixMixerToolStripMenuItem.Name = "matrixMixerToolStripMenuItem";
            this.matrixMixerToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.matrixMixerToolStripMenuItem.Text = "Matrix Mixer";
            this.matrixMixerToolStripMenuItem.Click += new System.EventHandler(this.matrixMixerToolStripMenuItem_Click);
            // 
            // statisticAnalyzerToolStripMenuItem1
            // 
            this.statisticAnalyzerToolStripMenuItem1.Name = "statisticAnalyzerToolStripMenuItem1";
            this.statisticAnalyzerToolStripMenuItem1.Size = new System.Drawing.Size(108, 20);
            this.statisticAnalyzerToolStripMenuItem1.Text = "Statistic Analyzer";
            this.statisticAnalyzerToolStripMenuItem1.Click += new System.EventHandler(this.statisticAnalyzerToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // mainControl
            // 
            this.mainControl.ConfirmOnClose = false;
            this.mainControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainControl.ItemSize = new System.Drawing.Size(330, 24);
            this.mainControl.Location = new System.Drawing.Point(0, 24);
            this.mainControl.Name = "mainControl";
            this.mainControl.SelectedIndex = 0;
            this.mainControl.Size = new System.Drawing.Size(1134, 638);
            this.mainControl.TabIndex = 0;
            this.mainControl.TabStop = false;
            this.mainControl.Visible = false;
            this.mainControl.OnClose += new MyControlLibrary.TabCtlEx.OnHeaderCloseDelegate(this.mainControl_OnClose);
            // 
            // newResearchToolStripMenuItem
            // 
            this.newResearchToolStripMenuItem.Name = "newResearchToolStripMenuItem";
            this.newResearchToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newResearchToolStripMenuItem.Text = "New Research";
            this.newResearchToolStripMenuItem.Click += new System.EventHandler(this.newResearchToolStripMenuItem_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::RandomGraphLauncher.Properties.Resources.networkImage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1134, 662);
            this.Controls.Add(this.mainControl);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "mainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Complex Network";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing_Event);
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
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;

        private MyControlLibrary.TabCtlEx mainControl;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem dataExportIMportToolStripMenuItem;
        private ToolStripMenuItem testerToolStripMenuItem;
        private ToolStripMenuItem modelCheckingToolStripMenuItem;
        private ToolStripMenuItem optionToolStripMenuItem;
        private ToolStripMenuItem matrixMixerToolStripMenuItem;
        private ToolStripMenuItem statisticAnalyzerToolStripMenuItem1;
        private ToolStripMenuItem newResearchToolStripMenuItem;

    }
}