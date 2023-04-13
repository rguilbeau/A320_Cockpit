using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du des témoins des panels du Glareshield
    /// </summary>
    public class A32nxGlareshieldIndicatorsRepository : IPayloadRepository
    {
        private static readonly GlareshieldIndicators glareshieldIndicators = new();

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        public PayloadEntity Find()
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
