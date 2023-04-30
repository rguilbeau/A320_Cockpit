using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.MsfsVariables;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.MsfsVariables.Enum;
using A320_Cockpit.Infrastructure.Simulator.Repository;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.Repository.Glareshield
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du des témoins des panels du Glareshield (pour l'A32NX)
    /// </summary>
    public class A32nxGlareshieldIndicatorsRepository : A32nxPayloadRepository<GlareshieldIndicators>
    {
        private static readonly GlareshieldIndicators glareshieldIndicators = new();

        /// <summary>
        /// Retourne l'entité
        /// </summary>
        public override GlareshieldIndicators Payload => glareshieldIndicators;

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfsSimulatorRepository"></param>
        public A32nxGlareshieldIndicatorsRepository(MsfsSimulatorRepository msfsSimulatorRepository) : base(msfsSimulatorRepository)
        {
        }

        /// <summary>
        /// Mise à jour de l'entité
        /// </summary>
        protected override bool Refresh(CockpitEvent e)
        {
            msfsSimulatorRepository.StartWatchRead();

            switch (e)
            {
                case CockpitEvent.ALL:
                    msfsSimulatorRepository.Read(A32nxVariables.Autopilot1Active);
                    msfsSimulatorRepository.Read(A32nxVariables.Autopilot2Active);
                    msfsSimulatorRepository.Read(A32nxVariables.AutoThrustStatus);
                    msfsSimulatorRepository.Read(A32nxVariables.LocModeActive);
                    msfsSimulatorRepository.Read(A32nxVariables.ExpedModeActive);
                    msfsSimulatorRepository.Read(A32nxVariables.ApprModeActive);
                    msfsSimulatorRepository.Read(A32nxVariables.LightIndicatorStatus);
                    msfsSimulatorRepository.Read(A32nxVariables.IsElectricityAc1BusPowered);
                    break;
                case CockpitEvent.FCU_AP1:
                    msfsSimulatorRepository.Read(A32nxVariables.Autopilot1Active);
                    msfsSimulatorRepository.Read(A32nxVariables.Autopilot2Active);
                    break;
                case CockpitEvent.FCU_AP2:
                    msfsSimulatorRepository.Read(A32nxVariables.Autopilot1Active);
                    msfsSimulatorRepository.Read(A32nxVariables.Autopilot2Active);
                    break;
                case CockpitEvent.FCU_ATHR:
                    msfsSimulatorRepository.Read(A32nxVariables.AutoThrustStatus);
                    break;
                case CockpitEvent.FCU_LOC:
                    msfsSimulatorRepository.Read(A32nxVariables.LocModeActive);
                    break;
                case CockpitEvent.FCU_EXPED:
                    msfsSimulatorRepository.Read(A32nxVariables.ExpedModeActive);
                    break;
                case CockpitEvent.FCU_APPR:
                    msfsSimulatorRepository.Read(A32nxVariables.ApprModeActive);
                    break;
                case CockpitEvent.FCU_METRICT_ALT:
                    msfsSimulatorRepository.Read(A32nxVariables.MetricAltToggle);
                    break;

            }

            return msfsSimulatorRepository.HasReadVariable;
        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override GlareshieldIndicators BuildPayload()
        {
            bool testLight = A32nxVariables.LightIndicatorStatus.Value == LightIndicatorEnum.TEST && A32nxVariables.IsElectricityAc1BusPowered.Value;

            glareshieldIndicators.FcuAp1 = testLight || A32nxVariables.Autopilot1Active.Value;
            glareshieldIndicators.FcuAp2 = testLight || A32nxVariables.Autopilot2Active.Value;
            glareshieldIndicators.FcuAthr = testLight || A32nxVariables.AutoThrustStatus.Value != AutothrottleEnum.DISENGAGED;
            glareshieldIndicators.FcuLoc = testLight || A32nxVariables.LocModeActive.Value;
            glareshieldIndicators.FcuExped = testLight || A32nxVariables.ExpedModeActive.Value;
            glareshieldIndicators.FcuAppr = testLight || A32nxVariables.ApprModeActive.Value;

            return glareshieldIndicators;
        }
    }
}
