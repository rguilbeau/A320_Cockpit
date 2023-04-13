using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Domain.UseCase.Connexion;
using A320_Cockpit.Infrastructure.View.SystemTray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Presenter.Connexion
{
    /// <summary>
    /// Présenteur dédié à la connexion pour le system tray
    /// </summary>
    public class TrayConnexionPresenter : IConnexionPresenter
    {
        private readonly List<Exception> exceptions;
        private readonly ILogHandler logger;

        /// <summary>
        /// Création du présenteur
        /// </summary>
        /// <param name="logger">Le logger</param>
        public TrayConnexionPresenter(ILogHandler logger)
        {
            exceptions = new List<Exception>();
            this.logger = logger;
        }

        /// <summary>
        /// La liste des erreurs de connexion
        /// </summary>
        public List<Exception> Exceptions
        {
            get { return exceptions; }
        }

        /// <summary>
        /// Ajoute une nouvelle erreur de connexion
        /// </summary>
        /// <param name="exception"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddException(Exception exception)
        {
            exceptions.Add(exception);
        }

        /// <summary>
        /// Présente les élements au system tray
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Present()
        {
            //applicationTray.ChangeStatus(exceptions.Count == 0 ? TrayStatus.SUCCESS : TrayStatus.FAILURE);
            if (exceptions.Count > 0)
            {
                foreach (Exception e in exceptions)
                {
                    logger.Error(e);
                }
            }
        }
    }
}
