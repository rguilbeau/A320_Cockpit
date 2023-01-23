using A320_Cockpit.Adapter.LogHandler;
using A320_Cockpit.Adapter.SimulatorHandler;
using A320_Cockpit.Domain.BusSend.Payload;
using A320_Cockpit.Domain.BusSend.UseCase;
using A320_Cockpit.Domain.CanBus;
using A320_Cockpit.Domain.Connexion.SimConnector;

namespace A320_Cockpit.Infrastructure.MsfsVariableUpdater.A32NX_VariableUpdater
{
    /// <summary>
    /// Système de mise à jours des variables de des afficheurs du FCU depuis l'A32NX
    /// </summary>
    public class A32NX_FcuDisplayUpdater : MsfsUpdater<A32NX_FcuDisplayUpdater.Updates, FcuDisplayPayload>
    {
        /// <summary>
        /// Mise à jour disponibles
        /// </summary>
        public enum Updates
        {
            SPEED_IN_MACH,
            SPEED,
            SPEED_MANAGED,
            HEADING,
            HEADING_MANAGED,
            IS_TRACK,
            ALTITUDE,
            ALTITUDE_MANAGED,
            VERTICAL_SPEED,
            VERTICAL_SPEED_MANAGED
        };

        private FcuDisplayPayload fcuDisplayPayload;
        private readonly SendFcuDisplay sender;

        /// <summary>
        /// Création du système de mise à jours des variables de des afficheurs du FCU depuis l'A32NX
        /// </summary>
        /// <param name="simulatorHandler">Le connecteur MSFS</param>
        /// <param name="canBus">Le CAN Bus</param>
        /// <param name="presenter">Le présenteur de sortie</param>
        public A32NX_FcuDisplayUpdater(ISimulatorHandler simulatorHandler, ICanBus can, ISendFramePresenter presenter, ILogHandler logger) : base(simulatorHandler, can, presenter, logger)
        {
            sender = new SendFcuDisplay(canBus, presenter);
            fcuDisplayPayload = new()
            {
                IsLat = true,
                IsAltitudeHidden = false,
            };
        }

        /// <summary>
        /// Le payload des variables
        /// </summary>
        public override FcuDisplayPayload Payload
        {
            get { return fcuDisplayPayload; }
            set { fcuDisplayPayload = value; }
        }

        /// <summary>
        /// Mets à jour les variables depuis MSFS. 
        /// Le type de mise à jours permet de cibler précisement quelles variables mettre à jour
        /// </summary>
        /// <param name="update">Le type de mise à jour</param>
        protected override void UpdateVariables(Updates update)
        {
            switch (update)
            {
                case Updates.SPEED_IN_MACH:
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.IsMachSpeed);
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.SpeedSelected);
                    break;
                case Updates.SPEED:
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.IsSpeedManagedDash);
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.SpeedSelected);
                    break;
                case Updates.SPEED_MANAGED:
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.IsSpeedDot);
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.IsSpeedManagedDash);
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.SpeedSelected);
                    break;
                case Updates.HEADING:
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.IsHeadingManageDash);
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.HeadingSelected);
                    break;
                case Updates.HEADING_MANAGED:
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.IsHeadingDot);
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.IsHeadingManageDash);
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.HeadingSelected);
                    break;
                case Updates.IS_TRACK:
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.IsTrackFpa);
                    if (A32NX_Variables.FcuDisplay.IsTrackFpa.Value)
                    {
                        simulatorHandler.Read(A32NX_Variables.FcuDisplay.VerticalSpeedSelectedFpa);
                    }
                    else
                    {
                        simulatorHandler.Read(A32NX_Variables.FcuDisplay.VerticalSpeedSelectedFpm);
                    }
                    break;
                case Updates.ALTITUDE:
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.AltitudeSelected);
                    break;
                case Updates.ALTITUDE_MANAGED:
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.AltitudeManaged);
                    break;
                case Updates.VERTICAL_SPEED:
                    if (fcuDisplayPayload.IsFpa)
                    {
                        simulatorHandler.Read(A32NX_Variables.FcuDisplay.VerticalSpeedSelectedFpa);
                    }
                    else
                    {
                        simulatorHandler.Read(A32NX_Variables.FcuDisplay.VerticalSpeedSelectedFpm);
                    }
                    break;
                case Updates.VERTICAL_SPEED_MANAGED:
                    simulatorHandler.Read(A32NX_Variables.FcuDisplay.VerticalSpeedManaged);

                    if (fcuDisplayPayload.IsFpa)
                    {
                        simulatorHandler.Read(A32NX_Variables.FcuDisplay.VerticalSpeedSelectedFpa);
                    }
                    else
                    {
                        simulatorHandler.Read(A32NX_Variables.FcuDisplay.VerticalSpeedSelectedFpm);
                    }
                    break;
            }
        }

        /// <summary>
        /// Methode appelé après le mise à jour des variables, enrechi le payload avec les valeurs des variables MSFS
        /// </summary>
        protected override void VariablesUpdated()
        {
            if (A32NX_Variables.FcuDisplay.SpeedSelected.Value <= 0)
            {
                fcuDisplayPayload.IsMach = A32NX_Variables.FcuDisplay.IsMachSpeed.Value;
            }
            else
            {
                fcuDisplayPayload.IsMach =
                    A32NX_Variables.FcuDisplay.SpeedSelected.Value > 0 &&
                    A32NX_Variables.FcuDisplay.SpeedSelected.Value < 1;
            }

            fcuDisplayPayload.IsSpeedForced = !A32NX_Variables.FcuDisplay.IsSpeedDot.Value;
            fcuDisplayPayload.IsSpeedHidden = A32NX_Variables.FcuDisplay.IsSpeedManagedDash.Value;
            fcuDisplayPayload.Speed = Math.Round(A32NX_Variables.FcuDisplay.SpeedSelected.Value, 2);

            fcuDisplayPayload.IsHeadingForced = !A32NX_Variables.FcuDisplay.IsHeadingDot.Value;
            fcuDisplayPayload.IsHeadingHidden = A32NX_Variables.FcuDisplay.IsHeadingManageDash.Value;
            fcuDisplayPayload.Heading = A32NX_Variables.FcuDisplay.HeadingSelected.Value;
            fcuDisplayPayload.IsTrack = A32NX_Variables.FcuDisplay.IsTrackFpa.Value;

            fcuDisplayPayload.Altitude = A32NX_Variables.FcuDisplay.AltitudeSelected.Value;
            fcuDisplayPayload.IsAltitudeForced = !A32NX_Variables.FcuDisplay.IsHeadingDot.Value;

            fcuDisplayPayload.IsFpa = A32NX_Variables.FcuDisplay.IsTrackFpa.Value;

            if (A32NX_Variables.FcuDisplay.IsTrackFpa.Value)
            {
                fcuDisplayPayload.VerticalSpeed = A32NX_Variables.FcuDisplay.VerticalSpeedSelectedFpa.Value;
            }
            else
            {
                fcuDisplayPayload.VerticalSpeed = A32NX_Variables.FcuDisplay.VerticalSpeedSelectedFpm.Value;
            }

            fcuDisplayPayload.IsVerticalSpeedHidden = A32NX_Variables.FcuDisplay.VerticalSpeedManaged.Value;
        }

        /// <summary>
        /// Envoi le payload
        /// </summary>
        public override void SendPayload()
        {
            sender.Send(fcuDisplayPayload);
        }
    }
}
