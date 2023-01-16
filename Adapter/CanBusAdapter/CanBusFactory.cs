using A320_Cockpit.Adapter.CanBusAdapter.SerialCanBusAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.CanBusAdapter
{
    public static class CanBusFactory
    {

        private static ICanBusAdapter? instance;

        public static ICanBusAdapter Get()
        {
            if (instance == null)
            {
                instance = new SerialCan(new System.IO.Ports.SerialPort(), "COM5", 9600, "125Kbit");
            }

            return instance;
        }
    }
}
