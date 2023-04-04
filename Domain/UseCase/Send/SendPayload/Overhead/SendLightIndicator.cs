using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Entity.Payload.Overhead;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Domain.Repository.Payload.Overhead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.Send.SendPayload.Overhead
{
    public class SendLightIndicator : SendUseCase
    {

        private ILightIndicatorsRepository lightIndicatorsRepository;

        public SendLightIndicator(ICockpitRepository cockpitRepository, ISendPresenter presenter, ILightIndicatorsRepository lightIndicatorsRepository) : base(cockpitRepository, presenter)
        {
            this.lightIndicatorsRepository = lightIndicatorsRepository;
        }

        protected override Frame BuildFrame()
        {
            LightIndicators lightIndicators = lightIndicatorsRepository.Find();

            Frame frame = new(lightIndicators.Id, lightIndicators.Size);

            bool[] indicatorLight = new bool[8];
            indicatorLight[0] = lightIndicators.TestIndicatorsLight;
            indicatorLight[1] = false; // not used
            indicatorLight[2] = false; // not used
            indicatorLight[3] = false; // not used
            indicatorLight[4] = false; // not used
            indicatorLight[5] = false; // not used
            indicatorLight[6] = false; // not used
            indicatorLight[7] = false; // not used

            frame.Data[0] = Frame.BitArrayToByte(indicatorLight);

            return frame;
        }
    }
}
