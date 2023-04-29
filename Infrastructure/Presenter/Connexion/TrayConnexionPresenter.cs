﻿using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Domain.UseCase.Connexion;
using A320_Cockpit.Infrastructure.View.SystemTray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Presenter.Connexion
{
    /// <summary>
    /// Présenteur dédié à la connexion pour le system tray
    /// </summary>
    public class TrayConnexionPresenter : IConnexionPresenter
    {
        private readonly ApplicationTray applicationTray;
        private readonly List<Exception> exceptions;
        private bool cockpitStatus;
        private bool simulatorStatus;

        /// <summary>
        /// Création du présenter
        /// </summary>
        /// <param name="applicationTray"></param>
        public TrayConnexionPresenter(ApplicationTray applicationTray)
        {
            exceptions = new List<Exception>();
            this.applicationTray = applicationTray;
        }

        /// <summary>
        /// La liste des erreurs de connexion
        /// </summary>
        public List<Exception> Exceptions
        {
            get { return exceptions; }
        }

        /// <summary>
        /// Ajoute une nouvelle erreur de connexion
        /// </summary>
        /// <param name="exception"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddException(Exception exception)
        {
            exceptions.Add(exception);
        }

        /// <summary>
        /// Présente les élements au system tray
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Present()
        {
            if(!cockpitStatus || !simulatorStatus)
            {
                applicationTray.ChangeStatus(TrayStatus.FAILURE);
            }
        }

        /// <summary>
        /// Le status du simulateur
        /// </summary>
        public bool SimulatorStatus { get => simulatorStatus; set => simulatorStatus = value; }
        /// <summary>
        /// Le status du cockpit (CAN bus)
        /// </summary>
        public bool CockpitStatus { get => cockpitStatus; set => cockpitStatus = value; }
    }
}
