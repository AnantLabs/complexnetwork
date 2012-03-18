using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomGraph.Core.Exceptions
{
    class TaskExecutionException : Exception
    {
        public TaskExecutionException(String message)
            : base(message)
        { }

        public TaskExecutionException(String message, Exception inner)
            : base(message, inner)
        { }
    }
}
