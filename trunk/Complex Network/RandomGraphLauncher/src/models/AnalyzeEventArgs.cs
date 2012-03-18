using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model.Result;
using RandomGraph.Common.Model.StatAnalyzer;

namespace RandomGraphLauncher.models
{
    public class AnalyzeEventArgs : EventArgs
    {
        public ResultAssembly Assembly
        {
            get;
            protected set;
        }
        public int InstantceNumber
        {
            get;
            set;
        }
        public AnalyzeEventArgs(ResultAssembly assembly)
        {
            Assembly = assembly;
        }
    }
}
