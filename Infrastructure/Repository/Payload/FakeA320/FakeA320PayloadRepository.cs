using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.FakeA320
{
    public abstract class FakeA320PayloadRepository<T> : IPayloadRepository where T: PayloadEntity
    {
        private bool askRefresh;

        public abstract T Payload { get; }

        public FakeA320PayloadRepository()
        {
            askRefresh = true;
        }

        public bool AskRefresh { get { return askRefresh; } set { askRefresh = value; } }

        protected abstract T BuildPayload();

        protected abstract bool Refresh(CockpitEvent e);

        public PayloadEntity Find(CockpitEvent e) 
        {
            if(Refresh(e))
            {
                askRefresh = false;
                return BuildPayload();
            } else
            {
                askRefresh = false;
                return Payload;
            }
        }

    }
}
