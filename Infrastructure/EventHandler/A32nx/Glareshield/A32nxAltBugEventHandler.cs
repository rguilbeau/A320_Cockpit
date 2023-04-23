using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.EventHandler.A32nx.Glareshield
{
    public class A32nxAltBugEventHandler : IPayloadEventHandler
    {

        private readonly MsfsSimulatorRepository msfsSimulatorRepository;

        public A32nxAltBugEventHandler(MsfsSimulatorRepository msfsSimulatorRepository)
        {
            this.msfsSimulatorRepository = msfsSimulatorRepository;
        }

        public List<CockpitEvent> EventSubscriber => new()
        {
            CockpitEvent.FCU_ALTITUDE_BUG,
            CockpitEvent.FCU_ALTITUDE_PUSH,
            CockpitEvent.FCU_ALTITUDE_PULL,
            CockpitEvent.FCU_ALTITUDE_STEP
        };

        public void Handle(CockpitEvent cockpitEvent, float value)
        {
            switch(cockpitEvent)
            {
                case CockpitEvent.FCU_ALTITUDE_BUG:
                    msfsSimulatorRepository.Send(value > 0 ? A32nxEvents.FcuAltIncr : A32nxEvents.FcuAltDecr);
                    break;
                case CockpitEvent.FCU_ALTITUDE_PUSH:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuAltPush);
                    break;
                case CockpitEvent.FCU_ALTITUDE_PULL:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuAltPull);
                    break;
                case CockpitEvent.FCU_ALTITUDE_STEP:
                    A32nxEvents.FcuAltIncr.Value = (short)(value > 0 ? 100 : 1000);
                    A32nxEvents.FcuAltDecr.Value = (short)(value > 0 ? 100 : 1000);
                    break;
            }
        }
    }
}
