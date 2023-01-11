using A320_Cockpit.Adapter.CanBusAdapter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.CanBus
{
    internal class Frame
    {
        private readonly int id;

        private readonly int size;

        private byte[] data;

        public Frame(int id, int size)
        {
            this.id = id;
            this.size = size;
            data = new byte[size];
        }

        public int Id
        {
            get { return id; }
        }

        public int Size
        {
            get { return size; }
        }

        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        public override string ToString()
        {
            string message = Id.ToString("X") + "\t" + Size.ToString();

            for (int i = 0; i < Data.Length; i++)
            {
                message += "\t" + Data[i].ToString("X");
            }

            return message;
        }

        public static byte BitArrayToByte(BitArray bitArray)
        {
            byte[] value = { 0 };
            bitArray.CopyTo(value, 0);
            return value[0];
        }

        public override bool Equals(object? obj)
        {
            bool equals = false;

            if (obj != null && obj.GetType().Equals(GetType()))
            {
                Frame compare = (Frame)obj;
                if (Id == compare.Id && Size == compare.Size)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        if (Data[i] != compare.Data[i])
                        {
                            return false;
                        }
                        equals = true;
                    }
                }
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
