using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.MsfsVariables;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.MsfsVariables.Enum;
using A320_Cockpit.Infrastructure.Simulator.Repository;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.Repository.Brightness
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du rétroaiclairage (pour l'A32NX)
    /// </summary>
    public class A32nxBrightnessPanelRepository : A32nxPayloadRepository<BrightnessPanel>
    {
        private readonly BrightnessPanel brightnessPanel = new();

        /// <summary>
        /// Retourne l'entité
        /// </summary>
        public override BrightnessPanel Payload => brightnessPanel;

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfsSimulatorRepository"></param>
        public A32nxBrightnessPanelRepository(MsfsSimulatorRepository msfsSimulatorRepository) : base(msfsSimulatorRepository)
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
        protected override BrightnessPanel BuildPayload()
        {
            byte panelBrightness = 100;
            byte indicatorsBrightness = 80;
            byte buttonBrightness = 255;

            if (A32nxVariables.LightIndicatorStatus.Value == LightIndicatorEnum.DIM)
            {
                indicatorsBrightness = 10;
            }

            if (!A32nxVariables.IsElectricityAc1BusPowered.Value)
            {
                brightnessPanel.GlareshieldPanel = 0;
                brightnessPanel.OverheadPanel = 0;
                brightnessPanel.PedestalPanel = 0;
                brightnessPanel.Indicators = indicatorsBrightness;
                brightnessPanel.Buttons = 0;
            }
            else
            {
                brightnessPanel.GlareshieldPanel = panelBrightness;
                brightnessPanel.OverheadPanel = panelBrightness;
                brightnessPanel.PedestalPanel = panelBrightness;
                brightnessPanel.Indicators = indicatorsBrightness;
                brightnessPanel.Buttons = buttonBrightness;
            }

            return brightnessPanel;
        }
    }
}
