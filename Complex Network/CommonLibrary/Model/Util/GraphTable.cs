using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Model.Util
{
    public class GraphTable
    {
        public GraphTable() { }

        public GraphTable(bool[,] matrix)
        {
            this.Matrix = matrix;
        }

        public bool[,] Matrix { get; set;}
    }
}
