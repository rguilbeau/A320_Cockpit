using A320_Cockpit.Domain.UseCase.ListenEvent;
using A320_Cockpit.Domain.UseCase.SendPayload;
using A320_Cockpit.Infrastructure.Presenter.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Runner
{
    /// <summary>
    /// Interface représentant le thead principale pour la communication avec le cockpit
    /// </summary>
    public interface IRunner
    {
        /// <summary>
        /// Démarre le thread
        /// </summary>
        public void Start();

        /// <summary>
        /// Arrête le thread
        /// </summary>
        public void Stop();

        /// <summary>
        /// Ajout des présenter d'envoi de frame
        /// </summary>
        /// <param name="sendPayloadPresenter"></param>
        public void AddSendPayloadPresenter(ISendPayloadPresenter sendPayloadPresenter);

        /// <summary>
        /// Ajoute des présenter d'écoute de frame
        /// </summary>
        /// <param name="listenEventPresenter"></param>
        public void AddListenEventPresenter(IListenEventPresenter listenEventPresenter);

        /// <summary>
        /// Ajoute le presenter du monitoring
        /// </summary>
        /// <param name="monitoringPresenter"></param>
        public void AddMonitoringPresenter(MonitoringPresenter monitoringPresenter);

        /// <summary>
        /// Supprime des présenter d'envoi de frame
        /// </summary>
        /// <param name="sendPayloadPresenter"></param>
        public void RemoveSendPayloadPresenter(ISendPayloadPresenter sendPayloadPresenter);

        /// <summary>
        /// Supprime des présenter d'écoute de frame
        /// </summary>
        /// <param name="listenEventPresenter"></param>
        public void RemoveListenEventPresenter(IListenEventPresenter listenEventPresenter);

        /// <summary>
        /// Supprime le presenter du monitoring
        /// </summary>
        /// <param name="monitoringPresenter"></param>
        public void RemoveMonitoringPresenter(MonitoringPresenter monitoringPresenter);

    }
}
