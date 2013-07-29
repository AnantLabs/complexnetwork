using System;
using System.Collections;
using System.Collections.Generic;

namespace CommonLibrary.Model
{
    // Базовый абстрактный класс для имплементации контейнера графа.
    public abstract class AbstractGraphContainer
    {        
        // Размер контейнера (число вершин в графе).
        public abstract int Size { get; set; }

        // Строится граф на основе матрицы смежности.
        public abstract void SetMatrix(string fileName);

        // Возвращается матрица смежности, соответсвующая графу.
        public abstract bool[,] GetMatrix();

        // ??
        public abstract int[][] GetBranches();
    }
}