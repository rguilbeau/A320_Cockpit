using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Payload
{
    internal class FcuDisplayPayload
    {
        private double? speed;
        private short? heading;
        private bool? isMach;
        private bool? isTrack;
        private bool? isLat;
        private bool? isFpa;
        private bool? isSpeedForced;
        private bool? isHeadingForced;
        private bool? isAltitudeForced;
        private bool? isVerticalSpeedPositive;
        private int? altitude;
        private double? verticalSpeed;
        private bool? isSpeedHidden;
        private bool? isHeadingHidden;
        private bool? isAltitudeHidden;
        private bool? isVerticalSpeedHidden;

        public double Speed
        {
            get => speed ?? 0; set => speed = value;
        }

        public short Heading
        {
            get => heading ?? 0; set => heading = value;
        }

        public bool IsMach
        {
            get => isMach ?? false; set => isMach = value;
        }

        public bool IsTrack
        {
            get => isTrack ?? false; set => isTrack = value;
        }

        public bool IsLat
        {
            get => isLat ?? false; set => isLat = value;
        }

        public bool IsFpa
        {
            get => isFpa ?? false; set => isFpa = value;
        }

        public bool IsSpeedForced
        {
            get => isSpeedForced ?? false; set => isSpeedForced = value;
        }

        public bool IsHeadingForced
        {
            get => isHeadingForced ?? false; set => isHeadingForced = value;
        }

        public bool IsAltitudeForced
        {
            get => isAltitudeForced ?? false; set => isAltitudeForced = value;
        }

        public bool IsVerticalSpeedPositive
        {
            get => isVerticalSpeedPositive ?? false; set => isVerticalSpeedPositive = value;
        }

        public int Altitude
        {
            get => altitude ?? 0; set => altitude = value;
        }

        public double VerticalSpeed
        {
            get => verticalSpeed ?? 0; set => verticalSpeed = value;
        }

        public bool IsSpeedHidden
        {
            get => isSpeedHidden ?? false; set => isSpeedHidden = value;
        }

        public bool IsHeadingHidden
        {
            get => isHeadingHidden ?? false; set => isHeadingHidden = value;
        }

        public bool IsAltitudeHidden
        {
            get => isAltitudeHidden ?? false; set => isAltitudeHidden = value;
        }

        public bool IsVerticalSpeedHidden
        {
            get => isVerticalSpeedHidden ?? false; set => isVerticalSpeedHidden = value;
        }
    }
}
