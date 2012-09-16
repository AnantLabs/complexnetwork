using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RandomGraphLauncher.Controllers;

namespace RandomGraphLauncher
{
    // Реализация формы для выбора модели.
    public partial class modelChooserWindow : Form
    {
        public modelChooserWindow()
        {
            InitializeComponent();

            this.modelCmb.Items.AddRange(SessionController.GetAvailableModelNames().ToArray());
            if (this.modelCmb.Items.Count != 0)
                this.modelCmb.SelectedIndex = 0;

            this.ActiveControl = this.jobNameTxt;        
        }

        // Обработчики сообщений.
        
        private void OK_ButtonClick(object sender, EventArgs e)
        {
            if (this.jobNameTxt.Text == "")
            {
                MessageBox.Show("Please, enter the job name!");
                this.jobNameTxt.SelectAll();
                this.jobNameTxt.Focus();
            }
            else if (SessionController.CheckJobName(this.jobNameTxt.Text))
            {
                MessageBox.Show("There is a job with the same name.\nPlease, choose another name!");
                this.jobNameTxt.SelectAll();
                this.jobNameTxt.Focus();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
