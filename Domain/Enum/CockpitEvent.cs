using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Enum
{
    /// <summary>
    /// Les évenements du cockpit (bouton appuyé, potare tourné, etc...)
    /// 
    /// Les ids sont définit suivant cette règle :
    /// - Les deux 1er chiffre est le type : (FCU:01)
    /// - Le deux dernier et le numéro de l'évènement
    /// 
    /// </summary>
    public enum CockpitEvent
    {
        ///
        /// Glareshield FCU (0x01##)
        ///
        FCU_AP1                 = 0x0101,
        FCU_AP2                 = 0x0102,
        FCU_ATHR                = 0x0103,
        FCU_LOC                 = 0x0104,
        FCU_EXPED               = 0x0105,
        FCU_APPR                = 0x0106,
        FCU_SPEED_MACH          = 0x0107,
        FCU_VS_FPA              = 0x0108,
        FCU_METRICT_ALT         = 0x0109,
        FCU_SPEED_BUG           = 0x010A,
        FCU_SPEED_PUSH          = 0x010B,
        FCU_SPEED_PULL          = 0x010C,
        FCU_HEADING_BUG         = 0x010D,
        FCU_HEADING_PUSH        = 0x010E,
        FCU_HEADING_PULL        = 0x010F,
        FCU_ALTITUDE_BUG        = 0x0110,
        FCU_ALTITUDE_PUSH       = 0x0111,
        FCU_ALTITUDE_PULL       = 0x0112,
        FCU_ALTITUDE_STEP       = 0x0113,
        FCU_VS_BUG              = 0x0114,
        FCU_VS_PUSH             = 0x0115,
        FCU_VS_PULL             = 0x0116,
        
        //
        // Overhead panel Light panel (0x02##)
        //
        OHP_TEST_LIGHT          = 0x0201
    }
}
