using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Model.Settings
{
    public class ApplicationMode
    {
        public static bool IsTrainingMode { get; set; }
        public static bool IsTracingMode { get; set; }
    }
    // <Mikayel Samvelyan>
    public enum GenerationMode
    {
        randomGeneration,
        staticGeneration
    }
    // </Mikayel Samvelyan>
}
