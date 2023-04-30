
using System;

namespace A320_Cockpit.Adaptation.Msfs.MsfsWasm
{
    /// <summary>
    /// Utilitaire pour la conversion des valeurs recu de MSFS vers les variables
    /// </summary>
    public class TypeConverter
    {
        /// <summary>
        /// Converti une variable double
        /// </summary>
        /// <typeparam name="T">Le type de retour</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T? Convert<T>(double value)
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
            else if (typeof(T).Equals(typeof(short)))
            {
                convertValue = (short)Math.Round(value, 0);
            }
            else if (typeof(T).Equals(typeof(int)))
            {
                convertValue = (int)Math.Round(value, 0);
            }
            else if (typeof(T).IsEnum)
            {
                convertValue = (T)Enum.ToObject(typeof(T), (int)value);
            }
            else
            {
                convertValue = value;
            }

            return (T?)convertValue;
        }

        public static string Convert<T>(T? value)
        {
            string convertValue;

            if (value == null)
            {
                convertValue = "0";
            } else if (value is string)
            {
                convertValue = value + "";
            }
            else if (value is bool)
            {
                bool boolValue = (bool)(object)value;
                convertValue = boolValue ? "1" : "0";
            }
            else if (value is short)
            {
                convertValue = value + "";
            }
            else if (typeof(T).Equals(typeof(int)))
            {
                convertValue = value + "";
            }
            else if (typeof(T).IsEnum)
            {
                convertValue = value + "";
            }
            else
            {
                convertValue = value + ""; ;
            }

            return convertValue;
        }
    }
}
