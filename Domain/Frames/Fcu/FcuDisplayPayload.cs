using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Frames.Fcu
{
    internal class FcuDisplayPayload
    {
        public float Speed
        {
            get { return 0; }
        }

        public short Heading
        {
            get { return 0; }
        }

        public bool IsMach
        {
            get { return false; }
        }

        public bool IsTrack
        {
            get { return false; }
        }

        public bool IsLat
        {
            get { return false; }
        }

        public bool IsFpa
        {
            get { return false; }
        }

        public bool IsSpeedForced
        {
            get { return false; }
        }

        public bool IsHeadingForced
        {
            get { return false; }
        }

        public bool IsAltitudeForced
        {
            get { return false; }
        }

        public bool IsVerticalSpeedPositive
        {
            get { return false; }
        }

        public int Altitude
        {
            get { return 0; }
        }

        public float VerticalSpeed
        {
            get { return 0; }
        }

        public bool IsSpeedHidden
        {
            get { return false; }
        }

        public bool IsHeadingHidden
        {
            get { return false; }
        }

        public bool IsAltitudeHidden
        {
            get { return false; }
        }

        public bool IsVerticalSpeedHidden
        {
            get { return false; }
        }
    }
}
