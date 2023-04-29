using A320_Cockpit.Adaptation.Canbus.ArduinoSerialCan;
using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Adaptation.Log.Sirelog;
using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.Repository.Simulator;
using A320_Cockpit.Domain.UseCase.Connexion;
using A320_Cockpit.Domain.UseCase.ListenEvent;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.EventHandler;
using A320_Cockpit.Infrastructure.Presenter.Connexion;
using A320_Cockpit.Infrastructure.Presenter.ListenEvent;
using A320_Cockpit.Infrastructure.Presenter.Send;
using A320_Cockpit.Infrastructure.Repository.Cockpit;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using A320_Cockpit.Infrastructure.Runner;
using A320_Cockpit.Adaptation.Msfs.FakeMsfs;
using A320_Cockpit.Infrastructure.Repository.Payload.FakeA320.Brightness;
using A320_Cockpit.Infrastructure.Repository.Payload.FakeA320.Glareshield;
using A320_Cockpit.Infrastructure.EventHandler.FakeA320.Glareshield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Aircraft
{
    /// <summary>
    /// Classe permettant de charger toutes les dépendences nécessaire au FakeA320 (debug)
    /// </summary>
    public class FakeA320 : IAircraft
    {
        private readonly ConnextionUseCase connextionUseCase;
        private readonly ListenEventUseCase listenEventUseCase;
        private readonly IRunner runner;
        private readonly ILogHandler logger;

        /// <summary>
        /// Chargement des dépendences liées au FakeA320 (debug)
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="comPort"></param>
        public FakeA320(ILogHandler logger, string comPort)
        {
            this.logger = logger;
            IMsfs msfs = new FakeMsfs();
            MsfsSimulatorRepository msfsSimulatorRepository = new(msfs);
            ISimulatorConnexionRepository simulatorConnexionRepository = msfsSimulatorRepository;
            ICockpitRepository cockpitRepository = new SerialBusCockpitRepository(new ArduinoSerialCanAdapter(new System.IO.Ports.SerialPort(), comPort));

            IConnexionPresenter connexionPresenter = new TrayConnexionPresenter(logger);
            connextionUseCase = new ConnextionUseCase(simulatorConnexionRepository, cockpitRepository, connexionPresenter);

            IListenEventPresenter listenEventPresenter = new TrayListenEventPresenter();
            listenEventUseCase = new ListenEventUseCase(cockpitRepository, listenEventPresenter);

            List<IPayloadRepository> payloadRepositories = new()
            {
                new FakeA320BrightnessRepository(),
                new FakeA320FcuDisplayRepository(),
                new FakeA320GlareshieldIndicatorsRepository()
            };

            List<IPayloadEventHandler> payloadEventHandlers = new()
            {
                new FakeA320FcuBugEventHandler(new FakeA320FcuDisplayRepository()),
                new FakeA320FcuGlareshieldButtonsEventHandler(new FakeA320GlareshieldIndicatorsRepository(), new FakeA320FcuDisplayRepository())
            };

            ISendPayloadPresenter sendPayloadPresenter = new TraySendPresenter(logger);
            List<SendPayloadUseCase> sendPayloadUseCases = new();
            foreach (IPayloadRepository payloadRepository in payloadRepositories)
            {
                sendPayloadUseCases.Add(new SendPayloadUseCase(cockpitRepository, payloadRepository, sendPayloadPresenter));
            }

            runner = new MsfsVariableRunner(
                msfsSimulatorRepository,
                logger,
                cockpitRepository,
                listenEventUseCase,
                payloadEventHandlers,
                sendPayloadUseCases
            );
        }

        /// <summary>
        /// Le use case de connexion du FakeA320 (debug)
        /// </summary>
        public ConnextionUseCase ConnextionUseCase => connextionUseCase;

        /// <summary>
        /// Le use case de l'écoute des evenements du cockpit du FakeA320 (debug)
        /// </summary>
        public ListenEventUseCase ListenEventUseCase => listenEventUseCase;

        /// <summary>
        /// Le runner du FakeA320 (debug)
        /// </summary>
        public IRunner Runner => runner;

        /// <summary>
        /// Le logger du FakeA320 (debug)
        /// </summary>
        public ILogHandler Logger => logger;
    }
}
