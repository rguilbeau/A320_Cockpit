using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Enum;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.Payload.Repository.Brightness
{
    internal class FakeA320BrightnessSevenSegementsRepository : FakeA320PayloadRepository<BrightnessSevenSegments>
    {
        private static readonly BrightnessSevenSegments brightnessSevenSegments = new();

        /// <summary>
        /// L'entité
        /// </summary>
        public override BrightnessSevenSegments Payload => brightnessSevenSegments;

        /// <summary>
        /// Création du repository
        /// </summary>
        public FakeA320BrightnessSevenSegementsRepository() : base()
        {
            brightnessSevenSegments.IsTestLight = false;
            brightnessSevenSegments.Fcu = 255;
            brightnessSevenSegments.Altimeters = 255;
            brightnessSevenSegments.Bateries = 255;
            brightnessSevenSegments.Radio = 255;
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
        protected override BrightnessSevenSegments BuildPayload()
        {
            return brightnessSevenSegments;
        }
    }
}
