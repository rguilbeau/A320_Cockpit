namespace A320_Cockpit.Domain.BusSend.Payload
{
    /// <summary>
    /// Contenu de la frame des afficheurs du FCU
    /// </summary>
    public class FcuDisplayPayload
    {
        private double speed;
        private short heading;
        private bool isMach;
        private bool isTrack;
        private bool isLat;
        private bool isFpa;
        private bool isSpeedForced;
        private bool isHeadingForced;
        private bool isAltitudeForced;
        private int altitude;
        private double verticalSpeed;
        private bool isSpeedHidden;
        private bool isHeadingHidden;
        private bool isAltitudeHidden;
        private bool isVerticalSpeedHidden;

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
        public bool IsSpeedForced { get => isSpeedForced; set => isSpeedForced = value; }
        /// <summary>
        /// Le cap est forcée par le pilote, le point ne s'affiche pas
        /// </summary>
        public bool IsHeadingForced { get => isHeadingForced; set => isHeadingForced = value; }
        /// <summary>
        /// L'altitude est forcée par le pilote, le point ne s'affiche pas
        /// </summary>
        public bool IsAltitudeForced { get => isAltitudeForced; set => isAltitudeForced = value; }
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
        public bool IsSpeedHidden { get => isSpeedHidden; set => isSpeedHidden = value; }
        /// <summary>
        /// Le cap n'est pas affiché sur le FCU "---"
        /// </summary>
        public bool IsHeadingHidden { get => isHeadingHidden; set => isHeadingHidden = value; }
        /// <summary>
        /// L'altitude n'est pas affichée sur le FCU "-----"
        /// </summary>
        public bool IsAltitudeHidden { get => isAltitudeHidden; set => isAltitudeHidden = value; }
        /// <summary>
        /// La vitesse verticale n'est pas affichée sur le FCU "----"
        /// </summary>
        public bool IsVerticalSpeedHidden { get => isVerticalSpeedHidden; set => isVerticalSpeedHidden = value; }
    }
}
