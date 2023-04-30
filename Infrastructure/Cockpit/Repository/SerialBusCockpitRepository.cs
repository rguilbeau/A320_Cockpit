using A320_Cockpit.Adaptation.Canbus;
using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Cockpit;

namespace A320_Cockpit.Infrastructure.Cockpit.Repository
{
    /// <summary>
    /// Le repository pour la connexion au cockpit via le CAN Bus par USB
    /// </summary>
    public class SerialBusCockpitRepository : ICockpitRepository
    {
        private readonly ICanbus canbus;

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="canbus"></param>
        public SerialBusCockpitRepository(ICanbus canbus)
        {
            this.canbus = canbus;
            this.canbus.MessageReceived += Canbus_MessageReceived;
        }

        /// <summary>
        /// Retourne si la connexion est ouverte
        /// </summary>
        public bool IsOpen => canbus.IsOpen;

        /// <summary>
        /// Reception des messages du cockpit
        /// </summary>
        public event EventHandler<Frame>? FrameReceived;

        /// <summary>
        /// Ferme la connexion
        /// </summary>
        public void Close()
        {
            canbus.Close();
        }

        /// <summary>
        /// Ouvre la connexion
        /// </summary>
        public void Open()
        {
            canbus.Open();
        }

        /// <summary>
        /// Envoi une frame
        /// </summary>
        /// <param name="frame"></param>
        public void Send(Frame frame)
        {
            canbus.Send(frame);
        }

        /// <summary>
        /// Active le ping
        /// </summary>
        /// <param name="pingId"></param>
        /// <param name="randomData"></param>
        public void ActivePing(FrameId pingId, bool randomData)
        {
            canbus.ActivePing(4000, pingId, randomData);
        }

        /// <summary>
        /// Reactive le ping
        /// </summary>
        public void ResumePing()
        {
            canbus.ResumePing();
        }

        /// <summary>
        /// Suspend le ping
        /// </summary>
        public void SuspendPing()
        {
            canbus.SuspendPing();
        }

        /// <summary>
        /// Reception des messages du cockpit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Canbus_MessageReceived(object? sender, Frame e)
        {
            FrameReceived?.Invoke(this, e);
        }
    }
}
