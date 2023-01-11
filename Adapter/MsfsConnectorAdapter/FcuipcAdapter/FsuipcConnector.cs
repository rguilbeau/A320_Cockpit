using FSUIPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Adapter.MsfsConnectorAdapter.FcuipcAdapter
{
    internal class FsuipcConnector
    {

        private readonly TypeConverter typeConverter;

        private static FsuipcConnector? instance;

        public static FsuipcConnector Get()
        {
            if(instance == null)
            {
                instance = new FsuipcConnector(new TypeConverter());
            }
            return instance;
        }


        private FsuipcConnector(TypeConverter typeConverter)
        {
            this.typeConverter = typeConverter;
        }

        public bool IsOpen
        {
            get { return FSUIPCConnection.IsOpen; }
        }

        public void Open()
        {
            FSUIPCConnection.Open();

            if (!FSUIPCConnection.IsOpen)
            {
                throw new Exception("Unable to connect to FSUIPC");
            }
        }

        public void Close()
        {
            FSUIPCConnection.Close();
        }

        public T? ReadLvar<T>(Lvar<T> lvar)
        {
            T? value = default;

            if (FSUIPCConnection.IsOpen)
            {
                double lvarValue = FSUIPCConnection.ReadLVar(lvar.Name);
                value = typeConverter.Convert<T>(lvarValue);
            }

            return value;
        }
    }
}
