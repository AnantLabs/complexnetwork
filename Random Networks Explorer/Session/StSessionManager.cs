using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Enumerations;
using Core.Attributes;
using Core.Result;
using Core.Settings;

namespace Session
{
    public static class StSessionManager
    {
        public static List<List<ResearchResult>> existingResultsByGroups;
        private static AbstractResultStorage storage;

        static StSessionManager()
        {
            existingResultsByGroups = new List<List<ResearchResult>>();
            storage = CreateStorage();
        }

        public static void RefreshExistingResults()
        {
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

        public static void SortByGroups()
        {
            List<ResearchResult> existingResults = storage.LoadAllResearchInfo();

            while (existingResults.Count != 0)
            {
                List<ResearchResult> current = new List<ResearchResult>();
                ResearchResult currentResult = existingResults[0];
                current.Add(currentResult);
                existingResults.Remove(currentResult);
                int i = 0;
                while(i < existingResults.Count())
                {
                    if (existingResults[i].ModelType == currentResult.ModelType &&
                        existingResults[i].ResearchType == currentResult.ResearchType &&
                        existingResults[i].Size == currentResult.Size &&
                        SameParameters(existingResults[i], currentResult))
                    {
                        current.Add(existingResults[i]);
                        existingResults.Remove(existingResults[i]);
                    }
                    else
                    {
                        ++i;
                    }
                }
                existingResultsByGroups.Add(current);
            }

            return;
        }

        private static bool SameParameters(ResearchResult r1, ResearchResult r2)
        {
            // entadrum enq, te researchner@ nuyn model type-i ev nuyn research type-i en
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
