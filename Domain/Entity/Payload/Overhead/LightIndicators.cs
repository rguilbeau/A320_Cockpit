using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Entity.Payload.Overhead
{
    /// <summary>
    /// Contenu de la frame des informations des lumières du cockpit
    /// </summary>
    public class LightIndicators : PayloadEntity
    {
        private const int SIZE = 1;
        private bool testIndicatorsLight = false;

        /// <summary>
        /// Retourne l'entité converti en frame
        /// </summary>
        public override Frame Frame
        {
            get
            {
                Frame frame = new(Id, Size);

                bool[] indicatorLight = new bool[8];
                indicatorLight[0] = TestIndicatorsLight;
                indicatorLight[1] = false; // not used
                indicatorLight[2] = false; // not used
                indicatorLight[3] = false; // not used
                indicatorLight[4] = false; // not used
                indicatorLight[5] = false; // not used
                indicatorLight[6] = false; // not used
                indicatorLight[7] = false; // not used

                frame.Data[0] = Frame.BitArrayToByte(indicatorLight);

                return frame;

            }
        }

        /// <summary>
        /// L'id de la frame
        /// </summary>
        public override int Id => (int)FrameId.LIGHT_INDICATOR;
        /// <summary>
        /// La taille de la frame
        /// </summary>
        public override int Size => SIZE;
        /// <summary>
        /// Le test des indicateurs est sur ON (affiche toutes les limières du cockpit pour checker l'état)
        /// </summary>
        public bool TestIndicatorsLight { get => testIndicatorsLight; set => testIndicatorsLight = value; }
    }
}
