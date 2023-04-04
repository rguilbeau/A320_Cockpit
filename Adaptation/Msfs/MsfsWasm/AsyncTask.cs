
namespace A320_Cockpit.Adaptation.Msfs.MsfsWasm
{
    /// <summary>
    /// Class permettant l'attante des tâches asynchrone du connecteur MSFS
    /// </summary>
    class AsyncTask
    {
        private readonly Type typeVar;
        private readonly int idRequest;
        private object? value;
        private Exception? exception;

        /// <summary>
        /// Création d'une nouvelle attente
        /// </summary>
        /// <param name="idRequest">L'ID de la request de MSFS</param>
        /// <param name="typeVar">Le type de la variable</param>
        public AsyncTask(int idRequest, Type typeVar) : base() {
            this.typeVar = typeVar;
            this.idRequest = idRequest;
            value = null;
            exception = null;
        }

        /// <summary>
        /// La tâche est terminé
        /// </summary>
        public bool IsCompleted 
        {
            get { return value != null || exception != null; }
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

        /// <summary>
        /// La valeur lu
        /// </summary>
        public object? Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// L'Exception en cas d'erreur de lecture
        /// </summary>
        public Exception? Exception
        {
            get { return exception; }
            set { exception = value; }
        }
    }
}
