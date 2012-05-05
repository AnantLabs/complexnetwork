using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using log4net;
using System.Threading;

namespace ModelCheck
{
        /// <author>Hovhannes Antonyan</author>
        /// <summary>
        /// Provides an API to check whether given graph is hierarhical or not
        /// (Whether some hierarchical tree exists or not which is isomorphous to
        /// the given graph)
        /// </summary>
        public class HierarchicExactChecker : IThreadEvent
        {
            private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(HierarchicExactChecker));

            private Container _container; // container which holds the graph to check for being hierarchic.
            private Tree _tree;
            private int _working_threads;
            private ManualResetEvent _stopWorkItems;

            /// <summary>
            /// Default and the only constructor
            /// </summary>
            public HierarchicExactChecker()
            {
                _stopWorkItems = new ManualResetEvent(false);
            }

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

            /// <summary>
            /// Checks whether the graph specified by the given matrix
            /// is hierarchical or not
            /// </summary>
            /// <param name="matrix">Matrix which specifies the given graph</param>
            /// <returns>True if the graph is hierarchical, otherwise false</returns>
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

            /// <summary>
            /// Checks whether the graph specified by the given matrix
            /// is hierarchical or not
            /// </summary>
            /// <param name="matrix">Matrix which specifies the given graph</param>
            /// <param name="tree">Tree object to hold the corresponding hierarchial tree
            /// if the graph is hierarchical</param>
            /// <returns>True if the graph is hierarchical, otherwise false</returns>
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

            // Gets all possible numbers(and their degrees) if some of their 
            // degrees is equal to n.
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

            // Constructs the hierarchical tree of the given graph if it is hierarchcal,
            // otherwise returns false
            private Tree generateTree(int p, int n)
            {
                _tree = null;
                int count = n - p + 1;
                _stopWorkItems.Reset();
                _working_threads = 0;
                for (int i = 1; i <= count && _stopWorkItems.WaitOne(0) == false; ++i)
                {
                    WorkItem workItem = new WorkItem(this, _stopWorkItems, p, n, i, _container);
                    ++_working_threads;
                    ThreadPool.QueueUserWorkItem(workItem.ThreadPoolCallback);
                }
                lock (this)
                {
                    while (_working_threads > 0)
                    {
                        Monitor.Wait(this);
                    }
                }
                return _tree;
            }

