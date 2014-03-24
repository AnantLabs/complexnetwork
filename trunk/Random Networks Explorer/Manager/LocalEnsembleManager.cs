using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Core;
using Core.Attributes;
using Core.Enumerations;
using Core.Exceptions;

namespace Manager
{
    /// <summary>
    /// 
    /// </summary>
    class LocalEnsembleManager : AbstractEnsembleManager
    {
        private AbstractNetwork[] networks;
        private Thread[] threads;
        private AutoResetEvent[] waitHandles;

        public override void Run()
        {
            networks = new AbstractNetwork[realizationCount];
            threads = new Thread[realizationCount];
            waitHandles = new AutoResetEvent[realizationCount];

            PrepareForRun();            

            int pc = Environment.ProcessorCount;
            for (int i = 0; i < realizationCount; i++)
            {
                threads[i].Start(i);
                if ((i + 1) % pc == 0)
                {
                    for (int j = 0; j < pc; ++j)
                        threads[i - j].Join();
                }
            }
            Thread waitingThread = new Thread(new ThreadStart(Waiting));
            waitingThread.Start();
        }

        public override void Cancel()
        {
            for (int i = 0; i < realizationCount; i++)
            {
                Thread thread = threads[i];
                if (thread.ThreadState != ThreadState.Suspended)
                {
                    try
                    {
                        thread.Abort();
                    }
                    catch (ThreadStateException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void PrepareForRun()
        {
            for (int i = 0; i < realizationCount; i++)
            {
                ModelTypeInfo[] info = (ModelTypeInfo[])modelType.GetType().GetCustomAttributes(typeof(ModelTypeInfo), false);
                Type t = Type.GetType(info[0].Implementation);
                Type[] constructTypes = new Type[] { 
                    typeof(Dictionary<GenerationParameter, object>), 
                    typeof(AnalyzeOption), 
                    typeof(String) };
                object[] invokeParams = new object[] { 
                    generationParameterValues, 
                    analyzeOptions, 
                    tracingPath };
                networks[i] = (AbstractNetwork)t.GetConstructor(constructTypes).Invoke(invokeParams);

                threads[i] = new Thread(new ParameterizedThreadStart(ThreadEntry)) { Priority = ThreadPriority.Lowest };
                waitHandles[i] = new AutoResetEvent(false);
            }
        }

        private void ThreadEntry(object p)
        {
            int currentIndex = (int)p;
            try
            {
                networks[currentIndex].Generate();
                networks[currentIndex].Analyze();
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
                waitHandles[currentIndex].Set();
            }
        }

        private void Waiting()
        {
            foreach (WaitHandle wh in waitHandles)
            {
                wh.WaitOne();
            }
        }
    }
}
