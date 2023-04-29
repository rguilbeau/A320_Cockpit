using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.Connexion
{

    /// <summary>
    /// Présenteur dédié à la connexion au simulateur et au cockpit
    /// </summary>
    public interface IConnexionPresenter
    {
        /// <summary>
        /// Les erreurs de connexion
        /// </summary>
        public List<Exception> Exceptions { get; }

        /// <summary>
        /// Ajoute une erreur de connexion
        /// </summary>
        /// <param name="exception"></param>
        public void AddException(Exception exception);

        /// <summary>
        /// Le status de connexion du simulateur
        /// </summary>
        public bool SimulatorStatus { get; set; }

        /// <summary>
        /// Le status du cockit (CAN bus)
        /// </summary>
        public bool CockpitStatus { get; set; }

        /// <summary>
        /// Présente les résultats
        /// </summary>
        public void Present();
    }
}
