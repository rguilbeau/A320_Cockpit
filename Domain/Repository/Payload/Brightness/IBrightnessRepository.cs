﻿using A320_Cockpit.Domain.Entity.Payload.Brightness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Repository.Payload.Brightness
{
    /// <summary>
    /// Le repository des rétroaiclairage du cockpit
    /// </summary>
    public interface IBrightnessRepository : IPayloadRepository<BrightnessCockpit>
    {
    }
}
