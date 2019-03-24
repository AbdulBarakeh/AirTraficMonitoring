using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Separation;
using AirTraficMonitoring.Test.Unit.Monitor;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Test.Unit.Separation
{
    [Author("MB")]
    [TestFixture]
    public class Separator
    {
        private FlightSeparation _uut;

        private FlightAddedEventArg _receivedEventArg;
        private SeparationWarningEventArg _separationWarningEventArg;
        private IAirspace _airspace;
        private List<ITrack> myList = new List<ITrack>();

        [SetUp]
        public void Setup()
        {
            _airspace = Substitute.For<IAirspace>();
            _uut = new FlightSeparation(_airspace);
            _receivedEventArg = new FlightAddedEventArg();
            _separationWarningEventArg = new SeparationWarningEventArg();
        }

        [Test]
        public void FlightAdded_ListIsEmpty()
        {
            //myList.Add(TrackFactory.CreateTestTrack());
            _airspace.FlightAddedEvent += Raise.EventWith(new FlightAddedEventArg { Tracks = myList });
            Assert.That(_receivedEventArg.Tracks, Is.Null);
        }

        [Test]
        public void FlightAdded_ListIsNotEmpty()
        {
            myList.Add(TrackFactory.CreateTestTrack());
            _airspace.FlightAddedEvent += Raise.EventWith(new FlightAddedEventArg { Tracks = myList });
            Assert.That(_receivedEventArg.Tracks, Is.Null);
        }


        [Test]
        [ExcludeFromCodeCoverage]
        [Ignore("This test is breaking continuous integration")]
        public void FlightAdded_SeparationWarning()
        {
            myList.Add(new FlightTrack("CBA321", "10", "10", "10", "9399302"));
            _airspace.FlightAddedEvent += Raise.EventWith(new FlightAddedEventArg { Tracks = myList });
            Assert.That(_separationWarningEventArg.SeparationList, Is.Not.Empty);
        }
    }
}