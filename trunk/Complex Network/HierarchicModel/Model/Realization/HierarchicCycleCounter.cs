using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Diagnostics;
//using System.IO;

namespace Model.HierarchicModel.Realization
{
    public class HierarchicCycleCounter
    {
        //private bool _traceToFile;
        //private string _fileName = null;
        //private TextWriter _fileWriter = null;
        private ISet<MyList> _cycles = new SortedSet<MyList>();

        public HierarchicCycleCounter()
        {
            //_traceToFile = false;
        }

        /*public HierarchicCycleCounter(string filename)
        {
            _traceToFile = true;
            this._fileName = filename;
        }*/

        public long getCycleCount(HierarchicGraph tree, long cycleLength)
        {
            //Debug.Assert(cycleLength > 2);
            try
            {
                /*if (_traceToFile)
                {
                    _fileWriter = new StreamWriter(_fileName);
                }*/
                long verticesCount = (long)System.Math.Pow(tree.prime, tree.degree);
                MyList branch = new MyList();
                for (long vertex = 0; vertex < verticesCount; ++vertex)
                {
                    branch.Add(vertex);
                    getPivotsCycles(tree, vertex, tree.degree, branch, cycleLength);
                    branch.RemoveAt(branch.Count - 1);
                    //Debug.Assert(branch.Count == 0);
                }
            }
            catch (System.Exception e)
            {
                return -1;
            }
            /*finally
            {
                if (_traceToFile)
                {
                    printSet();
                    _fileWriter.Close();
                }
            }*/
            return _cycles.Count / 2;
        }

        private void getPivotsCycles(HierarchicGraph tree, long pivot, int level, MyList branch, long cycleLength)
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
                    getPivotsPathsStartingEndingWith(tree, pivot, level, branch, parentId, start, end, cycleLength - 1);
                }
            }
            if (level > 1)
            {
                branch.Add(parentId);
                getPivotsCycles(tree, pivot, level - 1, branch, cycleLength);
                branch.RemoveAt(branch.Count - 1);
            }
        }

        private void getPivotsPathsStartingEndingWith(HierarchicGraph tree, long pivot, int level, MyList branch,
                long parentId, int start, int end, long pathLength)
        {
            long treeId = branch[branch.Count - 1];
            for (int innerLength = 0; innerLength <= pathLength - 1; ++innerLength)
            {
                ISet<MyList> paths = getInnerPaths(tree, pivot, level, branch, new MyList(), innerLength, pivot, start);
                foreach (MyList path in paths)
                {
                    //Debug.Assert(path.Count != 0);
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

        private ISet<MyList> getPathContinuations(HierarchicGraph tree, long pivot, int level, long treeId,
                long parentId, int start, int current, int end, MyList pathStart, long pathLength, long origin, int originStart)
        {
            ISet<MyList> paths = new SortedSet<MyList>();
            MyList branch = new MyList();
            branch.Add(pivot);
            //Debug.Assert(pathLength != 0);
            if (pathLength == 1)
            {
                if (end != current)
                {
                    if (tree.areAdjacent(level - 1, parentId, current, end) == 1)
                    {
                        KeyValuePair<long, long> range = getVerticesRange(tree, treeId + end - current, level);
                        for (long vertex = range.Key; vertex < range.Value; ++vertex)
                        {
                            //Debug.Assert(pathStart.Count != 0);
                            if (!pathStart.Contains(vertex) && (vertex > origin) && (vertex != pivot)
                                && (start != end || start == end && areVerticesConnected(tree, vertex, origin)))
                            {
                                //Debug.Assert(!pathStart.Contains(pivot));
                                //Debug.Assert(vertex >= origin);
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
                    //Debug.Assert(path.Count == innerLength + 1);
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
                                        //Debug.Assert(pathStart.Count != 0);
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

        private ISet<MyList> getPaths(HierarchicGraph tree, long pivot, int highestAllowedLayer, int level,
                MyList branch, MyList pathStart, long pathLength, long origin, bool isStartEqualToEnd, int originStart)
        {
            ISet<MyList> paths = new SortedSet<MyList>();
            if (pathLength == 0)
            {
                //Debug.Assert(!pathStart.Contains(pivot));
                //Debug.Assert(pathStart.Count != 0);
                //Debug.Assert(pivot >= origin);
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
                            //Debug.Assert(!pathStart.Contains(pivot));
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
                                        //Debug.Assert(pathStart.Count != 0);
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

        private ISet<MyList> getInnerPaths(HierarchicGraph tree, long pivot, int level, MyList branch,
                MyList pathStart, long pathLength, long origin, int originStart)
        {
            ISet<MyList> paths = new SortedSet<MyList>();
            if (pathLength == 0)
            {
                //Debug.Assert(!pathStart.Contains(pivot));
                //Debug.Assert(pivot >= origin);
                paths.Add(new MyList(new long[] { pivot }));
                return paths;
            }
            if (level == tree.degree)
            {
                return paths;
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
            return paths;
        }

        private KeyValuePair<long, long> getVerticesRange(HierarchicGraph tree, long treeId, int level)
        {
            long branchCount = (long)System.Math.Pow(tree.prime, tree.degree - level);
            long start = treeId * branchCount;
            long end = start + branchCount;
            return new KeyValuePair<long, long>(start, end);
        }

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

        /*private void printSet()
        {
            if (_traceToFile == true)
            {
                foreach (MyList path in _cycles)
                {
                    _fileWriter.WriteLine(string.Join(" ", path.ToArray()));
                }
                _fileWriter.Flush();
            }
        }*/
    }

    class MyList : List<long>, System.IComparable<MyList>
    {
        public MyList()
            : base()
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
    }
}