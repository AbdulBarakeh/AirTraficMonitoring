using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;
using AirTraficMonitoring.Validator;
using NSubstitute;
using NUnit.Framework;

namespace AirTraficMonitoring.Test.Unit.Validation
{
    [Author("GIG")]

    [TestFixture]
    public class FlightValidation
    {
        private const string Tag = "ATR345";
        private const string Time = "20151006213456789";

        private FlightValidator _uut;
        private IAirspace _airspace;

        [SetUp]
        public void Setup()
        {
            _uut = new FlightValidator();
            _airspace = Substitute.For<IAirspace>();

            _airspace.Height = 80000;
            _airspace.Width = 80000;
            _airspace.MaxAlt = 20000;
            _airspace.MinAlt = 500;
        }

        [TestCase("0", "0", "500")]
        [TestCase("1", "1", "501")]
        [TestCase("40000", "40000", "10000")]
        [TestCase("79999", "79999", "19999")]
        [TestCase("80000", "80000", "20000")]
        public void Validation_DifferentValidTracks_ReturnTrue(string xpos, string ypos, string alt)
        {
            var flightTrack = new FlightTrack(Tag, xpos, ypos, alt, Time);

            var result = _uut.Validate(_airspace, flightTrack);

            Assert.That(result, Is.True);
        }

        [TestCase("0", "0", "0")]
        [TestCase("1", "1", "1")]
        [TestCase("-1", "-1", "-1")]
        [TestCase("-1", "-1", "499")]
        [TestCase("-1", "20000", "500")]
        [TestCase("20000", "-1", "500")]
        [TestCase("20000", "20000", "-1")]
        [TestCase("20000", "20000", "499")]
        [TestCase("20000", "80001", "500")]
        [TestCase("80001", "20000", "500")]
        [TestCase("80001", "80001", "20001")]
        [TestCase("99999", "99999", "99999")]
        [TestCase("-80000", "-80000", "-20000")]
        public void Validation_DifferentNotValidTracks_ReturnFalse(string xpos, string ypos, string alt)
        {
            var flightTrack = new FlightTrack(Tag, xpos, ypos, alt, Time);

            var result = _uut.Validate(_airspace, flightTrack);

            Assert.That(result, Is.False);
        }
    }
}