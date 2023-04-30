using A320_Cockpit.Infrastructure.Aircraft;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320;
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
            string comPort = AskPort();
            Console.WriteLine();
            IAircraft aircraft = AskAircraft(comPort);
            Console.CursorVisible = false;
            aircraft.Runner.Start();
        }

        private static string AskPort()
        {
            string[] ports = SerialPort.GetPortNames();

            if(ports.Length == 0)
            {
                Console.WriteLine("Aucun COM port dispobilbe.");
                Console.ReadKey();
                return AskPort();
            }

            for (int i = 0; i < ports.Length; i++)
            {
                if (ports[i].Contains("COM"))
                {
                    string numPort = ports[i][3..];
                    Console.WriteLine("COM[" + numPort + "]");
                }
            }

            Console.Write("Sélectionnez le COM port: ");           

            string? portKey = Console.ReadLine();
            bool success = int.TryParse(portKey, out int indexPort);

            if(success && ports.Contains("COM" + indexPort))
            {
                return "COM" + indexPort;
            } else
            {
                Console.WriteLine();
                return AskPort();
            }
        }

        private static IAircraft AskAircraft(string comPort)
        {
            string[] aircrafts = { "A32NX", "FakeA320" };

            for (int i = 0; i < aircrafts.Length; i++)
            {
                Console.WriteLine(i + "-" + aircrafts[i]);
            }

            Console.Write("Sélectionnez un avion: ");

            string? portKey = Console.ReadLine();
            bool success = int.TryParse(portKey, out int indexPort);

            if (success && indexPort < aircrafts.Length)
            {
                return indexPort switch
                {
                    0 => new A32nx(comPort),
                    1 => new FakeA320(comPort),
                    _ => AskAircraft(comPort),
                };
            }
            else
            {
                Console.WriteLine();
                return AskAircraft(comPort);
            }
        }
    }
}