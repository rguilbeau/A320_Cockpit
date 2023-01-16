using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter
{
    public interface IMsfsConnector
    {

        public bool IsOpen { get; }

        public void Open();

        public void Close();

        public T? Read<T>(IVar<T> variable);

    }
}
