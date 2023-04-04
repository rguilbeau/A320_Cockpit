using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Entity.Payload.Brightness
{
    public class BrightnessCockpit : PayloadEntity
    {

        private const int ID = 0x002;
        private const int SIZE = 1;
        private byte fcu = 100;

        public override int Id => ID;

        public override int Size => SIZE;

        public byte Fcu { get => fcu; set => fcu = value; }
    }
}
