using A320_Cockpit.Adaptation.Log;
using A320_Cockpit.Infrastructure.Aircraft;
using A320_Cockpit.Infrastructure.View.SystemTray;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.View.StartupDialog
{
    /// <summary>
    /// Classe permettant de checker les options choisis dans la StartupDialog
    /// </summary>
    public class AircraftOptionsChecker
    {
        private readonly IAircraft ?aircraft;
        private readonly string errorMessage;

        public AircraftOptionsChecker(string? port, string? aircraftName, ILogHandler logger)
        {
            errorMessage = string.Empty;

            if (port == null || aircraftName == null)
            {
                errorMessage = "Vous devez sélectionner un COM Port et un avion pour démarrer l'application";
            }
            else
            {
                switch (aircraftName)
                {
                    case A32nx.NAME:
                        aircraft = new A32nx(logger, port);
                        break;
                    case FakeA320.NAME:
                        aircraft = new FakeA320(logger, port);
                        break;
                }

                if (aircraft == null)
                {
                    errorMessage = "L'avion " + aircraftName + " est inconnu";
                }
            }
        }

        /// <summary>
        /// L'avion
        /// </summary>
        public IAircraft? Aircraft => aircraft;

        /// <summary>
        /// Le message d'erreur
        /// </summary>
        public string ErrorMessage => errorMessage;
    }
}
