using System;

namespace RandomGraph.Common.Model.Generation
{
    // Перечисление (enumeration) функций для вычисления вероятности появления соединений
    // на этапе генерации графа (сети).
    // Только для иерархической модели (исследование критических вероятностей).
    public enum GenerationProbability
    {
        Constant,
        Classical,
        Logarithmical
    }
}