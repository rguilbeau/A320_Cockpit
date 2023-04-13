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
        public static readonly KEvent<KEventEmpty> FcuSpeedIncr = new("A32NX.FCU_SPD_INC");
        public static readonly KEvent<KEventEmpty> FcuSpeedDecr = new("A32NX.FCU_SPD_DEC");
    }
}
