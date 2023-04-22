using A320_Cockpit.Adaptation.Canbus;
using A320_Cockpit.Adaptation.Canbus.CANtact;
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
using A320_Cockpit.Infrastructure.EventHandler.FakeA320.Glareshield;
using A320_Cockpit.Infrastructure.Presenter.Connexion;
using A320_Cockpit.Infrastructure.Presenter.ListenEvent;
using A320_Cockpit.Infrastructure.Presenter.Send;
using A320_Cockpit.Infrastructure.Repository.Cockpit;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Brightness;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Overhead;
using A320_Cockpit.Infrastructure.Repository.Payload.FakeA320.Brightness;
using A320_Cockpit.Infrastructure.Repository.Payload.FakeA320.Glareshield;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure
{
    public class GlobalFactory
    {
        public static bool DEBUG { get; set; }

        private static GlobalFactory? instance;

        public static GlobalFactory Get()
        {
            instance ??=  new GlobalFactory();
            return instance;
        }

        public GlobalFactory() 
        {
            ICanbus canbus = new CANtactAdapter(new System.IO.Ports.SerialPort(), "COM6", 9600, "125Kbit");

            IMsfs msfs;

            if (DEBUG)
            {
                msfs = new FakeMsfs();
            } else
            {
                msfs = new MsfsWasmAdapter();
            }
            
            MsfsSimulatorRepository = new MsfsSimulatorRepository(msfs);


            IPayloadRepository brightnessRepository;
            IPayloadRepository fcuDisplayRepository;
            IPayloadRepository glareshieldIndicatorsRepository;

            if (DEBUG)
            {
                brightnessRepository = new FakeA320BrightnessRepository();
                fcuDisplayRepository = new FakeA320FcuDisplayRepository();
                glareshieldIndicatorsRepository = new FakeA320GlareshieldIndicatorsRepository();

                List<IPayloadEventHandler> allEvents = new()
                {
                    new FakeA320FcuAltBugEventHandler((FakeA320FcuDisplayRepository)fcuDisplayRepository),
                    new FakeA320FcuHdgBugEventHandler((FakeA320FcuDisplayRepository)fcuDisplayRepository),
                    new FakeA320FcuSpdBugEventHandler((FakeA320FcuDisplayRepository)fcuDisplayRepository),
                    new FakeA320FcuVsBugEventHandler((FakeA320FcuDisplayRepository)fcuDisplayRepository)
                };
                PayloadEventHandlers = allEvents;
            } else
            {
                brightnessRepository = new A32nxBrightnessRepository(MsfsSimulatorRepository);
                fcuDisplayRepository = new A32nxFcuDisplayRepository(MsfsSimulatorRepository);
                glareshieldIndicatorsRepository = new A32nxGlareshieldIndicatorsRepository(MsfsSimulatorRepository);

                List<IPayloadEventHandler> allEvents = new()
                {
                    new A32nxFcuBugEventHandler(MsfsSimulatorRepository),
                };
                PayloadEventHandlers = allEvents;
            }
            

            Log = new SirelogAdapter("");

            SendPayloadPresenter = new TraySendPresenter(Log);
            ConnexionPresenter = new TrayConnexionPresenter(Log);
            ListenEventPresenter = new TrayListenEventPresenter();

            CockpitRepository = new SerialBusCockpitRepository(canbus);
            
            SimulatorConnexionRepository = MsfsSimulatorRepository;


            List<IPayloadRepository> allRepository = new()
            {
                brightnessRepository,
                fcuDisplayRepository,
                glareshieldIndicatorsRepository
            };
            PayloadRepositories = allRepository;
        }

        public ILogHandler Log { get; private set; }
        public ICockpitRepository CockpitRepository { get; private set; }
        public List<IPayloadRepository> PayloadRepositories { get; private set; }
        public List<IPayloadEventHandler> PayloadEventHandlers { get; private set; }
        public ISimulatorConnexionRepository SimulatorConnexionRepository { get; private set; }
        public MsfsSimulatorRepository MsfsSimulatorRepository { get; private set; }

        public ISendPayloadPresenter SendPayloadPresenter { get; private set; }
        public IConnexionPresenter ConnexionPresenter { get; private set; }
        public IListenEventPresenter ListenEventPresenter { get; private set; } 
    }
}
