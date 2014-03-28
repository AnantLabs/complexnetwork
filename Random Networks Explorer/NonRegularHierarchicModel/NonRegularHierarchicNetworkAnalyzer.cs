using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Model;
using NetworkModel;

namespace NonRegularHierarchicModel
{
    class NonRegularHierarchicNetworkAnalyzer : AbstractNetworkAnalyzer
    {
        // Контейнер, в котором содержится граф конкретной модели (BA).
        private NonRegularHierarchicNetworkContainer container;

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public override INetworkContainer Container
        {
            get { return container; }
            set { container = (NonRegularHierarchicNetworkContainer)value; }
        }

        public NonRegularHierarchicNetworkAnalyzer() { }
    }
}
