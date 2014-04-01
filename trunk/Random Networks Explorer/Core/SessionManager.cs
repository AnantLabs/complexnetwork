using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Attributes;
using Core.Exceptions;

namespace Core
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
            string tracingPath)
        {
            AbstractResearch r = CreateResearchFromType(researchType);
            r.ModelType = modelType;
            r.ResearchName = researchName;
            r.Storage = CreateStorage(storage, storageString);
            r.TracingPath = tracingPath;

            existingResearches.Add(r.ResearchID, r);
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
        /// Starts a research - Generation, Analyzing, Saving.
        /// </summary>
        /// <param name="id">ID of research to start.</param>
        public static void StartResearch(Guid id)
        {
            try
            {
                if (existingResearches[id].Status == Status.NotStarted)
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
                if (existingResearches[id].Status == Status.Running)
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
                if (existingResearches[id].Status == Status.NotStarted)
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
                if (existingResearches[id].Status == Status.NotStarted)
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
                AvailableAnalyzeOption mAvailableOptions = ((AvailableAnalyzeOption[])existingResearches[id].ModelType.GetType().GetCustomAttributes(typeof(AvailableAnalyzeOption), false))[0];

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
                if (existingResearches[id].Status == Status.NotStarted)
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

        /// <summary>
        /// Creates a research of specified type using metadata information of enumeration value.
        /// </summary>
        /// <param name="rt">Type of research to create.</param>
        /// <returns>Newly created research.</returns>
        private static AbstractResearch CreateResearchFromType(ResearchType rt)
        {
            ResearchTypeInfo[] info = (ResearchTypeInfo[])rt.GetType().GetCustomAttributes(typeof(ResearchTypeInfo), false);
            Type t = Type.GetType(info[0].Implementation);
            return (AbstractResearch)t.GetConstructor(null).Invoke(null);
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
            StorageTypeInfo[] info = (StorageTypeInfo[])st.GetType().GetCustomAttributes(typeof(StorageTypeInfo), false);
            Type t = Type.GetType(info[0].Implementation);
            return (AbstractResultStorage)t.GetConstructor(patametersType).Invoke(invokeParameters);
        }
    }
}