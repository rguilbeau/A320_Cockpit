using A320_Cockpit.Adaptation.Canbus;
using A320_Cockpit.Adaptation.Canbus.CANtact;
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
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Overhead;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure
{
    public class GlobalFactory
    {

        private static GlobalFactory? instance;

        public static GlobalFactory Get()
        {
            instance ??=  new GlobalFactory();
            return instance;
        }

        public GlobalFactory() 
        {
            ICanbus canbus = new CANtactAdapter(new System.IO.Ports.SerialPort(), "COM6", 9600, "125Kbit");
            IMsfs msfs = new MsfsWasmAdapter();
            MsfsSimulatorRepository = new MsfsSimulatorRepository(msfs);

            A32nxBrightnessRepository a32NxBrightnessRepository = new(MsfsSimulatorRepository);
            A32nxFcuDisplayRepository a32NxFcuDisplayRepository = new(MsfsSimulatorRepository);
            A32nxGlareshieldIndicatorsRepository a32NxGlareshieldIndicatorsRepository = new(MsfsSimulatorRepository);

            Log = new SirelogAdapter("");

            SendPayloadPresenter = new TraySendPresenter(Log);
            ConnexionPresenter = new TrayConnexionPresenter(Log);
            ListenEventPresenter = new TrayListenEventPresenter();

            CockpitRepository = new SerialBusCockpitRepository(canbus);
            
            SimulatorConnexionRepository = MsfsSimulatorRepository;


            List<IPayloadRepository> allRepository = new()
            {
                a32NxBrightnessRepository,
                a32NxFcuDisplayRepository,
                a32NxGlareshieldIndicatorsRepository
            };
            PayloadRepositories = allRepository;

            List<IPayloadEventHandler> allEvents = new()
            {
                new A32nxFcuBugEventHandler(MsfsSimulatorRepository),
            };
            PayloadEventHandlers = allEvents;
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
