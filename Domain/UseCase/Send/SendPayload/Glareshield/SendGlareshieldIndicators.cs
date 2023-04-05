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
    /// <summary>
    /// Envoi de la frame Glareshield indicators au cockpit
    /// Hérite du UseCase "SendUseCase" la méthode Exec est dans la classe mère.
    /// Ici on consrtuit juste la frame
    /// </summary>
    public class SendGlareshieldIndicators : SendUseCase
    {
        private readonly IFcuGlareshieldIndicators fcuRepository;

        /// <summary>
        /// Création du UseCase
        /// </summary>
        /// <param name="cockpitRepository"></param>
        /// <param name="presenter"></param>
        /// <param name="fcuRepository"></param>
        public SendGlareshieldIndicators(ICockpitRepository cockpitRepository, ISendPresenter presenter, IFcuGlareshieldIndicators fcuRepository) : base(cockpitRepository, presenter)
        {
            this.fcuRepository = fcuRepository;
        }

        /// <summary>
        /// Création de la frame à partir de l'entité
        /// </summary>
        /// <returns>La frame</returns>
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
