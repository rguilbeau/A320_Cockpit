using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Repository.Payload.FakeA320.Glareshield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.EventHandler.FakeA320.Glareshield
{
    public class FakeA320FcuGlareshieldButtonsEventHandler : IPayloadEventHandler
    {
        private readonly FakeA320GlareshieldIndicatorsRepository glareshieldIndicatorsRepository;
        private readonly FakeA320FcuDisplayRepository fcuDisplayRepository;

        public FakeA320FcuGlareshieldButtonsEventHandler(
            FakeA320GlareshieldIndicatorsRepository glareshieldIndicatorsRepository,
            FakeA320FcuDisplayRepository fcuDisplayRepository
        ) {
            this.glareshieldIndicatorsRepository = glareshieldIndicatorsRepository;
            this.fcuDisplayRepository = fcuDisplayRepository;
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
            GlareshieldIndicators glareshieldIndicators = glareshieldIndicatorsRepository.Payload;
            FcuDisplay fcuDisplay = fcuDisplayRepository.Payload;

            switch(cockpitEvent)
            {
                case CockpitEvent.FCU_LOC:
                    glareshieldIndicators.FcuLoc = !glareshieldIndicators.FcuLoc;
                    break;
                case CockpitEvent.FCU_SPEED_MACH:
                    fcuDisplay.IsMach = !fcuDisplay.IsMach;
                    if(fcuDisplay.IsMach)
                    {
                        fcuDisplay.Speed = 0.85;
                    } else
                    {
                        fcuDisplay.Speed = 250;
                    }
                    break;
                case CockpitEvent.FCU_VS_FPA:
                    fcuDisplay.IsFpa = !fcuDisplay.IsFpa;
                    break;
                case CockpitEvent.FCU_METRICT_ALT:
                    break;

            }

            glareshieldIndicatorsRepository.AskRefresh = true;
        }
    }
}
