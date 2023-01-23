using A320_Cockpit.Adapter.LogHandler;
using A320_Cockpit.Domain.BusSend.UseCase;
using A320_Cockpit.Domain.CanBus;
using A320_Cockpit.Infrastructure.View.ApplicationTray;

namespace A320_Cockpit.Infrastructure.Presenter.SendFramePresenter
{
    /// <summary>
    /// Le présenteur permettant un affichage du system tray
    /// </summary>
    public class SystemTrayFramePresenter : ISendFramePresenter
    {
        private Frame? frame;
        private Exception? error;
        private ApplicationTray applicationTray;
        private ILogHandler logger;

        /// <summary>
        /// Création du présenter pour l'affiche du system tray
        /// </summary>
        /// <param name="applicationTray">Le system tray</param>
        /// <param name="logger">Lo logger</param>
        public SystemTrayFramePresenter(ApplicationTray applicationTray, ILogHandler logger)
        {
            this.applicationTray = applicationTray;
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
        /// Présente les éléments au system tray
        /// </summary>
        /// <param name="isSent"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Present(bool isSent)
        {
            if(error != null)
            {
                logger.Error(error);
                // blink red
            } 
            else if(isSent)
            {
                applicationTray.BlinkIcon();
            }
        }
    }
}
