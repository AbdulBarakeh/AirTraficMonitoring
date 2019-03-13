using System;
using System.Collections.Generic;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Monitor
{
    public class ConsoleMonitor : IMonitor
    {
        public ConsoleMonitor()
        {
            
        }

        public void ShowAllFlightsInAirspace(IList<ITrack> tracks)
        {
            foreach (var track in tracks)
            {
                Console.WriteLine($"{track.Tag} X:{track.XPosition} Y:{track.YPosition}");
            }
        }

        public void ShowSeparationCondition(List<ITrack> tracks)
        {
            Console.WriteLine($"SEPERATION: [{tracks[0].Tag}, {tracks[1].Tag}]");
        }
    }
}