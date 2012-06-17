using System;
using System.Collections.Generic;
using System.Text;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Result;
using RandomGraph.Common.Model.Status;
using System.Threading;
using System.Collections;
using RandomGraph.Core.Events;
using System.Runtime.Serialization;
using AnalyzerFramework.Manager.ModelRepo;
using CommonLibrary.Model.Events;
using CommonLibrary.Model.Util;
using System.Collections;
using System.Configuration;

namespace RandomGraph.Common.Model
{
    /// <summary>
    /// Base class for graph models implementations,
    /// Defines Template method Design Pattern by introducing
    /// two protected methods for calling appropriate generation and analyze 
    /// methods, and provides other interface to outside.
    /// Generation and analyze methods available from outside
    /// simply wrap that computational process into another thread
    /// so it can gain control over it when needed.
    /// 
    /// </summary>
    [KnownType("GetKnownModelTypes")]
    public abstract class AbstractGraphModel : IGraphModel
    {
        public event GraphProgressEventHandler Progress;
        public event GraphGeneratedDelegate GraphGenerated;
        public  static bool staticGeneration;

        public AbstractGraphModel() { }

        /// <summary>
        /// only constructor of Graph model base class, so
        /// it's mandatory that input parameters are passed from child constructor to this
        /// during objetc creation process
        /// </summary>
        /// <param name="genParam">Generation parameteres map</param>
        /// <param name="options">selected analyze options</param>
        /// <param name="sequenceNumber">number in sequence for identifieng results</param>
        public AbstractGraphModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, int sequenceNumber)
        {
            ID = sequenceNumber;
            GenerationParamValues = genParam;
            AnalizeOptions = options;
            Result = new AnalizeResult()
            {
                InstanceID = ID
            };

            CurrentStatus = new GraphProgressStatus();
            CurrentStatus.GraphProgress = GraphProgress.Initializing;
        }

        /// <summary>
        /// Current status of graph model
        /// execution process
        /// </summary>
        public GraphProgressStatus CurrentStatus { get; set; }

        /// <summary>
        /// Map of values that should be used for generation as generation 
        /// params.
        /// </summary>
        public Dictionary<GenerationParam, object> GenerationParamValues { get; set; }
        #region Properties

        /// <summary>
        /// Unique ID of current model
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// reference to graph object created during
        /// generation process
        /// </summary>
        public object Graph { get; set; }

        /// <summary>
        /// Defines options that are available for 
        /// calculation and could be choosen by user.
        /// </summary>
        public AnalyseOptions AvailableOptions { get; set; }

        /// <summary>
        /// Defines generation rule for model.
        /// Currently supported types are separate and
        /// sequential.
        /// </summary>
        public GenerationRule GenerationRule { get; set; }

        /// <summary>
        /// Result of current model computations
        /// </summary>
        public AnalizeResult Result { get; set; }

        /// <summary>
        /// Defines list of GenerationParams that are 
        /// mandatory for starting generation as this list could 
        /// various from model to model.
        /// </summary>
        public List<GenerationParam> RequiredGenerationParams { get; set; }

        /// <summary>
        /// Selected analyze options that should be calculated
        /// during analyze process
        /// </summary>
        public AnalyseOptions AnalizeOptions { get; set; }

        /// <summary>
        /// Values of AnalyseOptions
        /// </summary>
        public  Dictionary<String, Object> AnalizeOptionsValues { get; set; }

        public string ModelName { get; set; }

        #endregion

        /// <summary>
        /// Should be implemented by inhereted class. Called in StartGenerate method in separate thread. 
        /// For generation params refer GenerationParamValues dictionary.
        /// This method is required to initialize graph object after generation and
        /// invoke OnModelProgress() with generation done if generation is complete successfuly
        /// and failed argument otherwise.
        /// </summary>
        protected abstract void GenerateModel();
        protected abstract void StaticGenerateModel();
        /// <summary>
        /// Should be implemented by inhereted class. Called in StartAnalize method in separate thread. 
        /// For selected analize options refer AnalizeOptions flag enumeration.
        /// Should set Result object after completion
        /// </summary>
        protected abstract void AnalizeModel();

        /// <summary>
        /// Check input generation parameters. 
        /// </summary>
        public abstract bool CheckGenerationParams(int instances);

