using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.UseCase.ListenEvent;
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
        /// <summary>
        /// Présente l'evenement sur le system tray
        /// </summary>
        /// <param name="e"></param>
        public void Present(CockpitEvent e)
        {
            
        }
    }
}
