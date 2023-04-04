using A320_Cockpit.Adaptation.Canbus;
using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Repository.Cockpit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Cockpit
{
    public class SerialBusCockpitRepository : ICockpitRepository
    {

        private ICanbus canbus;

        public SerialBusCockpitRepository(ICanbus canbus)
        {
            this.canbus = canbus;
        }

        public bool IsOpen => canbus.IsOpen;

        public void Close()
        {
            canbus.Close();
        }

        public void Open()
        {
            canbus.Open();
        }

        public void Send(Frame frame)
        {
            canbus.Send(frame);
        }
    }
}
