using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.Separation;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.FlightAirspace
{
   public interface IAirspace
   {
       void attachment(ISeparation separation);
        void Add(ITrack track);
        double Width { get; set; }
        double Height { get; set; }
        double MinAlt { get; set; }
        double MaxAlt { get; set; }
    }
}
