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
        private static List<List<ResearchResult>> existingResults;
        private static AbstractResultStorage storage;

        static StSessionManager()
        {
            existingResults = new List<List<ResearchResult>>();
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
    }
}
