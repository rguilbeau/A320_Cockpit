using A320_Cockpit.Domain.CanBus;

namespace A320_Cockpit.Domain.BusSend.UseCase
{
    /// <summary>
    /// Présenteur des frames envoyées
    /// </summary>
    public interface ISendFramePresenter
    {
        /// <summary>
        /// La frame envoyée
        /// </summary>
        public Frame? Frame { get; set; }

        /// <summary>
        /// L'erreur potentiel
        /// </summary>
        public Exception? Error { get; set; }

        /// <summary>
        /// Présente les valeurs
        /// </summary>
        /// <param name="isSent"></param>
        public void Present(bool isSent);
    }
}
