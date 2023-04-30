using A320_Cockpit.Adaptation.Canbus.ArduinoSerialCan;
using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Adaptation.Msfs.MsfsWasm;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.Repository.Simulator;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.EventHandler.Glareshield;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.Repository.Brightness;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx.Repository.Glareshield;
using A320_Cockpit.Infrastructure.Cockpit.EventHandlerDispatcher;
using A320_Cockpit.Infrastructure.Cockpit.Repository;
using A320_Cockpit.Infrastructure.Simulator.Repository;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.A32nx
{
    /// <summary>
    /// Classe permettant de charger toutes les dépendences nécessaire à l'A32NX
    /// </summary>
    public class A32nx : IAircraft
    {
        private readonly MsfsSimulatorRepository msfsSimulatorRepository;
        private readonly ISimulatorConnexionRepository simulatorConnexionRepository;
        private readonly ICockpitRepository cockpitRepository;

        private readonly List<IPayloadRepository> payloadRepositories;
        private readonly List<IPayloadEventHandler> payloadEventHandlers;

        private readonly IRunner runner;

        public const string NAME = "FlyByWire - A32NX";

        /// <summary>
        /// Chargement des dépendences liées à l'A32NX
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="comPort"></param>
        public A32nx(string comPort)
        {
            IMsfs msfs = new MsfsWasmAdapter();
            msfsSimulatorRepository = new(msfs);
            simulatorConnexionRepository = msfsSimulatorRepository;
            cockpitRepository = new SerialBusCockpitRepository(new ArduinoSerialCanAdapter(new System.IO.Ports.SerialPort(), comPort));


            payloadRepositories = new()
            {
                new A32nxBrightnessPanelRepository(msfsSimulatorRepository),
                new A32nxBrightnessSevenSegmentsRepository(msfsSimulatorRepository),
                new A32nxFcuDisplayRepository(msfsSimulatorRepository),
                new A32nxGlareshieldIndicatorsRepository(msfsSimulatorRepository)
            };

            payloadEventHandlers = new()
            {
                new A32nxFcuBugEventHandler(msfsSimulatorRepository),
                new A32nxFcuGlareshieldButtonsEventHandler(msfsSimulatorRepository)
            };

            runner = new MsfsVariableRunner(
                msfsSimulatorRepository,
                cockpitRepository,
                payloadRepositories,
                payloadEventHandlers
            );
        }

        /// <summary>
        /// Le repository de connexion au simulateur de l'A32NX
        /// </summary>
        public ISimulatorConnexionRepository SimulatorConnexionRepository => simulatorConnexionRepository;

        /// <summary>
        /// Le repository du cockpit de l'A32NX
        /// </summary>
        public ICockpitRepository CockpitRepository => cockpitRepository;

        /// <summary>
        /// Le runner de l'A32NX
        /// </summary>
        public IRunner Runner => runner;
    }
}
