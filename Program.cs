using A320_Cockpit.Adapter;
using A320_Cockpit.Adapter.FcuipcAdapter;
using A320_Cockpit.Adapter.SimConnectAdapter;
using A320_Cockpit.Domain.Frames.Fcu;
using A320_Cockpit.Infrastructure.Can;
using A320_Cockpit.Infrastructure.Updator;
using A320_Cockpit.View;
using FSUIPC;
using Microsoft.FlightSimulator.SimConnect;
using System.IO.Ports;
using System.Runtime.InteropServices;

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

            SerialPort serialPort = new();

            VarRequester requester = VarRequester.CreateConnection();
            SerialCan serialCan = new SerialCan(serialPort, "COM3", 9600, "125kBit");


            while (true)
            {
                try
                {
                    foreach (var updator in updators)
                    {
                        updator.Update();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(1000);
            }

        }
    }
}