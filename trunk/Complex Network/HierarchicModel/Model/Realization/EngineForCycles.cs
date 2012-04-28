﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using log4net;

namespace Model.HierarchicModel.Realization
{

/// <author>Hovhannes Antonyan</author>
/// <summary>
/// Provides an API to calculate and get the number of
/// cycles with the specified length in the hierarchical tree.
/// </summary>
class EngineForCycles
{
    private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(EngineForCycles));

    // --------------------------------------------------------------
    // Instance variables

    private bool _traceToFile;
    private string _fileName = null;
    private TextWriter _fileWriter = null;
    private SortedSet<MyList> _cycles = new SortedSet<MyList>();
    private IDictionary<int/*pathLength*/, IDictionary<int/*level*/, ISet<MyList>/*paths*/>> _paths = 
            new Dictionary<int, IDictionary<int, ISet<MyList>>>();

    // --------------------------------------------------------------
    // Constructors

    public EngineForCycles()
    {
        _traceToFile = false;
    }

    public EngineForCycles(string filename)
    {
        _traceToFile = true;
        this._fileName = filename;
    }

    // --------------------------------------------------------------
    // Public API

    /// <summary>
    /// Calculates the number of cycles with the specified length in the specified
    /// hierarchical tree
    /// </summary>
    /// <param name="tree">Tree in which cycles count should be found</param>
    /// <param name="length">Length of the cycles to search for (should be greater than 1).
    /// If it is equal to 2, number of edges is being returned</param>
    /// <returns>Return the count of cycles with the specified length.
    /// If the length is equal to 2, number of edges is being returned</returns>
    public long GetCycleCount(HierarchicGraph tree, long length)
    {
        if (length <= 1)
        {
            return 0;
        }
        long cycleCount = 0;
        try
        {
            if (length == 2)
            {
                double count = tree.countEdgesAllGraph();
                Debug.Assert(count == System.Math.Ceiling(count));
                cycleCount = (long)count;
            }
            else
            {
                getCycles(tree, length);
                Debug.Assert(_cycles.Count % 2 == 0);
                cycleCount = _cycles.Count / 2;
                _cycles.Clear();
            }
        }
        catch (System.Exception e)
        {
            logger.Error("Failed to get cycle count. The reason was: " + e.Message);
            logger.Info(null, e);
            cycleCount = -1;
        }
        return cycleCount;
    }

    // --------------------------------------------------------------
    // Inner functions

    private void getCycles(HierarchicGraph tree, long cycleLength)
    {
        try
        {
            if (_traceToFile && cycleLength > 2)
            {
                _fileWriter = new StreamWriter(_fileName);
                _fileWriter.WriteLine("Start time: " + System.DateTime.Now);
            }
            long verticesCount = (long)System.Math.Pow(tree.prime, tree.degree);
            MyList branch = new MyList();
            for (long origin = 0; origin < verticesCount; ++origin)
            {
                branch.Add(origin);
                getCyclesStartingWithOrigin(tree, origin, tree.degree, branch, cycleLength);
                _paths.Clear();
                branch.RemoveAt(branch.Count - 1);
                Debug.Assert(branch.Count == 0);
            }
            if (_traceToFile && cycleLength > 2)
            {
                _fileWriter.WriteLine("End time: " + System.DateTime.Now);
                _fileWriter.WriteLine("Calculation succeeded");
            }
        }
        catch (System.Exception e)
        {
            if (_traceToFile && cycleLength > 2)
            {
                _fileWriter.WriteLine("End time: " + System.DateTime.Now);
                _fileWriter.WriteLine("Calculation failed: an exception was thrown");
            }
            throw e;
        }
        finally
        {
            if (_traceToFile && cycleLength > 2)
            {
                _fileWriter.WriteLine("Vertices Count: " + (long)System.Math.Pow(tree.prime, tree.degree));
                _fileWriter.WriteLine("Cycle Count: " + (_cycles.Count / 2));
                _fileWriter.WriteLine("Found cycles(each cycle is mentioned twice):");
                printSet();
                _fileWriter.Close();
            }
        }
    }

