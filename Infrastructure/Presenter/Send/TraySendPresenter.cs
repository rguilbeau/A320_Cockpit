using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.UseCase.Send;
using A320_Cockpit.Infrastructure.View.SystemTray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Presenter.Send
{
    public class TraySendPresenter : ISendPresenter
    {
        private Frame? frame;
        private Exception? error;
        private bool isSent = false;
        private readonly ApplicationTray applicationTray;
        private readonly ILogHandler logger;


        /// <summary>
        /// Création du présenter pour l'affiche du system tray
        /// </summary>
        /// <param name="applicationTray">Le system tray</param>
        /// <param name="logger">Lo logger</param>
        public TraySendPresenter(ApplicationTray applicationTray, ILogHandler logger)
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
        /// Si le message a été envoyé
        /// </summary>
        public bool IsSent { get => isSent; set => isSent = value; }



        /// <summary>
        /// Présente les éléments au system tray
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Present()
        {
            if (error != null)
            {
                logger.Error(error);
                // blink red
            }
            else if (isSent)
            {
                applicationTray.BlinkIcon();
            }
        }
    }
}
