using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model.Generation;

namespace CommonLibrary.Model.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class RequiredGenerationParam :System.Attribute
    {
        private GenerationParam param;
        public RequiredGenerationParam(GenerationParam param, int index)
        {
            this.param = param;
            this.Index = index;
        }
        public GenerationParam GenParam
        {
            get
            {
                return param;
            }
            private set
            {
                param = value;
            }
        }
        public int Index { get; set; }
    }
}
