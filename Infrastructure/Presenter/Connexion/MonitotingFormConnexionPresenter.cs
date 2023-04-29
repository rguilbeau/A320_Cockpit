using A320_Cockpit.Domain.UseCase.Connexion;
using A320_Cockpit.Infrastructure.View.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Presenter.Connexion
{
    /// <summary>
    /// Présenteur de status de connexion pour le form Monitoring
    /// </summary>
    public class MonitotingFormConnexionPresenter : IConnexionPresenter
    {
        private readonly MonitringForm monitringForm;
        private readonly List<Exception> exceptions;
        private bool cockpitStatus;
        private bool simulatorStatus;

        /// <summary>
        /// Création du présenter
        /// </summary>
        /// <param name="monitringForm"></param>
        public MonitotingFormConnexionPresenter(MonitringForm monitringForm)
        {
            this.monitringForm = monitringForm;
            exceptions = new();
        }

        /// <summary>
        /// Ajoute une erreur
        /// </summary>
        /// <param name="exception"></param>
        public void AddException(Exception exception)
        {
            exceptions.Add(exception);
        }

        /// <summary>
        /// Présente les status de connexion
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Present()
        {
            monitringForm.SetStatusCanBus(cockpitStatus);
            monitringForm.SetStatusSimulator(simulatorStatus);

            foreach (Exception exception in exceptions)
            {
                monitringForm.AddError(exception);
            }
        }

        /// <summary>
        /// La liste des erreurs
        /// </summary>
        public List<Exception> Exceptions => exceptions;

        /// <summary>
        /// Le status du simulateur
        /// </summary>
        public bool SimulatorStatus { get => simulatorStatus; set => simulatorStatus = value; }
        /// <summary>
        /// Le status du cockpit (CAN bus)
        /// </summary>
        public bool CockpitStatus { get => cockpitStatus; set => cockpitStatus = value; }
    }
}
