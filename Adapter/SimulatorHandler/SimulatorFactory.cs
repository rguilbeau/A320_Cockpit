using A320_Cockpit.Adapter.SimulatorHandler;
using A320_Cockpit.Adapter.SimulatorHandler.MsfsConnectorAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.SimulatorHandler
{
    /// <summary>
    /// Factory pour la création de la connexion au simulateur de vol
    /// </summary>
    public class SimulatorFactory
    {
        private static ISimulatorHandler? simulatorHandler;

        /// <summary>
        /// Singleton de récupération de la connexion au simulateur de vol
        /// </summary>
        /// <returns></returns>
        public static ISimulatorHandler Get()
        {
            if (simulatorHandler == null)
            {
                simulatorHandler = new MsfsConnector(new TypeConverter());
            }
            return simulatorHandler;
        }

    }
}
