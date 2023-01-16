using A320_Cockpit.Infrastructure.View.ApplicationTray;
using A320_Cockpit.Adapter.LogHandler;

namespace A320_Cockpit
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ApplicationConfiguration.Initialize();
            Application.Run(new ApplicationTray());
        }

        /// <summary>
        /// Log les exception non gérées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogHandlerFactory.Get().Error(new Exception("Unhandled exception", (Exception)e.ExceptionObject));
            {
                Application.Exit();
            }
        }
    }
}

//public System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
//public System.Windows.Forms.Timer timerSend = new System.Windows.Forms.Timer();
//public System.Windows.Forms.Timer timerStopPriority = new System.Windows.Forms.Timer();


//MsfsConnector simConnector;
//SerialCan canBus;

//A32NxFcuDisplayUpdater msfsFcuDisplayUpdater;
//A32NxLightIndicatorsUpdater msfsLightIndicatorsupdater;
//A32NxElectricityUpdater msfsElectricityUpdater;

//public TestForm()
//{
//    InitializeComponent();

//    simConnector = MsfsConnector.OpenConnexions();
//    canBus = new SerialCan(new SerialPort(), "COM5", 9600, "125Kbit");
//    canBus.Open();

//    msfsFcuDisplayUpdater = new(simConnector, canBus);
//    msfsLightIndicatorsupdater = new(simConnector, canBus);
//    msfsElectricityUpdater = new(simConnector, canBus);

//    MouseWheel += TestForm_MouseWheel;
//    timer.Tick += Timer_Tick;
//    timer.Interval = 1;
//    timer.Start();

//    timerSend.Tick += TestForm_TimerSendTick;
//    timerSend.Interval = 1;

//    timerStopPriority.Tick += TimerStopPriority_Tick;
//    timerStopPriority.Interval = 1000;
//}

//private void TimerStopPriority_Tick(object? sender, EventArgs e)
//{
//    timerSend.Stop();
//    timerStopPriority.Stop();
//    timer.Start();
//}

//private void TestForm_TimerSendTick(object? sender, EventArgs e)
//{
//    msfsFcuDisplayUpdater.Update(Domain.VariableUpdater.IFcuDisplayVariableUpdater.Updates.HEADING);

//    /*if(toSend.Count > 0)
//    {
//        FSUIPCConnection.SendControlToFS(toSend[0], 1);
//        toSend.RemoveAt(0);
//    }*/
//}

//private void Timer_Tick(object? sender, EventArgs e)
//{
//    simConnector.StartTransaction();
//    msfsFcuDisplayUpdater.Update();
//    msfsLightIndicatorsupdater.Update();
//    msfsElectricityUpdater.Update();
//    simConnector.StopTransaction();
//}

//private List<FsControl> toSend = new List<FsControl>();

//private void TestForm_MouseWheel(object? sender, MouseEventArgs e)
//{
//    timer.Stop();
//    timerStopPriority.Stop();
//    if (e.Delta > 0)
//    {
//        FSUIPCConnection.SendControlToFS(FsControl.HEADING_BUG_INC, 1);
//        //msfsFcuDisplayUpdater.Update(Domain.VariableUpdater.IFcuDisplayVariableUpdater.Updates.SPEED);

//    }
//    else
//    {
//        FSUIPCConnection.SendControlToFS(FsControl.HEADING_BUG_DEC, 1);
//        //toSend.Add(FsControl.HEADING_BUG_DEC);
//        //msfsFcuDisplayUpdater.Update(Domain.VariableUpdater.IFcuDisplayVariableUpdater.Updates.SPEED);
//    }
//    timerSend.Start();
//    timerStopPriority.Start();
//}