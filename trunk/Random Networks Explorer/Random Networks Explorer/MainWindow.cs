using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

using Session;
using Core;
using Core.Exceptions;
using Core.Enumerations;
using Core.Attributes;
using Core.Events;
using Core.Settings;

namespace RandomNetworksExplorer
{
    public partial class MainWindow : Form
    {
        private static List<Guid> researchIDs = new List<Guid>();
        private int selectedIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        #region Event Handlers

        private void MainWindow_Load(object sender, EventArgs e)
        {
            InitializeStorageTypeColumn();
            InitializeGenerationTypeColumn();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckClosing())
                e.Cancel = true;
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
            if(CheckClosing())
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

        private void matrixConvertionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixConvertion matrixConvertionWnd = new MatrixConvertion();
            matrixConvertionWnd.Show();
        }

        private void probabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProbabilityCalculator probabilityCalculatorWnd = new ProbabilityCalculator();
            probabilityCalculatorWnd.Show();
        }

        private void statisticAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("Random Networks Statistic Analyzer.exe");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow(@"HELP/main.html");
            helpWindow.Show();
        }

        private void deleteResearchMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
                RemoveResearch(researchesTable.Rows[selectedIndex]);
        }

        private void cloneResearchMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
            {
                // cloning the specified research
                Guid id = SessionManager.CloneResearch(researchIDs[selectedIndex]);
                SessionManager.AddResearchUpdateHandler(id, CurrentResearch_OnResearchUpdateStatus);
                researchIDs.Add(id);

                // adding a new row for created research
                int newRowIndex = researchesTable.Rows.Add();

                selectedIndex = newRowIndex;
                FillResearchInformation(id);

                researchesTable.CurrentCell = researchesTable.Rows[newRowIndex].Cells["nameColumn"];
                researchesTable.BeginEdit(true);
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
                    SessionManager.SetResearchTracingPath(researchIDs[e.RowIndex],
                        ExplorerSettings.TracingDirectory);
                }
                else
                    SessionManager.SetResearchTracingPath(researchIDs[e.RowIndex], "");
                FillGenerationParametersTable(researchIDs[e.RowIndex]);
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
                case "nameColumn":
                    SessionManager.SetResearchName(researchIDs[e.RowIndex],
                        editedCell.Value.ToString());
                    break;
                case "modelColumn":
                    SessionManager.SetResearchModelType(researchIDs[e.RowIndex],
                        (ModelType)Enum.Parse(typeof(ModelType), editedCell.Value.ToString()));
                    FillGenerationParametersTable(researchIDs[e.RowIndex]);
                    FillAnalyzeOptionsTable(researchIDs[e.RowIndex]);
                    break;
                case "storageColumn":
                    StorageType stType = (StorageType)Enum.Parse(typeof(StorageType), 
                        editedCell.Value.ToString());
                    SessionManager.SetResearchStorage(researchIDs[e.RowIndex],
                        stType, RetrieveStorageString(stType));
                    break;
                case "generationColumn":
                    SessionManager.SetResearchGenerationType(researchIDs[e.RowIndex],
                        (GenerationType)Enum.Parse(typeof(GenerationType), editedCell.Value.ToString()));
                    FillGenerationParametersTable(researchIDs[e.RowIndex]);
                    SetRealizationCount(researchIDs[e.RowIndex]);
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

                if (selectedIndex != -1)
                {
                    SessionManager.RemoveResearchEnsembleUpdateHandler(researchIDs[selectedIndex],
                        CurrentResearch_OnResearchEnsembleUpdateStatus);
                }
                selectedIndex = selectedRowIndex;
                SessionManager.AddResearchEnsembleUpdateHandler(researchIDs[selectedIndex],
                    CurrentResearch_OnResearchEnsembleUpdateStatus);

                FillResearchInformation(researchIDs[selectedIndex]);
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
            DataGridViewCell editedCell = generationParametersTable[e.ColumnIndex, e.RowIndex];
            if (editedCell.OwningColumn.Name == "generationParameterValueColumn")
            {
                string parameterName = generationParametersTable.Rows[e.RowIndex].
                    Cells["generationParameterNameColumn"].Value.ToString();

                if (Enum.IsDefined(typeof(GenerationParameter), parameterName))
                {
                    GenerationParameter currentGenerationParameter =
                        (GenerationParameter)Enum.Parse(typeof(GenerationParameter),
                        parameterName);

                    SessionManager.SetGenerationParameterValue(researchIDs[selectedIndex],
                        currentGenerationParameter, editedCell.Value);
                }
                else if (Enum.IsDefined(typeof(ResearchParameter), parameterName))
                {
                    ResearchParameter currentResearchParameter =
                        (ResearchParameter)Enum.Parse(typeof(ResearchParameter),
                        parameterName);

                    SessionManager.SetResearchParameterValue(researchIDs[selectedIndex],
                        currentResearchParameter, editedCell.Value);
                }
                else
                {
                    SessionManager.SetExtendedInformationValue(researchIDs[selectedIndex],
                        parameterName, editedCell.Value.ToString());
                }
            }
        }

        private void generationParametersTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (generationParametersTable[e.ColumnIndex, e.RowIndex].OwningColumn.Name == "generationParameterValueColumn"
                && generationParametersTable[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell)
            {
                openFileDlg.InitialDirectory = ExplorerSettings.StaticGenerationDirectory;
                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    ExplorerSettings.StaticGenerationDirectory = Path.GetDirectoryName(openFileDlg.FileName);
                    ExplorerSettings.Refresh();
                    generationParametersTable[e.ColumnIndex, e.RowIndex].Value = openFileDlg.FileName;
                }

                SessionManager.SetGenerationParameterValue(researchIDs[selectedIndex],
                        GenerationParameter.AdjacencyMatrixFile, 
                        generationParametersTable[e.ColumnIndex, e.RowIndex].Value);
            }
        }

        private void analyzeOptionsTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (analyzeOptionsTable[e.ColumnIndex, e.RowIndex].OwningColumn.Name ==
                "analyzeOptionCheckedColumn")
            {
                AnalyzeOption currentAnalyzeOption =
                    (AnalyzeOption)Enum.Parse(typeof(AnalyzeOption),
                    analyzeOptionsTable["analyzeOptionNameColumn", e.RowIndex].Value.ToString());
                AnalyzeOption opts = SessionManager.GetAnalyzeOptions(researchIDs[selectedIndex]);

                DataGridViewCheckBoxCell c = analyzeOptionsTable[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;
                if ((bool)(c.Value) == true)
                    opts |= currentAnalyzeOption;
                else
                    opts &= ~currentAnalyzeOption;

                SessionManager.SetAnalyzeOptions(researchIDs[selectedIndex], opts);
            }
        }

        private void analyzeOptionsTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (analyzeOptionsTable.IsCurrentCellDirty)
            {
                analyzeOptionsTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in analyzeOptionsTable.Rows)
            {
                DataGridViewCheckBoxCell cell = r.Cells["analyzeOptionCheckedColumn"] as DataGridViewCheckBoxCell;
                cell.Value = true;
            }
        }

        private void deselectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in analyzeOptionsTable.Rows)
            {
                DataGridViewCheckBoxCell cell = r.Cells["analyzeOptionCheckedColumn"] as DataGridViewCheckBoxCell;
                cell.Value = false;
            }
        }

        private void realizationCountTxt_ValueChanged(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
            {
                Guid researchId = researchIDs[selectedIndex];
                SessionManager.SetResearchRealizationCount(researchId,
                    (int)realizationCountTxt.Value);
            }
        }

        private void startResearch_Click(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
            {
                foreach (DataGridViewRow row in generationParametersTable.Rows)
                {
                    if (row.Cells["generationParameterValueColumn"].Value == null)
                    {
                        MessageBox.Show("Parameters are not set correctly.", "Error");
                        return;
                    }
                    else
                        continue;
                }

                Guid researchId = researchIDs[selectedIndex];
                if (SessionManager.GetResearchGenerationType(researchId) == GenerationType.Static)
                {
                    GenerationParameter g = GenerationParameter.AdjacencyMatrixFile;
                    Core.Model.MatrixInfoToRead matrix = Core.Utility.FileManager.Read(
                        SessionManager.GetGenerationParameterValues(researchId)[g].ToString(), 
                        int.Parse(SessionManager.GetExtendedInformation(researchId)["Size"]), 
                        (AdjacencyMatrixType)Enum.Parse(typeof(AdjacencyMatrixType), SessionManager.GetExtendedInformation(researchId)["Matrix Type"]));
                    SessionManager.SetGenerationParameterValue(researchId, GenerationParameter.AdjacencyMatrix, matrix);
                }

                SessionManager.StartResearch(researchId);
                FillStatusTableOnStart(researchId);
                DisableButtons(false);
            }
        }

        private void stopResearch_Click(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
            {
                SessionManager.StopResearch(researchIDs[selectedIndex]);
                stopResearch.Enabled = false;
            }
        }

        #endregion

        #region Utilities

        private void InitializeStorageTypeColumn()
        {
            DataGridViewComboBoxColumn storageColumn =
                researchesTable.Columns["storageColumn"] as DataGridViewComboBoxColumn;
            storageColumn.Items.Clear();
            string[] storageTypeNames = Enum.GetNames(typeof(StorageType));
            for (int i = 0; i < storageTypeNames.Length; ++i)
                storageColumn.Items.Add(storageTypeNames[i]);
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

        private bool CheckClosing()
        {
            if (SessionManager.ExistsAnyRunningResearch())
            {
                DialogResult res = MessageBox.Show("There are running researches. \nDo you want to abort them and close?",
                       "Warning",
                       MessageBoxButtons.OKCancel);
                if (DialogResult.OK == res)
                {
                    SessionManager.StopAllRunningResearches();
                    return true;
                }
                else if (DialogResult.Cancel == res)
                    return false;
            }

            return true;
        }

        private void AddResearch(ResearchType type)
        {
            // creating a default research of specified type
            Guid id = SessionManager.CreateResearch(type);
            SessionManager.AddResearchUpdateHandler(id, CurrentResearch_OnResearchUpdateStatus);
            researchIDs.Add(id);

            // adding a new row for created research
            int newRowIndex = researchesTable.Rows.Add();

            selectedIndex = newRowIndex;
            FillResearchInformation(id);

            researchesTable.CurrentCell = researchesTable.Rows[newRowIndex].Cells["nameColumn"];
            researchesTable.BeginEdit(true);
        }

        private void FillResearchInformation(Guid researchId)
        {
            // filling specified research properties into researchesTable's specified row
            DataGridViewRow row = researchesTable.Rows[selectedIndex];
            for (int i = 0; i < row.Cells.Count; ++i)
            {
                switch (row.Cells[i].OwningColumn.Name)
                {
                    case "researchColumn":
                        row.Cells[i].Value = SessionManager.GetResearchType(researchId).ToString();
                        break;
                    case "nameColumn":
                        row.Cells[i].Value = SessionManager.GetResearchName(researchId);
                        break;
                    case "modelColumn":
                        DataGridViewComboBoxCell comboCellM = row.Cells[i] as DataGridViewComboBoxCell;
                        comboCellM.Items.Clear();
                        foreach (ModelType m in SessionManager.GetAvailableModelTypes(researchId))
                            comboCellM.Items.Add(m.ToString());

                        comboCellM.Value = SessionManager.GetResearchModelType(researchId).ToString();
                        break;
                    case "storageColumn":
                        row.Cells[i].Value = SessionManager.GetResearchStorageType(researchId).ToString();
                        break;
                    case "generationColumn":
                        row.Cells[i].Value = SessionManager.GetResearchGenerationType(researchId).ToString();
                        break;
                    case "tracingColumn":
                        DataGridViewCheckBoxCell checkCell = row.Cells[i] as DataGridViewCheckBoxCell;
                        if (SessionManager.GetResearchTracingPath(researchId) == "")
                            checkCell.Value = false;
                        else
                            checkCell.Value = true;
                        break;
                    case "statusColumn":
                        row.Cells[i].Value = SessionManager.GetResearchStatus(researchId).ToString();
                        stopResearch.Enabled = (ResearchStatus.Running.ToString() == row.Cells[i].Value.ToString());
                        break;
                    default:
                        break;
                }
            }

            // filling specified research generation parameters and analyze options
            FillGenerationParametersTable(researchId);
            FillAnalyzeOptionsTable(researchId);
            SetRealizationCount(researchId);
            FillStatusTable(researchId);

            if (SessionManager.GetResearchStatus(researchId) == ResearchStatus.NotStarted)
                DisableButtons(true);
            else
                DisableButtons(false);
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
            if(researchIDs.Count != 0)
                selectedIndex = 0;
            else
                selectedIndex = -1;
            researchesTable.Rows.Remove(rowToRemove);

            if (researchesTable.Rows.Count == 0)
            {
                generationParametersTable.Rows.Clear();
                analyzeOptionsTable.Rows.Clear();
                realizationCountTxt.Value = 1;
                statusTable.Rows.Clear();
                selectedIndex = -1;
            }
        }

        private String RetrieveStorageString(StorageType stType)
        {
            switch (stType)
            {
                case StorageType.XMLStorage:
                case StorageType.TXTStorage:
                case StorageType.ExcelStorage:
                    return ExplorerSettings.StorageDirectory;
                case StorageType.SQLStorage:
                    return null; //Settings.ConnectionString;
                default:
                    return null;
            }
        }

        private void FillGenerationParametersTable(Guid researchId)
        {
            generationParametersTable.Rows.Clear();
            
            if (SessionManager.GetResearchGenerationType(researchId) == GenerationType.Static)
            {
                FillStaticGenerationParameters(researchId);
            }
            else
            {
                FillRandomGenerationParameters(researchId);
            }
        }

        private void FillStaticGenerationParameters(Guid researchId)
        {
            Dictionary<GenerationParameter, object> gValues =
                SessionManager.GetGenerationParameterValues(researchId);
            Dictionary<ResearchParameter, object> rValues =
                SessionManager.GetResearchParameterValues(researchId);
            Dictionary<String, String> eValues =
                SessionManager.GetExtendedInformation(researchId);

            GenerationParameter g = GenerationParameter.AdjacencyMatrixFile;
            AddGenerationParameterAsButton(g.ToString(),
                gValues[g] != null ? gValues[g].ToString() : "Browse");
            AddGenerationParameterAsTextBox("Size", eValues.ContainsKey("Size") ? eValues["Size"] : "0");
            AddGenerationParameterAsComboBox("Matrix Type", 
                eValues.ContainsKey("Matrix Type") ? eValues["Matrix Type"] : AdjacencyMatrixType.ClassicalMatrix.ToString(), 
                typeof(AdjacencyMatrixType));

            foreach (ResearchParameter r in rValues.Keys)
            {
                if (SessionManager.GetResearchTracingPath(researchId) == "" &&
                    r == ResearchParameter.TracingStepIncrement)
                    continue;
                ResearchParameterInfo rInfo = (ResearchParameterInfo)(r.GetType().GetField(r.ToString()).GetCustomAttributes(typeof(ResearchParameterInfo), false)[0]);
                if (rInfo.Type == typeof(bool))
                {
                    AddGenerationParameterAsCheckBox(r.ToString(),
                        rValues[r] != null ? bool.Parse(rValues[r].ToString()) : false);
                }
                else
                {
                    AddGenerationParameterAsTextBox(r.ToString(),
                        r == ResearchParameter.TracingStepIncrement ? 0 : rValues[r]);
                }
            }
        }

        private void FillRandomGenerationParameters(Guid researchId)
        {
            Dictionary<GenerationParameter, object> gValues =
                SessionManager.GetGenerationParameterValues(researchId);
            Dictionary<ResearchParameter, object> rValues =
                SessionManager.GetResearchParameterValues(researchId);

            foreach (GenerationParameter g in gValues.Keys)
            {
                if (g != GenerationParameter.AdjacencyMatrixFile &&
                    g != GenerationParameter.AdjacencyMatrix)
                {
                    if (gValues[g] != null)
                        AddGenerationParameterAsTextBox(g.ToString(), gValues[g]);
                    else
                        generationParametersTable.Rows.Add(g.ToString());
                }
            }

            foreach (ResearchParameter r in rValues.Keys)
            {
                if (SessionManager.GetResearchTracingPath(researchId) == "" &&
                    r == ResearchParameter.TracingStepIncrement)
                    continue;
                ResearchParameterInfo rInfo = (ResearchParameterInfo)(r.GetType().GetField(r.ToString()).GetCustomAttributes(typeof(ResearchParameterInfo), false)[0]);
                if (rInfo.Type == typeof(bool))
                {
                    AddGenerationParameterAsCheckBox(r.ToString(),
                        rValues[r] != null ? bool.Parse(rValues[r].ToString()) : false);
                }
                else
                {
                    AddGenerationParameterAsTextBox(r.ToString(),
                        r == ResearchParameter.TracingStepIncrement ? 0 : rValues[r]);
                }
            }
        }

        private void AddGenerationParameterAsTextBox(String g, Object v)
        {
            generationParametersTable.Rows.Add(g, v.ToString());
        }

        private void AddGenerationParameterAsButton(String g, String v)
        {
            DataGridViewRow r = new DataGridViewRow();
            DataGridViewCell rColumn1 = new DataGridViewTextBoxCell();
            DataGridViewCell rColumn2 = new DataGridViewButtonCell();
            rColumn1.Value = g;
            rColumn2.Value = v;
            r.Cells.Add(rColumn1);
            r.Cells.Add(rColumn2);
            generationParametersTable.Rows.Add(r);
        }

        private void AddGenerationParameterAsCheckBox(String g, bool v)
        {
            DataGridViewRow r = new DataGridViewRow();
            DataGridViewCell rColumn1 = new DataGridViewTextBoxCell();
            DataGridViewCell rColumn2 = new DataGridViewCheckBoxCell();
            rColumn1.Value = g;
            rColumn2.Value = v;
            r.Cells.Add(rColumn1);
            r.Cells.Add(rColumn2);
            generationParametersTable.Rows.Add(r);
        }

        private void AddGenerationParameterAsComboBox(String g, String v, Type t)
        {
            DataGridViewRow r = new DataGridViewRow();
            DataGridViewCell rColumn1 = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell rColumn2 = new DataGridViewComboBoxCell();
            rColumn2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            rColumn1.Value = g;
            rColumn2.Items.Clear();
            string[] mt = Enum.GetNames(t);
            rColumn2.Items.AddRange(mt);
            rColumn2.Value = v;
            r.Cells.Add(rColumn1);
            r.Cells.Add(rColumn2);
            generationParametersTable.Rows.Add(r);
        }

        private void FillAnalyzeOptionsTable(Guid researchId)
        {
            analyzeOptionsTable.Rows.Clear();

            AnalyzeOption availableOptions = SessionManager.GetAvailableAnalyzeOptions(researchId);
            AnalyzeOption checkedOptions = SessionManager.GetAnalyzeOptions(researchId);

            Array existingOptions = Enum.GetValues(typeof(AnalyzeOption));
            foreach (AnalyzeOption opt in existingOptions)
            {
                if ((availableOptions & opt) == opt && opt != AnalyzeOption.None)
                {
                    if((checkedOptions & opt) == opt)
                        analyzeOptionsTable.Rows.Add(opt.ToString(), true);
                    else
                        analyzeOptionsTable.Rows.Add(opt.ToString(), false);
                }
            }
        }

        private void DisableButtons(bool b)
        {
            if (selectedIndex != -1)
            {
                researchesTable.Rows[selectedIndex].ReadOnly = !b;
                generationParametersTable.Columns[1].ReadOnly = !b;
                analyzeOptionsTable.Columns[1].ReadOnly = !b;
                selectAll.Enabled = b;
                deselectAll.Enabled = b;
                startResearch.Enabled = b;
                if (b && GenerationType.Static == SessionManager.GetResearchGenerationType(researchIDs[selectedIndex]))
                    return;
                realizationCountTxt.Enabled = b;                
            }
        }

        private void SetRealizationCount(Guid researchId)
        {
            if (GenerationType.Static == SessionManager.GetResearchGenerationType(researchId))
            {
                realizationCountTxt.Value = 1;
                realizationCountTxt.Enabled = false;
                SessionManager.SetResearchRealizationCount(researchId, 1);
            }
            else
            {
                realizationCountTxt.Value = SessionManager.GetResearchRealizationCount(researchId);
                realizationCountTxt.Enabled = true;
            }
        }

        private void FillStatusTableOnStart(Guid researchId)
        {
            statusTable.Rows.Clear();

            for (int i = 0; i < SessionManager.GetResearchRealizationCount(researchId); ++i)
            {
                int newRowIndex = statusTable.Rows.Add();
                statusTable.Rows[newRowIndex].Cells["statusStatusColumn"].Value = "Not Started.";
                statusTable.Rows[newRowIndex].Cells["statusStopColumn"].Value = "Stop";
            }
        }

        private void FillStatusTable(Guid researchId)
        {
            statusTable.Rows.Clear();

            NetworkEventArgs[] statuses = SessionManager.GetResearchEnsembleStatus(researchId);
            if (statuses != null)
            {
                for (int i = 0; i < statuses.Length; ++i)
                {
                    int newRowIndex = statusTable.Rows.Add();
                    statusTable.Rows[newRowIndex].Cells["statusStatusColumn"].Value = statuses[i].ExtendedInfo;
                    statusTable.Rows[newRowIndex].Cells["statusStopColumn"].Value = "Stop";
                }
            }
        }

        private void CurrentResearch_OnResearchUpdateStatus(object sender, ResearchEventArgs e)
        {
            int researchIndex = researchIDs.IndexOf(e.ResearchID);
            researchesTable.Rows[researchIndex].Cells["statusColumn"].Value = e.Status.ToString();
            stopResearch.Enabled = (ResearchStatus.Running == e.Status);
        }

        private void CurrentResearch_OnResearchEnsembleUpdateStatus(object sender, ResearchEnsembleEventArgs e)
        {
            statusTable.Rows[e.UpdatedNetworkID].Cells["statusStatusColumn"].Value = e.UpdatedExtendedInfo;
        }

        #endregion
    }
}
