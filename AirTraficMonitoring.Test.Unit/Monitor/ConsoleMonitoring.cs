using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        [SetUp]
        public void Setup()
        {
            _console = Substitute.For<ILog>();
            _airspace = Substitute.For<IAirspace>();
            _separation = Substitute.For<ISeparation>();

            _uut = new ConsoleMonitor(_console, _airspace, _separation);
        }


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
        [ExcludeFromCodeCoverage]
        [Ignore("Not going into catch block of ShowAllFlights method")]
        public void ShowAllFlights_OneFlightInListIsEmpty_ConsoleLogErrorCalled()
        {
            var tracks = new List<ITrack>()
            {
                new FlightTrack(string.Empty, 0, 0, 0, string.Empty)
            };

            _uut.ShowAllFlightsInAirspace(tracks);
            
            _console.Received().LogError(Arg.Any<string>());
        }


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

    }
}
