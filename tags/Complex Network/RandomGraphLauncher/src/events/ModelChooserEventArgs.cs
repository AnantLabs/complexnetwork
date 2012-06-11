using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomGraphLauncher.events
{
    public class ModelChooserEventArgs : EventArgs
    {
        public Type ModelFactory
        {
            get;
            protected set;
        }
        public String JobName
        {
            get;
            protected set;
        }
        public ModelChooserEventArgs(Type modelFactory, String jobName)
        {
            ModelFactory = modelFactory;
        }
    }
}
