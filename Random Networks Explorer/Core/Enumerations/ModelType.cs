using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Attributes;

namespace Core.Enumerations
{
    /// <summary>
    /// Enumaration, used for indicating the model type for Research.
    /// Uses Attribute AnalyzeTypeInfo for storing metadata about every model.
    /// </summary>
    public enum ModelType
    {
        [ModelTypeInfo("", "", "BAModel")]
        ER = 1,

        [ModelTypeInfo("", "", "ERModel")]
        WS,

        [ModelTypeInfo("", "", "WSModel")]
        BA,

        [ModelTypeInfo("", "", "RegularHierarchicModel")]
        RegularHierarchic,

        [ModelTypeInfo("", "", "NonRegularHierarchicModel")]
        NonRegularHierarchic
    }
}
