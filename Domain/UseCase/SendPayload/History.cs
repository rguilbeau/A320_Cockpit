using A320_Cockpit.Domain.Entity.Cockpit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.UseCase.SendPayload
{
    /// <summary>
    /// Classe facilitant l'historique des frames
    /// </summary>
    public class History
    {
        private readonly int timeout;
        private readonly Dictionary<int, Tuple<Frame, long>> history;

        /// <summary>
        /// Création d'un historique
        /// </summary>
        /// <param name="timeout"></param>
        public History(int timeout)
        {
            this.timeout = timeout;
            history = new();
        }

        /// <summary>
        /// Determine si la frame est expiré
        /// Peut être différente ou en timeout
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public bool IsExpired(Frame frame)
        {
            bool isExpired = true;

            if (history.ContainsKey(frame.Id))
            {
                Frame historyFrame = history[frame.Id].Item1;
                long timestamp = history[frame.Id].Item2;

                if (
                    timestamp + timeout >= DateTimeOffset.Now.ToUnixTimeMilliseconds() &&
                    frame.Equals(historyFrame)
                )
                {
                    isExpired = false;
                }
            }

            return isExpired;
        }

        /// <summary>
        /// Mise à jour de l'historique
        /// </summary>
        /// <param name="frame"></param>
        public void Update(Frame frame)
        {
            history[frame.Id] = new(frame, DateTimeOffset.Now.ToUnixTimeMilliseconds());
        }

        /// <summary>
        /// Rend une frame expiré
        /// </summary>
        /// <param name="frame"></param>
        public void Expire(Frame frame)
        {
            if(history.ContainsKey(frame.Id)) 
            {
                history.Remove(frame.Id);
            }
        }

    }
}
