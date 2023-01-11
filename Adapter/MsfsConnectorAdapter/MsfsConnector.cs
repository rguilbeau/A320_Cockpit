using A320_Cockpit.Adapter.MsfsConnectorAdapter.FcuipcAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter
{
    internal class MsfsConnector
    {

        private FsuipcConnector fsuipc;

        private SimConnectConnector simConnect;


        public static MsfsConnector CreateConnection()
        {

            FsuipcConnector fsuipcRequester = new(new TypeConverter());
            fsuipcRequester.Open();


            SimConnect simConnect = new SimConnect("A320 Cockpit", IntPtr.Zero, SimConnectConnector.DEFAULT_USER_EVENT_WIN32, null, 0);
            SimConnectConnector simConnectRequester = new(simConnect, new TypeConverter());
            simConnectRequester.Open();

            return new MsfsConnector(fsuipcRequester, simConnectRequester);
        }

        public MsfsConnector(FsuipcConnector fsuipcRequester, SimConnectConnector simConnectRequester)
        {
            fsuipc = fsuipcRequester;
            simConnect = simConnectRequester;
        }

        public bool Request<T>(IVar<T> variable)
        {
            bool hasMuted = false;
            T? value = default;

            switch (variable)
            {
                case Lvar<T> lvar:
                    value = fsuipc.ReadLvar(lvar);
                    break;
                case SimVar<T> simVar:
                    value = simConnect.ReadSimVar(simVar);
                    break;
            }

            if (variable.Value != null && !variable.Value.Equals(value))
            {
                variable.Value = value;
                hasMuted = true;
            }

            return hasMuted;
        }
    }
}
