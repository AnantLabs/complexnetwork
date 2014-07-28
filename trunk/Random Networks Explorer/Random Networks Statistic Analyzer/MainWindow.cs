using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Storage;

namespace Random_Networks_Statistic_Analyzer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void MainWindow_Load(object sender, EventArgs e)
        {
            XMLResultStorage str = new XMLResultStorage("D:\\Disertation\\System (code)\\Last Version\\Random Networks Explorer\\RNE\\Results");
            str.LoadAllResearchInfo();
            InitializeResearchType();
        }

        private void loadFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFrom loadFromWindow = new LoadFrom();
            loadFromWindow.ShowDialog(this);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteResearchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectDeselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void researchTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void modelTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void refresh_Click(object sender, EventArgs e)
        {

        }

        private void researchesTable_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void color_Click(object sender, EventArgs e)
        {
            colorDlg.ShowDialog();
        }

        #endregion

        #region Utilities

        void InitializeResearchType()
        {
        }

        #endregion
    }
}
