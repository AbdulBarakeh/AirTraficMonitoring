using System;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Separation;
using NSubstitute;
using NUnit.Framework;

namespace AirTraficMonitoring.Test.Unit.Separation
{
    [TestFixture]
    public class SeparatorTest
    {
        private FlightSeparation _uut;

        //private FlightAddedEventArg _receivedEventArg;
        private TestSeparationSource _airspace;

        [SetUp]
        public void Setup()
        {
            _airspace = Substitute.For<IAirspace>();
            _uut = new FlightSeparation(_airspace);

        }
    }
}