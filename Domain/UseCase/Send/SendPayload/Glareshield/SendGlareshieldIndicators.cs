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
    public class SendGlareshieldIndicators : SendUseCase
    {

        private readonly IFcuGlareshieldIndicators fcuRepository;

        public SendGlareshieldIndicators(ICockpitRepository cockpitRepository, ISendPresenter presenter, IFcuGlareshieldIndicators fcuRepository) : base(cockpitRepository, presenter)
        {
            this.fcuRepository = fcuRepository;
        }

        protected override Frame BuildFrame()
        {
            GlareshieldIndicators glareshieldIndicators = fcuRepository.Find();

            Frame frame = new(glareshieldIndicators.Id, glareshieldIndicators.Size);
            bool[] fcuLight = new bool[8];
            fcuLight[0] = glareshieldIndicators.FcuAp1;
            fcuLight[1] = glareshieldIndicators.FcuAp2;
            fcuLight[2] = glareshieldIndicators.FcuAthr;
            fcuLight[3] = glareshieldIndicators.FcuLoc;
            fcuLight[4] = glareshieldIndicators.FcuExped;
            fcuLight[5] = glareshieldIndicators.FcuAppr;
            fcuLight[6] = false; // not used
            fcuLight[7] = glareshieldIndicators.FcuIsPowerOn;

            frame.Data[0] = Frame.BitArrayToByte(fcuLight);
            return frame;
        }
    }
}
