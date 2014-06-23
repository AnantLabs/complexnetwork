using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Core;
using Core.Attributes;
using Core.Enumerations;
using Core.Exceptions;
using Core.Result;

namespace Manager
{
    /// <summary>
    /// Implementation of manager, which distributes work on local machine.
    /// </summary>
    public class LocalEnsembleManager : AbstractEnsembleManager
    {
        private AbstractNetwork[] networks;
        private Thread[] threads;
        private AutoResetEvent[] waitHandles;
        private ThreadEntryData[] threadData;

        private class ThreadEntryData
        {
            public int FirstIndex { get; private set; }
            public int SecondIndex { get; private set; }
            public int ThreadIndex { get; set; }

            public ThreadEntryData(int f, int s, int tIndex)
            {
                FirstIndex = f;
                SecondIndex = s;
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

            Result = EnsembleResult.AverageResults(results);
        }

        public override void Cancel()
        {
            for (int i = 0; i < Environment.ProcessorCount; ++i)
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
            for (int i = 0; i < RealizationCount; i++)
            {
                ModelTypeInfo[] info = (ModelTypeInfo[])ModelType.GetType().GetField(ModelType.ToString()).GetCustomAttributes(typeof(ModelTypeInfo), false);
                Type t = Type.GetType(info[0].Implementation);
                Type[] constructTypes = new Type[] { 
                    typeof(Dictionary<ResearchParameter, object>),
                    typeof(Dictionary<GenerationParameter, object>), 
                    typeof(AnalyzeOption) };
                object[] invokeParams = new object[] { 
                    ResearchParamaterValues,
                    GenerationParameterValues, 
                    AnalyzeOptions };
                networks[i] = (AbstractNetwork)t.GetConstructor(constructTypes).Invoke(invokeParams);
            }

            int threadCount = Math.Min(networks.Length, Environment.ProcessorCount);
            // Creating thread related members
            threads = new Thread[threadCount];
            waitHandles = new AutoResetEvent[threadCount];
            threadData = new ThreadEntryData[threadCount];

            // Initialize threadData[]
            int c = networks.Length / threadCount;
            int e = networks.Length % threadCount;   
            for (int i = 0; i < e; ++i)
            {
                threadData[i] = new ThreadEntryData(i * (c + 1), (i + 1) * (c + 1), i);
            }
            for (int i = e; i < threadCount; ++i)
            {
                threadData[i] = new ThreadEntryData(i * c + e, (i + 1) * c + e, i);
            }
            
            // Initialize threads and handles
            for (int i = 0; i < threadCount; ++i)
            {
                waitHandles[i] = new AutoResetEvent(false);
                threads[i] = new Thread(new ParameterizedThreadStart(ThreadEntry)) { Priority = ThreadPriority.Lowest };
            }
        }

        private void ThreadEntry(object p)
        {
            ThreadEntryData d = (ThreadEntryData)p;

            try
            {
                for (int i = d.FirstIndex; i < d.SecondIndex; ++i)
                {
                    networks[i].OnUpdateStatus += new AbstractNetwork.StatusUpdateHandler(LocalEnsembleManager_OnUpdateStatus);
                    networks[i].Generate();
                    if(TracingPath != "")
                        networks[i].Trace(TracingPath + "_" + i.ToString());
                    networks[i].Analyze();

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

        private void LocalEnsembleManager_OnUpdateStatus(object sender, ProgressEventArgs e)
        {
            Console.WriteLine(e.Status);
        }
    }
}
