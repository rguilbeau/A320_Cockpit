using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter
{
    internal class SimVar<T> : IVar<T>
    {

        private readonly string name;

        private T? value;

        public SimVar(string name)
        {
            this.name = name;
            value = default;
        }

        public string Name
        {
            get { return name; }
        }

        public T? Value
        {
            get { return value; }
            set { this.value = value; }
        }

    }
}
