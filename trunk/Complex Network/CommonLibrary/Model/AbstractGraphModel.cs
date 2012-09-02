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
using System.Configuration;

using CommonLibrary.Model;
using RandomGraph.Common.Model.Settings;

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
    public abstract class AbstractGraphModel
    {
        public event GraphProgressEventHandler Progress;
        public event GraphGeneratedDelegate GraphGenerated;

        // Генератор графа.
        protected IGraphGenerator generator;
        // Анализатор графа.
        protected AbstarctGraphAnalyzer analyzer;

        public AbstractGraphModel() { }


        /// <summary>
        /// Constructor of Graph model base class, so
        /// it's mandatory that input parameters are passed from child constructor to this
        /// during objetc creation process
        /// </summary>
        /// <param name="genParam">Generation parameteres map</param>
        /// <param name="options">selected analyze options</param>
        /// <param name="sequenceNumber">number in sequence for identifieng results</param>
        public AbstractGraphModel(Dictionary<GenerationParam, object> genParam, AnalyseOptions options, Dictionary<String, Object> analizeOptionsValues)
        {
            //ID = sequenceNumber;
            GenerationParamValues = genParam;
            AnalizeOptions = options;
            AnalizeOptionsValues = analizeOptionsValues;

            CurrentStatus = new GraphProgressStatus();
            CurrentStatus.GraphProgress = GraphProgress.Initializing;
        }

        public AbstractGraphModel(ArrayList matrix, AnalyseOptions options, Dictionary<String, Object> analizeOptionsValues)
        {

            //GenerationParamValues = genParam;
            AnalizeOptions = options;
            AnalizeOptionsValues = analizeOptionsValues;
            NeighbourshipMatrix = matrix;

            CurrentStatus = new GraphProgressStatus();
            CurrentStatus.GraphProgress = GraphProgress.Initializing;
        }

        public void setID(int ID)
        {
            this.ID = ID;
            Result = new AnalizeResult()
            {
                InstanceID = ID
            };
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

        // Матрица смежности
        public ArrayList NeighbourshipMatrix { get; set; }

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
        public Dictionary<String, Object> AnalizeOptionsValues { get; set; }

        public string ModelName { get; set; }

        #endregion

        /// <summary>
        /// Функция вызывается в методе StartGenerate в отдельном потоке.
        /// Для получения параметров генерации (динамическая генерация) используется GenerationParamValues dictionary.
        /// Для получения матрицы смежности (статическая генерация) используется NeighbourshipMatrix ArrayList.
        /// </summary>
        protected void GenerateModel()
        {
            InvokeProgressEvent(GraphProgress.StartingGeneration, 5);

            try
            {
                if (Options.GenerationMode.randomGeneration == Options.Generation)    // Динамическая генерация
                    generator.RandomGeneration(GenerationParamValues);
                else    // Статическая генерация
                    generator.StaticGeneration(NeighbourshipMatrix);

                InvokeProgressEvent(GraphProgress.GenerationDone, 30);
            }
            catch (ThreadAbortException) { }
            catch (Exception)
            {
                InvokeFailureProgressEvent(GraphProgress.GenerationFailed, "ENTER FAIL REASON HERE");
                //RETHROW EXCEPTION 
            }
            finally
            {
                //Place clean up code here
            }
        }

        /// <summary>
        /// Функция вызывается в методе StartAnalize в отдельном потоке. 
        /// Для получения выбранных опций анализа используется флаг перечисления AnalizeOptions.
        /// После окончания анализа обьект Result должен иметь значение.
        /// </summary>
        protected void AnalyzeModel()
        {
            InvokeProgressEvent(GraphProgress.StartingAnalizing);

            analyzer.Container = generator.Container;

            try
            {
                if ((AnalizeOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 10, "Average Path");
                    Result.Result[AnalyseOptions.AveragePath] = analyzer.GetAveragePath();
                }

                if ((AnalizeOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 20, "Diameter");
                    Result.Result[AnalyseOptions.Diameter] = analyzer.GetDiameter();
                }

                if ((AnalizeOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 30, "Cycles of order 3");
                    Result.Result[AnalyseOptions.Cycles3] = analyzer.GetCycles3();
                }

                if ((AnalizeOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 40, "Cycles of order 4");
                    Result.Result[AnalyseOptions.Cycles4] = analyzer.GetCycles4();
                }

                if ((AnalizeOptions & AnalyseOptions.CycleEigen3) == AnalyseOptions.CycleEigen3)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 40, "Cycles of order 3 (Eigen)");
                    Result.Result[AnalyseOptions.CycleEigen3] = analyzer.GetCyclesEigen3();
                }

                if ((AnalizeOptions & AnalyseOptions.CycleEigen4) == AnalyseOptions.CycleEigen4)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 40, "Cycles of order 4 (Eigen)");
                    Result.Result[AnalyseOptions.CycleEigen4] = analyzer.GetCyclesEigen4();
                }

                if ((AnalizeOptions & AnalyseOptions.EigenValue) == AnalyseOptions.EigenValue)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 50, "Calculating EigenValues");
                    Result.EigenVector = analyzer.GetEigenValues();
                }

                if ((AnalizeOptions & AnalyseOptions.DistEigenPath) == AnalyseOptions.DistEigenPath)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 60, "Distances between Eigen Values");
                    Result.DistancesBetweenEigenValues = analyzer.GetDistEigenPath();
                }

                if ((AnalizeOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 60, "Degree Distribution");
                    Result.VertexDegree = analyzer.GetDegreeDistribution();
                }

                if ((AnalizeOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 70, "Clustering Coefficient");
                    Result.Coefficient = analyzer.GetClusteringCoefficient();
                }

                if ((AnalizeOptions & AnalyseOptions.ConnSubGraph) == AnalyseOptions.ConnSubGraph)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 70, "Connected Subgraphs Orders");
                    Result.Subgraphs = analyzer.GetConnSubGraph();
                }

                if ((AnalizeOptions & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 70, "Full Subgraphs Orders");
                    Result.FullSubgraphs = analyzer.GetFullSubGraph();
                }

                if ((AnalizeOptions & AnalyseOptions.MinPathDist) == AnalyseOptions.MinPathDist)
                {
                    InvokeProgressEvent(GraphProgress.Analizing, 80, "Minimal Path Distance Distribution");
                    Result.DistanceBetweenVertices = analyzer.GetMinPathDist();
                }

                if ((AnalizeOptions & AnalyseOptions.Cycles) == AnalyseOptions.Cycles)
                {
                    int maxValue = Int32.Parse((String)AnalizeOptionsValues["cyclesHi"]);
                    int minvalue = Int32.Parse((String)AnalizeOptionsValues["cyclesLow"]);

                    InvokeProgressEvent(GraphProgress.Analizing, 85, "Cycles of " + minvalue + "-" + maxValue + "degree");
                    Result.Cycles = analyzer.GetCycles(minvalue, maxValue);
                }

                if ((AnalizeOptions & AnalyseOptions.Motifs) == AnalyseOptions.Motifs)
                {
                    int maxValue = Int32.Parse((String)AnalizeOptionsValues["motiveHi"]);
                    int minvalue = Int32.Parse((String)AnalizeOptionsValues["motiveLow"]);
                    InvokeProgressEvent(GraphProgress.Analizing, 90, "Motiv of " + minvalue + "-" + maxValue + "degree");
                    Result.MotivesCount = analyzer.GetMotivs(minvalue, maxValue);
                }

                Result.graphSize = analyzer.Container.Size;

                InvokeProgressEvent(GraphProgress.AnalizingDone, 95);

            }
            catch (Exception ex)
            {
                InvokeFailureProgressEvent(GraphProgress.AnalizingFailed, ex.Message);
                //RETHROW EXCEPTION
            }
            finally
            {
                InvokeProgressEvent(GraphProgress.Done, 100);
            }
        }

        /// <summary>
        /// Check input generation parameters. 
        /// </summary>
        public abstract bool CheckGenerationParams(int instances);
        /// <summary>
        /// Получение матрицы смежности из контейнера.
        /// </summary>
        public bool[,] GetMatrix()
        {
            return analyzer.Container.GetMatrix();
        }

        /// <summary>
        /// Prints details information of parameters into the form.
        /// </summary>
        public abstract string GetParamsInfo();

        /// <summary>
        /// Create and return object of specific type
        /// </summary>
        public abstract AbstractGraphModel Clone();

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

        /// Triggers analyze start process,
        /// and can be called only in case when 
        /// generation is fully completed
        public void StartAnalize()
        {
            if (CurrentStatus.GraphProgress != GraphProgress.GenerationDone)
            {
                return;
            }
            AnalyzeModel();
        }

        /// Starts generation of graph model for separate 
        /// generation rule and for the first time of 
        /// sequential generation 
        public void StartGenerate()
        {
            if (CurrentStatus.GraphProgress != GraphProgress.Ready)
            {
                return;
            }
            GenerateModel();
        }

        /// Starts generation of graph model for sequential
        /// generation rule starting from second to the last model in the
        /// queue
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
