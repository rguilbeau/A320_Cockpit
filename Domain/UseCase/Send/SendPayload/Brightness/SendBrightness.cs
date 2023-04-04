using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload.Brightness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.Send.SendPayload.Brightness
{
    public class SendBrightness : SendUseCase
    {

        private IBrightnessRepository brightnessRepository;

        public SendBrightness(ICockpitRepository cockpitRepository, ISendPresenter presenter, IBrightnessRepository brightnessRepository) : base(cockpitRepository, presenter)
        {
            this.brightnessRepository = brightnessRepository;
        }

        protected override Frame BuildFrame()
        {
            BrightnessCockpit brightness = brightnessRepository.Find();

            Frame frame = new(brightness.Id, brightness.Size);
            frame.Data[0] = brightness.Fcu;
            
            return frame;
        }
    }
}