            public void threadFinished(Tree tree)
            {
                lock (this)
                {
                    --_working_threads;
                    if (tree != null)
                    {
                        _tree = tree;
                        _stopWorkItems.Set();
                    }
                    Monitor.Pulse(this);
                }
            }
        }

        interface IThreadEvent
        {
            void threadFinished(Tree tree);
        }

        class WorkItem
        {
            private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(WorkItem));

            private readonly IThreadEvent _handler;
            private readonly ManualResetEvent _finishEvent;
            private readonly int _p;
            private int _n;
            private readonly int _id;
            private readonly Container _container;

            public WorkItem(IThreadEvent handler, ManualResetEvent finishEvent, int p, int n, int id, Container container)
            {
                _handler = handler;
                _finishEvent = finishEvent;
                _p = p;
                _n = n;
                _id = id;
                _container = container;
            }

            // Callback which is being launched by the thread pool
            public void ThreadPoolCallback(object threadContext)
            {
                Tree tree = generateTree();
                _handler.threadFinished(tree);
            }

            // Constructs the hierarchical tree of the given graph if it is hierarchcal,
            // otherwise returns false
            private Tree generateTree()
            {
                Tree tree = new Tree();
                return generateTree(tree);
            }

            // Constructs the next levels of the tree, and attaches into the given 'tree' object
            private Tree generateTree(Tree tree)
            {
                List<Group> combination = new List<Group>();
                try
                {
                    Group group = getFirstGroup(tree, combination);
                    if (group == null)
                    {
                        return null;
                    }
                    combination.Add(group);
                    if (_p < _n)
                    {
                        do
                        {
                            combination = getNextCombination(tree, combination);
                        } while (combination != null && riseUp(tree, combination) == false);
                        if (combination == null)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        riseUp(tree, combination);
                    }
                }
                catch (System.Exception e)
                {
                    logger.Error("WorkItem " + _id + " Failed to generate hierarchic tree. The reason was: " + e.Message);
                    logger.Info("WorkItem " + _id + ":", e);
                    return null;
                }
                return tree;
            }

            // Gets the first possible valid group (i.e. vertices of which are satistfying the statement to 
            // form a group) if there is such group, otherwise returns null
            private Group getFirstGroup(Tree tree, List<Group> combination)
            {
                Group group = new Group();
                Debug.Assert(_p > 0);
                group.SubGroups.Add(0);
                int start = 2;
                if (tree.Levels.Count == 0)
                {
                    group.SubGroups.Add(_id);
                    start = _id + 1;
                }
                else
                {
                    group.SubGroups.Add(1);
                }
                for (int i = start; group.SubGroups.Count < _p; ++i)
                {
                    group.SubGroups.Add(i);
                }
                if (checkConnections(tree, group) == false)
                {
                    group = getNextValidGroup(tree, combination, group);
                }
                return group;
            }

            // Gets the next possible combination of groups, where each group is valid
            private List<Group> getNextCombination(Tree tree, List<Group> originalCombination)
            {
                Debug.Assert(originalCombination.Count != 0);
                List<Group> combination = new List<Group>(originalCombination);
                if (combination.Count == _n / _p)
                {
                    Group group = null;
                    while (group == null && 0 < combination.Count)
                    {
                        if (_finishEvent.WaitOne(0) == true || combination.Count == 1)
                        {
                            //terminating work item
                            return null;
                        }
                        group = combination[combination.Count - 1];
                        combination.RemoveAt(combination.Count - 1);
                        group = getNextValidGroup(tree, combination, group);
                    }
                    if (group != null)
                    {
                        combination.Add(group);
                    }
                }
                while (0 < combination.Count && combination.Count < _n / _p)
                {
                    Group group = null;
                    group = getNextValidGroup(tree, combination);
                    if (group != null)
                    {
                        combination.Add(group);
                    }
                    else
                    {
                        while (group == null && 0 < combination.Count)
                        {
                            if (_finishEvent.WaitOne(0) == true || combination.Count == 1)
                            {
                                //terminating work item
                                return null;
                            }
                            group = combination[combination.Count - 1];
                            combination.RemoveAt(combination.Count - 1);
                            group = getNextValidGroup(tree, combination, group);
                        }
                        if (group != null)
                        {
                            combination.Add(group);
                        }
                    }
                }
                if (combination.Count == _n / _p)
                {
                    return combination;
                }
                Debug.Assert(combination.Count == 0);
                return null;
            }

            // Gets the next possible valid group which does not contain vertices of the
            // given combination's groups vertices.
            private Group getNextValidGroup(Tree tree, List<Group> combination)
            {
                SortedSet<int> set = new SortedSet<int>();
                foreach (Group group in combination)
                {
                    foreach (int vertex in group.SubGroups)
                    {
                        set.Add(vertex);
                    }
                }
                Group nextGroup = getNextGroup(combination, set);
                if (nextGroup != null && checkConnections(tree, nextGroup) == false)
                {
                    do
                    {
                        nextGroup = getNextGroup(combination, nextGroup, set);
                    } while (nextGroup != null && checkConnections(tree, nextGroup) == false);
                }
                return nextGroup;
            }

            // Gets the next possible valid group which succeeds the given 'group' object and 
            // does not contain vertices of the given combination's groups vertices.
            private Group getNextValidGroup(Tree tree, List<Group> combination, Group group)
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
                    nextGroup = getNextGroup(combination, nextGroup, set);
                } while (nextGroup != null && checkConnections(tree, nextGroup) == false);
                return nextGroup;
            }

            // Gets the next possible group of vertices which is probably not valid, it has to be checked
            // and which does not contain vertices of the given combination's groups vertices.
            private Group getNextGroup(List<Group> combination, SortedSet<int> set)
            {
                Debug.Assert(combination.Count != 0);
                Debug.Assert(combination[combination.Count - 1].SubGroups.Count == _p);
                int prevPivot = combination[combination.Count - 1].SubGroups[0];
                Group next = new Group();
                int prevVertex = -1;
                foreach (int vertex in set)
                {
                    if (vertex - prevVertex > 0)
                    {
                        int v = prevVertex;
                        while (next.SubGroups.Count < _p && vertex - v > 1)
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
                        if (next.SubGroups.Count == _p)
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
                while (next.SubGroups.Count < _p && prevVertex < _n - 1)
                {
                    next.SubGroups.Add(++prevVertex);
                }
                Debug.Assert(next.SubGroups.Count == _p);
                return next;
            }

            // Gets the next possible group of vertices which succeeds the given old group,
            // probably is not valid, it has to be checked and which does not contain 
            // vertices of the given combination's groups vertices.
            private Group getNextGroup(List<Group> combination, Group oldGroup, SortedSet<int> set)
            {
                Debug.Assert(oldGroup.SubGroups.Count == _p);
                int vertex = -1;
                while (vertex == -1 && 1 < oldGroup.SubGroups.Count)
                {
                    vertex = oldGroup.SubGroups[oldGroup.SubGroups.Count - 1];
                    oldGroup.SubGroups.RemoveAt(oldGroup.SubGroups.Count - 1);
                    vertex = getNextValidVertex(set, oldGroup, vertex);
                }
                if (vertex != -1)
                {
                    oldGroup.SubGroups.Add(vertex);
                }
                while (1 < oldGroup.SubGroups.Count && oldGroup.SubGroups.Count < _p)
                {
                    vertex = getNextValidVertex(set, oldGroup);
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
                            vertex = getNextValidVertex(set, oldGroup, vertex);
                        }
                        if (vertex != -1)
                        {
                            oldGroup.SubGroups.Add(vertex);
                        }
                    }
                }
                if (oldGroup.SubGroups.Count == _p)
                {
                    return oldGroup;
                }
                Debug.Assert(oldGroup.SubGroups.Count == 1);
                return null;
            }

            private int getNextValidVertex(SortedSet<int> set, Group group)
            {
                Debug.Assert(group.SubGroups.Count != 0);
                int vertex = group.SubGroups[group.SubGroups.Count - 1] + 1;
                while (set.Contains(vertex) && vertex < _n)
                {
                    ++vertex;
                }
                if (vertex < _n - _p + group.SubGroups.Count + 1)
                {
                    return vertex;
                }
                return -1;
            }

            private int getNextValidVertex(SortedSet<int> set, Group group, int oldVertex)
            {
                Debug.Assert(group.SubGroups.Count != 0);
                int vertex = oldVertex + 1;
                while (set.Contains(vertex) && vertex < _n)
                {
                    ++vertex;
                }
                if (vertex < _n - _p + group.SubGroups.Count + 1)
                {
                    return vertex;
                }
                return -1;
            }

            // Checks whether the vertices of the group simulateously have no connections with vertices
            // out of the group, or are connected to the same vertices out of the group
            private bool checkConnections(Tree tree, Group group)
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

            // Adds the current combination of groups to the tree and rises up to the next level
            // Returns true if it was able to rise to the highest level, which means the graph
            // is hierarchical, or otherwise returns false
            private bool riseUp(Tree tree, List<Group> combination)
            {
                tree.Levels.Add(combination);
                if (_n > _p)
                {
                    _n /= _p;
                    if (generateTree(tree) == null)
                    {
                        tree.Levels.RemoveAt(tree.Levels.Count - 1);
                        return false;
                    }
                }
                return true;
            }

            //private TextWriter _fileWriter = null;
            /*private void printCombination(List<Group> comb)
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
            }*/
        }

        // Inner class which holds the graph for check for being hierarchical
        class Container
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
        }
}