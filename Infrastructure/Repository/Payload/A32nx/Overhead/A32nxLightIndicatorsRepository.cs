using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Entity.Payload.Overhead;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload.Overhead;
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
    public class A32nxLightIndicatorsRepository : A32nxPayloadRepository<LightIndicators>, ILightIndicatorsRepository
    {
        private static readonly LightIndicators lightIndicators = new();

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfs"></param>
        public A32nxLightIndicatorsRepository(MsfsSimulatorRepository msfs) : base(msfs)
        {
        }

        /// <summary>
        /// Retourne l'entité LightIndicators
        /// </summary>
        protected override LightIndicators Payload => lightIndicators;

        /// <summary>
        /// Met à jour les valeurs des variables MSFS (LVar, SimVar...)
        /// Si en event est passé, on ne met à jour que les varibales susceptibles d'avoir été modifiées
        /// </summary>
        /// <param name="e"></param>
        protected override void Refresh(CockpitEvent? e)
        {
            bool all = e == null;

            if (all || e == CockpitEvent.OHP_TEST_LIGHT)
            {
                msfs.Read(A32nxVariables.LightIndicatorStatus);
            }

        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override void UpdateEntity()
        {
            lightIndicators.TestIndicatorsLight = A32nxVariables.LightIndicatorStatus.Value == 0;
        }
    }
}
