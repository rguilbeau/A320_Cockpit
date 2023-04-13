using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.ListenEvent
{
    public interface IListenEventPresenter
    {

        public void Present(CockpitEvent e);

    }
}
