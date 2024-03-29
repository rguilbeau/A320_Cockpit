﻿using A320_Cockpit.Adaptation.Msfs.Model.Event;
using A320_Cockpit.Adaptation.Msfs.Model.Variable;

namespace A320_Cockpit.Adaptation.Msfs
{
    /// <summary>
    /// Interface des adapteurs de connexion au simulateur de vol
    /// </summary>
    public interface IMsfs
    {
        /// <summary>
        /// Ferme la connexion
        /// </summary>
        public void Close();

        /// <summary>
        /// Lit une variable locale du simulateur de vol
        /// </summary>
        /// <typeparam name="T">Le type de la variable</typeparam>
        /// <param name="variable">La variable à lire</param>
        public void Read<T>(Lvar<T> variable);

        /// <summary>
        /// Modifie la valeur d'une LVAR
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        public void Write<T>(Lvar<T> variable);

        /// <summary>
        /// Lit une variable "native" du simulateur de vol
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        public void Read<T>(SimVar<T> variable);

        /// <summary>
        /// Envoi d'un event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="kEvent"></param>
        public void Send<T>(KEvent<T> kEvent);

        /// <summary>
        /// Envoi d'un HTML event
        /// </summary>
        /// <param name="hEvent"></param>
        public void Send(HEvent hEvent);

        /// <summary>
        /// Démarre une transaction (une transaction ne lit qu'une seule fois la même variable)
        /// </summary>
        public void StartTransaction();

        /// <summary>
        /// Arrête une transaction (une transaction ne lit qu'une seule fois la même variable)
        /// </summary>
        public void StopTransaction();

        /// <summary>
        /// Force l'arrêt de la lecture des variables
        /// </summary>
        public void StopRead();

        /// <summary>
        /// Reprend la lecture des variables
        /// </summary>
        public void ResumeRead();

        /// <summary>
        /// Retourne si la connexion est ouverte
        /// </summary>
        public bool IsOpen { get; }

        /// <summary>
        /// Ouvre une connexion
        /// </summary>
        public void Open();

    }
}
