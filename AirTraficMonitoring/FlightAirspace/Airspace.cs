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
        List<FlightTrack> ListOfFlights = new List<FlightTrack>();

        Airspace(FlightTrack flight)
        {
            //Iterate through list of flights 

            var results = ListOfFlights.FindAll(a => a.Tag == flight.Tag);

            if (results.Count < 0)
            {
                throw new System.ArgumentException("Parameter can't be less than 0", "results");
            }
            else if (results.Count == 0)
            {
                ListOfFlights.Add(flight);
            }
            else
            {
                //Udregn fart 
                // Opdaterer koordinater
            }

        }

        public string Tag { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double Altitude { get; set; }
        public double Velocity { get; set; }
        public double CompassCourse { get; set; }
    }
}
