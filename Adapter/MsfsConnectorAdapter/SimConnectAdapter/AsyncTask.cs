using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter
{
    class AsyncTask<T> : TaskCompletionSource<T>
    {
        private readonly Type typeVar;

        private readonly int idRequest;

        public AsyncTask(int idRequest, Type typeVar) : base() {
            this.typeVar = typeVar;
            this.idRequest = idRequest;
        }

        public Type TypeVar
        {
            get { return typeVar; }
        }

        public int IdRequest
        {
            get { return idRequest; }
        }

    }
}
