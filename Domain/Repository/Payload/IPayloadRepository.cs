using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Repository.Payload
{
    /// <summary>
    /// Interface mère de tous les répositories des frames
    /// </summary>
    public interface IPayloadRepository
    {
        /// <summary>
        /// Met à jour et récupère l'entité
        /// </summary>
        /// <returns></returns>
        public PayloadEntity Find();
    }
}
