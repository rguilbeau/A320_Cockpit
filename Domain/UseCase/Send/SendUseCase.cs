using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Repository.Cockpit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.Send
{
    public abstract class SendUseCase
    {
        private readonly ICockpitRepository cockpitRepository;

        private readonly static Dictionary<int, Frame> frameHistory = new();
        private readonly ISendPresenter presenter;

        public SendUseCase(ICockpitRepository cockpitRepository, ISendPresenter presenter)
        {
            this.cockpitRepository = cockpitRepository;
            this.presenter = presenter;
        }


        public void Exec()
        {
            Frame frame = BuildFrame();
            bool alreadySent = !frameHistory.ContainsKey(frame.Id) && frame.Equals(frameHistory[frame.Id]);
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

        protected abstract Frame BuildFrame();
    }
}