    // Gets all cycles which start with origin vertex (and have length equal to the cycleLength)
    private void getCyclesStartingWithOrigin(HierarchicGraph tree, long pivot, int level, MyList branch, long cycleLength)
    {
        long treeId = branch[branch.Count - 1];
        long parentId = treeId / tree.prime;
        int start = (int)(treeId % tree.prime);
        int begining = start;
        if (level == tree.degree)
        {
            ++begining;
        }
        for (int end = begining; end < tree.prime; ++end)
        {
            if (start == end || tree.areAdjacent(level - 1, parentId, start, end) == 1)
            {
                getOriginsPathsStartingEndingWith(tree, pivot, level, branch, parentId, start, end, cycleLength - 1);
            }
        }
        if (level > 1)
        {
            branch.Add(parentId);
            getCyclesStartingWithOrigin(tree, pivot, level - 1, branch, cycleLength);
            branch.RemoveAt(branch.Count - 1);
        }
    }

    // Gets all paths which start with origin vertex, in the "start" subtree and end in the "end" subtree
    private void getOriginsPathsStartingEndingWith(HierarchicGraph tree, long pivot, int level, MyList branch,
            long parentId, int start, int end, long pathLength)
    {
        long treeId = branch[branch.Count - 1];
        for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
        {
            ISet<MyList> paths = getInnerPaths(tree, pivot, level, branch, new MyList(), innerLength, pivot, start);
            if (paths.Count == 0)
			{
				return;
			}
			foreach (MyList path in paths)
            {
                Debug.Assert(path.Count != 0);
                for (int next = start + 1; next < tree.prime; ++next)
                {
                    if (tree.areAdjacent(level - 1, parentId, start, next) == 1)
                    {
                        KeyValuePair<long, long> range = getVerticesRange(tree, treeId + next - start, level);
                        if (pathLength - innerLength - 1 > 0)
                        {
                            for (long vertex = range.Key; vertex < range.Value; ++vertex)
                            {
                                int originStart = -1;
                                if (start == end)
                                {
                                    originStart = (int)(branch[branch.Count - 2] % tree.prime);
                                }
                                ISet<MyList> continuations = getPathContinuations(tree, vertex, level,
                                        treeId + next - start, parentId, start, next, end,
                                        path, pathLength - innerLength - 1, pivot, originStart);
                                addToSet(path, continuations);
                            }
                        }
                        else
                        {
                            addToSet(path, range);
                        }
                    }
                }
            }
        }
    }


