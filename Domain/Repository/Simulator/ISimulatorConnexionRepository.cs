using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Repository.Simulator
{
    /// <summary>
    /// Repository du simulateur
    /// </summary>
    public interface ISimulatorConnexionRepository
    {
        /// <summary>
        /// Etat de la connexion
        /// </summary>
        public bool IsOpen { get; }

        /// <summary>
        /// Ouvre la connexion au simulateur
        /// </summary>
        public void Open();
    }
}
