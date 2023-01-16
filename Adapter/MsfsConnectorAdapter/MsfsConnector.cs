using A320_Cockpit.Adapter.MsfsConnectorAdapter.FcuipcAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter;
using A320_Cockpit.Domain.Connexion.SimConnector;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter
{
    /// <summary>
    /// Agrégateur de connexion vers MSFS (via FSUIPC et SimConnect)
    /// </summary>
    public class MsfsConnector : ISimConnector
    {
        private readonly FsuipcConnector fsuipc;
        private readonly SimConnectConnector simConnect;
        private readonly List<string> transactionReaded;
        private bool isTransaction;

        /// <summary>
        /// Création des connexions vers MSFS
        /// </summary>
        /// <param name="fsuipcRequester"></param>
        /// <param name="simConnectRequester"></param>
        public MsfsConnector(FsuipcConnector fsuipcRequester, SimConnectConnector simConnectRequester)
        {
            fsuipc = fsuipcRequester;
            simConnect = simConnectRequester;
            transactionReaded = new();
            isTransaction = false;
        }

        /// <summary>
        /// Etat de la connexion
        /// </summary>
        public bool IsOpen
        {
            get { return fsuipc.IsOpen && simConnect.IsOpen; }
        }

        /// <summary>
        /// Démarre le mode transaction. Il ne va lire les variables qu'une seule fois
        /// même si elle est passé à plusieurs reprise à la méthode Update()
        /// </summary>
        public void StartTransaction()
        {
            transactionReaded.Clear();
            isTransaction = true;
        }

        /// <summary>
        /// Arrête le mode transaction
        /// </summary>
        public void StopTransaction()
        {
            transactionReaded.Clear();
            isTransaction = false;
        }

        /// <summary>
        /// Lit une variable (soit sur FSUIPC soit sur SimConnect)
        /// </summary>
        /// <typeparam name="T">Le type de la variable</typeparam>
        /// <param name="variable">La variable à lire</param>
        /// <exception cref="Exception"></exception>
        public void Update<T>(IVar<T> variable)
        {
            if (!isTransaction || !transactionReaded.Contains(variable.Identifier))
            {
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
                }

                if (isTransaction)
                {
                    transactionReaded.Add(variable.Identifier);
                }
            }
        }

        /// <summary>
        /// Ouvre les connexions à MSFS (fsuipc et Simconnect)
        /// </summary>
        public void Open()
        {
            if (!fsuipc.IsOpen)
            {
                fsuipc.Open();
            }

            if (!simConnect.IsOpen)
            {
                simConnect.Open();
            }
        }
    }
}
