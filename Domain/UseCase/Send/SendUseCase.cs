using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Repository.Cockpit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.Send
{
    /// <summary>
    /// Classe mère de tous les UsesCase d'envoi de frame.
    /// Dans les enfants, il faut implémenter la contruiction de la frame à partir de l'entité
    /// </summary>
    public abstract class SendUseCase
    {
        private readonly ICockpitRepository cockpitRepository;
        private readonly static Dictionary<int, Frame> frameHistory = new();
        private readonly ISendPresenter presenter;

        /// <summary>
        /// Création du UseCase
        /// </summary>
        /// <param name="cockpitRepository"></param>
        /// <param name="presenter"></param>
        public SendUseCase(ICockpitRepository cockpitRepository, ISendPresenter presenter)
        {
            this.cockpitRepository = cockpitRepository;
            this.presenter = presenter;
        }

        /// <summary>
        /// Méthode à implémenter dans les enfants pour la contruction de la frame à partir de l'entié
        /// </summary>
        /// <returns></returns>
        protected abstract Frame BuildFrame();

        /// <summary>
        /// Execution du UseCase
        /// </summary>
        public void Exec()
        {
            Frame frame = BuildFrame();

            // Si la frame n'a pas changé, il ne sert à rien de la renvoyer
            bool alreadySent = frameHistory.ContainsKey(frame.Id) && frame.Equals(frameHistory[frame.Id]);
            presenter.IsSent = false;

            if (!alreadySent)
            {
                try
                {
                    cockpitRepository.Send(frame);
                    presenter.IsSent = true;
                    frameHistory[frame.Id] = frame;
                }
                catch (Exception ex)
                {
                    presenter.Error = ex;
                    presenter.IsSent = false;
                    frameHistory.Remove(frame.Id);
                }
            }

            presenter.Present();
        }
    }
}
