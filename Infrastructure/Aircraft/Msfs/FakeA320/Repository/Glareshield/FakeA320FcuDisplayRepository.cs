using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.Repository.Glareshield
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du des valeurs 
    /// à afficher sur les écrans du FCU (pour le debug)
    /// </summary>
    public class FakeA320FcuDisplayRepository : FakeA320PayloadRepository<FcuDisplay>
    {
        private static readonly FcuDisplay fcuDisplay = new();

        /// <summary>
        /// L'entité
        /// </summary>
        public override FcuDisplay Payload => fcuDisplay;

        /// <summary>
        /// Création du repository
        /// </summary>
        public FakeA320FcuDisplayRepository() : base()
        {
            fcuDisplay.Speed = 250;
            fcuDisplay.Heading = 160;
            fcuDisplay.IsMach = false;
            fcuDisplay.IsTrack = false;
            fcuDisplay.IsLat = true;
            fcuDisplay.IsFpa = false;
            fcuDisplay.IsSpeedDot = false;
            fcuDisplay.IsHeadingDot = false;
            fcuDisplay.IsAltitudeDot = false;
            fcuDisplay.Altitude = 15000;
            fcuDisplay.VerticalSpeed = 0;
            fcuDisplay.IsSpeedDash = false;
            fcuDisplay.IsHeadingDash = false;
            fcuDisplay.IsAltitudeDash = false;
            fcuDisplay.IsVerticalSpeedDash = true;
        }

        /// <summary>
        /// Met à jour l'entité
        /// </summary>
        protected override bool Refresh(CockpitEvent e)
        {
            return AskRefresh;
        }

        /// <summary>
        /// Mise à jour de l'entité
        /// </summary>
        protected override FcuDisplay BuildPayload()
        {
            return fcuDisplay;
        }
    }
}
