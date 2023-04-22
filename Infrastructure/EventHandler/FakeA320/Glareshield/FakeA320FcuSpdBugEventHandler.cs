using A320_Cockpit.Adaptation.Msfs.Model.Event;
using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Simulator;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield;
using A320_Cockpit.Infrastructure.Repository.Payload.FakeA320.Glareshield;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.EventHandler.FakeA320.Glareshield
{
    /// <summary>
    /// Evenement liée au bouton rotatif de la vitesse
    /// </summary>
    public class FakeA320FcuSpdBugEventHandler : IPayloadEventHandler
    {
        private readonly FakeA320FcuDisplayRepository fcuDisplayRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fcuDisplayRepository"></param>
        public FakeA320FcuSpdBugEventHandler(FakeA320FcuDisplayRepository fcuDisplayRepository)
        {
            this.fcuDisplayRepository = fcuDisplayRepository;
        }

        /// <summary>
        /// Les évenements à écouter
        /// </summary>
        public List<CockpitEvent> EventSubscriber => new() { 
            CockpitEvent.FCU_SPEED_BUG, 
            CockpitEvent.FCU_SPEED_PUSH, 
            CockpitEvent.FCU_SPEED_PULL
        };

        /// <summary>
        /// Gestion de l'évenement
        /// </summary>
        /// <param name="frame"></param>
        public void Handle(CockpitEvent e, float value)
        {
            FcuDisplay fcuDisplay = fcuDisplayRepository.Payload;
            switch(e)
            {
                case CockpitEvent.FCU_SPEED_BUG:
                    double incr = fcuDisplay.IsMach ? 0.1 : 1;
                    fcuDisplay.Speed += value > 0 ? incr : -incr;
                    break;
                case CockpitEvent.FCU_SPEED_PUSH:
                    fcuDisplay.IsSpeedDash = true;
                    fcuDisplay.IsSpeedDot = true;
                    break;
                case CockpitEvent.FCU_SPEED_PULL:
                    fcuDisplay.IsSpeedDash = false;
                    fcuDisplay.IsSpeedDot = false;
                    break;
            }
            
            fcuDisplayRepository.AskRefresh = true;
        }
    }
}
