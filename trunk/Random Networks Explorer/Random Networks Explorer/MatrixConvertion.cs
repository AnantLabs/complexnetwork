﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Core;
using Core.Utility;

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
            openFileDlg.InitialDirectory = Settings.StaticGenerationDirectory;
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                inputFileNameTxt.Text = openFileDlg.FileName;
            }
        }

        private void outputBrowse_Click(object sender, EventArgs e)
        {
            saveFileDlg.InitialDirectory = Settings.StaticGenerationDirectory;
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                outputFileNameTxt.Text = saveFileDlg.FileName;
            }
        }

        private void convert_Click(object sender, EventArgs e)
        {
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
        }
    }
}