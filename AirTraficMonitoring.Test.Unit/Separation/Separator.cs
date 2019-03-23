using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Separation;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Test.Unit.Separation
{
    [Author("MB")]
    [TestFixture]
    public class Separator
    {
        private FlightSeparation _uut;

        private FlightAddedEventArg _receivedEventArg;
        private IAirspace _airspace;
        private List<ITrack> myList = new List<ITrack>();

        [SetUp]
        public void Setup()
        {
            _airspace = Substitute.For<IAirspace>();
            _uut = new FlightSeparation(_airspace);
        }

        [Test]
        public void FlightAdded_ListIsNotEmpty_()
        {
            myList.Add();
            _airspace.FlightAddedEvent += Raise.EventWith(new FlightAddedEventArg { Tracks = myList });
            Assert.That(_receivedEventArg.Tracks, Is.Null);
        }
    }
}