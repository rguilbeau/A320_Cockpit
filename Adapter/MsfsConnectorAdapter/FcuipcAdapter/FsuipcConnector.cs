using A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter;
using FSUIPC;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter.FcuipcAdapter
{
    /// <summary>
    /// Connexion à MSFS au travers de FSUIPC
    /// </summary>
    public class FsuipcConnector
    {
        private readonly TypeConverter typeConverter;
        private static FsuipcConnector? instance;

        /// <summary>
        /// Singleton pour récupérer la connexion à FSUIPC
        /// </summary>
        /// <returns></returns>
        public static FsuipcConnector Get()
        {
            if (instance == null)
            {
                instance = new FsuipcConnector(new TypeConverter());
            }
            return instance;
        }

        /// <summary>
        /// L'état de la connexion vers FSUIPC
        /// </summary>
        public bool IsOpen
        {
            get { return true; }// FSUIPCConnection.IsOpen; }
        }

        /// <summary>
        /// Création d'une connexion à FSUIPC.
        /// Attention une seule connexion n'est possible
        /// </summary>
        /// <param name="typeConverter">L'utilitaire pour la convertion des types</param>
        private FsuipcConnector(TypeConverter typeConverter)
        {
            this.typeConverter = typeConverter;
        }

        /// <summary>
        /// Ouvre la connexion à FSUIPC
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Open()
        {
            /*FSUIPCConnection.Open();
            if (!FSUIPCConnection.IsOpen)
            {
                throw new Exception("Unable to connect to FSUIPC with unknwon error");
            }*/
        }

        /// <summary>
        /// Ferme la connexion à FSUIPC
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Close()
        {
            FSUIPCConnection.Close();
            if (FSUIPCConnection.IsOpen)
            {
                throw new Exception("Unable to close FSUIPC connexion with unkwnon error");
            }
        }

        /// <summary>
        /// Récupère la valeur d'un LVAR
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lvar"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T? ReadLvar<T>(Lvar<T> lvar)
        {
            if (!FSUIPCConnection.IsOpen)
            {
                throw new Exception("Unable to read LVAR, FSUIPC is not connected");
            }

            double lvarValue = FSUIPCConnection.ReadLVar(lvar.Name);
            T? value = typeConverter.Convert<T>(lvarValue);
            return value;
        }
    }
}
