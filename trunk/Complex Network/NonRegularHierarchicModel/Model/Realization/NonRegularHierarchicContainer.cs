using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CommonLibrary.Model;
using log4net;

namespace Model.NonRegularHierarchicModel.Realization
{
    // Реализация контейнера (Block-Hierarchic Non Regular).
    public class NonRegularHierarchicContainer : IGraphContainer
    {
        // Организация pаботы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(NonRegularHierarchicContainer));

        // Иерархияеская основа (простое число).
        private int branchIndex = 0;
        // Иерархический уровень (максимальный).
        private int level = 0;
        private int[][] branches;
        private const int ARRAY_MAX_SIZE = 2000000000;
        // Иерархическое дерево (специфическое).
        private BitArray[][] treeMatrix;

        // Конструктор по умолчанию для контейнера.
        public NonRegularHierarchicContainer()
        {
            log.Info("Creating HierarchicContainer default object.");
            treeMatrix = new BitArray[0][];
            branches = new int[0][];
        }

        public int BranchIndex
        {
            get { return branchIndex; }
            set { branchIndex = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public BitArray[][] TreeMatrix
        {
            set { treeMatrix = value; }
        }

        public int[][] Branches
        {
            get { return branches; }
            set { branches = value; }
        }
    }
}
