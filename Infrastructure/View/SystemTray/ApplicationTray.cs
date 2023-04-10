using A320_Cockpit.Adaptation.Canbus;
using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.UseCase.Connexion;
using A320_Cockpit.Infrastructure.Runner;
using A320_Cockpit.Infrastructure.Presenter.Connexion;
using A320_Cockpit.Infrastructure.Presenter.Send;
using A320_Cockpit.Infrastructure.Repository.Cockpit;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.View.SystemTray
{
    /// <summary>
    /// Vue dans le System tray
    /// </summary>
    public class ApplicationTray : ApplicationContext
    {
        private readonly NotifyIcon trayIcon;
        private readonly System.Windows.Forms.Timer timerConnexion;

        private readonly IRunner msfsVariableRunner;
        private readonly IRunner cockpitEventRunner;
        private readonly ConnextionUseCase connextionUseCase;

        /// <summary>
        /// Création du système tray
        /// </summary>
        public ApplicationTray()
        {
            connextionUseCase = new(
                new MsfsSimulatorRepository(MsfsFactory.Get()),
                new SerialBusCockpitRepository(CanBusFactory.Get()),
                new TrayConnexionPresenter(this, LogFactory.Get())
            );

            msfsVariableRunner = new MsfsVariableRunner(
                new MsfsSimulatorRepository(MsfsFactory.Get()),
                LogFactory.Get(),
                new TraySendPresenter(this, LogFactory.Get()),
                new SerialBusCockpitRepository(CanBusFactory.Get())
            );

            cockpitEventRunner = new CockpitEventRunner();

            timerConnexion = new() { Interval = 5000 };
            timerConnexion.Tick += TimerConnexion_Tick;
            timerConnexion.Start();

            trayIcon = new()
            {
                Text = AppResources.AppName,
                Icon = AppResources.AppTrayIcon,
                ContextMenuStrip = new ContextMenuStrip()
                {
                    Items = {
                        new ToolStripMenuItem(AppResources.Log, null, Log_OnClick),
                        new ToolStripSeparator(),
                        new ToolStripMenuItem(AppResources.Exit, null, Exit_OnClick)
                    }
                },
                Visible = true,
                BalloonTipText = AppResources.OpenMessage
            };

            trayIcon.ShowBalloonTip(3);

            msfsVariableRunner.Start();
            cockpitEventRunner.Start();
        }

        /// <summary>
        /// Modifie le l'icon du System tray avec la pastille rouge ou verte
        /// </summary>
        /// <param name="success"></param>
        public void ChangeStatus(TrayStatus status)
        {
            trayIcon.Icon = status switch
            {
                TrayStatus.SUCCESS => AppResources.AppTrayIconSuccess,
                TrayStatus.FAILURE => AppResources.AppTrayIconError,
                _ => AppResources.AppTrayIcon,
            };
        }

        /// <summary>
        /// Elève la pastille et la remet juste après
        /// </summary>
        public void BlinkIcon()
        {
            trayIcon.Icon = AppResources.AppTrayIcon;
            System.Windows.Forms.Timer timer = new() { Interval = 20 };
            timer.Tick += (sender, args) =>
            {
                trayIcon.Icon = AppResources.AppTrayIconSuccess;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        /// <summary>
        /// Quitte l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Exit_OnClick(object? sender, EventArgs e)
        {
            trayIcon.Visible = false;
            msfsVariableRunner.Stop();
            cockpitEventRunner.Stop();
            Dispose();
            Application.Exit();
        }

        /// <summary>
        /// Ouvre les logs de l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Log_OnClick(object? sender, EventArgs e)
        {
            LogFactory.Get().OpenInEditor();
        }

        /// <summary>
        /// S'assure que le cockpit et le simulateur sont bien connectés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerConnexion_Tick(object? sender, EventArgs e)
        {
            connextionUseCase.Exec();
        }
    }
}
