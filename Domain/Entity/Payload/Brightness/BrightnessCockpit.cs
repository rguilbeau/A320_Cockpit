using A320_Cockpit.Domain.Entity.Cockpit;
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
        private const int SIZE = 7;
        private byte segmentScreens = 255;
        private byte glareshieldPanel = 255;
        private byte overheadPanel = 255;
        private byte pedestalPanel = 255;
        private byte indicators = 255;
        private byte buttons = 255;
        private bool testLight = false;


        /// <summary>
        /// Retourne l'entité converti en frame
        /// </summary>
        public override Frame Frame
        {
            get
            {
                Frame frame = new(Id, Size);
                bool[] indicators = new bool[8];
                indicators[0] = TestLight;
                indicators[1] = false;
                indicators[2] = false;
                indicators[3] = false;
                indicators[4] = false;
                indicators[5] = false;
                indicators[6] = false;
                indicators[7] = false;
                frame.Data[0] = Frame.BitArrayToByte(indicators);

                frame.Data[1] = SegmentScreens;
                frame.Data[2] = GlareshieldPanel;
                frame.Data[3] = OverheadPanel;
                frame.Data[4] = PedestalPanel;
                frame.Data[5] = Indicators;
                frame.Data[6] = Buttons;

                return frame;
            }
        }

        /// <summary>
        /// L'id de la frame
        /// </summary>
        public override int Id => (int)FrameId.BRIGHTNESS;

        /// <summary>
        /// La taille de la frame
        /// </summary>
        public override int Size => SIZE;

        public byte SegmentScreens { get => segmentScreens; set => segmentScreens = value; }
        public byte GlareshieldPanel { get => glareshieldPanel; set => glareshieldPanel = value; }
        public byte OverheadPanel { get => overheadPanel; set => overheadPanel = value; }
        public byte PedestalPanel { get => pedestalPanel; set => pedestalPanel = value; }
        public byte Indicators { get => indicators; set => indicators = value; }
        public byte Buttons { get => buttons; set => buttons = value; }
        public bool TestLight { get => testLight; set => testLight = value; }
    }
}
