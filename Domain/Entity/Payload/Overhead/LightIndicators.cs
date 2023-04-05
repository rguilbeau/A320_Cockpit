using A320_Cockpit.Domain.Enum;
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
        private const int SIZE = 1;
        private bool testIndicatorsLight = false;

        /// <summary>
        /// L'id de la frame
        /// </summary>
        public override int Id => (int)FrameId.LIGHT_INDICATOR;
        /// <summary>
        /// La taille de la frame
        /// </summary>
        public override int Size => SIZE;
        /// <summary>
        /// Le test des indicateurs est sur ON (affiche toutes les limières du cockpit pour checker l'état)
        /// </summary>
        public bool TestIndicatorsLight { get => testIndicatorsLight; set => testIndicatorsLight = value; }
    }
}
