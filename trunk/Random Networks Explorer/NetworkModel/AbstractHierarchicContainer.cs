using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Model;

namespace NetworkModel
{
    /// <summary>
    /// Abstract class presenting container of hierarchic type.
    /// </summary>
    public abstract class AbstractHierarchicContainer : INetworkContainer
    {
        public abstract int Size { get; set; }

        public abstract void SetMatrix(ArrayList matrix);
        public abstract bool[,] GetMatrix();

        /// <summary>
        /// Gets branches for the graph.
        /// </summary>
        /// <returns>Branches by levels.</returns>
        public abstract int[][] GetBranches();
    }
}
