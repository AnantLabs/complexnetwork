using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Enumerations;
using Core.Attributes;
using Core.Exceptions;
using Core.Events;

namespace Session
{
    /// <summary>
    /// Research organization and manipulation interface.
    /// </summary>
    public static class SessionManager
    {
        private static Dictionary<Guid, AbstractResearch> existingResearches;

        static SessionManager()
        {
            existingResearches = new Dictionary<Guid, AbstractResearch>();
        }

        /// <summary>
        /// Creates a default research and adds to existingResearches.
        /// </summary>
        /// <param name="researchType">The type of research to create.</param>
        /// <returns>ID of created Research.</returns>
        public static Guid CreateResearch(ResearchType researchType)
        {
            AbstractResearch r = CreateResearchFromType(researchType);
            existingResearches.Add(r.ResearchID, r);
            r.ModelType = GetAvailableModelTypes(r.ResearchID)[0];
            r.ResearchName = "Default";
            r.Storage = CreateStorage(StorageType.XMLStorage, Settings.StorageDirectory);
            r.TracingPath = "";

            return r.ResearchID;
        }

        /// <summary>
        /// Creates a research and adds to existingResearches.
        /// </summary>
        /// <param name="researchType">The type of research to create.</param>
        /// <param name="modelType">The model type of research to create.</param>
        /// <param name="researchName">The name of research.</param>
        /// <param name="storage">The storage type for saving results of analyze.</param>
        /// <param name="storageString">Connection string or file path for data storage.</param>
        /// <param name="tracingPath">Path, if tracing is on, and empty string otherwise.</param>
        /// <returns>ID of created Research.</returns>
        public static Guid CreateResearch(ResearchType researchType,
            ModelType modelType,
            string researchName,
            StorageType storage,
            string storageString,
            GenerationType generationType,
            string tracingPath)
        {
            AbstractResearch r = CreateResearchFromType(researchType);
            existingResearches.Add(r.ResearchID, r);
            r.ModelType = modelType;
            r.ResearchName = researchName;
            r.Storage = CreateStorage(storage, storageString);
            r.GenerationType = generationType;
            r.TracingPath = tracingPath;

            return r.ResearchID;
        }

