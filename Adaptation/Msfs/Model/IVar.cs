namespace A320_Cockpit.Adaptation.Msfs.Model
{
    /// <summary>
    /// Représentation globale d'une variable de MSFS
    /// </summary>
    /// <typeparam name="T">La type de la variable</typeparam>
    public interface IVar<T>
    {
        /// <summary>
        /// L'identifiant unique de la variable
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// La valeur de la variable
        /// </summary>
        public T? Value { get; set; }
    }
}
