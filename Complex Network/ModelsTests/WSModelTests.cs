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
using Model.WSModel.Realization;
using Model.ERModel;
using GenericAlgorithms;

namespace ModelsTests
{
    [TestClass]
    public class WSModelTest
    {
        //const string RESULTPATH = Server.MapPath("~");
        //const string INPUTPATH = "";

        private bool compare(SortedDictionary<int, int> a, SortedDictionary<int, int> b)
        {
            return a.SequenceEqual(b);
        }

        private bool compare(SortedDictionary<double, int> a, SortedDictionary<double, int> b)
        {
            return a.SequenceEqual(b);
        }

        private bool compare(SortedDictionary<int, long> a, SortedDictionary<int, long> b)
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
        [Timeout(100)]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void AveragePathTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSContainer container = new WSContainer();
            container.SetMatrix(matrix);
            AbstarctGraphAnalyzer analyzer = new WSAnalyzer(container);

            double actualValue = analyzer.GetAveragePath();
            double expectedValue = goldResult.Results[0].Result[AnalyseOptions.AveragePath];
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [Timeout(100)]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void ClusteringCoefficientTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSContainer container = new WSContainer();
            container.SetMatrix(matrix);
            AbstarctGraphAnalyzer analyzer = new WSAnalyzer(container);

            SortedDictionary<double, int> actualValue = analyzer.GetClusteringCoefficient();
            SortedDictionary<double, int> expectedValue = goldResult.Results[0].Coefficient;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100)]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void DegreeDistributionTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSContainer container = new WSContainer();
            container.SetMatrix(matrix);
            AbstarctGraphAnalyzer analyzer = new WSAnalyzer(container);

            SortedDictionary<int, int> actualValue = analyzer.GetDegreeDistribution();
            SortedDictionary<int, int> expectedValue = goldResult.Results[0].VertexDegree;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100)]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void CyclesTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSContainer container = new WSContainer();
            container.SetMatrix(matrix);
            AbstarctGraphAnalyzer analyzer = new WSAnalyzer(container);

            SortedDictionary<int, long> actualValue = analyzer.GetCycles(4, 6);
            SortedDictionary<int, long> expectedValue = goldResult.Results[0].Cycles;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100)]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void Cycles3Test()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSContainer container = new WSContainer();
            container.SetMatrix(matrix);
            AbstarctGraphAnalyzer analyzer = new WSAnalyzer(container);

            long actualValue = analyzer.GetCycles3();
            long expectedValue = goldResult.Results[0].Cycles3;
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [Timeout(100)]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void Cycles4Test()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSContainer container = new WSContainer();
            container.SetMatrix(matrix);
            AbstarctGraphAnalyzer analyzer = new WSAnalyzer(container);

            long actualValue = analyzer.GetCycles3();
            long expectedValue = goldResult.Results[0].Cycles4;
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [Timeout(100)]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void FullSubGraphTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSContainer container = new WSContainer();
            container.SetMatrix(matrix);
            AbstarctGraphAnalyzer analyzer = new WSAnalyzer(container);

            SortedDictionary<int, int> actualValue = analyzer.GetFullSubGraph();
            SortedDictionary<int, int> expectedValue = goldResult.Results[0].FullSubgraphs;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100)]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void MinPathDistTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSContainer container = new WSContainer();
            container.SetMatrix(matrix);
            AbstarctGraphAnalyzer analyzer = new WSAnalyzer(container);

            SortedDictionary<int, int> actualValue = analyzer.GetMinPathDist();
            SortedDictionary<int, int> expectedValue = goldResult.Results[0].DistanceBetweenVertices;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100)]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void DistEigenPathTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSContainer container = new WSContainer();
            container.SetMatrix(matrix);
            AbstarctGraphAnalyzer analyzer = new WSAnalyzer(container);

            SortedDictionary<double, int> actualValue = analyzer.GetDistEigenPath();
            SortedDictionary<double, int> expectedValue = goldResult.Results[0].DistancesBetweenEigenValues;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        /*  Note support yet
         *         [TestMethod]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
          public void MotivesTest()
          {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSGraph graph = new WSGraph(matrix);
            IGraphAnalyzer analyzer = new WSAnalyzer(graph.Container);

              SortedDictionary<int, int> actualValue = analyzer.GetMotif();
              SortedDictionary<int, int> expectedValue = goldResult.Results[0].MotivesCount;
              Assert.IsTrue(compare(actualValue, expectedValue));
          }

        [TestMethod]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
          public void DiameterTest()
          {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSGraph graph = new WSGraph(matrix);
            IGraphAnalyzer analyzer = new WSAnalyzer(graph.Container);

              double actualValue = analyzer.GetDiameter();
              double expectedValue = goldResult.Results[0].Result[AnalyseOptions.Diameter];
              Assert.AreEqual(actualValue, expectedValue);
          }*/

        [TestMethod]
        [Timeout(100)]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void EigenValueTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSContainer container = new WSContainer();
            container.SetMatrix(matrix);
            AbstarctGraphAnalyzer analyzer = new WSAnalyzer(container);

            ArrayList actualValue = analyzer.GetEigenValues();
            ArrayList expectedValue = goldResult.Results[0].EigenVector;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }
    }
}
