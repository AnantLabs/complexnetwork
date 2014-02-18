using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Exceptions
{
    class CoreException : SystemException
    {
        public CoreException(string message) : base(message) { }
    }
}
