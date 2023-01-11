using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter
{
    internal class TypeConverter
    {
        public T? Convert<T>(double value)
        {
            object? convertValue;

            if (typeof(T).Equals(typeof(string)))
            {
                convertValue = value.ToString();
            }
            else if (typeof(T).Equals(typeof(bool)))
            {
                convertValue = value == 1;
            }
            else
            {
                convertValue = value;
            }

            return (T?)convertValue;
        }

    }
}
