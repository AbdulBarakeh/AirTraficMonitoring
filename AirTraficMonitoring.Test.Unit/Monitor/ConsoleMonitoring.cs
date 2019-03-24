using System.Collections.Generic;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Monitor;
using AirTraficMonitoring.Separation;
using AirTraficMonitoring.Track;
using NSubstitute;
using NUnit.Framework;

namespace AirTraficMonitoring.Test.Unit.Monitor
{
    [Author("MSK")]

    [TestFixture]
    public class ConsoleMonitoring
    {
        private ConsoleMonitor _uut;
        private ILog _console;
        private IAirspace _airspace;
        private ISeparation _separation;

        private bool _eventHandled;
        private FlightAddedEventArg _flightAddedEventArg;
        private SeparationWarningEventArg _separationWarningEventArg;

        [SetUp]
        public void Setup()
        {
            _eventHandled = false;

            _console = Substitute.For<ILog>();
            _airspace = Substitute.For<IAirspace>();
            _separation = Substitute.For<ISeparation>();

            _uut = new ConsoleMonitor(_console, _airspace, _separation);

            _airspace.FlightAddedEvent += (sender, args) => _flightAddedEventArg = args;
            _airspace.FlightAddedEvent += (sender, args) => _eventHandled = true;

            _separation.SeparationWarningEvent += (sender, args) => _separationWarningEventArg = args;
            _separation.SeparationWarningEvent += (sender, args) => _eventHandled = true;
        }

        #region ShowFlightsMethod
        
        [Test]
        public void ShowAllFlights_ListOfTracksIsEmpty_ConsoleLogInformationNotCalled()
        {
            var tracks = new List<ITrack>();

            _uut.ShowAllFlightsInAirspace(tracks);

            _console.DidNotReceive().LogInformation(Arg.Any<string>());
        }

        [Test]
        public void ShowAllFlights_ListOfTracksContainsOneElement_ConsoleLogInformationCalled()
        {
            var tracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack()
            };

            _uut.ShowAllFlightsInAirspace(tracks);

            _console.Received(1).LogInformation(Arg.Any<string>());
        }

        [Test]
        public void ShowAllFlights_ListOfTracksContainsTwoElements_ConsoleLogInformationCalled()
        {
            var tracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack()
            };

            _uut.ShowAllFlightsInAirspace(tracks);

            _console.Received(2).LogInformation(Arg.Any<string>());
        }

        [Test]
        public void ShowAllFlights_ListOfTracksContainsTenElements_ConsoleLogInformationCalled()
        {
            var tracks = new List<ITrack>();

            for (var i = 0; i < 10; i++)
                tracks.Add(TrackFactory.CreateTestTrack());
            
            _uut.ShowAllFlightsInAirspace(tracks);

            _console.Received(10).LogInformation(Arg.Any<string>());
        }

        [Test]
        public void ShowAllFlights_NoFlightInListIsEmpty_ConsoleLogErrorNotCalled()
        {
            var tracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack()
            };

            _uut.ShowAllFlightsInAirspace(tracks);

            _console.DidNotReceive().LogError(Arg.Any<string>());
        }

        [Test]
        public void ShowAllFlights_OneFlightInListIsNull_ThrowsException()
        {        
            Assert.That(() => _uut.ShowAllFlightsInAirspace(null), Throws.Exception);
        }

        #endregion

        #region ShowSeparationMethod

        [Test]
        public void ShowSeparationCondition_ListOfTracksContainsZeroElements_ConsoleLogInformationNotCalled()
        {
            var tracks = new List<ITrack>();

            _uut.ShowSeparationCondition(tracks);

            _console.DidNotReceive().LogInformation(Arg.Any<string>());
        }

        [Test]
        public void ShowSeparationCondition_ListOfTracksContainsOneElement_ConsoleLogInformationNotCalled()
        {
            var tracks = new List<ITrack>(){ TrackFactory.CreateTestTrack() };

            _uut.ShowSeparationCondition(tracks);

            _console.DidNotReceive().LogInformation(Arg.Any<string>());
        }

        [Test]
        public void ShowSeparationCondition_ListOfTracksContainsThreeElements_ConsoleLogInformationNotCalled()
        {
            var tracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack()
            };

            _uut.ShowSeparationCondition(tracks);

            _console.DidNotReceive().LogInformation(Arg.Any<string>());
        }

        [Test]
        public void ShowSeparationCondition_ListOfTracksContainsTenElements_ConsoleLogInformationNotCalled()
        {
            var tracks = new List<ITrack>();

            for (var i = 0; i < 10; i++)
                tracks.Add(TrackFactory.CreateTestTrack());

            _uut.ShowSeparationCondition(tracks);

            _console.DidNotReceive().LogInformation(Arg.Any<string>());
        }

        [Test]
        public void ShowSeparationCondition_ListOfTracksContainsTwoElements_ConsoleLogInformationCalled()
        {
            var tracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack(),
            };

            _uut.ShowSeparationCondition(tracks);

            _console.Received().LogInformation(Arg.Any<string>());
        }

        [Test]
        public void ShowSeparationCondition_ListOfTracksContainsTwoElements_ConsoleLogErrorNotCalled()
        {
            var tracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack(),
            };

            _uut.ShowSeparationCondition(tracks);

            _console.DidNotReceive().LogError(Arg.Any<string>());
        }

        [Test]
        public void ShowSeparationCondition_FlightInListIsNull_ThrowsException()
        {
            Assert.That(() => _uut.ShowSeparationCondition(null), Throws.Exception);
        }

        #endregion

        #region FlightAddedEventHandling

        [Test]
        public void HandleFlightAddedEvent_FlightsReceived_EventIsHandled()
        {
            var flightTracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack()
            };

            _airspace.FlightAddedEvent += Raise.EventWith(new FlightAddedEventArg(){ Tracks = flightTracks});

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

            _airspace.FlightAddedEvent += Raise.EventWith(new FlightAddedEventArg() {Tracks = flightTracks});

            Assert.That(_flightAddedEventArg.Tracks, Is.EqualTo(flightTracks));
        }

        #endregion

        #region SeparationWarningEventHandling

        [Test]
        public void HandleSeparationWarningEvent_SeparationOccured_EventIsHandled()
        {
            var flightTracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack()
            };

            _separation.SeparationWarningEvent += Raise.EventWith(new SeparationWarningEventArg()
                {SeparationList = flightTracks});
                
            Assert.That(_eventHandled, Is.True);
        }

        [Test]
        public void HandleSeparationWarningEvent_SeparationOccured_ListOfFlightsReceived()
        {
            var flightTracks = new List<ITrack>()
            {
                TrackFactory.CreateTestTrack(),
                TrackFactory.CreateTestTrack()
            };

            _separation.SeparationWarningEvent += Raise.EventWith(new SeparationWarningEventArg()
                {SeparationList = flightTracks});

            Assert.That(_separationWarningEventArg.SeparationList, Is.EqualTo(flightTracks));
        }

        #endregion

    }
}
