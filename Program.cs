using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Infrastructure.View.SystemTray;

namespace A320_Cockpit
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AllocConsole();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ApplicationConfiguration.Initialize();
            Application.Run(new ApplicationTray());
        }

        /// <summary>
        /// Log les exception non gérées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogFactory.Get().Error(new Exception("Unhandled exception", (Exception)e.ExceptionObject));
            {
                Application.Exit();
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}