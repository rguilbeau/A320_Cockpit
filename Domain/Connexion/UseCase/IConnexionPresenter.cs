namespace A320_Cockpit.Domain.Connexion.UseCase
{
    /// <summary>
    /// Présenteur dédié à la connexion au simulateur et au cockpit
    /// </summary>
    public interface IConnexionPresenter
    {
        /// <summary>
        /// Les erreurs de connexion
        /// </summary>
        public List<Exception> Errors { get; }

        /// <summary>
        /// Ajoute une erreur de connexion
        /// </summary>
        /// <param name="exception"></param>
        public void AddError(Exception exception);

        /// <summary>
        /// Présente les résultats
        /// </summary>
        public void Present();
    }
}
