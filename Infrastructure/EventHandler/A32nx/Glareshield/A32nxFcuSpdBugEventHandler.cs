using A320_Cockpit.Adaptation.Msfs.Model.Event;
using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Simulator;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.EventHandler.A32nx.Glareshield
{
    /// <summary>
    /// Evenement liée au bouton rotatif de la vitesse
    /// </summary>
    public class A32nxFcuSpdBugEventHandler : IPayloadEventHandler
    {
        private readonly MsfsSimulatorRepository msfsSimulatorRepository;

        public A32nxFcuSpdBugEventHandler(MsfsSimulatorRepository msfsSimulatorRepository) 
        {
            this.msfsSimulatorRepository = msfsSimulatorRepository;
        }

        /// <summary>
        /// Les évenements à écouter
        /// </summary>
        public List<CockpitEvent> EventSubscriber => new() 
        { 
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
            switch(e)
            {
                case CockpitEvent.FCU_SPEED_BUG:
                    msfsSimulatorRepository.Send(value > 0 ? A32nxEvents.FcuSpeedIncr : A32nxEvents.FcuSpeedDecr);
                    break;
                case CockpitEvent.FCU_SPEED_PUSH:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuSpeedPush);
                    break;
                case CockpitEvent.FCU_SPEED_PULL:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuSpeedPull);
                    break;

            }
        }
    }
}
