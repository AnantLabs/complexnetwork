using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MatrixLibrary;

namespace BAModel.Model.Realization.EigenValue
{
    class EigenValueForBA
    {
        private double[] EigenValue { get; set; }


        public ArrayList CalculateEigenValue(bool[,] matrix)
        {
            int size = matrix.GetLength(1);
            double[,] dm = EigenValueForBA.ConvertToDoubleMatrix(matrix);
            double[,] vector = new double[size, size];
            double[,] vectors = new double[size, size];
            try
            {
                MatrixLibrary.Matrix.Eigen(dm, out vector, out vectors);
                return GetSortEigineValue(vector);

            }
            catch
            {
                return new ArrayList();
            }
        }
        private static int isInArray(double[] array, double element)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == element)
                    return i;
            }
            return -1;
        }

        public static double[,] ConvertToDoubleMatrix(bool[,] matrix)
        {
            int size = matrix.GetLength(1);
            double[,] convertMatrix = new double[size, size];
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    if (i == j)
                    {
                        convertMatrix[j, i] = 0;
                    }
                    else
                    {
                        convertMatrix[j, i] = Convert.ToDouble(matrix[j, i]);
                    }
                }
            }

            return convertMatrix;
        }

        public  ArrayList GetSortEigineValue(double[,] vector)
        {
            EigenValue = new double[vector.Length];
            for (int i = 0; i < EigenValue.Length; ++i)
            {
                if (isInArray(EigenValue, Math.Round(vector[i, 0], 4)) == -1)
                    EigenValue[i] = Math.Round(vector[i, 0], 4);
            }
            Array.Sort(EigenValue);
            ArrayList a = new ArrayList();
            for (int i = 0; i < EigenValue.Length; ++i)
            {
                a.Add(EigenValue[i]);
            }
            return a;
        }
        public SortedDictionary<double, int> CalcEigenValuesDist()
        {
            Array.Sort(EigenValue);
            double[] dist = new double[EigenValue.Length - 1];
            SortedDictionary<double, int> distr = new SortedDictionary<double, int>();
            for (int i = 0; i < dist.Length; ++i)
            {
                dist[i] = EigenValue[i + 1] - EigenValue[i];
                dist[i] = Math.Round(dist[i], 4);
            }
            double[] array1 = new double[dist.Length];
            int[] count = new int[dist.Length];
            for (int i = 0, j = 0; i < dist.Length; ++i, ++j)
            {
                if (isInArray(array1, dist[i]) == -1)
                {
                    array1[j] = dist[i];
                    count[j]++;
                }
                else
                {
                    count[isInArray(array1, dist[i])]++;
                    j--;
                }
            }
            for (int i = 0; i < dist.Length - 1; ++i)
            {
                if (!distr.ContainsKey(array1[i]))
                    distr.Add(array1[i], count[i]);
            }
            return distr;
        }
    }
}
