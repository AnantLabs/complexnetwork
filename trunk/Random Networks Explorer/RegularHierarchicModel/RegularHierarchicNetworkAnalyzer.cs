using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Model;
using NetworkModel;

namespace RegularHierarchicModel
{
    class RegularHierarchicNetworkAnalyzer : AbstractNetworkAnalyzer
    {
        // Контейнер, в котором содержится граф конкретной модели (BA).
        private RegularHierarchicNetworkContainer container;

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public override INetworkContainer Container
        {
            get { return container; }
            set { container = (RegularHierarchicNetworkContainer)value; }
        }

        public RegularHierarchicNetworkAnalyzer() { }
    }
}
