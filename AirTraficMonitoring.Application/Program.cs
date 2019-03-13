using System;
using System.Collections.Generic;
using System.Configuration;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Monitor;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Application
{
    class Program
    {
        static void Main(string[] args)
        {

            var Flights = new List<ITrack>()
            {
                new FlightTrack("ATR423", 39045, 12932, 14000),
                new FlightTrack("BOR725", 25045, 56804, 13000),
                new FlightTrack("MTB927", 57045, 12560, 12000),
                new FlightTrack("KLT229", 70045, 11456, 11000),
            };


            var FlightsInSeperation = new List<ITrack>()
            {
                new FlightTrack("MTB927", 57045, 12560, 12000),
                new FlightTrack("KLT229", 70045, 11456, 11000),
            };


            var monitor = new ConsoleMonitor();

            monitor.ShowAllFlightsInAirspace(Flights);
            monitor.ShowSeparationCondition(FlightsInSeperation);
            

        }
    }
}
