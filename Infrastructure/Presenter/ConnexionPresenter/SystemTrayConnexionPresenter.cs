using A320_Cockpit.Adapter.LogHandler;
using A320_Cockpit.Adapter.LogHandler.SirelogAdapter;
using A320_Cockpit.Domain.Connexion.UseCase;
using A320_Cockpit.Infrastructure.View.ApplicationTray;
using System.Text;

namespace A320_Cockpit.Infrastructure.Presenter.ConnexionPresenter
{
    /// <summary>
    /// Présenteur dédié à la connexion pour le system tray
    /// </summary>
    public class SystemTrayConnexionPresenter : IConnexionPresenter
    {
        private ApplicationTray applicationTray;
        private List<Exception> errors;
        private ILogHandlerAdapter logger;

        /// <summary>
        /// Création du présenteur dédié à la connexion pour le system tray
        /// </summary>
        /// <param name="applicationTray">L'application system tray</param>
        /// <param name="logger">Le logger</param>
        public SystemTrayConnexionPresenter(ApplicationTray applicationTray, ILogHandlerAdapter logger)
        {
            errors = new List<Exception>();
            this.applicationTray= applicationTray;
            this.logger = logger;
        }

        /// <summary>
        /// La liste des erreurs de connexion
        /// </summary>
        public List<Exception> Errors
        {
            get { return errors; }
        }

        /// <summary>
        /// Ajoute une nouvelle erreur de connexion
        /// </summary>
        /// <param name="exception"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddError(Exception exception)
        {
            errors.Add(exception);
        }

        /// <summary>
        /// Présente les élements au system tray
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Present()
        {
            applicationTray.ChangeStatus(errors.Count == 0 ? TrayStatus.SUCCESS : TrayStatus.FAILURE);
            logger.Info("coucou!!!!!!");
            if(errors.Count > 0) 
            {
                foreach(Exception e in errors)
                {
                    logger.Error(e);
                }
            }
        }
    }
}