    // Gets all possible continuations for the "pathStart" passing through "pivot" vertex and ending in the "end" subtree,
    // and have the length equal to pathLength
    private ISet<MyList> getPathContinuations(HierarchicGraph tree, long pivot, int level, long treeId,
            long parentId, int start, int current, int end, MyList pathStart, long pathLength, long origin, int originStart)
    {
        ISet<MyList> paths = new SortedSet<MyList>();
        MyList branch = new MyList();
        branch.Add(pivot);
        Debug.Assert(pathLength != 0);
        if (pathLength == 1)
        {
            if (end != current)
            {
                if (tree.areAdjacent(level - 1, parentId, current, end) == 1)
                {
                    KeyValuePair<long, long> range = getVerticesRange(tree, treeId + end - current, level);
                    for (long vertex = range.Key; vertex < range.Value; ++vertex)
                    {
                        Debug.Assert(pathStart.Count != 0);
                        if (!pathStart.Contains(vertex) && (vertex > origin) && (vertex != pivot)
                            && (start != end || start == end && areVerticesConnected(tree, vertex, origin)))
                        {
                            Debug.Assert(!pathStart.Contains(pivot));
                            Debug.Assert(vertex >= origin);
                            MyList path = new MyList();
                            path.Add(pivot);
                            path.Add(vertex);
                            paths.Add(path);
                        }
                    }
                }
            }
            else
            {
                ISet<MyList> innerPaths = getPaths(tree, pivot, level, tree.degree, branch,
                        pathStart, pathLength, origin, start == end, originStart);
                mergeSet<MyList>(paths, innerPaths);
            }
            return paths;
        }
        for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
        {
            ISet<MyList> innerPaths = getPaths(tree, pivot, level, tree.degree, branch, pathStart, innerLength, origin, false, 0);
			foreach (MyList path in innerPaths)
            {
                Debug.Assert(path.Count == innerLength + 1);
                int begining = start + 1;
                if (start == end)
                {
                    --begining;
                }
                for (int next = start; next < tree.prime; ++next)
                {
                    if (next != current && tree.areAdjacent(level - 1, parentId, current, next) == 1)
                    {
                        KeyValuePair<long, long> range = getVerticesRange(tree, treeId + next - current, level);
                        for (long vertex = range.Key; vertex < range.Value; ++vertex)
                        {
                            pathStart.AddRange(path);
                            if (vertex > origin && !pathStart.Contains(vertex))
                            {
                                if (pathLength - innerLength > 1)
                                {
                                    ISet<MyList> continuations = getPathContinuations(tree, vertex, level,
                                            treeId + next - current, parentId, start, next, end, pathStart,
                                            pathLength - innerLength - 1, origin, originStart);
                                    foreach (MyList continuation in continuations)
                                    {
                                        MyList fullPath = new MyList(path);
                                        fullPath.AddRange(continuation);
                                        paths.Add(fullPath);
                                    }
                                }
                                else if (next == end && (start != end || start == end && 
                                        areVerticesConnected(tree, vertex, origin)))
                                {
                                    Debug.Assert(pathStart.Count != 0);
                                    MyList fullPath = new MyList(path);
                                    fullPath.Add(vertex);
                                    paths.Add(fullPath);
                                }
                            }
                            pathStart.RemoveRange(pathStart.Count - path.Count, path.Count);
                        }
                    }
                }
            }
        }
        if (current == end)
        {
            ISet<MyList> innerPaths = getPaths(tree, pivot, level, tree.degree, branch, pathStart, pathLength, origin, start == end, originStart);
            mergeSet<MyList>(paths, innerPaths);
        }
        branch.RemoveAt(branch.Count - 1);
        return paths;
    }


    // Gets all paths starting with the "pivot" vertex, having the length equal to pathLength by rising up to the tree
    // to the highestAllowedLayer
    private ISet<MyList> getPaths(HierarchicGraph tree, long pivot, int highestAllowedLayer, int level,
            MyList branch, MyList pathStart, long pathLength, long origin, bool isStartEqualToEnd, int originStart)
    {
        ISet<MyList> paths = new SortedSet<MyList>();
        if (pathLength == 0)
        {
            Debug.Assert(!pathStart.Contains(pivot));
            Debug.Assert(pathStart.Count != 0);
            Debug.Assert(pivot >= origin);
            MyList path = new MyList();
            path.Add(pivot);
            paths.Add(path);
            return paths;
        }
        long treeId = branch[branch.Count - 1];
        long parentId = treeId / tree.prime;
        int start = (int)(treeId % tree.prime);
        if (isStartEqualToEnd && level == highestAllowedLayer + 1)
        {
            for (int end = originStart + 1; end < tree.prime; ++end)
            {
                if (end != start && tree.areAdjacent(highestAllowedLayer, parentId, end, originStart) == 1)
                {
                    ISet<MyList> crossPaths = getCrossTreePaths(tree, pivot, level, branch, parentId, start, end,
                            pathStart, pathLength, origin, originStart);
                    mergeSet<MyList>(paths, crossPaths);
                }
            }
        }
        else
        {
            for (int end = 0; end < tree.prime; ++end)
            {
                if (end != start)
                {
                    ISet<MyList> crossPaths = getCrossTreePaths(tree, pivot, level, branch, parentId, start, end,
                            pathStart, pathLength, origin, originStart);
                    mergeSet<MyList>(paths, crossPaths);
                }
            }
        }
        if (level > highestAllowedLayer + 1)
        {
            branch.Add(parentId);
            ISet<MyList> innerPaths = getPaths(tree, pivot, highestAllowedLayer, level - 1, branch,
                    pathStart, pathLength, origin, isStartEqualToEnd, originStart);
            mergeSet<MyList>(paths, innerPaths);
        }
        return paths;
    }


