using System.Linq;
using NUnit.Framework;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Test.Unit.Monitor;
using AirTraficMonitoring.Track;
using AirTraficMonitoring.Validator;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using NSubstitute;

namespace AirTraficMonitoring.Test.Unit.Flightspace
{
    [Author("AMBA")]

    [TestFixture]
    public class AirspaceUnit
    {
        private const string DefaultFlightTag = "ABC123";
        private const string DefaultTimeStamp = "20190321155458675";

        private Airspace _uut;
        private IValidator _flightValidator;
        private bool _eventRaised;

        [SetUp]
        public void Setup()
        {
            _flightValidator = Substitute.For<IValidator>();
            
            _uut = new Airspace(_flightValidator, 80000, 80000, 500, 20000);

            _uut.FlightAddedEvent += (sender, args) => _eventRaised = true;
        }
        
        [Test]
        public void AddFlight_FlightAirspace_ValidatorCalled()
        {
            var flightTrack = TrackFactory.CreateTestTrack(); 

            _uut.Add(flightTrack);

            _flightValidator.Received().Validate(_uut, flightTrack);
        }

        [Test]
        public void AddFlight_FlightIsWithinAirspace_AddedToList()
        {
            var flightTrack = TrackFactory.CreateTestTrack();
            _flightValidator.Validate(_uut, flightTrack).Returns(true);

            _uut.Add(flightTrack);

            Assert.That(_uut.ListOfFlights.Count(t => t.Tag == flightTrack.Tag), Is.EqualTo(1));
        }

        [Test]
        public void AddFlight_FlightIsNotWithinAirspace_NotAddedToList()
        {
            var flightTrack = TrackFactory.CreateTestTrack();
            _flightValidator.Validate(_uut, flightTrack).Returns(false);

            _uut.Add(flightTrack);

            Assert.That(_uut.ListOfFlights.Count(t => t.Tag == flightTrack.Tag), Is.EqualTo(0));
        }

        [Test]
        public void AddFlight_FlightWithSameTagIsAlreadyWithinList_FlightIsReplaced()
        {
            _uut.ListOfFlights.Add(TrackFactory.CreateTestTrack());

            var flightTrack = TrackFactory.CreateTestTrack();
            _flightValidator.Validate(_uut, flightTrack).Returns(true);

            _uut.Add(flightTrack);

            Assert.That(_uut.ListOfFlights.Count(t => t.Tag == flightTrack.Tag), Is.EqualTo(1));
        }

        [Test]
        public void AddFlight_FlightAddedEvent_EventIsRaised()
        {
            var flightTrack = TrackFactory.CreateTestTrack();
            _flightValidator.Validate(_uut, flightTrack).Returns(true);

            _uut.Add(flightTrack);

            Assert.That(_eventRaised, Is.EqualTo(true));
        }
    }
}
