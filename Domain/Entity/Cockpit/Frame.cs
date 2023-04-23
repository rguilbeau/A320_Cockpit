using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Entity.Cockpit
{
    /// <summary>
    /// Définition d'une frame
    /// </summary>
    public class Frame
    {
        private readonly int id;
        private readonly int size;
        private byte[] data;

        /// <summary>
        /// Création d'une nouvelle frame
        /// </summary>
        /// <param name="id">Son ID</param>
        /// <param name="size">La taille de la frame</param>
        public Frame(int id, int size)
        {
            this.id = id;
            this.size = size;
            data = new byte[size];
        }

        /// <summary>
        /// L'id de la frame
        /// </summary>
        public int Id
        {
            get { return id; }
        }

        /// <summary>
        /// La taille de la frame
        /// </summary>
        public int Size
        {
            get { return size; }
        }

        /// <summary>
        /// Les données de la frame
        /// </summary>
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// Retourne la frame sous sa forme hexadécimale
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string message = "0x" + Id.ToString("X3") + " (" + Size.ToString() + ")";

            for (int i = 0; i < Data.Length; i++)
            {
                message += "\t" + Data[i].ToString("X2");
            }

            return message;
        }

        /// <summary>
        /// Compare si l'object passé en paramètre et une Frame égale à celle-ci
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Récupère le HashCode de la frame
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Converti une array de boolean en une décimale
        /// </summary>
        /// <param name="bitArray">L'array de boolean à convertire</param>
        /// <returns></returns>
        public static byte BitArrayToByte(bool[] bitArray)
        {
            byte val = 0;
            foreach (bool b in bitArray)
            {
                val <<= 1;
                if (b) val |= 1;
            }
            return val;
        }
    }
}
