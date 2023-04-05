using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Repository.Payload.Glareshield
{
    /// <summary>
    /// Le repository des témoins des boutons des panels du Glareshield
    /// </summary>
    public interface IFcuGlareshieldIndicators : IPayloadRepository<GlareshieldIndicators>
    {
    }
}
