using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Repository.Payload.FakeA320.Glareshield;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.EventHandler.A32nx.Glareshield
{
    public class A32nxFcuGlareshieldButtonsEventHandler : IPayloadEventHandler
    {
        private readonly MsfsSimulatorRepository msfsSimulatorRepository;

        public A32nxFcuGlareshieldButtonsEventHandler(MsfsSimulatorRepository msfsSimulatorRepository)
        {
            this.msfsSimulatorRepository = msfsSimulatorRepository;
        }

        public List<CockpitEvent> EventSubscriber => new()
        {
            CockpitEvent.FCU_LOC,
            CockpitEvent.FCU_SPEED_MACH,
            CockpitEvent.FCU_VS_FPA,
            CockpitEvent.FCU_METRICT_ALT
        };

        public void Handle(CockpitEvent cockpitEvent, float value)
        {
            switch(cockpitEvent)
            {
                case CockpitEvent.FCU_LOC:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuLocPush);
                    break;
                case CockpitEvent.FCU_SPEED_MACH:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuSpdMachTogglePush);
                    break;
                case CockpitEvent.FCU_VS_FPA:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuTrkFpaTogglePush);
                    break;
                case CockpitEvent.FCU_METRICT_ALT:
                    //todo: event non trouvé
                    break;

            }
        }
    }
}
