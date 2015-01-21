using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Core;
using Core.Attributes;
using Core.Enumerations;
using Core.Events;
using Core.Exceptions;
using Core.Result;

namespace Manager
{
    /// <summary>
    /// Implementation of manager, which distributes work on local machine.
    /// </summary>
    public class LocalEnsembleManager : AbstractEnsembleManager
    {
        private Thread[] threads;
        private AutoResetEvent[] waitHandles;
        private ThreadEntryData[] threadData;

        private class ThreadEntryData
        {
            public int ThreadCount { get; private set; }
            public int ThreadIndex { get; set; }

            public ThreadEntryData(int tCount, int tIndex)
            {
                ThreadCount = tCount;
                ThreadIndex = tIndex;
            }
        }

        public override void Run()
        {
            PrepareData();

            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i].Start(threadData[i]);
            }

            AutoResetEvent.WaitAll(waitHandles);

            List<RealizationResult> results = new List<RealizationResult>();
            for (int i = 0; i < networks.Length; ++i)
            {
                if (networks[i].SuccessfullyCompleted)
                    results.Add(networks[i].NetworkResult);
            }

            if(results.Count != 0)
                Result = EnsembleResult.AverageResults(results);
        }

        public override void Cancel()
        {
            for (int i = 0; i < threads.Length && i < Environment.ProcessorCount; ++i)
            {
                if (threads[i] != null)
                {
                    try
                    {
                        threads[i].Abort();
                    }
                    catch (ThreadAbortException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        foreach (AutoResetEvent handle in waitHandles)
                            handle.Set();
                    }
                }
            }
        }

        private void PrepareData()
        {
            networks = new AbstractNetwork[RealizationCount];
            NetworkStatuses = new NetworkEventArgs[RealizationCount];
            for (int i = 0; i < RealizationCount; ++i)
            {
                ModelTypeInfo[] info = (ModelTypeInfo[])ModelType.GetType().GetField(ModelType.ToString()).GetCustomAttributes(typeof(ModelTypeInfo), false);
                Type t = Type.GetType(info[0].Implementation);
                Type[] constructTypes = new Type[] {
                    typeof(Dictionary<ResearchParameter, object>),
                    typeof(Dictionary<GenerationParameter, object>), 
                    typeof(AnalyzeOption) };
                object[] invokeParams = new object[] {
                    ResearchName,
                    ResearchParamaterValues,
                    GenerationParameterValues, 
                    AnalyzeOptions };
                networks[i] = (AbstractNetwork)t.GetConstructor(constructTypes).Invoke(invokeParams);

                networks[i].NetworkID = i;
                networks[i].OnUpdateStatus += new NetworkStatusUpdateHandler(AbstractEnsembleManager_OnUpdateNetworkStatus);

                NetworkStatuses[i] = new NetworkEventArgs();
                NetworkStatuses[i].ID = i;
            }

            int threadCount = Math.Min(networks.Length, Environment.ProcessorCount);
            // Creating thread related members
            threads = new Thread[threadCount];
            waitHandles = new AutoResetEvent[threadCount];
            threadData = new ThreadEntryData[threadCount];

            // Initialize threads and handles
            for (int i = 0; i < threadCount; ++i)
            {
                waitHandles[i] = new AutoResetEvent(false);
                threadData[i] = new ThreadEntryData(threadCount, i);
                threads[i] = new Thread(new ParameterizedThreadStart(ThreadEntry)) { Priority = ThreadPriority.Lowest };
            }
        }

        private void ThreadEntry(object p)
        {
            ThreadEntryData d = (ThreadEntryData)p;

            try
            {
                for (int i = 0; (d.ThreadIndex + i * d.ThreadCount) < networks.Length; ++i)
                {
                    int networkToRun = d.ThreadIndex + i * d.ThreadCount;
                    networks[networkToRun].Generate();
                    if(TracingPath != "")
                        networks[networkToRun].Trace(TracingPath + "_" + networkToRun.ToString());
                    networks[networkToRun].Analyze();

                    Interlocked.Increment(ref realizationsDone);
                }
            }
            catch (CoreException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                waitHandles[d.ThreadIndex].Set();
            }
        }
    }
}
