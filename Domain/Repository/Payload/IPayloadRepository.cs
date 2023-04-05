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
    /// <typeparam name="T"></typeparam>
    public interface IPayloadRepository<T> where T : PayloadEntity
    {

        /// <summary>
        /// Met à jour et récupère l'entité
        /// </summary>
        /// <returns></returns>
        public T Find();

        /// <summary>
        /// Met à jour (uniquement les valeurs susceptible d'être modifiées par l'évènement passé en paramètre)
        /// et récupère l'entité
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public T FindByEvent(CockpitEvent e);
    }
}
