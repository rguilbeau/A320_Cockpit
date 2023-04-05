using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Repository.Payload.Glareshield
{
    /// <summary>
    /// Le repository des écrans du FCU
    /// </summary>
    public interface IFcuDisplayRepository : IPayloadRepository<FcuDisplay>
    {
    }
}
