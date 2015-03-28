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
using Core.Model;
using Core.Exceptions;
using Core.Enumerations;

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
            EnableControls(false);

            int size = int.Parse(sizeTxt.Text.ToString());
            try
            {
                MatrixInfoToRead matrix = FileManager.Read(inputFileNameTxt.Text, size, AdjacencyMatrixType.ClassicalMatrix);
                ArrayList m = matrix.Matrix;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(outputFileNameTxt.Text))
                {
                    ArrayList row = new ArrayList();
                    for (int i = 0; i < size - 1; ++i)
                    {
                        row = (ArrayList)m[i];
                        for (int j = i; j < size; ++j)
                        {
                            if ((bool)row[j] == true)
                            {
                                string s = csvRadioBtn.Checked ? "," : " ";
                                file.WriteLine(i.ToString() + s + j.ToString());
                            }
                        }
                    }
                }

                MessageBox.Show("Successfully converted connectivity matrix to degree-list.");
            }
            catch (MatrixFormatException)
            {
                MessageBox.Show("Input matrix format is not correct.", "Error");
                inputFileNameTxt.Focus();
                inputFileNameTxt.SelectAll();
                return;
            }
            finally
            {
                EnableControls(true);
            }
        }

        private void EnableControls(bool b)
        {
            inputFileNameTxt.Enabled = b;
            inputBrowse.Enabled = b;
            sizeTxt.Enabled = b;
            degreesRadioBtn.Enabled = b;
            csvRadioBtn.Enabled = b;
            outputFileNameTxt.Enabled = b;
            outputBrowse.Enabled = b;
            convert.Enabled = b;
        }
    }
}
