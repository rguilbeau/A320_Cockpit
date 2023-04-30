using A320_Cockpit.Domain.Enum;

namespace A320_Cockpit.Domain.UseCase.ListenEvent
{
    /// <summary>
    /// Les arguments de l'event lors de la reception des nouveaux messages du cockpit
    /// </summary>
    public class ListenEventArgs : EventArgs
    {
        private readonly CockpitEvent cockpitEvent;
        private readonly float value;
        private readonly bool isPing;

        /// <summary>
        /// Création des arguments de l'event
        /// </summary>
        /// <param name="cockpitEvent"></param>
        /// <param name="value"></param>
        public ListenEventArgs(CockpitEvent cockpitEvent, float value, bool isPing)
        {
            this.cockpitEvent = cockpitEvent;
            this.value = value;
            this.isPing = isPing;
        }

        /// <summary>
        /// Retourne le cockpit event
        /// </summary>
        public CockpitEvent Event { get { return cockpitEvent; } }

        /// <summary>
        /// Retourne la frame
        /// </summary>
        public float Value { get { return value; } }

        /// <summary>
        /// Retourne si l'evenement est issu d'un ping
        /// </summary>
        public bool IsPing { get {  return isPing; } }
    }
}
