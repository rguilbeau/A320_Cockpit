using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A320_Cockpit.Adapter.CanBusAdapter;

namespace A320_Cockpit.Domain.CanBus
{
    internal interface ICanBus
    {

        public bool Open();

        public void Close();

        public bool Send(Frame frame);

        public bool IsOpen { get; }

    }
}
