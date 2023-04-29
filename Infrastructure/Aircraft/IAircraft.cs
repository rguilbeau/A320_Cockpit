using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.Repository.Simulator;
using A320_Cockpit.Domain.UseCase.Connexion;
using A320_Cockpit.Domain.UseCase.ListenEvent;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.EventHandler;
using A320_Cockpit.Infrastructure.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Aircraft
{
    /// <summary>
    /// Interface obligeant chaque avion à charger leurs dépendences et instancier les
    /// classe nécéssaire à l'application
    /// </summary>
    public interface IAircraft
    {
        /// <summary>
        /// Le runner de l'avion
        /// </summary>
        public IRunner Runner { get; }
        /// <summary>
        /// Le repository de la connexion au simulateur
        /// </summary>
        public ISimulatorConnexionRepository SimulatorConnexionRepository { get; }

        /// <summary>
        /// Le repository du cockpit
        /// </summary>
        public ICockpitRepository CockpitRepository { get; }
    }
}
