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

        private void MainWindow_Load(object sender, EventArgs e)
        {
            InitializeGenerationTypeColumn();
        }

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
            modelCheckWnd.Show();
        }

        private void dataConvertionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataConvertionsWindow dataConvertionsWnd = new DataConvertionsWindow();
            dataConvertionsWnd.Show();
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
            // cloning the specified research
            int rowIndex = researchesTable.SelectedRows[0].Index;
            Guid id = SessionManager.CloneResearch(researchIDs[rowIndex]);
            researchIDs.Add(id);

            // adding a new row for created research
            int newRowIndex = researchesTable.Rows.Add();

            FillResearchInformation(id, newRowIndex);

            researchesTable.CurrentCell = researchesTable.Rows[newRowIndex].Cells["nameColumn"];
            researchesTable.BeginEdit(true);
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

                    SessionManager.SetResearchStorage(researchIDs[e.RowIndex],
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
                        SessionManager.SetResearchTracingPath(researchIDs[e.RowIndex],
                            browserDlg.SelectedPath);
                    }
                }
                else
                    SessionManager.SetResearchTracingPath(researchIDs[e.RowIndex], "");
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
            DataGridViewCell editedCell = researchesTable[e.ColumnIndex, e.RowIndex];
            switch (editedCell.OwningColumn.Name)
            {
                case "namaColumn":
                    SessionManager.SetResearchName(researchIDs[e.RowIndex],
                        editedCell.Value.ToString());
                    break;
                case "modelColumn":
                    SessionManager.SetResearchModelType(researchIDs[e.RowIndex],
                        (ModelType)Enum.Parse(typeof(ModelType), editedCell.Value.ToString()));
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
                int selectedRowIndex = researchesTable.SelectedRows[0].Index;

                FillResearchInformation(researchIDs[selectedRowIndex], selectedRowIndex);
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
            if (generationParametersTable[e.ColumnIndex, e.RowIndex].OwningColumn.Name ==
                "generationParameterValueColumn")
            {
                int currentResearchIndex = researchesTable.SelectedRows[0].Index;
                GenerationParameter currentGenerationParameter = 
                    (GenerationParameter)Enum.Parse(typeof(GenerationParameter),
                    generationParametersTable["generationParameterNameColumn", e.RowIndex].Value.ToString());

                SessionManager.SetGenerationParameterValue(researchIDs[currentResearchIndex],
                    currentGenerationParameter, 
                    generationParametersTable[e.ColumnIndex, e.RowIndex].Value);
            }
        }

        private void analyzeOptionsTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (analyzeOptionsTable[e.ColumnIndex, e.RowIndex].OwningColumn.Name ==
                "analyzeOptionCheckedColumn")
            {
                int currentResearchIndex = researchesTable.SelectedRows[0].Index;
                AnalyzeOption currentAnalyzeOption = 
                    (AnalyzeOption)Enum.Parse(typeof(AnalyzeOption),
                    analyzeOptionsTable["analyzeOptionNameColumn", e.RowIndex].Value.ToString());
                AnalyzeOption opts = SessionManager.GetAnalyzeOptions(researchIDs[currentResearchIndex]);
              
                DataGridViewCheckBoxCell c = analyzeOptionsTable[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;
                if ((bool)(c.Value) == true)
                    opts |= currentAnalyzeOption;
                else
                    opts ^= currentAnalyzeOption;

                SessionManager.SetAnalyzeOptions(researchIDs[currentResearchIndex], opts);
            }
        }

        private void realizationCountTxt_ValueChanged(object sender, EventArgs e)
        {
            Guid researchId = researchIDs[researchesTable.SelectedRows[0].Index];
            SessionManager.SetResearchRealizationCount(researchId,
                (int)realizationCountTxt.Value);
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

        private enum GenerationType
        {
            Random,
            Static
        }

        private void InitializeGenerationTypeColumn()
        {
            DataGridViewComboBoxColumn generationColumn =
                researchesTable.Columns["generationColumn"] as DataGridViewComboBoxColumn;
            generationColumn.Items.Clear();
            string[] generationTypeNames = Enum.GetNames(typeof(GenerationType));
            for (int i = 0; i < generationTypeNames.Length; ++i)
                generationColumn.Items.Add(generationTypeNames[i]);
        }

        private void AddResearch(ResearchType type)
        {
            // creating a default research of specified type
            Guid id = SessionManager.CreateResearch(type);
            researchIDs.Add(id);

            // adding a new row for created research
            int newRowIndex = researchesTable.Rows.Add();

            FillResearchInformation(id, newRowIndex);

            researchesTable.CurrentCell = researchesTable.Rows[newRowIndex].Cells["nameColumn"];
            researchesTable.BeginEdit(true);
        }

        private void FillResearchInformation(Guid researchID, int rowIndex)
        {
            // filling specified research properties into researchesTable's specified row
            DataGridViewRow row = researchesTable.Rows[rowIndex];
            for (int i = 0; i < row.Cells.Count; ++i)
            {
                switch (row.Cells[i].OwningColumn.Name)
                {
                    case "researchColumn":
                        row.Cells[i].Value = SessionManager.GetResearchType(researchID).ToString();
                        break;
                    case "nameColumn":
                        row.Cells[i].Value = SessionManager.GetResearchName(researchID);
                        break;
                    case "modelColumn":
                        DataGridViewComboBoxCell comboCellM = row.Cells[i] as DataGridViewComboBoxCell;
                        comboCellM.Items.Clear();
                        foreach (ModelType m in SessionManager.GetAvailableModelTypes(researchID))
                            comboCellM.Items.Add(m.ToString());

                        comboCellM.Value = SessionManager.GetResearchModelType(researchID).ToString();
                        break;
                    case "storageColumn":
                        row.Cells[i].Value = SessionManager.GetResearchStorageType(researchID).ToString();
                        break;
                    case "generationColumn":
                        DataGridViewComboBoxCell comboCellG = row.Cells[i] as DataGridViewComboBoxCell;
                        comboCellG.Value = comboCellG.Items[0];
                        break;
                    case "tracingColumn":
                        DataGridViewCheckBoxCell checkCell = row.Cells[i] as DataGridViewCheckBoxCell;
                        if (SessionManager.GetResearchTracingPath(researchID) == "")
                            checkCell.Value = false;
                        else
                            checkCell.Value = true;
                        break;
                    case "statusColumn":
                        row.Cells[i].Value = SessionManager.GetResearchStatus(researchID).ToString();
                        break;
                    default:
                        break;
                }
            }

            // filling specified research generation parameters and analyze options
            FillGenerationParametersTable(researchID);
            FillAnalyzeOptionsTable(researchID);

            realizationCountTxt.Value = SessionManager.GetResearchRealizationCount(researchID);

            FillStatusTable(researchID);
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
                    generationParametersTable.Rows.Add(g.ToString());
            }

            Dictionary<ResearchParameter, object> rValues = 
                SessionManager.GetResearchParameterValues(id);
            foreach (ResearchParameter r in rValues.Keys)
            {
                if (rValues[r] != null)
                    generationParametersTable.Rows.Add(r.ToString(), rValues[r].ToString());
                else
                    generationParametersTable.Rows.Add(r.ToString());
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

        private void FillStatusTable(Guid id)
        {
        }

        #endregion        
    }
}
