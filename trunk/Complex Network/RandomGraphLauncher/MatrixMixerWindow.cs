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

        // Обработчики сообщений.

        private void MatrixMixerWindow_Load(object sender, EventArgs e)
        {
            this.percent.Enabled = false;
            this.percentTxt.Enabled = false;
            this.mixMatrix.Enabled = false;

            this.browse.Select();
        }

        private void browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.filePathTxt.Text = openFileDialog.FileName;
            }

            this.percent.Enabled = true;
            this.percentTxt.Enabled = true;
            this.mixMatrix.Enabled = true;

            this.percentTxt.SelectAll();
            this.percentTxt.Focus();
        }

        private void MixMatrix_Click(object sender, EventArgs e)
        {
            int percentValue = 0;
            try
            {
                percentValue = Convert.ToInt32(this.percentTxt.Text);

                if (percentValue < 0 || percentValue > 100)
                {
                    throw new SystemException();
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Percent must be non-negative and less then 100 natural number.", "Error");
                this.percentTxt.SelectAll();
                this.percentTxt.Focus();
                return;
            }

            ReadFromFile();

            int percent = Convert.ToInt32(this.percentTxt.Text);
            int changesCount = neighbourship.Count * percent / 100;
            int f = 0, s = 0;
            Random rand = new Random();

            for (int i = 0; i < changesCount; ++i)
            {
                f = rand.Next(0, neighbourship.Count);
                s = rand.Next(0, neighbourship.Count);

                MixTwoIndices(f, s);
            }
            
            WriteToFile();
        }

        private void MixTwoIndices(int firstIndex, int secondIndex)
        {
            List<int> firstNeighbours = neighbourship[firstIndex];
            List<int> secondNeighbours = neighbourship[secondIndex];
            if (firstNeighbours.Contains(secondIndex))   // данные вершины смежны
            {
                firstNeighbours.Remove(secondIndex);
                firstNeighbours.Add(firstIndex);
                secondNeighbours.Remove(firstIndex);
                secondNeighbours.Add(secondIndex);
            }
            neighbourship[firstIndex] = secondNeighbours;
            neighbourship[secondIndex] = firstNeighbours;

            for (int i = 0; i < firstNeighbours.Count; ++i) // проход по соседям второй вершины
            {
                if (firstNeighbours[i] != firstIndex)
                {
                    neighbourship[firstNeighbours[i]].Remove(firstIndex);
                    neighbourship[firstNeighbours[i]].Add(secondIndex);
                }
            }

            for (int j = 0; j < secondNeighbours.Count; ++j) // проход по соседям первой вершины
            {
                if (secondNeighbours[j] != secondIndex)
                {
                    neighbourship[secondNeighbours[j]].Remove(secondIndex);
                    neighbourship[secondNeighbours[j]].Add(firstIndex);
                }
            }
        }

        private void ReadFromFile()
        {
            ArrayList matrixArr = MatrixFileReader.MatrixReader(this.filePathTxt.Text);
            ArrayList neighbourshipOfIVertex = new ArrayList();
            for (int i = 0; i < matrixArr.Count; i++)
            {
                neighbourshipOfIVertex = (ArrayList)matrixArr[i];
                neighbourship[i] = new List<int>();
                for (int j = 0; j < matrixArr.Count; j++)
                    if ((bool)neighbourshipOfIVertex[j] == true && i != j)
                        neighbourship[i].Add(j);
            }
        }

        private void WriteToFile()
        {
            bool[,] matrix = new bool[neighbourship.Count, neighbourship.Count];

            for (int i = 0; i < neighbourship.Count; ++i)
                for (int j = 0; j < neighbourship.Count; ++j)
                    matrix[i, j] = false;

            List<int> list = new List<int>();

            for (int i = 0; i < neighbourship.Count; i++)
            {
                list = neighbourship[i];
                for (int j = 0; j < list.Count; j++)
                    matrix[i, list[j]] = true;
            }

            int strLength = this.filePathTxt.Text.Length - 4;
            string filePath = this.filePathTxt.Text.Substring(0, strLength) + "_mix.txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                try
                {
                    for (int i = 0; i < matrix.GetLength(0); ++i)
                    {
                        for (int j = 0; j < matrix.GetLength(1); ++j)
                        {
                            if (matrix[i, j])
                            {
                                file.Write(1 + " ");
                            }
                            else
                            {
                                file.Write(0 + " ");
                            }
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

        private SortedDictionary<int, List<int>> neighbourship = new SortedDictionary<int, List<int>>();
    }
}
