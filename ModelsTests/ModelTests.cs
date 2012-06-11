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

namespace ModelsTests
{
    [TestClass]
    public class Hierarchictests
    {
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


        [TestMethod]
        public void WSModelTest()
        {
            XMLResultStorage resultStorage = new XMLResultStorage("C:\\ComplexNetwork");
            ResultAssembly goldResult = resultStorage.Load(new Guid("3c1a04a6-8869-4c8b-9213-6eed61125a5c"));
            WSGraph graph = new WSGraph(get_data("C:\\Users\\Artak\\Desktop\\Complex Network\\ModelsTests\\testData\\test.txt"));
            IGraphAnalyzer analyzer = new WSAnalyzer(graph.Container);
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
            Assert.AreEqual(goldResult.Results[0].Result[AnalyseOptions.AveragePath], analyzer.GetAveragePath());
        }


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
