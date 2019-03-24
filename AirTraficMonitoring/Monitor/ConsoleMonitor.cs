using System.Collections.Generic;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Logger.Exceptions;
using AirTraficMonitoring.Separation;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Monitor
{
    public class ConsoleMonitor : IMonitor
    {
        private readonly ILog _console;

        public ConsoleMonitor(ILog console, IAirspace airspace, ISeparation separation)
        {
            _console = console;
            airspace.FlightAddedEvent += HandleFlightAddedEvent;
            separation.SeparationWarningEvent += HandleSeparationWarningEvent;
        }

        #region FlightSection
        private void HandleFlightAddedEvent(object sender, FlightAddedEventArg args)
        {
            ShowAllFlightsInAirspace(args.Tracks);
        }

        public void ShowAllFlightsInAirspace(IList<ITrack> tracks)
        {
            if (tracks.Count == 0) return;

            foreach (var track in tracks)
            {
                _console.LogInformation($"{track.Tag}");
            }
        }
        #endregion

        #region SeparationSection
        private void HandleSeparationWarningEvent(object sender, SeparationWarningEventArg args)
        {
            ShowSeparationCondition(args.SeparationList);
        }

        public void ShowSeparationCondition(List<ITrack> tracks)
        {
            if (tracks.Count != 2) return;

            _console.LogInformation($"SEPARATION: [{tracks[0].Tag}, {tracks[1].Tag}]");
        }
        #endregion
    }
}