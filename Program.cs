using A320_Cockpit.Adaptation.Canbus.ArduinoSerialCan;
using A320_Cockpit.Adaptation.Log.Sirelog;
using A320_Cockpit.Infrastructure.Aircraft;
using A320_Cockpit.Infrastructure.View.StartupDialog;
using A320_Cockpit.Infrastructure.View.SystemTray;

namespace A320_Cockpit
{
    internal static class Program
    {
        static IAircraft ?aircraft;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            //AllocConsole();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ApplicationConfiguration.Initialize();
            Application.Run(new StartupDialog(new SirelogAdapter("")));
        }

        /// <summary>
        /// Log les exception non gérées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            aircraft?.Logger.Error(new Exception("Unhandled exception", (Exception)e.ExceptionObject));
            Application.Exit();
        }

        /// <summary>
        /// Affiche la console de l'application
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}