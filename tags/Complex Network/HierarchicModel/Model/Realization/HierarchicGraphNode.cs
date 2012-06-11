using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.HierarchicModel.Realization
{
    public class node
    {
        public BitArray data; // Tree node data
        public node[] childrenPointers; // Pointer array of node children
        /// <summary>
        /// node constructor
        /// </summary>
        /// <param name="childCount"></param>
        public node(int childCount)
        {
            int lenght = (childCount - 1) * childCount / 2;
            data = new BitArray(lenght, false);
            childrenPointers = new node[childCount];
        }
       
        //return count bloks which connected to i blok 
        public int this[int i,bool b]
        {
            get
            {
                i++;
                int s = 1, sum = 0;
                int findex = 0;
                while (s < i) 
                {
                    sum += this[findex + i - s - 1];
                    findex += this.childrenPointers.Length - s;
                    s++;
                }

                int endindex = findex + (this.childrenPointers.Length - s);
                s = findex;
                   
                while (s < endindex)
                {
                    sum += this[s];
                    s++;
                }
                
                return sum;
            }
        }
        /// <summary>
        /// Returns true if vertex1 connected to vertex2
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <returns></returns>
        public bool this[int vertex1, int vertex2]
        {
            get
            {
                if (vertex1 == vertex2)
                {
                   return false;
                }
                // vertex1 must have min number
                if (vertex1 > vertex2)
                {
                    int k = vertex2;
                    vertex2 = vertex1;
                    vertex1 = k;
                }
                // Get the index of two vertexes adjacent value
                int index = 0;
                for (int k = 1; k <= vertex1; k++)
                {
                    index += this.childrenPointers.Length - k;
                }
                index += vertex2 - vertex1 - 1;
                if (this.data[index])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Returns value of data[i]
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int this[int i]
        {
            get
            {
                if (i < (this.childrenPointers.Length - 1) * this.childrenPointers.Length / 2)
                {
                    return Convert.ToInt32(this.data[i]);
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
