using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Core.Exceptions;
using Core.Model;
using Core.Settings;

namespace Core.Utility
{
    /// <summary>
    /// Specialized functions for file system operations.
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static MatrixInfoToRead Read(String filePath)
        {
            MatrixInfoToRead result = new MatrixInfoToRead();

            result.Matrix = MatrixReader(filePath);
            result.Branches = BranchesReader(filePath.Insert(filePath.Length - 4, "_branches"));

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrixInfo"></param>
        /// <param name="filePath"></param>
        public static void Write(MatrixInfoToWrite matrixInfo, String filePath)
        {
            String directoryPath = ExplorerSettings.TracingDirectory;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            MatrixWriter(matrixInfo.Matrix, filePath);
            if(matrixInfo.Branches != null)
                BranchesWriter(matrixInfo.Branches, filePath);
        }

        public static ArrayList MatrixReader(String filePath)
        {
            ArrayList matrix;
            try
            {
                if (!TryReadClassicalMatrix(filePath, out matrix))
                {
                    if (!TryReadExtendedMatrix(filePath, out matrix))
                        throw new SystemException("Unknown matrix format.");
                }
            }
            catch (SystemException ex)
            {
                throw new CoreException(ex.Message);
            }

            return matrix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static bool TryReadClassicalMatrix(String filePath, out ArrayList matrix)
        {
            matrix = new ArrayList();
            using (StreamReader streamreader = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                string contents;
                while ((contents = streamreader.ReadLine()) != null)
                {
                    if (Char.IsLetter(contents[0]))
                        return false;

                    string[] split = System.Text.RegularExpressions.Regex.Split(contents,
                        "\\s+", System.Text.RegularExpressions.RegexOptions.None);
                    ArrayList tmp = new ArrayList();
                    foreach (string s in split)
                    {
                        if (s.Equals("0"))
                        {
                            tmp.Add(false);
                        }
                        else if (s.Equals("1"))
                        {
                            tmp.Add(true);
                        }
                    }
                    matrix.Add(tmp);
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static bool TryReadExtendedMatrix(String filePath, out ArrayList matrix)
        {
            matrix = new ArrayList();

            int size = 0;
            bool[,] n;
            using (StreamReader streamreader = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                string contents;
                // TODO check model name
                // retrieving model name
                if (!(contents = streamreader.ReadLine()).Contains("ERModel"))
                    return false;

                // retrieving size
                while (!(contents = streamreader.ReadLine()).Contains("-"))
                {
                    if (contents.Contains("Vertices"))
                        size = Convert.ToInt32(contents.Substring(contents.IndexOf('=') + 1));
                }

                n = new bool[size, size];
                // retrieving data
                while ((contents = streamreader.ReadLine()) != null)
                {
                    string[] split = System.Text.RegularExpressions.Regex.Split(contents,
                        "\\s+",
                        System.Text.RegularExpressions.RegexOptions.None);

                    int i = Convert.ToInt32(split[0]), j = Convert.ToInt32(split[1]);
                    n[i, j] = true;
                    n[j, i] = true;
                }
            }

            for (int i = 0; i < size; ++i)
            {
                ArrayList tmp = new ArrayList();
                for (int j = 0; j < size; ++j)
                {
                    tmp.Add(n[i, j]);
                }
                matrix.Add(tmp);
            }
            
            return true;
        }

        public static void MatrixWriter(bool[,] matrix, String filePath)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath + ".txt"))
            {
                for (int i = 0; i < matrix.GetLength(0); ++i)
                {
                    for (int j = 0; j < matrix.GetLength(1); ++j)
                    {
                        if (matrix[i, j])
                        {
                            file.Write(1 + " ");
                        }
                        else
                        {
                            file.Write(0 + " ");
                        }
                    }
                    file.WriteLine("");
                }
            }
        }

        public static ArrayList BranchesReader(String filePath)
        {
            if (File.Exists(filePath))
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
                                if (s != "")
                                    tmp.Add(UInt16.Parse(s));
                            }
                            branches.Add(tmp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new CoreException(ex.Message);
                }

                return branches;
            }
            else return null;
        }

        public static void BranchesWriter(UInt16[][] branches, String filePath)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath + "_branches.txt"))
            {
                for (int i = 0; i < branches.Length; i++)
                {
                    for (int k = 0; k < branches[i].Length; k++)
                    {
                        writer.Write(branches[i][k]);
                        writer.Write(" ");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
