using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace GenericAlgorithms
{
    public static class MatrixFileReader
    {
        public static Dictionary<int, List<int>> WormReader(String filePath)
        {
            Dictionary<int, List<int>> negList = new Dictionary<int, List<int>>();
            try
            {
                {
                    StreamReader streamReader;
                    using (streamReader = new StreamReader(filePath, System.Text.Encoding.Default))
                    {
                        string contents;
                        while ((contents = streamReader.ReadLine()) != null)
                        {
                            string[] first = System.Text.RegularExpressions.Regex.Split(contents, ",", System.Text.RegularExpressions.RegexOptions.None);
                            string[] seconde = System.Text.RegularExpressions.Regex.Split(first[0], ";", System.Text.RegularExpressions.RegexOptions.None);
                            if (negList.ContainsKey(Convert.ToInt32(first[0])))
                            {
                                negList[Convert.ToInt32(first[0])].Add(0);
                            }
                            else
                            {
                                List<int> tmp = new List<int>();
                                tmp.Add(Convert.ToInt32(seconde[0]));
                                negList.Add(Convert.ToInt32(first[0]), tmp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
            return negList;
        }

        public static int DetectFileType(String filePath)
        {
            try
            {
                StreamReader streamreader;
                    using (streamreader = new StreamReader(filePath, System.Text.Encoding.Default))
                    {
                        string contents;
                        if ((contents = streamreader.ReadLine()) != null)
                        {
                            string[] split = System.Text.RegularExpressions.Regex.Split(contents, ",", System.Text.RegularExpressions.RegexOptions.None);
                            if (split.Length == 2)
                            {
                                return  2;
                            }
                            else
                            {
                                return 1;
                            }
                        }
                        return -1;
                    }
                }
            catch (Exception ex)
            {
                return -1;
                //MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                //mMode = -1;
            }

        }

        public static ArrayList MatrixReader(String filePath)
        {
            ArrayList matrix = new ArrayList();
            try
            {
                StreamReader streamreader;
                    using (streamreader = new StreamReader(filePath, System.Text.Encoding.Default))
                    {
                        string contents;
                        while ((contents = streamreader.ReadLine()) != null)
                        {
                            string[] split = System.Text.RegularExpressions.Regex.Split(contents, "\\s+", System.Text.RegularExpressions.RegexOptions.None);
                            ArrayList tmp = new ArrayList();
                            foreach (string s in split)
                            {
                                if (s.Equals("0"))
                                {
                                    tmp.Add(false);
                                }
                                else
                                {
                                    tmp.Add(true);
                                }
                            }
                            matrix.Add(tmp);
                        }
                    }
                }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
            return matrix;
        }

        public static ArrayList BranchesReader(String filePath)
        {
            ArrayList branches = new ArrayList();
            try
            {
                using (StreamReader streamreader = 
                    new StreamReader(filePath, System.Text.Encoding.Default))
                {
                    string contents;
                    while ((contents = streamreader.ReadLine()) != null)
                    {
                        string[] split = System.Text.RegularExpressions.Regex.Split(contents, 
                            "\\s+", System.Text.RegularExpressions.RegexOptions.None);
                        ArrayList tmp = new ArrayList();
                        foreach (string s in split)
                        {
                            if(s != "")
                                tmp.Add(Int32.Parse(s));
                        }
                        branches.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
            }
            return branches;
        }

        public static List<KeyValuePair<int, int>> NeighbourshipsReader(string filePath)
        {
            // the first is size
            List<KeyValuePair<int, int>> neighbourship = new List<KeyValuePair<int, int>>();

            StreamReader streamreader;
            using (streamreader = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                string contents;
                // retrieving size
                while (!(contents = streamreader.ReadLine()).Contains("-"))
                {
                    if (contents.Contains("N"))
                    {
                        string genParamName = contents.Substring(0, contents.IndexOf('='));
                        string genParamValue = contents.Substring(contents.IndexOf('=') + 1);

                        neighbourship.Add(new KeyValuePair<int, int>(Convert.ToInt32(-1),
                            Convert.ToInt32(genParamValue)));
                    }
                }

                // retrieving data
                while ((contents = streamreader.ReadLine()) != null)
                {
                    string[] split = System.Text.RegularExpressions.Regex.Split(contents, 
                        "\\s+", 
                        System.Text.RegularExpressions.RegexOptions.None);

                    neighbourship.Add(new KeyValuePair<int, int>(Convert.ToInt32(split[0]), 
                        Convert.ToInt32(split[1])));
                }
            }
     
            return neighbourship;
        }
    }
}
