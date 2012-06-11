using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using RandomGraph.Common.Model;
using Model.BAModel.Result;
using NumberGeneration;

namespace Model.ERModel
{
    public class ERGraph
    {
        private int vc;
        private int ec;
        private int max_ec;
        private BitArray array;
        private SortedDictionary<double, int> m_coeff_dictionary;
        private SortedDictionary<int, int> m_degree_dictionary;
        private List<int> m_shortest_path_list;
        protected RNGCrypto r = new RNGCrypto();

// Constructor of Graph class
        public ERGraph(int vcount)
        {
            vc = vcount;
            ec = 0;
            max_ec = vc * (vc - 1) / 2;
            array = new BitArray(max_ec);
            m_coeff_dictionary = new SortedDictionary<double, int>();
            m_degree_dictionary = new SortedDictionary<int,int>();
            m_shortest_path_list = new List<int>();
        }
        public ERGraph(ArrayList m)
        {
            vc = m.Count;
            max_ec = vc * (vc - 1) / 2;
            ec = 0;
            int h;
            ArrayList tmp;
            //Console.WriteLine(vc);
            //Console.WriteLine(max_ec);

            array = new BitArray(max_ec);
            m_coeff_dictionary = new SortedDictionary<double, int>();
            m_degree_dictionary = new SortedDictionary<int, int>();
            m_shortest_path_list = new List<int>();

            //Console.WriteLine(ec);

            for (int i = 0; i < vc; ++i)
            {
                h = i * vc - i * (i + 1) / 2;
                tmp = (ArrayList)m[i];
                for (int j = 0; j < vc-1-i; ++j, ++h)
                {
                    if ((bool)tmp[j + i + 1] == false)
                    {
                        array[h] = false;
                    }
                    else
                    {
                        array[h] = true;
                        ++ec;
                    }

                    //array[h] = (bool)tmp[j+i+1];
                }
            }
        }


// Generating graph with p probability
        public void Generate(double p)
        {
            
            for (int i = 0; i < max_ec; ++i)
            {
                //mx.WaitOne();
                double a = r.NextDouble();
                //mx.ReleaseMutex();
                if (a < p)
                {
                    array[i] = true;
                    ec++;
                }
            }
        }

        public int VC
        {
            get { return vc; }
            set { vc = value; }
        }

        public int EC
        {
            get { return ec; }
            set { ec = value; }
        }

        public int MAX_C
        {
            get { return max_ec; }
            set { max_ec = value; }
        }

        public double clust_vert(int n)
        {
            double result;
            int adjc = 0, count = 0;
            int i = ((n - 1) * vc) - ((n - 1) * n / 2);
            if (i != 0)
            {
                for (int i1 = n - 2, h = 1; h < n; i1 += vc - h - 1, h++)
                {
                    if (array[i1])
                    {
                        adjc++;
                        count++;

                        for (int i2 = i1 + 1, i3 = i, j1 = 0; j1 < vc - n; ++j1, ++i2, ++i3)
                        {
                            if (array[i2] && array[i3])
                            {
                                count++;
                            }
                        }

                        for (int i4 = 1, n1 = n - 2, h1 = h - 2; i4 < h; n1 += vc - i4 - 1, h1 += vc - i4 - 1, ++i4)
                        {
                            if (array[n1] && array[h1])
                            {
                                count++;
                            }
                        }
                    }
                }
            }

            int j, k;
            int ind = i, end = (n * vc - (n * (n + 1) / 2) - 1);
            for (int l = 0; l < vc - n; ++l, ++ind)
            {
                if (array[ind])
                {
                    adjc++;
                    count++;

                    if (ind == end)
                    {
                        break;
                    }
                    for (j = ind + 1, k = i + ((ind - i + 1) * (vc - n) - (ind - i) * (ind - i + 1) / 2); j < end + 1; ++j, k++)
                    {
                        if (array[j] && array[k])
                        {
                            count++;
                        }
                    }
                }
            }
            if (adjc == 0 || adjc == 1)
            {
                result = 0.0;
            }
            else
            {
                result = (2 * (double)count) / ((double)adjc * ((double)adjc + 1));
            }

            return result;

        }
/*

// The clutering coefficient of the n-th vertex
        public double clust_vert(int n)
        {
            double result;
            int adjc = 0, count = 0;
            int i = ((n - 1) * vc) - ((n - 1) * n / 2);
            int j, h, i1, j1, h1, k;
            
            if (i != 0)
            {
                for (j = n - 2, h = 1; h < n; h++)
                {
                    if (array[j])
                    {
                        adjc++;
                        count++;

                        // Checking with n-th vertex by &&
                        for (j1 = j + 1, i1 = i, h1 = 0; h1 < vc - n; ++h1, ++j1, ++i1)
                        {
                            if (array[j1] && array[i1])
                            {
                                count++;
                            }
                        }

                        for (h1 = 1, j1 = n-2, i1 = h-2; h1 < h; j1+= vc-h1-1, i1+=vc-h1-1, ++h1)
                        {
                            if (array[j1] && array[i1])
                            {
                                count++;
                            }
                        }
                    }
                }
                j += vc - h - 1;
            }
            
            int last = (n * vc - (n * (n + 1) / 2) - 1);
            i1 = i;

            for (h = 0; h < vc - n; ++h, ++i1)
            {
                if (array[i1])
                {
                    adjc++;
                    count++;

                    if (i1 == last)
                    {
                        break;
                    }

                    k = i + ((i1 - i + 1) * (vc - n) - (i1 - i) * (i1 - i + 1) / 2);
                    for (j = i1 + 1; j < last + 1; ++j, k++)
                    {
                        if (array[j] && array[k])
                        {
                            count++;
                        }
                    }
                }
            }
            if (adjc == 0 || adjc == 1)
            {
                result = 0.0;
            }
            else
            {
                result = (2 * (double)count) / ((double)adjc * ((double)adjc + 1));
            }
                        return result;
        }
 */

// The clustering coefficient of g graph
        public double clust_graph()
        {
            double r = 0.0;
            for (int i = 1; i <= vc; ++i)
            {
                r += clust_vert(i);
            }
            return r / vc;
        }

