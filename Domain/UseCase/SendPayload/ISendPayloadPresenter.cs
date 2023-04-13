using A320_Cockpit.Domain.Entity.Cockpit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.SendPayload
{
    public interface ISendPayloadPresenter
    {
        /// <summary>
        /// La frame traité
        /// </summary>
        public Frame? Frame { get; set; }

        /// <summary>
        /// L'erreur potentiel
        /// </summary>
        public Exception? Error { get; set; }

        /// <summary>
        /// Si la frame a été envoyé
        /// </summary>
        public bool IsSent { get; set; }

        /// <summary>
        /// Présente les valeurs
        /// </summary>
        public void Present();
    }
}