    // Gets all paths starting with the "pivot" vertex, having the length equal to pathLength by crossing
    // subtrees and ending with "end" subtree
    private ISet<MyList> getCrossTreePaths(HierarchicGraph tree, long pivot, int level, MyList branch,
            long parentId, int start, int end, MyList pathStart, long pathLength, long origin, int originStart)
    {
        ISet<MyList> paths = new SortedSet<MyList>();
        long treeId = branch[branch.Count - 1];
        if (pathLength == 1)
        {
            if (tree.areAdjacent(level - 1, parentId, start, end) == 1)
            {
                KeyValuePair<long, long> range = getVerticesRange(tree, treeId + end - start, level);
                for (long vertex = range.Key; vertex < range.Value; ++vertex)
                {
                    if (!pathStart.Contains(vertex) && (vertex > origin) && (vertex != pivot))
                    {
                        Debug.Assert(!pathStart.Contains(pivot));
                        MyList path = new MyList();
                        path.Add(pivot);
                        path.Add(vertex);
                        paths.Add(path);
                    }
                }
            }
            return paths;
        }
        for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
        {
            ISet<MyList> innerPaths = getInnerPaths(tree, pivot, level, branch, pathStart, innerLength, origin, originStart);
            foreach (MyList path in innerPaths)
            {
                for (int next = 0; next < tree.prime; ++next)
                {
                    if (next != start && tree.areAdjacent(level - 1, parentId, start, next) == 1)
                    {
                        KeyValuePair<long, long> range = getVerticesRange(tree, treeId + next - start, level);
                        for (long vertex = range.Key; vertex < range.Value; ++vertex)
                        {
                            pathStart.AddRange(path);
                            if (vertex > origin && !pathStart.Contains(vertex))
                            {
                                if (pathLength - innerLength > 1)
                                {
                                    ISet<MyList> continuations = getPathContinuations(tree, vertex, level,
                                            treeId + next - start, parentId, start, next, end, pathStart,
                                            pathLength - innerLength - 1, origin, originStart);
                                    foreach (MyList continuation in continuations)
                                    {
                                        MyList fullPath = new MyList(path);
                                        fullPath.AddRange(continuation);
                                        paths.Add(fullPath);
                                    }
                                }
                                else if (next == end && vertex != pivot)
                                {
                                    Debug.Assert(pathStart.Count != 0);
                                    MyList fullPath = new MyList(path);
                                    fullPath.Add(vertex);
                                    paths.Add(fullPath);
                                }
                            }
                            pathStart.RemoveRange(pathStart.Count - path.Count, path.Count);
                        }
                    }
                }
            }
        }
        return paths;
    }


    // Gets all inner paths of the current subtree having the length equal to pathLength
    private ISet<MyList> getInnerPaths(HierarchicGraph tree, long pivot, int level, MyList branch,
            MyList pathStart, long pathLength, long origin, int originStart)
    {
        ISet<MyList> paths = new SortedSet<MyList>();
        if (pathLength == 0)
        {
            Debug.Assert(!pathStart.Contains(pivot));
            Debug.Assert(pivot >= origin);
            paths.Add(new MyList(new long[] {pivot}));
            return paths;
        }
        if (level == tree.degree)
        {
            return paths;
        }
        if (_paths.Count > pathLength && _paths[(int)pathLength].ContainsKey(level) && pivot == origin && pathStart.Count == 0)
        {
            return _paths[(int)pathLength][level];
        }
        long treeId = branch[branch.Count - 1];
        long childId = branch[branch.Count - 2];
        int start = (int)(childId % tree.prime);
        branch.RemoveAt(branch.Count - 1);
        for (int end = start + 1; end < tree.prime; ++end)
        {
            ISet<MyList> crossPaths = getCrossTreePaths(tree, pivot, level + 1, branch, treeId, start, end,
                    pathStart, pathLength, origin, originStart);
            mergeSet<MyList>(paths, crossPaths);
        }
        ISet<MyList> innerPaths = getInnerPaths(tree, pivot, level + 1, branch, pathStart, pathLength, origin, originStart);
        mergeSet<MyList>(paths, innerPaths);
        branch.Add(treeId);
        if (pivot == origin && pathStart.Count == 0)
        {
            IDictionary<int, ISet<MyList>> dict = null;
            if (_paths.Count > pathLength && _paths[(int)pathLength] != null)
            {
                dict = _paths[(int)pathLength];
            }
            else
            {
                dict = new Dictionary<int, ISet<MyList>>();
            }
            if (_paths.ContainsKey((int)pathLength))
            {
                if (_paths[(int)pathLength].ContainsKey(level))
                {
                    ISet<MyList> set = _paths[(int)pathLength][level];
                    foreach (MyList path in paths)
                    {
                        set.Add(path);
                    }
                }
                else
                {
                    _paths[(int)pathLength][level] = paths;
                }
            }
            else
            {
                dict.Add(level, paths);
                _paths.Add((int)pathLength, dict);
            }
        }
        return paths;
    }