        /// <summary>
        /// Removes a research from existingResearches, without save.
        /// </summary>
        /// <param name="id">ID of research to destroy.</param>
        public static void DestroyResearch(Guid id)
        {
            try
            {
                existingResearches.Remove(id);
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Clones the specified research.
        /// </summary>
        /// <param name="id">ID of research to clone.</param>
        /// <returns>ID of created Research.</returns>
        public static Guid CloneResearch(Guid id)
        {
            try
            {
                AbstractResearch researchToClone = existingResearches[id];

                AbstractResearch r = CreateResearchFromType(researchToClone.GetResearchType());
                existingResearches.Add(r.ResearchID, r);
                r.ModelType = researchToClone.ModelType;
                r.ResearchName = "Cloned from " + researchToClone.ResearchName;
                r.Storage = CreateStorage(researchToClone.Storage.GetStorageType(),
                    researchToClone.Storage.StorageString);
                r.TracingPath = researchToClone.TracingPath;

                r.RealizationCount = researchToClone.RealizationCount;

                r.ResearchParameterValues = researchToClone.ResearchParameterValues;
                r.GenerationParameterValues = researchToClone.GenerationParameterValues;
                r.AnalyzeOption = researchToClone.AnalyzeOption;

                return r.ResearchID;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Starts a research - Generation, Analyzing, Saving.
        /// </summary>
        /// <param name="id">ID of research to start.</param>
        public static void StartResearch(Guid id)
        {
            try
            {
                if (existingResearches[id].Status == ResearchStatus.NotStarted)
                    existingResearches[id].StartResearch();
                else
                    throw new CoreException("Unable to start the specified research.");
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Stops a research. Results have to be saved.
        /// </summary>
        /// <param name="id">ID of research to stop</param>
        public static void StopResearch(Guid id)
        {
            try
            {
                if (existingResearches[id].Status == ResearchStatus.Running)
                    existingResearches[id].StopResearch();
                else
                    throw new CoreException("Unable to stop the specified research");
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Stops all running researches.
        /// </summary>
        public static void StopAllRunningResearches()
        {
            foreach (Guid id in existingResearches.Keys)
            {
                if (existingResearches[id].Status == ResearchStatus.Running)
                    existingResearches[id].StopResearch();
            }
        }

        /// <summary>
        /// Checks if exists any running research.
        /// </summary>
        /// <returns>'true', if exists. 'false' otherwise.</returns>
        public static bool ExistsAnyRunningResearch()
        {
            if (existingResearches.Count == 0)
                return false;
            else
            {
                foreach (Guid id in existingResearches.Keys)
                {
                    if (existingResearches[id].Status == ResearchStatus.Running)
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Retrieved the type of specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Type of research.</returns>
        public static ResearchType GetResearchType(Guid id)
        {
            try
            {
                return existingResearches[id].GetResearchType();
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets the name of specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Name of research.</returns>
        public static string GetResearchName(Guid id)
        {
            try
            {
                return existingResearches[id].ResearchName;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Sets name for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <param name="modelType">Name to set.</param>
        public static void SetResearchName(Guid id, string researchName)
        {
            try
            {
                existingResearches[id].ResearchName = researchName;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Retrieves available model types for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>List of available model types.</returns>
        public static List<ModelType> GetAvailableModelTypes(Guid id)
        {
            try
            {
                List<ModelType> r = new List<ModelType>();
                AvailableModelType[] rAvailableModelTypes = (AvailableModelType[])existingResearches[id].GetType().GetCustomAttributes(typeof(AvailableModelType), true);
                for (int i = 0; i < rAvailableModelTypes.Length; ++i)
                    r.Add(rAvailableModelTypes[i].ModelType);

                return r;
            }
            catch(KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets model type for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Model type.</returns>
        public static ModelType GetResearchModelType(Guid id)
        {
            try
            {
                return existingResearches[id].ModelType;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Sets model type for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <param name="modelType">Model type to set.</param>
        public static void SetResearchModelType(Guid id, ModelType modelType)
        {
            try
            {
                existingResearches[id].ModelType = modelType;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Retrieves the storage type for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Storage type of research.</returns>
        public static StorageType GetResearchStorageType(Guid id)
        {
            try
            {
                return existingResearches[id].Storage.GetStorageType();
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Retrieves the storage string for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Storage string.</returns>
        public static string GetResearchStorageString(Guid id)
        {
            try
            {
                return existingResearches[id].Storage.StorageString;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Sets storage for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <param name="storage">The storage type for saving results of analyze.</param>
        /// <param name="storageString">Connection string or file path for data storage.</param>
        public static void SetResearchStorage(Guid id, StorageType storageType, string storageString)
        {
            try
            {
                existingResearches[id].Storage = CreateStorage(storageType, storageString);
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets the type of generation for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Generation type.</returns>
        public static GenerationType GetResearchGenerationType(Guid id)
        {
            try
            {
                return existingResearches[id].GenerationType;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Sets the type of generation for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <param name="generationType">Generation type to set.</param>
        public static void SetResearchGenerationType(Guid id, GenerationType generationType)
        {
            try
            {
                existingResearches[id].GenerationType = generationType;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets the tracing path for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Tracing path of research.</returns>
        public static string GetResearchTracingPath(Guid id)
        {
            try
            {
                return existingResearches[id].TracingPath;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Sets tracing path for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <param name="modelType">Path to set. Empty, if tracing is off.</param>
        public static void SetResearchTracingPath(Guid id, string tracingPath)
        {
            try
            {
                existingResearches[id].TracingPath = tracingPath;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets the realization count for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Realization count of research.</returns>
        public static int GetResearchRealizationCount(Guid id)
        {
            try
            {
                return existingResearches[id].RealizationCount;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Sets realization count for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <param name="modelType">Realization count to set. Not less then 1.</param>
        public static void SetResearchRealizationCount(Guid id, int realizationCount)
        {
            try
            {
                existingResearches[id].RealizationCount = realizationCount;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets status for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Status.</returns>
        public static ResearchStatus GetResearchStatus(Guid id)
        {
            try
            {
                return existingResearches[id].Status;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        public static void AddResearchUpdateHandler(Guid id, ResearchStatusUpdateHandler method)
        {
            try
            {
                existingResearches[id].OnUpdateResearchStatus += method;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        public static void RemoveResearchUpdateHandler(Guid id, ResearchStatusUpdateHandler method)
        {
            try
            {
                existingResearches[id].OnUpdateResearchStatus -= method;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NetworkEventArgs[] GetResearchEnsembleStatus(Guid id)
        {
            try
            {
                return existingResearches[id].GetEnsembleStatus();
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        public static void AddResearchEnsembleUpdateHandler(Guid id,
            ResearchEnsembleStatusUpdateHandler method)
        {
            try
            {
                existingResearches[id].OnUpdateResearchEnsembleStatus += method;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        public static void RemoveResearchEnsembleUpdateHandler(Guid id,
            ResearchEnsembleStatusUpdateHandler method)
        {
            try
            {
                existingResearches[id].OnUpdateResearchEnsembleStatus -= method;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets research parameters for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Research parameters with values.</returns>
        public static Dictionary<ResearchParameter, object> GetResearchParameterValues(Guid id)
        {
            try
            {
                return existingResearches[id].ResearchParameterValues;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Sets value of specified research parameter for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <param name="p">Research parameter.</param>
        /// <param name="value">Value to set.</param>
        public static void SetResearchParameterValue(Guid id, 
            ResearchParameter p, 
            object value)
        {
            try
            {
                if (existingResearches[id].Status == ResearchStatus.NotStarted)
                    existingResearches[id].ResearchParameterValues[p] = value;
                else
                    throw new CoreException("Unable to modify research after start.");
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets generation parameters for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Generation parameters with values.</returns>
        public static Dictionary<GenerationParameter, object> GetGenerationParameterValues(Guid id)
        {
            try
            {
                return existingResearches[id].GenerationParameterValues;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Sets value of specified generation parameter for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <param name="p">Generation parameter.</param>
        /// <param name="value">Value to set.</param>
        public static void SetGenerationParameterValue(Guid id, 
            GenerationParameter p, 
            object value)
        {
            try
            {
                if (existingResearches[id].Status == ResearchStatus.NotStarted)
                    existingResearches[id].GenerationParameterValues[p] = value;
                else
                    throw new CoreException("Unable to modify research after start.");
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets available analyze options for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Available analyze options.</returns>
        /// <note>Analyze option is available for research, if it is available 
        /// both for research type and model type.</note>
        public static AnalyzeOption GetAvailableAnalyzeOptions(Guid id)
        {
            try
            {
                AvailableAnalyzeOption rAvailableOptions = ((AvailableAnalyzeOption[])existingResearches[id].GetType().GetCustomAttributes(typeof(AvailableAnalyzeOption), true))[0];

                ModelType t = existingResearches[id].ModelType;
                ModelTypeInfo[] info = (ModelTypeInfo[])t.GetType().GetField(t.ToString()).GetCustomAttributes(typeof(ModelTypeInfo), false);
                Type mt = Type.GetType(info[0].Implementation, true);

                AvailableAnalyzeOption mAvailableOptions = ((AvailableAnalyzeOption[])mt.GetCustomAttributes(typeof(AvailableAnalyzeOption), true))[0];

                return rAvailableOptions.Options & mAvailableOptions.Options;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets analyze options for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Analyze options (flag).</returns>
        public static AnalyzeOption GetAnalyzeOptions(Guid id)
        {
            try
            {
                return existingResearches[id].AnalyzeOption;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Sets analyze options for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <param name="o">Analyze options to set (flag).</param>
        /// <node>param o must be subset from available analyze options.</node>
        public static void SetAnalyzeOptions(Guid id, AnalyzeOption o)
        {
            try
            {
                if (existingResearches[id].Status == ResearchStatus.NotStarted)
                {
                    AnalyzeOption opt = GetAvailableAnalyzeOptions(id);
                    if ((opt | o) != opt)
                        throw new CoreException("Specified option is not available for current research and model type.");
                    else
                        existingResearches[id].AnalyzeOption = o;
                }
                else
                    throw new CoreException("Unable to modify research after start.");
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        #region Utilities

        /// <summary>
        /// Creates a research of specified type using metadata information of enumeration value.
        /// </summary>
        /// <param name="rt">Type of research to create.</param>
        /// <returns>Newly created research.</returns>
        private static AbstractResearch CreateResearchFromType(ResearchType rt)
        {
            ResearchTypeInfo[] info = (ResearchTypeInfo[])rt.GetType().GetField(rt.ToString()).GetCustomAttributes(typeof(ResearchTypeInfo), false);
            Type t = Type.GetType(info[0].Implementation, true);
            return (AbstractResearch)t.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
        }

        /// <summary>
        /// Creates a storage of specified type using metadata information of enumeration value.
        /// </summary>
        /// <param name="st">Type of storage to create.</param>
        /// <param name="storageStr">Connection string or file path for data storage.</param>
        /// <returns>Newly created storage.</returns>
        private static AbstractResultStorage CreateStorage(StorageType st, string storageStr)
        {
            Type[] patametersType = { typeof(String) };
            object[] invokeParameters = { storageStr };
            StorageTypeInfo[] info = (StorageTypeInfo[])st.GetType().GetField(st.ToString()).GetCustomAttributes(typeof(StorageTypeInfo), false);
            Type t = Type.GetType(info[0].Implementation, true);
            return (AbstractResultStorage)t.GetConstructor(patametersType).Invoke(invokeParameters);
        }

        #endregion
    }
}