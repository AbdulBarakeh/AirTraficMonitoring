using System;
using System.Collections.Generic;
using System.Linq;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Separation
{
    public class FlightSeparation : ISeparation
    {
        private readonly ILog _file;

        public event EventHandler<SeparationWarningEventArg> SeparationWarningEvent;

        public FlightSeparation(ILog file, IAirspace airspace)
        {
            _file = file;

            airspace.FlightAddedEvent += HandleFlightAddedEvent;
        }

        private void HandleFlightAddedEvent(object sender, FlightAddedEventArg e)
        {
            CheckForSeparation(e.Tracks);
        }

        protected virtual void OnSeparationWarningEvent(SeparationWarningEventArg args)
        {
            SeparationWarningEvent?.Invoke(this, args);
        }

        #region TO DO
        // Separation tilføjes til fil to gange pr. separation fordi listen løbes igennem for alle fly og checkes med samme liste
        // Log kun til filen en gang pr. seperation
        #endregion
        public void CheckForSeparation(List<ITrack> tracks)
        {
            foreach (var track in tracks)
            {
                foreach (var otherTrack in tracks)
                {
                    if (track.Tag == otherTrack.Tag) continue;

                    var delta = new
                    {
                        X = track.XPosition - otherTrack.XPosition,
                        Y = track.YPosition - otherTrack.YPosition,
                    };

                    var distance = new
                    {
                        Horizontal = Math.Sqrt(Math.Pow(delta.X, 2) + Math.Pow(delta.Y, 2)),
                        Vertical = Math.Abs(track.Altitude - otherTrack.Altitude)
                    };

                    if (distance.Horizontal < 5000 && distance.Vertical < 300)
                    {
                        var flightsInSeparation = new List<ITrack>(){track, otherTrack};
            
                        _file.LogWarning($"SEPARATION: [{flightsInSeparation[0].Tag}, {flightsInSeparation[1].Tag}] DISTANCE: [H:V] [{distance.Horizontal}:{distance.Vertical}]");

                        OnSeparationWarningEvent(new SeparationWarningEventArg(){SeparationList = flightsInSeparation});
                    }
                }
            }
        }
    }
}