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
            this.degreesRadio.Checked = true;
            this.filePath.Enabled = false;
            this.filePathTxt.Enabled = false;
            this.browse.Enabled = false;
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
            degreeSequence.Clear();
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
            if (this.modelNameCmb.Text == "Block-Hierarchic")
            {
                if (this.exactCheckRadio.Checked == true)
                {
                    HierarchicExactChecker checker = new HierarchicExactChecker();
                    if (this.degreesRadio.Checked == false)
                    {
                        bool result = checker.IsHierarchic(this.filePathTxt.Text);
                        this.resultTxt.Text = result ? "Is Hierarchic" : "Is Not Hierarchic";
                    }
                    else
                    {
                        this.resultTxt.Text = "Cannot calculate. File path should be specified!";
                    }
                }
                else
                {
                    HierarchicChecker checker;
                    if (this.degreesRadio.Checked == true)
                    {
                        ParceDegrees();
                        checker = new HierarchicChecker(degreeSequence);
                    }
                    else
                    {
                        checker = new HierarchicChecker(this.filePathTxt.Text);
                        FillDegrees(checker.FromMatrixToDegrees());
                    }

                    this.resultTxt.Text = checker.IsHierarchic().ToString();
                }
            }
        }

        private void FillDegrees(List<int> degrees)
        {
            string d = "";
            for (int i = 0; i < degrees.Count; ++i)
            {
                d += degrees[i].ToString() + ",";
            }
            this.degreesTxt.Text = d;
        }

        private void degreesRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (this.degreesRadio.Checked == true)
            {
                this.degrees.Enabled = true;
                this.degreesTxt.Enabled = true;
            }
            else
            {
                this.degrees.Enabled = false;
                this.degreesTxt.Enabled = false;
            }
        }

        private void fromFileRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (this.fromFileRadio.Checked == true)
            {
                this.filePath.Enabled = true;
                this.filePathTxt.Enabled = true;
                this.browse.Enabled = true;
            }
            else
            {
                this.filePath.Enabled = false;
                this.filePathTxt.Enabled = false;
                this.browse.Enabled = false;
            }
        }

        private void modelNameCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.modelNameCmb.Text)
            {
                case "Block-Hierarchic":
                    {
                        this.exactCheckRadio.Visible = true;
                        this.notExactCheckRadio.Visible = true;
                        this.exactCheckRadio.Checked = true;
                        break;
                    }
                default:
                    {
                        this.exactCheckRadio.Visible = false;
                        this.notExactCheckRadio.Visible = false;
                        break;
                    }
            }
        }

        private void browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.filePathTxt.Text = openFileDialog1.FileName;
            }
        }
    }
}
