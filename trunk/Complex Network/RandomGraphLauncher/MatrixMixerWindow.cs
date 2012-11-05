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
            this.firstIndexTxt.Enabled = false;
            this.secondIndexTxt.Enabled = false;
            this.mix.Enabled = false;

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

            this.firstIndexTxt.Enabled = true;
            this.secondIndexTxt.Enabled = true;
            this.mix.Enabled = true;

            this.firstIndexTxt.SelectAll();
            this.firstIndexTxt.Focus();
        }

        private void mix_Click(object sender, EventArgs e)
        {
            int firstIndex = 0, secondIndex = 0;
            try
            {
                firstIndex = Convert.ToInt32(this.firstIndexTxt.Text);
                secondIndex = Convert.ToInt32(this.secondIndexTxt.Text);

                if (firstIndex < 0 || secondIndex < 0)
                {
                    throw new SystemException();
                }
            }
            catch(SystemException)
            {
                MessageBox.Show("Wrong Index.", "Error");
                this.firstIndexTxt.SelectAll();
                this.firstIndexTxt.Focus();
                return;
            }

            ArrayList matrixArr = MatrixFileReader.MatrixReader(this.filePathTxt.Text);
            SortedDictionary<int, List<int>> neighbourship = new SortedDictionary<int, List<int>>();
            ArrayList neighbourshipOfIVertex = new ArrayList();
            for (int i = 0; i < matrixArr.Count; i++)
            {
                neighbourshipOfIVertex = (ArrayList)matrixArr[i];
                neighbourship[i] = new List<int>();
                for (int j = 0; j < matrixArr.Count; j++)
                    if ((bool)neighbourshipOfIVertex[j] == true && i != j)
                        neighbourship[i].Add(j);
            }

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
            string filePath = this.filePathTxt.Text.Substring(0, strLength)  + "_mix.txt";
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
    }
}
