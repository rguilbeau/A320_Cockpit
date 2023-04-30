using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Enum
{
    /// <summary>
    /// La liste des ID du CAN bus (de 0x000 à 0x7FF)
    /// </summary>
    public enum FrameId
    {
        // Ping
        PING = 0x7FF,

        //
        // Globale (0x0##)
        //
        BRIGHTNESS = 0x001,

        //
        // Glareshield panel (0x1##)
        //
        FCU_DISPLAY                 = 0x101,
        GLARESHIELD_INDICATORS      = 0x102
    }
}
