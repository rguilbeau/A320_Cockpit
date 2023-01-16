using A320_Cockpit.Domain.BusSend.Payload;
using A320_Cockpit.Domain.CanBus;

namespace A320_Cockpit.Domain.BusSend.UseCase
{
    /// <summary>
    /// Envoi la frame des afficheurs du FCU
    /// </summary>
    public class SendFcuDisplay : BaseSender<FcuDisplayPayload>
    {
        private const int ID = 0x064;
        private const int SIZE = 8;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canBus">Le CAN Bus</param>
        /// <param name="presenter">Le présenteur de sortie</param>
        public SendFcuDisplay(ICanBus canBus, ISendFramePresenter presenter) : base(canBus, presenter)
        {
        }

        /// <summary>
        /// Construction de la frame à partir du payload
        /// </summary>
        /// <param name="payload">Le payload à envoyer</param>
        /// <returns></returns>
        protected override Frame BuildFrame(FcuDisplayPayload payload)
        {
            Frame frame = new(ID, SIZE);

            int speed = (int)payload.Speed;
            if (payload.IsMach)
            {
                speed = (int)(payload.Speed * 100);
            }

            int speedHundreds = speed / 100 * 100;
            int headingHundreds = payload.Heading / 100 * 100;

            bool[] hundreds = new bool[8];
            hundreds[0] = speedHundreds == 100;
            hundreds[1] = speedHundreds == 200;
            hundreds[2] = speedHundreds == 300;
            hundreds[3] = speedHundreds == 400;
            hundreds[4] = speedHundreds == 500;
            hundreds[5] = headingHundreds == 100;
            hundreds[6] = headingHundreds == 200;
            hundreds[7] = headingHundreds == 300;

            frame.Data[0] = (byte)(speed - speedHundreds);
            frame.Data[1] = (byte)(payload.Heading - headingHundreds);
            frame.Data[2] = Frame.BitArrayToByte(hundreds);

            bool[] indicators = new bool[8];
            indicators[0] = payload.IsMach;
            indicators[1] = payload.IsTrack;
            indicators[2] = payload.IsLat;
            indicators[3] = payload.IsFpa;
            indicators[4] = payload.IsSpeedForced;
            indicators[5] = payload.IsHeadingForced;
            indicators[6] = payload.IsAltitudeForced;
            indicators[7] = payload.VerticalSpeed >= 0;

            frame.Data[3] = Frame.BitArrayToByte(indicators);

            int altitude = payload.Altitude / 100;
            frame.Data[4] = (byte)(altitude > byte.MaxValue ? byte.MaxValue : altitude);
            frame.Data[5] = (byte)(altitude > byte.MaxValue ? altitude - byte.MaxValue : 0);

            double verticalSpeedForcePositive = Math.Abs(payload.VerticalSpeed);
            if (payload.IsFpa)
            {
                verticalSpeedForcePositive = Math.Round(verticalSpeedForcePositive, 1);
                frame.Data[6] = (byte)(verticalSpeedForcePositive * 10);
            }
            else
            {
                frame.Data[6] = (byte)(verticalSpeedForcePositive / 100);
            }

            bool[] hiddens = new bool[8];
            hiddens[0] = payload.IsSpeedHidden;
            hiddens[1] = payload.IsHeadingHidden;
            hiddens[2] = payload.IsAltitudeHidden;
            hiddens[3] = payload.IsVerticalSpeedHidden;
            hiddens[4] = false;
            hiddens[5] = false;
            hiddens[6] = false;
            hiddens[7] = false;

            frame.Data[7] = Frame.BitArrayToByte(hiddens);

            return frame;
        }
    }
}
