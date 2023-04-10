using A320_Cockpit.Adaptation.Canbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static A320_Cockpit.Adaptation.Msfs.MsfsWasm.MsfsWasmAdapter;

namespace A320_Cockpit.Infrastructure.Runner
{
    public class CockpitEventRunner : IRunner
    {
        /// <summary>
        /// Mise en écoute des messages du cockpit
        /// </summary>
        public void Start()
        {
            CanBusFactory.Get().MessageReceived += CockpitThread_MessageReceived;
        }

        /// <summary>
        /// Récéption des message du cockpit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void CockpitThread_MessageReceived(object? sender, Domain.Entity.Cockpit.Frame frame)
        {
           // if(frame.Id == 0xFFF) 
            //{
                int eventId = (frame.Data[0] << 8) | (frame.Data[1]);

                int i = 0;
            //}
        }

        /// <summary>
        /// Arrêt de l'écoute des messages du cockpit
        /// </summary>
        public void Stop()
        {
            CanBusFactory.Get().MessageReceived -= CockpitThread_MessageReceived;
        }
    }
}
