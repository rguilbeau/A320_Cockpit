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

namespace A320_Cockpit.Infrastructure.Repository.Payload.FakeA320.Overhead
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du contrôle des LED témoins (boutons) du cockpit
    /// </summary>
    public class FakeA320LightIndicatorsRepository : FakeA320PayloadRepository<LightIndicators>
    {
        private static readonly LightIndicators lightIndicators = new();

        /// <summary>
        /// Retourne l'entité
        /// </summary>
        public override LightIndicators Payload => lightIndicators;

        /// <summary>
        /// Création du repository
        /// </summary>
        public FakeA320LightIndicatorsRepository() : base()
        {
            lightIndicators.TestIndicatorsLight = false;
        }

        /// <summary>
        /// Mise à jour de l'entité
        /// </summary>
        protected override bool Refresh(CockpitEvent e)
        {
            return AskRefresh;
        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override LightIndicators BuildPayload()
        {
            return lightIndicators;
        }
    }
}
