using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using CommonLibrary.Model.Result;
using RandomGraph.Common.Storage;
using ResultStorage.Storage;

namespace ResultStorage.StorageConverter
{
    // Реализация конвертирования информации из входных файлов (Analyze Results File) в БД.
    public class ResultsFileConverter
    {
        private DictionaryStructure dictStruct = new DictionaryStructure();
        // Имя корневого каталога, в котором находятся подкаталоги (job-ы) с информацией.
        private string rootPath;
        // Список сборок для конвертации.
        private List<ResultAssembly> assembliesToConvert = new List<ResultAssembly>(); 

        // Конструктор, который получает имя корневого каталога.
        public ResultsFileConverter(string path)
        {
            rootPath = path;
        }

        // Чтение корневого каталога, 
        // последовательная обработка всех подкаталогов, 
        // получение сборок для конвертации.
        public void ReadRootDirectory()
        {
            DirectoryInfo parentDir = new DirectoryInfo(this.rootPath);
            foreach (DirectoryInfo dir in parentDir.GetDirectories())
            {
                dictStruct.ReadDirectory(dir.FullName);
                this.assembliesToConvert.Add(dictStruct.Result);
            }
        }

        // Сохранение всех сборок в БД.
        public void Save(SQLResultStorage storage)
        {
            foreach (ResultAssembly assembly in assembliesToConvert)
            {
                storage.Save(assembly);
            }
        }
    }
}
