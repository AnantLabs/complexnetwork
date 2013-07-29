using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RandomGraph.Common.Model;
using System.Threading;
using RandomGraph.Core.Events;
using RandomGraph.Common.Model.Status;
using RandomGraph.Common.Model.Result;
using System.Net;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ComplexNetworkWorkerService : IComplexNetworkWorkerService
    {
        private string hostname = System.Environment.MachineName;
        private int startIndex;
        private int endIndex;
        private Dictionary<int, AbstractGraphModel> Models { get; set; }
        private Dictionary<int, Thread> Threads { get; set; }
        private OperationContext context;

        IDuplexCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IDuplexCallback>();
            }
        }

        public void StopAll()
        {
            ThreadPool.QueueUserWorkItem(
            delegate
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    StopInstanceAsync(i);
                }
            });
        }

        // no callback during service's method work.
        public void StopInstance(int index)
        {
            ThreadPool.QueueUserWorkItem(StopInstanceAsync, index);
        }

        private void StopInstanceAsync(object state)
        {
            int index = (int)state;
            Models[index].InvokeFailureProgressEvent(GraphProgress.Stopped, "User stopped calculation");
            Thread thread = Threads[index];
            if (thread.ThreadState != ThreadState.Suspended)
            {
                try
                {
                    thread.Abort();
                }
                catch (ThreadStateException) { }
            }  
        }

        public void PauseAll()
        {
            ThreadPool.QueueUserWorkItem(
            delegate
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    PauseInstanceAsync(i);
                }
            });
        }

        public void PauseInstance(int index)
        {
            ThreadPool.QueueUserWorkItem(PauseInstanceAsync, index);
        }

        private void PauseInstanceAsync(object state)
        {
            
            int index = (int)state;
            if (Threads[index].IsAlive)
            {
                Models[index].InvokeFailureProgressEvent(GraphProgress.Paused, "User paused calculation");
                Threads[index].Suspend();
            }
        }

        public void ContinueAll()
        {
            ThreadPool.QueueUserWorkItem(
            delegate
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    ContinueInstanceAsync(i);
                }
            });
        }

        public void ContinueInstance(int index)
        {
            ThreadPool.QueueUserWorkItem(ContinueInstanceAsync, index);
        }

        private void ContinueInstanceAsync(object state)
        {
            int index = (int)state;
            Models[index].InvokeFailureProgressEvent(GraphProgress.Calculating, "");
            Thread thread = Threads[index];
            if (thread.ThreadState == ThreadState.Suspended || thread.ThreadState == ThreadState.Unstarted)
            {
                thread.Resume();
            }
        }

        public void Start(AbstractGraphModel origineModel, int startIndex, int endIndex)
        {
            context = OperationContext.Current;
            context.Channel.OperationTimeout = TimeSpan.FromSeconds(1000);
            this.startIndex = startIndex;
            this.endIndex = endIndex;

            Models = new Dictionary<int, AbstractGraphModel>();
            Threads = new Dictionary<int, Thread>();
            int processorcount = Environment.ProcessorCount;

            for (int i = startIndex; i < endIndex; i++)
            {
                AbstractGraphModel model = origineModel.Clone();
                model.Progress += new GraphProgressEventHandler(OnSeparateModelProgress);
                Models.Add(i, model);

                Threads[i] = new Thread(new ParameterizedThreadStart(StartAnalyze)) { Priority = ThreadPriority.Lowest };
                
            }

            for (int i = startIndex; i < endIndex; i++)
            {
                if (Models[i].CurrentStatus.GraphProgress != GraphProgress.Stopped)
                {
                    Threads[i].Start(i);
                    if ((i + 1) % processorcount == 0)
                    {
                        if ((i + 1) % processorcount == 0)
                        {
                            for (int j = 0; j < processorcount; ++j)
                                Threads[i - j].Join();
                        }
                    }
                }
            }
        }

        private void StartAnalyze(object obj)
        {
            int index = (int)obj;
            try
            {
                Models[index].StartGenerate();
                Models[index].StartAnalize();
            }
            catch (SystemException)
            {
                //Models[index].InvokeFailureProgressEvent(GraphProgress.Stopped, "User stopped calculation");
            }
            catch (Exception ex)
            {
               // Models[index].InvokeFailureProgressEvent(GraphProgress.Failed, "");
            }
            finally
            {
                Models[index].Dispose();
            }
        }

        void OnSeparateModelProgress(object sender, GraphProgressEventArgs e)
        {
            AbstractGraphModel senderModel = (AbstractGraphModel)sender;
            senderModel.CurrentStatus.HostName = hostname;
            try
            {
                AnalizeResult tempResult = senderModel.Result;
                object graph = senderModel.Graph;
                if (e.Progress.GraphProgress != GraphProgress.Done)
                {
                    senderModel.Result = null;
                }
                senderModel.Graph = null;
                lock (this)
                {
                    if (context.Channel.State != CommunicationState.Opened)
                    {
                       //context.Channel.Open(TimeSpan.FromSeconds(5));
                        //
                    }

                    try
                    {
                        context.GetCallbackChannel<IDuplexCallback>().ProgressReport(senderModel, e);
                    }
                    catch (SystemException) { }
                }
                senderModel.Result = tempResult;
                senderModel.Graph = graph;
            }
            catch (CommunicationException) { }
            catch (TimeoutException) {}
        }
    }
}
