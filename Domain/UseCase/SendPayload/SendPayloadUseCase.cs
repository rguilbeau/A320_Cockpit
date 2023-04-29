using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Enum;
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
        private readonly List<ISendPayloadPresenter> presenters;
        private readonly ICockpitRepository cockpitRepository;
        private readonly IPayloadRepository payloadRepository;
        private readonly static Dictionary<int, Frame> frameHistory = new();
        
        /// <summary>
        /// Création du UseCase
        /// </summary>
        /// <param name="cockpitRepository"></param>
        /// <param name="presenter"></param>
        public SendPayloadUseCase(ICockpitRepository cockpitRepository, IPayloadRepository payloadRepository)
        {
            this.cockpitRepository = cockpitRepository;
            this.payloadRepository = payloadRepository;
            presenters = new();
        }

        /// <summary>
        /// Execute le UseCase
        /// </summary>
        public void Exec(CockpitEvent e)
        {
            PayloadEntity payload = payloadRepository.Find(e);
            Frame frame = payload.Frame;

            // Si la frame n'a pas changé, il ne sert à rien de la renvoyer
            bool alreadySent = frameHistory.ContainsKey(frame.Id) && frame.Equals(frameHistory[frame.Id]);

            if (alreadySent)
            {
                presenters.ForEach(presenter => presenter.IsSent = false);
            } else { 
                try
                {
                    cockpitRepository.Send(frame);
                    presenters.ForEach(presenter => presenter.IsSent = true);
                    presenters.ForEach(presenter => presenter.Frame = frame);
                    frameHistory[frame.Id] = frame;
                } catch (Exception ex)
                {
                    presenters.ForEach(presenter => presenter.IsSent = false);
                    presenters.ForEach(presenter => presenter.Error = ex);
                    frameHistory.Remove(frame.Id);
                }
            }

            presenters.ForEach(presenter => presenter.Present());
        }

        /// <summary>
        /// Ajoute un présenter
        /// </summary>
        /// <param name="presenter"></param>
        public void AddPresenter(ISendPayloadPresenter presenter)
        {
            presenters.Add(presenter);
        }

        /// <summary>
        /// Supprime un présenter
        /// </summary>
        /// <param name="presenter"></param>
        public void RemovePresenter(ISendPayloadPresenter presenter)
        {
            presenters.Remove(presenter);
        }

    }
}
