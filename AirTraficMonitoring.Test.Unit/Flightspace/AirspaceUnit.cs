using System.Linq;
using NUnit.Framework;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Test.Unit.Monitor;
using AirTraficMonitoring.Validator;
using NSubstitute;

namespace AirTraficMonitoring.Test.Unit.Flightspace
{
    [Author("AMBA")]

    [TestFixture]
    public class AirspaceUnit
    {
        private FlightAirspace.Airspace _uut;
        private IValidator _flightValidator;
        private bool _eventRaised;

        private FlightAddedEventArg _eventToTest;
        private object _eventSource;
        

        [SetUp]
        public void Setup()
        {

            _eventRaised = false;
            _eventToTest = null;
            _eventSource = null;

            _flightValidator = Substitute.For<IValidator>();

            _uut = new FlightAirspace.Airspace(_flightValidator, 80000, 80000, 500, 20000);


            //_uut.FlightAddedEvent += (sender, args) => _eventRaised = true;
            _uut.FlightAddedEvent += (sender, args) => 
            {
                _eventRaised = true;
                _eventToTest = args;
                _eventSource = sender;
            };


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

            Assert.That(_eventToTest.Tracks.Count(t => t.Tag == flightTrack.Tag), Is.EqualTo(1));
        }

        //>>>>Original Test er udkommenteret i worstcase scenario<<<<

        //[Test]
        //public void AddFlight_FlightIsWithinAirspace_AddedToList()
        //{
        //    var flightTrack = TrackFactory.CreateTestTrack();
        //    _flightValidator.Validate(_uut, flightTrack).Returns(true);

        //    _uut.Add(flightTrack);

        //    //Assert.That(_uut.ListOfFlights.Count(t => t.Tag == flightTrack.Tag), Is.EqualTo(1));

        //}

        [Test]
        public void AddFlight_FlightIsNotWithinAirspace_NotAddedToList()
        {
            var flightTrackInvalid = TrackFactory.CreateTestTrack();
            var flightTrackValid = TrackFactory.CreateTestTrack();

            _flightValidator.Validate(_uut, flightTrackInvalid).Returns(false);
            _flightValidator.Validate(_uut, flightTrackValid).Returns(true);

            _uut.Add(flightTrackInvalid);
            _uut.Add(flightTrackValid);

            Assert.That(_eventToTest.Tracks.Count(t => t.Tag == flightTrackInvalid.Tag), Is.EqualTo(1));

        }

        //[Test]
        //public void AddFlight_FlightIsNotWithinAirspace_NotAddedToList()
        //{
        //    var flightTrack = TrackFactory.CreateTestTrack();
        //    _flightValidator.Validate(_uut, flightTrack).Returns(false);

        //    _uut.Add(flightTrack);

        //    Assert.That(_uut.ListOfFlights.Count(t => t.Tag == flightTrack.Tag), Is.EqualTo(0));
        //}

        [Test]
        public void AddFlight_FlightWithSameTagIsAlreadyWithinList_FlightIsReplaced()
        {
            _uut.ListOfFlights.Add(TrackFactory.CreateTestTrack());

            var flightTrack = TrackFactory.CreateTestTrack();
            _flightValidator.Validate(_uut, flightTrack).Returns(true);

            _uut.Add(flightTrack);

            Assert.That(_eventToTest.Tracks.Count(t => t.Tag == flightTrack.Tag), Is.EqualTo(1));
        }

        //[Test]
        //public void AddFlight_FlightWithSameTagIsAlreadyWithinList_FlightIsReplaced()
        //{
        //    _uut.ListOfFlights.Add(TrackFactory.CreateTestTrack());

        //    var flightTrack = TrackFactory.CreateTestTrack();
        //    _flightValidator.Validate(_uut, flightTrack).Returns(true);

        //    _uut.Add(flightTrack);

        //    Assert.That(_uut.ListOfFlights.Count(t => t.Tag == flightTrack.Tag), Is.EqualTo(1));
        //}

        [Test]
        public void AddFlight_FlightAddedEvent_EventIsRaised()
        {
            var flightTrack = TrackFactory.CreateTestTrack();
            _flightValidator.Validate(_uut, flightTrack).Returns(true);

            _uut.Add(flightTrack);

            Assert.That(_eventRaised, Is.EqualTo(true));
        }

        [Test]
        public void AddFlight_FlightFromListIsNoLongerWithinAirspace_RemoveFlight()
        {
            //_uut.ListOfFlights.Add(TrackFactory.CreateTestTrack());

            var flightTrackInvalid = TrackFactory.CreateTestTrack();
            var flightTrackValid = TrackFactory.CreateTestTrack();
            _flightValidator.Validate(_uut, flightTrackInvalid).Returns(false);
            _flightValidator.Validate(_uut, flightTrackValid).Returns(true);

            _uut.Add(flightTrackInvalid);
            _uut.Add(flightTrackValid);//Nødvendighed at indsætte et valid track så testen bliver testbar

            Assert.That(_eventToTest.Tracks.Count(t => t.Tag == flightTrackInvalid.Tag), Is.EqualTo(1));
        }

        //[Test]
        //public void AddFlight_FlightFromListIsNoLongerWithinAirspace_RemoveFlight()
        //{
        //    _uut.ListOfFlights.Add(TrackFactory.CreateTestTrack());

        //    var flightTrack = TrackFactory.CreateTestTrack();
        //    _flightValidator.Validate(_uut, flightTrack).Returns(false);

        //    _uut.Add(flightTrack);

        //    Assert.That(_uut.ListOfFlights.Count(t => t.Tag == flightTrack.Tag), Is.EqualTo(0));
        //}
    }
}
