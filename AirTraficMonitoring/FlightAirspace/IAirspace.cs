using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTraficMonitoring.FlightAirspace
{
   public interface IAirspace
    {
        double Width { get; set; }
        double Height { get; set; }
        double MinAlt { get; set; }
        double MaxAlt { get; set; }
    }
}
