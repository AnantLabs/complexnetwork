using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Attributes
{
    /// <summary>
    /// Attribute for ResearchParameter (enum).
    /// FullName - user-friendly name for a Research Parameter.
    /// Description - extended information about a Research Parameter.
    /// Type - type of a Research Parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ResearchParameterInfo : Attribute
    {
        public ResearchParameterInfo(string fullName, string description, Type type)
        {
            FullName = fullName;
            Description = description;
            Type = type;
        }

        public string FullName { get; private set; }
        public string Description { get; private set; }
        public Type Type { get; private set; }
    }
}
