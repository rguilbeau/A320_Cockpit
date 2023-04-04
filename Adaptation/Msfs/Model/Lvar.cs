namespace A320_Cockpit.Adaptation.Msfs.Model
{
    /// <summary>
    /// Définition d'une LVAR
    /// </summary>
    /// <typeparam name="T">Le type de la variable à lire</typeparam>
    public class Lvar<T> : IVar<T>
    {
        private readonly string identifier;
        private readonly string name;
        private T? value;

        /// <summary>
        /// Création d'une nouvelle variable LVAR
        /// </summary>
        /// <param name="name"></param>
        public Lvar(string name)
        {
            this.name = name;
            value = default;
            identifier = "lvar" + typeof(T) + Name;
        }

        /// <summary>
        /// Identifiant unique de la LVAR
        /// </summary>
        public string Identifier
        {
            get { return identifier; }
        }

        /// <summary>
        /// Nom de la LVAR
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// La valeur de la LVAR
        /// </summary>
        public T? Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
