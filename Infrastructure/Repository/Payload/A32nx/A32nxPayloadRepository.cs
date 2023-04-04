using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx
{
    public abstract class A32nxPayloadRepository<T>
    {

        protected IMsfs msfs;

        public A32nxPayloadRepository(IMsfs msfs)
        {
            this.msfs = msfs;
        }

        public T Find()
        {
            Refresh(null);
            UpdateEntity();
            return Payload;
        }

        public T FindByEvent(CockpitEvent e)
        {
            Refresh(e);
            UpdateEntity();
            return Payload;
        }

        protected abstract T Payload { get; }

        protected abstract void Refresh(CockpitEvent? e);

        protected abstract void UpdateEntity();

    }
}
