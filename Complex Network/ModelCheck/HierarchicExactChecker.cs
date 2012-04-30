using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using log4net;

namespace ModelCheck
{
    public class HierarchicExactChecker
    {
        private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(HierarchicExactChecker));

        private Neighbourship _container;
        private TextWriter _fileWriter = null;

        public static void main()
        {
            ArrayList matrix = Neighbourship.get_data("C:/ComplexNetwork/graph.txt");
            HierarchicExactChecker checker = new HierarchicExactChecker();
            checker.IsHierarchic(matrix);
        }

        public bool IsHierarchic(ArrayList matrix)
        {
            _container = new Neighbourship(matrix);
            ICollection<int> primeNumbers = getAllDegrees(_container.Size).Keys;
            foreach (int prime in primeNumbers)
            {
                if (isHierarchic(prime))
                {
                    return true;
                }
            }
            return false;
        }

        private static IDictionary<int, int> getAllDegrees(long n)
        {
            Dictionary<int, int> collection = new Dictionary<int, int>();
            long degree = 0;
            for (int p = 2; (degree = p * p) <= n; ++p)
            {
                int k = 2;
                while (degree < n)
                {
                    degree *= p;
                    ++k;
                }
                if (degree == n)
                {
                    collection.Add(p, k);
                }
            }
            return collection;
        }

        private bool isHierarchic(int prime)
        {
            List<List<long>> combination = generateCombination(prime, _container.Size);
            if (combination != null)
            {
                return true;
            }
            return false;
        }

        public List<List<long>> generateCombination(long p, long n)
        {
            List<List<long>> combination = new List<List<long>>();
            try
            {
                long pivot = 0;
                List<long> group = getFirstGroup(pivot, p, n);
                combination.Add(group);
                do
                {
                    combination = getNextCombination(combination, p, n);
                } while (combination != null && riseUp(combination) == false);
                if (_fileWriter != null)
                {
                    _fileWriter.Close();
                }
            }
            catch (System.Exception e)
            {
                logger.Error("Failed to generate combinations. The reason was: " + e.Message);
                logger.Info(null, e);
                return null;
            }
            return combination;
        }

        private List<long> getFirstGroup(long pivot, long p, long n)
        {
            List<long> group = new List<long>();
            for (long i = 0; i < p; ++i)
            {
                group.Add(i);
            }
            return group;
        }

        private List<List<long>> getNextCombination(List<List<long>> originalCombination, long p, long n)
        {
            Debug.Assert(originalCombination.Count != 0);
            List<List<long>> combination = new List<List<long>>(originalCombination);
            if (combination.Count == n / p)
            {
                List<long> group = null;
                while (group == null && 0 < combination.Count)
                {
                    group = combination[combination.Count - 1];
                    combination.RemoveAt(combination.Count - 1);
                    group = getNextValidGroup(combination, group, p, n);
                }
                if (group != null)
                {
                    combination.Add(group);
                }
            }
            while (0 < combination.Count && combination.Count < n / p)
            {
                List<long> group = null;
                group = getNextValidGroup(combination, p, n);
                if (group != null)
                {
                    combination.Add(group);
                }
                else
                {
                    while (group == null && 0 < combination.Count)
                    {
                        group = combination[combination.Count - 1];
                        combination.RemoveAt(combination.Count - 1);
                        group = getNextValidGroup(combination, group, p, n);
                    }
                    if (group != null)
                    {
                        combination.Add(group);
                    }
                }
            }
            if (combination.Count == n / p)
            {
                return combination;
            }
            Debug.Assert(combination.Count == 0);
            return null;
        }

        private List<long> getNextValidGroup(List<List<long>> combination, long p, long n)
        {
            SortedSet<long> set = new SortedSet<long>();
            foreach (List<long> group in combination)
            {
                foreach (long vertex in group)
                {
                    set.Add(vertex);
                }
            }
            List<long> nextGroup = null;
            do
            {
                nextGroup = getNextGroup(combination, set, p, n);
            } while (nextGroup != null && checkConnections(combination, nextGroup, p, n) == false);
            return nextGroup;
        }

        private List<long> getNextValidGroup(List<List<long>> combination, List<long> group, long p, long n)
        {
            SortedSet<long> set = new SortedSet<long>();
            foreach (List<long> g in combination)
            {
                foreach (long vertex in g)
                {
                    set.Add(vertex);
                }
            }
            List<long> nextGroup = group;
            do
            {
                nextGroup = getNextGroup(combination, nextGroup, set, p, n);
            } while (nextGroup != null && checkConnections(combination, nextGroup, p, n) == false);
            return nextGroup;
        }

