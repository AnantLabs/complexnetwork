using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Attributes
{
    /// <summary>
    /// Attribute for AnalyzeOption (enum).
    /// FullName - user-friendly name for an Analyze Option.
    /// Description - extended information about an Analyze Option.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AnalyzeOptionInfo : Attribute
    {
        public AnalyzeOptionInfo(string fullName, string description)
        {
            FullName = fullName;
            Description = description;
        }

        public string FullName { get; private set; }
        public string Description { get; private set; }
    }
}
