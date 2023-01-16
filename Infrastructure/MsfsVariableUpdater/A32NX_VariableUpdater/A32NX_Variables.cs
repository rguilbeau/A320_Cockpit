using A320_Cockpit.Adapter.MsfsConnectorAdapter.FcuipcAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter;

namespace A320_Cockpit.Infrastructure.MsfsVariableUpdater.A32NX_VariableUpdater
{
    /// <summary>
    /// Définition des varaibles de l'A32NX de FlyByWire
    /// </summary>
    public static class A32NX_Variables
    {
        /// <summary>
        /// Bus d'alimentation en électricité
        /// </summary>
        public static class Eletricity
        {
            public static readonly Lvar<bool> IsElectricityAc1BusPowered = new("L:A32NX_ELEC_AC_1_BUS_IS_POWERED");
        }

        /// <summary>
        /// Indicateurs de lumières
        /// </summary>
        public static class LightIndicators
        {
            public static readonly Lvar<short> LightIndicatorStatus = new("A32NX_OVHD_INTLT_ANN");
        }

        /// <summary>
        /// Afficheurs du FCU
        /// </summary>
        public static class FcuDisplay
        {
            public static readonly Lvar<double> SpeedSelected = new("L:A32NX_AUTOPILOT_SPEED_SELECTED");
            public static readonly Lvar<bool> IsSpeedDot = new("L:A32NX_FCU_SPD_MANAGED_DOT");
            public static readonly Lvar<bool> IsSpeedManagedDash = new("L:A32NX_FCU_SPD_MANAGED_DASHES");
            public static readonly SimVar<bool> IsMachSpeed = new("AUTOPILOT MANAGED SPEED IN MACH", "Bool");

            public static readonly Lvar<short> HeadingSelected = new("A32NX_AUTOPILOT_HEADING_SELECTED");
            public static readonly Lvar<bool> IsHeadingManageDash = new("L:A32NX_FCU_HDG_MANAGED_DASHES");
            public static readonly Lvar<bool> IsHeadingDot = new("L:A32NX_FCU_HDG_MANAGED_DOT");
            public static readonly Lvar<bool> IsTrackFpa = new("L:A32NX_TRK_FPA_MODE_ACTIVE");

            public static readonly SimVar<int> AltitudeSelected = new("AUTOPILOT ALTITUDE LOCK VAR:3", "feet");
            public static readonly Lvar<bool> AltitudeManaged = new("L:A32NX_FCU_ALT_MANAGED");

            public static readonly Lvar<double> VerticalSpeedSelectedFpa = new("L:A32NX_AUTOPILOT_FPA_SELECTED");
            public static readonly Lvar<double> VerticalSpeedSelectedFpm = new("L:A32NX_AUTOPILOT_VS_SELECTED");
            public static readonly Lvar<bool> VerticalSpeedManaged = new("L:A32NX_FCU_VS_MANAGED");
        }

        /// <summary>
        /// Le panel FCU
        /// </summary>
        public static class FcuPanel
        {
            public static readonly Lvar<bool> IsExpeditedMode = new("A32NX_FMA_EXPEDITE_MODE");
        }
    }
}
