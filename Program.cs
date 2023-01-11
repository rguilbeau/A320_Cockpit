using A320_Cockpit.Adapter.CanBusAdapter.SerialCanBusAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter;
using A320_Cockpit.Infrastructure.MsfsVariableUpdater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new MainForm());

            MsfsConnector msfsConnector = MsfsConnector.CreateConnection();
            SerialCan canBus = new SerialCan(new SerialPort(), "COM3", 9600, "125kBit");

            while(true)
            {
                new MsfsFcuDisplayUpdater(msfsConnector, canBus).Update();

                Thread.Sleep(10);
            }
        }
    }
}