using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Cockpit.EventHandlerDispatcher;
using A320_Cockpit.Infrastructure.Simulator.Repository;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.EventHandler.Glareshield
{
    /// <summary>
    /// Gestion des evenements liées aux boutons rotatif du FCU (pour l'A32NX)
    /// </summary>
    public class A32nxFcuBugEventHandler : IPayloadEventHandler
    {
        private readonly MsfsSimulatorRepository msfsSimulatorRepository;

        /// <summary>
        /// Création du gestionnaire d'evenement
        /// </summary>
        /// <param name="msfsSimulatorRepository"></param>
        public A32nxFcuBugEventHandler(MsfsSimulatorRepository msfsSimulatorRepository)
        {
            this.msfsSimulatorRepository = msfsSimulatorRepository;
            A32nxEvents.FcuAltIncr.Value = 1000;
        }

        /// <summary>
        /// La liste des évenements du cockpit en écoute pour ce gestionnaire
        /// </summary>
        public List<CockpitEvent> EventSubscriber => new()
        {
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
        /// <param name="cockpitEvent"></param>
        /// <param name="value"></param>
        public void Handle(CockpitEvent cockpitEvent, float value)
        {
            switch (cockpitEvent)
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
                case CockpitEvent.FCU_HEADING_BUG:
                    msfsSimulatorRepository.Send(value > 0 ? A32nxEvents.FcuHdgIncr : A32nxEvents.FcuHdgDecr);
                    break;
                case CockpitEvent.FCU_HEADING_PUSH:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuHdgPush);
                    break;
                case CockpitEvent.FCU_HEADING_PULL:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuHdgPull);
                    break;
                case CockpitEvent.FCU_ALTITUDE_BUG:
                    msfsSimulatorRepository.Send(value > 0 ? A32nxEvents.FcuAltIncr : A32nxEvents.FcuAltDecr);
                    break;
                case CockpitEvent.FCU_ALTITUDE_PUSH:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuAltPush);
                    break;
                case CockpitEvent.FCU_ALTITUDE_PULL:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuAltPull);
                    break;
                case CockpitEvent.FCU_ALTITUDE_STEP:
                    A32nxEvents.FcuAltIncr.Value = (short)(value > 0 ? 100 : 1000);
                    A32nxEvents.FcuAltDecr.Value = (short)(value > 0 ? 100 : 1000);
                    break;
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
