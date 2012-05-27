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
using CommonLibrary.Model;
using Model.ERModel;
using GenericAlgorithms;
using model.ERModel.Realization;
using Model.ERModel.Realization;

namespace ModelsTests
{
    [TestClass]
    public class ERModelTest
    {
        private bool compare(SortedDictionary<int, int> a, SortedDictionary<int, int> b)
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
                if ((double)a[i] != (double)b[i])
                {
                    return false;
                }
            }
            return true;
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("ERModelTestData//EROutput.xml")]
        [DeploymentItem("ERModelTestData//ERInput.txt")]
        public void ERAveragePathTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

            double actualValue = analyzer.GetAveragePath();
            double expectedValue = goldResult.Results[0].Result[AnalyseOptions.AveragePath];
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("ERModelTestData//EROutput.xml")]
        [DeploymentItem("ERModelTestData//ERInput.txt")]
        public void ERClusteringCoefficientTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

            SortedDictionary<double, int> actualValue = analyzer.GetClusteringCoefficient();
            SortedDictionary<double, int> expectedValue = goldResult.Results[0].Coefficient;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("ERModelTestData//EROutput.xml")]
        [DeploymentItem("ERModelTestData//ERInput.txt")]
        public void ERDegreeDistributionTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

            SortedDictionary<int, int> actualValue = analyzer.GetDegreeDistribution();
            SortedDictionary<int, int> expectedValue = goldResult.Results[0].VertexDegree;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("ERModelTestData//EROutput.xml")]
        [DeploymentItem("ERModelTestData//ERInput.txt")]
        public void ERCyclesTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

            SortedDictionary<int, long> actualValue = analyzer.GetCycles(4, 6);
            SortedDictionary<int, long> expectedValue = goldResult.Results[0].Cycles;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("ERModelTestData//EROutput.xml")]
        [DeploymentItem("ERModelTestData//ERInput.txt")]
        public void ERCycles3Test()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

            int actualValue = analyzer.GetCycles3();
            int expectedValue = (int)goldResult.Results[0].Result[AnalyseOptions.Cycles3];
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("ERModelTestData//EROutput.xml")]
        [DeploymentItem("ERModelTestData//ERInput.txt")]
        public void ERCycles4Test()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

            int actualValue = analyzer.GetCycles4();
            //FIXME
            //int expectedValue = goldResult.Results[0].Cycles4;
            int expectedValue = (int)goldResult.Results[0].Result[AnalyseOptions.Cycles4];
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("ERModelTestData//EROutput.xml")]
        [DeploymentItem("ERModelTestData//ERInput.txt")]
        public void ERFullSubGraphTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

            SortedDictionary<int, int> actualValue = analyzer.GetFullSubGraph();
            SortedDictionary<int, int> expectedValue = goldResult.Results[0].FullSubgraphs;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("ERModelTestData//EROutput.xml")]
        [DeploymentItem("ERModelTestData//ERInput.txt")]
        public void ERMinPathDistTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

            SortedDictionary<int, int> actualValue = analyzer.GetMinPathDist();
            SortedDictionary<int, int> expectedValue = goldResult.Results[0].DistanceBetweenVertices;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("ERModelTestData//EROutput.xml")]
        [DeploymentItem("ERModelTestData//ERInput.txt")]
        public void ERDistEigenPathTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

            SortedDictionary<double, int> actualValue = analyzer.GetDistEigenPath();
            SortedDictionary<double, int> expectedValue = goldResult.Results[0].DistancesBetweenEigenValues;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }

        /*  Note support yet
         * [TestMethod]
          [DeploymentItem("ERModelTestData//EROutput.xml")]
          [DeploymentItem("ERModelTestData//ERInput.txt")]
          public void MotivesTest()
          {
              XMLResultStorage resultStorage = new XMLResultStorage("");
              ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
              ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
              ERGraph graph = new ERGraph(matrix);
              IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

              SortedDictionary<int, int> actualValue = analyzer.GetMotif();
              SortedDictionary<int, int> expectedValue = goldResult.Results[0].MotivesCount;
              Assert.IsTrue(compare(actualValue, expectedValue));
          }

          [TestMethod]
          [DeploymentItem("ERModelTestData//EROutput.xml")]
          [DeploymentItem("ERModelTestData//ERInput.txt")]
          public void DiameterTest()
          {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

              double actualValue = analyzer.GetDiameter();
              double expectedValue = goldResult.Results[0].Result[AnalyseOptions.Diameter];
              Assert.AreEqual(actualValue, expectedValue);
          }*/

        [TestMethod]
        [Timeout(100000)]
        [DeploymentItem("ERModelTestData//EROutput.xml")]
        [DeploymentItem("ERModelTestData//ERInput.txt")]
        public void EREigenValueTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("EROutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("ERInput.txt");
            ERGraph graph = new ERGraph(matrix);
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);

            ArrayList actualValue = analyzer.GetEigenValue();
            ArrayList expectedValue = goldResult.Results[0].EigenVector;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }
    }
}
