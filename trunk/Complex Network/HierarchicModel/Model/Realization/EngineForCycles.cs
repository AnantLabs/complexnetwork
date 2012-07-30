using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using log4net;

namespace Model.HierarchicModel.Realization
{
    /// <author>Hovhannes Antonyan</author>
    /// <summary>
    /// Вспомогательный класс-инженер (для вычисления числя циклов данного порядка в иерархическом графе).
    /// </summary>
    class EngineForCycles
    {
        // Организация работы с лог файлом.
        private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(EngineForCycles));

        private long cycleCount = 0;
        private int origin = -1;
        private HierarchicContainer container;

        public EngineForCycles(HierarchicContainer c) 
        {
            container = c;
        }

        public HierarchicContainer Container
        {
            set { container = value; }
        }

        /// <summary>
        /// Calculates the number of cycles with the specified length in the specified
        /// hierarchical container
        /// </summary>
        /// <param name="container">container in which cycles count should be found</param>
        /// <param name="length">Length of the cycles to search for (should be greater than 1).
        /// If it is equal to 2, number of edges is being returned</param>
        /// <returns>Return the count of cycles with the specified length.
        /// If the length is equal to 2, number of edges is being returned</returns>
        public long GetCycleCount(int length)
        {
            try
            {
                logger.Info("GetCycleCount Started: " + System.DateTime.Now.ToString() + ". Length= " + length);
                cycleCount = 0;
                if (length <= 1)
                {
                    logger.Warn("Received incorrect parameter. Length should be greater than 1. Returning 0");
                    return 0;
                }
                if (length == 2)
                {
                    double count = container.CountEdgesAllGraph();
                    Debug.Assert(count == System.Math.Ceiling(count));
                    cycleCount = (long)count;
                }
                else
                {
                    getCycleCount(length);
                }
            }
            catch (System.Exception e)
            {
                logger.Error("GetCycleCount Failed. The reason was: " + e.Message);
                logger.Info("Found " + cycleCount + " cycles at that moment");
                if (length > 2)
                {
                    logger.Info("Considered " + origin + " vertices out of " + container.Size);
                }
                throw e;
            }
            finally
            {
                logger.Info("GetCycleCount Finished: " + System.DateTime.Now.ToString());
            }
            return cycleCount;
        }

