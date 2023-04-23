using A320_Cockpit.Adaptation.Msfs.Model.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.EventHandler.A32nx
{
    /// <summary>
    /// Liste des évenements de l'A32NX
    /// </summary>
    public class A32nxEvents
    {
        //
        // FCU
        //
        // SPD Bug
        public static readonly KEvent<KEventEmpty> FcuSpeedIncr = new("A32NX.FCU_SPD_INC");
        public static readonly KEvent<KEventEmpty> FcuSpeedDecr = new("A32NX.FCU_SPD_DEC");
        public static readonly KEvent<KEventEmpty> FcuSpeedPush = new("A32NX.FCU_SPD_PUSH");
        public static readonly KEvent<KEventEmpty> FcuSpeedPull = new("A32NX.FCU_SPD_PULL");
        // HDG Bug
        public static readonly KEvent<KEventEmpty> FcuHdgIncr = new("A32NX.FCU_HDG_INC");
        public static readonly KEvent<KEventEmpty> FcuHdgDecr = new("A32NX.FCU_HDG_DEC");
        public static readonly KEvent<KEventEmpty> FcuHdgPush = new("A32NX.FCU_HDG_PUSH");
        public static readonly KEvent<KEventEmpty> FcuHdgPull = new("A32NX.FCU_HDG_PULL");
        // ALT Bug
        public static readonly KEvent<short> FcuAltIncr = new("A32NX.FCU_ALT_INC");
        public static readonly KEvent<short> FcuAltDecr = new("A32NX.FCU_ALT_DEC");
        public static readonly KEvent<KEventEmpty> FcuAltPush = new("A32NX.FCU_ALT_PUSH"); 
        public static readonly KEvent<KEventEmpty> FcuAltPull = new("A32NX.FCU_ALT_PULL");
        // VS Bug
        public static readonly KEvent<KEventEmpty> FcuVsIncr = new("A32NX.FCU_VS_INC");
        public static readonly KEvent<KEventEmpty> FcuVsDecr = new("A32NX.FCU_VS_DEC");
        public static readonly KEvent<KEventEmpty> FcuVsPush = new("A32NX.FCU_VS_PUSH");
        public static readonly KEvent<KEventEmpty> FcuVsPull = new("A32NX.FCU_VS_PULL");

    }
}
