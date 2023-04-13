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
        private readonly IConnexionPresenter presenter;

        /// <summary>
        /// Création du UseCase
        /// <param name="simulatorRepository"></param>
        /// <param name="cockpitRepository"></param>
        public ConnextionUseCase(ISimulatorConnexionRepository simulatorRepository, ICockpitRepository cockpitRepository, IConnexionPresenter presenter)
        {
            this.simulatorRepository = simulatorRepository;
            this.cockpitRepository = cockpitRepository;
            this.presenter = presenter;
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
                    presenter.AddException(ex);
                }
            }

            if (!cockpitRepository.IsOpen)
            {
                try
                {
                    cockpitRepository.Open();
                }
                catch (Exception ex)
                {
                    presenter.AddException(ex);
                }
            }

            presenter.Present();
        }
    }
}
