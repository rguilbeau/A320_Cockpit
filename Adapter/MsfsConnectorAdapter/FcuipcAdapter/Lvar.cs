using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter.FcuipcAdapter
{
    internal class Lvar<T> : IVar<T>
    {

        private readonly string name;

        private T? value;

        public Lvar(string name)
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
