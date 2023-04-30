using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Simulator;

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
