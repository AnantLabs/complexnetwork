using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CoreException : ApplicationException
    {
        public CoreException() { }
        public CoreException(string message) : base(message) { }
        public CoreException(string message, Exception inner) : base(message, inner) { }
        public CoreException(SerializationInfo info, StreamingContext context) :
            base(info, context) { }
    }
}
