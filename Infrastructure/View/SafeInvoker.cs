using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.View
{
    public static class SafeInvoker
    {
        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired && !control.IsDisposed)
            {
                control.Invoke(action);
            }
            else
            {
                if (!control.IsDisposed && control.IsHandleCreated)
                {
                    action.Invoke();
                }
            }
        }
    }
}
