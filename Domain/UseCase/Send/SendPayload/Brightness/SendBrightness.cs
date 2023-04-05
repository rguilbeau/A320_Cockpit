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
    /// <summary>
    /// Envoi de la frame Brightness au cockpit
    /// Hérite du UseCase "SendUseCase" la méthode Exec est dans la classe mère.
    /// Ici on consrtuit juste la frame
    /// </summary>
    public class SendBrightness : SendUseCase
    {
        private readonly IBrightnessRepository brightnessRepository;

        /// <summary>
        /// Création du UseCase
        /// </summary>
        /// <param name="cockpitRepository"></param>
        /// <param name="presenter"></param>
        /// <param name="brightnessRepository"></param>
        public SendBrightness(ICockpitRepository cockpitRepository, ISendPresenter presenter, IBrightnessRepository brightnessRepository) : base(cockpitRepository, presenter)
        {
            this.brightnessRepository = brightnessRepository;
        }

        /// <summary>
        /// Création de la frame à partir de l'entité
        /// </summary>
        /// <returns>La frame</returns>
        protected override Frame BuildFrame()
        {
            BrightnessCockpit brightness = brightnessRepository.Find();

            Frame frame = new(brightness.Id, brightness.Size);
            frame.Data[0] = brightness.FcuDisplay;
            
            return frame;
        }
    }
}
