using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;

namespace A320_Cockpit.Domain.Entity.Payload.Brightness
{
    /// <summary>
    /// Le rétroéclaige du cockpit (les panels, boutons...)
    /// </summary>
    public class BrightnessPanel : PayloadEntity
    {
        private const int SIZE = 5;
        private byte glareshieldPanel = 255;
        private byte overheadPanel = 255;
        private byte pedestalPanel = 255;
        private byte indicators = 255;
        private byte buttons = 255;

        /// <summary>
        /// Retourne l'entité converti en frame
        /// </summary>
        public override Frame Frame
        {
            get
            {
                Frame frame = new(Id, Size);
                
                frame.Data[0] = GlareshieldPanel;
                frame.Data[1] = OverheadPanel;
                frame.Data[2] = PedestalPanel;
                frame.Data[3] = Indicators;
                frame.Data[4] = Buttons;

                return frame;
            }
        }

        /// <summary>
        /// L'id de la frame
        /// </summary>
        public override int Id => (int)FrameId.BRIGHTNESS_PANEL;
        /// <summary>
        /// La taille de la frame
        /// </summary>
        public override int Size => SIZE;
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
    }
}
