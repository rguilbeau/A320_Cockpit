using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.VariableUpdater
{
    internal interface IFcuDisplayVariableUpdater
    {
        public enum Updates
        {
            SPEED,
            SPEED_MANAGED,
            SPEED_DOTTED,
            MACH_SPEED
        };


        public void Update();

        public void Update(Updates update);

        public void Update(params Updates[] updates);

    }
}
