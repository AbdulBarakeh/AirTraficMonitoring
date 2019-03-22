using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;
using AirTraficMonitoring.Validator;
using NUnit.Framework;

namespace AirTraficMonitoring.Test.Unit.Validation
{
    [Author("GIG")]

    [TestFixture]

    public class GeneralTests
    {
        private Validator.FlightValidator _vUUT;
        private Track.FlightTrack _testTrack;
        private FlightAirspace.Airspace _testAirspace;

        public void Setup()
        {
            _testAirspace = new FlightAirspace.Airspace(80000, 80000, 500, 20000);
            _testTrack = new FlightTrack("au589", 78000, 55000, 15000, "20190322093230000");

            _vUUT = new FlightValidator();
        }

        [Test]

        public void TestingValidateCorrectData()
        {
            var testTrack1 = new FlightTrack("au555", 78000, 55000, 15000, "20190322093230000");
            bool validationValue = _vUUT.Validate(_testAirspace, testTrack1);
            Assert.That(validationValue, Is.EqualTo(true));
        }

        [Test]
        public void TestingValidateWrongData()
        {
            var testTrack2 = new FlightTrack("auC556", 81000, 81000, 21000, "20190322093230001");
            bool validationValue1 = _vUUT.Validate(_testAirspace, testTrack2);
            Assert.That(validationValue1, Is.EqualTo(false));
        }

    }
}
