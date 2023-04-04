using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload.Glareshield;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield
{
    public class A32nxFcuDisplayRepository : A32nxPayloadRepository<FcuDisplay>, IFcuDisplayRepository
    {
        private static readonly FcuDisplay fcuDisplay = new();

        public A32nxFcuDisplayRepository(IMsfs msfs) : base(msfs)
        {
        }

        protected override FcuDisplay Payload => fcuDisplay;

        protected override void Refresh(CockpitEvent? e)
        {
            bool all = e == null;
            msfs.StartTransaction();

            if (all || e == CockpitEvent.FCU_SPEED_MACH)
            {
                msfs.Read(A32nxVariables.IsManagedSpeedInMach);
                msfs.Read(A32nxVariables.SpeedSelected);
            }

            if (all || e == CockpitEvent.FCU_SPEED_BUG)
            {
                msfs.Read(A32nxVariables.IsSpeedManagedDash);
                msfs.Read(A32nxVariables.SpeedSelected);
            }

            if (all || e == CockpitEvent.FCU_SPEED_PULL || e == CockpitEvent.FCU_SPEED_PULL)
            {
                msfs.Read(A32nxVariables.IsSpeedDot);
                msfs.Read(A32nxVariables.IsSpeedManagedDash);
                msfs.Read(A32nxVariables.SpeedSelected);
            }

            if (all || e == CockpitEvent.FCU_HEADING_BUG)
            {
                msfs.Read(A32nxVariables.IsHeadingManageDash);
                msfs.Read(A32nxVariables.HeadingSelected);
            }

            if (all || e == CockpitEvent.FCU_HEADING_PUSH || e == CockpitEvent.FCU_HEADING_PULL)
            {
                msfs.Read(A32nxVariables.IsHeadingDot);
                msfs.Read(A32nxVariables.IsHeadingManageDash);
                msfs.Read(A32nxVariables.HeadingSelected);
            }

            if (all || e == CockpitEvent.FCU_VS_FPA)
            {
                msfs.Read(A32nxVariables.IsTrackFpa);
                if (A32nxVariables.IsTrackFpa.Value)
                {
                    msfs.Read(A32nxVariables.VerticalSpeedSelectedFpa);
                }
                else
                {
                    msfs.Read(A32nxVariables.VerticalSpeedSelectedFpm);
                }

                msfs.Read(A32nxVariables.HeadingSelected);
            }

            if (all || e == CockpitEvent.FCU_ALTITUDE_BUG)
            {
                msfs.Read(A32nxVariables.AltitudeSelected);
            }

            if (all || e == CockpitEvent.FCU_ALTITUDE_PUSH || e == CockpitEvent.FCU_ALTITUDE_PULL)
            {
                msfs.Read(A32nxVariables.AltitudeManaged);
            }

            if (all || e == CockpitEvent.FCU_VS_BUG)
            {
                if (A32nxVariables.IsTrackFpa.Value)
                {
                    msfs.Read(A32nxVariables.VerticalSpeedSelectedFpa);
                }
                else
                {
                    msfs.Read(A32nxVariables.VerticalSpeedSelectedFpm);
                }
            }

            if (all || e == CockpitEvent.FCU_VS_PUSH || e == CockpitEvent.FCU_VS_PULL)
            {
                msfs.Read(A32nxVariables.VerticalSpeedManaged);

                if (A32nxVariables.IsTrackFpa.Value)
                {
                    msfs.Read(A32nxVariables.VerticalSpeedSelectedFpa);
                }
                else
                {
                    msfs.Read(A32nxVariables.VerticalSpeedSelectedFpm);
                }
            }

            if (all)
            {
                msfs.Read(A32nxVariables.IsElectricityAc1BusPowered);
            }

            msfs.StopTransaction();
        }

        protected override void UpdateEntity()
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

            fcuDisplay.IsTrack = A32nxVariables.IsTrackFpa.Value;
            fcuDisplay.IsFpa = A32nxVariables.IsTrackFpa.Value;

            fcuDisplay.Altitude = A32nxVariables.AltitudeSelected.Value;
            fcuDisplay.IsAltitudeDot = A32nxVariables.IsHeadingDot.Value;
            fcuDisplay.IsAltitudeDash = false;

            if (A32nxVariables.IsTrackFpa.Value)
            {
                fcuDisplay.VerticalSpeed = A32nxVariables.VerticalSpeedSelectedFpa.Value;
            }
            else
            {
                fcuDisplay.VerticalSpeed = A32nxVariables.VerticalSpeedSelectedFpm.Value;
            }

            fcuDisplay.IsVerticalSpeedHidden = A32nxVariables.VerticalSpeedManaged.Value;
            fcuDisplay.IsPowerOn = A32nxVariables.IsElectricityAc1BusPowered.Value;
        }
    }
}
