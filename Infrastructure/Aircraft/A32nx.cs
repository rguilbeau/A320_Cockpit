using A320_Cockpit.Adaptation.Canbus.ArduinoSerialCan;
using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Adaptation.Log.Sirelog;
using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Adaptation.Msfs.FakeMsfs;
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
        private readonly MsfsSimulatorRepository msfsSimulatorRepository;
        private readonly ILogHandler logger;
        private readonly ISimulatorConnexionRepository simulatorConnexionRepository;
        private readonly ICockpitRepository cockpitRepository;

        private readonly List<IPayloadRepository> payloadRepositories;
        private readonly List<IPayloadEventHandler> payloadEventHandlers;

        /// <summary>
        /// Chargement des dépendences liées à l'A32NX
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="comPort"></param>
        public A32nx(ILogHandler logger, string comPort)
        {
            this.logger = logger;
            IMsfs msfs = new FakeMsfs();
            msfsSimulatorRepository = new(msfs);
            simulatorConnexionRepository = msfsSimulatorRepository;
            cockpitRepository = new SerialBusCockpitRepository(new ArduinoSerialCanAdapter(new System.IO.Ports.SerialPort(), comPort));


            payloadRepositories = new()
            {
                new A32nxBrightnessRepository(msfsSimulatorRepository),
                new A32nxFcuDisplayRepository(msfsSimulatorRepository),
                new A32nxGlareshieldIndicatorsRepository(msfsSimulatorRepository)
            };

            payloadEventHandlers = new()
            {
                new A32nxFcuBugEventHandler(msfsSimulatorRepository),
                new A32nxFcuGlareshieldButtonsEventHandler(msfsSimulatorRepository)
            };
        }

        /// <summary>
        /// Le logger du FakeA320 (debug)
        /// </summary>
        public ILogHandler Logger => logger;

        public ISimulatorConnexionRepository SimulatorConnexionRepository => simulatorConnexionRepository;

        public ICockpitRepository CockpitRepository => cockpitRepository;

        public IRunner CreateRunner(IConnexionPresenter connexionPresenter, IListenEventPresenter listenEventPresenter, ISendPayloadPresenter sendPayloadPresenter)
        {
            return new MsfsVariableRunner(
                msfsSimulatorRepository,
                logger,
                cockpitRepository,
                payloadRepositories,
                payloadEventHandlers,
                sendPayloadPresenter,
                listenEventPresenter
            );
        }
    }
}
