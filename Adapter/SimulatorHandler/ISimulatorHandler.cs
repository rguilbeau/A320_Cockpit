using A320_Cockpit.Adapter.SimulatorHandler.Model;
using A320_Cockpit.Domain.Connexion.SimConnector;

namespace A320_Cockpit.Adapter.SimulatorHandler
{
    /// <summary>
    /// Interface des adapteurs de connexion au simulateur de vol
    /// </summary>
    public interface ISimulatorHandler : ISimulatorConnector
    {
        /// <summary>
        /// Ferme la connexion
        /// </summary>
        public void Close();

        /// <summary>
        /// Lit une variable locale du simulateur de vol
        /// </summary>
        /// <typeparam name="T">Le type de la variable</typeparam>
        /// <param name="variable">La variable à lire</param>
        public void Read<T>(Lvar<T> variable);

        /// <summary>
        /// Lit une variable "native" du simulateur de vol
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        public void Read<T>(SimVar<T> variable);


        /// <summary>
        /// Démarre une transaction (une transaction ne lit qu'une seule fois la même variable)
        /// </summary>
        public void StartTransaction();

        /// <summary>
        /// Arrête une transaction (une transaction ne lit qu'une seule fois la même variable)
        /// </summary>
        public void StopTransaction();

    }
}
