using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.FlightValidation
{
    public interface IFlightValidation
    {
        bool Validation(ITrack track, IAirspace airspace);
    }
}
