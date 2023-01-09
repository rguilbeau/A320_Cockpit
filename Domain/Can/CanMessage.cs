using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Can
{
    internal class CanMessage
    {
        private int _id;

        private int _size;

        private byte[] _data;

        public CanMessage(int id, int size)
        {
            _id = id;
            _size = size;
            _data = new byte[size];
        }

        public int Id
        {
            get { return _id; }
        }

        public int Size
        {
            get { return _size; }
        }

        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public override string ToString()
        {
            String message = Id.ToString("X") + "\t" + Size.ToString();

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
    }
}
