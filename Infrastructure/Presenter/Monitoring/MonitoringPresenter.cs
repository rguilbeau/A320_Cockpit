using A320_Cockpit.Infrastructure.Presenter.ListenEvent;
using A320_Cockpit.Infrastructure.View.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Presenter.Monitoring
{
    public class MonitoringPresenter
    {
        private readonly MonitringForm monitringForm;
        private long ?timing;
        private Exception ?exception;

        /// <summary>
        /// Création du presenter
        /// </summary>
        /// <param name="monitringForm"></param>
        public MonitoringPresenter(MonitringForm monitringForm)
        {
             this.monitringForm = monitringForm; 
        }

        /// <summary>
        /// Présente les élement au monitoring
        /// </summary>
        public void Present()
        {
            if(timing != null)
            {
                monitringForm.UpdateProgressTiming((long)timing);
            }

            if(exception != null)
            {
                monitringForm.AddError(exception);
            }
        }

        /// <summary>
        /// Le timing de lecture des LVar et SimVar d'une loop
        /// </summary>
        public long Timing { set => timing = value; }

        /// <summary>
        /// Les erreurs
        /// </summary>
        public Exception Exception { set => exception = value; }
        
    }
}
