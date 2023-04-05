using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Entity.Payload.Glareshield
{
    /// <summary>
    /// Le témoins des boutons des panels du glareshield (AP1, AP2...) 
    /// </summary>
    public class GlareshieldIndicators : PayloadEntity
    {
        private const int SIZE = 1;
        private bool fcuAp1 = false;
        private bool fcuAp2 = false;
        private bool fcuAthr = false;
        private bool fcuLoc = false;
        private bool fcuAppr = false;
        private bool fcuExped = false;
        private bool fcuIsPowerOn = false;

        /// <summary>
        /// L'id de la frame
        /// </summary>
        public override int Id => (int)FrameId.GLARESHIELD_INDICATORS;
        /// <summary>
        /// La taille de la frame
        /// </summary>
        public override int Size => SIZE;
        /// <summary>
        /// Le témoin du bouton AP1 (FCU)
        /// </summary>
        public bool FcuAp1 { get => fcuAp1; set => fcuAp1 = value; }
        /// <summary>
        /// Le témoin du bouton AP2 (FCU)
        /// </summary>
        public bool FcuAp2 { get => fcuAp2; set => fcuAp2 = value; }
        /// <summary>
        /// Le témoin du bouton A/THR (FCU)
        /// </summary>
        public bool FcuAthr { get => fcuAthr; set => fcuAthr = value; }
        /// <summary>
        /// Le témoin du bouton LOC (FCU)
        /// </summary>
        public bool FcuLoc { get => fcuLoc; set => fcuLoc = value; }
        /// <summary>
        /// Le témoin du bouton APPR (FCU)
        /// </summary>
        public bool FcuAppr { get => fcuAppr; set => fcuAppr = value; }
        /// <summary>
        /// Le témoin du bouton EXPED (FCU)
        /// </summary>
        public bool FcuExped { get => fcuExped; set => fcuExped = value; }
        /// <summary>
        /// Le FCU est allimenté
        /// </summary>
        public bool FcuIsPowerOn { get => fcuIsPowerOn; set => fcuIsPowerOn = value; }
    }
}
