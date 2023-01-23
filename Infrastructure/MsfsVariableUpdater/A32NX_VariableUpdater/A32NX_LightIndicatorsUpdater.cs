using A320_Cockpit.Adapter.LogHandler;
using A320_Cockpit.Adapter.SimulatorHandler;
using A320_Cockpit.Domain.BusSend.Payload;
using A320_Cockpit.Domain.BusSend.UseCase;
using A320_Cockpit.Domain.CanBus;
using A320_Cockpit.Domain.Connexion.SimConnector;

namespace A320_Cockpit.Infrastructure.MsfsVariableUpdater.A32NX_VariableUpdater
{
    /// <summary>
    /// Système de mise à jours des variables des indicateurs de lumières depuis l'A32NX
    /// </summary>
    public class A32NX_LightIndicatorsUpdater : MsfsUpdater<A32NX_LightIndicatorsUpdater.Updates, LightIndicatorsPayload>
    {
        /// <summary>
        /// Mise à jour disponibles
        /// </summary>
        public enum Updates
        {
            TEST_INDICATORS_LIGHT,
            FCU_DISPLAY_BRIGHTNESS
        };

        private LightIndicatorsPayload lightIndicatorsPayload;
        private readonly SendLightIndicators sender;

        /// <summary>
        /// Création du système de mise à jours des variables des indicateurs de lumières depuis l'A32NX
        /// </summary>
        /// <param name="simulatorHandler">Le connecteur MSFS</param>
        /// <param name="canBus">Le CAN Bus</param>
        /// <param name="presenter">Le présenteur de sortie</param>
        public A32NX_LightIndicatorsUpdater(ISimulatorHandler simulatorHandler, ICanBus can, ISendFramePresenter presenter, ILogHandler logger) : base(simulatorHandler, can, presenter, logger)
        {
            sender = new SendLightIndicators(canBus, presenter);

            lightIndicatorsPayload = new();
        }

        /// <summary>
        /// Le payload des variables
        /// </summary>
        public override LightIndicatorsPayload Payload
        {
            get { return lightIndicatorsPayload; }
            set { lightIndicatorsPayload = value; }
        }

        /// <summary>
        /// Mets à jour les variables depuis MSFS. 
        /// Le type de mise à jours permet de cibler précisement quelles variables mettre à jour
        /// </summary>
        /// <param name="update">Le type de mise à jour</param>
        protected override void UpdateVariables(Updates update)
        {
            switch (update)
            {
                case Updates.TEST_INDICATORS_LIGHT:
                    simulatorHandler.Read(A32NX_Variables.LightIndicators.LightIndicatorStatus);
                    break;
                case Updates.FCU_DISPLAY_BRIGHTNESS:
                    break;
            }
        }

        /// <summary>
        /// Methode appelé après le mise à jour des variables, enrechi le payload avec les valeurs des variables MSFS
        /// </summary>
        protected override void VariablesUpdated()
        {
            lightIndicatorsPayload.TestIndicatorsLight = A32NX_Variables.LightIndicators.LightIndicatorStatus.Value == 0;
            lightIndicatorsPayload.FcuDisplayBrightness = 100;
        }

        /// <summary>
        /// Envoi le payload
        /// </summary>
        public override void SendPayload()
        {
            sender.Send(lightIndicatorsPayload);
        }
    }
}
