using A320_Cockpit.Domain.Enum;
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

        /// <summary>
        /// Création du UseCase
        /// <param name="simulatorRepository"></param>
        /// <param name="cockpitRepository"></param>
        public ConnextionUseCase(ISimulatorConnexionRepository simulatorRepository, ICockpitRepository cockpitRepository)
        {
            this.simulatorRepository = simulatorRepository;
            this.cockpitRepository = cockpitRepository;
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
                    Console.WriteLine("Connexion au simulateur...");
                    simulatorRepository.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            if (!cockpitRepository.IsOpen)
            {
                try
                {
                    Console.WriteLine("Connexion au cockpit...");
                    cockpitRepository.Open();
                    cockpitRepository.ActivePing(FrameId.PING, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
