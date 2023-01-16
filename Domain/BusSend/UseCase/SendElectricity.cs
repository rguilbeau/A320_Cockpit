using A320_Cockpit.Domain.BusSend.Payload;
using A320_Cockpit.Domain.CanBus;
using System.Drawing;

namespace A320_Cockpit.Domain.BusSend.UseCase
{
    /// <summary>
    /// Envoi la frame de l'électricité
    /// </summary>
    public class SendElectricity : BaseSender<ElectricityPayload>
    {
        private const int ID = 0x12C;
        private const int SIZE = 1;

        /// <summary>
        /// Construction du UseCase pour l'envoi de la frame de l'électricité
        /// </summary>
        /// <param name="canBus">Le CAN Bus</param>
        /// <param name="presenter">Le présenteur de sortie</param>
        public SendElectricity(ICanBus canBus, ISendFramePresenter presenter) : base(canBus, presenter)
        {
        }

        /// <summary>
        /// Construction de la frame à partir du payload
        /// </summary>
        /// <param name="payload">Le payload à envoyer</param>
        /// <returns></returns>
        protected override Frame BuildFrame(ElectricityPayload payload)
        {
            Frame frame = new(ID, SIZE);

            bool[] elecBus = new bool[8];
            elecBus[0] = payload.IsElectricityAc1BusPowered;
            elecBus[1] = false;
            elecBus[2] = false;
            elecBus[3] = false;
            elecBus[4] = false;
            elecBus[5] = false;
            elecBus[6] = false;
            elecBus[7] = false;

            frame.Data[0] = Frame.BitArrayToByte(elecBus);
            return frame;
        }
    }
}
