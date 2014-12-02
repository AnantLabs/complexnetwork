using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RandomNetworksExplorer
{
    public partial class ProbabilityCalculator : Form
    {
        private SortedDictionary<double, double> results = new SortedDictionary<double, double>();

        public ProbabilityCalculator()
        {
            InitializeComponent();
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            if ((branchingIndexTxt.Text.ToString() == "") ||
                (levelTxt.Text.ToString() == "") ||
                (minMuTxt.Text.ToString() == "") ||
                (maxMuTxt.Text.ToString() == "") ||
                (deltaTxt.Text.ToString() == ""))
            {
                MessageBox.Show("Some parameters are not specified.", "Error");
                return;
            }

            results.Clear();
            int p = int.Parse(branchingIndexTxt.Text.ToString());
            int level = int.Parse(levelTxt.Text.ToString());
            double mu = double.Parse(minMuTxt.Text.ToString());
            double muMax = double.Parse(maxMuTxt.Text.ToString());
            double delta = double.Parse(deltaTxt.Text.ToString());
            while (mu <= muMax)
            {
                results.Add(mu, CalculateProbability(p, level, mu));
                mu += delta;
            }

            FillResultsTable();

            save.Enabled = true;
        }

        private void save_Click(object sender, EventArgs e)
        {
            /*std::fstream rfile;
	        rfile.open ("result.txt");
	        std::vector<double>::const_iterator it = results.begin();
	        for(; it != results.end(); ++it)
	        {
		        rfile << *it << std::endl;
	        }
	        rfile.close();*/
        }

        private double CalculateProbability(int p, int level, double mu)
        {
	        double s = Math.Pow((double)p, (double)level);
	        double l = (s - 1) * p;
	        double coeff = (p - 1) / l;

	        double b = Math.Pow(p, (1 - mu));

	        double sum = 0;
	        for(int i = 1; i <= level; ++i)
	        {
		        sum += Math.Pow(b, i);
	        }

	        return coeff * sum;
        }

        private void FillResultsTable()
        {
            resultsTable.Rows.Clear();

            foreach (double m in results.Keys)
            {
                int newRowIndex = resultsTable.Rows.Add();
                DataGridViewRow row = resultsTable.Rows[newRowIndex];
                row.Cells["muColumn"].Value = m.ToString();
                row.Cells["probabilityColumn"].Value = results[m].ToString();
            }
        }
    }
}
