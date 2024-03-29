﻿using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.Repository.Glareshield;
using A320_Cockpit.Infrastructure.Cockpit.EventHandlerDispatcher;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.EventHandler.Glareshield
{
    /// <summary>
    /// Gestion des evenements liées aux boutons du Glareshield (pour le debug)
    /// </summary>
    public class FakeA320FcuGlareshieldButtonsEventHandler : IPayloadEventHandler
    {
        private readonly FakeA320GlareshieldIndicatorsRepository glareshieldIndicatorsRepository;
        private readonly FakeA320FcuDisplayRepository fcuDisplayRepository;

        /// <summary>
        /// Création du gestionnaire d'evenement
        /// </summary>
        /// <param name="glareshieldIndicatorsRepository"></param>
        /// <param name="fcuDisplayRepository"></param>
        public FakeA320FcuGlareshieldButtonsEventHandler(
            FakeA320GlareshieldIndicatorsRepository glareshieldIndicatorsRepository,
            FakeA320FcuDisplayRepository fcuDisplayRepository
        )
        {
            this.glareshieldIndicatorsRepository = glareshieldIndicatorsRepository;
            this.fcuDisplayRepository = fcuDisplayRepository;
        }

        /// <summary>
        /// La liste des évenements du cockpit en écoute pour ce gestionnaire
        /// </summary>
        public List<CockpitEvent> EventSubscriber => new()
        {
            CockpitEvent.FCU_AP1,
            CockpitEvent.FCU_AP2,
            CockpitEvent.FCU_ATHR,
            CockpitEvent.FCU_LOC,
            CockpitEvent.FCU_APPR,
            CockpitEvent.FCU_EXPED,
            CockpitEvent.FCU_SPEED_MACH,
            CockpitEvent.FCU_VS_FPA,
            CockpitEvent.FCU_METRICT_ALT,
        };

        /// <summary>
        /// Gestion de l'event
        /// </summary>
        /// <param name="cockpitEvent"></param>
        /// <param name="value"></param>
        public void Handle(CockpitEvent cockpitEvent, float value)
        {
            GlareshieldIndicators glareshieldIndicators = glareshieldIndicatorsRepository.Payload;
            FcuDisplay fcuDisplay = fcuDisplayRepository.Payload;

            switch (cockpitEvent)
            {
                case CockpitEvent.FCU_AP1:
                    glareshieldIndicators.FcuAp1 = !glareshieldIndicators.FcuAp1;
                    break;
                case CockpitEvent.FCU_AP2:
                    glareshieldIndicators.FcuAp2 = !glareshieldIndicators.FcuAp2;
                    break;
                case CockpitEvent.FCU_ATHR:
                    glareshieldIndicators.FcuAthr = !glareshieldIndicators.FcuAthr;
                    break;
                case CockpitEvent.FCU_LOC:
                    glareshieldIndicators.FcuLoc = !glareshieldIndicators.FcuLoc;
                    break;
                case CockpitEvent.FCU_APPR:
                    glareshieldIndicators.FcuAppr = !glareshieldIndicators.FcuAppr;
                    break;
                case CockpitEvent.FCU_EXPED:
                    glareshieldIndicators.FcuExped = !glareshieldIndicators.FcuExped;
                    break;
                case CockpitEvent.FCU_SPEED_MACH:
                    fcuDisplay.IsMach = !fcuDisplay.IsMach;
                    if (fcuDisplay.IsMach)
                    {
                        fcuDisplay.Speed = 0.85;
                    }
                    else
                    {
                        fcuDisplay.Speed = 250;
                    }
                    break;
                case CockpitEvent.FCU_VS_FPA:
                    fcuDisplay.IsFpa = !fcuDisplay.IsFpa;
                    fcuDisplay.IsTrack = !fcuDisplay.IsTrack;
                    break;
                case CockpitEvent.FCU_METRICT_ALT:
                    break;

            }

            glareshieldIndicatorsRepository.AskRefresh = true;
        }
    }
}
