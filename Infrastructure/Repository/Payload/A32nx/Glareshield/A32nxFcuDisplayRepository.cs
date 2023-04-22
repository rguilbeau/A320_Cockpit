using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Adaptation.Msfs.Model;
using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du des valeurs 
    /// à afficher sur les écrans du FCU
    /// </summary>
    public class A32nxFcuDisplayRepository : A32nxPayloadRepository<FcuDisplay>
    {
        private static readonly FcuDisplay fcuDisplay = new();

        protected override FcuDisplay Payload => fcuDisplay;

        /// <summary>
        /// Création du repository
        /// </summary>
        /// <param name="msfsSimulatorRepository"></param>
        public A32nxFcuDisplayRepository(MsfsSimulatorRepository msfsSimulatorRepository) : base(msfsSimulatorRepository)
        {
        }

        /// <summary>
        /// Met à jour l'entité
        /// </summary>
        protected override bool Refresh(CockpitEvent e)
        {
            msfsSimulatorRepository.StartWatchRead();

            switch(e)
            {
                case CockpitEvent.ALL:
                    msfsSimulatorRepository.Read(A32nxVariables.IsManagedSpeedInMach);
                    msfsSimulatorRepository.Read(A32nxVariables.SpeedSelected);
                    msfsSimulatorRepository.Read(A32nxVariables.IsSpeedDot);
                    msfsSimulatorRepository.Read(A32nxVariables.IsSpeedManagedDash);
                    msfsSimulatorRepository.Read(A32nxVariables.IsHeadingDot);
                    msfsSimulatorRepository.Read(A32nxVariables.IsHeadingManageDash);
                    msfsSimulatorRepository.Read(A32nxVariables.HeadingSelected);
                    msfsSimulatorRepository.Read(A32nxVariables.IsTrackFpa);
                    msfsSimulatorRepository.Read(A32nxVariables.AltitudeSelected);
                    msfsSimulatorRepository.Read(A32nxVariables.AltitudeManaged);
                    msfsSimulatorRepository.Read(A32nxVariables.VerticalSpeedSelectedFpa);
                    msfsSimulatorRepository.Read(A32nxVariables.VerticalSpeedSelectedFpm);
                    msfsSimulatorRepository.Read(A32nxVariables.VerticalSpeedManaged);
                    msfsSimulatorRepository.Read(A32nxVariables.IsElectricityAc1BusPowered);
                    break;
                case CockpitEvent.FCU_SPEED_BUG:
                    msfsSimulatorRepository.Read(A32nxVariables.SpeedSelected);
                    msfsSimulatorRepository.Read(A32nxVariables.IsSpeedManagedDash);
                    break;
            }

            return msfsSimulatorRepository.HasReadVariable;
        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override FcuDisplay BuildPayload()
        {
            if (A32nxVariables.SpeedSelected.Value <= 0)
            {
                fcuDisplay.IsMach = A32nxVariables.IsManagedSpeedInMach.Value;
            }
            else
            {
                fcuDisplay.IsMach = A32nxVariables.SpeedSelected.Value > 0 && A32nxVariables.SpeedSelected.Value < 1;
            }

            fcuDisplay.IsSpeedDot = A32nxVariables.IsSpeedDot.Value;
            fcuDisplay.IsSpeedDash = A32nxVariables.IsSpeedManagedDash.Value;
            fcuDisplay.Speed = Math.Round(A32nxVariables.SpeedSelected.Value, 2);

            fcuDisplay.IsHeadingDot = A32nxVariables.IsHeadingDot.Value;
            fcuDisplay.IsHeadingDash = A32nxVariables.IsHeadingManageDash.Value;
            fcuDisplay.Heading = A32nxVariables.HeadingSelected.Value;
            fcuDisplay.IsLat = true;

            fcuDisplay.IsTrack = A32nxVariables.IsTrackFpa.Value;
            fcuDisplay.IsFpa = A32nxVariables.IsTrackFpa.Value;

            fcuDisplay.Altitude = A32nxVariables.AltitudeSelected.Value;
            fcuDisplay.IsAltitudeDot = A32nxVariables.AltitudeManaged.Value;
            fcuDisplay.IsAltitudeDash = false;

            if (A32nxVariables.IsTrackFpa.Value)
            {
                fcuDisplay.VerticalSpeed = A32nxVariables.VerticalSpeedSelectedFpa.Value;
            }
            else
            {
                fcuDisplay.VerticalSpeed = A32nxVariables.VerticalSpeedSelectedFpm.Value;
            }

            fcuDisplay.IsVerticalSpeedDash = A32nxVariables.VerticalSpeedManaged.Value;
            fcuDisplay.IsPowerOn = A32nxVariables.IsElectricityAc1BusPowered.Value;
            return fcuDisplay;
        }
    }
}
