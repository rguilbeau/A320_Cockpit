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
        /// <summary>
        /// Le niveau de rétroéclairage des afficheurs à segements (0 à 255)
        /// </summary>
        public byte SegmentScreens { get => segmentScreens; set => segmentScreens = value; }
        /// <summary>
        /// Le niveau de rétroéclairage des panels du Glareshield (0 à 255)
        /// </summary>
        public byte GlareshieldPanel { get => glareshieldPanel; set => glareshieldPanel = value; }
        /// <summary>
        /// Le niveau de rétroéclairage des panels de l'Overhead (0 à 255)
        /// </summary>
        public byte OverheadPanel { get => overheadPanel; set => overheadPanel = value; }
        /// <summary>
        /// Le niveau de rétroéclairage des panels du Pedestal (0 à 255)
        /// </summary>
        public byte PedestalPanel { get => pedestalPanel; set => pedestalPanel = value; }
        /// <summary>
        /// Le niveau de rétroéclairage des indicteurs des boutons de tous le cockpit (par exemple le témoins AP1 actif)
        /// </summary>
        public byte Indicators { get => indicators; set => indicators = value; }
        /// <summary>
        /// Le niveau de rétroéclairage des boutons de tous le cockpit (le texte ou simbole des boutons)
        /// </summary>
        public byte Buttons { get => buttons; set => buttons = value; }
        /// <summary>
        /// Mode de test des temoins (allume tous les témoins de tous le cockpit)
        /// </summary>
        public bool TestLight { get => testLight; set => testLight = value; }
    }
}
