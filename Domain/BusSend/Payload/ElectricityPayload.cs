namespace A320_Cockpit.Domain.BusSend.Payload
{
    /// <summary>
    /// Contenu de la frame "Electricité"
    /// </summary>
    public class ElectricityPayload
    {
        private bool isElectricityAc1BusPowered;

        /// <summary>
        /// Etat de l'AC 1 Bus
        /// </summary>
        public bool IsElectricityAc1BusPowered { get => isElectricityAc1BusPowered; set => isElectricityAc1BusPowered = value; }
    }
}
