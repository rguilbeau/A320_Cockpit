using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.Connexion
{
    /// <summary>
    /// Vérification et connexion au cockpit et au simulateur
    /// </summary>
    public class ConnextionUseCase
    {
        private readonly ISimulatorConnexionRepository simulatorRepository;
        private readonly ICockpitRepository cockpitRepository;
        private readonly List<IConnexionPresenter> presenters;

        /// <summary>
        /// Création du UseCase
        /// <param name="simulatorRepository"></param>
        /// <param name="cockpitRepository"></param>
        public ConnextionUseCase(ISimulatorConnexionRepository simulatorRepository, ICockpitRepository cockpitRepository)
        {
            this.simulatorRepository = simulatorRepository;
            this.cockpitRepository = cockpitRepository;
            presenters = new();
        }

        /// <summary>
        /// Execution du UseCase
        /// </summary>
        public void Exec()
        {
            if (!simulatorRepository.IsOpen)
            {
                try
                {
                    simulatorRepository.Open();
                }
                catch (Exception ex)
                {
                    presenters.ForEach(presenter => presenter.AddException(ex));
                }
            }

            if (!cockpitRepository.IsOpen)
            {
                try
                {
                    cockpitRepository.Open();
                    cockpitRepository.ActivePing();
                }
                catch (Exception ex)
                {
                    presenters.ForEach(presenter => presenter.AddException(ex));
                }
            }

            foreach(IConnexionPresenter presenter in presenters) 
            {
                presenters.ForEach(presenter => presenter.Present());
            }
        }

        /// <summary>
        /// Ajoute un autre présenter
        /// </summary>
        /// <param name="presenter"></param>
        public void AddPresenter(IConnexionPresenter presenter)
        {
            presenters.Add(presenter);
        }

        /// <summary>
        /// Supprime un présenter
        /// </summary>
        /// <param name="presenter"></param>
        public void RemovePresenter(IConnexionPresenter presenter) 
        { 
            presenters.Remove(presenter);
        }
    }
}
