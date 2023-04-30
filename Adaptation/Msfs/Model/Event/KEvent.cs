
namespace A320_Cockpit.Adaptation.Msfs.Model.Event
{
    /// <summary>
    /// Definition d'un custom event.
    /// Les KEvent peuvent prendre un paramètre, dans ce cas indiquer le type du paramètre dans le type générique
    /// sinon, passer KEventEmpty si l'evenement ne prend de paramètre
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KEvent<T>
    {
        private readonly string name;
        private T? value;

        /// <summary>
        /// Création de l'event
        /// </summary>
        /// <param name="name"></param>
        public KEvent(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Le nom de l'evenement
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// La valeur du paramètre de l'evenement
        /// </summary>
        public T? Value { get { return value; } set { this.value = value; } }
    }

    /// <summary>
    /// Type à indiquer pour un KEvent sans paramètre
    /// </summary>
    public class KEventEmpty { }
}
