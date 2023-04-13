using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Simulator;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.EventHandler
{
    /// <summary>
    /// Base d'un event dispatch
    /// </summary>
    public interface IPayloadEventHandler
    {
        /// <summary>
        /// Les évenements qui en écoute
        /// </summary>
        public List<CockpitEvent> EventSubscriber { get; }

        /// <summary>
        /// Gestion de l'évenement
        /// </summary>
        /// <param name="frame"></param>
        public void Handle(CockpitEvent cockpitEvent, double value);
    }
}
