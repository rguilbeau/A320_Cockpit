using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adaptation.Msfs.Model.Event
{
    public class KEvent<T>
    {
        private string name;
        private T? value;

        public KEvent(string name)
        {
            this.name = name;
        }

        public string Name { get { return name; } }

        public T? Value => value;
    }

    public class KEventEmpty { }
}
