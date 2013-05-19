using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;
using System.Configuration;

using ResultStorage.Storage;

namespace StatisticAnalyzerUI
{
    // Реализация формы для ручного вызова оптимизаций баз данных.
    // "All Jobs" mode - вызов оптимизаций для всех (пока не оптимизорованных) job-ов.
    // "By Jobs" mode - вызов оптимизаций для сборки с данным именем job-а.
    public partial class DBOptimizer : Form
    {
        private SQLResultStorage sqlStorage;

        public DBOptimizer()
        {
            InitializeComponent();
        }

        // Обработчики сообщений.

        private void connectionsBtn_Click(object sender, EventArgs e)
        {
            DataConnectionDialog connectionDlg = new DataConnectionDialog();
            DataConnectionConfiguration dcs = new DataConnectionConfiguration(null);
            dcs.LoadConfiguration(connectionDlg);

            if (DataConnectionDialog.Show(connectionDlg) == DialogResult.OK)
            {
                this.connectionStringTxt.Text = connectionDlg.ConnectionString;
            }

            dcs.SaveConfiguration(connectionDlg);
            sqlStorage = new SQLResultStorage(new ConnectionStringSettings("temporaryconnection",
                    this.connectionStringTxt.Text, "System.Data.SqlClient"));
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            if (btn.Name == "AllJobsRadio")  // "All Jobs" mode
            {
                this.availableJobs.Enabled = false;
                this.availableJobsCmb.Enabled = false;
            }
            else if (btn.Name == "ByJobsRadio")  // "By Jobs" mode
            {
                this.availableJobs.Enabled = true;
                this.availableJobsCmb.Enabled = true;

                FillAvailableJobNames();
            }
        }

        private void startOptimizationBtn_Click(object sender, EventArgs e)
        {
            if (null == sqlStorage)
            {
                MessageBox.Show("Connection is not defined.", "Error");
                return;
            }

            if (this.AllJobsRadio.Checked)
            {
                try
                {
                    FreezeButtons(true);
                    sqlStorage.FillOptimizationTablesForAllJobs();
                    FreezeButtons(false);
                    MessageBox.Show("Optimizations for all jobs completed with success.", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Optimizations failed.", "Failed");
                }
            }
            else if (this.ByJobsRadio.Checked)
            {
                try
                {
                    FreezeButtons(true); 
                    sqlStorage.FillOptimizationTablesForCurrentJob(this.availableJobsCmb.Text);
                    FreezeButtons(false);
                    MessageBox.Show("Optimizations for job <" +
                        this.availableJobsCmb.Text +
                        "> completed with success.", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Optimizations for job <" +
                        this.availableJobsCmb.Text +
                        "> failed.", "Failed");
                }
            }
        }

        // Утилиты.

        // Добавление имен всех доступных job-ов в ComboBox availableJobsCmb.
        // Получаются имена job-ов из БД с определенным ConnectionString-ом.
        private void FillAvailableJobNames()
        {
            if (null == sqlStorage)
            {
                MessageBox.Show("Connection is not defined.", "Error");
            }
            else
            {
                sqlStorage.GetJobNames();

                this.availableJobsCmb.Text = "";

                this.availableJobsCmb.Items.Clear();
                this.availableJobsCmb.Items.AddRange(sqlStorage.GetJobNames());
                if (this.availableJobsCmb.Items.Count != 0)
                    this.availableJobsCmb.SelectedIndex = 0;
            }
        }

        private void FreezeButtons(bool freeze)
        {
            this.connectionsBtn.Enabled = !freeze;
            this.ByJobsRadio.Enabled = !freeze;
            this.AllJobsRadio.Enabled = !freeze;
            this.startOptimizationBtn.Enabled = !freeze;
        }
    }
}
