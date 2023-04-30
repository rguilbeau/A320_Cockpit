using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Entity.Payload.Brightness
{
    public class BrightnessSevenSegments : PayloadEntity
    {
        private const int SIZE = 5;
        private bool isTestLight = false;
        private byte fcu = 255;
        private byte altimeters = 255;
        private byte bateries = 255;
        private byte radio = 255;


        /// <summary>
        /// Retourne l'entité converti en frame
        /// </summary>
        public override Frame Frame
        {
            get
            {
                Frame frame = new(Id, Size);
                bool[] testLight = new bool[8];
                testLight[0] = IsTestLight;
                testLight[1] = false; // not used
                testLight[2] = false; // not used
                testLight[3] = false; // not used
                testLight[4] = false; // not used
                testLight[5] = false; // not used
                testLight[6] = false; // not used
                testLight[7] = false; // not used
                frame.Data[0] = Frame.BitArrayToByte(testLight);

                frame.Data[1] = Fcu;
                frame.Data[2] = Altimeters;
                frame.Data[3] = Bateries;
                frame.Data[4] = Radio;

                return frame;
            }
        }

        /// <summary>
        /// L'id de la frame
        /// </summary>
        public override int Id => (int)FrameId.BRIGHTNESS_SEVEN_SEGMENT;
        /// <summary>
        /// La taille de la frame
        /// </summary>
        public override int Size => SIZE;
        /// <summary>
        /// Le mode "Test light" est actif
        /// </summary>
        public bool IsTestLight { get => isTestLight; set => isTestLight = value; }
        /// <summary>
        /// Rétroéclairage des écrans du FCU (0 à 255)
        /// </summary>
        public byte Fcu { get => fcu; set => fcu = value; }
        /// <summary>
        /// Rétroaiclairage des écrans des altimètres (0 à 255)
        /// </summary>
        public byte Altimeters { get => altimeters; set => altimeters = value; }
        /// <summary>
        /// Rétroaiclairage des écrans des BAT1 et BAT2 (0 à 255)
        /// </summary>
        public byte Bateries { get => bateries; set => bateries = value; }
        /// <summary>
        /// Rétroaiclairage des écrans des radios (0 à 255)
        /// </summary>
        public byte Radio { get => radio; set => radio = value; }
    }
}
