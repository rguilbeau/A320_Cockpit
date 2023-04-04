using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Adaptation.Msfs.Model;
using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Repository.Simulator
{
    public class MsfsSimulatorRepository : ISimulatorRepository
    {

        private readonly IMsfs msfs;

        public MsfsSimulatorRepository(IMsfs msfs)
        {
            this.msfs = msfs;
        }

        public bool IsOpen => msfs.IsOpen;

        public void Open()
        {
            msfs.Open();
        }

        public void Close()
        {
            msfs.Close();
        }

        public void Read<T>(Lvar<T> variable)
        {
            msfs.Read(variable);
        }

        public void Read<T>(SimVar<T> variable)
        {
            msfs.Read(variable);
        }

        public void StartTransaction()
        {
            msfs.StartTransaction();
        }

        public void StopTransaction()
        {
            msfs.StopTransaction();
        }
    }
}
