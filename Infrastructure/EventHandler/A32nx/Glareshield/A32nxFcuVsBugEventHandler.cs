using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.EventHandler.A32nx.Glareshield
{
    public class A32nxFcuVsBugEventHandler : IPayloadEventHandler
    {
        private readonly MsfsSimulatorRepository msfsSimulatorRepository;

        public A32nxFcuVsBugEventHandler(MsfsSimulatorRepository msfsSimulatorRepository)
        {
            this.msfsSimulatorRepository = msfsSimulatorRepository;
        }

        public List<CockpitEvent> EventSubscriber => new()
        {
            CockpitEvent.FCU_VS_BUG,
            CockpitEvent.FCU_VS_PUSH,
            CockpitEvent.FCU_VS_PULL
        };

        public void Handle(CockpitEvent cockpitEvent, float value)
        {
            switch(cockpitEvent)
            {
                case CockpitEvent.FCU_VS_BUG:
                    msfsSimulatorRepository.Send(value > 0 ? A32nxEvents.FcuVsIncr : A32nxEvents.FcuVsDecr);
                    break;
                case CockpitEvent.FCU_VS_PUSH:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuVsPush);
                    break;
                case CockpitEvent.FCU_VS_PULL:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuVsPull);
                    break;
            }
        }
    }
}
