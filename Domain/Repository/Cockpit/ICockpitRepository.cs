using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A320_Cockpit.Domain.Entity.Cockpit;

namespace A320_Cockpit.Domain.Repository.Cockpit
{
    /// <summary>
    /// Repository de la connexion au cockpit du cockpit
    /// </summary>
    public interface ICockpitRepository
    {
        /// <summary>
        /// Ouvre la connexion au cockpit
        /// </summary>
        public void Open();

        /// <summary>
        /// Ferme la connexion au cockpit
        /// </summary>
        public void Close();

        /// <summary>
        /// Envoi une frame au cockpit
        /// </summary>
        /// <param name="frame">La frame à envoyer</param>
        public void Send(Frame frame);

        /// <summary>
        /// Etat de la connexion au cockpit
        /// </summary>
        public bool IsOpen { get; }
    }
}
