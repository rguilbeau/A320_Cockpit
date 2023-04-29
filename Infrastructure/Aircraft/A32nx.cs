using A320_Cockpit.Adaptation.Canbus.ArduinoSerialCan;
using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Adaptation.Log.Sirelog;
using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Adaptation.Msfs.MsfsWasm;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.Repository.Simulator;
using A320_Cockpit.Domain.UseCase.Connexion;
using A320_Cockpit.Domain.UseCase.ListenEvent;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.EventHandler;
using A320_Cockpit.Infrastructure.EventHandler.A32nx.Glareshield;
using A320_Cockpit.Infrastructure.Presenter.Connexion;
using A320_Cockpit.Infrastructure.Presenter.ListenEvent;
using A320_Cockpit.Infrastructure.Presenter.Send;
using A320_Cockpit.Infrastructure.Repository.Cockpit;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Brightness;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using A320_Cockpit.Infrastructure.Runner;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Aircraft
{
    /// <summary>
    /// Classe permettant de charger toutes les dépendences nécessaire à l'A32NX
    /// </summary>
    public class A32nx : IAircraft
    {
        private readonly ConnextionUseCase connextionUseCase;
        private readonly ListenEventUseCase listenEventUseCase;
        private readonly IRunner runner;
        private readonly ILogHandler logger;

        /// <summary>
        /// Chargement des dépendences liées à l'A32NX
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="comPort"></param>
        public A32nx(ILogHandler logger, string comPort)
        {
            this.logger = logger;
            IMsfs msfs = new MsfsWasmAdapter();
            MsfsSimulatorRepository msfsSimulatorRepository = new(msfs);
            ISimulatorConnexionRepository simulatorConnexionRepository = msfsSimulatorRepository;
            ICockpitRepository cockpitRepository = new SerialBusCockpitRepository(new ArduinoSerialCanAdapter(new System.IO.Ports.SerialPort(), comPort));

            IConnexionPresenter connexionPresenter = new TrayConnexionPresenter(logger);
            connextionUseCase = new ConnextionUseCase(simulatorConnexionRepository, cockpitRepository, connexionPresenter);

            IListenEventPresenter listenEventPresenter = new TrayListenEventPresenter();
            listenEventUseCase = new ListenEventUseCase(cockpitRepository, listenEventPresenter);

            List<IPayloadRepository> payloadRepositories = new()
            {
                new A32nxBrightnessRepository(msfsSimulatorRepository),
                new A32nxFcuDisplayRepository(msfsSimulatorRepository),
                new A32nxGlareshieldIndicatorsRepository(msfsSimulatorRepository)
            };

            List<IPayloadEventHandler> payloadEventHandlers = new()
            {
                new A32nxFcuBugEventHandler(msfsSimulatorRepository),
                new A32nxFcuGlareshieldButtonsEventHandler(msfsSimulatorRepository)
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
        /// Le use case de connexion de l'a32NX
        /// </summary>
        public ConnextionUseCase ConnextionUseCase => connextionUseCase;

        /// <summary>
        /// Le use case de l'écoute des evenements du cockpit de l'A32NX
        /// </summary>
        public ListenEventUseCase ListenEventUseCase => listenEventUseCase;

        /// <summary>
        /// Le runner de l'A32NX
        /// </summary>
        public IRunner Runner => runner;

        /// <summary>
        /// Le logger de l'A32NX
        /// </summary>
        public ILogHandler Logger => logger;
    }
}
