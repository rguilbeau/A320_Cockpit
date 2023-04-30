using A320_Cockpit.Adaptation.Canbus.ArduinoSerialCan;
using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.Repository.Simulator;
using A320_Cockpit.Adaptation.Msfs.FakeMsfs;
using A320_Cockpit.Infrastructure.Simulator.Repository;
using A320_Cockpit.Infrastructure.Cockpit.Repository;
using A320_Cockpit.Infrastructure.Cockpit.EventHandlerDispatcher;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.EventHandler.Glareshield;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.Repository.Brightness;
using A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.Repository.Glareshield;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320
{
    /// <summary>
    /// Classe permettant de charger toutes les dépendences nécessaire au FakeA320 (debug)
    /// </summary>
    public class FakeA320 : IAircraft
    {
        private readonly MsfsSimulatorRepository msfsSimulatorRepository;
        private readonly ISimulatorConnexionRepository simulatorConnexionRepository;
        private readonly ICockpitRepository cockpitRepository;

        private readonly List<IPayloadRepository> payloadRepositories;
        private readonly List<IPayloadEventHandler> payloadEventHandlers;

        private readonly IRunner runner;

        public const string NAME = "Debug";

        /// <summary>
        /// Chargement des dépendences liées au FakeA320 (debug)
        /// </summary>
        /// <param name="comPort"></param>
        public FakeA320(string comPort)
        {
            IMsfs msfs = new FakeMsfs();
            msfsSimulatorRepository = new(msfs);
            simulatorConnexionRepository = msfsSimulatorRepository;
            cockpitRepository = new SerialBusCockpitRepository(new ArduinoSerialCanAdapter(new System.IO.Ports.SerialPort(), comPort));


            payloadRepositories = new()
            {
                new FakeA320BrightnessPanelRepository(),
                new FakeA320BrightnessSevenSegementsRepository(),
                new FakeA320FcuDisplayRepository(),
                new FakeA320GlareshieldIndicatorsRepository()
            };

            payloadEventHandlers = new()
            {
                new FakeA320FcuBugEventHandler(new FakeA320FcuDisplayRepository()),
                new FakeA320FcuGlareshieldButtonsEventHandler(new FakeA320GlareshieldIndicatorsRepository(), new FakeA320FcuDisplayRepository())
            };

            runner = new MsfsVariableRunner(
                msfsSimulatorRepository,
                cockpitRepository,
                payloadRepositories,
                payloadEventHandlers
            );
        }

        /// <summary>
        /// Le repository de connexion au simulateur du FakeA320 (debug)
        /// </summary>
        public ISimulatorConnexionRepository SimulatorConnexionRepository => simulatorConnexionRepository;

        /// <summary>
        /// Le repository du cockpit du FakeA320 (debug)
        /// </summary>
        public ICockpitRepository CockpitRepository => cockpitRepository;

        /// <summary>
        /// Le runner du FakeA320 (debug)
        /// </summary>
        public IRunner Runner => runner;
    }
}
