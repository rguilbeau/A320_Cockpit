using A320_Cockpit.Adaptation.Canbus;
using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Repository.Cockpit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Cockpit
{
    /// <summary>
    /// Le repository pour la connexion au cockpit via le CAN Bus par USB
    /// </summary>
    public class SerialBusCockpitRepository : ICockpitRepository
    {
        private readonly ICanbus canbus;

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="canbus"></param>
        public SerialBusCockpitRepository(ICanbus canbus)
        {
            this.canbus = canbus;
        }

        /// <summary>
        /// Retourne si la connexion est ouverte
        /// </summary>
        public bool IsOpen => canbus.IsOpen;

        /// <summary>
        /// Ferme la connexion
        /// </summary>
        public void Close()
        {
            canbus.Close();
        }

        /// <summary>
        /// Ouvre la connexion
        /// </summary>
        public void Open()
        {
            canbus.Open();
        }

        /// <summary>
        /// Envoi une frame
        /// </summary>
        /// <param name="frame"></param>
        public void Send(Frame frame)
        {
            canbus.Send(frame);
        }
    }
}
