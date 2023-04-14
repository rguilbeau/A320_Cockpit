﻿using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx
{
    public abstract class PayloadRepository<T> : IPayloadRepository where T: PayloadEntity
    {
        protected readonly MsfsSimulatorRepository msfsSimulatorRepository;

        protected abstract T Payload { get; }

        public PayloadRepository(MsfsSimulatorRepository msfsSimulatorRepository)
        {
            this.msfsSimulatorRepository = msfsSimulatorRepository;
        }

        protected abstract T BuildPayload();

        protected abstract bool Refresh(CockpitEvent e);

        public PayloadEntity Find(CockpitEvent e) 
        {
            if(Refresh(e))
            {
                return BuildPayload();
            } else
            {
                return Payload;
            }
            
        }

    }
}
