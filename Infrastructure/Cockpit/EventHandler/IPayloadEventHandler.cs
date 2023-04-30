using A320_Cockpit.Domain.Enum;

namespace A320_Cockpit.Infrastructure.Cockpit.EventHandler
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
        public void Handle(CockpitEvent cockpitEvent, float value);
    }
}
