using A320_Cockpit.Adapter.CanBusHandler.SerialCanBusAdapter;

namespace A320_Cockpit.Adapter.CanBusHandler
{
    /// <summary>
    /// Factory pour la création de l'adapter du CAN Bus
    /// </summary>
    public static class CanBusFactory
    {
        private static ICanBusHandler? instance;

        /// <summary>
        /// Singleton de récupération de l'adapter du CAN Bus
        /// </summary>
        /// <returns></returns>
        public static ICanBusHandler Get()
        {
            if (instance == null)
            {
                instance = new SerialCan(new System.IO.Ports.SerialPort(), "COM5", 9600, "125Kbit");
            }

            return instance;
        }
    }
}
