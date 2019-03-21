using System;
using System.Collections.Generic;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;
using NSubstitute;
using NUnit.Framework;

namespace AirTraficMonitoring.Test.Unit.Separation
{
    internal class TestSeparationSource : ECS.IAirspace
    {
        public event EventHandler<FlightAddedEventArg> FlightAddedEvent;
        public void RaiseEvent(List<ITrack> newTrack)
        {
            FlightAddedEvent?.Invoke(
                this,
                new FlightAddedEventArg() { Tracks = newTrack}
                );

        }
    }
}