        /// <summary>
        /// Converted Graph local strucur to the adjacency matrix.
        /// </summary>
        public abstract bool[,] GetMatrix();

        /// <summary>
        /// Prints details information of parameters into the form.
        /// </summary>
        public abstract string GetParamsInfo();

        /// <summary>
        /// Dump generated graph matrix into file
        /// </summary>
        public void StartTrace(int instanceIndex, string modelName, string jobName)
        {
            string provider = ConfigurationManager.AppSettings["Storage"];
            string dir = ConfigurationManager.AppSettings[provider] + "\\" + modelName + "\\";
            string filePath = dir + jobName + "_" + instanceIndex + "_dump.txt";
            System.IO.Directory.CreateDirectory(dir);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                try
                {
                    bool[,] matrix = GetMatrix();
                    for (int i = 0; i < matrix.GetLength(0); ++i)
                    {
                        for (int j = 0; j < matrix.GetLength(1); ++j)
                        {
                            if (matrix[i, j])
                            {
                                file.Write(1 + " ");
                            }
                            else
                            {
                                file.Write(0 + " ");
                            }
                        }
                        file.WriteLine("");
                    }
                }
                catch (Exception)
                {

                }
                finally
                {

                }
            }
        }

        /// <summary>
        /// ???.
        /// </summary>
        protected void OnModelProgress(GraphProgressEventArgs args)
        {
            if (Progress != null)
            {
                CurrentStatus = args.Progress;
                args.Progress.ID = ID;
                Progress(this, args);
            }
        }

        /// <summary>
        /// This method is invoked if generation are completed
        /// </summary>
        protected void OnGraphGenerated(GraphGeneratedArgs e)
        {
            if (GraphGenerated != null)
            {
                GraphGenerated(this, e);
            }
        }

        #region Helper Methods

        protected void InvokeProgressEvent(GraphProgress progress)
        {
            InvokeProgressEvent(progress, null, null);
        }

        protected void InvokeProgressEvent(GraphProgress progress, String targetName)
        {
            InvokeProgressEvent(progress, null, targetName);
        }

        protected void InvokeProgressEvent(GraphProgress progress, int? percent)
        {
            InvokeProgressEvent(progress, percent, null);
        }
        protected void InvokeProgressEvent(GraphProgress progress, int? percent, String targetName)
        {
            CurrentStatus.GraphProgress = progress;
            CurrentStatus.Percent = percent != null ? percent : CurrentStatus.Percent;
            CurrentStatus.TargetName = targetName;
            CurrentStatus.FailReason = null;
            OnModelProgress(new GraphProgressEventArgs(CurrentStatus));
        }

        public void InvokeFailureProgressEvent(GraphProgress progress, String failReason)
        {
            if (CurrentStatus == null)
            {
                CurrentStatus = new GraphProgressStatus();
            }
            CurrentStatus.GraphProgress = progress;
            CurrentStatus.FailReason = failReason;
            OnModelProgress(new GraphProgressEventArgs(CurrentStatus));
        }

        #endregion

        #region IGraphModel Members

        /// <summary>
        /// Started analize GrapeMode.
        /// </summary>
        public void StartAnalize()
        {
            if (CurrentStatus.GraphProgress != GraphProgress.GenerationDone)
            {
                return;
            }
            AnalizeModel();
        }

        /// <summary>
        /// Started generate Graph model. Invoked only Randome mode.
        /// </summary>
        public void StartGenerate()
        {
            if (CurrentStatus.GraphProgress != GraphProgress.Ready)
            {
                return;
            }
            if(AbstractGraphModel.staticGeneration)
                 GenerateModel();
            else
                StaticGenerateModel();
        }

        public void StartGenerate(Object graphObj)
        {

            Graph = graphObj;
            StartGenerate();
        }

        public void UpdateGeneratedMatrix()
        {
            bool[,] matrix = GetMatrix();
            OnGraphGenerated(new GraphGeneratedArgs(new GraphTable(matrix)));
        }

        public virtual void Dispose()
        {

        }

        #endregion

        /// <summary>
        /// This method finds .dll files of all Models and imported to the system.
        /// </summary>
        private static Type[] GetKnownModelTypes()
        {
            return ModelRepository.GetInstance().GetAvailableModelTypes().ToArray();
        }
    }
}
