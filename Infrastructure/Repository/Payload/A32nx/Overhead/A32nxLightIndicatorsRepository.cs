using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Entity.Payload.Overhead;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload.Overhead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Overhead
{
    public class A32nxLightIndicatorsRepository : A32nxPayloadRepository<LightIndicators>, ILightIndicatorsRepository
    {

        private static readonly LightIndicators lightIndicators = new();

        public A32nxLightIndicatorsRepository(IMsfs msfs) : base(msfs)
        {
        }

        protected override LightIndicators Payload => lightIndicators;

        protected override void Refresh(CockpitEvent? e)
        {
            bool all = e == null;

            if (all || e == CockpitEvent.OHP_TEST_LIGHT)
            {
                msfs.Read(A32nxVariables.LightIndicatorStatus);
            }

        }

        protected override void UpdateEntity()
        {
            lightIndicators.TestIndicatorsLight = A32nxVariables.LightIndicatorStatus.Value == 0;
        }
    }
}
