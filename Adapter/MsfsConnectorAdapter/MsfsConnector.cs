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

        private readonly FsuipcConnector fsuipc;

        private readonly SimConnectConnector simConnect;


        public static MsfsConnector CreateConnection()
        {

            FsuipcConnector fsuipcRequester = FsuipcConnector.Get();
            fsuipcRequester.Open();

            SimConnectConnector simConnectRequester = SimConnectConnector.Get();
            simConnectRequester.Open();

            return new MsfsConnector(fsuipcRequester, simConnectRequester);
        }

        public MsfsConnector(FsuipcConnector fsuipcRequester, SimConnectConnector simConnectRequester)
        {
            fsuipc = fsuipcRequester;
            simConnect = simConnectRequester;
        }

        public bool Update<T>(IVar<T> variable)
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
