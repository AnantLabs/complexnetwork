using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ResultStorage.StorageConverter
{
    public class ResultsFileConverter: AbstractStorageConverter
    {
        DictionaryStructure dictStruct = new DictionaryStructure();

        public ResultsFileConverter(string path) 
            : base(path) { }

        public override void ReadRootDirectory()
        {
            DirectoryInfo parentDir = new DirectoryInfo(this.rootPath);
            foreach (DirectoryInfo dir in parentDir.GetDirectories())
            {
                dictStruct.ReadDirectory(dir.FullName);
                this.assembliesToConvert.Add(dictStruct.Result);
            }
        }
    }
}
