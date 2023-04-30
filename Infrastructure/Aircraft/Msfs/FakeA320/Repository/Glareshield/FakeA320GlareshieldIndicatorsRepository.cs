using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.Repository.Glareshield
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du des témoins des panels du Glareshield (pour le debug)
    /// </summary>
    public class FakeA320GlareshieldIndicatorsRepository : FakeA320PayloadRepository<GlareshieldIndicators>
    {
        private static readonly GlareshieldIndicators glareshieldIndicators = new();

        /// <summary>
        /// L'entité
        /// </summary>
        public override GlareshieldIndicators Payload => glareshieldIndicators;

        /// <summary>
        /// Création du repository
        /// </summary>
        public FakeA320GlareshieldIndicatorsRepository() : base()
        {
            glareshieldIndicators.FcuAp1 = true;
            glareshieldIndicators.FcuAp2 = true;
            glareshieldIndicators.FcuAthr = true;
            glareshieldIndicators.FcuLoc = true;
            glareshieldIndicators.FcuExped = true;
            glareshieldIndicators.FcuAppr = true;
        }

        /// <summary>
        /// Mise à jour de l'entité
        /// </summary>
        protected override bool Refresh(CockpitEvent e)
        {
            return AskRefresh;
        }

        /// <summary>
        /// Mise à jour de l'entité
        /// </summary>
        protected override GlareshieldIndicators BuildPayload()
        {
            return glareshieldIndicators;
        }
    }
}
