using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Entity.Payload.Overhead
{
    /// <summary>
    /// Contenu de la frame des informations des lumières du cockpit
    /// </summary>
    public class LightIndicators : PayloadEntity
    {
        private const int ID = 0x003;
        private const int SIZE = 1;
        private bool testIndicatorsLight = false;

        /// <summary>
        /// Retourne l'ID de la frame
        /// </summary>
        public override int Id => ID;
        /// <summary>
        /// Retourne la taille de la frame
        /// </summary>
        public override int Size => SIZE;
        /// <summary>
        /// Le test des indicateurs est sur ON (affiche toutes les limières du cockpit pour checker l'état)
        /// </summary>
        public bool TestIndicatorsLight { get => testIndicatorsLight; set => testIndicatorsLight = value; }
    }
}
