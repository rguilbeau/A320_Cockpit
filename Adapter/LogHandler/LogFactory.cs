
namespace A320_Cockpit.Adapter.LogHandler
{
    /// <summary>
    /// Factory pour la création de l'adapteur du système de log de l'application
    /// </summary>
    public static class LogFactory
    {
        private static ILogHandler? logHandler;

        /// <summary>
        /// Singleton de création du système de log
        /// </summary>
        /// <returns></returns>
        public static ILogHandler Get()
        {
            if(logHandler == null)
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                logHandler = new SirelogAdapter.SirelogAdapter(Path.Combine(appDataPath, AppResources.AppName, "logs.log"));
            }

            return logHandler;
        }


    }
}
