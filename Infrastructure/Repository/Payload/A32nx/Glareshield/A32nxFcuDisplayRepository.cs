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
    public class A32nxFcuDisplayRepository : IPayloadRepository
    {
        private static readonly FcuDisplay fcuDisplay = new();

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        public PayloadEntity Find()
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
