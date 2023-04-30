
namespace A320_Cockpit.Domain.Enum
{
    /// <summary>
    /// La liste des ID du CAN bus (de 0x000 à 0x7FF)
    /// </summary>
    public enum FrameId
    {
        // Globale (0x##0)
        EVENT = 0x000,
        PING = 0x010,
        BRIGHTNESS_PANEL = 0x020,
        BRIGHTNESS_SEVEN_SEGMENT = 0x030,

        //
        // Glareshield panel (0x##1)
        //
        FCU_DISPLAY                 = 0x011,
        GLARESHIELD_INDICATORS      = 0x021
    }
}
