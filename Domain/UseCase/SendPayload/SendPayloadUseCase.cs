using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.SendPayload
{
    /// <summary>
    /// UseCase pour l'envoi d'une frame
    /// </summary>
    public class SendPayloadUseCase
    {
        private readonly ISendPayloadPresenter presenter;
        private readonly ICockpitRepository cockpitRepository;
        private readonly IPayloadRepository payloadRepository;
        private readonly static Dictionary<int, Frame> frameHistory = new();
        
        /// <summary>
        /// Création du UseCase
        /// </summary>
        /// <param name="cockpitRepository"></param>
        /// <param name="presenter"></param>
        public SendPayloadUseCase(ICockpitRepository cockpitRepository, IPayloadRepository payloadRepository, ISendPayloadPresenter presenter)
        {
            this.presenter = presenter;
            this.cockpitRepository = cockpitRepository;
            this.payloadRepository = payloadRepository;
        }

        /// <summary>
        /// Execute le UseCase
        /// </summary>
        public void Exec()
        {
            PayloadEntity payload = payloadRepository.Find();
            Frame frame = payload.Frame;

            // Si la frame n'a pas changé, il ne sert à rien de la renvoyer
            bool alreadySent = frameHistory.ContainsKey(frame.Id) && frame.Equals(frameHistory[frame.Id]);

            if (alreadySent)
            {
                presenter.IsSent = false;
            } else { 
                try
                {
                    cockpitRepository.Send(frame);
                    presenter.IsSent = true;
                    presenter.Frame = frame;
                    frameHistory[frame.Id] = frame;
                } catch (Exception ex)
                {
                    presenter.IsSent= false;
                    presenter.Error = ex;
                    frameHistory.Remove(frame.Id);
                }
            }

            presenter.Present();
        }

    }
}
