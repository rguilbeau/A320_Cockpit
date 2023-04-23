
namespace A320_Cockpit.Adaptation.Msfs.MsfsWasm
{
    /// <summary>
    /// Class permettant l'attante des tâches asynchrone du connecteur MSFS
    /// </summary>
    class AsyncTask
    {
        private readonly Type typeVar;
        private readonly int idRequest;
        private readonly string name;
        private object? value;
        private Exception? exception;

        /// <summary>
        /// Création d'une nouvelle attente
        /// </summary>
        /// <param name="idRequest"></param>
        /// <param name="typeVar"></param>
        /// <param name="name"></param>
        public AsyncTask(int idRequest, Type typeVar, String name) : base() {
            this.typeVar = typeVar;
            this.idRequest = idRequest;
            this.name = name;
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
        /// Retourne le nom de l'attente
        /// </summary>
        public string Name { get { return name; } }

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
