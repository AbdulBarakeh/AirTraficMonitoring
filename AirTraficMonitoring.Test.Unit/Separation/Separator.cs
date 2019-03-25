using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Separation;
using AirTraficMonitoring.Test.Unit.Monitor;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Test.Unit.Separation
{
    [Author("MB")]

    [TestFixture]
    public class Separator
    {
        private const string Timestamp = "20151006213456789";

        private FlightAddedEventArg _flightAddedEventArg;
        private FlightSeparation _uut;

        private ILog _fileLog;
        private IAirspace _airspace;


        private bool _eventHandled = false;
        private bool _eventRaised = false;

        [SetUp]
        public void Setup()
        {
            _fileLog = Substitute.For<ILog>();
            _airspace = Substitute.For<IAirspace>();

            _uut = new FlightSeparation(_fileLog, _airspace);

            _uut.SeparationWarningEvent += (sender, args) => _eventRaised = true;
            _airspace.FlightAddedEvent += (sender, args) => _eventHandled = true;
            _airspace.FlightAddedEvent += (sender, args) => _flightAddedEventArg = args;
        }

        [Test]
        public void HandleFlightAddedEvent_FlightIsAdded_EventIsHandled()
        {
            var flightTracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack()
            };

            _airspace.FlightAddedEvent += Raise.EventWith(new FlightAddedEventArg(){Tracks = flightTracks});

            Assert.That(_eventHandled, Is.True);
        }

        [Test]
        public void HandleFlightAddedEvent_FlightsReceived_ListOfFlightReceived()
        {
            var flightTracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack()
            };

            _airspace.FlightAddedEvent += Raise.EventWith(new FlightAddedEventArg() { Tracks = flightTracks });

            Assert.That(_flightAddedEventArg.Tracks, Is.EqualTo(flightTracks));
        }

        [Test]
        public void CheckForSeparation_FlightAreInSeparation_EventIsRaised()
        {
            var flightTracks = new List<ITrack>()
            {
                new FlightTrack("ABC123", "5000", "5000", "1000", Timestamp),
                new FlightTrack("DEF456", "4999", "4999", "1000", Timestamp)
            };

            _uut.CheckForSeparation(flightTracks);

            Assert.That(_eventRaised, Is.True);
        }

        [Test]
        public void CheckForSeparation_FlightsAreNotInSeparation_LogWarningNotCalled()
        {
            var flightTracks = new List<ITrack>()
            {
                new FlightTrack("ABC123", "5000", "5000", "1000", Timestamp),
                new FlightTrack("DEF456", "20000", "20000", "1000", Timestamp)
            };

            _uut.CheckForSeparation(flightTracks);

            _fileLog.DidNotReceive().LogWarning(Arg.Any<string>());
        }

        [Test]
        public void CheckForSeparation_FlightsAreInSeparation_LogWarningIsCalled()
        {
            var flightTracks = new List<ITrack>()
            {
                new FlightTrack("ABC123", "5000", "5000", "1000", Timestamp),
                new FlightTrack("DEF456", "4999", "4999", "1000", Timestamp)
            };

            _uut.CheckForSeparation(flightTracks);

            _fileLog.Received().LogWarning(Arg.Any<string>());
        }

        [TestCase("ABC234", "5000", "5000", "701")]
        [TestCase("ABC234", "4999", "4999", "999")]
        [TestCase("ABC234", "5001", "5001", "1001")]
        [TestCase("ABC234", "5000", "5000", "1299")]
        [TestCase("ABC234", "5000", "5000", "1000")]
        public void CheckForSeparation_FlightsAreInSeparation_SeparationWarning(string tag, string xpos, string ypos, string alt)
        {
            var flightTracks = new List<ITrack>()
            {
                new FlightTrack("ABC123", "5000", "5000", "1000", Timestamp),
                new FlightTrack(tag, xpos, ypos, alt, Timestamp)
            };

            _uut.CheckForSeparation(flightTracks);

            _fileLog.Received().LogWarning(Arg.Any<string>());
        }
        
        [TestCase("ABC234", "0", "0", "0")]
        [TestCase("ABC234", "0", "0", "700")]
        [TestCase("ABC234", "0", "0", "1300")]
        [TestCase("ABC234", "0", "5000", "1000")]
        [TestCase("ABC234", "5000", "0", "1000")]
        [TestCase("ABC234", "5000", "5000", "0")]
        [TestCase("ABC234", "5000", "5000", "700")]
        [TestCase("ABC234", "20000", "20000", "1000")]
        [TestCase("ABC234", "10000", "10000", "1000")]
        public void CheckForSeparation_FlightAreNotInSeparation_NoSeparationWarning(string tag, string xpos, string ypos, string alt)
        {
            var flightTracks = new List<ITrack>()
            {
                new FlightTrack("ABC123", "5000", "5000", "1000", Timestamp),
                new FlightTrack(tag, xpos, ypos, alt, Timestamp)
            };

            _uut.CheckForSeparation(flightTracks);

            _fileLog.DidNotReceive().LogWarning(Arg.Any<string>());
        }
    }
}