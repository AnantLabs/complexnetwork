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

        private Container _container;
        private TextWriter _fileWriter = null;

        public static void main()
        {
            ArrayList matrix = Container.get_data("C:/ComplexNetwork/graph.txt");
            HierarchicExactChecker checker = new HierarchicExactChecker();
            Tree tree = null;
            bool isHierarchic = checker.IsHierarchic(matrix, ref tree);
            if (isHierarchic)
            {
                Debug.WriteLine("Is Hierarchic");
            }
            else
            {
                Debug.WriteLine("Is Not Hierarchic");
            }
        }

        public bool IsHierarchic(ArrayList matrix)
        {
            _container = new Container(matrix);
            IDictionary<int, int> degrees = getAllDegrees(_container.Size);
            foreach (int prime in degrees.Keys)
            {
                if (isHierarchic(prime))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsHierarchic(ArrayList matrix, ref Tree tree)
        {
            _container = new Container(matrix);
            IDictionary<int, int> degrees = getAllDegrees(_container.Size);
            foreach (int prime in degrees.Keys)
            {
                if (isHierarchic(prime, ref tree))
                {
                    Debug.Assert(tree.Levels.Count == degrees[prime]);
                    return true;
                }
            }
            return false;
        }

        private static IDictionary<int, int> getAllDegrees(int n)
        {
            Dictionary<int, int> collection = new Dictionary<int, int>();
            int degree = 0;
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
            Tree tree = generateTree(prime, _container.Size);
            return tree != null;
        }

        private bool isHierarchic(int prime, ref Tree tree)
        {
            tree = generateTree(prime, _container.Size);
            return tree != null;
        }

        private Tree generateTree(int p, int n)
        {
            Tree tree = new Tree();
            return generateTree(tree, p, n);
        }

        private Tree generateTree(Tree tree, int p, int n)
        {
            List<Group> combination = new List<Group>();
            try
            {
                Group group = getFirstGroup(tree, combination, p, n);
                if (group == null)
                {
                    return null;
                }
                combination.Add(group);
                if (p < n)
                {
                    do
                    {
                        combination = getNextCombination(tree, combination, p, n);
                    } while (combination != null && riseUp(tree, combination, p, n) == false);
                    if (combination == null)
                    {
                        return null;
                    }
                }
                else
                {
                    riseUp(tree, combination, p, n);
                }
            }
            catch (System.Exception e)
            {
                logger.Error("Failed to generate tree. The reason was: " + e.Message);
                logger.Info(null, e);
                return null;
            }
            return tree;
        }

        private Group getFirstGroup(Tree tree, List<Group> combination, int p, int n)
        {
            Group group = new Group();
            for (int i = 0; i < p; ++i)
            {
                group.SubGroups.Add(i);
            }
            if (checkConnections(tree, group, p, n) == false)
            {
                group = getNextValidGroup(tree, combination, group, p, n);
            }
            return group;
        }

        private List<Group> getNextCombination(Tree tree, List<Group> originalCombination, int p, int n)
        {
            Debug.Assert(originalCombination.Count != 0);
            List<Group> combination = new List<Group>(originalCombination);
            if (combination.Count == n / p)
            {
                Group group = null;
                while (group == null && 0 < combination.Count)
                {
                    group = combination[combination.Count - 1];
                    combination.RemoveAt(combination.Count - 1);
                    group = getNextValidGroup(tree, combination, group, p, n);
                }
                if (group != null)
                {
                    combination.Add(group);
                }
            }
            while (0 < combination.Count && combination.Count < n / p)
            {
                Group group = null;
                group = getNextValidGroup(tree, combination, p, n);
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
                        group = getNextValidGroup(tree, combination, group, p, n);
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

        private Group getNextValidGroup(Tree tree, List<Group> combination, int p, int n)
        {
            SortedSet<int> set = new SortedSet<int>();
            foreach (Group group in combination)
            {
                foreach (int vertex in group.SubGroups)
                {
                    set.Add(vertex);
                }
            }
            Group nextGroup = getNextGroup(combination, set, p, n);
            if (nextGroup != null && checkConnections(tree, nextGroup, p, n) == false)
            {
                do
                {
                    nextGroup = getNextGroup(combination, nextGroup, set, p, n);
                } while (nextGroup != null && checkConnections(tree, nextGroup, p, n) == false);
            }
            return nextGroup;
        }

        private Group getNextValidGroup(Tree tree, List<Group> combination, Group group, int p, int n)
        {
            SortedSet<int> set = new SortedSet<int>();
            foreach (Group g in combination)
            {
                foreach (int vertex in g.SubGroups)
                {
                    set.Add(vertex);
                }
            }
            Group nextGroup = group;
            do
            {
                nextGroup = getNextGroup(combination, nextGroup, set, p, n);
            } while (nextGroup != null && checkConnections(tree, nextGroup, p, n) == false);
            return nextGroup;
        }

        private Group getNextGroup(List<Group> combination, SortedSet<int> set, int p, int n)
        {
            Debug.Assert(combination.Count != 0);
            Debug.Assert(combination[combination.Count - 1].SubGroups.Count == p);
            int prevPivot = combination[combination.Count - 1].SubGroups[0];
            Group next = new Group();
            int prevVertex = -1;
            foreach (int vertex in set)
            {
                if (vertex - prevVertex > 0)
                {
                    int v = prevVertex;
                    while (next.SubGroups.Count < p && vertex - v > 1)
                    {
                        if (++v >= prevPivot)
                        {
                            next.SubGroups.Add(v);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    if (next.SubGroups.Count == p)
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
            while (next.SubGroups.Count < p && prevVertex < n - 1)
            {
                next.SubGroups.Add(++prevVertex);
            }
            Debug.Assert(next.SubGroups.Count == p);
            return next;
        }

        private Group getNextGroup(List<Group> combination, Group oldGroup, SortedSet<int> set, int p, int n)
        {
            Debug.Assert(oldGroup.SubGroups.Count == p);
            int vertex = -1;
            while (vertex == -1 && 1 < oldGroup.SubGroups.Count)
            {
                vertex = oldGroup.SubGroups[oldGroup.SubGroups.Count - 1];
                oldGroup.SubGroups.RemoveAt(oldGroup.SubGroups.Count - 1);
                vertex = getNextValidVertex(set, oldGroup, vertex, p, n);
            }
            if (vertex != -1)
            {
                oldGroup.SubGroups.Add(vertex);
            }
            while (1 < oldGroup.SubGroups.Count && oldGroup.SubGroups.Count < p)
            {
                vertex = getNextValidVertex(set, oldGroup, p, n);
                if (vertex != -1)
                {
                    oldGroup.SubGroups.Add(vertex);
                }
                else
                {
                    while (vertex == -1 && 1 < oldGroup.SubGroups.Count)
                    {
                        vertex = oldGroup.SubGroups[oldGroup.SubGroups.Count - 1];
                        oldGroup.SubGroups.RemoveAt(oldGroup.SubGroups.Count - 1);
                        vertex = getNextValidVertex(set, oldGroup, vertex, p, n);
                    }
                    if (vertex != -1)
                    {
                        oldGroup.SubGroups.Add(vertex);
                    }
                }
            }
            if (oldGroup.SubGroups.Count == p)
            {
                return oldGroup;
            }
            Debug.Assert(oldGroup.SubGroups.Count == 1);
            return null;
        }

        private int getNextValidVertex(SortedSet<int> set, Group group, int p, int n)
        {
            Debug.Assert(group.SubGroups.Count != 0);
            int vertex = group.SubGroups[group.SubGroups.Count - 1] + 1;
            while (set.Contains(vertex) && vertex < n)
            {
                ++vertex;
            }
            if (vertex < n - p + group.SubGroups.Count + 1)
            {
                return vertex;
            }
            return -1;
        }

        private int getNextValidVertex(SortedSet<int> set, Group group, int oldVertex, int p, int n)
        {
            Debug.Assert(group.SubGroups.Count != 0);
            int vertex = oldVertex + 1;
            while (set.Contains(vertex) && vertex < n)
            {
                ++vertex;
            }
            if (vertex < n - p + group.SubGroups.Count + 1)
            {
                return vertex;
            }
            return -1;
        }

        private bool checkConnections(Tree tree, Group group, int p, int n)
        {
            HashSet<int> neighbours = new HashSet<int>();
            List<HashSet<int>> neighboursList = new List<HashSet<int>>();
            if (tree.Levels.Count == 0)
            {
                group.Vertices = new HashSet<int>(group.SubGroups);
            }
            else 
            {
                group.Vertices = new HashSet<int>();
                foreach (int vertex in group.SubGroups)
                {
                    Debug.Assert(tree.Levels[tree.Levels.Count - 1].Count > vertex);
                    ISet<int> vertices = tree.Levels[tree.Levels.Count - 1][vertex].Vertices;
                    foreach (int v in vertices)
                    {
                        group.Vertices.Add(v);
                    }
                }
            }
            foreach (int vertex in group.SubGroups)
            {
                HashSet<int> vertexNeighbours = new HashSet<int>();
                if (tree.Levels.Count == 0)
                {
                    Debug.Assert(_container.Neighbourship.ContainsKey(vertex));
                    foreach (int v in _container.Neighbourship[vertex])
                    {
                        if (!group.Vertices.Contains(v))
                        {
                            neighbours.Add(v);
                            vertexNeighbours.Add(v);
                        }
                    }
                }
                else
                {
                    foreach (int v in tree.Levels[tree.Levels.Count - 1][vertex].NeighbourVertices)
                    {
                        if (!group.Vertices.Contains(v))
                        {
                            neighbours.Add(v);
                            vertexNeighbours.Add(v);
                        }
                    }
                }
                neighboursList.Add(vertexNeighbours);
            }
            foreach (HashSet<int> vertexNeighbours in neighboursList) 
            {
                HashSet<int> set = new HashSet<int>(neighbours);
                set.ExceptWith(vertexNeighbours);
                if (set.Count > 0)
                {
                    return false;
                }
            }
            group.NeighbourVertices = neighbours;
            return true;
        }

        private bool riseUp(Tree tree, List<Group> combination, int p, int n)
        {
            tree.Levels.Add(combination);
            if (n > p)
            {
                if (generateTree(tree, p, n / p) == null)
                {
                    tree.Levels.RemoveAt(tree.Levels.Count - 1);
                    return false;
                }
            }
            return true;
        }

        public void printCombination(List<Group> comb)
        {
            if (_fileWriter == null)
            {
                _fileWriter = new StreamWriter("C:/Isomorphism/combinations.txt");
            }
            if (comb != null)
            {
                foreach (Group group in comb)
                {
                    _fileWriter.Write("{ " + string.Join(", ", group.SubGroups) + " } ");
                }
                _fileWriter.WriteLine();
                _fileWriter.Flush();
            }
        }

        private class Container
        {
            private int _size; // number of vertices
            private SortedDictionary<int, List<int>> _neighbourship; // list of neighbours     

            public Container(ArrayList matrix)
            {
                _size = matrix.Count;
                _neighbourship = new SortedDictionary<int, List<int>>();
                ArrayList neighbourshipOfIVertex = new ArrayList();
                for (int i = 0; i < matrix.Count; i++)
                {
                    neighbourshipOfIVertex = (ArrayList)matrix[i];
                    setDataToDictionary(i, neighbourshipOfIVertex);
                }
            }

            public int Size
            {
                get { return _size; }
            }

            public SortedDictionary<int, List<int>> Neighbourship
            {
                get { return _neighbourship; }
            }

            public bool areConnected(int vertex1, int vertex2)
            {
                return _neighbourship[vertex1].Contains(vertex2);
            }

            public static ArrayList get_data(string filename)
            {
                ArrayList matrix = new ArrayList();
                using (StreamReader streamreader = new StreamReader(filename))
                {
                    string contents;
                    while ((contents = streamreader.ReadLine()) != null)
                    {
                        string[] split = System.Text.RegularExpressions.Regex.Split(contents,
                                "\\s+", System.Text.RegularExpressions.RegexOptions.None);
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
                _neighbourship[index] = new List<int>();
                for (int j = 0; j < _size; j++)
                {
                    if ((bool)neighbourshipOfIVertex[j] == true && index != j)
                    {
                        _neighbourship[index].Add(j);
                    }
                }
            }
        }
    }

    public class Tree
    {
        private readonly List<List<Group>> _levels; // contains Groups of each level starting from lowest to highest levels.

        public Tree()
        {
            _levels = new List<List<Group>>();
        }

        public List<List<Group>> Levels
        {
            get
            {
                return _levels;
            }
        }
    }

    public class Group
    {
        private List<int> _subgroups; // a group of the previous level
        private ISet<int> _vertices; // all vertices which belong to the subtree of this group
        private ISet<int> _neighbourVertices; // neighbour vertices of the group

        public Group()
        {
            _subgroups = new List<int>();
            _neighbourVertices = new HashSet<int>();
        }

        public Group(IEnumerable<int> collection)
        {
            _subgroups = new List<int>(collection);
            _neighbourVertices = new HashSet<int>();
        }

        public Group(Group group)
        {
            _subgroups = new List<int>();
            _neighbourVertices = new HashSet<int>(group.NeighbourVertices);
        }

        public List<int> SubGroups
        {
            get
            {
                return _subgroups;
            }
            set
            {
                _subgroups = value;
            }
        }

        public ISet<int> Vertices
        {
            get
            {
                return _vertices;
            }
            set
            {
                _vertices = value;
            }
        }

        public ISet<int> NeighbourVertices
        {
            get
            {
                return _neighbourVertices;
            }
            set
            {
                _neighbourVertices = value;
            }
        }

        public bool Equals(object obj)
        {
            if (obj != null && obj is Group)
            {
                if (ReferenceEquals(this, obj)) 
                {
                    return true;
                }
                Group group = (Group)obj;
                if (this._subgroups.Count != group._subgroups.Count)
                {
                    return false;
                }
                for (int i = 0; i < this._subgroups.Count; ++i)
                {
                    if (this._subgroups[i] != group._subgroups[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}