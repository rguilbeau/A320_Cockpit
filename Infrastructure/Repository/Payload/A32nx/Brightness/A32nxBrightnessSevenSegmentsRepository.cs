using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Brightness
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

            if(e == CockpitEvent.ALL)
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
            if(!A32nxVariables.IsElectricityAc1BusPowered.Value)
            {
                seventSegmentBrightness = 0;
            }

            brightnessSeventSegments.IsTestLight = A32nxVariables.LightIndicatorStatus.Value == 0;
            brightnessSeventSegments.Fcu = seventSegmentBrightness;
            brightnessSeventSegments.Altimeters = seventSegmentBrightness;
            brightnessSeventSegments.Bateries = seventSegmentBrightness;
            brightnessSeventSegments.Radio = seventSegmentBrightness;
            return brightnessSeventSegments;
        }
    }
}
