using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.FakeA320.Brightness
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
            brightnessPanel.GlareshieldPanel = 100;
            brightnessPanel.OverheadPanel = 100;
            brightnessPanel.PedestalPanel = 100;
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
