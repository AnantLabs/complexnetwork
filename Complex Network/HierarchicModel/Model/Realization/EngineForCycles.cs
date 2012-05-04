using System.Collections.Generic;
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
    // Constructors

    public EngineForCycles()
    {
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
    public long GetCycleCount(HierarchicGraph tree, int length)
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
                cycleCount = getCycleCount(tree, length);
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

    private long getCycleCount(HierarchicGraph tree, int cycleLength)
    {
        int verticesCount = (int)System.Math.Pow(tree.prime, tree.degree);
        MyList branch = new MyList();
        long count = 0;
        for (int origin = 0; origin < verticesCount; ++origin)
        {
            branch.Add(origin);
            count += getCyclesStartingWithOrigin(tree, origin, tree.degree, branch, cycleLength);
            branch.RemoveAt(branch.Count - 1);
            Debug.Assert(branch.Count == 0);
        }
        return count / 2;
    }

    // Gets all cycles which start with origin vertex (and have length equal to the cycleLength)
    private long getCyclesStartingWithOrigin(HierarchicGraph tree, int origin, int level, MyList branch, int cycleLength)
    {
        long count = 0;
        int treeId = branch[branch.Count - 1];
        int parentId = treeId / tree.prime;
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
                count += getOriginsPathsStartingEndingWith(tree, origin, level, branch, parentId, start, end, cycleLength - 1);
            }
        }
        if (level > 1)
        {
            branch.Add(parentId);
            count += getCyclesStartingWithOrigin(tree, origin, level - 1, branch, cycleLength);
            branch.RemoveAt(branch.Count - 1);
        }
        return count;
    }

    // Gets all paths which start with origin vertex, in the "start" subtree and end in the "end" subtree
    private long getOriginsPathsStartingEndingWith(HierarchicGraph tree, int origin, int level, MyList branch,
            int parentId, int start, int end, int pathLength)
    {
        long count = 0;
        int treeId = branch[branch.Count - 1];
        for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
        {
            ISet<MyList> paths = getInnerPaths(tree, origin, level, branch, new MyList(), innerLength, origin, start);
            if (paths.Count == 0)
			{
				return count;
			}
			foreach (MyList path in paths)
            {
                Debug.Assert(path.Count != 0);
                for (int next = start + 1; next < tree.prime; ++next)
                {
                    if (tree.areAdjacent(level - 1, parentId, start, next) == 1)
                    {
                        KeyValuePair<int, int> range = getVerticesRange(tree, treeId + next - start, level, origin);
                        if (pathLength - innerLength - 1 > 0)
                        {
                            for (int vertex = range.Key; vertex < range.Value; ++vertex)
                            {
                                int originStart = -1;
                                if (start == end)
                                {
                                    originStart = (int)(branch[branch.Count - 2] % tree.prime);
                                }
                                count += getPathContinuationsCount(tree, vertex, level,
                                        treeId + next - start, parentId, start, next, end,
                                        path, pathLength - innerLength - 1, origin, originStart);
                            }
                        }
                        else if (start != end && next == end)
                        {
                            count += range.Value - range.Key;
                        }
                    }
                }
            }
        }
        return count;
    }

    private long getPathContinuationsCount(HierarchicGraph tree, int pivot, int level, int treeId,
            int parentId, int start, int current, int end, MyList pathStart, int pathLength, int origin, int originStart)
    {
        long count = 0;
        MyList branch = new MyList();
        branch.Add(pivot);
        Debug.Assert(pathLength != 0);
        if (pathLength == 1)
        {
            if (end != current)
            {
                if (tree.areAdjacent(level - 1, parentId, current, end) == 1)
                {
                    KeyValuePair<int, int> range = getVerticesRange(tree, treeId + end - current, level, origin);
                    if (start != end)
                    {
                        count = range.Value - range.Key;
                        foreach (long vertex in pathStart) 
                        {
                            if (vertex >= range.Key && vertex < range.Value)
                            {
                                --count;
                            }
                        }
                    }
                    else 
                    {
                        count = getEdgesCount(tree, level, treeId, range, pathStart, origin);
                    }
                }
            }
            else
            {
                count = getPathsCount(tree, pivot, level, tree.degree, branch,
                        pathStart, pathLength, origin, start == end /*is always false*/, originStart);
            }
            return count;
        }
        for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
        {
            ISet<MyList> innerPaths = getPaths(tree, pivot, level, tree.degree, branch, pathStart, innerLength, origin, false, 0);
            foreach (MyList path in innerPaths)
            {
                Debug.Assert(path.Count == innerLength + 1);
                for (int next = start; next < tree.prime; ++next)
                {
                    if (next != current && tree.areAdjacent(level - 1, parentId, current, next) == 1)
                    {
                        KeyValuePair<int, int> range = getVerticesRange(tree, treeId + next - current, level, origin);
                        if (pathLength - innerLength > 1)
                        {
                            for (int vertex = range.Key; vertex < range.Value; ++vertex)
                            {
                                if (!pathStart.Contains(vertex) && !path.Contains(vertex))
                                {
                                    pathStart.AddRange(path);
                                    count += getPathContinuationsCount(tree, vertex, level,
                                            treeId + next - current, parentId, start, next, end, pathStart,
                                            pathLength - innerLength - 1, origin, originStart);
                                    pathStart.RemoveRange(pathStart.Count - path.Count, path.Count);
                                }
                            }
                        }
                        else if (next == end)
                        {
                            if (start != end)
                            {
                                count += range.Value - range.Key;
                                foreach (long vertex in pathStart)
                                {
                                    if (vertex >= range.Key && vertex < range.Value)
                                    {
                                        --count;
                                    }
                                }
                            }
                            else
                            {
                                count += getEdgesCount(tree, level, treeId, range, pathStart, origin);
                            }
                        }
                    }
                }
            }
        }
        if (current == end)
        {
            count += getPathsCount(tree, pivot, level, tree.degree, branch, pathStart, pathLength, origin, start == end, originStart);
        }
        branch.RemoveAt(branch.Count - 1);
        return count;
    }

    private long getEdgesCount(HierarchicGraph tree, int level, int treeId, KeyValuePair<int, int> range, 
            MyList pathStart, int origin)
    {
        // TODO change implementation to following: get paths count with length 1 in the treeId subtree and decrement 
        // as much how many vertices in pathStart belong to the range
        long count = 0;
        for (int vertex = range.Key; vertex < range.Value; ++vertex)
        {
            Debug.Assert(pathStart.Count != 0);
            if (!pathStart.Contains(vertex) && (vertex > origin) && areVerticesConnected(tree, vertex, origin))
            {
                ++count;
            }
        }
        return count;
    }

    private long getPathsCount(HierarchicGraph tree, int pivot, int highestAllowedLevel, int level, MyList branch, MyList pathStart, int pathLength,
            int origin, bool isStartEqualToEnd, int originStart)
    {
        return getPaths(tree, pivot, highestAllowedLevel, level, branch, pathStart, pathLength, origin, isStartEqualToEnd, originStart).Count;
    }


    // Gets all possible continuations for the "pathStart" passing through "pivot" vertex and ending in the "end" subtree,
    // and have the length equal to pathLength
    private ISet<MyList> getPathContinuations(HierarchicGraph tree, int pivot, int level, int treeId,
            int parentId, int start, int current, int end, MyList pathStart, int pathLength, int origin, int originStart)
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
                    KeyValuePair<int, int> range = getVerticesRange(tree, treeId + end - current, level, origin);
                    for (int vertex = range.Key; vertex < range.Value; ++vertex)
                    {
                        Debug.Assert(pathStart.Count != 0);
                        if (!pathStart.Contains(vertex) && (vertex != pivot)
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
                for (int next = start; next < tree.prime; ++next)
                {
                    if (next != current && tree.areAdjacent(level - 1, parentId, current, next) == 1)
                    {
                        KeyValuePair<int, int> range = getVerticesRange(tree, treeId + next - current, level, origin);
                        for (int vertex = range.Key; vertex < range.Value; ++vertex)
                        {
                            pathStart.AddRange(path);
                            if (!pathStart.Contains(vertex))
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
    // to the highestAllowedLevel
    private ISet<MyList> getPaths(HierarchicGraph tree, int pivot, int highestAllowedLevel, int level,
            MyList branch, MyList pathStart, int pathLength, int origin, bool isStartEqualToEnd, int originStart)
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
        if (level > highestAllowedLevel)
        {
            int treeId = branch[branch.Count - 1];
            int parentId = treeId / tree.prime;
            int start = (int)(treeId % tree.prime);
            if (isStartEqualToEnd && level == highestAllowedLevel + 1)
            {
                for (int end = originStart + 1; end < tree.prime; ++end)
                {
                    if (end != start && tree.areAdjacent(highestAllowedLevel, parentId, end, originStart) == 1)
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
            if (level > highestAllowedLevel + 1)
            {
                branch.Add(parentId);
                ISet<MyList> innerPaths = getPaths(tree, pivot, highestAllowedLevel, level - 1, branch,
                        pathStart, pathLength, origin, isStartEqualToEnd, originStart);
                mergeSet<MyList>(paths, innerPaths);
            }
        }
        return paths;
    }


    // Gets all paths starting with the "pivot" vertex, having the length equal to pathLength by crossing
    // subtrees and ending with "end" subtree
    private ISet<MyList> getCrossTreePaths(HierarchicGraph tree, int pivot, int level, MyList branch,
            int parentId, int start, int end, MyList pathStart, int pathLength, int origin, int originStart)
    {
        ISet<MyList> paths = new SortedSet<MyList>();
        int treeId = branch[branch.Count - 1];
        if (pathLength == 1)
        {
            if (tree.areAdjacent(level - 1, parentId, start, end) == 1)
            {
                KeyValuePair<int, int> range = getVerticesRange(tree, treeId + end - start, level, origin);
                for (int vertex = range.Key; vertex < range.Value; ++vertex)
                {
                    if (!pathStart.Contains(vertex) && (vertex != pivot))
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
                        KeyValuePair<int, int> range = getVerticesRange(tree, treeId + next - start, level, origin);
                        for (int vertex = range.Key; vertex < range.Value; ++vertex)
                        {
                            pathStart.AddRange(path);
                            if (!pathStart.Contains(vertex))
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
    private ISet<MyList> getInnerPaths(HierarchicGraph tree, int pivot, int level, MyList branch,
            MyList pathStart, int pathLength, int origin, int originStart)
    {
        ISet<MyList> paths = new SortedSet<MyList>();
        if (pathLength == 0)
        {
            Debug.Assert(!pathStart.Contains(pivot));
            Debug.Assert(pivot >= origin);
            paths.Add(new MyList(new int[] {pivot}));
            return paths;
        }
        if (level == tree.degree)
        {
            return paths;
        }
        int treeId = branch[branch.Count - 1];
        int childId = branch[branch.Count - 2];
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
        return paths;
    }

    // Gets the "treeId" tree's all vertices range
    private KeyValuePair<int/*start*/, int/*end+1*/> getVerticesRange(HierarchicGraph tree, int treeId, int level, int origin)
    {
        int branchCount = (int)System.Math.Pow(tree.prime, tree.degree - level);
        int start = treeId * branchCount;
        int end = start + branchCount;
        if (start <= origin)
        {
            start = origin + 1;
        }
        return new KeyValuePair<int, int>(start, end);
    }


    // Checks if two vertices (probably in the different subtrees) are connected or not
    private bool areVerticesConnected(HierarchicGraph tree, int vertex1, int vertex2)
    {
        int min = -1;
        int max = -1;
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

    /// <summary>
    /// Custom class extending List and implementing IComparable, so that 
    /// objects of this class be able to be stored in the SortedSet
    /// </summary>
    class MyList : List<int>, System.IComparable<MyList>
    {
        public MyList()
            : base()
        {
        }

        public MyList(IEnumerable<int> enumerable)
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

    } // MyList
} // EngineForCycles

} // namespace