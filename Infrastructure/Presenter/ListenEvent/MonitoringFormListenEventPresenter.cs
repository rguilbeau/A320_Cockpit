using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.UseCase.ListenEvent;
using A320_Cockpit.Infrastructure.View.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Presenter.ListenEvent
{
    /// <summary>
    /// Présenteur de l'envoi d'une frame pour le form Monitoring
    /// </summary>
    public class MonitoringFormListenEventPresenter : IListenEventPresenter
    {
        private readonly MonitringForm monitringForm;
        private Frame? frame;


        /// <summary>
        /// Création du presenter
        /// </summary>
        /// <param name="monitringForm"></param>
        public MonitoringFormListenEventPresenter(MonitringForm monitringForm)
        {
            this.monitringForm = monitringForm;
        }

        /// <summary>
        /// Présente la reception d'une frame
        /// </summary>
        /// <param name="e"></param>
        public void Present(CockpitEvent e)
        {
            if(frame != null)
            {
                monitringForm.AddFrame(FrameDirection.FROM_COCKPIT, frame);
            }
        }

        /// <summary>
        /// La frame recu
        /// </summary>
        public Frame? Frame { get => frame; set => frame = value; }
    }
}
