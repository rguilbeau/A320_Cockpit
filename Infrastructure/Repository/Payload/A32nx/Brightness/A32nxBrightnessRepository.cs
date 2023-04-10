using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload.Brightness;
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
    public class A32nxBrightnessRepository : A32nxPayloadRepository<BrightnessCockpit>, IBrightnessRepository
    {
        private static readonly BrightnessCockpit brightness = new ();

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfs"></param>
        public A32nxBrightnessRepository(MsfsSimulatorRepository msfs) : base(msfs)
        {
        }
        
        /// <summary>
        /// Retourne l'entité Brightness
        /// </summary>
        protected override BrightnessCockpit Payload => brightness;

        /// <summary>
        /// Met à jour les valeurs des variables MSFS (LVar, SimVar...)
        /// Si en event est passé, on ne met à jour que les varibales susceptibles d'avoir été modifiées
        /// </summary>
        /// <param name="e"></param>
        protected override void Refresh(CockpitEvent e)
        {
            // todo
        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override void UpdateEntity()
        {
            brightness.FcuDisplay = 100;
        }
    }
}
