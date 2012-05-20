using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 namespace Motifs
{
        /// <summary>
        /// helper class for isomorfis,
        /// </summary>
    public class Sign : IComparable
    {
        public int binarySign;
        public int collDistance;
        public int pairNumVertices;
        public int pairNumEdges;
        public Sign(int binarySign, int collDistance, int pairNumVertices, int pairNumEdges)
        {
            this.binarySign = binarySign;
            this.collDistance = collDistance;
            this.pairNumVertices = pairNumVertices;
            this.pairNumEdges = pairNumEdges;
        }
        /// <summary>
        /// default constructor
        /// </summary>
        public Sign()
        {
            binarySign = 0;
            collDistance = 0;
            pairNumEdges = 0;
            pairNumVertices = 0;
        }
        /// <summary>
        /// implement CompareTo method of IComparable
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int CompareTo(Object s)
        {
            return this.ToString().CompareTo(((Sign)s).ToString());
        }
        /// <summary>
        /// override toString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return binarySign == 1 ? "+" : "-" + collDistance + "." + pairNumVertices + "." + pairNumEdges;
        }
        /// <summary>
        /// override equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(Sign))
                return false;
            if (this.binarySign == ((Sign)obj).binarySign
                && this.collDistance == ((Sign)obj).collDistance
                && this.pairNumEdges == ((Sign)obj).pairNumEdges
                && this.pairNumVertices == ((Sign)obj).pairNumVertices)
                return true;
            return false;
        }
    }

        /// <summary>
        /// class for garph"s Isomorfism
        /// </summary>
    public static class Isomorphism
    {
        //Dijkstra's shortest paths
        public static List<List<Vertice>> GetDijkstraShortestPaths(Graph graph, Vertice vSource)
        {
            int[] distances = new int[graph.Vertices.Count];
            bool[] visited = new bool[graph.Vertices.Count];
            visited[vSource.index] = true;

            foreach (Vertice vOuter in graph.Vertices)
                distances[vOuter.index] = GetTentativeDistance(graph, vOuter, vSource);

            while (true)
            {
                int iMin = GetMinimumUnvisitedIndex(distances, visited);
                visited[iMin] = true;

                foreach (Vertice vCurrent in graph.Vertices)
                {
                    if (visited[vCurrent.index] == false)
                    {
                        if (GetTentativeDistance(graph, graph.Vertices[iMin], vCurrent) == int.MaxValue)
                            continue;

                        distances[vCurrent.index] = Math.Min(distances[iMin] + GetTentativeDistance(graph, graph.Vertices[iMin], vCurrent), distances[vCurrent.index]);
                    }
                }

                bool finished = true;
                for (int i = 0; i < visited.Length; i++)
                {
                    if (visited[i] == false && distances[i] != int.MaxValue)
                    {
                        finished = false;
                        break;
                    }
                }
                if (finished)
                    break;
            }

            var shortestPaths = new List<List<Vertice>>();

            foreach (Vertice vCurrent in graph.Vertices)
            {
                Vertice vTemp = vCurrent;
                shortestPaths.Add(new List<Vertice>());
                shortestPaths[vCurrent.index].Insert(0, vCurrent);
                if (distances[vCurrent.index] == int.MaxValue)
                {
                    shortestPaths[vCurrent.index] = null;
                    continue;
                }
                if (vCurrent == vSource)
                {
                    shortestPaths[vCurrent.index].Add(vSource);
                    continue;
                }

                while (true)
                {
                    foreach (Edge eCurrent in graph.Edges)
                    {
                        if (eCurrent.v1 == vTemp.index && distances[eCurrent.v2] == distances[vTemp.index] - 1)
                        {
                            shortestPaths[vCurrent.index].Insert(0, graph.Vertices[eCurrent.v2]);
                            vTemp = graph.Vertices[eCurrent.v2];
                            break;
                        }
                        else if (eCurrent.v2 == vTemp.index && distances[eCurrent.v1] == distances[vTemp.index] - 1)
                        {
                            shortestPaths[vCurrent.index].Insert(0, graph.Vertices[eCurrent.v1]);
                            vTemp = graph.Vertices[eCurrent.v1];
                            break;
                        }
                    }
                    if (vTemp == vSource)
                    {
                        break;
                    }
                }
            }
            return shortestPaths;
        } //Procedure 3.1       
        private static int GetTentativeDistance(Graph graph, Vertice v1, Vertice v2)
        {
            if (v1 == v2)
                return 0;
            if (graph.Contains(new Edge(v1.index, v2.index)))
                return 1;
            return int.MaxValue;
        }
        private static int GetMinimumUnvisitedIndex(int[] array, bool[] vis)
        {
            int minIndex = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (vis[i] == false)
                {
                    minIndex = i;
                    break;
                }
            }

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] <= array[minIndex] && vis[i] == false)
                    minIndex = i;
            }

            return minIndex;
        }

        //Collaterial and Pair graphs
        private static Graph GetCollaterialGraph(Graph graph, Edge e)
        {
            if (!graph.Contains(e))
                return graph;

            Graph collaterial = new Graph();

            collaterial.Vertices = graph.Vertices;

            foreach (Edge current in graph.Edges)
            {
                if (current != e)
                    collaterial.Edges.Add(current);
            }

            return collaterial;
        }
        private static int GetCollaterialSign(Graph graph, Edge e)
        {
            if (graph.Contains(e))
                return 1;
            return -1;
        }
        private static Graph GetPairGraph(Graph graph, Vertice u, Vertice v)
        {
            List<List<Vertice>> paths = GetDijkstraShortestPaths(graph, u);
            List<Vertice> shortestPathUV = paths[v.index];

            Graph pairUV = new Graph();
            if (shortestPathUV != null)
            {
                foreach (Vertice current in shortestPathUV)
                {
                    pairUV.Vertices.Add(current);
                    foreach (Edge edge in graph.Edges)
                    {
                        if (edge.v1 == current.index || edge.v2 == current.index)
                        {
                            pairUV.AddEdge(edge);
                        }
                    }
                }
            }

            return pairUV;

        }
        public static int GetCollaterialDistanceAndPairGraph(Graph graph, Vertice u, Vertice v,  //Procedure 3.2
            ref Graph pair)
        {
            Graph collaterial = GetCollaterialGraph(graph, new Edge(u.index, v.index));
            List<List<Vertice>> shortestPathsU = GetDijkstraShortestPaths(collaterial, u);
            List<List<Vertice>> shortestPathsV = GetDijkstraShortestPaths(collaterial, v);
            pair = GetPairGraph(graph, u, v);

            int collaterialUVDistance = shortestPathsU[graph.Vertices.IndexOf(v)].Count - 1;
            return collaterialUVDistance;
        }

        //Sign matrix and its Canonical form
        //keyword ref used to explicitly mention that the parameter is going to change
        private static string GetFrequencyVectorString(int[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (int num in array)
            {
                builder.Append(num + ",");
            }
            return builder.ToString();
        } //array converted to string for sorting purposes
        public static Sign[,] GetSignMatrix(Graph graph)
        {
            Sign[,] signMatrix = new Sign[graph.Vertices.Count, graph.Vertices.Count];

            foreach (Vertice v1 in graph.Vertices)
            {
                foreach (Vertice v2 in graph.Vertices)
                {
                    Sign sign = new Sign();
                    int i = graph.Vertices.IndexOf(v1);
                    int j = graph.Vertices.IndexOf(v2);
                    Graph collaterial = GetCollaterialGraph(graph, new Edge(v1.index, v2.index));
                    List<List<Vertice>> collPaths = GetDijkstraShortestPaths(collaterial, v1);
                    sign.binarySign = GetCollaterialSign(graph, new Edge(v1.index, v2.index));
                    if (collPaths[j] != null && collPaths[j].Count != 0)
                    {
                        sign.collDistance = collPaths[j].Count - 1;
                    }
                    Graph pair = GetPairGraph(graph, v1, v2);
                    sign.pairNumEdges = pair.Edges.Count;
                    sign.pairNumVertices = pair.Vertices.Count;
                    signMatrix[i, j] = sign;
                }
            }
            return signMatrix;
        }
        /// <summary>
        /// sort given array
        /// </summary>
        /// <param name="array"></param>
        public static void SortLexicoGraphic(IComparable[] array) //bubble sort
        {
            for (int i = 1; i < array.Length; i++)
            {
                int k = i;
                for (int j = k - 1; j >= 0; j--)
                {
                    if (array[j].CompareTo(array[k]) == 1)
                    {
                        IComparable temp = array[j];
                        array[j] = array[k];
                        array[k] = temp;
                        k--;
                    }
                }
            }
        }
        public static Sign[,] GetCanonicalForm(Sign[,] matrix)
        {
            List<Sign> distinctSigns = new List<Sign>();
            foreach (Sign s in matrix)
                if (!distinctSigns.Contains(s))
                    distinctSigns.Add(s);            

            //Get sorted array of distinct signs
            Sign[] distinctSignsArray = distinctSigns.ToArray();            
            SortLexicoGraphic(distinctSignsArray);

            //Calculate frequency vectors
            int[][] frequencyVectors = new int[matrix.GetLength(0)][];
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                Sign[] currentRow = new Sign[matrix.GetLength(0)];
                for(int j = 0; j < currentRow.Length; j++)
                    currentRow[j] = matrix[i, j];
                frequencyVectors[i] = GetSignFrequencyVector(currentRow, distinctSignsArray);
            }

            string[] frequencyVectorStrings = new string[frequencyVectors.Length];
            string[] frequencyVectorBackup = new string[frequencyVectors.Length]; //for comparison
            for (int i = 0; i < frequencyVectors.Length; i++)
                frequencyVectorStrings[i] = GetFrequencyVectorString(frequencyVectors[i]);

            Sign[,] canonical = (Sign[,])matrix.Clone();
            SortLexicoGraphic(frequencyVectorStrings);
            for (int i = 0; i < frequencyVectorStrings.Length; i++ )
            {
                //column j=>i; row j=>i
                int j = frequencyVectorBackup.ToList().IndexOf(frequencyVectorStrings[i]);
                frequencyVectorBackup[j] = null;
                SwitchRows(ref canonical, j, i);
                SwitchColumns(ref canonical, j, i);                
            }
            return canonical;
        }
        private static int[] GetSignFrequencyVector(Sign[] row, Sign[] distinct)
        {
            int[] frequency = new int[distinct.Length];
            for (int i = 0; i < frequency.Length; i++)
            {
                int count = 0;
                foreach (Sign s in row)
                    if (s.Equals(distinct[i]))
                        count++;
                frequency[i] = count;
            }
            return frequency;
        }
        private static void SwitchRows(ref Sign[,] matrix, int row1, int row2)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                Sign temp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = temp;
            }
        }
        private static void SwitchColumns(ref Sign[,] matrix, int col1, int col2)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Sign temp = matrix[i, col1];
                matrix[i, col1] = matrix[i, col2];
                matrix[i, col2] = temp;
            }
        }
        public static void GetSignMatrixAndCanonicalForm(Graph graph, ref Sign[,] matrix, ref Sign[,] canonical) //Procedure 3.3
        {
            matrix = GetSignMatrix(graph);
            canonical = GetCanonicalForm(matrix);
        }

       /// <summary>
       /// returns true if givens graphs are isomorph false otherwise
       /// </summary>
       /// <param name="A"></param>
       /// <param name="B"></param>
       /// <returns></returns>
        public static bool AreIsomorph(Graph A, Graph B)
        {
            int mismatchCount = 0;
            Sign[,] ASignMatrix = GetSignMatrix(A);
            Sign[,] BSignMatrix = GetSignMatrix(B);
            int[,] AFrequencyMatrix = GetFrequencyMatrix(ASignMatrix);
            int[,] BFrequencyMatrix = GetFrequencyMatrix(BSignMatrix);
            if (AFrequencyMatrix.Length != BFrequencyMatrix.Length)
                return false;

            for (int i = 0; i < AFrequencyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < AFrequencyMatrix.GetLength(1); j++)
                {
                    if (AFrequencyMatrix[i,j] != BFrequencyMatrix[i,j])
                    {
                        mismatchCount++;

                        for (int i1 = i; i1 < AFrequencyMatrix.GetLength(0); i1++)
                        {
                            for (int j1 = j + 1; j1 < AFrequencyMatrix.GetLength(1); j1++)
                                if (AFrequencyMatrix[i,j] == BFrequencyMatrix[i1,j1])
                                {
                                    SwitchRowsInFrequencyMatrix(ref BFrequencyMatrix, i, i1);
                                    SwitchColumnsInFrequencyMatrix(ref BFrequencyMatrix, j1, j);
                                    mismatchCount--;
                                }
                        }
                    }
                }
            }
            if (mismatchCount == 0)
            {
                return true;
            }
            return false;
        } //Procedure 3.5
        public static int[,] GetFrequencyMatrix(Sign[,] matrix)
        {
            List<Sign> distinctSigns = new List<Sign>();
            foreach (Sign s in matrix)
                if (!distinctSigns.Contains(s))
                    distinctSigns.Add(s);

            //Get sorted array of distinct signs
            Sign[] distinctSignsArray = distinctSigns.ToArray();
            SortLexicoGraphic(distinctSignsArray);
            int[] row;
            //Calculate frequency vectors
            //Calculate frequency vectors
            int[][] frequencyVectors = new int[matrix.GetLength(0)][];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Sign[] currentRow = new Sign[matrix.GetLength(0)];
                for (int j = 0; j < currentRow.Length; j++)
                    currentRow[j] = matrix[i, j];
                frequencyVectors[i] = GetSignFrequencyVector(currentRow, distinctSignsArray);
            }
            int[,] frequencyVectorsMatrix = new int[matrix.GetLength(0), distinctSignsArray.Length];
            int columnIndex = 0;
            foreach (int[] row2 in frequencyVectors)
            {
                for (int i = 0; i < row2.Length; i++)
                {
                    frequencyVectorsMatrix[columnIndex, i] = row2[i];
                }

            }
            return frequencyVectorsMatrix;
        }        
        private static void SwitchRowsInFrequencyMatrix(ref int[,] matrix, int row1, int row2)
        {
            int exchange;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                exchange = matrix[row1,i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = exchange;
            }
        }
        private static void SwitchColumnsInFrequencyMatrix(ref int[,] matrix, int col1, int col2)
        {
            int exchange;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                exchange = matrix[i,col1];
                matrix[i, col1] = matrix[i, col2];
                matrix[i, col2] = exchange;
            }
           
        }
    }    
}