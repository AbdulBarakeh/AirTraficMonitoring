using System.Collections.Generic;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Logger;
using AirTraficMonitoring.Monitor;
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

        [SetUp]
        public void Setup()
        {
            _console = Substitute.For<ILog>();
            _airspace = Substitute.For<IAirspace>();

            _uut = new ConsoleMonitor(_console, _airspace);
        }


        [Test]
        public void ShowAllFlights_ListOfTracksIsEmpty_ConsoleLogInformationNotCalled()
        {
            var tracks = new List<ITrack>();

            _uut.ShowAllFlightsInAirspace(tracks);

            _console.DidNotReceive().LogInformation(Arg.Any<string>());
        }

        public void ShowAllFlights_ListOfTracksIsNotEmpty_ConsoleLogInformationCalled()
        {
            var tracks = 
        }
    }
}
