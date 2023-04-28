using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.ListenEvent
{
    /// <summary>
    /// Presenteur de l'écoute des events recu du cockpit
    /// </summary>
    public interface IListenEventPresenter
    {

        /// <summary>
        /// Présente l'event recu
        /// </summary>
        /// <param name="e"></param>
        public void Present(CockpitEvent e);

    }
}
