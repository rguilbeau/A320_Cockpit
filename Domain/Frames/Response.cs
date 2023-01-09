using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Domain.Frames
{
    internal class Response
    {

        private bool _success;

        private int _idMessage;

        public Response(bool success, int idMessage) 
        {
            _success = success;
            _idMessage = idMessage;
        }

        public bool IsSuccess { get { return _success; } }

        public int IdMessage { get { return _idMessage; } }

    }
}
