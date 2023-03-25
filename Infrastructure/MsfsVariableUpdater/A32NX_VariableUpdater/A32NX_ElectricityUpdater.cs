using A320_Cockpit.Adapter.LogHandler;
using A320_Cockpit.Adapter.SimulatorHandler;
using A320_Cockpit.Domain.BusSend.Payload;
using A320_Cockpit.Domain.BusSend.UseCase;
using A320_Cockpit.Domain.CanBus;
using A320_Cockpit.Domain.Connexion.SimConnector;

namespace A320_Cockpit.Infrastructure.MsfsVariableUpdater.A32NX_VariableUpdater
{
    /// <summary>
    /// Système de mise à jours des variables de l'électricité depuis l'A32NX
    /// </summary>
    public class A32NX_ElectricityUpdater : MsfsUpdater<A32NX_ElectricityUpdater.Updates, ElectricityPayload>
    {
        /// <summary>
        /// Mise à jours disponibles
        /// </summary>
        public enum Updates
        {
            ALL
        };

        private ElectricityPayload electricityPayload;

        /// <summary>
        /// Création du système de mise à jours des variables de l'électricité depuis l'A32NX
        /// </summary>
        /// <param name="simulatorHandler">Le connecteur MSFS</param>
        /// <param name="canBus">Le CAN Bus</param>
        /// <param name="presenter">Le présenteur de sortie</param>
        public A32NX_ElectricityUpdater(ISimulatorHandler simulatorHandler, ICanBus can, ISendFramePresenter presenter, ILogHandler logger) : base(simulatorHandler, can, presenter, logger)
        {
            electricityPayload = new();
        }

        /// <summary>
        /// Le payload des variables
        /// </summary>
        public override ElectricityPayload Payload
        {
            get { return electricityPayload; }
            set { electricityPayload = value; }
        }

        /// <summary>
        /// Mets à jour les variables depuis MSFS
        /// Le type de mise à jours permet de cibler précisement quelles variables mettre à jour
        /// </summary>
        /// <param name="update">Le type de mise à jour</param>
        protected override void UpdateVariables(Updates update)
        {
            switch (update)
            {
                case Updates.ALL:
                    simulatorHandler.Read(A32NX_Variables.Eletricity.IsElectricityAc1BusPowered);
                    break;
            }
        }

        /// <summary>
        /// Methode appelé après le mise à jour des variables, enrechi le payload avec les valeurs des variables MSFS
        /// </summary>
        protected override void VariablesUpdated()
        {
            electricityPayload.IsElectricityAc1BusPowered = A32NX_Variables.Eletricity.IsElectricityAc1BusPowered.Value;
        }

        /// <summary>
        /// Envoi le payload
        /// </summary>
        public override void SendPayload()
        {
            new SendElectricity(canBus, presenter).Send(electricityPayload);
        }
    }
}
