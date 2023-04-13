using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Entity.Payload.Overhead;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Overhead
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du contrôle des LED témoins (boutons) du cockpit
    /// </summary>
    public class A32nxLightIndicatorsRepository : IPayloadRepository
    {
        private static readonly LightIndicators lightIndicators = new();

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        public PayloadEntity Find()
        {
            lightIndicators.TestIndicatorsLight = A32nxVariables.LightIndicatorStatus.Value == 0;
            return lightIndicators;
        }
    }
}
