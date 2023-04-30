using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.Repository.Glareshield;
using A320_Cockpit.Infrastructure.Cockpit.EventHandlerDispatcher;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.EventHandler.Glareshield
{
    /// <summary>
    /// Gestion des evenements liées aux boutons rotatif du FCU (pour le debug)
    /// </summary>
    public class FakeA320FcuBugEventHandler : IPayloadEventHandler
    {
        private readonly FakeA320FcuDisplayRepository fcuDisplayRepository;
        private int incrAlt;

        /// <summary>
        /// Création du gestionnaire d'evenement
        /// </summary>
        /// <param name="fcuDisplayRepository"></param>
        public FakeA320FcuBugEventHandler(FakeA320FcuDisplayRepository fcuDisplayRepository)
        {
            this.fcuDisplayRepository = fcuDisplayRepository;
            incrAlt = 1000;
        }

        /// <summary>
        /// La liste des évenements du cockpit en écoute pour ce gestionnaire
        /// </summary>
        public List<CockpitEvent> EventSubscriber => new() {
            CockpitEvent.FCU_SPEED_BUG,
            CockpitEvent.FCU_SPEED_PUSH,
            CockpitEvent.FCU_SPEED_PULL,
            CockpitEvent.FCU_HEADING_BUG,
            CockpitEvent.FCU_HEADING_PUSH,
            CockpitEvent.FCU_HEADING_PULL,
            CockpitEvent.FCU_ALTITUDE_BUG,
            CockpitEvent.FCU_ALTITUDE_PUSH,
            CockpitEvent.FCU_ALTITUDE_PULL,
            CockpitEvent.FCU_ALTITUDE_STEP,
            CockpitEvent.FCU_VS_BUG,
            CockpitEvent.FCU_VS_PUSH,
            CockpitEvent.FCU_VS_PULL
        };

        /// <summary>
        /// Gestion de l'event
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        public void Handle(CockpitEvent e, float value)
        {
            FcuDisplay fcuDisplay = fcuDisplayRepository.Payload;
            switch (e)
            {
                case CockpitEvent.FCU_SPEED_BUG:
                    double incrSpeed = fcuDisplay.IsMach ? 0.01 : 1;
                    fcuDisplay.Speed += value > 0 ? incrSpeed : -incrSpeed;
                    break;
                case CockpitEvent.FCU_SPEED_PUSH:
                    fcuDisplay.IsSpeedDash = true;
                    fcuDisplay.IsSpeedDot = true;
                    break;
                case CockpitEvent.FCU_SPEED_PULL:
                    fcuDisplay.IsSpeedDash = false;
                    fcuDisplay.IsSpeedDot = false;
                    break;
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
                case CockpitEvent.FCU_ALTITUDE_BUG:
                    fcuDisplay.Altitude += value > 0 ? incrAlt : -incrAlt;
                    break;
                case CockpitEvent.FCU_ALTITUDE_PUSH:
                    fcuDisplay.IsAltitudeDot = true;
                    break;
                case CockpitEvent.FCU_ALTITUDE_PULL:
                    fcuDisplay.IsAltitudeDot = false;
                    break;
                case CockpitEvent.FCU_ALTITUDE_STEP:
                    incrAlt = value > 0 ? 100 : 1000;
                    break;
                case CockpitEvent.FCU_VS_BUG:
                    double incrVs = fcuDisplay.IsFpa ? 0.1 : 100;
                    fcuDisplay.VerticalSpeed += value > 0 ? incrVs : -incrVs;
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

