using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Brightness
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du rétroaiclairage
    /// </summary>
    public class A32nxBrightnessRepository : A32nxPayloadRepository<BrightnessCockpit>
    {
        private readonly BrightnessCockpit brightness = new();
        
        /// <summary>
        /// Retourne l'entité
        /// </summary>
        public override BrightnessCockpit Payload => brightness;

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfsSimulatorRepository"></param>
        public A32nxBrightnessRepository(MsfsSimulatorRepository msfsSimulatorRepository) : base(msfsSimulatorRepository)
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
                msfsSimulatorRepository.Read(A32nxVariables.LightIndicatorStatus);
            }

            return msfsSimulatorRepository.HasReadVariable;
        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override BrightnessCockpit BuildPayload()
        {
            brightness.SegmentScreens = 255;
            brightness.GlareshieldPanel = 100;
            brightness.OverheadPanel = 255;
            brightness.PedestalPanel = 255;
            brightness.Indicators = 255;
            brightness.Buttons = 255;
            brightness.TestLight = A32nxVariables.LightIndicatorStatus.Value == 0;
            return brightness;
        }
    }
}
