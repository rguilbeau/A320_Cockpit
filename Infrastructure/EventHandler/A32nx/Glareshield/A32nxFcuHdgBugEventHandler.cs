using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.EventHandler.A32nx.Glareshield
{
    public class A32nxFcuHdgBugEventHandler : IPayloadEventHandler
    {
        private readonly MsfsSimulatorRepository msfsSimulatorRepository;

        public A32nxFcuHdgBugEventHandler(MsfsSimulatorRepository msfsSimulatorRepository)
        {
            this.msfsSimulatorRepository = msfsSimulatorRepository;
        }

        public List<CockpitEvent> EventSubscriber => new()
        {
            CockpitEvent.FCU_HEADING_BUG,
            CockpitEvent.FCU_HEADING_PUSH,
            CockpitEvent.FCU_HEADING_PULL
        };


        public void Handle(CockpitEvent cockpitEvent, float value)
        {
            switch(cockpitEvent)
            {
                case CockpitEvent.FCU_HEADING_BUG:
                    msfsSimulatorRepository.Send(value > 0 ? A32nxEvents.FcuHdgIncr : A32nxEvents.FcuHdgDecr);
                    break;
                case CockpitEvent.FCU_HEADING_PUSH:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuHdgPush);
                    break;
                case CockpitEvent.FCU_HEADING_PULL:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuHdgPull);
                    break;
            }
        }
    }
}
