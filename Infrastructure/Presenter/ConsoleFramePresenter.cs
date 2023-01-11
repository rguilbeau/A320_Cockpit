using A320_Cockpit.Adapter.CanBusAdapter;
using A320_Cockpit.Domain.CanBus;
using A320_Cockpit.Domain.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A320_Cockpit.Infrastructure.Presenter
{
    internal class ConsoleFramePresenter : IUpdateCockpitPresenter
    {
        public void Present(bool success, bool sent, Frame? frame)
        {
            if(sent)
            {
                Console.WriteLine((success ? "==> " : "/!\\ ") + " " + frame?.ToString());
            }
        }
    }
}
