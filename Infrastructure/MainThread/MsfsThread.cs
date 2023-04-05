using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload.Glareshield;
using A320_Cockpit.Domain.UseCase.Send;
using A320_Cockpit.Domain.UseCase.Send.SendPayload.Brightness;
using A320_Cockpit.Domain.UseCase.Send.SendPayload.Glareshield;
using A320_Cockpit.Domain.UseCase.Send.SendPayload.Overhead;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Brightness;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Overhead;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.MainThread
{
    /// <summary>
    /// Le thread principale pour la communication avec MSFS2020
    /// </summary>
    public class MsfsThread : IMainThread
    {

        private readonly ILogHandler logger;
        private readonly MsfsSimulatorRepository msfs;
        private bool running = false;
        private readonly List<SendUseCase> sendUseCases;

        /// <summary>
        /// Création du thread
        /// </summary>
        /// <param name="msfs"></param>
        /// <param name="logger"></param>
        /// <param name="presenter"></param>
        /// <param name="cockpitRepository"></param>
        public MsfsThread(MsfsSimulatorRepository msfs, ILogHandler logger, ISendPresenter presenter, ICockpitRepository cockpitRepository)
        {
            this.msfs = msfs;
            this.logger = logger;

            // La liste de toutes les mises à jours à faire
            sendUseCases = new()
            {
                new SendFcuDisplay(cockpitRepository, presenter, new A32nxFcuDisplayRepository(msfs)),
                new SendGlareshieldIndicators(cockpitRepository, presenter, new A32nxGlareshieldIndicatorsRepository(msfs)),
                new SendBrightness(cockpitRepository, presenter, new A32nxBrightnessRepository(msfs)),
                new SendLightIndicator(cockpitRepository, presenter, new A32nxLightIndicatorsRepository(msfs))
            };
        }

        /// <summary>
        /// Arrête le thread
        /// </summary>
        public void Stop()
        {
            running = false;
        }

        /// <summary>
        /// Demarre le thread et met à jour les variables
        /// </summary>
        public void Start() 
        {
            running = true;
            new Thread(() =>
            {
                while(true)
                {
                    if(!running)
                    {
                        // Il a été demandé d'arrêter le thread
                        break;
                    }

                    if(!msfs.IsOpen)
                    {
                        // La connexion n'est pas établie on attend un peu avant de réessayer
                        Thread.Sleep(1000);
                        continue;
                    }
                    
                    // Mise à jour
                    try
                    {
                        // Demarre une transaction (pour s'assurer de mettre à jour une seule fois la même variable par tour de boucle)
                        msfs.StartTransaction();

                        foreach(SendUseCase sendUseCase in sendUseCases)
                        {
                            // Mise à jour de toutes les variables
                            sendUseCase.Exec();
                        }

                        msfs.StopTransaction();
                    } catch(Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }).Start();
        }

    }
}
