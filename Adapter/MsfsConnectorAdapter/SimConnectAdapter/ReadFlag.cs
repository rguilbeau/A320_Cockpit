namespace A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter
{
    /// <summary>
    /// Drapeau de lecture d'une variable, permet d'attendre la lecture d'une varibale après le callback
    /// de reception de message de SimConnect
    /// </summary>
    public class ReadFlag
    {
        private bool readed;
        private int currentIdRequest;
        private Type currentreadType;
        private object? currentReadValue;

        /// <summary>
        /// Création d'un nouveau drapeau de lecture d'une SimVar
        /// </summary>
        /// <param name="currentIdRequest">L'id de la request</param>
        /// <param name="currentReadValue">La v</param>
        /// <param name="currentreadType"></param>
        public ReadFlag(int currentIdRequest, Type currentreadType)
        {
            readed = false;
            this.currentIdRequest = currentIdRequest;
            this.currentreadType = currentreadType;
            currentReadValue = default;
        }

        public bool Readed { get => readed; set => readed = value; }
        public int CurrentIdRequest { get => currentIdRequest; set => currentIdRequest = value; }
        public object? CurrentReadValue { get => currentReadValue; set => currentReadValue = value; }
        public Type CurrentreadType { get => currentreadType; set => currentreadType = value; }
    }
}
