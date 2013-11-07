using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model.Result;
using ResultStorage.Storage;
using Percolations;

namespace StatisticAnalyzerUI
{
    // !исправить!
    public partial class Researches : Form
    {
        private XMLResultStorage storage;
        private List<ResultResearch> researches;
        private List<Guid> researchesID = new List<Guid>();
        private ResultResearch currentResearch;

        public Researches()
        {
            InitializeComponent();

            this.storage = new XMLResultStorage(ConfigurationManager.
                AppSettings[ConfigurationManager.AppSettings["Storage"]]);
            this.researches = storage.LoadAllResearches();
        }

        private void Researches_Load(object sender, EventArgs e)
        {
            foreach(ResultResearch r in this.researches)
            {
                this.researchesID.Add(r.ResearchID);
                this.researchNamesCmb.Items.Add(r.Name);
            }
            if (this.researchNamesCmb.Items.Count != 0)
                this.researchNamesCmb.SelectedIndex = 0;
        }

        private void researchNamesCmb_SelectedValueChanged(object sender, EventArgs e)
        {
            this.currentResearch = 
                storage.LoadResearch(this.researchesID[this.researchNamesCmb.SelectedIndex]);

            this.informationGrd.Rows.Clear();

            this.informationGrd.Rows.Add("ID", this.currentResearch.ResearchID.ToString());
            this.informationGrd.Rows.Add("Name", this.currentResearch.Name);
            this.informationGrd.Rows.Add("Delta", this.currentResearch.Delta.ToString());
            this.informationGrd.Rows.Add("Realization Count", this.currentResearch.RealizationCount);
            this.informationGrd.Rows.Add("Function", this.currentResearch.Function);
            this.informationGrd.Rows.Add("Size", this.currentResearch.Size);
            foreach (GenerationParam p in this.currentResearch.GenerationParams.Keys)
            {
                this.informationGrd.Rows.Add(p.ToString(), 
                    this.currentResearch.GenerationParams[p].ToString());
            }
        }

        private void showGraphics_Click(object sender, EventArgs e)
        {
            MultipleGraphics graphics = new MultipleGraphics(this.currentResearch.Size, 
                this.currentResearch.Result);
            graphics.Text = this.currentResearch.Name;
            graphics.Show();
        }
    }
}
