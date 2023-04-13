using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.UseCase.ListenEvent;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.EventHandler;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System.Diagnostics;
using System.Threading;


namespace A320_Cockpit.Infrastructure.Runner
{
    /// <summary>
    /// Le thread principale pour la communication avec MSFS2020
    /// </summary>
    public class MsfsVariableRunner : IRunner
    {
        private readonly ILogHandler logger;
        private readonly MsfsSimulatorRepository msfs;
        private bool running = false;
        private readonly List<SendPayloadUseCase> sendPayloadUseCase;
        private readonly ListenEventUseCase listenEventUseCase;
        private readonly Stopwatch stopwatch;
        private readonly CockpitEventDispatcher eventDispatcher;
        private Thread? thread;
        private bool reading = true;

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
            ISendPayloadPresenter presenter, 
            IListenEventPresenter listentEventPresenter, 
            ICockpitRepository cockpitRepository
        ) {
            this.msfs = msfs;
            this.logger = logger;

            sendPayloadUseCase = new();
            foreach(IPayloadRepository payloadRepository in GlobalFactory.Get().PayloadRepositories)
            {
                sendPayloadUseCase.Add(new SendPayloadUseCase(cockpitRepository, payloadRepository, presenter));
            }

            listenEventUseCase = new(cockpitRepository, listentEventPresenter);
            
            eventDispatcher = CockpitEventDispatcher.Get(GlobalFactory.Get().PayloadEventHandlers);
            stopwatch = new();
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
            listenEventUseCase.Listen();

            thread = new Thread(() =>
            {
                listenEventUseCase.EventReceived += ListenEventUseCase_EventReceived;

                while (true)
                {
                    stopwatch.Restart();

                    if (!running)
                    {
                        // Il a été demandé d'arrêter le thread
                        listenEventUseCase.Stop();
                        listenEventUseCase.EventReceived -= ListenEventUseCase_EventReceived;
                        break;
                    }

                    if (!msfs.IsOpen)
                    {
                        // La connexion n'est pas établie on attend un peu avant de réessayer
                        Thread.Sleep(1000);
                        continue;
                    }

                    // Mise à jour
                    try
                    {
                        //A32nxVariables.ReadAll(msfs);

                        if(!reading)
                        {
                            Thread.Sleep(1000);
                            //A32nxVariables.ReadAll(msfs);
                            reading = true;
                        }

                        foreach (SendPayloadUseCase sendUseCase in sendPayloadUseCase)
                        {
                            //sendUseCase.Exec();
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }

                    stopwatch.Stop();
                    //Console.WriteLine("Loop time:" + stopwatch.ElapsedMilliseconds + "ms");
                }
            });
            thread.Start();
        }

        /// <summary>
        /// Recepetion d'un nouvel event du cockpit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListenEventUseCase_EventReceived(object? sender, ListenEventArgs listenEventArgs)
        {
            reading = false;
            for(int i = 0; i < 10; i++)
            {
                eventDispatcher.Dispatch(listenEventArgs.Event, listenEventArgs.Value);
            }

        }
    }
}
