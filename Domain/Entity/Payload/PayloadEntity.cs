using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Entity.Payload
{
    /// <summary>
    /// Base des Frames
    /// </summary>
    public abstract class PayloadEntity
    {
        /// <summary>
        /// Retourne l'ID de la frame
        /// </summary>
        public abstract int Id { get; }
        /// <summary>
        /// Retourne la taille de la frame
        /// </summary>
        public abstract int Size { get; }
    }
}
