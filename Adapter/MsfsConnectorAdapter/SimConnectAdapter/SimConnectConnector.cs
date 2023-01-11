using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter
{
    internal class SimConnectConnector
    {

        public const int DEFAULT_USER_EVENT_WIN32 = 0x402;

        private const int DEFAUL_ID_OBJECT = 1;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct StructStr
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string sValue;
        };

        private enum ID_DEFINITION { }

        private SimConnect simConnect;

        private readonly List<string> registeredSimVars;

        private bool isOpen;

        private TypeConverter typeConverter;

        public SimConnectConnector(SimConnect simConnect, TypeConverter typeConverter)
        {
            isOpen = false;
            this.simConnect = simConnect;
            this.typeConverter = typeConverter;
            registeredSimVars = new List<string>();
        }


        public bool IsOpen
        {
            get { return isOpen; }
        }

        public void Open()
        {
            if (!isOpen)
            {
                simConnect = new SimConnect("A320 Cockpit", IntPtr.Zero, DEFAULT_USER_EVENT_WIN32, null, 0);
                simConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                simConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(SimConnect_OnRecvQuit);
                simConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_OnRecvException);
                simConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);

                while (!IsOpen)
                {
                    simConnect.ReceiveMessage();
                    Thread.Yield();
                }
            }
        }

        public T? ReadSimVar<T>(SimVar<T> simVar)
        {
            T? value = default;
            Console.WriteLine("Read sim var " + IsOpen);

            if (IsOpen)
            {
                if (!registeredSimVars.Contains(simVar.Name))
                {
                    RegisterSimVar(simVar);
                }

                int definition = registeredSimVars.IndexOf(simVar.Name);
                simConnect.RequestDataOnSimObjectType((ID_DEFINITION)definition, (ID_DEFINITION)definition, 0, SIMCONNECT_SIMOBJECT_TYPE.ALL);

                readed = false;
                currentreadType = typeof(T);
                int hintReceive = 0;
                while (!readed)
                {
                    hintReceive++;
                    simConnect.ReceiveMessage();
                    Thread.Yield();
                }

                Console.WriteLine("hint: " + hintReceive);

                if (currentReadValue != null)
                {
                    if (!typeof(T).Equals(typeof(string)))
                    {
                        value = typeConverter.Convert<T>((double)currentReadValue);
                    }
                    else
                    {
                        value = (T?)currentReadValue;
                    }
                }
            }

            return value;
        }

        private bool readed = false;
        private int currentIdRequest = 0;
        private object? currentReadValue = default;
        private Type? currentreadType = default;

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            if (!readed)
            {
                uint iRequest = data.dwRequestID;
                uint iObject = data.dwObjectID;

                if (iObject == DEFAUL_ID_OBJECT && iRequest == currentIdRequest)
                {
                    if (currentreadType != null && currentreadType == typeof(string))
                    {
                        StructStr result = (StructStr)data.dwData[0];
                        currentReadValue = result.sValue;
                    }
                    else
                    {
                        double value = (double)data.dwData[0];
                        currentReadValue = value;
                    }

                    readed = true;
                }
            }

        }

        private void RegisterSimVar<T>(SimVar<T> simVar)
        {
            if (IsOpen)
            {
                registeredSimVars.Add(simVar.Name);
                int definition = registeredSimVars.IndexOf(simVar.Name);

                if (typeof(T).Equals(typeof(string)))
                {
                    simConnect.AddToDataDefinition((ID_DEFINITION)definition, simVar.Name, string.Empty, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                    simConnect.RegisterDataDefineStruct<StructStr>((ID_DEFINITION)definition);
                }
                else
                {
                    simConnect.AddToDataDefinition((ID_DEFINITION)definition, simVar.Name, string.Empty, SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                    simConnect.RegisterDataDefineStruct<double>((ID_DEFINITION)definition);
                }
            }
        }

        private void SimConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            int i = 0;
        }

        private void SimConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            int i = 0;
        }

        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Console.WriteLine("connected received");
            isOpen = true;
            /*Console.WriteLine("connected received");
            if(_connectedListener != null)
            {
                _connectedListener.IsConnected = true;
                _connectedListener.RunSynchronously();
            }*/
        }

    }
}
