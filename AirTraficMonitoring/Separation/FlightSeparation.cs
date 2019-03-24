using System;
using System.Collections.Generic;
using AirTraficMonitoring.Track;
using AirTraficMonitoring.FlightAirspace;

namespace AirTraficMonitoring.Separation
{
    public class FlightSeparation : ISeparation
    {
        private bool _onEvent;
        public event EventHandler<SeparationWarningEventArg> SeparationWarningEvent;
        public List<ITrack> SeparationList = new List<ITrack>();
        public FlightSeparation(IAirspace airspace)
        {
            airspace.FlightAddedEvent += HandleFlightAddedEvent;
        }

        protected virtual void OnSeparationAddedEvent(SeparationWarningEventArg arg)
        {
            SeparationWarningEvent?.Invoke(this, arg);
        }

        private void HandleFlightAddedEvent(object sender, FlightAddedEventArg e)
        {
            foreach (var track in e.Tracks)
            {
                foreach (var otherTrack in e.Tracks)
                {
                    if (track.Tag != otherTrack.Tag)
                    {
                        var deltaXPos = track.XPosition - otherTrack.XPosition;
                        var deltaYPos = track.YPosition - otherTrack.YPosition;
                        var deltaAlt = Math.Abs(track.Altitude - otherTrack.Altitude);

                        //var distance = Math.Sqrt(Math.Pow(deltaXPos, 2) + Math.Pow(deltaYPos, 2));
                        while((Math.Sqrt(Math.Pow(deltaXPos, 2) + Math.Pow(deltaYPos, 2)) < 5000
                            && deltaAlt < 300))
                        {
                            Console.WriteLine("Plains too close");
                            if (!_onEvent)
                            {
                                SeparationList.Add(track);
                                SeparationList.Add(otherTrack);

                                OnSeparationAddedEvent(new SeparationWarningEventArg {SeparationList = SeparationList});
                                _onEvent = true;
                            }
                        } 
                    }
                }
            }
        }

        private void CalculateDistance()
        {

        }


        //public void Update(ITrack track)
        //{
        //    Console.WriteLine("test");

        //    foreach (var newTrack in _airspace.ListOfFlights)
        //    {
        //        foreach (var otherTrack in _airspace.ListOfFlights)
        //        {
        //            //_airspace.ListOfFlights.FindAll(x => x.Tag != newTrack.Tag);
        //            if (newTrack.Tag != otherTrack.Tag)
        //            {
        //                if ((newTrack.XPosition - otherTrack.XPosition) < 300)
        //                {
        //                }

        //                //raise event
        //            }
        //        }
        //    }
        //}
    }
}