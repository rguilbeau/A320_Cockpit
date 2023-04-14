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
    public class A32nxBrightnessRepository : PayloadRepository<BrightnessCockpit>
    {
        private static readonly BrightnessCockpit brightness = new();
        
        /// <summary>
        /// Retourne l'entité
        /// </summary>
        protected override BrightnessCockpit Payload => brightness;

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
            if(e == CockpitEvent.ALL)
            {
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override BrightnessCockpit BuildPayload()
        {
            brightness.FcuDisplay = 100;
            return brightness;
        }
    }
}
