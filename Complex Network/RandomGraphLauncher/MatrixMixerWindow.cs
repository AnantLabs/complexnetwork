using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GenericAlgorithms;

namespace RandomGraphLauncher
{
    public partial class MatrixMixerWindow : Form
    {
        public MatrixMixerWindow()
        {
            InitializeComponent();
        }

        private void mix_Click(object sender, EventArgs e)
        {
            ArrayList matrix = MatrixFileReader.MatrixReader(this.filePathTxt.Text);

            int firstIndex = Convert.ToInt32(this.firstIndexTxt.Text);
            int secondIndex = Convert.ToInt32(this.secondIndexTxt.Text);
            List<int> firstNeighbours = new List<int>();
            List<int> secondNeighbours = new List<int>();

            ArrayList firstArr = (ArrayList)matrix[firstIndex];
            ArrayList secondArr = (ArrayList)matrix[secondIndex];
            for (int i = 0; i < matrix.Count; ++i)
            {
                if((bool)firstArr[i])
                    firstNeighbours.Add(i);
                if ((bool)secondArr[i])
                    secondNeighbours.Add(i);
            }

            for (int j = 0; j < matrix.Count; ++j)
            {
                if (firstNeighbours.Contains(j))
                    ((ArrayList)matrix[firstIndex])[j] = true;
                else
                    ((ArrayList)matrix[firstIndex])[j] = false;

                if (secondNeighbours.Contains(j))
                    ((ArrayList)matrix[secondIndex])[j] = true;
                else
                    ((ArrayList)matrix[secondIndex])[j] = false;
            }

            int strLength = this.filePathTxt.Text.Length - 4;
            string filePath = this.filePathTxt.Text.Substring(0, strLength)  + "_mix.txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                try
                {
                    for (int i = 0; i < matrix.Count; ++i)
                    {
                        ArrayList neighbourshipOfIVertex = (ArrayList)matrix[i];
                        for (int j = 0; j < neighbourshipOfIVertex.Count; ++j)
                        {
                            file.Write(Convert.ToInt32(neighbourshipOfIVertex[j]) + " ");
                        }
                        file.WriteLine("");
                    }
                }
                catch (Exception)
                {

                }
                finally
                {

                }
            }
        }

        private void browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.filePathTxt.Text = openFileDialog1.FileName;
            }
        }
    }
}
