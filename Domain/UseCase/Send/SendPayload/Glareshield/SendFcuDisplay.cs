using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.Repository.Payload.Glareshield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.Send.SendPayload.Glareshield
{
    public class SendFcuDisplay : SendUseCase
    {

        private readonly IFcuDisplayRepository repository;

        public SendFcuDisplay(ICockpitRepository cockpitRepository, ISendPresenter presenter, IFcuDisplayRepository repository) : base(cockpitRepository, presenter)
        {
            this.repository = repository;
        }

        protected override Frame BuildFrame()
        {
            FcuDisplay fcuDisplay = repository.Find();
            Frame frame = new(fcuDisplay.Id, fcuDisplay.Size);


            int speed = (int)fcuDisplay.Speed;
            if (fcuDisplay.IsMach)
            {
                speed = (int)(fcuDisplay.Speed * 100);
            }

            int speedHundreds = speed / 100 * 100;
            int headingHundreds = fcuDisplay.Heading / 100 * 100;

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
            frame.Data[1] = (byte)(fcuDisplay.Heading - headingHundreds);
            frame.Data[2] = Frame.BitArrayToByte(hundreds);

            bool[] indicators = new bool[8];
            indicators[0] = fcuDisplay.IsMach;
            indicators[1] = fcuDisplay.IsTrack;
            indicators[2] = fcuDisplay.IsLat;
            indicators[3] = fcuDisplay.IsFpa;
            indicators[4] = fcuDisplay.IsSpeedDot;
            indicators[5] = fcuDisplay.IsHeadingDot;
            indicators[6] = fcuDisplay.IsAltitudeDot;
            indicators[7] = fcuDisplay.VerticalSpeed >= 0;

            frame.Data[3] = Frame.BitArrayToByte(indicators);

            int altitude = fcuDisplay.Altitude / 100;
            frame.Data[4] = (byte)(altitude > byte.MaxValue ? byte.MaxValue : altitude);
            frame.Data[5] = (byte)(altitude > byte.MaxValue ? altitude - byte.MaxValue : 0);

            double verticalSpeedPositive = Math.Abs(fcuDisplay.VerticalSpeed);
            if (fcuDisplay.IsFpa)
            {
                verticalSpeedPositive = Math.Round(verticalSpeedPositive, 1);
                frame.Data[6] = (byte)(verticalSpeedPositive * 10);
            }
            else
            {
                frame.Data[6] = (byte)(verticalSpeedPositive / 100);
            }

            bool[] hiddens = new bool[8];
            hiddens[0] = fcuDisplay.IsSpeedDash;
            hiddens[1] = fcuDisplay.IsHeadingDash;
            hiddens[2] = fcuDisplay.IsAltitudeDash;
            hiddens[3] = fcuDisplay.IsVerticalSpeedHidden;
            hiddens[4] = false; //not used
            hiddens[5] = false; //not used
            hiddens[6] = false; //not used
            hiddens[7] = fcuDisplay.IsPowerOn;

            frame.Data[7] = Frame.BitArrayToByte(hiddens);

            return frame;
        }
    }
}
