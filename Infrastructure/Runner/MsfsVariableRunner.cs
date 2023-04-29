using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.UseCase.ListenEvent;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.EventHandler;
using A320_Cockpit.Infrastructure.Presenter.Monitoring;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System.Diagnostics;
using System.Timers;

namespace A320_Cockpit.Infrastructure.Runner
{
    /// <summary>
    /// Le thread principale pour la communication avec MSFS2020
    /// </summary>
    public class MsfsVariableRunner : IRunner
    {
        private readonly MsfsSimulatorRepository msfs;
        private readonly CockpitEventDispatcher eventDispatcher;
        private readonly ICockpitRepository cockpitRepository;

        private readonly List<SendPayloadUseCase> sendPayloadUseCases;
        private readonly ListenEventUseCase listenEventUseCase;

        private MonitoringPresenter? monitoringPresenter;

        private readonly System.Timers.Timer eventReadTimeout;

        private bool running = false;
        private CockpitEvent cockpitEvent;

        private readonly Stopwatch stopWatch;

        /// <summary>
        /// Création du thread
        /// </summary>
        /// <param name="msfs"></param>
        /// <param name="presenter"></param>
        /// <param name="cockpitRepository"></param>
        public MsfsVariableRunner(
            MsfsSimulatorRepository msfs, 
            ICockpitRepository cockpitRepository,
            List<IPayloadRepository> payloadRepositories,
            List<IPayloadEventHandler> payloadEventHandlers
        ) {
            this.msfs = msfs;
            this.cockpitRepository = cockpitRepository;

            sendPayloadUseCases = new();
            payloadRepositories.ForEach(payloadRepository => sendPayloadUseCases.Add(new(cockpitRepository, payloadRepository)));

            listenEventUseCase = new ListenEventUseCase(cockpitRepository);
            listenEventUseCase.EventReceived += ListenEventUseCase_EventReceived;

            eventDispatcher = CockpitEventDispatcher.Get(payloadEventHandlers);
            cockpitEvent = CockpitEvent.ALL;
            eventReadTimeout = new() { Interval = 1000 };
            eventReadTimeout.Elapsed += EventReadTimeout_Elapsed;
            stopWatch = new();
        }

        /// <summary>
        /// Arrête le thread
        /// </summary>
        public void Stop()
        {
            running = false;
            listenEventUseCase.Stop();
            listenEventUseCase.EventReceived -= ListenEventUseCase_EventReceived;
        }

        /// <summary>
        /// Demarre le thread et met à jour les variables
        /// </summary>
        public void Start() 
        {
            running = true;
            listenEventUseCase.Listen();

            new Thread(() =>
            {
                Thread.Sleep(2000);
                while (true)
                {
                    stopWatch.Restart();

                    msfs.StartTransaction();

                    // Arrêt du thread
                    if (!running) { break; }

                    // Les connexions ne sont pas ouvertes, on attend un peut
                    if (!msfs.IsOpen || !cockpitRepository.IsOpen) { Thread.Sleep(1000); continue;}

                    try
                    {
                        // Mise à jour des payload
                        foreach (SendPayloadUseCase sendUseCase in sendPayloadUseCases)
                        {
                            sendUseCase.Exec(cockpitEvent);
                        }
                    }
                    catch (Exception ex)
                    {
                        if(monitoringPresenter != null)
                        {
                            monitoringPresenter.Exception = ex;
                        }
                    }

                    msfs.StopTransaction();

                    stopWatch.Stop();

                    if (monitoringPresenter != null)
                    {
                        monitoringPresenter.Timing = stopWatch.ElapsedMilliseconds;
                        monitoringPresenter.Present();
                    }
                }
            }).Start();
        }

        /// <summary>
        /// Ajout des présenter d'envoi de frame
        /// </summary>
        /// <param name="sendPayloadPresenter"></param>
        public void AddSendPayloadPresenter(ISendPayloadPresenter sendPayloadPresenter)
        {
            sendPayloadUseCases.ForEach(sendPayloadUseCases => sendPayloadUseCases.AddPresenter(sendPayloadPresenter));
        }

        /// <summary>
        /// Ajoute des présenter d'écoute de frame
        /// </summary>
        /// <param name="listenEventPresenter"></param>
        public void AddListenEventPresenter(IListenEventPresenter listenEventPresenter)
        {
            listenEventUseCase.AddPresenter(listenEventPresenter);
        }

        /// <summary>
        /// Ajout des présenter d'envoi de frame
        /// </summary>
        /// <param name="sendPayloadPresenter"></param>
        public void RemoveSendPayloadPresenter(ISendPayloadPresenter sendPayloadPresenter)
        {
            sendPayloadUseCases.ForEach(sendPayloadUseCases => sendPayloadUseCases.RemovePresenter(sendPayloadPresenter));
        }

        /// <summary>
        /// Ajoute des présenter d'écoute de frame
        /// </summary>
        /// <param name="listenEventPresenter"></param>
        public void RemoveListenEventPresenter(IListenEventPresenter listenEventPresenter)
        {
            listenEventUseCase.RemovePresenter(listenEventPresenter);
        }

        /// <summary>
        /// Ajoute le presenter du monitoring
        /// </summary>
        /// <param name="monitoringPresenter"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddMonitoringPresenter(MonitoringPresenter monitoringPresenter)
        {
            this.monitoringPresenter = monitoringPresenter;
        }

        /// <summary>
        /// Supprime le presenter du montoring
        /// </summary>
        /// <param name="monitoringPresenter"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RemoveMonitoringPresenter(MonitoringPresenter monitoringPresenter)
        {
            this.monitoringPresenter = null;
        }

        /// <summary>
        /// Reception d'un nouvel event du cockpit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListenEventUseCase_EventReceived(object? sender, ListenEventArgs listenEventArgs)
        {
            msfs.StopRead();
            eventDispatcher.Dispatch(listenEventArgs.Event, listenEventArgs.Value);
            cockpitEvent = listenEventArgs.Event; // Priorité sur cet evenement
            msfs.ResumeRead();
            eventReadTimeout.Stop();
            eventReadTimeout.Start();
        }

        /// <summary>
        /// Fin de la priorité de l'évenement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void EventReadTimeout_Elapsed(object? sender, ElapsedEventArgs e)
        {
            cockpitEvent = CockpitEvent.ALL;
            eventReadTimeout.Stop();
        }
    }
}