        private List<long> getNextGroup(List<List<long>> combination, SortedSet<long> set, long p, long n)
        {
            Debug.Assert(combination.Count != 0);
            Debug.Assert(combination[combination.Count - 1].Count == p);
            long prevPivot = combination[combination.Count - 1][0];
            List<long> next = new List<long>();
            long prevVertex = -1;
            foreach (long vertex in set)
            {
                if (vertex - prevVertex > 0)
                {
                    long v = prevVertex;
                    while (next.Count < p && vertex - v > 1)
                    {
                        if (++v >= prevPivot)
                        {
                            next.Add(v);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    if (next.Count == p)
                    {
                        return next;
                    }
                }
                prevVertex = vertex;
            }
            if (set.Count > 0)
            {
                prevVertex = set.Max;
            }
            while (next.Count < p)
            {
                next.Add(++prevVertex);
            }
            return next;
        }

        private List<long> getNextGroup(List<List<long>> combination, List<long> oldGroup, SortedSet<long> set, long p, long n)
        {
            //Debug.Assert(combination.Count != 0);
            Debug.Assert(oldGroup.Count == p);
            long vertex = -1;
            while (vertex == -1 && 1 < oldGroup.Count)
            {
                vertex = oldGroup[oldGroup.Count - 1];
                oldGroup.RemoveAt(oldGroup.Count - 1);
                vertex = getNextValidVertex(set, oldGroup, vertex, p, n);
            }
            if (vertex != -1)
            {
                oldGroup.Add(vertex);
            }
            while (1 < oldGroup.Count && oldGroup.Count < p)
            {
                vertex = getNextValidVertex(set, oldGroup, p, n);
                if (vertex != -1)
                {
                    oldGroup.Add(vertex);
                }
                else
                {
                    while (vertex == -1 && 1 < oldGroup.Count)
                    {
                        vertex = oldGroup[oldGroup.Count - 1];
                        oldGroup.RemoveAt(oldGroup.Count - 1);
                        vertex = getNextValidVertex(set, oldGroup, vertex, p, n);
                    }
                    if (vertex != -1)
                    {
                        oldGroup.Add(vertex);
                    }
                }
            }
            if (oldGroup.Count == p)
            {
                return oldGroup;
            }
            Debug.Assert(oldGroup.Count == 1);
            return null;
        }

        private long getNextValidVertex(SortedSet<long> set, List<long> group, long p, long n)
        {
            Debug.Assert(group.Count != 0);
            long vertex = group[group.Count - 1] + 1;
            while (set.Contains(vertex) && vertex < n)
            {
                ++vertex;
            }
            if (vertex < n - p + group.Count + 1)
            {
                return vertex;
            }
            return -1;
        }

        private long getNextValidVertex(SortedSet<long> set, List<long> group, long oldVertex, long p, long n)
        {
            //Debug.Assert(group.Count != 0);
            long vertex = oldVertex + 1;
            while (set.Contains(vertex) && vertex < n)
            {
                ++vertex;
            }
            if (vertex < n - p + group.Count + 1)
            {
                return vertex;
            }
            return -1;
        }

        private bool checkConnections(List<List<long>> combination, List<long> next, long p, long n)
        {
            return true;
        }

        private bool riseUp(List<List<long>> combination)
        {
            printCombination(combination);
            return false;
        }

        public void printCombination(List<List<long>> comb)
        {
            if (_fileWriter == null)
            {
                _fileWriter = new StreamWriter("C:/Isomorphism/combinations.txt");
            }
            if (comb != null)
            {
                foreach (List<long> group in comb)
                {
                    _fileWriter.Write("{ " + string.Join(", ", group) + " } ");
                }
                _fileWriter.WriteLine();
                _fileWriter.Flush();
            }
        }
    }

    private class Neighbourship
    {
        private int m_size; // number of vertices
        private SortedDictionary<int, List<int>> m_neighbourship; // list of neighbours     

        public Neighbourship(ArrayList matrix)
        {
            m_size = matrix.Count;
            m_neighbourship = new SortedDictionary<int, List<int>>();
            ArrayList neighbourshipOfIVertex = new ArrayList();
            for (int i = 0; i < matrix.Count; i++)
            {
                neighbourshipOfIVertex = (ArrayList)matrix[i];
                setDataToDictionary(i, neighbourshipOfIVertex);
            }
        }

        public int Size
        {
            get { return m_size; }
        }

        public SortedDictionary<int, List<int>> Neighbourship
        {
            get { return m_neighbourship; }
        }

        public bool areConnected(int vertex1, int vertex2)
        {
            return m_neighbourship[vertex1].Contains(vertex2);
        }

        public static ArrayList get_data(string filename)
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

        private void setDataToDictionary(int index, ArrayList neighbourshipOfIVertex)
        {
            m_neighbourship[index] = new List<int>();
            for (int j = 0; j < m_size; j++)
                if ((bool)neighbourshipOfIVertex[j] == true && index != j)
                    m_neighbourship[index].Add(j);
        }
    }
    
    /*private bool check(Group newGroup, Groups groups, long prime, bool isLowestLevel)
    {
        Debug.Assert(newGroup.Count == prime);
        if (groups != null)
        {
            foreach (Group group in groups)
            {
                Debug.Assert(group.Count == prime);
                bool someAreConnected = false;
                bool someAreNotConnected = false;
                foreach (int vertex in group)
                {
                    foreach (int newVertex in newGroup)
                    {
                        if (newVertex == vertex)
                        {
                            return false;
                        }
                        bool connected = (isLowestLevel
                                ? _container.Neighbourship[vertex].Contains(newVertex)
                                : _generator.areConnected(vertex, newVertex));
                        if (connected)
                        {
                            someAreConnected = true;
                        }
                        else
                        {
                            someAreNotConnected = true;
                        }

                        if (someAreConnected && someAreNotConnected)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }*/
}