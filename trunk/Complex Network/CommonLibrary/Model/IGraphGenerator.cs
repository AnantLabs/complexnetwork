using System;
using System.Collections;
using System.Collections.Generic;

using RandomGraph.Common.Model.Generation;

namespace CommonLibrary.Model
{
    // Общий интерфейс для генератора графа.
    // Каждий генератор любой модели должен реализовать этот интерфейс.
    public interface IGraphGenerator
    {
        // Контейнер, в котором содержится сгенерированный граф.
        IGraphContainer Container { get; set; }

        // Случайным образом генерируется граф, на основе параметров генерации.
        void RandomGeneration(Dictionary<GenerationParam, object> genParam);

        //Permanet Generation
        void PermanentGeneration(Dictionary<GenerationParam, object> genParam);

        // Строится граф, на основе матрицы смежности.
        void StaticGeneration(ArrayList matrix);
    }
}