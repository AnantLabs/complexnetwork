using System;
using System.Collections;
using System.Collections.Generic;

namespace CommonLibrary.Model
{
    // Общий интерфейс для контейнера графа.
    // Каждий контейнер графа любой модели должен реализовать этот интерфейс.
    public interface IGraphContainer
    {
        
        // Размер контейнера (число вершин в графе).
        int Size { get; set; }

        // Строится граф на основе матрицы смежности.
        void SetMatrix(ArrayList matrix);
        // Возвращается матрица смежности, соответсвующая графу.
        bool[,] GetMatrix();
    }
}