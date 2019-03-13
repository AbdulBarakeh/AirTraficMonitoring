using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.FlightAirspace
{
    class Airspace : ITrack
    {
        List<FlightTrack> ListOfFlights;
        Airspace()
        {

        }// Tager track objekt og tjekker liste efter tidligerer fly;

        string ITrack.Tag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double ITrack.XPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double ITrack.YPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double ITrack.Altitude { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double ITrack.Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double ITrack.CompassCourse { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
