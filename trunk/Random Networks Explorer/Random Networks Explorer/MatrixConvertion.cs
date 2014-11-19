using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Core;
using Core.Utility;
using Core.Settings;

namespace RandomNetworksExplorer
{
    public partial class MatrixConvertion : Form
    {
        public MatrixConvertion()
        {
            InitializeComponent();
        }

        private void inputBrowse_Click(object sender, EventArgs e)
        {
            openFileDlg.InitialDirectory = ExplorerSettings.MatrixConvertionToolDirectory;
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                ExplorerSettings.MatrixConvertionToolDirectory = Path.GetDirectoryName(openFileDlg.FileName);
                ExplorerSettings.Refresh();
                inputFileNameTxt.Text = openFileDlg.FileName;
                int l = inputFileNameTxt.Text.Length;
                outputFileNameTxt.Text = inputFileNameTxt.Text.Substring(0, l - 4) + "_out.txt";
            }
        }

        private void outputBrowse_Click(object sender, EventArgs e)
        {
            saveFileDlg.InitialDirectory = ExplorerSettings.MatrixConvertionToolDirectory;
            saveFileDlg.FileName = outputFileNameTxt.Text;
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                outputFileNameTxt.Text = saveFileDlg.FileName;
            }
        }

        private void convert_Click(object sender, EventArgs e)
        {
            convert.Enabled = false;

            ArrayList matrix;
            if (!FileManager.TryReadClassicalMatrix(inputFileNameTxt.Text, out matrix))
            {
                MessageBox.Show("Input matrix format is not correct.", "Error");
                inputFileNameTxt.Focus();
                inputFileNameTxt.SelectAll();
            }

            int size = matrix.Count;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(outputFileNameTxt.Text))
            {
                // Header.
                file.WriteLine("ModelName=ERModel");
                file.WriteLine("Vertices=" + size.ToString());
                file.WriteLine("-");

                ArrayList row = new ArrayList();
                for (int i = 0; i < matrix.Count - 1; ++i)
                {
                    row = (ArrayList)matrix[i];
                    for (int j = i; j < matrix.Count; ++j)
                    {
                        if ((bool)row[j] == true)
                            file.WriteLine(i.ToString() + " " + j.ToString());
                    }
                }
            }

            MessageBox.Show("Successfully converted connectivity matrix to degree-list.");
            convert.Enabled = true;
        }
    }
}
