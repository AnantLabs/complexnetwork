using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.IO;
using ResultStorage.Storage;
using CommonLibrary.Model.Result;
using RandomGraph.Common.Model;
using Model.BAModel.Realization;
using CommonLibrary.Model;
using GenericAlgorithms;

namespace ModelsTests
{
    [TestClass]
    public class BAModelTest
    {
        private bool compare(SortedDictionary<int, int> a, SortedDictionary<int, int> b)
        {
            return a.SequenceEqual(b);
        }

        private bool compare(SortedDictionary<int, float> a, SortedDictionary<int, float> b)
        {
            return a.SequenceEqual(b);
        }

        private bool compare(SortedDictionary<int, long> a, SortedDictionary<int, long> b)
        {
            return a.SequenceEqual(b);
        }

        private bool compare(SortedDictionary<double, int> a, SortedDictionary<double, int> b)
        {
            return a.SequenceEqual(b);
        }

        private bool compare(ArrayList a, ArrayList b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }
            for (int i = 0; i < a.Count; ++i)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BAAveragePathTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            double actualValue = analyzer.GetAveragePath();
            double expectedValue = goldResult.Results[0].Result[AnalyseOptions.AveragePath];
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BAClusteringCoefficientTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            SortedDictionary<double, int> actualValue = analyzer.GetClusteringCoefficient();
            SortedDictionary<double, int> expectedValue = goldResult.Results[0].Coefficient;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BADegreeDistributionTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            SortedDictionary<int, int> actualValue = analyzer.GetDegreeDistribution();
            SortedDictionary<int, int> expectedValue = goldResult.Results[0].VertexDegree;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BACyclesTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            SortedDictionary<int, long> actualValue = analyzer.GetCycles(4, 6);
            SortedDictionary<int, long> expectedValue = goldResult.Results[0].Cycles;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BACycles3Test()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            int actualValue = analyzer.GetCycles3();
            double expectedValue = goldResult.Results[0].Result[AnalyseOptions.Cycles3];
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BACycles4Test()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            int actualValue = analyzer.GetCycles4();
            double expectedValue = goldResult.Results[0].Result[AnalyseOptions.Cycles4];
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BAFullSubGraphTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            SortedDictionary<int, int> actualValue = analyzer.GetFullSubGraph();
            SortedDictionary<int, int> expectedValue = goldResult.Results[0].FullSubgraphs;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BAMinPathDistTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            SortedDictionary<int, int> actualValue = analyzer.GetMinPathDist();
            SortedDictionary<int, int> expectedValue = goldResult.Results[0].DistanceBetweenVertices;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BADistEigenPathTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            SortedDictionary<double, int> actualValue = analyzer.GetDistEigenPath();
            SortedDictionary<double, int> expectedValue = goldResult.Results[0].DistancesBetweenEigenValues;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BAMotivesTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            SortedDictionary<int, float> actualValue = analyzer.GetMotif(2, 4);
            SortedDictionary<int, float> expectedValue = goldResult.Results[0].MotivesCount;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BADiameterTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            double actualValue = analyzer.GetDiameter();
            double expectedValue = goldResult.Results[0].Result[AnalyseOptions.Diameter];
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("BAModelTestData//BAOutput.xml")]
        [DeploymentItem("BAModelTestData//BAInput.txt")]
        public void BAEigenValueTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("BAOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("BAInput.txt");
            BAGraph graph = new BAGraph(matrix);
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);

            ArrayList actualValue = analyzer.GetEigenValue();
            ArrayList expectedValue = goldResult.Results[0].EigenVector;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }
    }
}
