using A320_Cockpit.Domain.BusSend.Payload;
using A320_Cockpit.Domain.CanBus;

namespace A320_Cockpit.Domain.BusSend.UseCase
{
    /// <summary>
    /// Envoi la frame des indicateurs de lumières
    /// </summary>
    public class SendLightIndicators : BaseSender<LightIndicatorsPayload>
    {
        private const int ID = 0x0C8;
        private const int SIZE = 2;

        /// <summary>
        /// Création du UseCase pour l'envoi de la frame des indicateurs de lumières
        /// </summary>
        /// <param name="canBus">Le CAN Bus</param>
        /// <param name="presenter">Le présenteur de sortie</param>
        public SendLightIndicators(ICanBus canBus, ISendFramePresenter presenter) : base(canBus, presenter)
        {
        }

        /// <summary>
        /// Construction de la frame à partir du payload
        /// </summary>
        /// <param name="payload">Le payload à envoyer</param>
        /// <returns></returns>
        protected override Frame BuildFrame(LightIndicatorsPayload payload)
        {
            Frame frame = new(ID, SIZE);

            bool[] indicatorLight = new bool[8];
            indicatorLight[0] = payload.TestIndicatorsLight;
            indicatorLight[1] = false;
            indicatorLight[2] = false;
            indicatorLight[3] = false;
            indicatorLight[4] = false;
            indicatorLight[5] = false;
            indicatorLight[6] = false;
            indicatorLight[7] = false;

            frame.Data[0] = Frame.BitArrayToByte(indicatorLight);

            frame.Data[1] = payload.FcuDisplayBrightness;

            return frame;
        }
    }
}
