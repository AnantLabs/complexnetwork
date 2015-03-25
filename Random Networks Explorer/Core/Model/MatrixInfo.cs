using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// Representation of adjacency matrix and branches of the network read from file.
    /// <note>Branches property is null, if the network is not hierarchical.</note>
    /// </summary>
    public struct MatrixInfoToRead
    {
        public ArrayList Matrix;
        public ArrayList Branches;
    }

    /// <summary>
    /// Representation of adjacency matrix and branches of the network to be written to file.
    /// <note>Branches property is null, if the network is not hierarchical.</note>
    /// </summary>
    public struct MatrixInfoToWrite
    {
        public bool[,] Matrix;
        public UInt16[][] Branches;
    }
}
