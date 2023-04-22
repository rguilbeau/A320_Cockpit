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
    public class FakeA320FcuHdgBugEventHandler : IPayloadEventHandler
    {
        private readonly FakeA320FcuDisplayRepository fcuDisplayRepository;

        public FakeA320FcuHdgBugEventHandler(FakeA320FcuDisplayRepository fcuDisplayRepository)
        {
            this.fcuDisplayRepository = fcuDisplayRepository;
        }

        public List<CockpitEvent> EventSubscriber => new()
        {
            CockpitEvent.FCU_HEADING_BUG,
            CockpitEvent.FCU_HEADING_PUSH,
            CockpitEvent.FCU_HEADING_PULL
        };

        public void Handle(CockpitEvent cockpitEvent, float value)
        {
            FcuDisplay fcuDisplay = fcuDisplayRepository.Payload;

            switch (cockpitEvent)
            {
                case CockpitEvent.FCU_HEADING_BUG:
                    fcuDisplay.Heading += (short)(value > 0 ? 1 : -1);
                    break;
                case CockpitEvent.FCU_HEADING_PUSH:
                    fcuDisplay.IsHeadingDash = true;
                    fcuDisplay.IsHeadingDot = true;
                    break;
                case CockpitEvent.FCU_HEADING_PULL:
                    fcuDisplay.IsHeadingDash = false;
                    fcuDisplay.IsHeadingDot = false;
                    break;
            }

            fcuDisplayRepository.AskRefresh = true;
        }
    }
}
