using System;

namespace Model.HierarchicModel.Realization
{
    // Делегат для вычисления вероятности появления соединений на этапе генерации графа.
    public delegate double ProbabilityCounter(Int16 branchingIndex, Int16 level, double mu);

    // Класс для реализации функций вычисления вероятности появления соединений.
    public class ProbabilityFunctions
    {
        // Возвращает переданную вероятность без изменений. 
        // Не зависит от параметров генерации иерархической модели.
        public static double Constant(Int16 p1, Int16 p2, double probability)
        {
            return probability;
        }

        // Возвращает значение вероятности, вычисленной по классической формуле.
        // Зависит от идекса ветвления, данного номера уровня и параметра mu иерархияеской модели.
        public static double Classical(Int16 branchingIndex, Int16 level, double mu)
        {
            return 1 / Math.Pow(branchingIndex, level * mu);
        }

        /*public static double Logarithmical(Int16 branchingIndex, Int16 level, double mu)
        {
            return Math.Log(branchingIndex, level * mu);
        }*/
    }
}