using A320_Cockpit.Adapter.LogHandler;
using A320_Cockpit.Domain.BusSend.UseCase;
using A320_Cockpit.Domain.CanBus;

namespace A320_Cockpit.Infrastructure.Presenter.SendFramePresenter
{
    /// <summary>
    /// Le présenter permettant un affichage Console
    /// </summary>
    public class ConsoleSendFramePresenter : ISendFramePresenter
    {
        private Frame? frame;
        private Exception? error;
        private ILogHandlerAdapter logger;

        /// <summary>
        /// Construction du présenter de frame via la Console
        /// </summary>
        /// <param name="logger"></param>
        public ConsoleSendFramePresenter(ILogHandlerAdapter logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// La frame envoyé
        /// </summary>
        public Frame? Frame { get => frame; set => frame = value; }
        /// <summary>
        /// L'erreur potentiel
        /// </summary>
        public Exception? Error { get => error; set => error = value; }


        /// <summary>
        /// Présente directement sans passer par de view model
        /// </summary>
        public void Present(bool isSent)
        {
            string logo = "==>";
            if(error != null)
            {
                logo = "/!\\";
            } else if(frame == null)
            {
                logo = "/?\\";
            }

            if (isSent)
            {
                Console.WriteLine(logo + " " + frame?.ToString());
            }
            
            if(error != null)
            {
                logger.Error(error);
            }
        }
    }
}
