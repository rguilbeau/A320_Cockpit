using A320_Cockpit.Domain.Entity.Cockpit;

namespace A320_Cockpit.Adaptation.Canbus
{
    /// <summary>
    /// Interface des adapteur du CAN Bus
    /// </summary>
    public interface ICanbus
    {
        /// <summary>
        /// Evenement sur la récéption de nouveau message
        /// </summary>
        public event EventHandler<Frame> ?MessageReceived;
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
