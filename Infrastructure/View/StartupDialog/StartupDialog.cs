using A320_Cockpit.Adaptation.Log.Sirelog;
using A320_Cockpit.Infrastructure.Aircraft;
using A320_Cockpit.Infrastructure.View.SystemTray;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A320_Cockpit.Infrastructure.View.StartupDialog
{
    public partial class StartupDialog : Form
    {
        public StartupDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lance l'application si l'utilisateur clique sur "Demarrer"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_start_Click(object sender, EventArgs e)
        {
            FakeA320 aircraft = new(new SirelogAdapter(""), "COM5");
            ApplicationTray applicationTray = new(aircraft);
            applicationTray.ChangeStatus(TrayStatus.STAND_BY);
            Dispose();
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
    }
}
