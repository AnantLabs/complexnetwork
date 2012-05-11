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
        private ArrayList algNames = new ArrayList();
        private ArrayList checkList = new ArrayList();
        ResultAssembly goldResult;

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
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                  inputMatrixPath.Text = openFileDialog1.FileName;
            }
        }

        private void goldenOut_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                goldenOutPath.Text = openFileDialog1.FileName;
            }
        }

        private void WSModelTest(WSGraph graph)
        {
            IGraphAnalyzer analyzer = new WSAnalyzer(graph.Container);

            //test tDegreeDistribution
            testDegreeDistribution(0, analyzer);

            //test AveragePath
            testAveragePath(1, analyzer);

            //test ClusteringCoefficient
            testClusteringCoefficient(2, analyzer);

            //test EigenValue
            testEigenValue(3, analyzer);

            //test Cycles of order 3
            testCycles3(4, analyzer);

            // test diameter
            testDiameter(5, analyzer);
           
            // test cycle of order 4
            testCycles4(6, analyzer);

            // test order of max full subgraph
            testFullSubgraphs(7, analyzer);
        }

        private void BAModelTest(BAGraph graph)
        {
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            //test tDegreeDistribution
            testDegreeDistribution(0, analyzer);

            //test AveragePath
            testAveragePath(1, analyzer);

            //test ClusteringCoefficient
            testClusteringCoefficient(2, analyzer);

            //test EigenValue
            testEigenValue(3, analyzer);

            //test Cycles of order 3
            testCycles3(4, analyzer);

            // test diameter
            testDiameter(5, analyzer);

            // test cycle of order 4
            testCycles4(6, analyzer);

            // test distance between vertexes
            testDistanceBetweenVertices(7, analyzer);

            // test distance between eigen values
            testDistancesBetweenEigenValues(8, analyzer);

            // test order of max full subgraph
            testFullSubgraphs(9, analyzer);
        }

        private void ERModelTest(ERGraph graph)
        {
            //XMLResultStorage resultStorage = new XMLResultStorage("");
            //goldResult = resultStorage.LoadXML(goldenOutPath.Text);
            ERAnalyzer analyzer = new ERAnalyzer(graph.Container);

            //test tDegreeDistribution
            testDegreeDistribution(0, analyzer);

            //test AveragePath
            testAveragePath(1, analyzer);

            //test ClusteringCoefficient
            testClusteringCoefficient(2, analyzer);

            //test EigenValue
            testEigenValue(3, analyzer);

            //test Cycles of order 3
            testCycles3(4, analyzer);

            // test diameter
            testDiameter(5, analyzer);

            // test cycle of order 4
            testCycles4(6, analyzer);

            // test motive
           // testMotiv(7, analyzer);

            // test distance between vertexes
            testDistanceBetweenVertices(7, analyzer);

            // test distance between eigen values
            testDistancesBetweenEigenValues(8, analyzer);

            // test order of max full subgraph
            testFullSubgraphs(9, analyzer);

           // testCycles(11, analyzer);
        }

        private bool checkIfNeedToTest(int number)
        {
            Label label;
            if (!((CheckBox)checkList[number]).Checked)
            {
                label = (Label)labels[number];
                label.Text = "dismissed";
                return false;
            }
            return true;
        }
        private void testDegreeDistribution(int number, IGraphAnalyzer analyzer)
        {
            Label label;
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            try
            {
                if (compare(goldResult.Results[0].VertexDegree, analyzer.GetDegreeDistribution()))
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testAveragePath(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            if (!((CheckBox)checkList[number]).Checked)
            {
                label = (Label)labels[number];
                label.Text = "dismissed";
            }
            try
            {
                if (goldResult.Results[0].Result[AnalyseOptions.AveragePath] == analyzer.GetAveragePath())
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception e)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testClusteringCoefficient(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            try
            {
                if (compare(goldResult.Results[0].Coefficient, analyzer.GetClusteringCoefficient()))
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testEigenValue(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            try
            {
                if (compare(goldResult.Results[0].EigenVector, analyzer.GetEigenValue()))
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testCycles3(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            try
            {
                if (goldResult.Results[0].Result[AnalyseOptions.Cycles3] == analyzer.GetCycles3())
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testDiameter(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            try
            {
                if (goldResult.Results[0].Result[AnalyseOptions.Diameter] == analyzer.GetDiameter())
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testCycles4(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            try
            {
                if (goldResult.Results[0].Result[AnalyseOptions.Cycles4] == analyzer.GetCycles4())
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testDistanceBetweenVertices(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            try
            {
                if (compare(goldResult.Results[0].DistanceBetweenVertices, analyzer.GetMinPathDist()))
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testDistancesBetweenEigenValues(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            try
            {
                if (compare(goldResult.Results[0].DistancesBetweenEigenValues, analyzer.GetDistEigenPath()))
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testFullSubgraphs(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            try
            {
                if (compare(goldResult.Results[0].FullSubgraphs, analyzer.GetFullSubGraph()))
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testMotiv(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            try
            {
                if (true)//compare(goldResult.Results[0].MotivesCount, analyzer.GetMotif()))
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void testCycles(int number, IGraphAnalyzer analyzer)
        {
            if (!checkIfNeedToTest(number))
            {
                return;
            }
            Label label;
            try
            {
                if (compare(goldResult.Results[0].Cycles, analyzer.GetCycles(4, 6)))
                {
                    label = (Label)labels[number];
                    label.Text = "Passed";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label = (Label)labels[number];
                    label.Text = "Feild";
                    label.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                label = (Label)labels[number];
                label.Text = "Feild";
                label.ForeColor = Color.Red;
            }
        }

        private void constructGraph(string modelName)
        {
            try
            {
                XMLResultStorage resultStorage = new XMLResultStorage("");
                goldResult = resultStorage.LoadXML(goldenOutPath.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "Unable load XML file", e.Message);
            }
            ArrayList matrix;
            try
            {
                matrix = MatrixFileReader.MatrixReader(inputMatrixPath.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "Unable load convert file to Arraylist", e.Message);
                return;
            }
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

        private bool compare(SortedDictionary<int, int> a, SortedDictionary<int, int> b)
        {
            return a.SequenceEqual(b);
        }

        private bool compare(SortedDictionary<double, int> a, SortedDictionary<double, int> b)
        {
            return a.SequenceEqual(b);
        }

        private bool compare(ArrayList a, ArrayList b)
        {
            if(a.Count != b.Count)
            {
                return false;
            }
            for (int i = 0; i < a.Count; ++i)
            {
                if(a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validate())
            {
                return;
            }
            string modelName = comboBox_ModelType.SelectedItem.ToString();

            constructGraph(modelName);
        }

        private bool validate()
        {
            if (inputMatrixPath.Text == "")
            {
                MessageBox.Show(this, "Please select input file");
                return false;
            }
            if (goldenOutPath.Text == "")
            {
                MessageBox.Show(this, "Please select golden out file");
                return false;
            }
            return true;
        }

        private void comboBox_ModelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int l = 0; l < labels.Count; ++l)
            {
                ((Label)labels[l]).Dispose();
                ((Label)algNames[l]).Dispose();
                ((CheckBox)checkList[l]).Dispose();
            }
            labels.Clear();
            algNames.Clear();
            checkList.Clear();
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
                    algNames.Add(label);
                    CheckBox c = new CheckBox();
                    point.X += 330;
                    c.Location = point;
                    c.Checked = true;
                    c.Parent = this;
                    checkList.Add(c);
                    Label status = new Label();
                    point.X -= 80;
                    status.Location = point;
                    status.Text = "waiting";
                    status.Parent = this;
                    labels.Add(status);
                    label.Parent = this;
                    point.Y += 25;
                    point.X -= 250;
                }
            }
        }
    }
}
