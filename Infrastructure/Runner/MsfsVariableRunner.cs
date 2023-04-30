using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.UseCase.Connexion;
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
        private readonly MsfsSimulatorRepository msfs;
        private readonly CockpitEventDispatcher eventDispatcher;
        private readonly ICockpitRepository cockpitRepository;

        private readonly List<SendPayloadUseCase> sendPayloadUseCases;
        private readonly ListenEventUseCase listenEventUseCase;
        private readonly ConnextionUseCase connexionUseCase;

        private readonly System.Timers.Timer eventReadTimeout;
        private readonly System.Timers.Timer connexionTimer;

        private bool running = false;
        private CockpitEvent cockpitEvent;

        private readonly Stopwatch monitoring;

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

            connexionUseCase = new(msfs, cockpitRepository);

            eventDispatcher = CockpitEventDispatcher.Get(payloadEventHandlers);
            cockpitEvent = CockpitEvent.ALL;
            eventReadTimeout = new() { Interval = 1000 };
            eventReadTimeout.Elapsed += EventReadTimeout_Elapsed;

            connexionTimer = new() { Interval = 5000 };
            connexionTimer.Elapsed += ConexionTimer_Elappsed;

            monitoring = new();
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
            connexionUseCase.Exec();
            connexionTimer.Start();

            running = true;
            listenEventUseCase.Listen();

            new Thread(() =>
            {
                Thread.Sleep(2000);
                while (true)
                {
                    monitoring.Restart();

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
                        Console.WriteLine(ex.ToString());
                    }

                    int cursorLeft = Console.CursorLeft;
                    int cursorTop = Console.CursorTop;
                    string header = monitoring.ElapsedMilliseconds + "ms";
                    Console.Write(header + new string(' ', Console.WindowWidth - header.Length));
                    Console.SetCursorPosition(cursorLeft, cursorTop);

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

            if(!listenEventArgs.IsPing)
            {
                cockpitRepository.SuspendPing();
            }

            eventDispatcher.Dispatch(listenEventArgs.Event, listenEventArgs.Value);

            if(!listenEventArgs.IsPing)
            {
                cockpitEvent = listenEventArgs.Event; // Priorité sur cet evenement
                eventReadTimeout.Stop();
                eventReadTimeout.Start();
            }

            msfs.ResumeRead();
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
            cockpitRepository.ResumePing();
            eventReadTimeout.Stop();
        }

        /// <summary>
        /// Vérification de la connexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConexionTimer_Elappsed(object ?sender, ElapsedEventArgs e)
        {
            connexionUseCase.Exec();
        }
    }
}
