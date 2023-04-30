
namespace A320_Cockpit.Adaptation.Msfs.Model.Variable
{
    /// <summary>
    /// Définition d'une SimVar
    /// </summary>
    /// <typeparam name="T">Le type de valeur de la variable</typeparam>
    public class SimVar<T> : IVar<T>
    {
        private readonly string identifier;
        private readonly string name;
        private readonly string unit;
        private T? value;

        /// <summary>
        /// Création d'une nouvelle SimVar
        /// </summary>
        /// <param name="name">Le nom de la variable</param>
        /// <param name="unit">Les untités de la variable (feet, knots, degres...)</param>
        public SimVar(string name, string unit)
        {
            this.name = name;
            this.unit = unit;
            value = default;
            identifier = "simvar" + typeof(T) + name;
        }

        /// <summary>
        /// L'identifiant unique de la variable
        /// </summary>
        public string Identifier
        {
            get { return identifier; }
        }

        /// <summary>
        /// Le nom de la variable
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Les unitées de la variable
        /// </summary>
        public string Unit
        {
            get { return unit; }
        }

        /// <summary>
        /// La valeur de la variable
        /// </summary>
        public T? Value
        {
            get { return value; }
            set { this.value = value; }
        }

    }
}
