using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;

namespace Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public interface INetworkGenerator
    {
        // Контейнер, в котором содержится сгенерированный граф.
        INetworkContainer Container { get; set; }

        void RandomGeneration(Dictionary<GenerationParameter, object> genParam);
        void StaticGeneration(ArrayList matrix);
    }
}
