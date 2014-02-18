using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Attributes
{
    /// <summary>
    /// Attribute for GenerationParameter (enum).
    /// FullName - user-friendly name for a Generation Parameter.
    /// Description - extended information about a Generation Parameter.
    /// Type - type of a Generation Parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class GenerationParameterInfo : Attribute
    {
        public GenerationParameterInfo(string fullName, string description, Type type)
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
