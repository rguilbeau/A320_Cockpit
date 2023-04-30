using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.MsfsVariables;
using A320_Cockpit.Infrastructure.Cockpit.EventHandlerDispatcher;
using A320_Cockpit.Infrastructure.Simulator.Repository;


namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.EventHandler.Glareshield
{
    /// <summary>
    /// Gestion des evenements liées aux boutons du Glareshield (pour l'A32NX)
    /// </summary>
    public class A32nxFcuGlareshieldButtonsEventHandler : IPayloadEventHandler
    {
        private readonly MsfsSimulatorRepository msfsSimulatorRepository;

        /// <summary>
        /// Création du gestionnaire d'evenement
        /// </summary>
        /// <param name="msfsSimulatorRepository"></param>
        public A32nxFcuGlareshieldButtonsEventHandler(MsfsSimulatorRepository msfsSimulatorRepository)
        {
            this.msfsSimulatorRepository = msfsSimulatorRepository;
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
            CockpitEvent.FCU_METRICT_ALT
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
                case CockpitEvent.FCU_AP1:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuAp1Push);
                    break;
                case CockpitEvent.FCU_AP2:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuAp2Push);
                    break;
                case CockpitEvent.FCU_ATHR:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuAthrPush);
                    break;
                case CockpitEvent.FCU_LOC:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuLocPush);
                    break;
                case CockpitEvent.FCU_APPR:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuApprPush);
                    break;
                case CockpitEvent.FCU_EXPED:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuExpedPush);
                    break;
                case CockpitEvent.FCU_SPEED_MACH:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuSpdMachTogglePush);
                    break;
                case CockpitEvent.FCU_VS_FPA:
                    msfsSimulatorRepository.Send(A32nxEvents.FcuTrkFpaTogglePush);
                    break;
                case CockpitEvent.FCU_METRICT_ALT:
                    // todo: event non trouvé
                    break;

            }
        }
    }
}
