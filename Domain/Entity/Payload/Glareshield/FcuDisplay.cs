using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private bool isSpeedForced = false;
        private bool isHeadingForced = false;
        private bool isAltitudeForced = false;
        private int altitude = 0;
        private double verticalSpeed = 0;
        private bool isSpeedHidden = false;
        private bool isHeadingHidden = false;
        private bool isAltitudeHidden = false;
        private bool isVerticalSpeedHidden = false;
        private bool isPowerOn = false;

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
        public bool IsSpeedDot { get => isSpeedForced; set => isSpeedForced = value; }
        /// <summary>
        /// Le cap est forcée par le pilote, le point ne s'affiche pas
        /// </summary>
        public bool IsHeadingDot { get => isHeadingForced; set => isHeadingForced = value; }
        /// <summary>
        /// L'altitude est forcée par le pilote, le point ne s'affiche pas
        /// </summary>
        public bool IsAltitudeDot { get => isAltitudeForced; set => isAltitudeForced = value; }
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
        public bool IsSpeedDash { get => isSpeedHidden; set => isSpeedHidden = value; }
        /// <summary>
        /// Le cap n'est pas affiché sur le FCU "---"
        /// </summary>
        public bool IsHeadingDash { get => isHeadingHidden; set => isHeadingHidden = value; }
        /// <summary>
        /// L'altitude n'est pas affichée sur le FCU "-----"
        /// </summary>
        public bool IsAltitudeDash { get => isAltitudeHidden; set => isAltitudeHidden = value; }
        /// <summary>
        /// La vitesse verticale n'est pas affichée sur le FCU "----"
        /// </summary>
        public bool IsVerticalSpeedHidden { get => isVerticalSpeedHidden; set => isVerticalSpeedHidden = value; }
        /// <summary>
        /// Le FCU est allimenté
        /// </summary>
        public bool IsPowerOn { get => isPowerOn; set => isPowerOn = value; }
    }
}
