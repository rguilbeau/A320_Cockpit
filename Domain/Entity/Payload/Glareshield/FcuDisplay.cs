using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;

namespace A320_Cockpit.Domain.Entity.Payload.Glareshield
{
    /// <summary>
    /// Les informations affiché sur les écrans du FCU
    /// </summary>
    public class FcuDisplay : PayloadEntity
    {
        private const int SIZE = 8;
        private double speed = 0;
        private short heading = 0;
        private bool isMach = false;
        private bool isTrack = false;
        private bool isLat = false;
        private bool isFpa = false;
        private bool isSpeedDot = false;
        private bool isHeadingDot = false;
        private bool isAltitudeDot = false;
        private int altitude = 0;
        private double verticalSpeed = 0;
        private bool isSpeedDash = false;
        private bool isHeadingDash = false;
        private bool isAltitudeDash = false;
        private bool isVerticalSpeedDash = false;

        /// <summary>
        /// Retoune l'entité converti en frame
        /// </summary>
        public override Frame Frame
        {
            get
            {
                Frame frame = new(Id, Size);

                int speed = (int)Speed;
                if (IsMach)
                {
                    speed = (int)(Math.Round(Speed * 100));
                }

                int speedHundreds = speed / 100 * 100;
                int headingHundreds = Heading / 100 * 100;

                bool[] hundreds = new bool[8];
                hundreds[0] = speedHundreds == 100;
                hundreds[1] = speedHundreds == 200;
                hundreds[2] = speedHundreds == 300;
                hundreds[3] = speedHundreds == 400;
                hundreds[4] = speedHundreds == 500;
                hundreds[5] = headingHundreds == 100;
                hundreds[6] = headingHundreds == 200;
                hundreds[7] = headingHundreds == 300;

                frame.Data[0] = (byte)(speed - speedHundreds);
                frame.Data[1] = (byte)(Heading - headingHundreds);
                frame.Data[2] = Frame.BitArrayToByte(hundreds);

                bool[] indicators = new bool[8];
                indicators[0] = IsMach;
                indicators[1] = IsTrack;
                indicators[2] = IsLat;
                indicators[3] = IsFpa;
                indicators[4] = IsSpeedDot;
                indicators[5] = IsHeadingDot;
                indicators[6] = IsAltitudeDot;
                indicators[7] = VerticalSpeed >= 0;
                frame.Data[3] = Frame.BitArrayToByte(indicators);

                int altitude = Altitude / 100;
                frame.Data[4] = (byte)(altitude > byte.MaxValue ? byte.MaxValue : altitude);
                frame.Data[5] = (byte)(altitude > byte.MaxValue ? altitude - byte.MaxValue : 0);

                double verticalSpeedPositive = Math.Abs(VerticalSpeed);
                if (IsFpa)
                {
                    verticalSpeedPositive = Math.Round(verticalSpeedPositive, 1);
                    frame.Data[6] = (byte)(verticalSpeedPositive * 10);
                }
                else
                {
                    frame.Data[6] = (byte)(verticalSpeedPositive / 100);
                }

                bool[] hiddens = new bool[8];
                hiddens[0] = IsSpeedDash;
                hiddens[1] = IsHeadingDash;
                hiddens[2] = IsAltitudeDash;
                hiddens[3] = IsVerticalSpeedDash;
                hiddens[4] = false; //not used
                hiddens[5] = false; //not used
                hiddens[6] = false; //not used
                hiddens[7] = false; //not used

                frame.Data[7] = Frame.BitArrayToByte(hiddens);

                return frame;
            }
        }

        /// <summary>
        /// L'id de la frame
        /// </summary>
        public override int Id => (int) FrameId.FCU_DISPLAY;
        /// <summary>
        /// La taille de la frame
        /// </summary>
        public override int Size => SIZE;
        /// <summary>
        /// La vitesse affichée sur le FCU (en knots ou en mach)
        /// </summary>
        public double Speed { get => speed; set => speed = value; }
        /// <summary>
        /// Le cap affiché sur le FCU
        /// </summary>
        public short Heading { get => heading; set => heading = value; }
        /// <summary>
        /// Le FCU affiche une vitesse en mach
        /// </summary>
        public bool IsMach { get => isMach; set => isMach = value; }
        /// <summary>
        /// Le FCU suit un cap en mode track
        /// </summary>
        public bool IsTrack { get => isTrack; set => isTrack = value; }
        /// <summary>
        /// L'indicateur LAT est affichée
        /// </summary>
        public bool IsLat { get => isLat; set => isLat = value; }
        /// <summary>
        /// Le FCU suit une descente verticale en mode FPA
        /// </summary>
        public bool IsFpa { get => isFpa; set => isFpa = value; }
        /// <summary>
        /// La vitesse est forcée par le pilote, le point ne s'affiche pas
        /// </summary>
        public bool IsSpeedDot { get => isSpeedDot; set => isSpeedDot = value; }
        /// <summary>
        /// Le cap est forcée par le pilote, le point ne s'affiche pas
        /// </summary>
        public bool IsHeadingDot { get => isHeadingDot; set => isHeadingDot = value; }
        /// <summary>
        /// L'altitude est forcée par le pilote, le point ne s'affiche pas
        /// </summary>
        public bool IsAltitudeDot { get => isAltitudeDot; set => isAltitudeDot = value; }
        /// <summary>
        /// L'atitude affichée sur le FCU
        /// </summary>
        public int Altitude { get => altitude; set => altitude = value; }
        /// <summary>
        /// La vitesse verticale affiché sur le FCU (en pied par minutes ou en degrés)
        /// </summary>
        public double VerticalSpeed { get => verticalSpeed; set => verticalSpeed = value; }
        /// <summary>
        /// La vitesse n'est pas affichée sur le FCU "---"
        /// </summary>
        public bool IsSpeedDash { get => isSpeedDash; set => isSpeedDash = value; }
        /// <summary>
        /// Le cap n'est pas affiché sur le FCU "---"
        /// </summary>
        public bool IsHeadingDash { get => isHeadingDash; set => isHeadingDash = value; }
        /// <summary>
        /// L'altitude n'est pas affichée sur le FCU "-----"
        /// </summary>
        public bool IsAltitudeDash { get => isAltitudeDash; set => isAltitudeDash = value; }
        /// <summary>
        /// La vitesse verticale n'est pas affichée sur le FCU "----"
        /// </summary>
        public bool IsVerticalSpeedDash { get => isVerticalSpeedDash; set => isVerticalSpeedDash = value; }
    }
}
