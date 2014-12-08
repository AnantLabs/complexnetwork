using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Session;
using Core.Enumerations;
using Core.Attributes;
using Core.Result;

namespace Random_Networks_Statistic_Analyzer
{
    // TODO remove to Core
    public enum ThickeningType
    {
        Delta,
        Percent
    }

    public enum ApproximationType
    {
        Degree,
        Exponential,
        Gaus
    }

    public partial class MainWindow : Form
    {
        private static Dictionary<int, List<Guid>> resultsByGroups = new Dictionary<int, List<Guid>>();
        private Guid selectedId = Guid.Parse("00000000-0000-0000-0000-000000000000");

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void MainWindow_Load(object sender, EventArgs e)
        {
            InitializeResearchType();
            InitializeModelType();
            InitializeOptionsTables();
            FillResearchesTable();
        }

        private void loadFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFrom loadFromWindow = new LoadFrom();
            loadFromWindow.ShowDialog(this);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void researchTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeModelType();
            FillResearchesTable();
        }

        private void modelTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillResearchesTable();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            StSessionManager.RefreshExistingResults();
            FillResearchesTable();
        }

        private void researchesTable_SelectionChanged(object sender, EventArgs e)
        {
            if (researchesTable.SelectedRows.Count > 0)
            {
                selectedId = Guid.Parse(researchesTable.SelectedRows[0].Cells["researchIdColumn"].Value.ToString());
                FillGroupParameters();
            }
        }

        private void researchesTable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            DataGridView.HitTestInfo hit = researchesTable.HitTest(e.X, e.Y);
            if (hit.RowIndex != -1)
            {
                foreach (DataGridViewRow r in researchesTable.SelectedRows)
                {
                    r.Selected = false;
                }
                researchesTable.Rows[hit.RowIndex].Selected = true;
                researchTableCSM.Items["eraseResearch"].Enabled = true;
                researchTableCSM.Items["selectGroup"].Enabled = true;
            }
            else
            {
                researchTableCSM.Items["eraseResearch"].Enabled = false;
                researchTableCSM.Items["selectGroup"].Enabled = false;
            }
            researchTableCSM.Show(researchesTable, e.X, e.Y);
        }

        private void eraseResearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (researchesTable.SelectedRows.Count > 0)
            {
                Guid id = Guid.Parse(researchesTable.SelectedRows[0].Cells["researchIdColumn"].Value.ToString());
                StSessionManager.DeleteResearch(id);
                FillResearchesTable();
            }
        }

        private void selectGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (int i in resultsByGroups.Keys)
            {
                if (resultsByGroups[i].Contains(selectedId))
                {
                    foreach (Guid id in resultsByGroups[i])
                    {
                        foreach (DataGridViewRow r in researchesTable.Rows)
                        {
                            if (r.Cells["researchIdColumn"].Value.ToString() == id.ToString())
                                r.Selected = true;
                        }
                    }
                }
            }
        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            switch (optionsTabs.SelectedIndex)
            {
                case 0:
                    foreach (DataGridViewRow r in globalOptionsTable.Rows)
                    {
                        DataGridViewCheckBoxCell cell = r.Cells["globalCheckedColumn"] as DataGridViewCheckBoxCell;
                        cell.Value = true;
                    }
                    break;
                case 1:
                    foreach (DataGridViewRow r in distributedOptionsTable.Rows)
                    {
                        DataGridViewCheckBoxCell cell = r.Cells["distributedCheckedColumn"] as DataGridViewCheckBoxCell;
                        cell.Value = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void deselectAll_Click(object sender, EventArgs e)
        {
            switch (optionsTabs.SelectedIndex)
            {
                case 0:
                    foreach (DataGridViewRow r in globalOptionsTable.Rows)
                    {
                        DataGridViewCheckBoxCell cell = r.Cells["globalCheckedColumn"] as DataGridViewCheckBoxCell;
                        cell.Value = false;
                    }
                    break;
                case 1:
                    foreach (DataGridViewRow r in distributedOptionsTable.Rows)
                    {
                        DataGridViewCheckBoxCell cell = r.Cells["distributedCheckedColumn"] as DataGridViewCheckBoxCell;
                        cell.Value = false;
                    }
                    break;
                default:
                    break;
            }
        }

        private void color_Click(object sender, EventArgs e)
        {
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                color.BackColor = colorDlg.Color;
            }
        }

        private void showGraphics_Click(object sender, EventArgs e)
        {
            foreach (int i in resultsByGroups.Keys)
            {
                if (resultsByGroups[i].Contains(selectedId))
                {
                    Dictionary<AnalyzeOption, ChartProperties> p = new Dictionary<AnalyzeOption,ChartProperties>();
                    foreach(DataGridViewRow r in distributedOptionsTable.Rows)
                    {
                        DataGridViewCheckBoxCell cell = r.Cells["distributedCheckedColumn"] as DataGridViewCheckBoxCell;
                        if ((bool)(cell.Value) == true)
                        {
                            AnalyzeOption opt = (AnalyzeOption)Enum.Parse(typeof(AnalyzeOption), 
                                r.Cells["distributedNameColumn"].Value.ToString());
                            ThickeningType t = (ThickeningType)Enum.Parse(typeof(ThickeningType), 
                                r.Cells["thickeningTypeColumn"].Value.ToString());
                            ApproximationType at = (ApproximationType)Enum.Parse(typeof(ApproximationType), 
                                r.Cells["approximationColumn"].Value.ToString());
                            ChartProperties pv = new ChartProperties(t, 
                                double.Parse(r.Cells["thickeningValueColumn"].Value.ToString()), 
                                at);
                            p.Add(opt, pv);
                        }
                    }
                    AnalyzeCharts chartsWindow = new AnalyzeCharts(i, p);
                    chartsWindow.Show();
                    return;
                }
            }
        }

        #endregion

        #region Utilities

        private void InitializeResearchType()
        {
            researchTypeCmb.Items.Clear();
            string[] researchTypeNames = Enum.GetNames(typeof(ResearchType));
            for (int i = 0; i < researchTypeNames.Length; ++i)
                researchTypeCmb.Items.Add(researchTypeNames[i]);

            researchTypeCmb.Items.Add("All types");

            if (researchTypeCmb.Items.Count != 0)
                researchTypeCmb.SelectedIndex = 0;
        }

        private void InitializeModelType()
        {
            modelTypeCmb.Items.Clear();
            if (researchTypeCmb.Text == "All types")
            {
                string[] modelTypeNames = Enum.GetNames(typeof(ModelType));
                for (int i = 0; i < modelTypeNames.Length; ++i)
                    modelTypeCmb.Items.Add(modelTypeNames[i]);
            }
            else
            {
                ResearchType rtype = (ResearchType)Enum.Parse(typeof(ResearchType), researchTypeCmb.Text);
                ResearchTypeInfo[] info = (ResearchTypeInfo[])rtype.GetType().GetField(rtype.ToString()).GetCustomAttributes(typeof(ResearchTypeInfo), false);
                Type rt = Type.GetType(info[0].Implementation, true);

                List<AvailableModelType> l = new List<AvailableModelType>((AvailableModelType[])rt.GetCustomAttributes(typeof(AvailableModelType), true));
                for (int i = 0; i < l.Count; ++i)
                    modelTypeCmb.Items.Add(l[i].ModelType.ToString());
            }

            modelTypeCmb.Items.Add("All types");

            if (modelTypeCmb.Items.Count != 0)
                modelTypeCmb.SelectedIndex = 0;
        }

        private void InitializeOptionsTables()
        {
            globalOptionsTable.Rows.Clear();
            distributedOptionsTable.Rows.Clear();

            DataGridViewComboBoxColumn thickeningTypeColumn =
                distributedOptionsTable.Columns["thickeningTypeColumn"] as DataGridViewComboBoxColumn;
            thickeningTypeColumn.Items.Clear();
            string[] thickeningTypeNames = Enum.GetNames(typeof(ThickeningType));
            for (int i = 0; i < thickeningTypeNames.Length; ++i)
                thickeningTypeColumn.Items.Add(thickeningTypeNames[i]);

            DataGridViewComboBoxColumn approximationColumn =
                distributedOptionsTable.Columns["approximationColumn"] as DataGridViewComboBoxColumn;
            approximationColumn.Items.Clear();
            string[] approximationTypeNames = Enum.GetNames(typeof(ApproximationType));
            for (int i = 0; i < approximationTypeNames.Length; ++i)
                approximationColumn.Items.Add(approximationTypeNames[i]);                

            Array options = Enum.GetValues(typeof(AnalyzeOption));
            foreach(AnalyzeOption opt in options)
            {
                AnalyzeOptionInfo[] info = (AnalyzeOptionInfo[])opt.GetType().GetField(opt.ToString()).GetCustomAttributes(typeof(AnalyzeOptionInfo), false);
                if (opt != AnalyzeOption.None && info[0].OptionType == OptionType.Global)
                {
                    globalOptionsTable.Rows.Add(opt.ToString(), false);
                }
                else if (info[0].OptionType == OptionType.Distribution)
                {
                    int newRowIndex = distributedOptionsTable.Rows.Add(opt.ToString());
                    DataGridViewRow newRow = distributedOptionsTable.Rows[newRowIndex];
                    DataGridViewComboBoxCell tCell = newRow.Cells["thickeningTypeColumn"] as DataGridViewComboBoxCell;
                    tCell.Value = tCell.Items[0].ToString();
                    newRow.Cells["thickeningValueColumn"].Value = "0";
                    DataGridViewComboBoxCell aCell = newRow.Cells["approximationColumn"] as DataGridViewComboBoxCell;
                    aCell.Value = aCell.Items[0].ToString();
                    newRow.Cells["distributedCheckedColumn"].Value = false;
                }
                // TODO add other Analyze Options
            }
        }

        private void FillResearchesTable()
        {
            researchesTable.Rows.Clear();

            ResearchType rt = (ResearchType)Enum.Parse(typeof(ResearchType), researchTypeCmb.Text.ToString());
            ModelType mt = (ModelType)Enum.Parse(typeof(ModelType), modelTypeCmb.Text.ToString());
            resultsByGroups = StSessionManager.GetFilteredResultsByGroups(rt, mt);

            Color c = Color.WhiteSmoke;
            foreach (int i in resultsByGroups.Keys)
            {
                foreach(Guid id in resultsByGroups[i])
                {
                    int newRowIndex = researchesTable.Rows.Add(id.ToString(),
                        StSessionManager.GetResearchName(id),
                        StSessionManager.GetResearchRealizationCount(id).ToString(),
                        StSessionManager.GetResearchNetworkSize(id).ToString());
                    DataGridViewRow row = researchesTable.Rows[newRowIndex];
                    row.DefaultCellStyle.BackColor = c;
                    //row.Cells["dateColumn"].Value = StSessionManager.GetResearchDate(id).ToString();
                }
                
                c = (c == Color.WhiteSmoke) ? Color.MistyRose : Color.WhiteSmoke;
            }
        }

        private void FillGroupParameters()
        {
            parametersTable.Rows.Clear();

            Dictionary<GenerationParameter, object> gValues =
                StSessionManager.GetGenerationParameterValues(selectedId);
            Dictionary<ResearchParameter, object> rValues =
                StSessionManager.GetResearchParameterValues(selectedId);

            foreach (GenerationParameter g in gValues.Keys)
            {
                if (g != GenerationParameter.AdjacencyMatrixFile)
                {
                    if (gValues[g] != null)
                        parametersTable.Rows.Add(g.ToString(), gValues[g].ToString());
                    else
                        parametersTable.Rows.Add(g.ToString());
                }
            }

            foreach (ResearchParameter r in rValues.Keys)
            {
                if (rValues[r] != null)
                    parametersTable.Rows.Add(r.ToString(), rValues[r].ToString());
                else
                    parametersTable.Rows.Add(r.ToString());
            }
        }

        #endregion

    }
}
