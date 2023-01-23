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

        private const int DEFAULT_USER_EVENT_WIN32 = 0x402;
        private const int DEFAUL_ID_OBJECT = 1;
        private static SimConnectConnector? instance;

        private SimConnect? simConnect;
        private readonly List<string> registeredSimVars;
        private bool isOpen;
        private readonly TypeConverter typeConverter;

        AsyncTask<object>? asyncTask = null;

        /// <summary>
        /// La structure d'une variable en string
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct StructStr
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string value;
        };

        // Structure to get the result of execute_calculator_code
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct Result
        {
            public double value;
        };

        // Structure to get the result of execute_calculator_code
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct ResponseError
        {
            public int errorCode;
        };

        /// <summary>
        /// L'enum des IDS des variables (permet juste de caster l'ID en Enum)
        /// </summary>
        private enum ID_DEFINITION { }

        /// <summary>
        /// L'enum des IDS des demandes client WASM (permet juste de caster l'ID en Enum)
        /// </summary>
        private enum ID_CLIENT { }

        /// <summary>
        /// L'IDS des requests SimConnect (permet juste de caster l'ID en Enum)
        /// </summary>
        enum ID_REQUEST { }

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
        public async void Open()
        {
            if (!isOpen)
            {
                simConnect = new SimConnect(NAME, IntPtr.Zero, DEFAULT_USER_EVENT_WIN32, null, 0);
                simConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                simConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(SimConnect_OnRecvQuit);
                simConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_OnRecvException);
                simConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);
                simConnect.OnRecvClientData += new SimConnect.RecvClientDataEventHandler(SimConnect_OnRecvClientData);

                simConnect.ReceiveMessage();

                asyncTask = new(0, typeof(bool));
                await asyncTask.Task;

                isOpen = (bool)asyncTask.Task.Result;
            }

            if (isOpen)
            {
                throw new Exception("Unable to connect to SimConnect with unkwnon error");
            }
            else
            {
                ConnectToWasm();
            }
        }

        /// <summary>
        /// Connecte de SimConnect au module WASM
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ConnectToWasm()
        {
            if (!isOpen || simConnect == null)
            {
                throw new Exception("Unable to connect to WASM module, SimConnect is closed");
            }

            simConnect.MapClientDataNameToID("A320_Cockpit.READ_LVAR", (ID_CLIENT)0);
            simConnect.CreateClientData((ID_CLIENT)0, 256, SIMCONNECT_CREATE_CLIENT_DATA_FLAG.DEFAULT);
            simConnect.AddToClientDataDefinition((ID_DEFINITION)0, 0, 256, 0, 0);


            simConnect.MapClientDataNameToID("A320_Cockpit.LVAR_VALUE", (ID_CLIENT)1);
            simConnect.CreateClientData((ID_CLIENT)1, (uint)Marshal.SizeOf<Result>(), SIMCONNECT_CREATE_CLIENT_DATA_FLAG.DEFAULT);
            simConnect.AddToClientDataDefinition((ID_DEFINITION)1, 0, (uint)Marshal.SizeOf<Result>(), 0, 0);
            simConnect.RegisterStruct<SIMCONNECT_RECV_CLIENT_DATA, Result>((ID_DEFINITION)1);

            simConnect.MapClientDataNameToID("A320_Cockpit.ERROR", (ID_CLIENT)2);
            simConnect.CreateClientData((ID_CLIENT)2, (uint)Marshal.SizeOf<ResponseError>(), SIMCONNECT_CREATE_CLIENT_DATA_FLAG.DEFAULT);
            simConnect.AddToClientDataDefinition((ID_DEFINITION)2, 0, (uint)Marshal.SizeOf<ResponseError>(), 0, 0);
            simConnect.RegisterStruct<SIMCONNECT_RECV_CLIENT_DATA, ResponseError>((ID_DEFINITION)2);

            simConnect.RequestClientData(
                (ID_CLIENT)1,
                (ID_REQUEST)1,
                (ID_DEFINITION)1,
                SIMCONNECT_CLIENT_DATA_PERIOD.ON_SET,
                SIMCONNECT_CLIENT_DATA_REQUEST_FLAG.DEFAULT,
                0, 0, 0
            );

            simConnect.RequestClientData(
                (ID_CLIENT)2,
                (ID_REQUEST)2,
                (ID_DEFINITION)2,
                SIMCONNECT_CLIENT_DATA_PERIOD.ON_SET,
                SIMCONNECT_CLIENT_DATA_REQUEST_FLAG.DEFAULT,
                0, 0, 0
            );
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
        /// Ferme la connexion à SimConnect
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Close()
        {
            simConnect?.Dispose();
            if (IsOpen)
            {
                throw new Exception("Unable to disconnect to SimConnect with unknwon error");
            }
        }

        /// <summary>
        /// Récupère la valeur d'une LVar via le module WASM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lvar"></param>
        /// <exception cref="Exception"></exception>
        public async void ReadVar<T>(Lvar<T> lvar)
        {
            if (!isOpen || simConnect == null)
            {
                throw new Exception("Unable to read LVar, SimConnect is closed");
            }

            StructStr cmd;
            cmd.value = lvar.Name;

            int definition = 0;

            simConnect.SetClientData((ID_CLIENT)0, (ID_DEFINITION)definition, SIMCONNECT_CLIENT_DATA_SET_FLAG.DEFAULT, 0, cmd);
            simConnect.ReceiveMessage();

            asyncTask = new(definition, typeof(double));
            await asyncTask.Task;

            lvar.Value = typeConverter.Convert<T>((double)asyncTask.Task.Result);
            asyncTask = null;
        }

        /// <summary>
        /// Récupère la valeur d'une SimVar
        /// </summary>
        /// <typeparam name="T">Le type de la variable</typeparam>
        /// <param name="simVar">La Simvar à lire</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async void ReadVar<T>(SimVar<T> simVar)
        {
            if (!isOpen || simConnect == null)
            {
                throw new Exception("Unable to read SimVar, SimConnect is closed");
            }

            if (!registeredSimVars.Contains(simVar.Name))
            {
                RegisterSimVar(simVar);
            }

            int definition = registeredSimVars.IndexOf(simVar.Name);
            simConnect.RequestDataOnSimObjectType((ID_DEFINITION)definition, (ID_DEFINITION)definition, 0, SIMCONNECT_SIMOBJECT_TYPE.ALL);

            asyncTask = new(definition, typeof(T));
            await asyncTask.Task;

            simVar.Value = typeConverter.Convert<T>((double)asyncTask.Task.Result);
            asyncTask = null;
        }

        /// <summary>
        /// Callback de récéption des valeurs des variables LVar via le module WASM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SimConnect_OnRecvClientData(SimConnect sender, SIMCONNECT_RECV_CLIENT_DATA data)
        {
            if (asyncTask != null)
            {
                switch (data.dwRequestID)
                {
                    case 1:
                        Result exeResult = (Result)data.dwData[0];
                        Console.WriteLine("----> LVar value : " + exeResult.value);
                        double value = exeResult.value;
                        asyncTask.TrySetResult(value);
                        break;
                    case 2:
                        ResponseError error = (ResponseError)data.dwData[0];
                        Console.WriteLine("----> Error code : " + error.errorCode);
                        asyncTask.TrySetResult(double.MinValue);
                        break;
                }
            }
        }

        /// <summary>
        /// Callback de récéption des valeurs des variables SimVar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            if (asyncTask != null)
            {
                uint iRequest = data.dwRequestID;
                uint iObject = data.dwObjectID;

                if (iObject == DEFAUL_ID_OBJECT && iRequest == asyncTask.IdRequest)
                {
                    if(asyncTask.TypeVar == typeof(string))
                    {
                        StructStr result = (StructStr)data.dwData[0];
                        asyncTask.TrySetResult(result.value);
                    }
                    else
                    {
                        double value = (double)data.dwData[0];
                        asyncTask.TrySetResult(value);
                    }
                }
            }
        }

        /// <summary>
        /// Callback d'une reception d'exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SimConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            SIMCONNECT_EXCEPTION eException = (SIMCONNECT_EXCEPTION)data.dwException;
            Console.WriteLine("Exception: " + eException.ToString());
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
            asyncTask?.TrySetResult(true);
        }
    }
}
