using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Attributes;

namespace Core.Enumerations
{
    /// <summary>
    /// Enumaration, used for indicating the result storage type for Research.
    /// Uses Attribute StorageTypeInfo for storing metadata about every storage.
    /// </summary>
    public enum StorageType
    {
        [StorageTypeInfo("", "XMLResultStorage")]
        XMLStorage = 1,

        [StorageTypeInfo("", "SQLResultStorage")]
        SQLStorage
    }
}
