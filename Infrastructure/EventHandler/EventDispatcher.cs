﻿using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Simulator;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.EventHandler
{
    /// <summary>
    /// Dispatch les évenement du cockpit 
    /// </summary>
    public class CockpitEventDispatcher
    {
        private readonly Dictionary<CockpitEvent, List<IPayloadEventHandler>> eventDictionary;
        private static CockpitEventDispatcher? instance;

        /// <summary>
        /// Récupération du singleton du dispatcher
        /// </summary>
        /// <returns></returns>
        public static CockpitEventDispatcher Get(List<IPayloadEventHandler> allEvents)
        {
            instance ??= new CockpitEventDispatcher(allEvents);
            return instance;
        }

        /// <summary>
        /// Création de l'event dispatcher
        /// </summary>
        private CockpitEventDispatcher(List<IPayloadEventHandler> allEvents)
        {
            eventDictionary = new();
            foreach(IPayloadEventHandler payloadEventHandler in allEvents) 
            { 
                foreach(CockpitEvent e in payloadEventHandler.EventSubscriber)
                {
                    if(!eventDictionary.ContainsKey(e))
                    {
                        eventDictionary[e] = new List<IPayloadEventHandler>();
                    }

                    eventDictionary[e].Add(payloadEventHandler);
                }
            }
        }

        /// <summary>
        /// Lance le FireEvent des classes associées à l'evenement
        /// </summary>
        /// <param name="e"></param>
        public void Dispatch(CockpitEvent e, double value)
        {
            foreach(IPayloadEventHandler eventClass in eventDictionary[e])
            {
                eventClass.Handle(e, value);
            }
        }
    }
}