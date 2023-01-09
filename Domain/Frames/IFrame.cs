using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Frames
{
    internal interface IFrame<TValue>
    {
        public Response Send(TValue payload);

    }
}
