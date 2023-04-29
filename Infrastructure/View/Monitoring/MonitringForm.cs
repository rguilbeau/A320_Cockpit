using A320_Cockpit.Adaptation.Canbus;
using A320_Cockpit.Domain.Entity.Cockpit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace A320_Cockpit.Infrastructure.View.Monitoring
{
    public partial class MonitringForm : Form
    {
        public MonitringForm()
        {
            InitializeComponent();

            progressBar.Minimum = 0;
            progressBar.Maximum = 5000; //ms

            UpdateProgressTiming(0);
            SetStatusCanBus(false);
            SetStatusSimulator(false);
        }

        /// <summary>
        /// Inscrit une frame dans le rich text box
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="frame"></param>
        public void AddFrame(FrameDirection direction, Frame frame)
        {

            richTextBox_frames.SafeInvoke(() =>
            {
                string iconFrame = "\u2B82"; // <-->

                switch (direction)
                {
                    case FrameDirection.FROM_COCKPIT:
                        richTextBox_frames.SelectionColor = Color.DarkBlue;
                        iconFrame = "\u2190"; // <--
                        break;
                    case FrameDirection.TO_COCKPIT:
                        richTextBox_frames.SelectionColor = Color.DarkGreen;
                        iconFrame = "\u2192"; // -->
                        break;
                }

                richTextBox_frames.AppendText(iconFrame + " " + frame.ToString() + "\n");
                richTextBox_frames.SelectionStart = richTextBox_frames.Text.Length;
                richTextBox_frames.ScrollToCaret();
            });
        }

        /// <summary>
        /// Incrit une erreur dans le rich text box
        /// </summary>
        /// <param name="e"></param>
        public void AddError(Exception e)
        {
            richTextBox_errors.SafeInvoke(() =>
            {
                richTextBox_errors.AppendText(e.ToString() + "\n" + "\n");
                richTextBox_errors.SelectionStart = richTextBox_errors.Text.Length;
                richTextBox_errors.ScrollToCaret();
            });
        }

        /// <summary>
        /// Met à jour le status du CAN Bus
        /// </summary>
        /// <param name="canBus"></param>
        public void SetStatusCanBus(bool canBus)
        {
            label_canbus.SafeInvoke(() =>
            {
                if (canBus)
                {
                    label_canbus.ForeColor = Color.Green;
                    label_canbus.Text = "Connecté";
                }
                else
                {
                    label_canbus.ForeColor = Color.Red;
                    label_canbus.Text = "Déconnecté";
                }
            });
        }

        /// <summary>
        /// Met à jour le status du simulateur
        /// </summary>
        /// <param name="simulator"></param>
        public void SetStatusSimulator(bool simulator)
        {
            label_sim.SafeInvoke(() =>
            {
                if (simulator)
                {
                    label_sim.ForeColor = Color.Green;
                    label_sim.Text = "Connecté";
                }
                else
                {
                    label_sim.ForeColor = Color.Red;
                    label_sim.Text = "Déconnecté";
                }
            });
        }

        /// <summary>
        /// Met à jour la barre de progression du timing
        /// </summary>
        /// <param name="timing"></param>
        public void UpdateProgressTiming(long timing)
        {
            progressBar.SafeInvoke(() =>
            {
                progressBar.Value = (int)timing;
            });


            label_timing.SafeInvoke(() =>
            {
                label_timing.Text = timing.ToString() + "ms";
            });
        }
    }
}
