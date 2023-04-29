using A320_Cockpit.Adaptation.Canbus.ArduinoSerialCan;
using A320_Cockpit.Adaptation.Log.Sirelog;
using A320_Cockpit.Infrastructure.Aircraft;
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
            ApplicationConfiguration.Initialize();
            Application.Run(new StartupDialog());
        }

        /// <summary>
        /// Log les exception non gérées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = new("Unhandled exception", (Exception)e.ExceptionObject);
            MessageBox.Show(exception.ToString(), "Unhandled exception");
        }
    }
}