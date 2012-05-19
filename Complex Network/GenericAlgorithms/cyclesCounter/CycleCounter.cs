/**
 * @File CycleCounter.cs
 * 
 * @Author Hovhannes Antonyan
 */

using System;
using System.Collections.Generic;
using System.Threading;

namespace Algorithms
{
    public interface INeighbourshipContainer
    {
        // Get functions //
        int Size
        {
            get;
        }

        Dictionary<int, List<int>> Neighbourship
        {
            get;
        }
    }

    interface ICycleCounter
    {
        long calculateCyclesCount(int cycleLength);
    }

    /**
     * The class is responsible for calculation of the cycles count with 
     * specified length in a graph.
     */
    public class CycleCounter
    {
        // The actual class which performs the calculation
        private ICycleCounter _counter;

        public CycleCounter(INeighbourshipContainer container)
        {
            _counter = new CycleCounterParallel(container);
            //_counter = new CycleCounterSingleThreaded(container);
        }

        public Dictionary<long, long> getCyclesCount(int startRange, int endRange)
        {
            Dictionary<long/*length*/, long/*count*/> counts = new Dictionary<long, long>();
            for (int i = startRange; i <= endRange; ++i)
            {
                long count = getCyclesCount(i);
                counts.Add(i, count);
            }
            return counts;
        }

        /**
         * Returns cycles count in the graph which length is equal 
         * to the specified 'cycleLength' argument
         * @pre cycleLength >= 3
         */
        public long getCyclesCount(int cycleLength)
        {
            long count = 0;
            try
            {
                count = _counter.calculateCyclesCount(cycleLength);
            }
            catch (ThreadInterruptedException e)
            {
                // LOG exception
                // The thread was interrupted. Calculation is terminated
                // Print e.Message
                count = -1;
            }
            catch (NotSupportedException e)
            {
                // LOG exception
                // The host does not fully support thread pools. 
                // Use single threaded version of the class.
                // Print e.Message
                count = -1;
            }
            catch (Exception e)
            {
                // LOG exception
                // Exception occurred during calculation.
                // Print e.Message
                count = 1;
            }
            return count;
        }
    }
}
