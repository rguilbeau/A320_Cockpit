﻿using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Entity.Payload.Glareshield;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Payload;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Payload.FakeA320.Glareshield
{
    /// <summary>
    /// Repository pour la mise à jour et la récupération de l'entité du des témoins des panels du Glareshield
    /// </summary>
    public class FakeA320GlareshieldIndicatorsRepository : FakeA320PayloadRepository<GlareshieldIndicators>
    {
        private static readonly GlareshieldIndicators glareshieldIndicators = new();

        /// <summary>
        /// Retourne l'entité
        /// </summary>
        public override GlareshieldIndicators Payload => glareshieldIndicators;

        /// <summary>
        /// Création du repository
        /// </summary>
        public FakeA320GlareshieldIndicatorsRepository() : base()
        {
            glareshieldIndicators.FcuAp1 = false;
            glareshieldIndicators.FcuAp2 = false;
            glareshieldIndicators.FcuAthr = false;
            glareshieldIndicators.FcuLoc = false;
            glareshieldIndicators.FcuExped = false;
            glareshieldIndicators.FcuAppr = false;
            glareshieldIndicators.FcuIsPowerOn = true;
        }

        /// <summary>
        /// Mise à jour de l'entité
        /// </summary>
        protected override bool Refresh(CockpitEvent e)
        {
            return AskRefresh;
        }

        /// <summary>
        /// Mise à jour de l'entité avec les variables MSFS
        /// </summary>
        protected override GlareshieldIndicators BuildPayload()
        {
            return glareshieldIndicators;
        }
    }
}