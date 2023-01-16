
namespace A320_Cockpit.Domain.CanBus
{
    /// <summary>
    /// Représente la connexion au CAN Bus
    /// </summary>
    public interface ICanBus
    {
        /// <summary>
        /// Ouvre la connexion au CAN Bus
        /// </summary>
        public void Open();

        /// <summary>
        /// Ferme la connexion au CAN Bus
        /// </summary>
        public void Close();

        /// <summary>
        /// Envoi une frame au CAN Bus
        /// </summary>
        /// <param name="frame">La frame à envoyer</param>
        public void Send(Frame frame);

        /// <summary>
        /// Etat de la connexion du CAN Bus
        /// </summary>
        public bool IsOpen { get; }

    }
}