        /// <summary>
        /// Calculates the number of cycles with the lengths from lengthStart till lengthEnd 
        /// in the specified hierarchical container
        /// </summary>
        /// <param name="container">container in which cycles count should be found</param>
        /// <param name="lengthStart">The lengths range start(inclusive)</param>
        /// <param name="lengthEnd">The lengths range end(inclusive)</param>
        /// <returns>Dictionary from cycle length to cycle counts of that length</returns>
        public SortedDictionary<int, long> GetCycleCount(int lengthStart, int lengthEnd)
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
                    double count = container.CountEdgesAllGraph();
                    Debug.Assert(count == System.Math.Ceiling(count));
                    result.Add(lengthStart, (long)count);
                }
                if (lengthEnd > 2)
                {
                    int start = (lengthStart > 2 ? lengthStart : 3);
                    for (length = start; length <= lengthEnd; ++length)
                    {
                        long count = getCycleCount(length);
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
                    logger.Info("Found " + cycleCount + " cycles of length " + length + " at that moment");
                    if (length > 2)
                    {
                        logger.Info("Considered " + origin + " vertices out of " + container.Size);
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

        // Закрытая часть класса.

        private long getCycleCount(int cycleLength)
        {
            cycleCount = 0;
            MyList branch = new MyList();
            for (origin = 0; origin < container.Size; ++origin)
            {
                branch.Add(origin);
                cycleCount += getCyclesStartingWithOrigin(origin, container.Level, branch, cycleLength) / 2;
                branch.RemoveAt(branch.Count - 1);
                Debug.Assert(branch.Count == 0);
            }
            return cycleCount;
        }

        // Gets all cycles which start with origin vertex (and have length equal to the cycleLength)
        private long getCyclesStartingWithOrigin(int origin, int level, MyList branch, int cycleLength)
        {
            long count = 0;
            int containerId = branch[branch.Count - 1];
            int parentId = containerId / container.BranchIndex;
            int start = (int)(containerId % container.BranchIndex);
            int begining = start;
            if (level == container.Level)
            {
                ++begining;
            }
            for (int end = begining; end < container.BranchIndex; ++end)
            {
                if (start == end || container.AreAdjacent(level - 1, parentId, start, end) == 1)
                {
                    count += getOriginsPathsStartingEndingWith(origin, level, branch, parentId, start, end, cycleLength - 1);
                }
            }
            if (level > 1)
            {
                branch.Add(parentId);
                count += getCyclesStartingWithOrigin(origin, level - 1, branch, cycleLength);
                branch.RemoveAt(branch.Count - 1);
            }
            return count;
        }

        // Gets all paths which start with origin vertex, in the "start" subcontainer and end in the "end" subcontainer
        private long getOriginsPathsStartingEndingWith(int origin, int level, MyList branch,
                int parentId, int start, int end, int pathLength)
        {
            long count = 0;
            int containerId = branch[branch.Count - 1];
            for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
            {
                ISet<MyList> paths = getInnerPaths(origin, level, branch, new MyList(), innerLength, origin, start);
                if (paths.Count == 0)
			    {
				    return count;
			    }
			    foreach (MyList path in paths)
                {
                    Debug.Assert(path.Count != 0);
                    for (int next = start + 1; next < container.BranchIndex; ++next)
                    {
                        if (container.AreAdjacent(level - 1, parentId, start, next) == 1)
                        {
                            KeyValuePair<int, int> range = getVerticesRange(containerId + next - start, level, origin);
                            if (pathLength - innerLength - 1 > 0)
                            {
                                for (int vertex = range.Key; vertex < range.Value; ++vertex)
                                {
                                    int originStart = -1;
                                    if (start == end)
                                    {
                                        originStart = (int)(branch[branch.Count - 2] % container.BranchIndex);
                                    }
                                    count += getPathContinuationsCount(vertex, level,
                                            containerId + next - start, parentId, start, next, end,
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

        private long getPathContinuationsCount(int pivot, int level, int containerId,
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
                    if (container.AreAdjacent(level - 1, parentId, current, end) == 1)
                    {
                        KeyValuePair<int, int> range = getVerticesRange(containerId + end - current, level, origin);
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
                                count = getEdgesCount(container, level, branch, pathStart, origin, originStart);
                            }
                        }
                    }
                }
                else
                {
                    count = getPathsCount(pivot, level, container.Level, branch,
                            pathStart, pathLength, origin, start == end, originStart);
                }
                return count;
            }
            for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
            {
                ISet<MyList> innerPaths = getPaths(pivot, level, container.Level, branch, pathStart, innerLength, origin, false, 0);
                foreach (MyList path in innerPaths)
                {
                    Debug.Assert(path.Count == innerLength + 1);
                    pathStart.AddRange(path);
                    for (int next = start; next < container.BranchIndex; ++next)
                    {
                        if (next != current && container.AreAdjacent(level - 1, parentId, current, next) == 1)
                        {
                            KeyValuePair<int, int> range = getVerticesRange(containerId + next - current, level, origin);
                            if (pathLength - innerLength > 1)
                            {
                                for (int vertex = range.Key; vertex < range.Value; ++vertex)
                                {
                                    if (!pathStart.Contains(vertex))
                                    {
                                        count += getPathContinuationsCount(vertex, level,
                                                containerId + next - current, parentId, start, next, end, pathStart,
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
                                    count += getEdgesCount(container, level, branch, pathStart, origin, originStart);
                                }
                            }
                        }
                    }
                    pathStart.RemoveRange(pathStart.Count - path.Count, path.Count);
                }
            }
            if (current == end)
            {
                count += getPathsCount(pivot, level, container.Level, branch, pathStart, pathLength, origin, 
                        start == end, originStart);
            }
            branch.RemoveAt(branch.Count - 1);
            return count;
        }

        private long getEdgesCount(HierarchicContainer container, int level, MyList branch, MyList pathStart, int origin, int originStart)
        {
            branch.Add(origin);
            long count = getPathsCount(origin, level, container.Level, branch, pathStart, 1, origin, true, originStart);
            branch.RemoveAt(branch.Count - 1);
            return count;
        }

        private long getPathsCount(int pivot, int highestAllowedLevel, int level, MyList branch,
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
                int containerId = branch[branch.Count - 1];
                int parentId = containerId / container.BranchIndex;
                int start = (int)(containerId % container.BranchIndex);
                if (isStartEqualToEnd && level == highestAllowedLevel + 1)
                {
                    for (int end = originStart + 1; end < container.BranchIndex; ++end)
                    {
                        if (end != start && container.AreAdjacent(highestAllowedLevel, parentId, end, originStart) == 1)
                        {
                            count += getCrosscontainerPathsCount(pivot, level, branch, parentId, start, end,
                                    pathStart, pathLength, origin, originStart);
                        }
                    }
                }
                else
                {
                    for (int end = 0; end < container.BranchIndex; ++end)
                    {
                        if (end != start)
                        {
                            count += getCrosscontainerPathsCount(pivot, level, branch, parentId, start, end,
                                    pathStart, pathLength, origin, originStart);
                        }
                    }
                }
                if (level > highestAllowedLevel + 1)
                {
                    branch.Add(parentId);
                    count += getPathsCount(pivot, highestAllowedLevel, level - 1, branch,
                            pathStart, pathLength, origin, isStartEqualToEnd, originStart);
                    branch.RemoveAt(branch.Count - 1);
                }
            }
            return count;
        }

        private long getCrosscontainerPathsCount(int pivot, int level, MyList branch, int parentId, int start, int end, 
                MyList pathStart, int pathLength, int origin, int originStart)
        {
            long count = 0;
            int containerId = branch[branch.Count - 1];
            if (pathLength == 1)
            {
                if (container.AreAdjacent(level - 1, parentId, start, end) == 1)
                {
                    KeyValuePair<int, int> range = getVerticesRange(containerId + end - start, level, origin);
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
                ISet<MyList> innerPaths = getInnerPaths(pivot, level, branch, pathStart, innerLength, origin, originStart);
                foreach (MyList path in innerPaths)
                {
                    pathStart.AddRange(path);
                    for (int next = 0; next < container.BranchIndex; ++next)
                    {
                        if (next != start && container.AreAdjacent(level - 1, parentId, start, next) == 1)
                        {
                            KeyValuePair<int, int> range = getVerticesRange(containerId + next - start, level, origin);
                            if (pathLength - innerLength > 1)
                            {
                                for (int vertex = range.Key; vertex < range.Value; ++vertex)
                                {
                                    if (!pathStart.Contains(vertex))
                                    {
                                        count += getPathContinuationsCount(vertex, level,
                                                containerId + next - start, parentId, start, next, end, pathStart,
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

        // Gets all possible continuations for the "pathStart" passing through "pivot" vertex and ending in the "end" subcontainer,
        // and have the length equal to pathLength
        private ISet<MyList> getPathContinuations(int pivot, int level, int containerId,
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
                    if (container.AreAdjacent(level - 1, parentId, current, end) == 1)
                    {
                        KeyValuePair<int, int> range = getVerticesRange(containerId + end - current, level, origin);
                        for (int vertex = range.Key; vertex < range.Value; ++vertex)
                        {
                            Debug.Assert(pathStart.Count != 0);
                            if (!pathStart.Contains(vertex) && (vertex != pivot)
                                && (start != end || start == end && areVerticesConnected(vertex, origin)))
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
                    ISet<MyList> innerPaths = getPaths(pivot, level, container.Level, branch,
                            pathStart, pathLength, origin, start == end, originStart);
                    mergeSet<MyList>(paths, innerPaths);
                }
                return paths;
            }
            for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
            {
                ISet<MyList> innerPaths = getPaths(pivot, level, container.Level, branch, pathStart, innerLength, origin, false, 0);
			    foreach (MyList path in innerPaths)
                {
                    Debug.Assert(path.Count == innerLength + 1);
                    pathStart.AddRange(path);
                    for (int next = start; next < container.BranchIndex; ++next)
                    {
                        if (next != current && container.AreAdjacent(level - 1, parentId, current, next) == 1)
                        {
                            KeyValuePair<int, int> range = getVerticesRange(containerId + next - current, level, origin);
                            for (int vertex = range.Key; vertex < range.Value; ++vertex)
                            {
                                if (!pathStart.Contains(vertex))
                                {
                                    if (pathLength - innerLength > 1)
                                    {
                                        ISet<MyList> continuations = getPathContinuations(vertex, level,
                                                containerId + next - current, parentId, start, next, end, pathStart,
                                                pathLength - innerLength - 1, origin, originStart);
                                        foreach (MyList continuation in continuations)
                                        {
                                            MyList fullPath = new MyList(path);
                                            fullPath.AddRange(continuation);
                                            paths.Add(fullPath);
                                        }
                                    }
                                    else if (next == end && (start != end || start == end && 
                                            areVerticesConnected(vertex, origin)))
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
                ISet<MyList> innerPaths = getPaths(pivot, level, container.Level, branch, pathStart, pathLength, origin, start == end, originStart);
                mergeSet<MyList>(paths, innerPaths);
            }
            branch.RemoveAt(branch.Count - 1);
            return paths;
        }

        // Gets all paths starting with the "pivot" vertex, having the length equal to pathLength by rising up to the container
        // to the highestAllowedLevel
        private ISet<MyList> getPaths(int pivot, int highestAllowedLevel, int level,
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
                int containerId = branch[branch.Count - 1];
                int parentId = containerId / container.BranchIndex;
                int start = (int)(containerId % container.BranchIndex);
                if (isStartEqualToEnd && level == highestAllowedLevel + 1)
                {
                    for (int end = originStart + 1; end < container.BranchIndex; ++end)
                    {
                        if (end != start && container.AreAdjacent(highestAllowedLevel, parentId, end, originStart) == 1)
                        {
                            ISet<MyList> crossPaths = getCrosscontainerPaths(pivot, level, branch, parentId, start, end,
                                    pathStart, pathLength, origin, originStart);
                            mergeSet<MyList>(paths, crossPaths);
                        }
                    }
                }
                else
                {
                    for (int end = 0; end < container.BranchIndex; ++end)
                    {
                        if (end != start)
                        {
                            ISet<MyList> crossPaths = getCrosscontainerPaths(pivot, level, branch, parentId, start, end,
                                    pathStart, pathLength, origin, originStart);
                            mergeSet<MyList>(paths, crossPaths);
                        }
                    }
                }
                if (level > highestAllowedLevel + 1)
                {
                    branch.Add(parentId);
                    ISet<MyList> innerPaths = getPaths(pivot, highestAllowedLevel, level - 1, branch,
                            pathStart, pathLength, origin, isStartEqualToEnd, originStart);
                    branch.RemoveAt(branch.Count - 1);
                    mergeSet<MyList>(paths, innerPaths);
                }
            }
            return paths;
        }

        // Gets all paths starting with the "pivot" vertex, having the length equal to pathLength by crossing
        // subcontainers and ending with "end" subcontainer
        private ISet<MyList> getCrosscontainerPaths(int pivot, int level, MyList branch,
                int parentId, int start, int end, MyList pathStart, int pathLength, int origin, int originStart)
        {
            ISet<MyList> paths = new SortedSet<MyList>();
            int containerId = branch[branch.Count - 1];
            if (pathLength == 1)
            {
                if (container.AreAdjacent(level - 1, parentId, start, end) == 1)
                {
                    KeyValuePair<int, int> range = getVerticesRange(containerId + end - start, level, origin);
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
                ISet<MyList> innerPaths = getInnerPaths(pivot, level, branch, pathStart, innerLength, origin, originStart);
                foreach (MyList path in innerPaths)
                {
                    pathStart.AddRange(path);
                    for (int next = 0; next < container.BranchIndex; ++next)
                    {
                        if (next != start && container.AreAdjacent(level - 1, parentId, start, next) == 1)
                        {
                            KeyValuePair<int, int> range = getVerticesRange(containerId + next - start, level, origin);
                            for (int vertex = range.Key; vertex < range.Value; ++vertex)
                            {
                                if (!pathStart.Contains(vertex))
                                {
                                    if (pathLength - innerLength > 1)
                                    {
                                        ISet<MyList> continuations = getPathContinuations(vertex, level,
                                                containerId + next - start, parentId, start, next, end, pathStart,
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

        // Gets all inner paths of the current subcontainer having the length equal to pathLength
        private ISet<MyList> getInnerPaths(int pivot, int level, MyList branch,
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
            if (level == container.Level)
            {
                return paths;
            }
            int containerId = branch[branch.Count - 1];
            int childId = branch[branch.Count - 2];
            int start = (int)(childId % container.BranchIndex);
            branch.RemoveAt(branch.Count - 1);
            for (int end = start + 1; end < container.BranchIndex; ++end)
            {
                ISet<MyList> crossPaths = getCrosscontainerPaths(pivot, level + 1, branch, containerId, start, end,
                        pathStart, pathLength, origin, originStart);
                mergeSet<MyList>(paths, crossPaths);
            }
            ISet<MyList> innerPaths = getInnerPaths(pivot, level + 1, branch, pathStart, pathLength, origin, originStart);
            mergeSet<MyList>(paths, innerPaths);
            branch.Add(containerId);
            return paths;
        }

        // Gets the "containerId" container's all vertices range
        private KeyValuePair<int/*start*/, int/*end+1*/> getVerticesRange(int containerId, int level, int origin)
        {
            int branchCount = (int)System.Math.Pow(container.BranchIndex, container.Level - level);
            int start = containerId * branchCount;
            int end = start + branchCount;
            if (start <= origin)
            {
                start = origin + 1;
            }
            return new KeyValuePair<int, int>(start, end);
        }

        // Checks if two vertices (probably in the different subcontainers) are connected or not
        private bool areVerticesConnected(int vertex1, int vertex2)
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
            int level = container.Level;
            while ((min / container.BranchIndex) != (max / container.BranchIndex))
            {
                min /= container.BranchIndex;
                max /= container.BranchIndex;
                --level;
            }
            return container.AreAdjacent(level - 1, max / container.BranchIndex, (int)min % container.BranchIndex, 
                (int)max % container.BranchIndex) == 1;
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
        }
    }
}