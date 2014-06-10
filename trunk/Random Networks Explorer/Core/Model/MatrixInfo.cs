using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public struct MatrixInfoToRead
    {
        public ArrayList Matrix;
        public ArrayList Branches;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct MatrixInfoToWrite
    {
        public bool[,] Matrix;
        public UInt16[][] Branches;
    }
}
