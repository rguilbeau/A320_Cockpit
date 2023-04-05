using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.MainThread
{
    /// <summary>
    /// Interface représentant le thead principale pour la communication avec le cockpit
    /// </summary>
    public interface IMainThread
    {
        /// <summary>
        /// Démarre le thread
        /// </summary>
        public void Start();

        /// <summary>
        /// Arrête le thread
        /// </summary>
        public void Stop();

    }
}
