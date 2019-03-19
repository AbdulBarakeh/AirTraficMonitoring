using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.FlightAirspace
{
   public interface IAirspace : ITrack
    {
        double Width { get; set; }
        double Height { get; set; }
        double MinAlt { get; set; }
        double MaxAlt { get; set; }
    }
}
