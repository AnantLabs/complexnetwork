using System;
namespace BAModel.Model.Realization
{
    interface IBAGenerator
    {
        void Generate();
        global::Model.BAModel.Realization.BAGraph Graph { get; }
        bool[] MakeGenerationStep(double[] probabilityArray);
    }
}
