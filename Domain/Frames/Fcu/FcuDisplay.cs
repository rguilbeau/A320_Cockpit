using A320_Cockpit.Domain.Can;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Frames.Fcu
{
    internal class FcuDisplay: IFrame<FcuDisplayPayload>
    {

        private static readonly int ID = 0x064;

        private static readonly int SIZE = 8;

        private ICan _can;

        public FcuDisplay(ICan can)
        {
            _can = can;
        }

        public Response Send(FcuDisplayPayload payload)
        {
            CanMessage message = new(ID, SIZE);

            int speed = (int)payload.Speed;
            if (payload.IsMach)
            {
                speed = (int)(payload.Speed * 100);
            }

            int speedHundreds = speed / 100 * 100;
            int headingHundreds = payload.Heading / 100 * 100;

            BitArray hundreds = new BitArray(8);

            hundreds[0] = speedHundreds == 100;
            hundreds[1] = speedHundreds == 200;
            hundreds[2] = speedHundreds == 300;
            hundreds[3] = speedHundreds == 400;
            hundreds[4] = speedHundreds == 500;
            hundreds[5] = headingHundreds == 100;
            hundreds[6] = headingHundreds == 200;
            hundreds[7] = headingHundreds == 300;

            message.Data[0] = (byte)(speed - speedHundreds);
            message.Data[1] = (byte)(payload.Heading - headingHundreds);
            message.Data[2] = CanMessage.BitArrayToByte(hundreds);

            BitArray indicators = new BitArray(8);
            indicators[0] = payload.IsMach;
            indicators[1] = payload.IsTrack;
            indicators[2] = payload.IsLat;
            indicators[3] = payload.IsFpa;
            indicators[4] = payload.IsSpeedForced;
            indicators[5] = payload.IsHeadingForced;
            indicators[6] = payload.IsAltitudeForced;
            indicators[7] = payload.IsVerticalSpeedPositive;

            message.Data[3] = CanMessage.BitArrayToByte(indicators);

            int altitude = payload.Altitude / 100;
            message.Data[4] = (byte)(altitude > byte.MaxValue ? byte.MaxValue : altitude);
            message.Data[5] = (byte)(altitude > byte.MaxValue ? altitude - byte.MaxValue : 0);

            if (payload.IsFpa)
            {
                message.Data[6] = (byte)(payload.VerticalSpeed * 10);
            }
            else
            {
                message.Data[6] = (byte)(payload.VerticalSpeed / 100);
            }

            BitArray hiddens = new BitArray(8);
            hiddens[0] = payload.IsSpeedHidden;
            hiddens[1] = payload.IsHeadingHidden;
            hiddens[2] = payload.IsAltitudeHidden;
            hiddens[3] = payload.IsVerticalSpeedHidden;
            hiddens[4] = false;
            hiddens[5] = false;
            hiddens[6] = false;
            hiddens[7] = false;

            message.Data[7] = CanMessage.BitArrayToByte(hiddens);

            return new Response(_can.Send(message), ID);
        }
    }
}
