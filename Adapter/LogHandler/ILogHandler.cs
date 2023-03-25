namespace A320_Cockpit.Adapter.LogHandler
{
    /// <summary>
    /// Interface des adapteurs du système de log de l'application
    /// </summary>
    public interface ILogHandler
    {
        /// <summary>
        /// Le chemin d'accès du log
        /// </summary>
        public string LogPath { get; }

        /// <summary>
        /// Inscrit une information dans le log
        /// </summary>
        /// <param name="message">Le message à logger</param>
        public void Info(string message);

        /// <summary>
        /// Inscrit une exception en tant qu'information dans le log
        /// </summary>
        /// <param name="e">L'exception à logger</param>
        public void Info(Exception e);

        /// <summary>
        /// Inscrit un warning dans le log
        /// </summary>
        /// <param name="message">Le message à logger</param>
        public void Warning(string message);

        /// <summary>
        /// Inscrit une exception en tant que warning dans le log
        /// </summary>
        /// <param name="e">L'exception à logger</param>
        public void Warning(Exception e);

        /// <summary>
        /// Inscrit une erreur dans le log
        /// </summary>
        /// <param name="message">Le message à logger</param>
        public void Error(string message);

        /// <summary>
        /// Inscrit une exception en tant qu'erreur dans le log
        /// </summary>
        /// <param name="e">L'exception à logger</param>
        public void Error(Exception e);

    }
}
