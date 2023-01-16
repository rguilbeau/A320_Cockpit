using A320_Cockpit.Adapter.CanBusAdapter;
using A320_Cockpit.Adapter.LogHandler;
using A320_Cockpit.Adapter.MsfsConnectorAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter.FcuipcAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter;
using A320_Cockpit.Domain.Connexion.UseCase;
using A320_Cockpit.Infrastructure.Presenter.ConnexionPresenter;
using System.Diagnostics;
using System.IO;

namespace A320_Cockpit.Infrastructure.View.ApplicationTray
{
    /// <summary>
    /// Vue dans le System tray
    /// </summary>
    public class ApplicationTray : ApplicationContext
    {
        private readonly ICanBusAdapter canBus;
        private readonly MsfsConnector simConnector;
        private readonly NotifyIcon trayIcon;
        private readonly System.Windows.Forms.Timer timerConnexion;

        /// <summary>
        /// Création du système tray
        /// </summary>
        public ApplicationTray()
        {
            canBus = CanBusFactory.Get();
            simConnector = new MsfsConnector(FsuipcConnector.Get(), SimConnectConnector.Get());
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

            new MainLoop(this, canBus, simConnector, LogHandlerFactory.Get()).Start();
        }

        /// <summary>
        /// Modifie le l'icon du System tray avec la pastille rouge ou verte
        /// </summary>
        /// <param name="success"></param>
        public void ChangeStatus(TrayStatus status)
        {
            switch(status)
            {
                case TrayStatus.SUCCESS: trayIcon.Icon = AppResources.AppTrayIconSuccess;break;
                case TrayStatus.FAILURE: trayIcon.Icon = AppResources.AppTrayIconError;break;
                default: trayIcon.Icon = AppResources.AppTrayIcon;break;
            }
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
            string path = LogHandlerFactory.Get().LogPath;
            var pi = new ProcessStartInfo(path)
            {
                Arguments = Path.GetFileName(path),
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(path),
                FileName = path,
                Verb = "OPEN"
            };
            Process.Start(pi);
        }

        /// <summary>
        /// S'assure que FSUIPC, SimConnect et le CAN Bus sont bien connectés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerConnexion_Tick(object? sender, EventArgs e)
        {
            new EnsureConnexion(simConnector, canBus).Connect(new SystemTrayConnexionPresenter(this, LogHandlerFactory.Get()));
        }
    }
}
