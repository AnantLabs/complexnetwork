using System;
using System.Collections;
using System.Collections.Generic;

using RandomGraph.Common.Model.Generation;

namespace CommonLibrary.Model
{
    // Базовый абстрактный класс для имплементации генератора графа.
    public abstract class AbstractGraphGenerator
    {
        // Контейнер, в котором содержится сгенерированный граф.
        public abstract AbstractGraphContainer Container { get; set; }

        // Permanet Generation
        public abstract void PermanentGeneration(Dictionary<GenerationParam, object> genParam);

        // Случайным образом генерируется граф, на основе параметров генерации.
        public void Generation(Dictionary<GenerationParam, object> genParam)
        {
            if (genParam.ContainsKey(GenerationParam.FileName))
            {
                // Статическая генерация.
                StaticGeneration((string)genParam[GenerationParam.FileName]);                
            }
            else
            {
                // Динамическая генерация.
                RandomGeneration(genParam);
            }
        }

        protected abstract void RandomGeneration(Dictionary<GenerationParam, object> genParam);
        protected abstract void StaticGeneration(string filePath);
    }
}