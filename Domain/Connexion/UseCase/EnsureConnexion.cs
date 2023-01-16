using A320_Cockpit.Domain.CanBus;
using A320_Cockpit.Domain.Connexion;
using A320_Cockpit.Domain.Connexion.SimConnector;

namespace A320_Cockpit.Domain.Connexion.UseCase
{
    /// <summary>
    /// Connexion au simulateur et au cockpit
    /// </summary>
    public class EnsureConnexion
    {
        private ISimConnector simConnexion;
        public ICanBus canBus;

        /// <summary>
        /// Création du UseCase pour la connexion au simulateur et au cockpit
        /// </summary>
        /// <param name="simConnexion"></param>
        /// <param name="canBus"></param>
        public EnsureConnexion(ISimConnector simConnexion, ICanBus canBus)
        {
            this.simConnexion = simConnexion;
            this.canBus = canBus;
        }

        /// <summary>
        /// Lance la connexion
        /// </summary>
        /// <param name="presenter"></param>
        public void Connect(IConnexionPresenter presenter)
        {
            if (!simConnexion.IsOpen)
            {
                try
                {
                    simConnexion.Open();
                }
                catch (Exception ex)
                {
                    presenter.AddError(ex);
                }
            }

            if (!canBus.IsOpen)
            {
                try
                {
                    canBus.Open();
                }
                catch (Exception ex)
                {
                    presenter.AddError(ex);
                }
            }


            presenter.Present();
        }
    }
}
