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


        [TestMethod]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void AveragePathTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSGraph graph = new WSGraph(matrix);
            IGraphAnalyzer analyzer = new WSAnalyzer(graph.Container);
            double actualValue = analyzer.GetAveragePath();
            double expectedValue = goldResult.Results[0].Result[AnalyseOptions.AveragePath];
            Assert.AreEqual(actualValue, expectedValue);
        }

        [TestMethod]
        [DeploymentItem("WSModelTestData//WSOutput.xml")]
        [DeploymentItem("WSModelTestData//WSInput.txt")]
        public void ClusteringCoefficientTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSGraph graph = new WSGraph(matrix);
            IGraphAnalyzer analyzer = new WSAnalyzer(graph.Container);
            SortedDictionary<double, int> actualValue = analyzer.GetClusteringCoefficient();
            SortedDictionary<double, int> expectedValue = goldResult.Results[0].Coefficient;
            Assert.IsTrue(compare(actualValue, expectedValue));
        }
       /* [TestMethod]
        public void WSModelTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("");
            ResultAssembly goldResult = resultStorage.LoadXML("WSOutput.xml");
            ArrayList matrix = MatrixFileReader.MatrixReader("WSInput.txt");
            WSGraph graph = new WSGraph(matrix);
            IGraphAnalyzer analyzer = new WSAnalyzer(graph.Container);
            //Assert.Istrue(compare(goldResult.Results[0].VertexDegree, analyzer.GetDegreeDistribution()));
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
        }*/
        [TestMethod]
        public void WSModelTest1()
        {
           /* XMLResultStorage resultStorage = new XMLResultStorage("C:\\ComplexNetwork");
            ResultAssembly goldResult = resultStorage.Load(new Guid("3c1a04a6-8869-4c8b-9213-6eed61125a5c"));
            WSGraph graph = new WSGraph(get_data("C:\\Users\\Artak\\Desktop\\Complex Network\\ModelsTests\\testData\\test.txt"));
            IGraphAnalyzer analyzer = new WSAnalyzer(graph.Container);
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());*/
        }

      /*  [TestMethod]
        public void BAModelTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("C:\\ComplexNetwork");
            ResultAssembly goldResult = resultStorage.Load(new Guid("3c1a04a6-8869-4c8b-9213-6eed61125a5c"));
            BAGraph graph = new BAGraph(0, 0, get_data("C:\\Users\\Artak\\Desktop\\Complex Network\\ModelsTests\\testData\\test.txt"));
            IGraphAnalyzer analyzer = new BAAnalyzer(graph.Container);
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
        }

        [TestMethod]
        public void WSModelTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("C:\\ComplexNetwork");
            ResultAssembly goldResult = resultStorage.Load(new Guid("3c1a04a6-8869-4c8b-9213-6eed61125a5c"));
            ERGraph graph = new ERGraph(get_data("C:\\Users\\Artak\\Desktop\\Complex Network\\ModelsTests\\testData\\test.txt"));
            IGraphAnalyzer analyzer = new ERAnalyzer(graph.Container);
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
        }*/
    }
}
