using System;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.FlightAirspace
{
   public interface IAirspace
   {
        event EventHandler<FlightAddedEventArg> FlightAddedEvent;
        void Add(ITrack track);
        double Width { get; set; }
        double Height { get; set; }
        double MinAlt { get; set; }
        double MaxAlt { get; set; }
    }
}
