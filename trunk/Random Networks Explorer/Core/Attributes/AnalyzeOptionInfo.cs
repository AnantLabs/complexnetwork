using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public enum OptionType
    {
        Global,
        ValueList,
        Distribution
    }

    /// <summary>
    /// Attribute for AnalyzeOption (enum).
    /// FullName - user-friendly name for an Analyze Option.
    /// Description - extended information about an Analyze Option.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AnalyzeOptionInfo : Attribute
    {
        public AnalyzeOptionInfo(string fullName, 
            string description,
            OptionType optionType,
            Type resultType)
        {
            FullName = fullName;
            Description = description;
            OptionType = optionType;
            ResultType = resultType;
        }

        public AnalyzeOptionInfo(string fullName,
            string description,
            OptionType optionType,
            Type resultType,
            string xAxisName,
            string yAxisName)
            : this(fullName, description, optionType, resultType)
        {
            XAxisName = xAxisName;
            YAxixName = yAxisName;
        }

        public string FullName { get; private set; }
        public string Description { get; private set; }
        public OptionType OptionType { get; private set; }
        public Type ResultType { get; private set; }
        public string XAxisName { get; private set; }
        public string YAxixName { get; private set; }
    }
}
