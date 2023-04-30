using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;

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

        /// <summary>
        /// Retourne l'entité converti en Frame
        /// </summary>
        public override Frame Frame
        {
            get
            {
                Frame frame = new(Id, Size);
                bool[] fcuLight = new bool[8];
                fcuLight[0] = FcuAp1;
                fcuLight[1] = FcuAp2;
                fcuLight[2] = FcuAthr;
                fcuLight[3] = FcuLoc;
                fcuLight[4] = FcuExped;
                fcuLight[5] = FcuAppr;
                fcuLight[6] = false; // not used
                fcuLight[7] = false; // not used

                frame.Data[0] = Frame.BitArrayToByte(fcuLight);
                return frame;
            }
        }

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
    }
}
