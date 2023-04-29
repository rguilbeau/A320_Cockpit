using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Adaptation.Log.Sirelog;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Infrastructure.Aircraft;
using A320_Cockpit.Infrastructure.View.SystemTray;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A320_Cockpit.Infrastructure.View.StartupDialog
{
    public partial class StartupDialog : Form
    {
        private readonly ILogHandler logger;
        private readonly string[] aircrafts;

        private readonly System.Windows.Forms.Timer portScannerTimer;

        /// <summary>
        /// Création de la modale d'ouverture
        /// </summary>
        public StartupDialog(ILogHandler logger)
        {
            InitializeComponent();
            this.logger = logger;

            aircrafts = new string[] { A32nx.NAME, FakeA320.NAME };

            combobox_port.DataSource = SerialPort.GetPortNames(); ;
            combobox_aircraft.DataSource = aircrafts;

            portScannerTimer = new() { Interval = 1000 };
            portScannerTimer.Tick += PortScannerTimer_Tick;
            portScannerTimer.Start();
        }

        /// <summary>
        /// Lance l'application si l'utilisateur clique sur "Demarrer"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_start_Click(object sender, EventArgs e)
        {
            string? port = combobox_port.SelectedItem?.ToString();
            string? aircraftName = combobox_aircraft.SelectedItem?.ToString();

            AircraftOptionsChecker optionsChecker = new(port, aircraftName, logger);
            
            if(optionsChecker.Aircraft == null)
            {
                MessageBox.Show(optionsChecker.ErrorMessage, "Attention");
            } else
            {
                ApplicationTray applicationTray = new(optionsChecker.Aircraft);

                if (checkBox_monitoring.Checked)
                {
                    applicationTray.OpenMonitoring();
                }

                portScannerTimer.Dispose();
                Hide(); // Obligé de garder la fenêtre caché, sinon le system tray ne répond plus
            }
        }

        /// <summary>
        /// Quitte l'application si l'utilisateur clique sur "Annnuler"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Scan des COM Ports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void PortScannerTimer_Tick(object? sender, EventArgs e)
        {
            combobox_port.DataSource = SerialPort.GetPortNames();
        }
    }
}
