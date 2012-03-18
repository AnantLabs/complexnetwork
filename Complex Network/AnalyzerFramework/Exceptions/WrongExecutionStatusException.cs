using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomGraph.Core.Exceptions
{
    class WrongExecutionStatusException : Exception
    {
        public WrongExecutionStatusException(String message)
            :base(message)
        { }
    }
}
