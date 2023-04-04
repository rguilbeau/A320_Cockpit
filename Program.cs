namespace A320_Cockpit
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            //ICockpitRepository cockpitRepository = new SerialBusCockpitRepository();
           // IReadingPresenter presenter = new TraySendMessagePresenter();

            //List<AReadingUseCase> useCaseReaders = new()
            //{
            //    new ReadingFcuDisplay(cockpitRepository, presenter, null)
            //};

            //while (true)
            //{
             //   foreach(AReadingUseCase useCaseReader in useCaseReaders)
              //  {
              //      useCaseReader.Exec();
               // }
           // }

        }
    }
}