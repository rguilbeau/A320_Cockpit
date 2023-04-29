using A320_Cockpit.Adaptation.Canbus.ArduinoSerialCan;
using A320_Cockpit.Adaptation.Log.Sirelog;
using A320_Cockpit.Infrastructure.Aircraft;

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
            Console.CursorVisible = false;

            string comPort = "COM4";
            
            IAircraft aircraft = new FakeA320(comPort);
            IAircraft aircraft = new A32nx(comPort);
            
                Console.WriteLine("Aircraft : " + A32nx.NAME);
            Console.WriteLine("COM Port : " + comPort);

            aircraft.Runner.Start();
        }
    }
}