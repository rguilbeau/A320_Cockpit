using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter
{
    internal interface IVar<T>
    {
        public T? Value { get; set; }
    }
}
