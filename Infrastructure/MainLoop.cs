using A320_Cockpit.Adapter.CanBusAdapter.SerialCanBusAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter.FcuipcAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter;
using A320_Cockpit.Domain.CanBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A320_Cockpit.Domain.Connexion;
using Microsoft.FlightSimulator.SimConnect;
using A320_Cockpit.Infrastructure.MsfsVariableUpdater.A32NX_VariableUpdater;
using A320_Cockpit.Infrastructure.Presenter.SendFramePresenter;
using A320_Cockpit.Infrastructure.View.ApplicationTray;
using A320_Cockpit.Adapter.MsfsConnectorAdapter;
using A320_Cockpit.Adapter.LogHandler.SirelogAdapter;
using A320_Cockpit.Adapter.LogHandler;

namespace A320_Cockpit.Infrastructure
{
    /// <summary>
    /// Représete la loop principale pour la mise à jours des variables
    /// </summary>
    public class MainLoop
    {
        private readonly ILogHandlerAdapter logger;
        private readonly System.Windows.Forms.Timer timerUpdateVars;
        private readonly ICanBus canBus;
        private readonly MsfsConnector msfsConnector;

        private readonly A32NX_FcuDisplayUpdater a32NX_FcuDisplayUpdater;
        private readonly A32NX_ElectricityUpdater a32NX_ElectricityUpdater;
        private readonly A32NX_LightIndicatorsUpdater a32NX_LightIndicatorsUpdater;

        /// <summary>
        /// Création de la loop principale pour la mise à jour des variables
        /// </summary>
        /// <param name="canBus"></param>
        /// <param name="simConnector"></param>
        public MainLoop(ApplicationTray applicationTray, ICanBus canBus, MsfsConnector msfsConnector, ILogHandlerAdapter logger)
        {
            this.canBus = canBus;
            this.msfsConnector = msfsConnector;
            this.logger = logger;
            a32NX_FcuDisplayUpdater = new(msfsConnector, canBus, new SystemTrayFramePresenter(applicationTray, logger), logger);
            a32NX_ElectricityUpdater = new(msfsConnector, canBus, new SystemTrayFramePresenter(applicationTray, logger), logger);
            a32NX_LightIndicatorsUpdater = new(msfsConnector, canBus, new SystemTrayFramePresenter(applicationTray, logger), logger);


            timerUpdateVars = new() { Interval = 1 };
            timerUpdateVars.Tick += TimerUpdateVars_Tick;
        }

        /// <summary>
        /// Démarre la mise à jour infini
        /// </summary>
        public void Start()
        {
            timerUpdateVars.Start();
        }

        /// <summary>
        /// La loop d'execution, mise à jour de toutes les variables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerUpdateVars_Tick(object? sender, EventArgs e)
        {
            if (msfsConnector.IsOpen)
            {
                try
                {
                    msfsConnector.StartTransaction();
                    a32NX_FcuDisplayUpdater.Update();
                    a32NX_ElectricityUpdater.Update();
                    a32NX_LightIndicatorsUpdater.Update();
                    msfsConnector.StopTransaction();
                } catch (Exception ex)
                {
                    LogHandlerFactory.Get().Error(ex);
                }
                
            }
        }

    }
}
