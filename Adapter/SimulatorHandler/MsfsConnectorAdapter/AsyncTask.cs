
namespace A320_Cockpit.Adapter.SimulatorHandler.MsfsConnectorAdapter
{
    /// <summary>
    /// Class permettant l'attante des tâches asynchrone du connecteur MSFS
    /// </summary>
    /// <typeparam name="T">Le type de retour attendu</typeparam>
    class AsyncTask<T> : TaskCompletionSource<T>
    {
        private readonly Type typeVar;
        private readonly int idRequest;

        /// <summary>
        /// Création d'une nouvelle attente
        /// </summary>
        /// <param name="idRequest">L'ID de la request de MSFS</param>
        /// <param name="typeVar">Le type de la variable</param>
        public AsyncTask(int idRequest, Type typeVar) : base() {
            this.typeVar = typeVar;
            this.idRequest = idRequest;
        }

        /// <summary>
        /// Retourne le type de la variable
        /// </summary>
        public Type TypeVar
        {
            get { return typeVar; }
        }

        /// <summary>
        /// Retourne l'ID Request de MSFS
        /// </summary>
        public int IdRequest
        {
            get { return idRequest; }
        }
    }
}
