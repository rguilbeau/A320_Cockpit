using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx
{
    /// <summary>
    /// Classe mère des repositories des Entité (contenu des frames) du cockpit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class A32nxPayloadRepository<T>
    {
        protected MsfsSimulatorRepository msfs;

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfs"></param>
        public A32nxPayloadRepository(MsfsSimulatorRepository msfs)
        {
            this.msfs = msfs;
        }

        /// <summary>
        /// Met à jour et récupère l'entité
        /// </summary>
        /// <returns></returns>
        public T Find()
        {
            Refresh(null);
            UpdateEntity();
            return Payload;
        }

        /// <summary>
        /// Met à jour (uniquement les valeurs susceptible d'être modifiées par l'évènement passé en paramètre)
        /// et récupère l'entité
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public T FindByEvent(CockpitEvent e)
        {
            Refresh(e);
            UpdateEntity();
            return Payload;
        }

        /// <summary>
        /// Retourne l'entité
        /// </summary>
        protected abstract T Payload { get; }

        /// <summary>
        /// Met à jour les valeurs des variables MSFS (LVar, SimVar...)
        /// Si en event est passé, on ne met à jour que les varibales susceptibles d'avoir été modifiées
        /// </summary>
        /// <param name="e"></param>
        protected abstract void Refresh(CockpitEvent? e);

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected abstract void UpdateEntity();

    }
}
