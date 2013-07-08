using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RandomGraphLauncher.Controllers;
using MyControlLibrary;
using RandomGraph.Settings;
using RandomGraph.Core.Manager.Impl;
using System.Diagnostics;

namespace RandomGraphLauncher
{
    // Реализация главной формы.
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            InitializeComponent();
        }

        // Обработчики сообщений.

        private void newJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modelChooserWindow modelChooserWnd = new modelChooserWindow();
            modelChooserWnd.ShowInTaskbar = false;

            if (modelChooserWnd.ShowDialog() == DialogResult.OK)
            {
                Options.InitializeLogManager();
                SessionController.AddJobToSession(modelChooserWnd.modelCmb.SelectedItem.ToString(),
                    modelChooserWnd.jobNameTxt.Text);
                InitializeNewJobTab(modelChooserWnd.jobNameTxt.Text);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsOptionsWindow settingsOptionsWnd = new SettingsOptionsWindow();
            settingsOptionsWnd.ShowInTaskbar = false;
            settingsOptionsWnd.ShowDialog();
        }

        private void dataExportIMportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataExportImportWindow dataExportImportWnd = new DataExportImportWindow();
            dataExportImportWnd.ShowDialog();
        }

        private void testerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TesterWindow testerWnd = new TesterWindow();
            testerWnd.ShowDialog();
        }

        private void modelCheckingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelCheckWindow modelCheckWnd = new ModelCheckWindow();
            modelCheckWnd.ShowDialog();
        }

        private void matrixMixerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixMixerWindow matrixMixerWnd = new MatrixMixerWindow();
            matrixMixerWnd.ShowDialog();
        }

        private void statisticAnalyzerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("StatisticAnalyzerUI.exe");
        }

        private void mainControl_OnClose(object sender, CloseEventArgs e)
        {
            TabPage tab = this.mainControl.TabPages[e.TabIndex];
            if (this.mainControl.TabPages.Count > 1)
            {
                SessionController.RemoveJobFromSession(tab.Text);
                this.mainControl.Controls.Remove(tab);
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void FormClosing_Event(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        // Утилиты.

        private void InitializeNewJobTab(string jobName)
        {
            TabPageEx tabPage = new MyControlLibrary.TabPageEx(new System.ComponentModel.Container());
            tabPage.SuspendLayout();
            this.mainControl.Controls.Add(tabPage);
            this.mainControl.SelectedTab = tabPage;
            // 
            // calculationControl
            // 
            CalculationControl calculationControl = new CalculationControl(jobName);
            calculationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            calculationControl.Location = new System.Drawing.Point(0, 0);
            calculationControl.Name = "calculationControl";
            calculationControl.Size = new System.Drawing.Size(1005, 457);
            calculationControl.TabIndex = 0;
            // 
            // tabPage
            // 
            tabPage.Controls.Add(calculationControl);
            tabPage.Location = new System.Drawing.Point(4, 28);
            tabPage.Menu = null;
            tabPage.Name = "tabPage";
            tabPage.Size = new System.Drawing.Size(1005, 457);
            tabPage.TabIndex = 1;
            tabPage.Text = jobName;
            tabPage.ResumeLayout(false);

            Width++;
        }
    }
}
