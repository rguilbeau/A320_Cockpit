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
        // Globale (0x0##)
        //
        BRIGHTNESS = 0x001,

        //
        // Glareshield panel (0x1##)
        //
        FCU_DISPLAY                 = 0x101,
        GLARESHIELD_INDICATORS      = 0x102,
        
        //
        // Overhead panel (0x2##)
        //
        LIGHT_INDICATOR             = 0x201
    }
}
