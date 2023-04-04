using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Entity.Payload.Glareshield
{
    public class GlareshieldIndicators : PayloadEntity
    {

        private const int ID = 0x004;
        private const int SIZE = 1;
        private bool fcuAp1 = false;
        private bool fcuAp2 = false;
        private bool fcuAthr = false;
        private bool fcuLoc = false;
        private bool fcuAppr = false;
        private bool fcuExped = false;
        private bool fcuIsPowerOn = false;


        public override int Id => ID;

        public override int Size => SIZE;

        public bool FcuAp1 { get => fcuAp1; set => fcuAp1 = value; }
        public bool FcuAp2 { get => fcuAp2; set => fcuAp2 = value; }
        public bool FcuAthr { get => fcuAthr; set => fcuAthr = value; }
        public bool FcuLoc { get => fcuLoc; set => fcuLoc = value; }
        public bool FcuAppr { get => fcuAppr; set => fcuAppr = value; }
        public bool FcuExped { get => fcuExped; set => fcuExped = value; }
        public bool FcuIsPowerOn { get => fcuIsPowerOn; set => fcuIsPowerOn = value; }
    }
}
