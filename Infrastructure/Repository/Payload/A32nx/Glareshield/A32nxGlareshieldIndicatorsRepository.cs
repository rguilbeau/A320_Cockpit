using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield
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

            switch(e)
            {
                case CockpitEvent.ALL:
                    msfsSimulatorRepository.Read(A32nxVariables.Autopilot1Active);
                    msfsSimulatorRepository.Read(A32nxVariables.Autopilot2Active);
                    msfsSimulatorRepository.Read(A32nxVariables.AutoThrustStatus);
                    msfsSimulatorRepository.Read(A32nxVariables.LocModeActive);
                    msfsSimulatorRepository.Read(A32nxVariables.ExpedModeActive);
                    msfsSimulatorRepository.Read(A32nxVariables.ApprModeActive);
                    msfsSimulatorRepository.Read(A32nxVariables.IsElectricityAc1BusPowered);
                    break;
                case CockpitEvent.FCU_LOC:
                    msfsSimulatorRepository.Read(A32nxVariables.LocModeActive);
                    break;
            }

            return msfsSimulatorRepository.HasReadVariable;
        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override GlareshieldIndicators BuildPayload()
        {
            glareshieldIndicators.FcuAp1 = A32nxVariables.Autopilot1Active.Value;
            glareshieldIndicators.FcuAp2 = A32nxVariables.Autopilot2Active.Value;
            glareshieldIndicators.FcuAthr = A32nxVariables.AutoThrustStatus.Value != 0;
            glareshieldIndicators.FcuLoc = A32nxVariables.LocModeActive.Value;
            glareshieldIndicators.FcuExped = A32nxVariables.ExpedModeActive.Value;
            glareshieldIndicators.FcuAppr = A32nxVariables.ApprModeActive.Value;
            glareshieldIndicators.FcuIsPowerOn = A32nxVariables.IsElectricityAc1BusPowered.Value;
            return glareshieldIndicators;
        }
    }
}
