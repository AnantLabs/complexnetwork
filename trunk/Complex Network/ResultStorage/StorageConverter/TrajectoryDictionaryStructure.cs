using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ResultStorage.StorageConverter
{
    class TrajectoryDictionaryStructure
    {
        public string fileName = "";
        public int N = 0;
        public double p = 0.0;
        public double mu = 0.0;
        public bool perm = false;
        public List<SortedDictionary<int, double>> dictionaries =
            new List<SortedDictionary<int, double>>();

        public void ReadDirectory(string fullName)
        {
            DirectoryInfo d = new DirectoryInfo(fullName);
            string dictionaryName = d.Name;

            this.fileName = dictionaryName;

            // Получение значение параметра N из имени каталога.
            int i = 1;
            string paramN = "";
            while (dictionaryName[i] != '_')
            {
                paramN += dictionaryName[i];
                ++i;
            }
            this.N = Convert.ToInt32(paramN);

            // Получение значение параметра p из имени каталога.
            i += 2;
            string paramP = "";
            while (dictionaryName[i] != '_')
            {
                paramP += dictionaryName[i];
                ++i;
            }
            this.p = Convert.ToDouble(paramP);

            // Получение значение параметра mu из имени каталога.
            i += 2;
            string paramMu = "";
            while (dictionaryName[i] != '_')
            {
                paramMu += dictionaryName[i];
                ++i;
            }
            this.mu = Convert.ToDouble(paramMu);

            // Получение значение параметра Permanent из имени каталога.
            ++i;
            string paramPerm = dictionaryName.Substring(i);
            this.perm = (paramPerm == "F") ? false : true;

            // Получение пар значений из файлов данного каталога.
            this.dictionaries.Clear();
            FileInfo[] f = d.GetFiles();
            foreach (FileInfo fInfo in f)
            {
                SortedDictionary<int, double> dict = new SortedDictionary<int, double>();
                StreamReader streamReader;
                using (streamReader = new StreamReader(fInfo.FullName, System.Text.Encoding.Default))
                {
                    string contents;
                    while ((contents = streamReader.ReadLine()) != null)
                    {
                        string first = "", second = "";
                        int j = 0;
                        while (contents[j] != ' ')
                        {
                            first += contents[j];
                            ++j;
                        }

                        second = contents.Substring(j);

                        dict.Add(Convert.ToInt32(first), Convert.ToDouble(second));
                    }
                }

                this.dictionaries.Add(dict);
            }
        }
    }
}
