using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Motifs
{
    public static class MotifFinder
    {
        public static Dictionary<Graph, float> MotifDictionary = new Dictionary<Graph,float>();
        /// <summary>
        /// creat given size graphs,by loading data in files from given path 
        /// create and puts in MotifDictionary keys creating graphs
        /// </summary>
        /// <param name="path"></param>
        /// <param name="size"></param>
        public static void PreloadMotifSamples(string path, int size)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                throw new Exception("The directory you are trying to access does not exist");
            }

            FileInfo[] motifFiles = dir.GetFiles();
            foreach (FileInfo info in motifFiles)
            {
                if (info.Extension == ".txt")
                {
                    Graph graph = Graph.ReadFromFile(info.FullName, size, size);
                    MotifDictionary.Add(graph, 0);

                }
            }
        }
        /// <summary>
        /// search in network given size motifs and after changes MotifDictionary values
        /// </summary>
        /// <param name="network"></param>
        /// <param name="motifSize"></param>
        public static void SearchMotifs(Graph network, int motifSize)
        {
             String pathName = "graph" + motifSize;
        
            MotifFinder.PreloadMotifSamples(pathName, motifSize);
            int sampleingCount = 0;
            int edgeCount = network.Edges.Count;
            //int sampleingCountForGivenMotif = edgeCount * edgeCount;
        //  int sampleingCountForGivenMotif = edgeCount * motifSize;
       //     int sampleingCountForGivenMotif = edgeCount;
            int sampleingCountForGivenMotif = 5000;
           while (sampleingCount < sampleingCountForGivenMotif)
 //           while (sampleingCount < edgeCount)
            {
                ICollection<Graph> graphs;
                graphs = MotifDictionary.Keys;
                Graph graph = SubgraphSampler.GetRandomSubgraphESA(network, motifSize);

                bool isIsomorf = false;
                foreach (Graph keyGraph in MotifDictionary.Keys.ToList()) 
                {
                   // MotifFinder.PrintGraphToConsole(graph);
                    if (Isomorphism.AreIsomorph(keyGraph, graph))
                    {
                        MotifDictionary[keyGraph]++;
                        isIsomorf = true;
                        break;
                    }
                }
         //       if (!isIsomorf)
          //      {
         //           Console.WriteLine("bad graph");
          //          MotifFinder.PrintGraphToConsole(graph);
         //       }
                sampleingCount++;
            }
            
            if (MotifDictionary.Keys.Count == 0)
            {
                throw new Exception("You must preload motif samples first.");
            }
            foreach (Graph keyGraph in MotifDictionary.Keys.ToList())
            {
                MotifDictionary[keyGraph] = MotifDictionary[keyGraph] / sampleingCountForGivenMotif;
                  
            }
        }
        /// <summary>
        /// prinnt graph edges indexes in console
        /// </summary>
        /// <param name="graph"></param>
        public static void PrintGraphToConsole(Graph graph)
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
    }
}
