using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload.Glareshield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.Send.SendPayload.Glareshield
{
    public class SendFcu : SendUseCase
    {

        private readonly IFcuRepository fcuRepository;

        public SendFcu(ICockpitRepository cockpitRepository, ISendPresenter presenter, IFcuRepository fcuRepository) : base(cockpitRepository, presenter)
        {
            this.fcuRepository = fcuRepository;
        }

        protected override Frame BuildFrame()
        {
            Fcu fcu = fcuRepository.Find();

            Frame frame = new(fcu.Id, fcu.Size);
            bool[] fcuLight = new bool[8];
            fcuLight[0] = fcu.Ap1;
            fcuLight[1] = fcu.Ap2;
            fcuLight[2] = fcu.Athr;
            fcuLight[3] = fcu.Loc;
            fcuLight[4] = fcu.Exped;
            fcuLight[5] = fcu.Appr;
            fcuLight[6] = false; // not used
            fcuLight[7] = fcu.IsPowerOn;

            frame.Data[0] = Frame.BitArrayToByte(fcuLight);
            return frame;
        }
    }
}
