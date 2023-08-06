using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Enum;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.Repository.Brightness
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du rétroaiclairage (pour de debug)
    /// </summary>
    public class FakeA320BrightnessPanelRepository : FakeA320PayloadRepository<BrightnessPanel>
    {
        private static readonly BrightnessPanel brightnessPanel = new();

        /// <summary>
        /// L'entité
        /// </summary>
        public override BrightnessPanel Payload => brightnessPanel;

        /// <summary>
        /// Création du repository
        /// </summary>
        public FakeA320BrightnessPanelRepository() : base()
        {
            brightnessPanel.GlareshieldPanel = 255;
            brightnessPanel.OverheadPanel = 255;
            brightnessPanel.PedestalPanel = 255;
            brightnessPanel.Indicators = 80;
            brightnessPanel.Buttons = 255;
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
        protected override BrightnessPanel BuildPayload()
        {
            return brightnessPanel;
        }
    }
}
