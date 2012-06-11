using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Model.BAModel.Realization;

namespace Motifs
{
    /// <summary>
    /// class which represent graphs vertices
    /// </summary>
    public class Vertice
    {
        public int index;
        /// <summary>
        /// constructor which get vertice index
        /// </summary>
        /// <param name="index"></param>
        public Vertice(int index)
        {
            if(index >=0)
                this.index = index;
            else
                throw new ArgumentOutOfRangeException();
        }
        /// <summary>
        /// override equals method vor vertice
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (this.index == ((Vertice)obj).index)
                return true;
            return false;
        }
    }

    /// <summary>
    /// class which represent graphs edges
    /// </summary>
    public class Edge
    {
        public int v1;
        public int v2;
        /// <summary>
        /// constructor which get edges vertices indexes
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public Edge(int v1, int v2)
        {
            if(v1 >=0 && v2 >=0)
            {
                this.v1 = v1;
                this.v2 = v2;
            }
            else
                throw new ArgumentOutOfRangeException();
        }
        /// <summary>
        /// override equals method vor edge
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if ((this.v1 == ((Edge)obj).v1 && this.v2 == ((Edge)obj).v2) 
                || (this.v1 == ((Edge)obj).v2 && this.v2 == ((Edge)obj).v1))
                return true;
            return false;
        }
     }  

    /// <summary>
    /// class which represent graph
    /// </summary>
    public class Graph
    {
        public List<Vertice> Vertices;
        public List<Edge> Edges;
        /// <summary>
        /// constructor which get graph edges matrix
        /// </summary>
        /// <param name="adj"></param>
        public Graph(bool[,]adj)
        {
            this.Vertices = new List<Vertice>();
            this.Edges = new List<Edge>();

            for(int i = 0; i < adj.GetLength(0); i++)
                Vertices.Add(new Vertice(i));

            for (int i = 0; i < adj.GetLength(0); i++)
                for (int j = 0; j < i; j++)
                    if (adj[i, j] == true)
                        AddEdge(new Edge(i, j));
        }
        /// <summary>
        /// default constructor
        /// </summary>
        public Graph()
        {
            Vertices = new List<Vertice>();
            Edges = new List<Edge>();
        }
        /// <summary>
        /// add edge to our graph
        /// </summary>
        /// <param name="e"></param>
        public void AddEdge(Edge e)
        {
            if (this.Contains(e))
                return;
            
            Vertice v1 = new Vertice(e.v1);
            Vertice v2 = new Vertice(e.v2);

            if(!this.Contains(v1))
                this.Vertices.Add(v1);
            if(!this.Contains(v2))
                this.Vertices.Add(v2);

            this.Edges.Add(e);            
        }
        /// <summary>
        /// add vertice to our graph
        /// </summary>
        /// <param name="e"></param>
        public void AddVertice(Vertice v)
        {
            if (this.Contains(v))
                return;

            this.Vertices.Add(v);
        }
        /// <summary>
        /// returns vertice by given index
        /// </summary>
        /// <param name="e"></param>
        public Vertice getVerticeByIndex(int index)
        {
            foreach (Vertice v in Vertices)
            {
                if (v.index == index)
                    return v;
            }
            return null;
        }
        /// <summary>
        /// returns sub graph neighbors edges in super graph
        /// </summary>
        /// <param name="super"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static List<Edge> GetEdgesNeighborhood(Graph super, Graph sub)
        {
            List<Edge> nhood = new List<Edge>();
            foreach (Vertice v in sub.Vertices)
                foreach (Edge e in super.Edges)
                    if (e.v1 == v.index || e.v2 == v.index && !sub.Contains(e))
                        nhood.Add(e);
            return nhood;
        }
        /// <summary>
        /// returns sub graph neighbors vertices in super graph
        /// </summary>
        /// <param name="super"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static List<Vertice> GetVerticesNeighborhood(Graph super, Graph sub,Vertice subVertice)
        {
            List<Vertice> nhood = new List<Vertice>();
            foreach (Edge e in super.Edges)
                    if (subVertice.index == e.v1 && !sub.Contains(e))
                    {
                        nhood.Add(new Vertice(e.v2));
                    }
                    else if (subVertice.index == e.v2 && !sub.Contains(e))
                    {
                        nhood.Add(new Vertice(e.v1));
                    }
            return nhood;
        }
        /// <summary>
        /// returns true if graph contains given edge false otherwise
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Contains(Edge e)
        {
            foreach(Edge edge in Edges)
                if(edge.Equals(e))
                    return true;
            return false;
        }
        /// <summary>
        /// returns true if graph contains given vertice false otherwise
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool Contains(Vertice v)
        {
            foreach (Vertice vert in Vertices)
                if (vert.Equals(v))
                    return true;
            return false;
        }
        /// <summary>
        /// create and returns graph in data from given BAContainer type graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static Graph reformatToOurGraghFromBAContainer(BAContainer graph)
        {

            Graph newGraph = new Graph();
            bool[,] matrix = new bool[graph.Neighbourship.Count, graph.Neighbourship.Count];

           

            List<int> list = new List<int>();
            SortedDictionary<int, List<int>> neighbourship = graph.Neighbourship;
            for (int i = 0; i < neighbourship.Count; i++)
            {

                list = neighbourship[i];
                newGraph.Vertices.Add(new Vertice(i));
                for (int j = 0; j < list.Count; j++)
                    newGraph.Edges.Add(new Edge(i, list[j]));
            }

            return newGraph;
        }
        /// <summary>
        /// change graph edges old indexes to news,gets changes from given Dictionary
        /// </summary>
        /// <param name="edgesExchange"></param>
        public void changeEdeVerticeIndex(Dictionary<int, int> edgesExchange)
        {
            foreach (Edge edge in Edges)
            {

                edge.v1 = edgesExchange[edge.v1];
                edge.v2 = edgesExchange[edge.v2];
            }
        }
        /// <summary>
        /// read data from file and create corresponding graph
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static Graph ReadFromFile(string filename, int rows = 1000, int columns = 1000)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenRead(filename);
               // stream = new FileStream(filename, FileMode.Open);
            }
            catch (IOException ex)
            {
                throw ex;
            }

            bool[,] matrix = new bool[rows,columns];
            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                k = 0;
                for(int j = 0; j < columns; j++)
                {
                    char c = Convert.ToChar(stream.ReadByte());
                    if (c != ' ')
                    {
                        matrix[i, k] = c == '1' ? true : false;
                        k++;
                    }
                    else if (c == ' ')
                    {
                        j--;
                    }
                    
                }
       //         if(stream.ReadByte() == -1)
        //        {
         //           Console.WriteLine("row" + i + "column" + k);
         //           break;
         //       }
                if(i != rows - 1) 
                {
                    char c1 = Convert.ToChar(stream.ReadByte());
                    c1 = Convert.ToChar(stream.ReadByte());
                }
                
            }
            return new Graph(matrix);
        }

        /// <summary>
        /// read data from file and create and returns matrix which given sizes
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static bool[,] ReadMatrixFromFile(string filename, int rows = 1000, int columns = 1000)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(filename, FileMode.Open);
            }
            catch (IOException ex)
            {
                throw ex;
            }

            bool[,] matrix = new bool[rows, columns];
            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                k = 0;
                for (int j = 0; j < columns; j++)
                {
                    char c = Convert.ToChar(stream.ReadByte());
                    if (c != ' ')
                    {
                        matrix[i, k] = c == '1' ? true : false;
                        k++;
                    }
                    else if (c == ' ')
                    {
                        j--;
                    }

                }
                //         if(stream.ReadByte() == -1)
                //        {
                //           Console.WriteLine("row" + i + "column" + k);
                //           break;
                //       }
                if (i != rows - 1)
                {
                    char c1 = Convert.ToChar(stream.ReadByte());
                    c1 = Convert.ToChar(stream.ReadByte());
                }

            }
            return matrix;
        }

        public static Graph createGraphFromFile()
        {
            //Protein-Protein Network
            Graph graph = new Graph();

            for (int i = 0; i < 2114; i++)
            {
                graph.AddVertice(new Vertice(i));
            }

            StreamReader reader = new StreamReader("NDYeast.net");
            string line = reader.ReadLine();
            while (line != null)
            {
                if (line[0] == '*')
                {
                    line = reader.ReadLine();
                    continue;
                }

                string[] tokens = line.Split(' ');
                int edge1Index = Convert.ToInt32(tokens[0]);
                for (int i = 1; i < tokens.Length; i++)
                {
                    graph.AddEdge(new Edge(edge1Index - 1, Convert.ToInt32(tokens[i]) - 1));
                }
                line = reader.ReadLine();
            }
            reader.Close();
            return graph;
        }
    }
    

}
 