        public double avj_deg()
        {
            double sum = 0.0;
            int[,] matrix = mult(to_adj_matrix(), to_adj_matrix());
//            print_matrix(matrix);

            for (int i = 0; i < vc; ++i)
            {
                sum += matrix[i, i];
            }

            Console.WriteLine(matrix[8,8]);
            return sum / vc;
        }

// The degree of n-th vertex
        public int degree(int n)
        {
            int i, j;
            int count = 0;
            // befor n-th degree
            for (j = 1, i = n-2; j < n; ++j)
            {
                if (true == array[i]) {
                    ++count;
                }
                i += vc - j - 1;
            }
            //after n-th degree
            i = (n-1) * vc - n * (n - 1) / 2;
            for (j = 1; j <= vc - n; ++j, ++i)
            {
                if (true == array[i])
                {
                    ++count;
                }
            }
            return count;
        }

// The average degree of g graph
        public double avg_degree()
        {
            return (double)(2*ec) / vc;
        }

//The length of shortest path between vertexes v1 and v2
        public int shortest_path(int v1, int v2)
        {
            int[,] matrix = ToMatrix();
            int i, j, k = 0, tmp1 = 0, tmp2 = 0;

            //print_matrix(matrix);
            Console.WriteLine();

            for (k = 0; k < vc; ++k)
            {
                for (i = 0; i < vc; ++i)
                {
                    for (j = 0; j < vc; ++j)
                    {
                        tmp1 = matrix[i,j];
                        tmp2 = matrix[i,k] + matrix[k,j];
                        matrix[i, j] = tmp1 < tmp2 ? tmp1 : tmp2;
                    }
                }
            }

            //print_matrix(matrix);
            return matrix[v1, v2];
        }

// The average path length of g graph
        public double avg_length()
        {
            int[,] matrix = ToMatrix();
            int i, j, k, tmp1, tmp2;
            int count = 0;
            double sum = 0.0;

//            print_matrix(matrix);

            for (k = 0; k < vc; ++k)
            {
                for (i = 0; i < vc; ++i)
                {
                    for (j = 0; j < vc; ++j)
                    {
                        tmp1 = matrix[i, j];
                        tmp2 = matrix[i, k] + matrix[k, j];
                        matrix[i, j] = tmp1 < tmp2 ? tmp1 : tmp2;
                    }
                }
            }
//            print_matrix(matrix);

            for (i = 0; i < vc-1; ++i)
            {
                for (j = i + 1; j < vc; ++j)
                {
                    if ((vc + 1) != matrix[i, j])
                    {
                        sum += matrix[i, j];
                        ++count;
                    }
                }
            }
//            Console.WriteLine("sum : {0}, count : {1}", sum, count);
            return sum / count;
        }

/*        
// Set all non zero elements of matrix to 1 
        public void correct_matrix(int[,] matrix)
        {
            for (int i = 0; i < vc; ++i) {
                for (int j = 0; j < vc; ++j) {
                    matrix[i, j] = matrix[i, j] >= 1 ? 1 : 0;
                }
            }
        }
*/

// Print the matrix
        public void print_matrix(int[,] m)
        {
            int i, j;
            for (i = 0; i < vc; ++i)
            {
                Console.WriteLine();
                for (j = 0; j < vc; ++j)
                {
                    Console.Write(" {0} ", m[i, j]);
                }
            }
        }

// Generating adjacency matrix of g graph
        public int[,] ToMatrix()
        {
            int i = 0, j;
            int[,] matrix = new int[vc, vc];
            for (int h = 0; h < vc - 1; ++h)
            {
                i = vc * h - h * (h + 1) / 2;
                for (j = 0; j < vc-1-h; ++j, ++i)
                {
                    if (array[i] == true)
                    {
                        matrix[h, j + h + 1] = 1;
                        matrix[j + h + 1, h] = 1;
                    }

                    // Added for algorithm to finding the shorthest path
                    else
                    {
                        matrix[h, j + h + 1] = vc + 1;
                        matrix[j + h + 1, h] = vc + 1;
                    }
                }
            }

            return matrix;
        }

// Generating the adjacency matrix of g graph
        public bool[,] to_adj_matrix_bool()
        {
            int i, j;
            bool[,] matrix = new bool[vc, vc];
            for (int h = 0; h < vc - 1; ++h)
            {
                i = vc * h - h * (h + 1) / 2;

                for (j = 0; j < vc-1-h; ++j, ++i)
                {
                    if (array[i] == true)
                    {
                        matrix[h, j + h + 1] = true;
                        matrix[j + h + 1, h] = true;
                    }
                    else
                    {
                        matrix[h, j + h + 1] = false;
                        matrix[j + h + 1, h] = false;
                    }
                }
            }

            return matrix;
        }

