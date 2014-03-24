using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Attributes;

namespace Core.Enumerations
{
    /// <summary>
    /// Enumaration, used for indicating the model type for Research.
    /// Uses Attribute ModelTypeInfo for storing metadata about every model.
    /// </summary>
    public enum ModelType
    {
        [ModelTypeInfo("Erdős-Rényi model", 
            "The classical random network.", 
            "ERNetwork")]
        ER = 1,

        [ModelTypeInfo("Watts-Strogatz model",
            "Random network, which represents the small-world property.", 
            "WSNetwork")]
        WS,

        [ModelTypeInfo("Baraba´si-Albert model",
            "Random network, which represents the scale-free property.", 
            "BANetwork")]
        BA,

        [ModelTypeInfo("Regular Hierarchical model", 
            "Random regularly branching block-hierarchical network.", 
            "RegularHierarchicNetwork")]
        RegularHierarchic,

        [ModelTypeInfo("Non Regular Hierarchical model", 
            "Random non-regularly branching block-hierarchical network.", 
            "NonRegularHierarchicNetwork")]
        NonRegularHierarchic
    }
}
