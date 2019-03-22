using System;
using AirTraficMonitoring.Track;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using AirTraficMonitoring.FlightAirspace;

namespace AirTraficMonitoring.Separation
{
    public class FlightSeparation : ISeparation
    {
        private Airspace _airspace;
        public FlightSeparation(Airspace airspace)
        {
            _airspace = airspace;
        }

        public void Update(ITrack track)
        {
            Console.WriteLine("test");

            foreach (var newTrack in _airspace.ListOfFlights)
            {
                foreach (var otherTrack in _airspace.ListOfFlights)
                {
                    //_airspace.ListOfFlights.FindAll(x => x.Tag != newTrack.Tag);
                    if (newTrack.Tag != otherTrack.Tag)
                    {
                        if ((newTrack.XPosition - otherTrack.XPosition) < 300)
                        {
                        }

                        //raise event
                    }
                }
            }
        }
    }
}