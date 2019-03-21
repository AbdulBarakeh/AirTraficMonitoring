using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.FlightAirspace
{
   public class FlightAddedEventArg : EventArgs
    {
        public List<ITrack> Tracks { get; set; }
    }
}
