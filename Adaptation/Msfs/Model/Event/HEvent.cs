
namespace A320_Cockpit.Adaptation.Msfs.Model.Event
{
    /// <summary>
    /// Definition d'un event MSFS HTML
    /// </summary>
    public class HEvent
    {
        private readonly string name;
        
        /// <summary>
        /// Création de l'evenement
        /// </summary>
        /// <param name="name"></param>
        public HEvent(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Le nom de l'evenement
        /// </summary>
        public string Name { get { return name; } }
    }
}
