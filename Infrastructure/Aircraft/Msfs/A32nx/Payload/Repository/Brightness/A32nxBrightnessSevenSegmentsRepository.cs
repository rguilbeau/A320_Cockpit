using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.Payload.Variables;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.Payload.Variables.Enum;
using A320_Cockpit.Infrastructure.Simulator.Repository;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.Payload.Repository.Brightness
{
    /// <summary>
    /// Repository des rétroaiclaire des écrans 7 segments
    /// </summary>
    public class A32nxBrightnessSevenSegmentsRepository : A32nxPayloadRepository<BrightnessSevenSegments>
    {
        private readonly BrightnessSevenSegments brightnessSeventSegments = new();

        /// <summary>
        /// Retourne l'entité
        /// </summary>
        public override BrightnessSevenSegments Payload => brightnessSeventSegments;

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfsSimulatorRepository"></param>
        public A32nxBrightnessSevenSegmentsRepository(MsfsSimulatorRepository msfsSimulatorRepository) : base(msfsSimulatorRepository)
        {
        }

        /// <summary>
        /// Mise à jour de l'entité
        /// </summary>
        protected override bool Refresh(CockpitEvent e)
        {
            msfsSimulatorRepository.StartWatchRead();

            if (e == CockpitEvent.ALL)
            {
                msfsSimulatorRepository.Read(A32nxVariables.IsElectricityAc1BusPowered);
                msfsSimulatorRepository.Read(A32nxVariables.LightIndicatorStatus);
            }

            return msfsSimulatorRepository.HasReadVariable;
        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override BrightnessSevenSegments BuildPayload()
        {
            byte seventSegmentBrightness = 255;
            if (!A32nxVariables.IsElectricityAc1BusPowered.Value)
            {
                seventSegmentBrightness = 0;
            }

            brightnessSeventSegments.IsTestLight = A32nxVariables.LightIndicatorStatus.Value == LightIndicatorEnum.TEST;
            brightnessSeventSegments.Fcu = seventSegmentBrightness;
            brightnessSeventSegments.Altimeters = seventSegmentBrightness;
            brightnessSeventSegments.Bateries = seventSegmentBrightness;
            brightnessSeventSegments.Radio = seventSegmentBrightness;
            return brightnessSeventSegments;
        }
    }
}
