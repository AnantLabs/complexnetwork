using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Enumerations;
using Core.Exceptions;
using Core.Attributes;
using Core.Result;
using Core.Settings;

namespace Session
{
    public static class StSessionManager
    {
        private static Dictionary<Guid, ResearchResult> existingResults;
        private static Dictionary<int, List<Guid>> existingResultsByGroups;
        private static AbstractResultStorage storage;

        static StSessionManager()
        {
            existingResults = new Dictionary<Guid, ResearchResult>();
            existingResultsByGroups = new Dictionary<int, List<Guid>>();
            storage = CreateStorage();
            RefreshExistingResults();
        }

        /// <summary>
        /// Refreshes existing results repository.
        /// <note>Sorts existing results by groups also.</note>
        /// </summary>
        public static void RefreshExistingResults()
        {
            existingResults.Clear();
            List<ResearchResult> results = storage.LoadAllResearchInfo();
            foreach (ResearchResult r in results)
            {
                existingResults.Add(r.ResearchID, r);
            }

            SortExistingResultsByGroups();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteResearch(Guid id)
        {
            storage.Delete(id);
            RefreshExistingResults();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetResearchName(Guid id)
        {
            try
            {
                return existingResults[id].ResearchName;
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
        public static int GetResearchRealizationCount(Guid id)
        {
            try
            {
                return existingResults[id].RealizationCount;
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
        public static uint GetResearchNetworkSize(Guid id)
        {
            try
            {
                return existingResults[id].Size;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        public static DateTime GetResearchDate(Guid id)
        {
            try
            {
                return existingResults[id].Date;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets research parameter values for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Research parameters with values.</returns>
        public static Dictionary<ResearchParameter, object> GetResearchParameterValues(Guid id)
        {
            try
            {
                return existingResults[id].ResearchParameterValues;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Gets generation parameter values for specified research.
        /// </summary>
        /// <param name="id">ID of research.</param>
        /// <returns>Generation parameters with values.</returns>
        public static Dictionary<GenerationParameter, object> GetGenerationParameterValues(Guid id)
        {
            try
            {
                return existingResults[id].GenerationParameterValues;
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// Loads ensemble results for specified research.
        /// </summary>
        /// <param name="id">ID of research</param>
        /// <note>Method checks if result is not loaded yet.</note>
        public static void LoadResearchResult(Guid id)
        {
            try
            {
                if (existingResults[id].EnsembleResults == null)
                    existingResults[id] = storage.Load(id);
            }
            catch (KeyNotFoundException)
            {
                throw new CoreException("Specified research does not exists.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, List<Guid>> GetResultsByGroup()
        {
            return existingResultsByGroups;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="mt"></param>
        /// <returns></returns>
        public static Dictionary<int, List<Guid>> GetFilteredResultsByGroups(ResearchType rt, ModelType mt)
        {
            Dictionary<int, List<Guid>> result = new Dictionary<int, List<Guid>>();

            foreach (int i in existingResultsByGroups.Keys)
            {
                Guid id = existingResultsByGroups[i].First();
                if (existingResults[id].ResearchType == rt && existingResults[id].ModelType == mt)
                    result.Add(i, existingResultsByGroups[i]);
            }

            return result;
        }

        /// <summary>
        /// Creates a storage of specified type using metadata information of enumeration value.
        /// </summary>
        /// <param name="st">Type of storage to create.</param>
        /// <param name="storageStr">Connection string or file path for data storage.</param>
        /// <returns>Newly created storage.</returns>
        private static AbstractResultStorage CreateStorage()
        {
            StorageType st = StatisticAnalyzerSettings.StorageType;
            string storageStr = null;
            switch (st)
            {
                case StorageType.XMLStorage:
                    storageStr = StatisticAnalyzerSettings.XMLStorageDirectory;
                    break;
                case StorageType.TXTStorage:
                    storageStr = StatisticAnalyzerSettings.TXTStorageDirectory;
                    break;
                case StorageType.ExcelStorage:
                    storageStr = StatisticAnalyzerSettings.ExcelStorageDirectory;
                    break;
                default:
                    break;
            }

            Type[] patametersType = { typeof(String) };
            object[] invokeParameters = { storageStr };
            StorageTypeInfo[] info = (StorageTypeInfo[])st.GetType().GetField(st.ToString()).GetCustomAttributes(typeof(StorageTypeInfo), false);
            Type t = Type.GetType(info[0].Implementation, true);
            return (AbstractResultStorage)t.GetConstructor(patametersType).Invoke(invokeParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void SortExistingResultsByGroups()
        {
            existingResultsByGroups.Clear();
            List<ResearchResult> temp = storage.LoadAllResearchInfo();
            int k = 0;
            while (temp.Count != 0)
            {
                List<Guid> current = new List<Guid>();
                ResearchResult currentResult = temp[0];
                current.Add(currentResult.ResearchID);
                temp.Remove(currentResult);
                int i = 0;
                while(i < temp.Count())
                {
                    if (temp[i].ModelType == currentResult.ModelType &&
                        temp[i].ResearchType == currentResult.ResearchType &&
                        temp[i].Size == currentResult.Size &&
                        AreParametersCompatible(temp[i], currentResult))
                    {
                        current.Add(temp[i].ResearchID);
                        temp.Remove(temp[i]);
                    }
                    else
                    {
                        ++i;
                    }
                }
                existingResultsByGroups.Add(k, current);
                ++k;
            }

            return;
        }

        /// <summary>
        /// Checks if specified results are compatible for statistic analyze.
        /// </summary>
        /// <param name="r1">First result.</param>
        /// <param name="r2">Second result</param>
        /// <returns>True, if results are compatible. False otherwise</returns>
        /// <note>Research result must have same researchType and modelType.</note>
        private static bool AreParametersCompatible(ResearchResult r1, ResearchResult r2)
        {
            foreach (GenerationParameter gp in r1.GenerationParameterValues.Keys)
            {
                if (r1.GenerationParameterValues[gp].ToString() != r2.GenerationParameterValues[gp].ToString())
                    return false;
            }

            foreach (ResearchParameter rp in r1.ResearchParameterValues.Keys)
            {
                if (r1.ResearchParameterValues[rp].ToString() != r2.ResearchParameterValues[rp].ToString())
                    return false;
            }

            return true;
        }
    }
}
