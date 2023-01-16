using A320_Cockpit.Adapter.LogHandler.SirelogAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.LogHandler
{
    public static class LogHandlerFactory
    {

        private static ILogHandlerAdapter? logHandler;

        public static ILogHandlerAdapter Get()
        {
            if(logHandler == null)
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                logHandler = new SirelogHandler(Path.Combine(appDataPath, AppResources.AppName, "logs.log"));
            }

            return logHandler;
        }


    }
}
