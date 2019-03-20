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
        List<ITrack> sepatationList = new List<ITrack>();
        public FlightSeparation(Airspace airspace)
        {
            _airspace = airspace;
            _airspace.attachment(this);
        }

        public void Update(ITrack track)
        {
            Console.WriteLine("test");

            sepatationList.Add(track);

            //foreach (var newTrack in IAirspace.sepatationList)
            //{
            //    if ()
            //}
        }
    }
}