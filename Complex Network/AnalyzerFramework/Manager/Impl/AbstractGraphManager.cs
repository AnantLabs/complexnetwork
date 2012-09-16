using System;
using System.Collections.Generic;

using RandomGraph.Settings;
using RandomGraph.Common.Model;
using RandomGraph.Common.Storage;
using RandomGraph.Core.Events;
using RandomGraph.Core.Manager.Status;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Result;
using CommonLibrary.Model.Util;
using AnalyzerFramework.Manager.Impl;
using CommonLibrary.Model.Events;

namespace RandomGraph.Core.Manager.Impl
{
    // Базовый абстрактный класс для manager-ов графа.
    public abstract class AbstractGraphManager
    {
        // Конструктор, которому передается хранилище данных, тип генерации и режим трассировки.
        public AbstractGraphManager(IResultStorage storage, Options.GenerationMode genMode, bool tracingMode)
        {
            DataStorage = storage;
            GenMode = genMode;
            TracingMode = tracingMode;

            Assembly = new ResultAssembly();
            GraphTables = new List<GraphTable>();
        }

        // События.

        // Событие, обозначающее изменения статуса manager-а (например, изменение статуса из Stopped в Running).
        public event StatusChangedEventHandler ExecutionStatusChange;
        // ??
        public event GraphsAreGenerated GraphsGenerated;
        // Событие, обозначающее изменение статуса любой реализации.
        // Каждый аргумент этого события имеет статусы всех реализаций, которые ныне выполняются.
        public event GraphProgressEventHandler OverallProgress;

        #region Properties

        // Хранилище данных.
        public IResultStorage DataStorage { get; protected set; }
        // Тип генерации.
        public Options.GenerationMode GenMode { get; protected set; }
        // Тип трассировки (true - трассировка есть).
        public bool TracingMode { get; protected set; }
        // Результат вычислительной работы.
        public ResultAssembly Assembly { get; protected set; }
        // Статус выполнения (например, Stopped, Running, Paused).
        public ExecutionStatus CurrentExecutionStatus { get; protected set; }

        // ??
        protected List<GraphTable> GraphTables { get; set; }
        // ?? List for storing all created instances of graph models
        protected List<AbstractGraphModel> Models { get; set; }

        #endregion

        public abstract void Stop();

        public abstract void Pause();

        public abstract void Continue();

        public abstract void Stop(int instanceID);

        public abstract void Pause(int instanceID);

        public abstract void Continue(int instanceID);

        public abstract void Start(AbstractGraphModel model, int iterations, string name);

        // Защищенная часть.

        protected void OnExecutionStatusChange(ExecutionStatusEventArgs args)
        {
            CurrentExecutionStatus = args.ExecutionStatus;
            if (ExecutionStatusChange != null)
            {
                ExecutionStatusChange(this, args);
            }
        }

        protected void OnOverallProgress(GraphProgressEventArgs args)
        {
            if (OverallProgress != null)
            {
                OverallProgress(this, args);
            }
        }

        protected void invokeOverallProgress(AbstractGraphModel graphModel)
        {
            OnOverallProgress(new GraphProgressEventArgs(graphModel.CurrentStatus));
        }

        protected void OnGraphsGenerated()
        {
            if (GraphsGenerated != null)
            {
                GraphsGenerated(this, GraphTables);
            }
        }
    }
}
