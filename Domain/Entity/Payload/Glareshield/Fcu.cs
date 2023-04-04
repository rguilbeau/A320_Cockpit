using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Entity.Payload.Glareshield
{
    public class Fcu : PayloadEntity
    {

        private const int ID = 0x004;
        private const int SIZE = 1;
        private bool ap1 = false;
        private bool ap2 = false;
        private bool athr = false;
        private bool loc = false;
        private bool appr = false;
        private bool exped = false;
        private bool isPowerOn = false;

        public override int Id => ID;

        public override int Size => SIZE;

        public bool Ap1 { get => ap1; set => ap1 = value; }
        public bool Ap2 { get => ap2; set => ap2 = value; }
        public bool Athr { get => athr; set => athr = value; }
        public bool Loc { get => loc; set => loc = value; }
        public bool Appr { get => appr; set => appr = value; }
        public bool Exped { get => exped; set => exped = value; }
        public bool IsPowerOn { get => isPowerOn; set => isPowerOn = value; }
    }
}
