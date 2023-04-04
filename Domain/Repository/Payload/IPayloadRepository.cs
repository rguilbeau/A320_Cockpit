using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Repository.Payload
{
    public interface IPayloadRepository<T> where T : PayloadEntity
    {
        public T Find();

        public T FindByEvent(CockpitEvent e);
    }
}
