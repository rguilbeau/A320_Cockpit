using A320_Cockpit.Adaptation.Msfs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx
{
    /// <summary>
    /// Lise des variables de MSFS (LVar, SimVar...)
    /// </summary>
    public class A32nxVariables
    {
        //
        // POWER
        //
        public static readonly Lvar<bool> IsElectricityAc1BusPowered = new("A32NX_ELEC_AC_1_BUS_IS_POWERED");

        //
        // FCU Display
        //
        public static readonly Lvar<double> SpeedSelected = new("A32NX_AUTOPILOT_SPEED_SELECTED");
        public static readonly Lvar<bool> IsSpeedDot = new("A32NX_FCU_SPD_MANAGED_DOT");
        public static readonly Lvar<bool> IsSpeedManagedDash = new("A32NX_FCU_SPD_MANAGED_DASHES");
        public static readonly SimVar<bool> IsManagedSpeedInMach = new("AUTOPILOT MANAGED SPEED IN MACH", "Bool");

        public static readonly Lvar<short> HeadingSelected = new("A32NX_AUTOPILOT_HEADING_SELECTED");
        public static readonly Lvar<bool> IsHeadingManageDash = new("A32NX_FCU_HDG_MANAGED_DASHES");
        public static readonly Lvar<bool> IsHeadingDot = new("A32NX_FCU_HDG_MANAGED_DOT");
        public static readonly Lvar<bool> IsTrackFpa = new("A32NX_TRK_FPA_MODE_ACTIVE");

        public static readonly SimVar<int> AltitudeSelected = new("AUTOPILOT ALTITUDE LOCK VAR:3", "feet");
        public static readonly Lvar<bool> AltitudeManaged = new("A32NX_FCU_ALT_MANAGED");

        public static readonly Lvar<double> VerticalSpeedSelectedFpa = new("A32NX_AUTOPILOT_FPA_SELECTED");
        public static readonly Lvar<double> VerticalSpeedSelectedFpm = new("A32NX_AUTOPILOT_VS_SELECTED");
        public static readonly Lvar<bool> VerticalSpeedManaged = new("A32NX_FCU_VS_MANAGED");

        //
        // FCU
        //
        public static readonly Lvar<bool> LocModeActive = new("A32NX_FCU_LOC_MODE_ACTIVE");
        public static readonly Lvar<bool> ExpedModeActive = new("A32NX_FMA_EXPEDITE_MODE");
        public static readonly Lvar<bool> ApprModeActive = new("A32NX_FCU_APPR_MODE_ACTIVE");
        public static readonly Lvar<bool> Autopilot1Active = new("A32NX_AUTOPILOT_1_ACTIVE");
        public static readonly Lvar<bool> Autopilot2Active = new("A32NX_AUTOPILOT_2_ACTIVE");
        public static readonly Lvar<short> AutoThrustStatus = new("A32NX_AUTOTHRUST_STATUS");

        //
        // Over head panel
        //
        public static readonly Lvar<short> LightIndicatorStatus = new("A32NX_OVHD_INTLT_ANN");
    }
}
