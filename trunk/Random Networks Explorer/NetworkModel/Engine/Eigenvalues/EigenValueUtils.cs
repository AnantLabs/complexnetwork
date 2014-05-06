﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using MatrixLibrary;

namespace NetworkModel.Engine.Eigenvalues
{
    public class EigenValueUtils
    {
        private List<Double> eigenValue;

        public EigenValueUtils()
        {
            eigenValue = new List<Double>();
        }

        public List<Double> CalculateEigenValue(bool[,] matrix)
        {
            int size = matrix.GetLength(1);
            var vector = new double[size, size];
            var  vectors = new double[size, size];

            try
            {
                Matrix.Eigen(ConvertToDoubleMatrix(matrix), 
                    out vector, out vectors);
                GetSortEigineValue(vector);
                return eigenValue;
            }
            catch(Exception)
            {
                return new List<Double>();
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

        public double[,] ConvertToDoubleMatrix(bool[,] matrix)
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

        public List<Double> GetSortEigineValue(double[,] vector)
        {
            eigenValue.Clear();
            for (int i = 0; i < vector.Length; ++i)
            {
                if(!eigenValue.Contains(Math.Round(vector[i, 0], 4)))
                {
                    eigenValue.Add(Math.Round(vector[i, 0], 4));
                }
            }

            eigenValue.Sort();

            return eigenValue;
        }

        public SortedDictionary<double, uint> CalcEigenValuesDist(List<double> eigenValues)
        {
            var dist = new List<double>();
            var resultdist = new SortedDictionary<double, uint>();
            for (int i = 0; i < eigenValues.Count - 1; ++i)
            {
                dist.Add(Math.Round((double)eigenValues[i + 1] - (double)eigenValues[i], 4));
            }
           
            for (int i = 0; i < dist.Count; i++)
            {
                if (!resultdist.ContainsKey(dist[i]))
                {
                    resultdist.Add(dist[i], (uint)dist.FindAll(m => m.Equals(dist[i])).Count);
                }
            }

            return resultdist;
        }
    }
}
