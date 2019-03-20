using System;
using System.Collections.Generic;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Logger.Exceptions;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Monitor
{
    public class ConsoleMonitor : IMonitor
    {
        private readonly ILog _console;

        public ConsoleMonitor(ILog console, IAirspace airspace)
        {
            _console = console;

            // TO DO:
            // LISTEN FOR NEW FLIGHTS IN AIRSPACE (EVENT) - AIRSPACE
            // LISTEN FOR NEW SEPARATION CONCERN IN AIRSPACE (EVENT) - SEPARATION
        }

        public void ShowAllFlightsInAirspace(IList<ITrack> tracks)
        {
            if (tracks.Count == 0) return;

            foreach (var track in tracks)
            {
                try
                {
                    _console.LogInformation($"{track.Tag}");
                }
                catch (LoggerArgumentIsNullOrWhiteSpaceException e)
                {
                    _console.LogError(e.Message);
                }
            }
        }

        public void ShowSeparationCondition(List<ITrack> tracks)
        {
            if (tracks.Count != 2) return;

            try
            {
                _console.LogInformation($"SEPARATION: [{tracks[0].Tag}, {tracks[1].Tag}]");
            }
            catch (LoggerArgumentIsNullOrWhiteSpaceException e)
            {
                _console.LogError(e.Message);
            }   
        }
    }
}