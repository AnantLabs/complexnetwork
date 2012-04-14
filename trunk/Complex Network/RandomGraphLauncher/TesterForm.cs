using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnalyzerFramework.Manager.ModelRepo;
using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Model;
using System.IO;
using ResultStorage.Storage;
using CommonLibrary.Model.Result;
using RandomGraph.Common.Model;
using Model.BAModel.Realization;
using CommonLibrary.Model;
using Model.WSModel.Realization;
using Model.ERModel.Realization;
using Model.StaticModel.Realization;
using GenericAlgorithms;
using System.Collections;
using model.ERModel.Realization;

namespace RandomGraphLauncher
{
    public partial class TesterForm : Form
    {

        private IDictionary<string, Tuple<Type, Type>> models = new Dictionary<string, Tuple<Type, Type>>();
        private ArrayList labels = new ArrayList();
        public TesterForm()
        {
            InitializeComponent();
            List<Type> availableModelFactoryTypes = ModelRepository.GetInstance().GetAvailableModelFactoryTypes();
            foreach (Type modelFactoryType in availableModelFactoryTypes)
            {

                object[] attr = modelFactoryType.GetCustomAttributes(typeof(TargetGraphModel), false);
                TargetGraphModel targetGraphMetadata = (TargetGraphModel)attr[0];
                Type modelType = targetGraphMetadata.GraphModelType;

                attr = modelType.GetCustomAttributes(typeof(GraphModel), false);
                string modelName = ((GraphModel)attr[0]).Name;

                models.Add(modelName, Tuple.Create<Type, Type>(modelFactoryType, modelType));
            }
            InitCompoBox(models.Keys);
        }

        private void InitCompoBox(ICollection<string> modelNames)
        {
            List<string> list = new List<string>();
            foreach (string s in modelNames)
            {
                list.Add(s);
            }
            list.Sort((x, y) => string.Compare(x, y));
            comboBox_ModelType.Items.AddRange(list.ToArray<string>());
            comboBox_ModelType.SelectedIndex = 0;
        }

        private void inputMatrix_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                  inputMatrixPath.Text = openFileDialog1.FileName;
            }
        }

        private void goldenOut_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                goldenOutPath.Text = openFileDialog1.FileName;
            }
        }

        public void WSModelTest(WSGraph graph)
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML(goldenOutPath.Text);
            IGraphAnalyzer analyzer = new WSAnalyzer(graph.Container);
            Label label;
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {
                label = (Label)labels[0];
                label.Text = "Passed";
            }
            else
            {
                label = (Label)labels[0];
                label.Text = "Feild";
            }
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {

            }
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {

            }
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {

            }
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {

            }
        }

        public void BAModelTest(BAGraph graph)
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML(goldenOutPath.Text);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);
            Label label;
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {
                label = (Label)labels[0];
                label.Text = "Passed";
            }
            else
            {
                label = (Label)labels[0];
                label.Text = "Feild";
            }
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {

            }
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {

            }
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {

            }
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {

            }
        }

        public void ERModelTest(ERGraph graph)
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML(goldenOutPath.Text);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);
            Label label;
            if (goldResult.Results[0].Result[AnalyseOptions.DegreeDistribution] == analyzer.GetAveragePath())
            {
                label = (Label)labels[0];
                label.Text = "Passed";
            }
            else
            {
                label = (Label)labels[0];
                label.Text = "Feild";
            }
            if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
            {
                label = (Label)labels[1];
                label.Text = "Passed";
            }
            else
            {
                label = (Label)labels[1];
                label.Text = "Feild";
            }
            if (goldResult.Results[0].Coefficient == analyzer.GetClusteringCoefficient())
            {
                label = (Label)labels[2];
                label.Text = "Passed";
            }
            else
            {
                label = (Label)labels[2];
                label.Text = "Feild";
            }
            if (goldResult.Results[0].EigenVector == analyzer.GetEigenValue())
            {
                label = (Label)labels[3];
                label.Text = "Passed";
            }
            else
            {
                label = (Label)labels[3];
                label.Text = "Feild";
            }
            if (goldResult.Results[0].DistancesBetweenEigenValues == analyzer.GetDistEigenPath())
            {
                label = (Label)labels[4];
                label.Text = "Passed";
            }
            else
            {
                label = (Label)labels[4];
                label.Text = "Feild";
            }
            if (goldResult.Results[0].DistanceBetweenVertices == analyzer.GetDegreeDistribution())
            {
                label = (Label)labels[5];
                label.Text = "Passed";
            }
            else
            {
                label = (Label)labels[5];
                label.Text = "Feild";
            }
        }

        private void constructGraph(string modelName)
        {
            ArrayList matrix = MatrixFileReader.MatrixReader(inputMatrix.Text);
            switch (modelName)
            {
                case "Barabasi-Albert":
                    BAGraph BAGraph = new BAGraph(matrix);
                    BAModelTest(BAGraph);
                    break;
                case "ERModel":
                    ERGraph ERGraph = new ERGraph(matrix);
                    ERModelTest(ERGraph);
                    break;
                case "Watts-Strogatz":
                    WSGraph WSGraph = new WSGraph(matrix);
                    WSModelTest(WSGraph);
                    break;
                case "Static Model":
                    StaticGraph STGraph = new StaticGraph(matrix);
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string modelName = comboBox_ModelType.SelectedItem.ToString();
            Type modelType = models[modelName].Item2;
            AvailableAnalyzeOptions[] optionsAttributes = (AvailableAnalyzeOptions[])modelType.GetCustomAttributes(typeof(AvailableAnalyzeOptions), false);
            AnalyseOptions availableOptions = optionsAttributes[0].Options;
            Point point = new Point(35, 200);
            foreach (AnalyseOptions opt in Enum.GetValues(typeof(AnalyseOptions)))
            {
                if ((opt & availableOptions) == opt && opt != AnalyseOptions.None)
                {
                    AnalyzeOptionInfo optionInfo = (AnalyzeOptionInfo)(opt.GetType().GetField(Enum.GetName(typeof(AnalyseOptions), opt)).GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);
                    Label label = new Label();
                    label.Text = optionInfo.Name;
                    label.Location = point;
                    label.AutoSize = true;
                    Label status = new Label();
                    point.X += 250;
                    status.Location = point;
                    status.Text = "waiting";
                    point.X -= 250;
                    status.Parent = this;
                    labels.Add(status);
                    point.Y += 25;
                    label.Parent = this;
                }
            }
            constructGraph(modelName);
        }
    }
}
