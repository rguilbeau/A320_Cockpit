using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.View.SystemTray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Presenter.Send
{
    /// <summary>
    /// Présenter pour le system tray, afin de gérer l'affichage des frames envoyées
    /// </summary>
    public class TraySendPresenter : ISendPayloadPresenter
    {
        private Frame? frame;
        private Exception? error;
        private bool isSent = false;
        private readonly ILogHandler logger;


        /// <summary>
        /// Création du présenter
        /// </summary>
        /// <param name="applicationTray">Le system tray</param>
        /// <param name="logger">Lo logger</param>
        public TraySendPresenter(ILogHandler logger)
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
                //applicationTray.BlinkIcon();
            }
        }
    }
}
