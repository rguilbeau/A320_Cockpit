using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.UseCase.ListenEvent;
using A320_Cockpit.Infrastructure.View.SystemTray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Presenter.ListenEvent
{
    /// <summary>
    /// Présenteur sur le Système Tray des évenements recu du cockpit
    /// </summary>
    public class TrayListenEventPresenter : IListenEventPresenter
    {
        private readonly ApplicationTray applicationTray;
        private Frame ?frame;

        /// <summary>
        /// Création du présenter
        /// </summary>
        /// <param name="applicationTray"></param>
        public TrayListenEventPresenter(ApplicationTray applicationTray)
        {
            this.applicationTray = applicationTray;
        }

        /// <summary>
        /// Présente l'evenement sur le system tray
        /// </summary>
        /// <param name="e"></param>
        public void Present(CockpitEvent e)
        {
            applicationTray.BlinkIcon(TrayStatus.SUCCESS);    
        }

        /// <summary>
        /// La frame recu
        /// </summary>
        public Frame ?Frame { get => frame; set => frame = value; }
    }
}
