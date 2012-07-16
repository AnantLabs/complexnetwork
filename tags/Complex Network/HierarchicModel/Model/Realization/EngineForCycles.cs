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

    private long _cycleCount = 0;
    private int _origin = -1;

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
        try
        {
            logger.Info("GetCycleCount Started: " + System.DateTime.Now.ToString() + ". Length= " + length);
            _cycleCount = 0;
            if (length <= 1)
            {
                logger.Warn("Received incorrect parameter. Length should be greater than 1. Returning 0");
                return 0;
            }
            if (length == 2)
            {
                double count = tree.countEdgesAllGraph();
                Debug.Assert(count == System.Math.Ceiling(count));
                _cycleCount = (long)count;
            }
            else
            {
                getCycleCount(tree, length);
            }
        }
        catch (System.Exception e)
        {
            logger.Error("GetCycleCount Failed. The reason was: " + e.Message);
            logger.Info("Found " + _cycleCount + " cycles at that moment");
            if (length > 2)
            {
                logger.Info("Considered " + _origin + " vertices out of " + tree.getGraphSize());
            }
            throw e;
        }
        finally
        {
            logger.Info("GetCycleCount Finished: " + System.DateTime.Now.ToString());
        }
        return _cycleCount;
    }

    /// <summary>
    /// Calculates the number of cycles with the lengths from lengthStart till lengthEnd 
    /// in the specified hierarchical tree
    /// </summary>
    /// <param name="tree">Tree in which cycles count should be found</param>
    /// <param name="lengthStart">The lengths range start(inclusive)</param>
    /// <param name="lengthEnd">The lengths range end(inclusive)</param>
    /// <returns>Dictionary from cycle length to cycle counts of that length</returns>
    public SortedDictionary<int, long> GetCycleCount(HierarchicGraph tree, int lengthStart, int lengthEnd)
    {
        logger.Info("GetCycleCount Started: " + System.DateTime.Now.ToString() 
                + ". Length= [" + lengthStart + ", " + lengthEnd + "]");
        SortedDictionary<int, long> result = new SortedDictionary<int, long>();
        int length = 0;
        try
        {
            if (lengthStart > lengthEnd || lengthStart <= 1)
            {
                logger.Error("Received incorrect parameters.");
                return null;
            }
            if (lengthStart == 2)
            {
                double count = tree.countEdgesAllGraph();
                Debug.Assert(count == System.Math.Ceiling(count));
                result.Add(lengthStart, (long)count);
            }
            if (lengthEnd > 2)
            {
                int start = (lengthStart > 2 ? lengthStart : 3);
                for (length = start; length <= lengthEnd; ++length)
                {
                    long count = getCycleCount(tree, length);
                    result.Add(length, count);
                }
            }
        }
        catch (System.Exception e)
        {
            logger.Error("GetCycleCount Failed. The reason was: " + e.Message);
            for (int l = lengthStart; l < length; ++length)
            {
                logger.Info("Found all " + result[l] + " cycles of length " + l);
            }
            if (length > 0)
            {
                logger.Info("Found " + _cycleCount + " cycles of length " + length + " at that moment");
                if (length > 2)
                {
                    logger.Info("Considered " + _origin + " vertices out of " + tree.getGraphSize());
                }
            }
            throw e;
        }
        finally
        {
            logger.Info("GetCycleCount Finished: " + System.DateTime.Now.ToString());
        }
        return result;
    }

    // --------------------------------------------------------------
    // Inner functions

    private long getCycleCount(HierarchicGraph tree, int cycleLength)
    {
        _cycleCount = 0;
        MyList branch = new MyList();
        for (_origin = 0; _origin < tree.getGraphSize(); ++_origin)
        {
            branch.Add(_origin);
            _cycleCount += getCyclesStartingWithOrigin(tree, _origin, tree.degree, branch, cycleLength) / 2;
            branch.RemoveAt(branch.Count - 1);
            Debug.Assert(branch.Count == 0);
        }
        return _cycleCount;
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
                            if (range.Key < range.Value)
                            {
                                count += range.Value - range.Key;
                            }
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
                    if (range.Key < range.Value)
                    {
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
                            if (!pathStart.Contains(pivot) && pivot >= range.Key && pivot < range.Key)
                            {
                                --count;
                            }
                        }
                        else  // start == end != current
                        {
                            count = getEdgesCount(tree, level, branch, pathStart, origin, originStart);
                        }
                    }
                }
            }
            else
            {
                count = getPathsCount(tree, pivot, level, tree.degree, branch,
                        pathStart, pathLength, origin, start == end, originStart);
            }
            return count;
        }
        for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
        {
            ISet<MyList> innerPaths = getPaths(tree, pivot, level, tree.degree, branch, pathStart, innerLength, origin, false, 0);
            foreach (MyList path in innerPaths)
            {
                Debug.Assert(path.Count == innerLength + 1);
                pathStart.AddRange(path);
                for (int next = start; next < tree.prime; ++next)
                {
                    if (next != current && tree.areAdjacent(level - 1, parentId, current, next) == 1)
                    {
                        KeyValuePair<int, int> range = getVerticesRange(tree, treeId + next - current, level, origin);
                        if (pathLength - innerLength > 1)
                        {
                            for (int vertex = range.Key; vertex < range.Value; ++vertex)
                            {
                                if (!pathStart.Contains(vertex))
                                {
                                    count += getPathContinuationsCount(tree, vertex, level,
                                            treeId + next - current, parentId, start, next, end, pathStart,
                                            pathLength - innerLength - 1, origin, originStart);
                                }
                            }
                        }
                        else if (next == end && range.Key < range.Value)
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
                            else // start == next == end != current
                            {
                                count += getEdgesCount(tree, level, branch, pathStart, origin, originStart);
                            }
                        }
                    }
                }
                pathStart.RemoveRange(pathStart.Count - path.Count, path.Count);
            }
        }
        if (current == end)
        {
            count += getPathsCount(tree, pivot, level, tree.degree, branch, pathStart, pathLength, origin, 
                    start == end, originStart);
        }
        branch.RemoveAt(branch.Count - 1);
        return count;
    }

    private long getEdgesCount(HierarchicGraph tree, int level, MyList branch, MyList pathStart, int origin, int originStart)
    {
        branch.Add(origin);
        long count = getPathsCount(tree, origin, level, tree.degree, branch, pathStart, 1, origin, true, originStart);
        branch.RemoveAt(branch.Count - 1);
        return count;
    }

    private long getPathsCount(HierarchicGraph tree, int pivot, int highestAllowedLevel, int level, MyList branch,
            MyList pathStart, int pathLength, int origin, bool isStartEqualToEnd, int originStart)
    {
        if (pathLength == 0)
        {
            Debug.Assert(!pathStart.Contains(pivot));
            Debug.Assert(pathStart.Count != 0);
            Debug.Assert(pivot >= origin);
            return 1;
        }
        long count = 0;
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
                        count += getCrossTreePathsCount(tree, pivot, level, branch, parentId, start, end,
                                pathStart, pathLength, origin, originStart);
                    }
                }
            }
            else
            {
                for (int end = 0; end < tree.prime; ++end)
                {
                    if (end != start)
                    {
                        count += getCrossTreePathsCount(tree, pivot, level, branch, parentId, start, end,
                                pathStart, pathLength, origin, originStart);
                    }
                }
            }
            if (level > highestAllowedLevel + 1)
            {
                branch.Add(parentId);
                count += getPathsCount(tree, pivot, highestAllowedLevel, level - 1, branch,
                        pathStart, pathLength, origin, isStartEqualToEnd, originStart);
                branch.RemoveAt(branch.Count - 1);
            }
        }
        return count;
    }

    private long getCrossTreePathsCount(HierarchicGraph tree, int pivot, int level, MyList branch, int parentId, int start, int end, 
            MyList pathStart, int pathLength, int origin, int originStart)
    {
        long count = 0;
        int treeId = branch[branch.Count - 1];
        if (pathLength == 1)
        {
            if (tree.areAdjacent(level - 1, parentId, start, end) == 1)
            {
                KeyValuePair<int, int> range = getVerticesRange(tree, treeId + end - start, level, origin);
                if (range.Key < range.Value)
                {
                    count += range.Value - range.Key;
                    foreach (long vertex in pathStart)
                    {
                        if (vertex >= range.Key && vertex < range.Value)
                        {
                            --count;
                        }
                    }
                    if (!pathStart.Contains(pivot) && pivot >= range.Key && pivot < range.Key)
                    {
                        --count;
                    }
                }
            }
            return count;
        }
        for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
        {
            ISet<MyList> innerPaths = getInnerPaths(tree, pivot, level, branch, pathStart, innerLength, origin, originStart);
            foreach (MyList path in innerPaths)
            {
                pathStart.AddRange(path);
                for (int next = 0; next < tree.prime; ++next)
                {
                    if (next != start && tree.areAdjacent(level - 1, parentId, start, next) == 1)
                    {
                        KeyValuePair<int, int> range = getVerticesRange(tree, treeId + next - start, level, origin);
                        if (pathLength - innerLength > 1)
                        {
                            for (int vertex = range.Key; vertex < range.Value; ++vertex)
                            {
                                if (!pathStart.Contains(vertex))
                                {
                                    count += getPathContinuationsCount(tree, vertex, level,
                                            treeId + next - start, parentId, start, next, end, pathStart,
                                            pathLength - innerLength - 1, origin, originStart);
                                }
                            }
                        }
                        else if (next == end && range.Key < range.Value)
                        {
                            count += range.Value - range.Key;
                            foreach (long vertex in pathStart)
                            {
                                if (vertex >= range.Key && vertex < range.Value)
                                {
                                    --count;
                                }
                            }
                            if (!pathStart.Contains(pivot) && pivot >= range.Key && pivot < range.Value)
                            {
                                --count;
                            }
                        }
                    }
                }
                pathStart.RemoveRange(pathStart.Count - path.Count, path.Count);
            }
        }
        return count;
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
                pathStart.AddRange(path);
                for (int next = start; next < tree.prime; ++next)
                {
                    if (next != current && tree.areAdjacent(level - 1, parentId, current, next) == 1)
                    {
                        KeyValuePair<int, int> range = getVerticesRange(tree, treeId + next - current, level, origin);
                        for (int vertex = range.Key; vertex < range.Value; ++vertex)
                        {
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
                        }
                    }
                }
                pathStart.RemoveRange(pathStart.Count - path.Count, path.Count);
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
                branch.RemoveAt(branch.Count - 1);
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
                pathStart.AddRange(path);
                for (int next = 0; next < tree.prime; ++next)
                {
                    if (next != start && tree.areAdjacent(level - 1, parentId, start, next) == 1)
                    {
                        KeyValuePair<int, int> range = getVerticesRange(tree, treeId + next - start, level, origin);
                        for (int vertex = range.Key; vertex < range.Value; ++vertex)
                        {
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
                        }
                    }
                }
                pathStart.RemoveRange(pathStart.Count - path.Count, path.Count);
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

    } // class MyList
} // class EngineForCycles

} // namespace