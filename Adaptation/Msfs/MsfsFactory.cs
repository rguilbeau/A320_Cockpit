using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Adaptation.Msfs.MsfsWasm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adaptation.Msfs
{
    /// <summary>
    /// Factory pour la création de la connexion au simulateur de vol
    /// </summary>
    public class MsfsFactory
    {
        private static IMsfs? msfs;

        /// <summary>
        /// Singleton de récupération de la connexion au simulateur de vol
        /// </summary>
        /// <returns></returns>
        public static IMsfs Get()
        {
            if (msfs == null)
            {
                msfs = new MsfsWasmAdapter(new TypeConverter());
            }
            return msfs;
        }

    }
}
