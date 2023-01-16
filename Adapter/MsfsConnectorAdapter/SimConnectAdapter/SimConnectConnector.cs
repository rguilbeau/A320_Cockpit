using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter
{
    /// <summary>
    /// Connexion à MSFS au travers de SimConnect
    /// </summary>
    public class SimConnectConnector
    {
        private const string NAME = "A320 - Cockpit";
        private const int TIMEOUT_CONNECTION = 3000;
        private const int TIMEOUT_READVAR = 500;

        private const int DEFAULT_USER_EVENT_WIN32 = 0x402;
        private const int DEFAUL_ID_OBJECT = 1;
        private static SimConnectConnector? instance;

        private SimConnect? simConnect;
        private readonly List<string> registeredSimVars;
        private bool isOpen;
        private readonly TypeConverter typeConverter;
        private ReadFlag? readFlag = null;

        /// <summary>
        /// La structure d'une variable en string
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct StructStr
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string sValue;
        };

        /// <summary>
        /// L'enum des IDS des variables (permet juste de caster l'ID en Enum)
        /// </summary>
        private enum ID_DEFINITION { }


        /// <summary>
        /// Singleton de récupération de la connexion à SimConnect
        /// </summary>
        /// <returns></returns>
        public static SimConnectConnector Get()
        {
            if (instance == null)
            {
                instance = new SimConnectConnector(
                    new TypeConverter()
                );
            }
            return instance;
        }

        /// <summary>
        /// Création d'une nouvelle connexion à SimConnect
        /// </summary>
        /// <param name="simConnect">La librairie SimConnect fournis par Microsoft</param>
        /// <param name="typeConverter">L'utilisataire de convertion des types des variables</param>
        private SimConnectConnector(TypeConverter typeConverter)
        {
            isOpen = false;
            this.typeConverter = typeConverter;
            registeredSimVars = new List<string>();
        }

        /// <summary>
        /// Etat de la connexion à SimConnect
        /// </summary>
        public bool IsOpen
        {
            get { return isOpen && simConnect != null; }
        }

        /// <summary>
        /// Ouvre la connexion à SimConnect
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Open()
        {
            if (!isOpen)
            {
                simConnect = new SimConnect(NAME, IntPtr.Zero, DEFAULT_USER_EVENT_WIN32, null, 0);
                simConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                simConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(SimConnect_OnRecvQuit);
                simConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_OnRecvException);
                simConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);

                long startMiliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                while (!IsOpen)
                {
                    // Attente de la récéption du message de connexion
                    simConnect.ReceiveMessage();
                    Thread.Yield();

                    long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    if (now > startMiliseconds + TIMEOUT_CONNECTION)
                    {
                        throw new Exception("Unable to connect to SimConnect, timeout");
                    }
                }
            }

            if (!isOpen)
            {
                throw new Exception("Unable to connect to SimConnect with unkwnon error");
            }
        }

        /// <summary>
        /// Ferme la connexion à SimConnect
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Close()
        {
            simConnect?.Dispose();

            /*long startMiliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            while (IsOpen)
            {
                // Attente de la récéption du message de déconnexion
                simConnect.ReceiveMessage();
                Thread.Yield();

                long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (now > (startMiliseconds + TIMEOUT_CONNECTION))
                {
                    throw new Exception("Unable to disconnect to SimConnect, timeout");
                }
            }*/

            if (IsOpen)
            {
                throw new Exception("Unable to disconnect to SimConnect with unknwon error");
            }
        }

        /// <summary>
        /// Récupère la valeur d'une SimVar
        /// </summary>
        /// <typeparam name="T">Le type de la variable</typeparam>
        /// <param name="simVar">La Simvar à lire</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T? ReadSimVar<T>(SimVar<T> simVar)
        {
            T? value = default;

            if (!isOpen || simConnect == null)
            {
                throw new Exception("Unable to read SimVar, SimConnect connexion is not opened");
            }

            if (!registeredSimVars.Contains(simVar.Name))
            {
                RegisterSimVar(simVar);
            }

            int definition = registeredSimVars.IndexOf(simVar.Name);
            simConnect.RequestDataOnSimObjectType((ID_DEFINITION)definition, (ID_DEFINITION)definition, 0, SIMCONNECT_SIMOBJECT_TYPE.ALL);

            readFlag = new ReadFlag(definition, typeof(T));

            long startMiliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            while (!readFlag.Readed)
            {
                // Attente de la récéption du message
                simConnect.ReceiveMessage();
                Thread.Yield();

                long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (now > startMiliseconds + TIMEOUT_READVAR)
                {
                    throw new Exception("Unable to read SimVar, timeout");
                }
            }

            if (readFlag.CurrentReadValue != null)
            {
                if (!typeof(T).Equals(typeof(string)))
                {
                    value = typeConverter.Convert<T>((double)readFlag.CurrentReadValue);
                }
                else
                {
                    value = (T?)readFlag.CurrentReadValue;
                }
            }

            return value;
        }

        /// <summary>
        /// Callback de récéption de message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            if (readFlag != null && !readFlag.Readed)
            {
                uint iRequest = data.dwRequestID;
                uint iObject = data.dwObjectID;

                if (iObject == DEFAUL_ID_OBJECT && iRequest == readFlag.CurrentIdRequest)
                {
                    if (readFlag.CurrentreadType != null && readFlag.CurrentreadType == typeof(string))
                    {
                        StructStr result = (StructStr)data.dwData[0];
                        readFlag.CurrentReadValue = result.sValue;
                    }
                    else
                    {
                        double value = (double)data.dwData[0];
                        readFlag.CurrentReadValue = value;
                    }

                    readFlag.Readed = true;
                }
            }
        }

        /// <summary>
        /// Enregistre la variable à lire dans SimConnect
        /// </summary>
        /// <typeparam name="T">Le type de varaible</typeparam>
        /// <param name="simVar"></param>
        /// <exception cref="Execution"></exception>
        private void RegisterSimVar<T>(SimVar<T> simVar)
        {
            if (!isOpen || simConnect == null)
            {
                throw new Exception("Unable to register variable, SimConnect connexion is not opened");
            }

            registeredSimVars.Add(simVar.Name);
            int definition = registeredSimVars.IndexOf(simVar.Name);

            if (typeof(T).Equals(typeof(string)))
            {
                simConnect.AddToDataDefinition((ID_DEFINITION)definition, simVar.Name, simVar.Unit, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.RegisterDataDefineStruct<StructStr>((ID_DEFINITION)definition);
            }
            else
            {
                simConnect.AddToDataDefinition((ID_DEFINITION)definition, simVar.Name, simVar.Unit, SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.RegisterDataDefineStruct<double>((ID_DEFINITION)definition);
            }
        }

        /// <summary>
        /// Callback d'une reception d'exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SimConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {

        }

        /// <summary>
        /// Callback d'une réception de demande de déconnexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SimConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            isOpen = false;
        }

        /// <summary>
        /// Callback d'une reception de l'information de connnexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            isOpen = true;
        }
    }
}
