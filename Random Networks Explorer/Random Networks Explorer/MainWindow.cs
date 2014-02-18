using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RandomNetworksExplorer
{
    public partial class MainWindow : Form
    {
        private int currentResearchIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
        }

        private void analyzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddResearch(0);
        }

        private void trajectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddResearch(1);
        }

        private void percolationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddResearch(2);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newResearchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddResearch(0);
        }

        private void deleteResearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.researchesTable.Rows.Remove(this.researchesTable.SelectedRows[0]);
        }

        private void cloneResearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int newRowIndex = this.researchesTable.Rows.Add();
            DataGridViewRow r = this.researchesTable.Rows[newRowIndex];

            for (int i = 0; i < r.Cells.Count; ++i)
            {
                switch (r.Cells[i].OwningColumn.Name)
                {
                    case "nameColumn":
                        break;
                    case "statusColumn":
                        r.Cells[i].Value = "Not Started";
                        break;
                    default:
                        r.Cells[i].Value = researchesTable.SelectedRows[0].Cells[i].Value;
                        break;
                }
            }

            researchesTable.CurrentCell = researchesTable.Rows[newRowIndex].Cells[1];
            researchesTable.BeginEdit(true);
        }

        private void researchesTable_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            /*if (researchesTable.Rows[e.RowIndex].Cells["nameColumn"].Value == null)
            {
                MessageBox.Show("Please, enter the research name.", "Error");

                researchesTable.CurrentCell = researchesTable.Rows[e.RowIndex].Cells["nameColumn"];
                researchesTable.BeginEdit(true);
            }*/
        }

        private void researchesTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == researchesTable.Rows[e.RowIndex].Cells["storageColumn"].ColumnIndex)
            {
                StorageSettings storageSettingsDlg = new StorageSettings();
                storageSettingsDlg.ShowDialog();
            }
        }

        private void researchesTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (researchesTable[e.ColumnIndex, e.RowIndex].OwningColumn.Name == "tracingColumn")
            {
                DataGridViewCheckBoxCell cell = researchesTable[e.ColumnIndex, e.RowIndex] as
                    DataGridViewCheckBoxCell;
                if ((bool)(cell.Value) == true)
                {
                    folderBrowserDialog1.ShowDialog();
                }
            }
        }

        private void researchesTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (researchesTable.IsCurrentCellDirty)
            {
                researchesTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void researchesTable_SelectionChanged(object sender, EventArgs e)
        {
            if (researchesTable.SelectedRows.Count > 0)
            {
                int currentRowIndex = researchesTable.SelectedRows[0].Index;
            }
        }

        // Utilities

        void AddResearch(int type)
        {
            int newRowIndex = this.researchesTable.Rows.Add();
            DataGridViewRow newRow = researchesTable.Rows[newRowIndex];

            for (int i = 0; i < newRow.Cells.Count; ++i)
            {
                switch (newRow.Cells[i].OwningColumn.Name)
                {
                    case "researchColumn":
                        DataGridViewComboBoxCell comboCellR = newRow.Cells[i] as DataGridViewComboBoxCell;
                        comboCellR.Value = comboCellR.Items[type];
                        break;
                    case "modelColumn":
                    case "generationColumn":
                        DataGridViewComboBoxCell comboCell = newRow.Cells[i] as DataGridViewComboBoxCell;
                        comboCell.Value = comboCell.Items[0];
                        break;
                    case "storageColumn":
                        DataGridViewButtonCell buttonCell = newRow.Cells[i] as DataGridViewButtonCell;
                        buttonCell.Value = "XML Store";
                        break;
                    case "statusColumn":
                        newRow.Cells[i].Value = "Not Started";
                        break;
                    default:
                        break;
                }
            }

            researchesTable.CurrentCell = researchesTable.Rows[newRowIndex].Cells[1];
            researchesTable.BeginEdit(true);
        }
    }
}
