using A320_Cockpit.Adapter.MsfsConnectorAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter;
using A320_Cockpit.Domain.CanBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.MsfsVariableUpdater
{
    internal abstract class MsfsUpdator<T> where T : Enum
    {

        protected readonly MsfsConnector msfsConnector;

        protected readonly ICanBus canBus;


        public MsfsUpdator(MsfsConnector msfsConnector, ICanBus can)
        {
            this.msfsConnector = msfsConnector;
            canBus = can;
        }

        public void Update()
        {
            foreach (T update in (T[])Enum.GetValues(typeof(T)))
            {
                Update(update);
            }
        }

        public abstract void Update(T update);

        public void Update(params T[] updates)
        {
            foreach (T update in updates)
            {
                Update(update);
            }
        }
    }
}
