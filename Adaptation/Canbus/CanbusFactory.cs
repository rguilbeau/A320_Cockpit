
using A320_Cockpit.Adaptation.Canbus.CANtact;

namespace A320_Cockpit.Adaptation.Canbus
{
    /// <summary>
    /// Factory pour la création de l'adapter du CAN Bus
    /// </summary>
    public static class CanBusFactory
    {
        private static ICanbus? instance;

        /// <summary>
        /// Singleton de récupération de l'adapter du CAN Bus
        /// </summary>
        /// <returns></returns>
        public static ICanbus Get()
        {
            instance ??= new CANtactAdapter(new System.IO.Ports.SerialPort(), "COM6", 9600, "125Kbit");
            return instance;
        }
    }
}
