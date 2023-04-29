using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.UseCase.ListenEvent;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.EventHandler;
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
        private readonly ILogHandler logger;
        private readonly MsfsSimulatorRepository msfs;
        private readonly CockpitEventDispatcher eventDispatcher;
        private readonly ICockpitRepository cockpitRepository;

        private readonly List<SendPayloadUseCase> sendPayloadUseCases;
        private readonly ListenEventUseCase listenEventUseCase;

        private readonly System.Timers.Timer eventReadTimeout;

        private bool running = false;
        private CockpitEvent cockpitEvent;

        /// <summary>
        /// Création du thread
        /// </summary>
        /// <param name="msfs"></param>
        /// <param name="logger"></param>
        /// <param name="presenter"></param>
        /// <param name="cockpitRepository"></param>
        public MsfsVariableRunner(
            MsfsSimulatorRepository msfs, 
            ILogHandler logger, 
            ICockpitRepository cockpitRepository,
            List<IPayloadRepository> payloadRepositories,
            List<IPayloadEventHandler> payloadEventHandlers,
            ISendPayloadPresenter sendPayloadPresenter,
            IListenEventPresenter listenEventPresenter
        ) {
            this.msfs = msfs;
            this.logger = logger;
            this.cockpitRepository = cockpitRepository;

            sendPayloadUseCases = new();
            foreach (IPayloadRepository payloadRepositoriy in payloadRepositories)
            {
                sendPayloadUseCases.Add(new SendPayloadUseCase(cockpitRepository, payloadRepositoriy, sendPayloadPresenter));
            }

            listenEventUseCase = new ListenEventUseCase(cockpitRepository, listenEventPresenter);
            listenEventUseCase.EventReceived += ListenEventUseCase_EventReceived;

            eventDispatcher = CockpitEventDispatcher.Get(payloadEventHandlers);
            cockpitEvent = CockpitEvent.ALL;
            eventReadTimeout = new() { Interval = 1000 };
            eventReadTimeout.Elapsed += EventReadTimeout_Elapsed;
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
                while (true)
                {
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
                        logger.Error(ex);
                    }

                    msfs.StopTransaction();
                }
            }).Start();
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
