using A320_Cockpit.Domain.CanBus;

namespace A320_Cockpit.Domain.BusSend.UseCase
{
    /// <summary>
    /// Class mère des UsesCase d'envoi de frame
    /// </summary>
    /// <typeparam name="T">Le type du payload</typeparam>
    public abstract class BaseSender<T>
    {
        private static Dictionary<int, Frame> frameHistory = new Dictionary<int, Frame>();
        private ICanBus canBus;
        private ISendFramePresenter presenter;

        /// <summary>
        /// Création du UseCase d'envoi de frame
        /// </summary>
        /// <param name="canBus"></param>
        /// <param name="presenter"></param>
        public BaseSender(ICanBus canBus, ISendFramePresenter presenter)
        {
            this.canBus = canBus;
            this.presenter = presenter;
        }

        /// <summary>
        /// Contruction de la frame à partir du payload
        /// </summary>
        /// <param name="payload">Le payload</param>
        /// <returns></returns>
        protected abstract Frame BuildFrame(T payload);

        /// <summary>
        /// Envoi la frame au CAN Bus
        /// </summary>
        /// <param name="payload">Le payload à envoyer</param>
        public void Send(T payload)
        {
            Frame frame = BuildFrame(payload);

            bool send = true;
            bool isSent = false;
            if (frameHistory.ContainsKey(frame.Id))
            {
                send = !frame.Equals(frameHistory[frame.Id]);
            }

            if (send)
            {
                try
                {
                    canBus.Send(frame);
                    isSent = true;
                    frameHistory[frame.Id] = frame;
                }
                catch (Exception e)
                {
                    presenter.Error = e;
                    isSent = false;
                    frameHistory.Remove(frame.Id);
                }
            }
            presenter.Present(isSent);
        }
    }
}
