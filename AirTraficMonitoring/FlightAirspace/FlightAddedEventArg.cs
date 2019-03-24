using System;
using System.Collections.Generic;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.FlightAirspace
{
   public class FlightAddedEventArg : EventArgs
    {
        public List<ITrack> Tracks { get; set; }
    }
}
