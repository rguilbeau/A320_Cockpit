using FSUIPC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A320_Cockpit.View
{
    public partial class MainForm : Form
    {
        private Offset<uint> heading;

        private Offset<uint> altitude;

        private Offset speed;

        private System.Windows.Forms.Timer timerMain;

        public MainForm()
        {
            InitializeComponent();

        }

        private void TimerMain_Tick(object? sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
