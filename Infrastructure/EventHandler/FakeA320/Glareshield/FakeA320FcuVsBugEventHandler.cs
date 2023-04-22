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
    public class FakeA320FcuVsBugEventHandler : IPayloadEventHandler
    {

        private readonly FakeA320FcuDisplayRepository fcuDisplayRepository;

        public FakeA320FcuVsBugEventHandler(FakeA320FcuDisplayRepository fcuDisplayRepository)
        {
            this.fcuDisplayRepository = fcuDisplayRepository;
        }

        public List<CockpitEvent> EventSubscriber => new()
        {
            CockpitEvent.FCU_VS_BUG,
            CockpitEvent.FCU_VS_PUSH,
            CockpitEvent.FCU_VS_PULL
        };

        public void Handle(CockpitEvent cockpitEvent, float value)
        {
            FcuDisplay fcuDisplay = fcuDisplayRepository.Payload;

            switch(cockpitEvent)
            {
                case CockpitEvent.FCU_VS_BUG:
                    double incr = fcuDisplay.IsFpa ? 0.1 : 100;
                    fcuDisplay.VerticalSpeed += value > 0 ? incr : -incr;
                    break;
                case CockpitEvent.FCU_VS_PUSH:
                    fcuDisplay.IsVerticalSpeedDash = true;
                    break;
                case CockpitEvent.FCU_VS_PULL:
                    fcuDisplay.IsVerticalSpeedDash = false;
                    break;
            }

            fcuDisplayRepository.AskRefresh = true;
        }
    }
}
