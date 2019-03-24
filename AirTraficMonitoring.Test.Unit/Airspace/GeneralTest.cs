using NUnit.Framework;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Test.Unit.Airspace
{
    [Author("AMBA")]

    [TestFixture]

    public class GeneralTest
    {
        private FlightAirspace.Airspace _asUUT;

        private FlightAddedEventArg _receivedEventArg;

        private FlightTrack _track;

        [SetUp]

        public void Setup()
        {
            _receivedEventArg = null;
            _asUUT = new FlightAirspace.Airspace(80000, 80000, 500, 20000);
            _track = new FlightTrack("ATR423", "39045", "12932", "14000", "20151006213456789");

            _asUUT.Add(_track);

            _asUUT.FlightAddedEvent += (x, args) => { _receivedEventArg = args; };
            
        }

        [Test]
        public void Checking_If_Listofflights_Is_NotEmpty_Equal_Not_Null()
        {
            var _testTrackOne = new FlightTrack("ATQ123", "52222", "45623", "600", "20190321155458675");
            _asUUT.Add(_testTrackOne);
            Assert.That(_receivedEventArg, Is.Not.Null);
        }

        [Test]
        public void Checking_If_Listofflights_Is_Empty_Equal_Null()
        {
            var _testTrackTwo = new FlightTrack("ATR423", "800000", "12932", "14000", "20151006213456789");
            _asUUT.Add(_testTrackTwo);
            Assert.That(_receivedEventArg, Is.Null);
        }

        [Test]
        public void Checking_If_Listofflights_Is_Empty_Equal_Null_NegativeValue()
        {
            var _testTrackThree = new FlightTrack("ATR423", "-800000", "12932", "14000", "20151006213456789");
            _asUUT.Add(_testTrackThree);
            Assert.That(_receivedEventArg, Is.Null);
        }

        [Test]
        public void Checking_If_Listofflights_Is_NotEmpty_Equal_Not_Null_Value_before_border()
        {
            var _testTrackFour = new FlightTrack("BRQ563", "79999", "79999", "500", "20190321155458675");
            _asUUT.Add(_testTrackFour);
            Assert.That(_receivedEventArg, Is.Not.Null);
        }


    }
}
