using A320_Cockpit.Adaptation.Msfs.Model.Event;
using A320_Cockpit.Adaptation.Msfs.Model.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adaptation.Msfs.FakeMsfs
{
    public class FakeMsfs : IMsfs
    {
        public bool IsOpen => true;

        public void Close() {}

        public void Open() {}

        public void Read<T>(Lvar<T> variable) {}

        public void Read<T>(SimVar<T> variable) {}

        public void ResumeRead() {}

        public void Send<T>(KEvent<T> kEvent) {}

        public void Send(HEvent hEvent) {}

        public void StartTransaction() {}

        public void StopRead() {}

        public void StopTransaction() {}
    }
}
