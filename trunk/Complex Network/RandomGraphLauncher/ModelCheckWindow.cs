using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CommonLibrary.Model.Attributes;
using AnalyzerFramework.Manager.ModelRepo;
using ModelCheck;

namespace RandomGraphLauncher
{
    public partial class ModelCheckWindow : Form
    {
        private List<int> degreeSequence;

        public ModelCheckWindow()
        {
            InitializeComponent();

            degreeSequence = new List<int>();
        }

        private void ModelCheckWindow_Load(object sender, EventArgs e)
        {
            InitializeModelNameCmb();
        }

        // Utilities //
        private void InitializeModelNameCmb()
        {
            List<Type> availableModelFactoryTypes = ModelRepository.GetInstance().GetAvailableModelFactoryTypes();
            foreach (Type modelFactoryType in availableModelFactoryTypes)
            {
                object[] attr = modelFactoryType.GetCustomAttributes(typeof(TargetGraphModel), false);
                TargetGraphModel targetGraphMetadata = (TargetGraphModel)attr[0];
                Type modelType = targetGraphMetadata.GraphModelType;

                attr = modelType.GetCustomAttributes(typeof(GraphModel), false);
                string modelName = ((GraphModel)attr[0]).Name;

                this.modelNameCmb.Items.Add(modelName);
            }

            this.modelNameCmb.SelectedIndex = 0;
        }

        private void ParceDegrees()
        {
            string degrees = this.degreesTxt.Text.ToString();
            string d = "";
            for (int i = 0; i < degrees.Length; ++i)
            {
                if(Char.IsDigit(degrees[i]))
                    d += degrees[i].ToString();
                else if (degrees[i] == ',')
                {
                    degreeSequence.Add(Convert.ToInt32(d));
                    d = "";
                }
            }
            degreeSequence.Add(Convert.ToInt32(d));
        }

        private void checkBtn_Click(object sender, EventArgs e)
        {
            ParceDegrees();
            if (this.modelNameCmb.Text == "Block-Hierarchic")
            {
                HierarchicChecker checker = new HierarchicChecker(degreeSequence);
                this.resultTxt.Text = checker.IsHierarchic().ToString();
            }
        }
    }
}
