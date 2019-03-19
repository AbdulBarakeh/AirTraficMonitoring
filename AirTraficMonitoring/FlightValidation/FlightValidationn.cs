using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTraficMonitoring.FlightValidation
{
    class FlightValidationn
    {
        public bool Validation(ITrack track, IAirspace airspace)
        {
            if (airspace.Width < track.XPosition)
            {
                return false;
            }

            if (airspace.Height < track.YPosition)
            {
                return false;
            }

            if (track.Altitude > airspace.MaxAlt)
            {
                return false;
            }

            if (track.Altitude < airspace.MinAlt)
            {
                return false;
            }
            return true;

        }
    }
}
