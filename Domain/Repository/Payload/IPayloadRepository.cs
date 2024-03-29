﻿using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Enum;

namespace A320_Cockpit.Domain.Repository.Payload
{
    /// <summary>
    /// Interface mère de tous les répositories des frames
    /// </summary>
    public interface IPayloadRepository
    {
        /// <summary>
        /// Récupère l'entité
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public PayloadEntity Find(CockpitEvent e);
    }
}
