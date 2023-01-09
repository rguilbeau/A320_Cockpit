using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Can
{
    public class MessageRecievedEventArgs : EventArgs
    {
        /// <summary>
        /// Le message CAN reçu
        /// </summary>
        internal CanMessage Message { get; }

        /// <summary>
        /// Contruction des arguments de l'event d'un message CAN
        /// </summary>
        /// <param name="message">Le message can reçu</param>
        internal MessageRecievedEventArgs(CanMessage message)
        {
            Message = message;
        }
    }

    internal interface ICan
    {
        public event EventHandler<MessageRecievedEventArgs>? MessageReceivedEvent;

        public bool Open();

        public void Close();

        public bool Send(CanMessage message);

        public bool IsOpen { get; }

    }
}
