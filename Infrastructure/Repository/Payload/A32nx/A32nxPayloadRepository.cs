using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx
{
    /// <summary>
    /// Classe mère des repository de l'A32nx
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class A32nxPayloadRepository<T> : IPayloadRepository where T: PayloadEntity
    {
        protected readonly MsfsSimulatorRepository msfsSimulatorRepository;

        /// <summary>
        /// Retourne l'entité
        /// </summary>
        public abstract T Payload { get; }

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfsSimulatorRepository"></param>
        public A32nxPayloadRepository(MsfsSimulatorRepository msfsSimulatorRepository)
        {
            this.msfsSimulatorRepository = msfsSimulatorRepository;
        }

        /// <summary>
        /// Construction de l'entité
        /// </summary>
        /// <returns></returns>
        protected abstract T BuildPayload();

        /// <summary>
        /// Mise à jour de l'entité
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected abstract bool Refresh(CockpitEvent e);

        /// <summary>
        /// Met à jour et retourne l'entité
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public PayloadEntity Find(CockpitEvent e) 
        {
            if(Refresh(e))
            {
                return BuildPayload();
            } else
            {
                return Payload;
            }   
        }
    }
}
