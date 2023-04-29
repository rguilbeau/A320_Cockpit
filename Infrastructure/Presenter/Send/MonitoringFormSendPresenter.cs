using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.View.Monitoring;
using A320_Cockpit.Infrastructure.View.SystemTray;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Presenter.Send
{
    /// <summary>
    /// Présenteur de l'envoi de frame pour le Form Monitoring
    /// </summary>
    public class MonitoringFormSendPresenter : ISendPayloadPresenter
    {
        private readonly MonitringForm monitringForm;
        private Frame? frame;
        private Exception? error;
        private bool isSent;

        /// <summary>
        /// Création du presenter
        /// </summary>
        /// <param name="monitringForm"></param>
        public MonitoringFormSendPresenter(MonitringForm monitringForm)
        {
            this.monitringForm = monitringForm;
        }

        /// <summary>
        /// Présente l'envoi de la frame
        /// </summary>
        public void Present()
        {
            if(error != null)
            {
                monitringForm.AddError(error);
            } 
            else if(isSent && frame != null)
            {
                monitringForm.AddFrame(FrameDirection.TO_COCKPIT, frame);
            }
        }

        /// <summary>
        /// La frame envoyée
        /// </summary>
        public Frame? Frame { get => frame; set => frame = value; }
        /// <summary>
        /// L'erreur lors de l'envoi
        /// </summary>
        public Exception? Error { get => error; set => error = value; }
        /// <summary>
        /// Si la frame a été envoyé ou non
        /// </summary>
        public bool IsSent { get => isSent; set => isSent = value; }
    }
}
