using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Enum
{
    public enum CockpitEvent
    {
        FCU_AP1,
        FCU_AP2,
        FCU_ATHR,
        FCU_LOC,
        FCU_EXPED,
        FCU_APPR,
        FCU_SPEED_MACH,
        FCU_VS_FPA,
        FCU_METRICT_ALT,
        FCU_SPEED_BUG,
        FCU_SPEED_PUSH,
        FCU_SPEED_PULL,
        FCU_HEADING_BUG,
        FCU_HEADING_PUSH,
        FCU_HEADING_PULL,
        FCU_ALTITUDE_BUG,
        FCU_ALTITUDE_PUSH,
        FCU_ALTITUDE_PULL,
        FCU_ALTITUDE_STEP,
        FCU_VS_BUG,
        FCU_VS_PUSH,
        FCU_VS_PULL,
        OHP_TEST_LIGHT
    }
}