    // Gets the "treeId" tree's all vertices range
    private KeyValuePair<long/*start*/, long/*end+1*/> getVerticesRange(HierarchicGraph tree, long treeId, int level)
    {
        long branchCount = (long)System.Math.Pow(tree.prime, tree.degree - level);
        long start = treeId * branchCount;
        long end = start + branchCount;
        return new KeyValuePair<long, long>(start, end);
    }


    // Checks if two vertices (probably in the different subtrees) are connected or not
    private bool areVerticesConnected(HierarchicGraph tree, long vertex1, long vertex2)
    {
        long min = -1;
        long max = -1;
        if (vertex1 < vertex2)
        {
            min = vertex1;
            max = vertex2;
        }
        else
        {
            min = vertex2;
            max = vertex1;
        }
        int level = tree.degree;
        while ((min / tree.prime) != (max / tree.prime))
        {
            min /= tree.prime;
            max /= tree.prime;
            --level;
        }
        return tree.areAdjacent(level - 1, max / tree.prime, (int)min % tree.prime, (int)max % tree.prime) == 1;
    }


    // Merges two sets into the first one
    private ISet<T> mergeSet<T>(ISet<T> set, ISet<T> secondSet)
    {
        foreach (T element in secondSet)
        {
            set.Add(element);
        }
        return set;
    }

    private void addToSet(MyList path, KeyValuePair<long, long> range)
    {
        for (long vertex = range.Key; vertex < range.Value; ++vertex)
        {
            MyList fullPath = new MyList(path);
            fullPath.Add(vertex);
            _cycles.Add(fullPath);
        }
    }

    private void addToSet(MyList path, ISet<MyList> continuations)
    {
        foreach (MyList continuation in continuations)
        {
            MyList fullPath = new MyList(path);
            fullPath.AddRange(continuation);
            _cycles.Add(fullPath);
        }
    }

    // Prints all found cycles in the file. Each cycle is being printed twice (e.g. 1234 and 1432)
    private void printSet()
    {
        if (_traceToFile == true)
        {
            foreach (MyList path in _cycles)
            {
                _fileWriter.WriteLine(string.Join(" ", path.ToArray()));
            }
            _fileWriter.Flush();
        }
    }
}

/// <summary>
/// Custom class extending List and implementing IComparable, so that 
/// objects of this class be able to be stored in the SortedSet
/// </summary>
class MyList : List<long>, System.IComparable<MyList>
{
    public MyList() : base()
    {
    }

    public MyList(IEnumerable<long> enumerable)
        : base(enumerable)
    {
    }

    public int CompareTo(MyList list)
    {
        if (list == null)
        {
            return 1;
        }
        int minCount = this.Count < list.Count ? this.Count : list.Count;
        for (int i = 0; i < minCount; ++i)
        {
            if (this[i] < list[i])
            {
                return -1;
            }
            else if (this[i] > list[i])
            {
                return 1;
            }
        }
        if (this.Count < minCount)
        {
            return -1;
        }
        else if (this.Count > minCount)
        {
            return 1;
        }
        return 0;
    }

    public override string ToString()
    {
        return string.Join(" ", this.ToArray());
    }

} // class

} // namespace