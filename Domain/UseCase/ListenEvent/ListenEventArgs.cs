using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.ListenEvent
{
    /// <summary>
    /// Les arguments de l'event lors de la reception des nouveaux messages du cockpit
    /// </summary>
    public class ListenEventArgs : EventArgs
    {
        private readonly CockpitEvent cockpitEvent;
        private readonly float value;

        /// <summary>
        /// Création des arguments de l'event
        /// </summary>
        /// <param name="cockpitEvent"></param>
        /// <param name="value"></param>
        public ListenEventArgs(CockpitEvent cockpitEvent, float value)
        {
            this.cockpitEvent = cockpitEvent;
            this.value = value;
        }

        /// <summary>
        /// Retourne le cockpit event
        /// </summary>
        public CockpitEvent Event { get { return cockpitEvent; } }

        /// <summary>
        /// Retourne la frame
        /// </summary>
        public float Value { get { return value; } }
    }
}
