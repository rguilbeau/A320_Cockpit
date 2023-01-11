using A320_Cockpit.Domain.CanBus;
using A320_Cockpit.Domain.FrameSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Presenter
{
    internal interface IUpdateCockpitPresenter
    {
        public void Present(bool success, bool sent, Frame? frame);

    }
}
