using A320_Cockpit.Adapter.LogHandler;
using A320_Cockpit.Adapter.SimulatorHandler;
using A320_Cockpit.Domain.BusSend.UseCase;
using A320_Cockpit.Domain.CanBus;
using A320_Cockpit.Domain.Connexion.SimConnector;

namespace A320_Cockpit.Infrastructure.MsfsVariableUpdater
{
    /// <summary>
    /// Classe mère du système de mise à jours des variables d'un avion de MSFS
    /// </summary>
    /// <typeparam name="T">Le type de mise à jour</typeparam>
    /// <typeparam name="P">Le type du payload</typeparam>
    public abstract class MsfsUpdater<T, P> where T : Enum
    {
        protected readonly ISimulatorHandler simulatorHandler;
        protected readonly ICanBus canBus;
        protected ISendFramePresenter presenter;
        protected readonly ILogHandler logger;

        /// <summary>
        /// Création du système de mise à jours des variables d'un avion de MSFS
        /// </summary>
        /// <param name="simulatorHandler">Le connecteur MSFS</param>
        /// <param name="canBus">Le CAN Bus</param>
        /// <param name="presenter">Le présenteur de sortie</param>
        /// <param name="logger">Le logger de l'application</param>
        public MsfsUpdater(ISimulatorHandler simulatorHandler, ICanBus canBus, ISendFramePresenter presenter, ILogHandler logger)
        {
            this.simulatorHandler = simulatorHandler;
            this.canBus = canBus;
            this.presenter = presenter;
            this.logger = logger;
        }

        /// <summary>
        /// Le payload des variables
        /// </summary>
        public abstract P Payload { get; set; }

        /// <summary>
        /// Mets à jour toutes les variables depuis MSFS
        /// </summary>
        public void Update()
        {
            foreach (T update in (T[])Enum.GetValues(typeof(T)))
            {
                UpdateVariables(update);
            }
            VariablesUpdated();
            SendPayload();
        }

        /// <summary>
        /// Mets à jour les variables depuis MSFS. 
        /// Le type de mise à jours permet de cibler précisement quelles variables mettre à jour
        /// </summary>
        /// <param name="update">Le type de mise à jour</param>
        public void Update(T update)
        {
            UpdateVariables(update);
            VariablesUpdated();
            SendPayload();
        }

        /// <summary>
        /// Mets à jour les variables depuis MSFS. 
        /// Le type de mise à jours permet de cibler précisement quelles variables mettre à jour
        /// </summary>
        /// <param name="updates">Le type de mise à jour</param>
        public void Update(params T[] updates)
        {
            foreach (T update in updates)
            {
                UpdateVariables(update);
            }

            VariablesUpdated();
            SendPayload();
        }

        /// <summary>
        /// Mets à jour les variables depuis MSFS. 
        /// Le type de mise à jours permet de cibler précisement quelles variables mettre à jour
        /// </summary>
        /// <param name="update">Le type de mise à jour</param>
        protected abstract void UpdateVariables(T update);

        /// <summary>
        /// Methode appelé après le mise à jour des variables, enrechi le payload avec les valeurs des variables MSFS
        /// </summary>
        protected abstract void VariablesUpdated();

        /// <summary>
        /// Envoi le payload
        /// </summary>
        public abstract void SendPayload();
    }
}
