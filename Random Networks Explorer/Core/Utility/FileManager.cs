using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Core.Enumerations;
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
        /// Reads matrix and branches (if exists) from specified file.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <param name="networkSize">The size of the network (matrix, which represents the network).</param>
        /// <param name="matrixType">The type of given matrix (content of the specified file)</param>
        /// <returns>Matrix and branches (if exists).</returns>
        /// <throws>CoreException, MatrixFormatException.</throws>
        public static MatrixInfoToRead Read(String filePath, int networkSize, AdjacencyMatrixType matrixType)
        {
            MatrixInfoToRead result = new MatrixInfoToRead();

            result.Matrix = MatrixReader(filePath, networkSize, matrixType);
            result.Branches = BranchesReader(filePath.Insert(filePath.Length - 4, "_branches"));

            return result;
        }

        /// <summary>
        /// Writes matrix and branches (if exists) to specified file.
        /// </summary>
        /// <param name="matrixInfo">Matrix and branches (if exists).</param>
        /// <param name="filePath">File path.</param>
        public static void Write(MatrixInfoToWrite matrixInfo, String filePath)
        {
            String directoryPath = ExplorerSettings.TracingDirectory;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            MatrixWriter(matrixInfo.Matrix, filePath);
            if(matrixInfo.Branches != null)
                BranchesWriter(matrixInfo.Branches, filePath);
        }

        private static ArrayList MatrixReader(String filePath, int networkSize, AdjacencyMatrixType matrixType)
        {
            ArrayList matrix;

            bool r = false;
            try
            {
                switch (matrixType)
                {
                    case AdjacencyMatrixType.ClassicalMatrix:
                        r = TryReadClassicalMatrix(filePath, networkSize, out matrix);
                        break;
                    case AdjacencyMatrixType.Degrees:
                        r = TryReadDegreesMatrix(filePath, networkSize, "\\s+", out matrix);
                        break;
                    case AdjacencyMatrixType.CSV:
                        r = TryReadDegreesMatrix(filePath, networkSize, ",", out matrix);
                        break;
                    default:
                        throw new CoreException("Unknown matrix type.");
                }
            }
            catch (SystemException)
            {
                throw new MatrixFormatException();
            }

            if (!r)
                throw new MatrixFormatException();

            return matrix;
        }

        private static bool TryReadClassicalMatrix(String filePath, int networkSize, out ArrayList matrix)
        {
            matrix = new ArrayList();
            using (StreamReader streamreader = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                string contents;
                while ((contents = streamreader.ReadLine()) != null)
                {
                    string[] split = System.Text.RegularExpressions.Regex.Split(contents,
                        "\\s+", System.Text.RegularExpressions.RegexOptions.None);
                    ArrayList tmp = new ArrayList();
                    foreach (string s in split)
                    {
                        if (s.Equals("0"))
                            tmp.Add(false);
                        else if (s.Equals("1"))
                            tmp.Add(true);
                        else return false;
                    }
                    matrix.Add(tmp);
                }
            }

            if (networkSize != matrix.Count)
                return false;

            return true;
        }

        private static bool TryReadDegreesMatrix(String filePath, int networkSize, string saparator, out ArrayList matrix)
        {
            matrix = new ArrayList();

            bool[,] n = new bool[networkSize, networkSize];
            using (StreamReader streamreader = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                string contents;
                while ((contents = streamreader.ReadLine()) != null)
                {
                    string[] split = System.Text.RegularExpressions.Regex.Split(contents,
                        saparator,
                        System.Text.RegularExpressions.RegexOptions.None);

                    try
                    {
                        int i = Convert.ToInt32(split[0]), j = Convert.ToInt32(split[1]);
                        n[i, j] = true;
                        n[j, i] = true;
                    }
                    catch(SystemException)
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < networkSize; ++i)
            {
                ArrayList tmp = new ArrayList();
                for (int j = 0; j < networkSize; ++j)
                {
                    tmp.Add(n[i, j]);
                }
                matrix.Add(tmp);
            }

            if (networkSize != matrix.Count)
                return false;

            return true;
        }

        private static void MatrixWriter(bool[,] matrix, String filePath)
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

        private static ArrayList BranchesReader(String filePath)
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
                catch (SystemException ex)
                {
                    throw new BranchesFormatException();
                }

                return branches;
            }
            else return null;
        }

        private static void BranchesWriter(UInt16[][] branches, String filePath)
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
