using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Enum
{
    public enum FrameId
    {
        //
        // Glareshield panel (0x1##)
        //
        FCU_DISPLAY                 = 0x101,
        GLARESHIELD_INDICATORS      = 0x102,
        BRIGHTNESS                  = 0x003,

        //
        // Overhead panel (0x2##)
        //
        LIGHT_INDICATOR             = 0x201
    }
}
