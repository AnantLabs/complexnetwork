/**
 * @File CyclesCounterSingleThreaded.cs
 * 
 * @Author Hovhannes Antonyan
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.BAModel.Realization;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Model.BAModel.Realization.CycleCounting;

namespace Model.BAModel.Realization
{
    /**
     * The class is responsible for calculation of the cycles count with 
     * specified length in a graph.
     */
    public class CyclesCounter
    {
        // reference to the BAContainer object which contains the graph
        private BAContainer _container;

        // The graph's vertices count
        private int _verticesCount;

        // The actual class which performs the calculation
        private CyclesParallelCounter _counter;

        public CyclesCounter(BAContainer container)
        {
            _container = container;
            _verticesCount = _container.Size;
            _counter = new CyclesParallelCounter(container);
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
