namespace A320_Cockpit.Domain.Connexion.SimConnector
{
    /// <summary>
    /// Connecteur du simulateur
    /// </summary>
    public interface ISimulatorConnector
    {
        /// <summary>
        /// Etat de la connexion
        /// </summary>
        public bool IsOpen { get; }

        /// <summary>
        /// Ouvre la connexion au simulateur
        /// </summary>
        public void Open();
    }
}
