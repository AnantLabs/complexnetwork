using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;


namespace Algorithms
{
    public class EigenValue
    {
        double[] mArrayOfEigVal;

        public  int isInArray(double[] array, double element)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == element)
                    return i;
            }
            return -1;
        }

        public  Vector CalcEigenValues(Matrix AdjMatrix)
        {
        
                var v = new EigenvalueDecomposition(AdjMatrix);
                return v.RealEigenvalues;
           
           
        }

        public  Matrix GetDMatrix(bool[,] m)
        {
            int size = m.GetLength(1);
            Vector vectorRow;
            IList<Vector> ListOfVector = new List<Vector>();
            double[] vectorEl = new double[size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    vectorEl[j] = Convert.ToDouble(m[i, j]);
                vectorRow = new Vector(vectorEl);
                ListOfVector.Add(vectorRow);
            }

            return Matrix.CreateFromRows(ListOfVector);
            
            //double[,] dm = new double[size, size];
            //for (int j = 0; j < size; j++)
            //{
            //    for (int i = 0; i < size; i++)
            //    {
            //        if (i == j)
            //        {
            //            dm[j, i] = 0;
            //        }
            //        else
            //        {
            //            dm[j, i] = Convert.ToDouble(m[j, i]);
            //        }
            //    }
            //}
            //return Matrix.Create(dm);
        }

        public ArrayList EV(bool[,] ar)
        {
            Matrix m = GetDMatrix(ar);
            Vector EigVal = CalcEigenValues(m);
            mArrayOfEigVal = new double[EigVal.Length];
            for (int i = 0; i < mArrayOfEigVal.Length; ++i)
            {
                if (isInArray(mArrayOfEigVal, Math.Round(EigVal[i], 4)) == -1)
                mArrayOfEigVal[i] = Math.Round(EigVal[i],4);
            }
           Array.Sort(mArrayOfEigVal);
           ArrayList a = new ArrayList();
           for (int i = 0; i < mArrayOfEigVal.Length; ++i)
           {
               a.Add(mArrayOfEigVal[i]);
           }
           return a;
        }
        public SortedDictionary<double, int> CalcEigenValuesDist()
        {
            Array.Sort(mArrayOfEigVal);
            double[] dist = new double[mArrayOfEigVal.Length - 1];
            SortedDictionary<double, int> distr = new SortedDictionary<double, int>();
            for (int i = 0; i < dist.Length; ++i)
            {
                dist[i] = mArrayOfEigVal[i + 1] - mArrayOfEigVal[i];
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
                if(!distr.ContainsKey(array1[i]))
                distr.Add(array1[i], count[i]);
            }
            return distr;
        }
    }
}