using A320_Cockpit.Domain.CanBus;
using A320_Cockpit.Infrastructure.MsfsVariableUpdater.A32NX_VariableUpdater;
using A320_Cockpit.Infrastructure.Presenter.SendFramePresenter;
using A320_Cockpit.Infrastructure.View.ApplicationTray;
using A320_Cockpit.Adapter.LogHandler;
using A320_Cockpit.Adapter.SimulatorHandler;
using System.Diagnostics;

namespace A320_Cockpit.Infrastructure
{
    /// <summary>
    /// Représete la loop principale pour la mise à jours des variables
    /// </summary>
    public class MainLoop
    {
        private readonly ILogHandler logger;
        private readonly ISimulatorHandler simulatorHandler;

        private readonly A32NX_FcuDisplayUpdater a32NX_FcuDisplayUpdater;
        private readonly A32NX_ElectricityUpdater a32NX_ElectricityUpdater;
        private readonly A32NX_LightIndicatorsUpdater a32NX_LightIndicatorsUpdater;

        /// <summary>
        /// Création de la loop principale pour la mise à jour des variables
        /// </summary>
        /// <param name="canBus"></param>
        /// <param name="simConnector"></param>
        public MainLoop(ApplicationTray applicationTray, ICanBus canBus, ISimulatorHandler simulatorHandler, ILogHandler logger)
        {
            this.simulatorHandler = simulatorHandler;
            this.logger = logger;
            a32NX_FcuDisplayUpdater = new(simulatorHandler, canBus, new SystemTrayFramePresenter(applicationTray, logger), logger);
            a32NX_ElectricityUpdater = new(simulatorHandler, canBus, new SystemTrayFramePresenter(applicationTray, logger), logger);
            a32NX_LightIndicatorsUpdater = new(simulatorHandler, canBus, new SystemTrayFramePresenter(applicationTray, logger), logger);
        }

        /// <summary>
        /// Démarre la mise à jour infini
        /// </summary>
        public void Start()
        {
            new Thread(() =>
            {
                while(true)
                {
                    if (simulatorHandler.IsOpen)
                    {
                        try
                        {
                            simulatorHandler.StartTransaction();

                            a32NX_FcuDisplayUpdater.Update();
                            a32NX_ElectricityUpdater.Update();
                            a32NX_LightIndicatorsUpdater.Update();

                            simulatorHandler.StopTransaction();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex);
                        }

                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
                
            }).Start();
        }
    }
}
