using A320_Cockpit.Adaptation.Canbus;
using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Infrastructure;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx;
using A320_Cockpit.Infrastructure.View.StartupDialog;
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
            GlobalFactory.DEBUG = true;

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
            GlobalFactory.Get().Log.Error(new Exception("Unhandled exception", (Exception)e.ExceptionObject));
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Affiche la console de l'application
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}