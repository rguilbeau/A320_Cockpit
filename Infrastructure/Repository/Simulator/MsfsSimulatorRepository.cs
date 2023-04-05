using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Adaptation.Msfs.Model;
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
    public class MsfsSimulatorRepository : ISimulatorRepository
    {
        private readonly IMsfs msfs;

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfs"></param>
        public MsfsSimulatorRepository(IMsfs msfs)
        {
            this.msfs = msfs;
        }

        /// <summary>
        /// Reoutne si la connexion est ouverte
        /// </summary>
        public bool IsOpen => msfs.IsOpen;

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
        }

        /// <summary>
        /// Lecture d'une variable SimVar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        public void Read<T>(SimVar<T> variable)
        {
            msfs.Read(variable);
        }

        /// <summary>
        /// Démarre une transaction (une transaction ne lit qu'une seule fois la même variable)
        /// </summary>
        public void StartTransaction()
        {
            msfs.StartTransaction();
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
