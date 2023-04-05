using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Entity.Payload.Brightness
{
    /// <summary>
    /// Le rétroéclaige du cockpit (les panels, boutons...)
    /// </summary>
    public class BrightnessCockpit : PayloadEntity
    {
        private const int SIZE = 1;
        private byte fcuDisplay = 100;

        /// <summary>
        /// L'id de la frame
        /// </summary>
        public override int Id => (int)FrameId.BRIGHTNESS;

        /// <summary>
        /// La taille de la frame
        /// </summary>
        public override int Size => SIZE;

        /// <summary>
        /// La valeur de rétroaiclairage des écrans du FCU
        /// </summary>
        public byte FcuDisplay { get => fcuDisplay; set => fcuDisplay = value; }
    }
}
