﻿using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Adaptation.Msfs;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload.Glareshield;
using A320_Cockpit.Domain.UseCase.Send;
using A320_Cockpit.Domain.UseCase.Send.SendPayload.Brightness;
using A320_Cockpit.Domain.UseCase.Send.SendPayload.Glareshield;
using A320_Cockpit.Domain.UseCase.Send.SendPayload.Overhead;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Brightness;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Glareshield;
using A320_Cockpit.Infrastructure.Repository.Payload.A32nx.Overhead;
using A320_Cockpit.Infrastructure.Repository.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.MainThread
{
    public class MsfsThread : IMainThread
    {

        private readonly ILogHandler logger;
        private readonly MsfsSimulatorRepository msfs;
        private bool running = false;

        private readonly List<SendUseCase> sendUseCases;

        public MsfsThread(MsfsSimulatorRepository msfs, ILogHandler logger, ISendPresenter presenter, ICockpitRepository cockpitRepository)
        {
            this.msfs = msfs;
            this.logger = logger;

            sendUseCases = new()
            {
                new SendFcuDisplay(cockpitRepository, presenter, new A32nxFcuDisplayRepository(msfs)),
                new SendFcu(cockpitRepository, presenter, new A32nxFcuRepository(msfs)),
                new SendBrightness(cockpitRepository, presenter, new A32nxBrightnessRepository(msfs)),
                new SendLightIndicator(cockpitRepository, presenter, new A32nxLightIndicatorsRepository(msfs))
            };
        }

        public void Stop()
        {
            running = false;
        }

        public void Start() 
        {
            running = true;
            new Thread(() =>
            {
                while(true)
                {
                    if(!running)
                    {
                        break;
                    }

                    if(!msfs.IsOpen)
                    {
                        Thread.Sleep(1000);
                    } else
                    {
                        try
                        {
                            msfs.StartTransaction();

                            foreach(SendUseCase sendUseCase in sendUseCases)
                            {
                                sendUseCase.Exec();
                            }

                            msfs.StopTransaction();
                        } catch(Exception ex)
                        {
                            logger.Error(ex);
                        }
                    }
                }
            }).Start();
        }

    }
}
