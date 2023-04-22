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
    public class FakeA320FcuAltBugEventHandler : IPayloadEventHandler
    {
        private readonly FakeA320FcuDisplayRepository fcuDisplayRepository;

        private int incrStep;

        public FakeA320FcuAltBugEventHandler(FakeA320FcuDisplayRepository fcuDisplayRepository) 
        {
            this.fcuDisplayRepository = fcuDisplayRepository;
            incrStep = 1000;
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
            FcuDisplay fcuDisplay = fcuDisplayRepository.Payload;

            switch (cockpitEvent)
            {
                case CockpitEvent.FCU_ALTITUDE_BUG:
                    fcuDisplay.Altitude += value > 0 ? incrStep : -incrStep;
                    break;
                case CockpitEvent.FCU_ALTITUDE_PUSH:
                    fcuDisplay.IsAltitudeDot = true;
                    break;
                case CockpitEvent.FCU_ALTITUDE_PULL:
                    fcuDisplay.IsAltitudeDot = false;
                    break;
                case CockpitEvent.FCU_ALTITUDE_STEP:
                    incrStep = value > 0 ? 100 : 1000;
                    break;
            }

            fcuDisplayRepository.AskRefresh = true;
        }
    }
}
