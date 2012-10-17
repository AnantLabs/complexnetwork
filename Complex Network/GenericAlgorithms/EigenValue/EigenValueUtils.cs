using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MatrixLibrary;
using log4net;

namespace Algorithms
{
    public class EigenValueUtils
    {
        protected static readonly new ILog log = log4net.LogManager.GetLogger(typeof(EigenValueUtils));
        private ArrayList eigenValue;

        public ArrayList CalculateEigenValue(bool[,] matrix)
        {
            int size = matrix.GetLength(1);
            var vector = new double[size, size];
            var  vectors = new double[size, size];

            try
            {
                MatrixLibrary.Matrix.Eigen(ConvertToDoubleMatrix(matrix), out vector, out vectors);
                GetSortEigineValue(vector);
                return eigenValue;

            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
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

        public  ArrayList GetSortEigineValue(double[,] vector)
        {
            eigenValue = new ArrayList();
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
        public SortedDictionary<double, int> CalcEigenValuesDist()
        {
            var  dist = new List<double>();
            var  rezultdist = new SortedDictionary<double, int>();
            for (int i = 0; i < eigenValue.Count - 1; ++i)
            {
                dist.Add(Math.Round((double)eigenValue[i + 1] - (double)eigenValue[i],4));
            }
           
            for (int i = 0; i < dist.Count; i++)
            {
                if (!rezultdist.ContainsKey(dist[i]))
                {
                    rezultdist.Add(dist[i], dist.FindAll(m => m.Equals(dist[i])).Count);
                }
            }
          
            return rezultdist;
        }
    }
}
