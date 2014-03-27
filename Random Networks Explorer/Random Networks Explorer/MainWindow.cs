using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Enumerations;

namespace RandomNetworksExplorer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
        }

        private void newAnalyzeMenuItem_Click(object sender, EventArgs e)
        {
            AddResearch(ResearchType.Basic);
        }

        private void newTrajectoryMenuItem_Click(object sender, EventArgs e)
        {
            AddResearch(ResearchType.Evolution);
        }

        private void newPercolationMenuItem_Click(object sender, EventArgs e)
        {
            AddResearch(ResearchType.Percolation);
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.ShowDialog(this);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void deleteResearchMenuItem_Click(object sender, EventArgs e)
        {
            this.researchTable.Rows.Remove(this.researchTable.SelectedRows[0]);
        }

        private void cloneResearchMenuItem_Click(object sender, EventArgs e)
        {
            int newRowIndex = this.researchTable.Rows.Add();
            DataGridViewRow r = this.researchTable.Rows[newRowIndex];

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
                        r.Cells[i].Value = researchTable.SelectedRows[0].Cells[i].Value;
                        break;
                }
            }

            researchTable.CurrentCell = researchTable.Rows[newRowIndex].Cells[1];
            researchTable.BeginEdit(true);
        }

        private void researchTable_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            /*if (researchesTable.Rows[e.RowIndex].Cells["nameColumn"].Value == null)
            {
                MessageBox.Show("Please, enter the research name.", "Error");

                researchesTable.CurrentCell = researchesTable.Rows[e.RowIndex].Cells["nameColumn"];
                researchesTable.BeginEdit(true);
            }*/
        }

        private void researchTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 &&
                e.ColumnIndex == researchTable.Rows[e.RowIndex].Cells["storageColumn"].ColumnIndex)
            {
                StorageSettings storageSettingsDlg = new StorageSettings();
                storageSettingsDlg.ShowDialog();
            }
        }

        private void researchTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (researchTable[e.ColumnIndex, e.RowIndex].OwningColumn.Name == "tracingColumn")
            {
                DataGridViewCheckBoxCell cell = researchTable[e.ColumnIndex, e.RowIndex] as
                    DataGridViewCheckBoxCell;
                if ((bool)(cell.Value) == true)
                {
                    folderBrowserDialog1.ShowDialog();
                }
            }
        }

        private void researchTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (researchTable.IsCurrentCellDirty)
            {
                researchTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void researchTable_SelectionChanged(object sender, EventArgs e)
        {
            if (researchTable.SelectedRows.Count > 0)
            {
                int currentRowIndex = researchTable.SelectedRows[0].Index;
            }
        }

        private void researchTable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }
            DataGridView.HitTestInfo hit = researchTable.HitTest(e.X, e.Y);
            if (hit.RowIndex != -1)
            {
                researchTable.CurrentCell = researchTable.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                researchTableCSM.Items["deleteResearch"].Enabled = true;
                researchTableCSM.Items["cloneResearch"].Enabled = true;
            }
            else
            {
                researchTableCSM.Items["deleteResearch"].Enabled =  false;
                researchTableCSM.Items["cloneResearch"].Enabled = false;
            }
            researchTableCSM.Show(researchTable, e.X, e.Y);
        }
        // Utilities

        void AddResearch(ResearchType type)
        {
            int newRowIndex = this.researchTable.Rows.Add();
            DataGridViewRow newRow = researchTable.Rows[newRowIndex];

            for (int i = 0; i < newRow.Cells.Count; ++i)
            {
                switch (newRow.Cells[i].OwningColumn.Name)
                {
                    case "researchColumn":
                        DataGridViewComboBoxCell comboCellR = newRow.Cells[i] as DataGridViewComboBoxCell;
                        comboCellR.Value = comboCellR.Items[(int)type];
                        break;
                    /*case "modelColumn":
                    case "generationColumn":
                        DataGridViewComboBoxCell comboCell = newRow.Cells[i] as DataGridViewComboBoxCell;
                        comboCell.Value = comboCell.Items[0];
                        break;*/
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

            researchTable.CurrentCell = researchTable.Rows[newRowIndex].Cells[1];
            researchTable.BeginEdit(true);
        }

    }
}
