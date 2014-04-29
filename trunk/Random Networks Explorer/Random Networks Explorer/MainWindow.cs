using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Session;
using Core;
using Core.Exceptions;
using Core.Enumerations;

namespace RandomNetworksExplorer
{
    public partial class MainWindow : Form
    {
        private static List<Guid> researchIDs = new List<Guid>();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void newBasicMenuItem_Click(object sender, EventArgs e)
        {
            AddResearch(ResearchType.Basic);
        }

        private void newEvolutionMenuItem_Click(object sender, EventArgs e)
        {
            AddResearch(ResearchType.Evolution);
        }

        private void newPercolationMenuItem_Click(object sender, EventArgs e)
        {
            AddResearch(ResearchType.Percolation);
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog(this);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            if (SessionManager.ExistsAnyRunningResearch())
            {
                DialogResult res = MessageBox.Show("There are running researches. \nDo you want to abort them and close?",
                       "Warning",
                       MessageBoxButtons.OKCancel);
                if (DialogResult.OK == res)
                {
                    SessionManager.StopAllRunningResearches();
                    Application.Exit();
                }
                else if (DialogResult.Cancel == res)
                    return;
            }
            else
                Application.Exit();
        }

        private void modelCheckingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelCheckWindow modelCheckWnd = new ModelCheckWindow();
            modelCheckWnd.ShowDialog();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow(@"HELP/main.html");
            helpWindow.Show();
        }

        private void deleteResearchMenuItem_Click(object sender, EventArgs e)
        {
            RemoveResearch(researchesTable.SelectedRows[0]);
        }

        private void cloneResearchMenuItem_Click(object sender, EventArgs e)
        {
            CloneResearch(researchesTable.SelectedRows[0]);
        }

