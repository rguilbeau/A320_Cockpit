﻿using A320_Cockpit.Adapter.MsfsConnectorAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter.FcuipcAdapter;
using A320_Cockpit.Adapter.MsfsConnectorAdapter.SimConnectAdapter;
using A320_Cockpit.Domain.CanBus;
using A320_Cockpit.Domain.Payload;
using A320_Cockpit.Domain.UseCase;
using A320_Cockpit.Domain.VariableUpdater;
using A320_Cockpit.Infrastructure.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.MsfsVariableUpdater
{
    internal class MsfsFcuDisplayUpdater : MsfsUpdator<IFcuDisplayVariableUpdater.Updates>, IFcuDisplayVariableUpdater
    {
        private static readonly  Lvar<bool> IsExpeditedMode = new("A32NX_FMA_EXPEDITE_MODE");
        public static readonly SimVar<bool> IsMachSpeed = new("AUTOPILOT MANAGED SPEED IN MACH");

        private FcuDisplayPayload fcuDisplayPayload;

        public MsfsFcuDisplayUpdater(MsfsConnector msfsConnector, ICanBus can) : base(msfsConnector, can)
        {
            fcuDisplayPayload = new();
        }

        protected override void UpdateVariables(IFcuDisplayVariableUpdater.Updates update)
        {
            switch (update)
            {
                case IFcuDisplayVariableUpdater.Updates.SPEED:
                    msfsConnector.Update(IsExpeditedMode);
                    msfsConnector.Update(IsMachSpeed);
                    break;
            }

            fcuDisplayPayload.IsMach = IsMachSpeed.Value;
        }

        protected override void VariablesUpdated()
        {
            new SendFcuDisplay(canBus, new ConsoleFramePresenter()).Send(fcuDisplayPayload);
        }
    }
}
