using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload.Glareshield;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield
{
    public class A32nxFcuRepository : A32nxPayloadRepository<Fcu>, IFcuRepository
    {

        private static readonly Fcu fcu = new();

        public A32nxFcuRepository(MsfsSimulatorRepository msfs) : base(msfs)
        {
        }

        protected override Fcu Payload => fcu;

        protected override void Refresh(CockpitEvent? e)
        {
            bool all = e == null;
            if (all || e == CockpitEvent.FCU_AP1)
            {
                msfs.Read(A32nxVariables.Autopilot1Active);
            }

            if(all || e == CockpitEvent.FCU_AP2)
            {
                msfs.Read(A32nxVariables.Autopilot2Active);
            }

            if (all || e == CockpitEvent.FCU_ATHR)
            {
                msfs.Read(A32nxVariables.AutoThrustStatus);
            }

            if(all || e == CockpitEvent.FCU_LOC)
            {
                msfs.Read(A32nxVariables.LocModeActive);
            }

            if(all || e == CockpitEvent.FCU_EXPED)
            {
                msfs.Read(A32nxVariables.ExpedModeActive);
            }

            if(all || e == CockpitEvent.FCU_APPR)
            {
                msfs.Read(A32nxVariables.ApprModeActive);
            }

            if (all)
            {
                msfs.Read(A32nxVariables.IsElectricityAc1BusPowered);
            }
        }

        protected override void UpdateEntity()
        {
            fcu.Ap1 = A32nxVariables.Autopilot1Active.Value;
            fcu.Ap2 = A32nxVariables.Autopilot2Active.Value;
            fcu.Athr = A32nxVariables.AutoThrustStatus.Value != 0;
            fcu.Loc = A32nxVariables.LocModeActive.Value;
            fcu.Exped = A32nxVariables.ExpedModeActive.Value;
            fcu.Appr = A32nxVariables.ApprModeActive.Value;
            fcu.IsPowerOn = A32nxVariables.IsElectricityAc1BusPowered.Value;
        }
    }
}
