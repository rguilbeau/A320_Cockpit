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
    /// <summary>
    /// Envoi de la frame light indicators au cockpit
    /// Hérite du UseCase "SendUseCase" la méthode Exec est dans la classe mère.
    /// Ici on consrtuit juste la frame
    /// </summary>
    public class SendLightIndicator : SendUseCase
    {
        private readonly ILightIndicatorsRepository lightIndicatorsRepository;

        /// <summary>
        /// Création du UseCase
        /// </summary>
        /// <param name="cockpitRepository"></param>
        /// <param name="presenter"></param>
        /// <param name="lightIndicatorsRepository"></param>
        public SendLightIndicator(ICockpitRepository cockpitRepository, ISendPresenter presenter, ILightIndicatorsRepository lightIndicatorsRepository) : base(cockpitRepository, presenter)
        {
            this.lightIndicatorsRepository = lightIndicatorsRepository;
        }

        /// <summary>
        /// Création de la frame à partir de l'entité
        /// </summary>
        /// <returns>La frame</returns>
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
