using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Repository.Simulator
{
    /// <summary>
    /// Connecteur du simulateur
    /// </summary>
    public interface ISimulatorRepository
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
