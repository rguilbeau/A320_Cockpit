using A320_Cockpit.Domain.Entity.Cockpit;
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
        /// La frame
        /// </summary>
        public abstract Frame Frame { get; }

        /// <summary>
        /// L'id de la frame
        /// </summary>
        public abstract int Id { get; }
        /// <summary>
        /// La taille de la frame
        /// </summary>
        public abstract int Size { get; }
    }
}