        public int[,] to_adj_matrix()
        {
            int i, j;
            int[,] matrix = new int[vc, vc];
            for (int h = 0; h < vc - 1; ++h)
            {
                i = vc * h - h * (h + 1) / 2;

                for (j = 0; j < vc - 1 - h; ++j, ++i)
                {
                    if (array[i] == true)
                    {
                        matrix[h, j + h + 1] = 1;
                        matrix[j + h + 1, h] = 1;
                    }
                    else
                    {
                        matrix[h, j + h + 1] = 0;
                        matrix[j + h + 1, h] = 0;
                    }
                }
            }

            return matrix;
        }

        public int[,] mult(int[,] matr1, int[,] matr2)
        {
            int[,] result = new int[vc, vc];
            for (int i = 0; i < vc; ++i)
            {
                for (int j = 0; j < vc; ++j)
                {
                    for (int k = 0; k < vc; ++k)
                    {
                        result[i, j] += matr1[i, k] * matr2[k, j];
                    }
                }
            }
//            print_matrix(result);
//            correct_matrix(result);
            return result;
        }

// Function for finding the number of 3-length cycles in the graph
        public int cycle_3(int k)
        {
            int[,] m = to_adj_matrix();
            int i = 1;

//            print_matrix(m);
//            Console.WriteLine();

            int num = 0;
            for (; i < k; ++i)
            {
                m = mult(m, to_adj_matrix());
            }
            
//            print_matrix(m);
//            Console.WriteLine();

            for (i = 0; i < vc; ++i)
            {
                num += m[i, i];
            }
            return num / 6;
        }

        public SortedDictionary<double, int> get_coeff_dict()
        {
            double r = 0.0;
            for (int i = 1; i <= vc; ++i)
            {
                r = Math.Round(clust_vert(i), 4);
                if (m_coeff_dictionary.ContainsKey(r))
                {
                    m_coeff_dictionary[r]++;
                }
                else
                {
                    m_coeff_dictionary.Add(r, 1);
                }
            }
            return m_coeff_dictionary;
        }

        public SortedDictionary<int, int> get_degree_dict()
        {
            int v = 0;
            for (int i = 1; i <= vc; ++i)
            {
                v = degree(i);
                if (m_degree_dictionary.ContainsKey(v))
                {
                    m_degree_dictionary[v]++;
                }
                else
                {
                    m_degree_dictionary.Add(v, 1);
                }

            }
            return m_degree_dictionary;
        }

        public List<int> get_shortes_path_list()
        {
            for (int i = 1; i <= vc; ++i)
            {
                for (int j = i + 1; j <= vc; ++j)
                {
                    m_shortest_path_list.Add(shortest_path(i - 1, j - 1));
                }
            }
            return m_shortest_path_list;
        }

// Show the graph g by bitarray
        public void to_array()
        {
            int n = 1;

            Console.Write("|");
            for (int i = 0; i < max_ec; ++i)
            {
                if (i == (n*vc - n*(n+1)/2))
                {
                    Console.Write("|");
                    n++;
                }
                if (array[i])
                {
                    Console.Write("1");
                }
                else
                {
                    Console.Write("0");
                }
            }
            Console.Write("\n");
        }
    }  
}