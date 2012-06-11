using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using AnalyzerFramework.Manager.ModelRepo;
using CommonLibrary.Model.Attributes;
using ResultStorage.Storage;
using CommonLibrary.Model.Result;
using RandomGraph.Common.Model;
using Model.BAModel.Realization;
using CommonLibrary.Model;
using Model.WSModel.Realization;
using Model.ERModel.Realization;

namespace RandomGraphLauncher
{
    public partial class ModelTestView : Form
    {
        private IDictionary<string, Tuple<Type, Type>> models = new Dictionary<string, Tuple<Type, Type>>();
        private ArrayList matrix = new ArrayList();

        public ModelTestView()
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
            comboBox1.Items.AddRange(list.ToArray<string>());
            comboBox1.SelectedIndex = 0;
        }

        public ArrayList get_data(String filename)
        {
            ArrayList matrix = new ArrayList();
            using (StreamReader streamreader = new StreamReader(filename))
            {
                string contents;
                while ((contents = streamreader.ReadLine()) != null)
                {
                    string[] split = System.Text.RegularExpressions.Regex.Split(contents, "\\s+", System.Text.RegularExpressions.RegexOptions.None);
                    ArrayList tmp = new ArrayList();
                    foreach (string s in split)
                    {
                        if (s.Equals("0"))
                        {
                            tmp.Add(false);
                        }
                        else
                        {
                            tmp.Add(true);
                        }
                    }
                    matrix.Add(tmp);
                }
            }
            return matrix;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog2.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            matrix = get_data(textBox1.Text);
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    Console.WriteLine("Case 1");
                    break;
                case 2:
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
           // Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());

        }

        private void testBAModel()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("TestData");
            ResultAssembly goldResult = resultStorage.Load(new Guid("b2059e51-ba01-498b-8150-8b47b1200659"));
            BAGraph graph = new BAGraph(0, 0, matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);
        }
        private void testERModel()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("TestData");
            ResultAssembly goldResult = resultStorage.Load(new Guid("b2059e51-ba01-498b-8150-8b47b1200659"));
            BAGraph graph = new BAGraph(0, 0, matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);
        }
        private void testWSModel()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("TestData");
            ResultAssembly goldResult = resultStorage.Load(new Guid("b2059e51-ba01-498b-8150-8b47b1200659"));
            BAGraph graph = new BAGraph(0, 0, matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);
        }
    }
}
