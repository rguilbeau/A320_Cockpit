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
        private readonly ConnextionUseCase connextionUseCase;
        private readonly IAircraft aircraft;

        private MonitringForm ?monitoringForm;

        /// <summary>
        /// Création du système tray
        /// </summary>
        public ApplicationTray(IAircraft aircraft)
        {
            this.aircraft = aircraft;

            TrayConnexionPresenter trayConnexionPresenter = new(aircraft.Logger, this);
            TrayListenEventPresenter trayListenEventPresenter = new(this);
            TraySendPresenter traySendPresenter = new(aircraft.Logger, this);

            connextionUseCase = new ConnextionUseCase(aircraft.SimulatorConnexionRepository, aircraft.CockpitRepository);
            connextionUseCase.AddPresenter(trayConnexionPresenter);
            
            
            msfsVariableRunner = aircraft.CreateRunner(trayConnexionPresenter, trayListenEventPresenter, traySendPresenter);

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
                        new ToolStripMenuItem("Monitoring", null, Monitoring_OnClick),
                        new ToolStripSeparator(),
                        new ToolStripMenuItem(AppResources.Exit, null, Exit_OnClick)
                    }
                },
                Visible = true,
                BalloonTipText = AppResources.OpenMessage
            };

            trayIcon.ShowBalloonTip(3);
            msfsVariableRunner.Start();
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
            trayIcon.Icon = status == TrayStatus.SUCCESS ? AppResources.AppTrayIcon : AppResources.AppTrayIconError;
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
                monitoringForm.Disposed += (sender, e) =>
                {
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
            msfsVariableRunner.Stop();
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
            aircraft.Logger.OpenInEditor();
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
