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
    /// Repository pour la mise à jour et la récupération de l'entité du rétroaiclairage
    /// </summary>
    public class FakeA320BrightnessRepository : FakeA320PayloadRepository<BrightnessCockpit>
    {
        private static readonly BrightnessCockpit brightness = new();
        
        /// <summary>
        /// Retourne l'entité
        /// </summary>
        public override BrightnessCockpit Payload => brightness;

        /// <summary>
        /// Création du repository
        /// </summary>
        public FakeA320BrightnessRepository() : base()
        {
            brightness.SegmentScreens = 255;
            brightness.GlareshieldPanel = 50;
            brightness.OverheadPanel = 255;
            brightness.PedestalPanel = 255;
            brightness.Indicators = 255;
            brightness.Buttons = 255;
            brightness.TestLight = false;
        }

        /// <summary>
        /// Mise à jour de l'entité
        /// </summary>
        protected override bool Refresh(CockpitEvent e)
        {
            return AskRefresh;
        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override BrightnessCockpit BuildPayload()
        {
            return brightness;
        }
    }
}
