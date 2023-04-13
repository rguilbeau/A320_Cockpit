﻿using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Entity.Payload.Brightness;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Brightness
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du rétroaiclairage
    /// </summary>
    public class A32nxBrightnessRepository : IPayloadRepository
    {
        private static readonly BrightnessCockpit brightness = new();
        
        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        public PayloadEntity Find()
        {
            brightness.FcuDisplay = 100;
            return brightness;
        }
    }
}
