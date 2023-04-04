using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;

namespace A320_Cockpit.Adaptation.Log.Sirelog
{
    /// <summary>
    /// Système de log de l'application
    /// </summary>
    public class SirelogAdapter : ILogHandler
    {
        private readonly string logPath;
        private readonly Logger logger;

        /// <summary>
        /// Création du logger de l'application
        /// </summary>
        /// <param name="logPath"></param>
        public SirelogAdapter(string logPath)
        {
            this.logPath = logPath;
            logger = new LoggerConfiguration()
                .WriteTo.File(
                    new Serilog.Formatting.Display.MessageTemplateTextFormatter(
                        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{Exception}{NewLine}"),
                        logPath
                    )
                .CreateLogger();
        }

        /// <summary>
        /// Le chemin d'accès au log-
        /// </summary>
        public string LogPath
        {
            get { return logPath; }
        }

        /// <summary>
        /// Log un message avec le tag "ERROR"
        /// </summary>
        /// <param name="message">Le message à inscrire dans les logs</param>
        public void Error(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Log une exception avec le tag "ERROR"
        /// </summary>
        /// <param name="e">L'exception à inscrire dans les logs</param>
        public void Error(Exception e)
        {
            logger.Error(e, string.Empty);
        }

        /// <summary>
        /// Log un message avec le tag "INFO"
        /// </summary>
        /// <param name="message">Le message à inscrire dans les logs</param>
        public void Info(string message)
        {
            logger.Information(message);
        }

        /// <summary>
        /// Log une exception avec le tag "INFO"
        /// </summary>
        /// <param name="e">L'exception à inscrire dans les logs</param>
        public void Info(Exception e)
        {
            logger.Information(e, string.Empty);
        }

        /// <summary>
        /// Log un message avec le tag "WARNING"
        /// </summary>
        /// <param name="message">Le message à inscrire dans les logs</param>
        public void Warning(string message)
        {
            logger.Warning(message);
        }

        /// <summary>
        /// Log une exception avec le tag "WARNING"
        /// </summary>
        /// <param name="e"></param>
        public void Warning(Exception e)
        {
            logger.Warning(e, string.Empty);
        }
    }
}
