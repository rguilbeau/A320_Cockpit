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
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Infrastructure.Presenter.ListenEvent;
using A320_Cockpit.Infrastructure.Aircraft;
using A320_Cockpit.Infrastructure.View.Monitoring;
using System.DirectoryServices.ActiveDirectory;
using A320_Cockpit.Infrastructure.Presenter.Monitoring;

namespace A320_Cockpit.Infrastructure.View.SystemTray
{
    /// <summary>
    /// Vue dans le System tray
    /// </summary>
    public class ApplicationTray : ApplicationContext
    {
        private readonly NotifyIcon trayIcon;
        private readonly System.Windows.Forms.Timer timerConnexion;

        private readonly IRunner runner;
        private readonly ConnextionUseCase connextionUseCase;

        private MonitoringFormListenEventPresenter ?monitoringFormListenEventPresenter;
        private MonitotingFormConnexionPresenter ?monitotingFormConnexionPresenter;
        private MonitoringFormSendPresenter ?monitoringFormSendPresenter;
        private MonitoringPresenter? monitoringPresenter;

        private MonitringForm ?monitoringForm;

        /// <summary>
        /// Création du système tray
        /// </summary>
        public ApplicationTray(IAircraft aircraft)
        {
            connextionUseCase = new ConnextionUseCase(aircraft.SimulatorConnexionRepository, aircraft.CockpitRepository);
            connextionUseCase.AddPresenter(new TrayConnexionPresenter(this));

            runner = aircraft.Runner;
            runner.AddListenEventPresenter(new TrayListenEventPresenter(this));
            runner.AddSendPayloadPresenter(new TraySendPresenter(this));

            timerConnexion = new() { Interval = 5000 };
            timerConnexion.Tick += TimerConnexion_Tick;
            timerConnexion.Start();

            trayIcon = new()
            {
                Text = AppResources.AppName,
                Icon = AppResources.AppTrayIconError,
                ContextMenuStrip = new ContextMenuStrip()
                {
                    Items = {
                        new ToolStripMenuItem("Monitoring", null, Monitoring_OnClick),
                        new ToolStripSeparator(),
                        new ToolStripMenuItem(AppResources.Exit, null, Exit_OnClick)
                    }
                },
                Visible = true,
                BalloonTipText = AppResources.OpenMessage
            };

            trayIcon.ShowBalloonTip(3);
            runner.Start();
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
        public void BlinkIcon(TrayStatus status)
        {
            /*trayIcon.Icon = status == TrayStatus.SUCCESS ? AppResources.AppTrayIcon : AppResources.AppTrayIconError;
            
            System.Timers.Timer blinkTimer = new() { Interval = 20 };
            blinkTimer.Elapsed += (sender, args) =>
            {
                trayIcon.Icon = AppResources.AppTrayIconSuccess;
                blinkTimer.Stop();
                blinkTimer.Dispose();
            };
            blinkTimer.Start();*/
        }

        /// <summary>
        /// Ouvre la fenêtre de monitoring
        /// </summary>
        public void OpenMonitoring()
        {
            if (monitoringForm != null)
            {
                monitoringForm.Focus();
            } else
            {
                monitoringForm = new();

                monitoringFormListenEventPresenter = new(monitoringForm);
                monitoringFormSendPresenter = new(monitoringForm);
                monitotingFormConnexionPresenter = new(monitoringForm);
                monitoringPresenter = new(monitoringForm);

                runner.AddListenEventPresenter(monitoringFormListenEventPresenter);
                runner.AddSendPayloadPresenter(monitoringFormSendPresenter);
                runner.AddMonitoringPresenter(monitoringPresenter);
                connextionUseCase.AddPresenter(monitotingFormConnexionPresenter);

                monitoringForm.Disposed += (sender, e) =>
                {
                    runner.RemoveListenEventPresenter(monitoringFormListenEventPresenter);
                    runner.RemoveSendPayloadPresenter(monitoringFormSendPresenter);
                    runner.RemoveMonitoringPresenter(monitoringPresenter);
                    connextionUseCase.RemovePresenter(monitotingFormConnexionPresenter);
                    monitoringForm = null;
                };
                monitoringForm.Show();
            }
        }

        /// <summary>
        /// Quitte l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Exit_OnClick(object? sender, EventArgs e)
        {
            trayIcon.Visible = false;
            runner.Stop();
            Dispose();
            Application.Exit();
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

        /// <summary>
        /// Ouvre la fenêtre de monitoring des messages et des erreurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Monitoring_OnClick(object? sender, EventArgs e)
        {
            OpenMonitoring();
        }
    }
}
