using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload.Brightness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Brightness
{
    public class A32nxBrightnessRepository : A32nxPayloadRepository<BrightnessCockpit>, IBrightnessRepository
    {
        private static readonly BrightnessCockpit brightness = new ();

        public A32nxBrightnessRepository(IMsfs msfs) : base(msfs)
        {
        }

        protected override BrightnessCockpit Payload => brightness;

        protected override void Refresh(CockpitEvent? e)
        {
            
        }

        protected override void UpdateEntity()
        {
            brightness.Fcu = 100;
        }
    }
}
