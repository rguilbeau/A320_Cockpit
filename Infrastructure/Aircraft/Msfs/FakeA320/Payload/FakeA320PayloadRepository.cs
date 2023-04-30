using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;

namespace A320_Cockpit.Infrastructure.Aircraft.Msfs.FakeA320.Payload
{
    /// <summary>
    /// Classe mère des repository pour le debug du cockpit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FakeA320PayloadRepository<T> : IPayloadRepository where T : PayloadEntity
    {
        private bool askRefresh;

        /// <summary>
        /// L'entité
        /// </summary>
        public abstract T Payload { get; }

        /// <summary>
        /// Création du repository
        /// </summary>
        public FakeA320PayloadRepository()
        {
            askRefresh = true;
        }

        /// <summary>
        /// Force ou non la mise à jour de la frame vers le cockpit
        /// </summary>
        public bool AskRefresh { get { return askRefresh; } set { askRefresh = value; } }

        /// <summary>
        /// Construction de l'entité
        /// </summary>
        /// <returns></returns>
        protected abstract T BuildPayload();

        /// <summary>
        /// Mise à jour de l'entité
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected abstract bool Refresh(CockpitEvent e);

        /// <summary>
        /// Met à jour et retourne l'entité
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public PayloadEntity Find(CockpitEvent e)
        {
            if (Refresh(e))
            {
                askRefresh = false;
                return BuildPayload();
            }
            else
            {
                askRefresh = false;
                return Payload;
            }
        }

    }
}
