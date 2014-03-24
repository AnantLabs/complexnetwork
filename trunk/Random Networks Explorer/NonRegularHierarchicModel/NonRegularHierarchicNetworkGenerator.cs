using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;

namespace NonRegularHierarchicModel
{
    class NonRegularHierarchicNetworkGenerator : INetworkGenerator
    {
        private NonRegularHierarchicNetworkContainer container;

        public INetworkContainer Container
        {
            get { return container; }
            set { container = (NonRegularHierarchicNetworkContainer)value; }
        }

        public NonRegularHierarchicNetworkGenerator() { }

        public void RandomGeneration(Dictionary<GenerationParameter, object> genParam)
        {
        }

        public void StaticGeneration(ArrayList matrix)
        {
        }
    }
}
