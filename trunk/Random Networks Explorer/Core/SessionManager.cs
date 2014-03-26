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
        /// Creates a research and adds to existingResearches
        /// </summary>
        /// <returns>ID of created Research</returns>
        public static Guid CreateResearch(ResearchType researchType,
            ModelType modelType,
            string researchName,
            StorageType storage,
            string tracingPath)
        {
            Guid id = Guid.NewGuid();

            AbstractResearch r = CreateResearchFromType(researchType);
            r.ModelType = modelType;
            r.ResearchName = researchName;
            // TODO get storageString
            r.Storage = CreateStorage(storage, "temporaryString");
            r.TracingPath = tracingPath;

            existingResearches.Add(id, r);
            return id;
        }

        /// <summary>
        /// Removes a research from existingResearches.
        /// </summary>
        /// <param name="id">ID of research to destroy</param>
        public static void DestroyResearch(Guid id)
        {
            try
            {
                existingResearches.Remove(id);
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not existst.");
            }
        }

        /// <summary>
        /// Starts a research - Generation, Analyzing, Saving.
        /// </summary>
        /// <param name="id">ID of research to start</param>
        public static void StartResearch(Guid id)
        {
            try
            {
                if (existingResearches[id].Status == Status.NotStarted)
                    existingResearches[id].StartResearch();
                else
                    throw new CoreException("Unable to start the specified research");
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not existst.");
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
                throw new CoreException("Specified research does not existst.");
            }
        }

        // Getting Info from GUI //

        /// <summary>
        /// Gets research parameters for specified research.
        /// </summary>
        /// <param name="id">ID of research needed</param>
        /// <returns></returns>
        public static Dictionary<ResearchParameter, object> GetResearchParameters(Guid id)
        {
            return new Dictionary<ResearchParameter, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        /// <param name="value"></param>
        public static void SetResearchParameterValue(Guid id, ResearchParameter p, object value)
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
                throw new CoreException("Specified research does not existst.");
            }
        }

        /// <summary>
        /// Gets generation parameters for specified research.
        /// </summary>
        /// <param name="id">ID of research needed</param>
        /// <returns></returns>
        public static Dictionary<GenerationParameter, object> GetGenerationParameters(Guid id)
        {
            return new Dictionary<GenerationParameter, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        /// <param name="value"></param>
        public static void SetGenerationParameterValue(Guid id, GenerationParameter p, object value)
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
                throw new CoreException("Specified research does not existst.");
            }
        }

        /// <summary>
        /// Gets available analyze options for specified research.
        /// </summary>
        /// <param name="id">ID of research needed</param>
        /// <returns></returns>
        public static AnalyzeOption GetAnalyzeOptions(Guid id)
        {
            return AnalyzeOption.None;
        }

        // think about //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="o"></param>
        public static void SetAnalyzeOption(Guid id, AnalyzeOption o)
        {
            try
            {
                if (existingResearches[id].Status == Status.NotStarted)
                    existingResearches[id].AnalyzeOption = o;
                else
                    throw new CoreException("Unable to modify research after start.");
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not existst.");
            }
        }

        ///////////////////////////

        /// <summary>
        /// Creates a research of specified type using metadata information of enumeration value.
        /// </summary>
        /// <param name="rt">Type of research to create</param>
        /// <returns>Newly created research</returns>
        private static AbstractResearch CreateResearchFromType(ResearchType rt)
        {
            ResearchTypeInfo[] info = (ResearchTypeInfo[])rt.GetType().GetCustomAttributes(typeof(ResearchTypeInfo), false);
            Type t = Type.GetType(info[0].Implementation);
            return (AbstractResearch)t.GetConstructor(null).Invoke(null);
        }

        /// <summary>
        /// Creates a storage of specified type using metadata information of enumeration value.
        /// </summary>
        /// <param name="st">Type of storage to create</param>
        /// <param name="storageStr"></param>
        /// <returns>Newly created storage</returns>
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