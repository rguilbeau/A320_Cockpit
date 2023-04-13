using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adaptation.Msfs.Model.Event
{
    public class HEvent
    {
        private readonly string name;

        public HEvent(string name)
        {
            this.name = name;
        }

        public string Name { get { return name; } }
    }
}
