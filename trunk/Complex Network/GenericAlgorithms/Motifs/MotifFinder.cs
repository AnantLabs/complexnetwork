using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using log4net;

namespace Algorithms
{
    public class MotifFinder
    {

        public static readonly ILog log = log4net.LogManager.GetLogger(typeof(MotifFinder));
        public  Dictionary<Graph, float> MotifDictionary;
        public  Dictionary<Graph, int> MotifDictionaryIds;
        /// <summary>
        /// creat given size graphs,by loading data in files from given path 
        /// create and puts in MotifDictionary keys creating graphs
        /// </summary>
        /// <param name="path"></param>
        /// <param name="size"></param>
        public  void PreloadMotifSamples(string path, int size)
        {
            MotifDictionaryIds = new Dictionary<Graph, int>();
            MotifDictionary = new Dictionary<Graph, float>();
            String currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(currentDirectory.Substring(0, currentDirectory.Length - 10)+'\\'+ path);
            if (!dir.Exists)
            {
                log.Info("The directory you are trying to access does not exist");
                throw new Exception("The directory you are trying to access does not exist");
            }
            FileInfo[] motifFiles = dir.GetFiles();
            foreach (FileInfo info in motifFiles)
            {
                if (info.Extension == ".txt")
                {
                    Graph graph = Graph.ReadFromFile(info.FullName, size, size);
                    MotifDictionary.Add(graph, 0);
        
                    MotifDictionaryIds.Add(graph, Convert.ToInt32(info.Name.Substring(0,3)));

                }
            }
        }
        /// <summary>
        /// search in network given size motifs and after changes MotifDictionary values
        /// </summary>
        /// <param name="network"></param>
        /// <param name="motifSize"></param>
        public  void SearchMotifs(Graph network, int motifSize)
        {
            String pathName = "graph" + motifSize;
            PreloadMotifSamples(pathName, motifSize);
            int sampleingCount = 0;
            int edgeCount = network.Edges.Count;
            int sampleingCountForGivenMotif = 5000;
            int subGraphsCount = 0;
            log.Info("Sampling start");
            using (StreamWriter outfile = new StreamWriter("D:\\test.txt"))
            {
                while (sampleingCount < sampleingCountForGivenMotif)
                {
                    SubgraphSampler subgraphSampler = new SubgraphSampler();
                    ICollection<Graph> graphs;
                    graphs = MotifDictionary.Keys;
                    Graph graph = subgraphSampler.GetRandomSubgraphESU(network, motifSize);
                    if (graph != null)
                    {
                        bool isIsomorf = false;
                        foreach (Graph keyGraph in MotifDictionary.Keys.ToList())
                        {
                            MotifFinder.PrintGraphToConsole(graph, outfile);
                            //MotifFinder.PrintGraphToConsole(keyGraph);
                            if (Isomorphism.AreIsomorph(keyGraph, graph))
                            {
                                MotifDictionary[keyGraph]++;
                                isIsomorf = true;
                                break;
                            }
                        }
                        if (!isIsomorf)
                        {
                            StringBuilder str = new StringBuilder();
                            foreach (Edge e in graph.Edges)
                            {
                                str.Append(e.v1 + "-" + e.v2);
                            }
                            log.Info("this graph don»t have isomorf" + str);
                            outfile.WriteLine("bad graph");
                            MotifFinder.PrintGraphToConsole(graph, outfile);
                        }
                        subGraphsCount++;

                    }
                    sampleingCount++;
                    
                }


                if (MotifDictionary.Keys.Count == 0)
                {
                    log.Info("You must preload motif samples first.");
                    throw new Exception("You must preload motif samples first.");
                }
                foreach (Graph keyGraph in MotifDictionary.Keys.ToList())
                {
                    MotifDictionary[keyGraph] = MotifDictionary[keyGraph] / subGraphsCount;

                }
                log.Info("Sampling end");
                try
                {
                    
                }
                catch (Exception ex)
                {
                    log.Info(ex.Message);
                    Console.WriteLine(ex.Message);
                }
                
            }
                
        }
        /// <summary>
        /// prinnt graph edges indexes in console
        /// </summary>
        /// <param name="graph"></param>
        public  void PrintGraphToConsole(Graph graph)
        {
            Console.WriteLine();

            Console.WriteLine("Vertice count: " + graph.Vertices.Count);

            Console.Write("Edges: ");
            foreach (Edge e in graph.Edges)
            {
                Console.Write(e.v1 + "-" + e.v2);
            }

            Console.WriteLine();
        }

        public  SortedDictionary<int, float> dictionaryIdsValues()
        {
            SortedDictionary<int, float> motifsInfo = new SortedDictionary<int, float>();
            ICollection<Graph> graphs =MotifDictionary.Keys.ToList();
            foreach(Graph keyGraph in graphs)
            {
                motifsInfo.Add(MotifDictionaryIds[keyGraph], MotifDictionary[keyGraph]);
            }
            return motifsInfo;
        }

        //public static XmlDocument createDocument(int motifSize)
        //{
        //    SortedDictionary<int, float> results = MotifFinder.dictionaryIdsValues();
        //    ICollection<int> graphs = results.Keys;
        //    List<int> keys = graphs.ToList();
        //    var document = new XmlDocument();
        //    XmlElement root = document.CreateElement("motifs" + motifSize);
        //    document.AppendChild(root);

        //    for (int i = 0; i < keys.Count; i++)
        //    {

        //        XmlElement node = document.CreateElement("m" + keys[i]);
        //        node.SetAttribute("count", Convert.ToString(results[keys[i]]));
        //        root.AppendChild(node);
        //    }

        //    return document;
        //}

        //public static XmlDocument createDocument(int motifSize)
        //{
        //    Dictionary<int, float> results = MotifFinder.dictionaryIdsValues();
        //    ICollection<int> graphs = results.Keys;
        //    List<int> keys = graphs.ToList();
        //    var document = new XmlDocument();
        //    XmlElement root = document.CreateElement("motifs" + motifSize);
        //    document.AppendChild(root);

        //    for (int i = 0; i < keys.Count; i++)
        //    {

        //        XmlElement node = document.CreateElement("m" + keys[i]);
        //        node.SetAttribute("count", Convert.ToString(results[keys[i]]));
        //        root.AppendChild(node);
        //    }

        //    return document;
        //}
        /// <summary>
        /// prinnt graph edges indexes in console
        /// </summary>
        /// <param name="graph"></param>
        public static void PrintGraphToConsole(Graph graph, StreamWriter outfile)
        {

            //Console.WriteLine();
            outfile.WriteLine();
            // Console.WriteLine("Vertice count: " + graph.Vertices.Count);
            outfile.WriteLine("Vertice count: " + graph.Vertices.Count);
            outfile.WriteLine("Edges: ");
            //  Console.Write("Edges: ");
            foreach (Edge e in graph.Edges)
            {

                outfile.WriteLine(e.v1 + "-" + e.v2);


                // Console.Write(e.v1 + "-" + e.v2);
            }

            // Console.WriteLine();
            outfile.WriteLine();

        }
    }
}
