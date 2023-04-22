using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Adaptation.Msfs.Model.Event;
using A320_Cockpit.Adaptation.Msfs.Model.Variable;
using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Simulator
{
    /// <summary>
    /// Repository du Simulateur MSFS2020
    /// </summary>
    public class MsfsSimulatorRepository : ISimulatorConnexionRepository
    {
        private readonly IMsfs msfs;
        private bool hasReadVariable;

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfs"></param>
        public MsfsSimulatorRepository(IMsfs msfs)
        {
            this.msfs = msfs;
            hasReadVariable = false;
        }

        /// <summary>
        /// Détect si une variable a été lu
        /// </summary>
        public bool HasReadVariable => hasReadVariable;

        /// <summary>
        /// Démarre la surveillance de lecture de variable
        /// </summary>
        public void StartWatchRead()
        {
            hasReadVariable = false;
        }

        /// <summary>
        /// Reoutne si la connexion est ouverte
        /// </summary>
        public bool IsOpen => true;

        /// <summary>
        /// Ouvre la connexion
        /// </summary>
        public void Open()
        {
            msfs.Open();
        }

        /// <summary>
        /// Ferme la connexion
        /// </summary>
        public void Close()
        {
            msfs.Close();
        }

        /// <summary>
        /// Lecture d'une variable Lvar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        public void Read<T>(Lvar<T> variable)
        {
            msfs.Read(variable);
            hasReadVariable = true;
        }

        /// <summary>
        /// Lecture d'une variable SimVar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        public void Read<T>(SimVar<T> variable)
        {
            msfs.Read(variable);
            hasReadVariable = true;
        }

        /// <summary>
        /// Envoi d'un KEvent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="kEvent"></param>
        public void Send<T>(KEvent<T> kEvent)
        {
            msfs.Send(kEvent);
        }

        /// <summary>
        /// Envoi d'un event HTML
        /// </summary>
        /// <param name="hEvent"></param>
        public void Send(HEvent hEvent)
        {
            msfs.Send(hEvent);
        }

        /// <summary>
        /// Démarre une transaction (une transaction ne lit qu'une seule fois la même variable)
        /// </summary>
        public void StartTransaction()
        {
            msfs.StartTransaction();
        }

        /// <summary>
        /// Force l'arrêt de la lecture des variables
        /// </summary>
        public void StopRead()
        {
            msfs.StopRead();
        }

        /// <summary>
        /// Reprend la lecture des variables
        /// </summary>
        public void ResumeRead()
        {
           msfs.ResumeRead();
        }

        /// <summary>
        /// Arrête une transaction (une transaction ne lit qu'une seule fois la même variable)
        /// </summary>
        public void StopTransaction()
        {
           msfs.StopTransaction();
        }
    }
}