        private void researchTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (researchesTable[e.ColumnIndex, e.RowIndex].OwningColumn.Name == "storageColumn")
            {
                StorageSettingsWindow storageSettingsDlg = new StorageSettingsWindow();
                if (storageSettingsDlg.ShowDialog() == DialogResult.OK)
                {
                    DataGridViewButtonCell btn = researchesTable.Rows[e.RowIndex].Cells["storageColumn"] as DataGridViewButtonCell;
                    btn.Value = storageSettingsDlg.StorageType.ToString();

                    string storageString = null;
                    switch (storageSettingsDlg.StorageType)
                    {
                        case StorageType.XMLStorage:
                            storageString = storageSettingsDlg.XmlOutputDirectory;
                            break;
                        case StorageType.TXTStorage:
                            storageString = storageSettingsDlg.TxtOutputDirectory;
                            break;
                        case StorageType.SQLStorage:
                            storageString = storageSettingsDlg.SqlConnectionString;
                            break;
                        default:
                            break;
                    }

                    SessionManager.SetStorage(researchIDs[e.RowIndex],
                        storageSettingsDlg.StorageType,
                        storageString);
                }
            }
        }

        private void researchTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (researchesTable[e.ColumnIndex, e.RowIndex].OwningColumn.Name == "tracingColumn")
            {
                DataGridViewCheckBoxCell cell = researchesTable[e.ColumnIndex, e.RowIndex] as
                    DataGridViewCheckBoxCell;
                if ((bool)(cell.Value) == true)
                {
                    if (browserDlg.ShowDialog() == DialogResult.OK)
                    {
                        SessionManager.SetTracingPath(researchIDs[e.RowIndex],
                            browserDlg.SelectedPath);
                    }
                }
                else
                    SessionManager.SetTracingPath(researchIDs[e.RowIndex], "");
            }
        }

        private void researchTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (researchesTable.IsCurrentCellDirty)
            {
                researchesTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void researchesTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (researchesTable[e.ColumnIndex, e.RowIndex].OwningColumn.Name)
            {
                case "namaColumn":
                    SessionManager.SetResearchName(researchIDs[e.RowIndex],
                        researchesTable[e.ColumnIndex, e.RowIndex].Value.ToString());
                    break;
                case "modelColumn":
                    SessionManager.SetModelType(researchIDs[e.RowIndex],
                        (ModelType)Enum.Parse(typeof(ModelType), 
                        researchesTable[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;
                case "generationColumn":
                    // TODO change generation mode
                    break;
                default:
                    break;
            }
        }

        private void researchTable_SelectionChanged(object sender, EventArgs e)
        {
            if (researchesTable.SelectedRows.Count > 0)
            {
                int currentRowIndex = researchesTable.SelectedRows[0].Index;
            }
        }

        private void researchTable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }
            researchesTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            researchesTable.EndEdit();
            DataGridView.HitTestInfo hit = researchesTable.HitTest(e.X, e.Y);
            if (hit.RowIndex != -1)
            {
                researchesTable.Rows[hit.RowIndex].Selected = true;
                researchTableCSM.Items["deleteResearch"].Enabled = true;
                researchTableCSM.Items["cloneResearch"].Enabled = true;
            }
            else
            {
                researchTableCSM.Items["deleteResearch"].Enabled =  false;
                researchTableCSM.Items["cloneResearch"].Enabled = false;
            }
            researchTableCSM.Show(researchesTable, e.X, e.Y);
        }

        private void generationParametersTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void analyzeOptionsTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void startResearch_Click(object sender, EventArgs e)
        {
            int currentResearchIndex = researchesTable.SelectedRows[0].Index;
            SessionManager.StartResearch(researchIDs[currentResearchIndex]);
        }

        private void stopResearch_Click(object sender, EventArgs e)
        {
            int currentResearchIndex = researchesTable.SelectedRows[0].Index;
            SessionManager.StartResearch(researchIDs[currentResearchIndex]);
        }

        #endregion

        #region Utilities

        private void AddResearch(ResearchType type)
        {
            Guid id = SessionManager.CreateResearch(type);
            researchIDs.Add(id);
            FillGenerationParametersTable(id);
            FillAnalyzeOptionsTable(id);

            int newRowIndex = this.researchesTable.Rows.Add();
            DataGridViewRow newRow = researchesTable.Rows[newRowIndex];

            for (int i = 0; i < newRow.Cells.Count; ++i)
            {
                switch (newRow.Cells[i].OwningColumn.Name)
                {
                    case "researchColumn":
                        DataGridViewTextBoxCell comboCellR = newRow.Cells[i] as DataGridViewTextBoxCell;
                        comboCellR.Value = type.ToString();
                        break;
                    case "namaColumn":
                        newRow.Cells[i].Value = "DefaultResearchName";
                        break;
                    case "modelColumn": // TODO dinamyc fill model types for each research
                    case "generationColumn":
                        DataGridViewComboBoxCell comboCell = newRow.Cells[i] as DataGridViewComboBoxCell;
                        comboCell.Value = comboCell.Items[0];
                        break;
                    case "storageColumn":
                        DataGridViewButtonCell buttonCell = newRow.Cells[i] as DataGridViewButtonCell;
                        buttonCell.Value = "XMLStorage";
                        break;
                    case "statusColumn":
                        newRow.Cells[i].Value = "Not Started";
                        break;
                    default:
                        break;
                }
            }

            researchesTable.CurrentCell = researchesTable.Rows[newRowIndex].Cells["nameColumn"];
            researchesTable.BeginEdit(true);
        }

        private void RemoveResearch(DataGridViewRow rowToRemove)
        {
            try
            {
                SessionManager.StopResearch(researchIDs[rowToRemove.Index]);
            }
            catch (CoreException) { }
            SessionManager.DestroyResearch(researchIDs[rowToRemove.Index]);

            researchIDs.Remove(researchIDs[rowToRemove.Index]);
            researchesTable.Rows.Remove(rowToRemove);
        }

        private void CloneResearch(DataGridViewRow rowToClone)
        {
            researchIDs.Add(SessionManager.CloneResearch(researchIDs[rowToClone.Index]));

            int newRowIndex = this.researchesTable.Rows.Add();
            DataGridViewRow newRow = this.researchesTable.Rows[newRowIndex];

            for (int i = 0; i < newRow.Cells.Count; ++i)
            {
                switch (newRow.Cells[i].OwningColumn.Name)
                {
                    case "namaColumn":
                        newRow.Cells[i].Value = "DefaultResearchName";
                        break;
                    case "statusColumn":
                        newRow.Cells[i].Value = "Not Started";
                        break;
                    default:
                        newRow.Cells[i].Value = rowToClone.Cells[i].Value;
                        break;
                }
            }

            researchesTable.CurrentCell = researchesTable.Rows[newRowIndex].Cells["nameColumn"];
            researchesTable.BeginEdit(true);
        }

        private void FillGenerationParametersTable(Guid id)
        {
            generationParametersTable.Rows.Clear();

            Dictionary<GenerationParameter, object> gValues =
                SessionManager.GetGenerationParameterValues(id);
            foreach (GenerationParameter g in gValues.Keys)
            {
                if (gValues[g] != null)
                    generationParametersTable.Rows.Add(g.ToString(), gValues[g].ToString());
                else
                    generationParametersTable.Rows.Add(g.ToString(), 0);
            }

            Dictionary<ResearchParameter, object> rValues = 
                SessionManager.GetResearchParameterValues(id);
            foreach (ResearchParameter r in rValues.Keys)
            {
                if (rValues[r] != null)
                    generationParametersTable.Rows.Add(r.ToString(), rValues[r].ToString());
                else
                    generationParametersTable.Rows.Add(r.ToString(), 0);
            }
        }

        private void FillAnalyzeOptionsTable(Guid id)
        {
            analyzeOptionsTable.Rows.Clear();

            AnalyzeOption opts = SessionManager.GetAvailableAnalyzeOptions(id);
            Array existingOptions = Enum.GetValues(typeof(AnalyzeOption));
            foreach (AnalyzeOption opt in existingOptions)
            {
                if ((opts & opt) == opt)
                {
                    analyzeOptionsTable.Rows.Add(opt.ToString(), true);
                }
            }
        }

        #endregion
    }
}
